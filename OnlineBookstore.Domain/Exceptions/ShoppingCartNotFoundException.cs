namespace OnlineBookstore.Domain.Exceptions
{
    public class ShoppingCartNotFoundException : Exception
    {
        public ShoppingCartNotFoundException(string message) : base(message)
        {
        }
    }
}
