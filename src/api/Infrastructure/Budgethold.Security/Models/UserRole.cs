using Budgethold.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class UserRole : IdentityUserRole<int>, IEntity
    {
        public bool IsDeleted { get; private set; }

        public virtual User? User { get; private set; }
        public virtual Role? Role { get; private set; }
    }
}
