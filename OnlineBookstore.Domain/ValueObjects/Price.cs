namespace OnlineBookstore.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Price Create(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Price must be positive.");
            }

            return new Price(amount, currency);
        }

        public void Update(decimal newAmount, string newCurrency)
        {
            if (newAmount <= 0)
            {
                throw new ArgumentException("Price must be positive.");
            }

            Amount = newAmount;
            Currency = newCurrency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        internal void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        internal void SetCurrency(string currency)
        {
            Currency = currency;
        }
    }
}
