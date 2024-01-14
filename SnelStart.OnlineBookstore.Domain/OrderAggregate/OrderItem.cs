namespace OnlineBookstore.Domain.OrderAggregate
{
    public class OrderItem
    {
        public int BookId { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(int bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }
    }
}
