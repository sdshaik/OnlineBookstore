using System;

namespace OnlineBookstore.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Validation error occurred in the domain.") { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string entity, string field, string description)
            : base($"Validation error in {entity} for field '{field}': {description}") { }


        // For exceeding the maximum number of books in the cart per user
        public ValidationException(int maxBooksPerUser)
            : base($"Cannot add more than {maxBooksPerUser} books to the cart per user.") { }

        // For attempting to add a duplicate book to the cart
        public ValidationException(int bookId, string entity)
            : base($"Book with ID {bookId} is already in the {entity}.") { }

        // For book not found in the cart
        public ValidationException(int bookId, string entity, string action)
            : base($"Book with ID {bookId} not found in the {entity} while trying to {action}.") { }

        // Other custom exceptions related to the domain...
    }
}
