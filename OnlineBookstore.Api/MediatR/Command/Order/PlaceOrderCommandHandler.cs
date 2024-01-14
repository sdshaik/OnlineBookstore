using MediatR;
using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Api.Services;

namespace OnlineBookstore.Api.MediatR.Command.Order
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderDto>
    {
        private readonly ILogger<PlaceOrderCommandHandler> _logger;
        private readonly IOrderService _orderService;

        public PlaceOrderCommandHandler(ILogger<PlaceOrderCommandHandler> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<OrderDto> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderDto = await _orderService.PlaceOrderAsync(request.UserId, request.CartItems);
                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in {0}", nameof(PlaceOrderCommandHandler));
                throw new ApplicationException($"Error placing order: {ex.Message}");
            }
        }
    }
}
