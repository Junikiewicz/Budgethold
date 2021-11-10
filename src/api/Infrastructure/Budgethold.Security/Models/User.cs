using Budgethold.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class User : IdentityUser<int>, IEntity
    {
        public User(string email)
        {
            UserName = email;
            Email = email;
        }

        public bool IsDeleted { get; private set; }

        public virtual HashSet<UserRole>? UserRoles { get; private set; }
    }
}
