using MediatR;
using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Api.Services;

namespace OnlineBookstore.Api.MediatR.Query.ShoppingCart
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartDto>
    {
        private readonly ILogger<GetShoppingCartQueryHandler> _logger;
        private readonly IShoppingCartService _shoppingCartService;

        public GetShoppingCartQueryHandler(ILogger<GetShoppingCartQueryHandler> logger, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<ShoppingCartDto> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _shoppingCartService.GetShoppingCartAsync(request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in {0}", nameof(GetShoppingCartQueryHandler));
                throw;
            }
        }
    }
}
