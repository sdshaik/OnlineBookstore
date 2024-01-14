using MediatR;
using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Api.Services;
using OnlineBookstore.Domain.Exceptions;

namespace OnlineBookstore.Api.MediatR.Query.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(request.OrderId);

                if (order == null)
                {
                    throw new OrderNotFoundException($"Order with ID {request.OrderId} not found");
                }
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in {0}", nameof(GetOrderByIdQueryHandler));
                throw new ApplicationException($"Error getting order by ID: {ex.Message}");
            }
        }
    }
}
