using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.Exceptions;
using OnlineBookstore.Domain.SeedWork;
using OnlineBookstore.Domain.UserAggregate;

namespace OnlineBookstore.Domain.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public int Id { get; private set; }

        public User User { get; private set; }

        public DateTime OrderDate { get; private set; }

        public decimal OrderPrice { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public string Status { get; private set; }

        private Order()
        {
            Items = new List<OrderItem>();
        }

        public Order(User user, DateTime orderDate, List<OrderItem> orderItems, decimal totalAmount)
        {
            User = user;
            OrderDate = orderDate;
            Items = orderItems;
            OrderPrice = totalAmount;
            Status = "Placed";

            // Raise the OrderPlacedEvent when a new order is created
            AddDomainEvent(new OrderPlacedEvent(Id, OrderDate, user.Id, totalAmount));
        }

        public void CancelOrder(int orderId, string cancellationReason)
        {
            if (OrderPrice == 0 || OrderDate < DateTime.Now)
            {
                throw new ValidationException("Cannot cancel this order.");
            }
            Status = $"canceld, reason : {cancellationReason}";
            AddDomainEvent(new OrderCancelledEvent(orderId, DateTime.Now, cancellationReason));
        }


    }
}
