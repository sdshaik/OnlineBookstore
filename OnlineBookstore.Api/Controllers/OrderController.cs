using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Api.MediatR.Command.Order;
using OnlineBookstore.Api.MediatR.Query.Order;

namespace OnlineBookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderCommand placeOrderCommand)
        {
            var result = await _mediator.Send(placeOrderCommand);
            return Ok(result);
        }

        [HttpGet("OrderId")]
        public async Task<IActionResult> GetOrderById(int userId)
        {
            var query = new GetOrderByIdQuery { OrderId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}