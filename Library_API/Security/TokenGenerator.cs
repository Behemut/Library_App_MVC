using Library_API.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Library_API.Security
{
    public class TokenGenerator
    {
        public static string GenerateTokenJWT(string username)
        {
            var secretKey = Tokenizator.SecretKey;
            var audienceToken = Tokenizator.AudienceToken;
            var issuerToken = Tokenizator.IssuerToken;
            var expireTime = Tokenizator.ExpirationTime;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username)
                });

            var tokenHandler = new JwtSecurityTokenHandler();

            var JWTSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials:  signingCredentials
                ) ;

            var jwtTokenString = tokenHandler.WriteToken(JWTSecurityToken);
            return jwtTokenString;
        }


        public static JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Tokenizator.SecretKey));
            var tokenValidityInMinutes = Tokenizator.ExpireTimeMinutes;

            var token = new JwtSecurityToken(
                issuer: Tokenizator.IssuerToken,
                audience: Tokenizator.AudienceToken,
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(randomNumber);
            //    return Convert.ToBase64String(randomNumber);
            //}
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken == null)
                return null;

            var key = Encoding.Default.GetBytes(Tokenizator.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = Tokenizator.IssuerToken,
                ValidateAudience = true,
                ValidAudience = Tokenizator.AudienceToken,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
