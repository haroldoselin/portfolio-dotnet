using CSharp14.Showcase.Domain.Enums;
using CSharp14.Showcase.Domain.ValueObjects;

namespace CSharp14.Showcase.Domain.Entities
{
    public sealed class Transaction
    {
        public required Guid Id { get; init; }

        public required string Description { get; init; }

        public required Money Amount { get; init; }

        public required TransactionType Type { get; init; }

        public string? Category { get; private set; }

        public DateTime CreatedAt { get; init; }

        public bool IsCategorized =>
            !string.IsNullOrWhiteSpace(Category);

        public void Categorize(string category)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(category);

            Category = category.Trim();
        }

        public void RemoveCategory()
        {
            Category = null;
        }
    }
}