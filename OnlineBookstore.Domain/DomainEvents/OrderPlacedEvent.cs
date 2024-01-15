using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Domain.DomainEvents
{
    public record OrderPlacedEvent : IDomainEvent
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime OrderPlacedAt { get; set; }

        public OrderPlacedEvent(int orderId, DateTime orderPlacedAt, int userId, decimal amount)
        {
            OrderId = orderId;
            OrderPlacedAt = orderPlacedAt;
            UserId = userId;
            OrderAmount = amount;
        }
    }


}
