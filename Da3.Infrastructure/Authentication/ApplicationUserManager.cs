using System;
using System.Collections.Generic;
using Da3.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Da3.Infrastructure.Authentication
{
    public class ApplicationUserManager : UserManager<Da3User>
    {
        public ApplicationUserManager(IUserStore<Da3User> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<Da3User> passwordHasher, 
            IEnumerable<IUserValidator<Da3User>> userValidators,
            IEnumerable<IPasswordValidator<Da3User>> passwordValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            IServiceProvider services,
            ILogger<UserManager<Da3User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}