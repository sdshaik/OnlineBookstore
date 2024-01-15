namespace OnlineBookstore.Domain.DomainEvents
{
    public class UserRegisteredEvent
    {
        public int UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public DateTime RegisteredAt { get; }

        public UserRegisteredEvent(int userId, string userName, string email, DateTime registeredAt)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            RegisteredAt = registeredAt;
        }
    }
}
