using MediatR;

namespace OnlineBookstore.Api.MediatR.Command.ShoppingCart
{
    public class RemoveBookFromCartCommand : IRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
