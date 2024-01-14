using OnlineBookstore.Domain.SeedWork;
using OnlineBookstore.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookstore.Domain.UserAggregate
{
    public class User : IAggregateRoot
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        [EmailAddress]
        public string Email { get; private set; }

        public string? ProfileUrl { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public DateTime? DeletedDate { get; private set; }

        public Address Address { get; private set; } //this is an ValueObject

        public User(int id, string name, string email, string profileUrl)
        {
            Id = id;
            Name = name;
            Email = email;
            ProfileUrl = profileUrl;
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
