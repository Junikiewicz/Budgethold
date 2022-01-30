using System.ComponentModel;
using Budgethold.Domain.Enums;

namespace Budgethold.Application.Commands.Transaction.Helpers
{
    internal static class TransactionHelper
    {
        public static decimal GetTransactionValue(int transactionTypeId, decimal amount)
        {
            return transactionTypeId switch
            {
                (int)TransactionType.Income => amount,
                (int)TransactionType.Expense => amount * (-1),
                _ => throw new InvalidEnumArgumentException("Transaction type is not correct")
            };
        }
    }
}
