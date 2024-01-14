using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Api.Events.EventHandlers
{
    public class OrderPlacedEventHandler : IDomainEventHandler<OrderPlacedEvent>
    {
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(ILogger<OrderPlacedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderPlacedEvent domainEvent)
        {
            _logger.LogInformation($"Order with ID {domainEvent.OrderId} was placed at {domainEvent.OrderPlacedAt}.");
            return Task.CompletedTask;
        }
    }
}
