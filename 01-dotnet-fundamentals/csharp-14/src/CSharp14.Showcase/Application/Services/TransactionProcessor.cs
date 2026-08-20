using CSharp14.Showcase.Application.Models;
using CSharp14.Showcase.Domain.Entities;
using CSharp14.Showcase.Extensions;

namespace CSharp14.Showcase.Application.Services
{
    public sealed class TransactionProcessor
    {
        public string GetCollectionTypeName()
        {
            return nameof(List<>);
        }

        public static void RenameCustomer(Customer? customer, string newName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newName);

            customer?.Name = newName.Trim();
        }

        public static int CountTransactions(
            IEnumerable<Transaction> transactions)
        {
            ArgumentNullException.ThrowIfNull(transactions);

            return transactions.Count();
        }

        public static decimal CalculateTotal(
            IEnumerable<Transaction> transactions)
        {
            ArgumentNullException.ThrowIfNull(transactions);

            return transactions.Sum(
                transaction => transaction.Amount.Amount);
        }

        public IReadOnlyList<Transaction> GetExpenses(
            IEnumerable<Transaction> transactions)
        {
            ArgumentNullException.ThrowIfNull(transactions);

            return [.. transactions.Where(transaction => transaction.IsExpense)];
        }

        public ProcessingResult Process(
            IEnumerable<Transaction> transactions)
        {
            ArgumentNullException.ThrowIfNull(transactions);

            var transactionList = transactions.ToList();

            return new ProcessingResult
            {
                CollectionType = nameof(List<>),
                TotalTransactions = transactionList.Count,
                TotalAmount = transactionList.Sum(
                    transaction => transaction.Amount.Amount),
                ExpenseCount = transactionList.Count(
                    transaction => transaction.IsExpense),
                IncomeCount = transactionList.Count(
                    transaction => transaction.IsIncome)
            };
        }
    }
}
