using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.Exceptions;
using OnlineBookstore.Domain.OrderAggregate;
using OnlineBookstore.Domain.OrderAggregate.OrderRepository;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;
using OnlineBookstore.Domain.UserAggregate.Interface;

namespace OnlineBookstore.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderService(IUserRepository userRepository, IOrderRepository orderRepository, IBookRepository bookRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var orderDtos = orders.Select(order => new OrderDto
            {
                OrderId = order.Id,
                UserId = order.User.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderPrice,
                OrderItems = order.Items.Select(item => new OrderItemDto
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity
                }).ToList()
            }).ToList();

            return orderDtos;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {orderId} not found");
            }

            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                UserId = order.User.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderPrice,
                OrderItems = order.Items.Select(item => new OrderItemDto
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity
                }).ToList()
            };

            return orderDto;
        }

        public async Task<OrderDto> PlaceOrderAsync(int userId, List<CartItemDto> cartItems)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found");
            }

            // Create order items from cart items
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                var book = await _bookRepository.GetByIdAsync(cartItem.BookId);

                if (book == null)
                {
                    throw new BookNotFoundException($"Book with ID {cartItem.BookId} not found");
                }

                var orderItem = new OrderItem(book.Id, cartItem.Quantity);

                orderItems.Add(orderItem);
            }
            var totalAmount = await CalculateTotalWithDiscount(cartItems);
            var order = new Order(user, DateTime.Now, orderItems, totalAmount);
            await _orderRepository.AddAsync(order);

            //clear the user's shopping cart
            await _shoppingCartRepository.RemoveAsync(userId);
            // Return order details
            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                UserId = order.User.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderPrice,
                OrderItems = order.Items.Select(item => new OrderItemDto
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity
                }).ToList()
            };

            return orderDto;
        }

        public async Task CancelOrderAsync(int orderId, string cancellationReason)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {orderId} not found");
            }

            //This will update the status and will raise the OrderCancelEvent
            order.CancelOrder(orderId, cancellationReason);
        }

        private async Task<decimal> CalculateTotalWithDiscount(List<CartItemDto> items)
        {
            decimal totalWithoutDiscount = await CalculateTotalWithoutDiscount(items);

            // Apply 10% discount if the order has three or more items
            if (items.Count >= 3)
            {
                decimal discountAmount = totalWithoutDiscount * 0.10m;
                return totalWithoutDiscount - discountAmount;
            }

            return totalWithoutDiscount;
        }

        private async Task<decimal> CalculateTotalWithoutDiscount(List<CartItemDto> items)
        {
            // Calculate the total without discount (sum of item prices)
            //return order.Items.Sum( item => item.Quantity * item.Price.Amount);
            //return items.Sum(item => item.Quantity * (_bookRepository.GetPriceByBookId(item.BookId).Result.Amount));
            decimal totalAmount = 0;
            foreach (var item in items)
            {
                var price = await _bookRepository.GetPriceByBookId(item.BookId);
                totalAmount += price.Amount * item.Quantity;
            }
            return totalAmount;
        }
    }
}
