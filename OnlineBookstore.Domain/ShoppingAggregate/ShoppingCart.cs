using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.SeedWork;
using OnlineBookstore.Domain.ShoppingAggregate;

namespace OnlineBookstore.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; private set; }
        public List<CartItem> CartItems { get; private set; }

        public ShoppingCart(int userId, List<CartItem> cartItems)
        {
            UserId = userId;
            CartItems = cartItems;
        }

        private ShoppingCart()
        {
            CartItems = new List<CartItem>();
        }
        public void AddItem(int userId, CartItem newItem, int maxBooksPerUser)
        {
            //Cannot exceed the maximum number of books in the cart per user
            if (CartItems.Count >= maxBooksPerUser)
            {
                throw new Exceptions.ValidationException(maxBooksPerUser);
            }

            foreach (var item in CartItems)
            {
                if (item.BookId == newItem.BookId)
                {
                    throw new Exceptions.ValidationException(newItem.BookId, "ShoppingCart", "add");
                }
            }

            CartItems.Add(newItem);

            //Raise the BookAddedToCartEvent
            AddDomainEvent(new BookAddedToCartEvent(userId, newItem.BookId, newItem.Quantity, DateTime.Now));
        }

        public void UpdateCartItems(List<CartItem> newCartItems)
        {
            CartItems = newCartItems;
        }

        public decimal CalculateTotalPrice(IBookRepository bookRepository)
        {
            decimal totalPrice = 0;

            foreach (var item in CartItems)
            {
                var book = bookRepository.GetByIdAsync(item.BookId).Result;

                if (book != null)
                {
                    totalPrice += book.Price.Amount * item.Quantity;
                }
            }

            return totalPrice;
        }

    }
}
