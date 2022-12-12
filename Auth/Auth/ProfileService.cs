using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

public class ProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var roleClaim = context.Subject.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Role);
        
        if  (roleClaim is not null)
        {
            context.IssuedClaims = new List<Claim> { roleClaim };
        }

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;

        return Task.CompletedTask;
    }
}