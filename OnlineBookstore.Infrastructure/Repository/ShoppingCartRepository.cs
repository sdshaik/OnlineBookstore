using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;
using OnlineBookstore.Infrastructure.Persistence;

namespace OnlineBookstore.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public ShoppingCartRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddOrUpdateAsync(ShoppingCart shoppingCart)
        {
            var existingCart = await _dbContext.ShoppingCarts
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == shoppingCart.UserId);

            if (existingCart != null)
            {
                existingCart.UpdateCartItems(shoppingCart.CartItems);
            }
            else
            {
                // Add new cart
                _dbContext.ShoppingCarts.Add(shoppingCart);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetByUserIdAsync(int userId)
        {
            return await _dbContext.ShoppingCarts
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task RemoveAsync(int userId)
        {
            var cartToRemove = await _dbContext.ShoppingCarts
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (cartToRemove != null)
            {
                _dbContext.ShoppingCarts.Remove(cartToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
