using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Api.Events.EventHandlers
{
    public class OrderCancelledEventHandler : IDomainEventHandler<OrderCancelledEvent>
    {
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderCancelledEventHandler(ILogger<OrderPlacedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCancelledEvent domainEvent)
        {
            _logger.LogInformation($"Order with ID {domainEvent.OrderId} was canceld at {domainEvent.OrderCancelledAt}.");
            //other logic , we can call other service like sendemailservice 
            return Task.CompletedTask;
        }
    }
}
