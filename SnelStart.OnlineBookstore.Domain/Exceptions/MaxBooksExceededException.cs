namespace OnlineBookstore.Domain.Exceptions
{
    public class MaxBooksExceededException : Exception
    {
        public MaxBooksExceededException(string message) : base(message)
        {
        }
    }
}
