namespace OnlineBookstore.Api.Models.Dto
{
    public class ShoppingCartDto
    {
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalPrice { get; set; }

    }

    public class CartItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

    }
}
