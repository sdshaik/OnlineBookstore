namespace OnlineBookstore.Domain.SeedWork
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
