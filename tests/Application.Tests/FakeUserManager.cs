using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using UserManagement.Domain;

namespace Application.Tests
{
    public class FakeUserManager : UserManager<User>
    {
     
        public FakeUserManager() 
            : base(Substitute.For<IUserStore<User>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<User>>(),
                Array.Empty<IUserValidator<User>>(),
                Array.Empty<IPasswordValidator<User>>(),
                Substitute.For<ILookupNormalizer>(), 
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<User>>>())
        { }
        public override Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(new User{Email = email});
        }

        public override Task<bool> IsEmailConfirmedAsync(User user)
        {
            return Task.FromResult(user.Email == "test@test.com");
        }

        public override Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return Task.FromResult("---------------");
        }
    }
}