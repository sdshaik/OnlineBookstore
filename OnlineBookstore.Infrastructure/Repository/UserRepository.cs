using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.UserAggregate;
using OnlineBookstore.Domain.UserAggregate.Interface;
using OnlineBookstore.Infrastructure.Persistence;

namespace OnlineBookstore.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public UserRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User entity)
        {
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userToRemove = await _dbContext.Users.FindAsync(id);
            if (userToRemove != null)
            {
                _dbContext.Users.Remove(userToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(us => us.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(us => us.Name == username);
        }

        public async Task UpdateAsync(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
