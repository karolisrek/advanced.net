using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Auth
{
    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private UserRepository _userRepository;

        public PasswordValidator(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.GetUser(context.UserName);

            context.Result =
                GetInvalidUserResult(user, context.Password) ??
                new GrantValidationResult("subject", "custom", claims: GetUserClaims(user));
        }

        private GrantValidationResult? GetInvalidUserResult(User user, string password)
        {
            if (user == null)
            {
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User not found");
            }

            if (password != user.Password)
            {
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
            }

            return null;
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            return new List<Claim>() { new Claim(JwtClaimTypes.Role, user.Role.ToString()) };
        }
    }
}
