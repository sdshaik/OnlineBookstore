using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.BookAggregate;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.ValueObjects;
using OnlineBookstore.Infrastructure.Persistence;

namespace OnlineBookstore.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public BookRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeData();
        }

        public void InitializeData()
        {
            var authors = new List<Author>
        {
            new Author(1,"Joydip Kanjilal", "joydip@example.com", "Null"),
            new Author(2,"Joydip Kanjilal", "joydip@example.com", "Null")
        };

            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();

            var books = new List<Book>
        {
            new Book(1,"Mastering C# 8.0", authors[0], new Price( 29.99m, "USD"), new Genre("Programming")),
            new Book(2,"Let us C", authors[1],new Price ( 19.99m, "USD"), new Genre("Programming"))
        };

            _dbContext.Books.AddRange(books);
            _dbContext.SaveChanges();
        }



        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookToRemove = await _dbContext.Books.FindAsync(id);
            if (bookToRemove != null)
            {
                _dbContext.Books.Remove(bookToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetByAuthorAsync(Author author)
        {
            return await _dbContext.Books.Where(b => b.Author.Name == author.Name).ToListAsync();
        }

        public async Task<Price> GetPriceByBookId(int id)
        {
            return await _dbContext.Books.Where(x => x.Id == id).Select(x => x.Price).FirstOrDefaultAsync();
        }
    }
}
