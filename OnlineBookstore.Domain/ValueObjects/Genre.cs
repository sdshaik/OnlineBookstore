
namespace OnlineBookstore.Domain.ValueObjects
{
    public class Genre : ValueObject
    {
        public string GenreName { get; private set; }

        public Genre()
        {
        }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GenreName;
        }
    }
}
