using Application.Common.Interfaces;
using Domain.Entities.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(ApplicationUser user, IList<string> roles, bool keepLogin)
        {
            var signingCredentials = GetSigningCredentials();
            var claims =  GetClaims(user, roles);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims, keepLogin);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = _configuration.GetSection("JwtSettings:secretKey").Value;
            var key = Encoding.UTF8.GetBytes(secretKey ?? "");
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email??""));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, bool keepLogin = false)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var expireValue = int.Parse(jwtSettings.GetSection("expires").Value ?? "30");
            var expires = keepLogin ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(expireValue);
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }
        public ClaimsPrincipal DecryptTokenToClaim(string decrypt, bool validateLifetime = true)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:secretKey"] ?? ""));
            //var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:Secret"]));
            string tokenString = decrypt;

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[] { _configuration["JWTSettings:validAudience"]??"" },
                ValidIssuers = new string[] { _configuration["JWTSettings:validIssuer"] ?? "" },
                IssuerSigningKey = securityKey,
                //TokenDecryptionKey = secretKey,
                RequireExpirationTime = true,
                ValidateLifetime = validateLifetime
            };

            SecurityToken validatedToken;
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();
            return handler.ValidateToken(tokenString, tokenValidationParameters, out validatedToken);
        }
    }
}
