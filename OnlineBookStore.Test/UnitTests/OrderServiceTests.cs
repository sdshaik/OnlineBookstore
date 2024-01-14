using Moq;
using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Api.Services;
using OnlineBookstore.Domain.BookAggregate;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.OrderAggregate;
using OnlineBookstore.Domain.OrderAggregate.OrderRepository;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;
using OnlineBookstore.Domain.UserAggregate;
using OnlineBookstore.Domain.UserAggregate.Interface;
using OnlineBookstore.Domain.ValueObjects;

namespace OnlineBookStore.Test.UnitTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService _orderService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IShoppingCartRepository> _mockShoppingCartRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockShoppingCartRepository = new Mock<IShoppingCartRepository>();

            _orderService = new OrderService(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockBookRepository.Object,
                _mockShoppingCartRepository.Object
            );
        }

        [Test]
        public async Task PlaceOrderAsync_ValidOrder_ReturnsOrderDto()
        {
            // Arrange
            var userId = 1;
            var cartItems = new List<CartItemDto>
            {
                new CartItemDto { BookId = 101, Quantity = 2 },
                new CartItemDto { BookId = 102, Quantity = 1 }
            };

            var user = new User(1, "TestUser", "test@example.com", null);
            var author = new Author(1, "Testt@email.com", "John Doe", "null");
            var genre = new Genre("Fiction");


            var book1 = new Book(101, "Book1", author, 20, "USD", genre);
            var book2 = new Book(102, "Book2", author, 30, "USD", genre);

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int bookId) => bookId switch
                {
                    101 => book1,
                    102 => book2,
                    _ => null
                });

            // Act
            var orderDto = await _orderService.PlaceOrderAsync(userId, cartItems);

            // Assert
            Assert.IsNotNull(orderDto);
            Assert.AreEqual(userId, orderDto.UserId);
            Assert.AreEqual(2, orderDto.OrderItems.Count);
        }

        [Test]
        public async Task CancelOrderAsync_ValidOrder_CallsCancelOrder()
        {
            // Arrange
            var orderId = 1;
            var cancellationReason = "Not available";

            var order = new Order(new User(1, "TestUser", "test@example.com", null), DateTime.Now, new List<OrderItem>(), 50);

            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            await _orderService.CancelOrderAsync(orderId, cancellationReason);

            // Assert
            _mockOrderRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Once);

            // Assert.AreEqual(OrderStatus.Canceled, order.Status);
            Assert.AreEqual(cancellationReason, order.Status);
        }

        [Test]
        public async Task PlaceOrderAsync_WithoutDiscount()
        {
            var userId = 1;
            var cartItems = new List<CartItemDto>
            {
                new CartItemDto { BookId = 1, Quantity = 1 },
                new CartItemDto { BookId = 2, Quantity = 1 }
            };

            var orderService = new OrderService(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockBookRepository.Object,
                _mockShoppingCartRepository.Object
            );

            _mockBookRepository.Setup(repo => repo.GetPriceByBookId(It.IsAny<int>()))
               .ReturnsAsync(new Price(50, "usd"));

            var orderDto = await orderService.PlaceOrderAsync(userId, cartItems);

            Assert.IsNotNull(orderDto);
            Assert.AreEqual(100, orderDto.TotalAmount); // 2 books * $50 = $100
        }

        [Test]
        public async Task PlaceOrderAsync_WithDiscount()
        {
            var userId = 1;
            var cartItems = new List<CartItemDto>
            {
                new CartItemDto { BookId = 1, Quantity = 2 },
                new CartItemDto { BookId = 2, Quantity = 2 },
                new CartItemDto { BookId = 3, Quantity = 2 }
            };

            var orderService = new OrderService(
                _mockUserRepository.Object,
                _mockOrderRepository.Object,
                _mockBookRepository.Object,
                _mockShoppingCartRepository.Object
            );

            _mockBookRepository.Setup(repo => repo.GetPriceByBookId(It.IsAny<int>()))
                .ReturnsAsync(new Price(50, "usd"));

            var orderDto = await orderService.PlaceOrderAsync(userId, cartItems);

            Assert.IsNotNull(orderDto);
            Assert.AreEqual(270, orderDto.TotalAmount);
        }
    }
}
