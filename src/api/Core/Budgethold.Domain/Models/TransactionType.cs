using Budgethold.Domain.Common;

namespace Budgethold.Domain.Models
{
    public class TransactionType : Entity
    {
        public TransactionType()
        {
            Name = null!;
            Categories = null!;
        }

        public TransactionType(Enums.TransactionType transactionType)
        {
            Id = (int)transactionType;
            Name = transactionType.ToString();
            Categories = new();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public virtual HashSet<Category> Categories { get; private set; }
    }
}
