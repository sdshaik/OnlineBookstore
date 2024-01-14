using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.Services
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrderAsync(int userId, List<CartItemDto> cartItems);
        Task<OrderDto> GetOrderAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task CancelOrderAsync(int orderId, string cancellationReason);
    }
}

