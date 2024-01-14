using Moq;
using OnlineBookstore.Domain.BookAggregate;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.ShoppingAggregate;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;
using OnlineBookstore.Domain.ValueObjects;

namespace OnlineBookstore.Api.Services.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTests
    {
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IShoppingCartRepository> _mockShoppingCartRepository;
        private ShoppingCartService _shoppingCartService;

        [SetUp]
        public void SetUp()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockShoppingCartRepository = new Mock<IShoppingCartRepository>();
            _shoppingCartService = new ShoppingCartService(_mockBookRepository.Object, _mockShoppingCartRepository.Object);
        }

        [Test]
        public async Task AddBookToCart_ValidBook_ReturnsShoppingCartDto()
        {
            // Arrange
            var userId = 1;
            var bookId = 1;
            var quantity = 2;

            var book = new Book(1, "Sample Book", new Author(1, "test@email.com", "John Doe", "null"), 20.0m, "USD", new Genre("Fiction"));

            _mockBookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(book);
            _mockShoppingCartRepository.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync((ShoppingCart)null);

            // Act
            var result = await _shoppingCartService.AddBookToCart(userId, bookId, quantity);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(1, result.CartItems.Count);
            Assert.AreEqual(bookId, result.CartItems[0].BookId);
            Assert.AreEqual(quantity, result.CartItems[0].Quantity);

            _mockShoppingCartRepository.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Test]
        public async Task RemoveBookFromCart_ValidBook_RemovesBookFromCart()
        {
            // Arrange
            var userId = 1;
            var bookId = 1;

            var shoppingCart = new ShoppingCart(userId, new List<CartItem>
            {
                new CartItem(bookId, 2)
            });

            _mockShoppingCartRepository.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync(shoppingCart);

            // Act
            await _shoppingCartService.RemoveBookFromCart(userId, bookId);

            // Assert
            Assert.AreEqual(0, shoppingCart.CartItems.Count);
            _mockShoppingCartRepository.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<ShoppingCart>()), Times.Once);
        }
    }
}
