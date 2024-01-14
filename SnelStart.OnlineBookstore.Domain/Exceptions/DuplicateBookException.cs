namespace OnlineBookstore.Domain.Exceptions
{
    public class DuplicateBookException : Exception
    {
        public DuplicateBookException(string message) : base(message)
        {
        }
    }
}
