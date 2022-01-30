using Budgethold.Domain.Common;
using Budgethold.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Budgethold.Security.Models
{
    public class AspNetUser : IdentityUser<int>, IEntity
    {
        public AspNetUser()
        {
            User = null!;
        }

        public AspNetUser(string email, string name)
        {
            UserName = email;
            Email = email;
            User = new User(name);
        }

        public bool IsDeleted { get; private set; }

        public virtual User User { get; private set; }

        public virtual HashSet<AspNetUserRole>? UserRoles { get; private set; }

        public void SetIsDeleted()
        {
            IsDeleted = true;
        }
    }
}
