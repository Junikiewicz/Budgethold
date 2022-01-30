using Budgethold.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class AspNetUserRole : IdentityUserRole<int>, IEntity
    {
        public bool IsDeleted { get; private set; }

        public virtual AspNetUser? User { get; private set; }

        public virtual AspNetRole? Role { get; private set; }

        public void SetIsDeleted()
        {
            IsDeleted = true;
        }
    }
}
