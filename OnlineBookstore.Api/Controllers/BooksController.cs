using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Api.MediatR.Query.Books;

namespace OnlineBookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var res = await _mediator.Send(new GetAllBooksQuery());
            return Ok(res);
        }
    }
}
