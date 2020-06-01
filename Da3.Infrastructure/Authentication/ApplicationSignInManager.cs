using Da3.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Da3.Infrastructure.Authentication
{
    public class ApplicationSignInManager : SignInManager<Da3User>
    {
        public ApplicationSignInManager(UserManager<Da3User> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<Da3User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<Da3User>> logger, 
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<Da3User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}