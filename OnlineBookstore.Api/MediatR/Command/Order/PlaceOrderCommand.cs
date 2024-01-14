using MediatR;
using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.MediatR.Command.Order
{
    public class PlaceOrderCommand : IRequest<OrderDto>
    {
        public int UserId { get; }
        public List<CartItemDto> CartItems { get; }

        public PlaceOrderCommand(int userId, List<CartItemDto> cartItems)
        {
            UserId = userId;
            CartItems = cartItems;
        }
    }
}
