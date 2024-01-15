using OnlineBookstore.Domain.Repository;
using OnlineBookstore.Domain.ValueObjects;

namespace OnlineBookstore.Domain.BookAggregate.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByAuthorAsync(Author author);
        Task<Price> GetPriceByBookId(int id);
    }

}
