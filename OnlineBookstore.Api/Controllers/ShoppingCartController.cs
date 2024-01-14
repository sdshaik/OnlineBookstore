using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Api.MediatR.Command.ShoppingCart;
using OnlineBookstore.Api.MediatR.Query.ShoppingCart;

namespace OnlineBookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(AddToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetShoppingCart(int userId)
        {
            var query = new GetShoppingCartQuery { UserId = userId };
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpDelete("{bookId, userId}")]
        public async Task<IActionResult> RemoveBookFromCart(int bookId, int userId)
        {
            var query = new RemoveBookFromCartCommand { BookId = bookId, UserId = userId };
            await _mediator.Send(query);
            return Ok();
        }
    }
}
