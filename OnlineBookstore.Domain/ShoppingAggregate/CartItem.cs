namespace OnlineBookstore.Domain.ShoppingAggregate
{
    public class CartItem
    {
        public int BookId { get; private set; }
        public int Quantity { get; private set; }

        public CartItem(int bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }
    }
}
