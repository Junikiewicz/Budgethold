namespace Budgethold.Domain.Enums
{
    public enum TransactionType
    {
        Income = 1,
        Expense = 2,
    }

    public static class AmountSign
    {
        public static decimal SetAmountSign(int transactionTypeId, decimal amount)
        {
            decimal transactionAmount = transactionTypeId switch
            {
                1 => amount,
                2 => amount * (-1),
                _ => amount
            };
            return transactionAmount;
        }
    }
}
