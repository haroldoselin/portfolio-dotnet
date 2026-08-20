using CSharp14.Showcase.Application.Services;
using CSharp14.Showcase.Domain.Entities;
using CSharp14.Showcase.Domain.Enums;
using CSharp14.Showcase.Domain.ValueObjects;

var transactions = new List<Transaction>
{
    new()
    {
        Id = Guid.NewGuid(),
        Description = "Salário",
        Amount = new Money(5000m),
        Type = TransactionType.Renda,
        CreatedAt = DateTime.UtcNow
    },

    new()
    {
        Id = Guid.NewGuid(),
        Description = "Supermercado",
        Amount = new Money(-350m),
        Type = TransactionType.Gasto,
        CreatedAt = DateTime.UtcNow
    },

    new()
    {
        Id = Guid.NewGuid(),
        Description = "Restaurante",
        Amount = new Money(-120m),
        Type = TransactionType.Gasto,
        CreatedAt = DateTime.UtcNow
    },

    new()
    {
        Id = Guid.NewGuid(),
        Description = "Pix",
        Amount = new Money(-300m),
        Type = TransactionType.Transferencia,
        CreatedAt = DateTime.UtcNow
    }
};

var processor = new TransactionProcessor();

Console.WriteLine("======================================");
Console.WriteLine(" C# 14 - SENIOR SHOWCASE");
Console.WriteLine("======================================");

Console.WriteLine();

Console.WriteLine(
    $"Collection: {processor.GetCollectionTypeName()}");

Console.WriteLine();

foreach (var transaction in transactions)
{
    Console.WriteLine(
        $"{transaction.Description} | " +
        $"{transaction.Type} | " +
        $"{transaction.Amount}");
}

Console.WriteLine();

var result = processor.Process(transactions);

Console.WriteLine("=== PROCESSAMENTO ===");

Console.WriteLine(
    $"Total de transações: {result.TotalTransactions}");

Console.WriteLine(
    $"Valor total: {result.TotalAmount:C}");

Console.WriteLine(
    $"Despesas: {result.ExpenseCount}");

Console.WriteLine(
    $"Receitas: {result.IncomeCount}");

Console.WriteLine();

var customer = new Customer
{
    Name = "Cliente Inicial"
};

TransactionProcessor.RenameCustomer(
    customer,
    " Cliente Atualizado ");

Console.WriteLine(
    $"Cliente: {customer.Name}");

Console.WriteLine();

var parser = new TransactionAmountParser();

if (parser.TryParse("250.50", out var parsedAmount))
{
    Console.WriteLine(
        $"Valor convertido: {parsedAmount:C}");
}

Console.WriteLine();

ReadOnlySpan<char> spanInput = "99.90";

var spanAmount =
    CSharp14.Showcase.Performance.TransactionParser
        .ParseWithSpan(spanInput);

Console.WriteLine(
    $"Valor usando ReadOnlySpan: {spanAmount:C}");