using MediatR;
using OnlineBookstore.Api.Models.Dto;

namespace OnlineBookstore.Api.MediatR.Query.Order
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }
}
