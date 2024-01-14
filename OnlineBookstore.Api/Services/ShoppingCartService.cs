using OnlineBookstore.Api.Models.Dto;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Exceptions;
using OnlineBookstore.Domain.ShoppingAggregate;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;

namespace OnlineBookstore.Api.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private const int MaxBooksPerUser = 10;

        public ShoppingCartService(IBookRepository bookRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _bookRepository = bookRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCartDto> AddBookToCart(int userId, int bookId, int quantity)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new BookNotFoundException($"Book with ID {bookId} not found");
            }

            var cart = await _shoppingCartRepository.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart(userId, new List<CartItem>());
            }

            try
            {
                cart.AddItem(userId, new CartItem(book.Id, quantity), MaxBooksPerUser);

                await _shoppingCartRepository.AddOrUpdateAsync(cart);

                var cartDto = new ShoppingCartDto
                {
                    UserId = cart.UserId,
                    CartItems = cart.CartItems.Select(item => new CartItemDto
                    {
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                    }).ToList()
                };

                return cartDto;
            }
            catch (MaxBooksExceededException ex)
            {
                throw ex;
            }
            catch (DuplicateBookException ex)
            {
                throw ex;
            }
        }

        public async Task RemoveBookFromCart(int userId, int bookId)
        {
            var cart = await _shoppingCartRepository.GetByUserIdAsync(userId);
            if (cart == null)
            {
                throw new ShoppingCartNotFoundException($"Shopping cart not found for user ID {userId}");
            }

            var itemToRemove = cart.CartItems.FirstOrDefault(item => item.BookId == bookId);
            if (itemToRemove == null)
            {
                throw new BookNotFoundException($"Book with ID {bookId} not found in the shopping cart");
            }

            cart.CartItems.Remove(itemToRemove);

            await _shoppingCartRepository.AddOrUpdateAsync(cart);
        }

        public async Task<ShoppingCartDto> GetShoppingCartAsync(int userId)
        {
            var cart = await _shoppingCartRepository.GetByUserIdAsync(userId);

            if (cart == null)
            {
                // If the cart doesn't exist,creating an empty cart 
                cart = new ShoppingCart(userId, new List<CartItem>());
            }

            // Calculate total price
            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                var book = _bookRepository.GetByIdAsync(item.BookId).Result;

                if (book != null)
                {
                    totalPrice += book.Price.Amount * item.Quantity;
                }
            }
            var cartDto = new ShoppingCartDto
            {
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                }).ToList(),
                TotalPrice = totalPrice
            };

            return cartDto;
        }

    }


}
