using MediatR;
using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.MediatR.Query.ShoppingCart
{
    public class GetShoppingCartQuery : IRequest<ShoppingCartDto>
    {
        public int UserId { get; set; }
    }
}
