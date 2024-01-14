using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.SeedWork;

namespace OnlineBookstore.Api.Events.EventHandlers
{
    public class BookAddedToCartEventHandler : IDomainEventHandler<BookAddedToCartEvent>
    {
        private readonly ILogger<BookAddedToCartEventHandler> _logger;

        public BookAddedToCartEventHandler(ILogger<BookAddedToCartEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BookAddedToCartEvent domainEvent)
        {
            _logger.LogInformation($"Book with ID {domainEvent.BookId} was added by UserId = {domainEvent.UserId}.");
            return Task.CompletedTask;
        }
    }
}
