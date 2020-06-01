using Da3.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Da3.Infrastructure.Authentication
{
    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}