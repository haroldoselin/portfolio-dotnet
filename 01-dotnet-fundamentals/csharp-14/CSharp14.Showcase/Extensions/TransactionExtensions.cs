using CSharp14.Showcase.Domain.Entities;
using CSharp14.Showcase.Domain.Enums;

namespace CSharp14.Showcase.Extensions
{
    // <summary>
    // Extension members permitem não apenas métodos, mas também propriedades e extensões estáticas
    // </summary>
    public static class TransactionExtensions
    {
        extension(Transaction transaction)
        {
            public bool IsExpense =>
                transaction.Type == TransactionType.Gasto;

            public bool IsIncome =>
                transaction.Type == TransactionType.Renda;

            public bool IsTransfer =>
                transaction.Type == TransactionType.Transferencia;
        }
    }
}
