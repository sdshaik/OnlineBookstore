using OnlineBookstore.Domain.SeedWork;
using OnlineBookstore.Domain.ValueObjects;

namespace OnlineBookstore.Domain.BookAggregate
{
    public class Book : BaseEntity, IAggregateRoot
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public Author Author { get; private set; } //author is an diffrent entity

        public Price Price { get; private set; } //price is an valueobject

        public Genre Genre { get; private set; } //genre is an valueobject

        public Book()
        {

        }

        public Book(int id, string title, Author author, decimal amount, string currency, Genre genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = new Price(amount, currency);
            Genre = genre;
        }

        public Book(int id, string title, Author author, Price price, Genre genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
            Genre = genre;
        }
    }
}
