using Budgethold.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class Role : IdentityRole<int>, IEntity
    {
        public bool IsDeleted { get; private set; }

        public virtual HashSet<UserRole>? UserRoles { get; private set; }
    }
}
