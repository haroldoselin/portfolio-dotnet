namespace CSharp14.Showcase.Application.Models
{
    public sealed class ProcessingResult
    {
        public required string CollectionType { get; init; }

        public int TotalTransactions { get; init; }

        public decimal TotalAmount { get; init; }

        public int ExpenseCount { get; init; }

        public int IncomeCount { get; init; }
    }
}
