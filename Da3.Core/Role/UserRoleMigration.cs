using System;
using System.Threading.Tasks;
using Da3.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Da3.Core.Role
{
    public static class UserRoleMigration
    {
        public static void EnsureApplicationUserRoles(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            CheckRoleAsync(serviceProvider).GetAwaiter().GetResult();
        }

        private static async Task CheckRoleAsync(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<Da3User>>();

            //Adding Admin Role 
            await TryAddRole(RoleManager, RoleConstants.Admin);
            await TryAddRole(RoleManager, RoleConstants.Employee);
            await TryAddRole(RoleManager, RoleConstants.Employer);
        }

        private static async Task TryAddRole(RoleManager<IdentityRole> RoleManager, string role)
        {
            var roleCheck = await RoleManager.RoleExistsAsync(role);
            if (!roleCheck)
            {
                //create the roles and seed them to the database 
                await RoleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}