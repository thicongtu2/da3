using Da3.Core.Entities;
using Da3.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Da3.Infrastructure.Authentication
{
    public class ApplicationUserStore : UserStore<Da3User>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}