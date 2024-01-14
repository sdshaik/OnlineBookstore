using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.OrderAggregate;
using OnlineBookstore.Domain.OrderAggregate.OrderRepository;
using OnlineBookstore.Infrastructure.Persistence;

namespace OnlineBookstore.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookstoreDbContext _dbContext;


        public OrderRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderToRemove = await _dbContext.Orders.FindAsync(id);
            if (orderToRemove != null)
            {
                _dbContext.Orders.Remove(orderToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
