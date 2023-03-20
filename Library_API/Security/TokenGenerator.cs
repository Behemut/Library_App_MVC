using Library_API.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain;
using Library_API.Models.DTO;

namespace Library_API.Security
{
    public class TokenGenerator
    {
        public string CreateToken(UsersPerson user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())             
            };

            var audienceToken = Tokenizator.AudienceToken;
            var issuerToken = Tokenizator.IssuerToken;
            var expireTime = Tokenizator.ExpireTimeMinutes;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Tokenizator.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenHandler = new JwtSecurityTokenHandler();

            var JWTSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: new ClaimsIdentity(claims),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expireTime),
                signingCredentials: signingCredentials
                );

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
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                );
            return token;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
             
            };
        }

       
        //public string generaterefreshtoken()
        //{
        //    var randomnumber = new byte[64];
        //    using var rng = randomnumbergenerator.create();
        //    rng.getbytes(randomnumber);
        //    return convert.tobase64string(randomnumber);
        //}

        public  ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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
            if (jwtSecurityToken == null || jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
