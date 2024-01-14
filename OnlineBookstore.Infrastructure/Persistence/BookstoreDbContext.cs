using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.BookAggregate;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.OrderAggregate;
using OnlineBookstore.Domain.UserAggregate;

namespace OnlineBookstore.Infrastructure.Persistence
{
    public class BookstoreDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=bookstore.db");
            optionsBuilder.UseInMemoryDatabase(databaseName: "bookstoreDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne(b => b.Author);
            modelBuilder.Entity<Book>().OwnsOne(b => b.Price);
            modelBuilder.Entity<Book>().OwnsOne(b => b.Genre);
            modelBuilder.Entity<ShoppingCart>().OwnsMany(s => s.CartItems);
            modelBuilder.Entity<Order>().OwnsMany(o => o.Items);
            modelBuilder.Entity<User>(builder =>
            {
                builder.OwnsOne(u => u.Address, addressBuilder =>
                {
                    addressBuilder.Property(a => a.Street).HasColumnName("Street");
                    addressBuilder.Property(a => a.City).HasColumnName("City");
                    addressBuilder.Property(a => a.Country).HasColumnName("Country");
                    addressBuilder.Property(a => a.State).HasColumnName("State");
                    addressBuilder.Property(a => a.ZipCode).HasColumnName("ZipCode");
                });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}