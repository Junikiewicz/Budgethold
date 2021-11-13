namespace Budgethold.Domain.Common
{
    public class Entity : IEntity
    {
        public bool IsDeleted { get; protected set; }

        public void SetIsDeleted()
        {
            IsDeleted = true;
        }
    }
}
