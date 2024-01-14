using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> AddBookToCart(int userId, int bookId, int quantity);
        Task RemoveBookFromCart(int userId, int bookId);
        Task<ShoppingCartDto> GetShoppingCartAsync(int userId);
    }
}
