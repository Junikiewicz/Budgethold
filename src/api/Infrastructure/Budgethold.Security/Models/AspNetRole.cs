using Budgethold.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class AspNetRole : IdentityRole<int>, IEntity
    {
        public bool IsDeleted { get; private set; }

        public virtual HashSet<AspNetUserRole>? UserRoles { get; private set; }
    }
}
