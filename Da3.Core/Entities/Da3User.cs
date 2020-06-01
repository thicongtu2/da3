using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Da3.Core.Entities
{
    public class Da3User : IdentityUser
    {
        public string Da3UserId { get; set; }
        public List<IdentityUserRole<string>> UserRoles { get; set; }
    }
}