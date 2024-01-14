using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Domain.ShoppingAggregate.Interface
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetByUserIdAsync(int userId);
        Task AddOrUpdateAsync(ShoppingCart shoppingCart);
        Task RemoveAsync(int userId);
    }
}
