using Library_API.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Library_API.Repository.Interfaces
{
    public interface ITokenRepository
    {
        APITokenDTO GetToken(string username);
        JwtSecurityToken CreateToken(List<Claim> authClaims);

        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
