namespace OnlineBookstore.Domain.BookAggregate
{
    public class Author
    {

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string? Email { get; set; }

        public string? ProfileUrl { get; private set; }

        public string AboutAuthor { get; private set; }

        public Author(int id, string name, string email, string profileUrl)
        {
            Id = id;
            Name = name;
            Email = email;
            ProfileUrl = profileUrl;

        }
    }
}
