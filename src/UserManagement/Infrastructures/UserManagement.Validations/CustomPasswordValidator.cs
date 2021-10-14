using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagement.Domain;

namespace UserManagement.Validations
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                Task.FromResult(IdentityResult.Success);
            }

            return Task.FromResult(IdentityResult.Success);
            ;
        }
    }

    public class CustomPasswordHasher : PasswordHasher<User>
    {
        public override PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword,
            string providedPassword)
        {
            if (hashedPassword == null && string.IsNullOrEmpty(providedPassword))
                return PasswordVerificationResult.Success;
            return base.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }

}