using MediatR;
using OnlineBookstore.Domain.BookAggregate;

namespace OnlineBookstore.Api.MediatR.Query.Books
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
    }
}
