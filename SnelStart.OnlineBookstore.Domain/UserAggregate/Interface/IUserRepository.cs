using OnlineBookstore.Domain.Repository;

namespace OnlineBookstore.Domain.UserAggregate.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
    }
}
