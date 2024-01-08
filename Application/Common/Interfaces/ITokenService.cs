using Domain.Entities.User;
using Google.Apis.Auth;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        ClaimsPrincipal DecryptTokenToClaim(string decrypt, bool validateLifetime = true);
        string CreateToken(ApplicationUser user, IList<string> roles);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token);
        Task<SocialNetworkAPI.FacebookJsonWeb.Payload> VerifyFacebookTokenAsync(string token);
        Task<ClaimsPrincipal> VerifyMicrosoftTokenAsync(string token);
    }
}
