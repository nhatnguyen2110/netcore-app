using Application.Models.Account;
using Domain.Entities.User;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        ClaimsPrincipal DecryptTokenToClaim(string decrypt, bool validateLifetime = true);
        string CreateToken(ApplicationUser user, IList<string> roles);

    }
}
