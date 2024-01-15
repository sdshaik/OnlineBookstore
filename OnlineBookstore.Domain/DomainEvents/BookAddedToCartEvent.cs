using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Domain.DomainEvents
{
    public class BookAddedToCartEvent : IDomainEvent
    {
        public int UserId { get; }
        public int BookId { get; }
        public int Quantity { get; }
        public DateTime AddedAt { get; }

        public BookAddedToCartEvent(int userId, int bookId, int quantity, DateTime addedAt)
        {
            UserId = userId;
            BookId = bookId;
            Quantity = quantity;
            AddedAt = addedAt;
        }
    }
}
