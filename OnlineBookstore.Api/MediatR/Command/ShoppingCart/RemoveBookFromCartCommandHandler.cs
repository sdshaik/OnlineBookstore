using MediatR;
using OnlineBookstore.Api.Services;

namespace OnlineBookstore.Api.MediatR.Command.ShoppingCart
{
    public class RemoveBookFromCartCommandHandler : IRequestHandler<RemoveBookFromCartCommand>
    {
        private readonly ILogger<RemoveBookFromCartCommandHandler> _logger;
        private readonly IShoppingCartService _shoppingCartService;

        public RemoveBookFromCartCommandHandler(ILogger<RemoveBookFromCartCommandHandler> logger, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        public async Task Handle(RemoveBookFromCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _shoppingCartService.RemoveBookFromCart(request.UserId, request.BookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in {0}", nameof(RemoveBookFromCartCommandHandler));
                throw;
            }
        }
    }
}
