using Budgethold.Domain.Enums;
using System.ComponentModel;

namespace Budgethold.Application.Commands.Transaction.Helpers
{
    internal static class ITransactionHelper
    {
        public static decimal SetAmountSign(int transactionTypeId, decimal amount)
        {
            decimal transactionAmount = transactionTypeId switch
            {
                ((int)TransactionType.Income) => amount,
                ((int)TransactionType.Expense) => amount * (-1),
                _ => throw new InvalidEnumArgumentException("Transaction type is not correct")
            };
            return transactionAmount;
        }
    }
}
