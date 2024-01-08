using Application.Common.Interfaces;
using Domain.Entities.User;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using SocialNetworkAPI;
using SocialNetworkAPI.FacebookJsonWeb;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ISocialNetworkClient _socialNetworkClient;
        public TokenService(IConfiguration configuration,
            ISocialNetworkClient socialNetworkClient
            )
        {
            _configuration = configuration;
            _socialNetworkClient = socialNetworkClient;
        }

        public string CreateToken(ApplicationUser user, IList<string> roles)
        {
            var signingCredentials = GetSigningCredentials();
            var claims =  GetClaims(user, roles);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

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
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var expireValue = int.Parse(jwtSettings.GetSection("tokenValidityInMinutes").Value ?? "5");
            var expires = DateTime.Now.AddMinutes(expireValue);
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
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] }
            };
#pragma warning restore CS8604 // Possible null reference argument.
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }

        public async Task<Payload> VerifyFacebookTokenAsync(string token)
        {
            var payload = await _socialNetworkClient.VerifyFacebookTokenAsync(token);
            return payload;
        }

        public async Task<ClaimsPrincipal> VerifyMicrosoftTokenAsync(string token)
        {
            var clientId = _configuration["Authentication:Microsoft:ClientId"] ?? "";
            var tenantId = _configuration["Authentication:Microsoft:TenantId"] ?? "";
            var audience = _configuration["Authentication:Microsoft:Audience"] ?? "";

            // Validate Azure AD token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"https://login.microsoftonline.com/{tenantId}", 
                ValidAudience = clientId,
                ValidateAudience = true, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = await GetIssuerSigningKeys(tenantId), // Implement the method to get the signing keys
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero, // Adjust as needed
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        private async Task<IEnumerable<SecurityKey>> GetIssuerSigningKeys(string tenantId)
        {
            string authority = $"https://login.microsoftonline.com/{tenantId}";
            // Construct the OpenID Connect configuration endpoint URL
            string openIdConfigEndpoint = $"{authority}/.well-known/openid-configuration";

            // Use the ConfigurationManager to retrieve the OpenID Connect configuration
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                openIdConfigEndpoint,
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

            // Retrieve the OpenID Connect configuration
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None).ConfigureAwait(false);

            return openIdConfig.SigningKeys;
        }

    }
}
