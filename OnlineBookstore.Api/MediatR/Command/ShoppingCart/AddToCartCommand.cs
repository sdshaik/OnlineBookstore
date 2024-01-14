using MediatR;
using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.MediatR.Command.ShoppingCart
{
    public class AddToCartCommand : IRequest<ShoppingCartDto>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
