using MediatR;
using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Api.Services;

namespace OnlineBookstore.Api.MediatR.Command.ShoppingCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ShoppingCartDto>
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<AddToCartCommandHandler> _logger;

        public AddToCartCommandHandler(IShoppingCartService shoppingCartService, ILogger<AddToCartCommandHandler> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        public async Task<ShoppingCartDto> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _shoppingCartService.AddBookToCart(request.UserId, request.BookId, request.Quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in {0}", nameof(AddToCartCommandHandler));
                throw;
            }
        }
    }
}
