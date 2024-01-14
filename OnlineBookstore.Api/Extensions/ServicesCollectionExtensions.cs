using OnlineBookstore.Api.Events;
using OnlineBookstore.Api.Events.EventHandlers;
using OnlineBookstore.Api.Services;
using OnlineBookstore.Domain.DomainEvents;
using OnlineBookstore.Domain.SeedWork;
using System.Reflection;

namespace OnlineBookstore.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddDbServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IDomainEventHandler<BookAddedToCartEvent>, BookAddedToCartEventHandler>();
            services.AddScoped<IDomainEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
            services.AddScoped<IDomainEventHandler<OrderCancelledEvent>, OrderCancelledEventHandler>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
