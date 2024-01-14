using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookstore.Domain.BookAggregate.Interface;
using OnlineBookstore.Domain.OrderAggregate.OrderRepository;
using OnlineBookstore.Domain.ShoppingAggregate.Interface;
using OnlineBookstore.Domain.UserAggregate.Interface;
using OnlineBookstore.Infrastructure.Persistence;
using OnlineBookstore.Infrastructure.Repository;

namespace OnlineBookstore.Infrastructure.Configuration
{
    public static class ServicesCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookstoreDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
