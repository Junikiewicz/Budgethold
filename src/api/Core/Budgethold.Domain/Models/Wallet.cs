using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class Wallet : Entity
    {
        public Wallet()
        {
            User = null!;
            Name = null!;
            Categories = null!;
            Transactions = null!;
        }

        public Wallet(string name, decimal startingValue, int userId)
        {
            Name = name;
            StartingValue = startingValue;
            CurrentValue = startingValue;
            UserId = userId;
            Categories = new ();
            User = null!;
            Transactions = null!;
        }

        public int Id { get; private set; }

        public decimal StartingValue { get; private set; }

        public decimal CurrentValue { get; private set; }

        public string Name { get; private set; }

        public int UserId { get; private set; }

        public virtual User User { get; private set; }

        public virtual HashSet<Category> Categories { get; private set; }

        public virtual HashSet<Transaction> Transactions { get; private set; }

        public void Update(string name, decimal startingValue)
        {
            Name = name;
            CurrentValue = CurrentValue - StartingValue + startingValue;
            StartingValue = startingValue;
        }

        public void ApplyNewTransaction(decimal newTransactionValue)
        {
            EditTransactionValueChange(0, newTransactionValue);
        }

        public void EditTransactionValueChange(decimal oldTransactionValue, decimal newTransactionValue)
        {
            CurrentValue = CurrentValue - oldTransactionValue + newTransactionValue;
        }

        public void RevertTransactionValueChange(decimal oldTransactionValue)
        {
            EditTransactionValueChange(oldTransactionValue, 0);
        }
    }
}
