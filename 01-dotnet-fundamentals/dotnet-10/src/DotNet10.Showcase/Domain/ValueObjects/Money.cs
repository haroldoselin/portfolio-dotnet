namespace DotNet10.Showcase.Domain.ValueObjects
{
    public readonly record struct Money
    {
        public Money(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }

        public bool IsPositive
            => Amount > 0;

        public bool IsNegative
            => Amount < 0;

        public bool IsZero
            => Amount == 0;

        public static Money Zero
            => new(0);

        public static Money operator +(Money left, Money right)
            => new(left.Amount + right.Amount);

        public static Money operator -(Money left, Money right)
            => new(left.Amount - right.Amount);

        public static Money operator *(Money money, decimal multiplier)
            => new(money.Amount * multiplier);

        public override string ToString()
            => Amount.ToString("F2");
    }
}