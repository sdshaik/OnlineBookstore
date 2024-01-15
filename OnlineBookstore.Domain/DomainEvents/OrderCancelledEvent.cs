using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Domain.DomainEvents
{
    public class OrderCancelledEvent : IDomainEvent
    {
        public int OrderId { get; }
        public DateTime OrderCancelledAt { get; }
        public string CancellationReason { get; }

        public OrderCancelledEvent(int orderId, DateTime orderCancelledAt, string cancellationReason)
        {
            OrderId = orderId;
            OrderCancelledAt = orderCancelledAt;
            CancellationReason = cancellationReason;
        }
    }
}
