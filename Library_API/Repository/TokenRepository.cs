using AutoMapper;
using Library_API.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Library_API.Security;
using Library_API.Models.DTO;

namespace Library_API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private IMapper _mapper;

        public TokenRepository(IMapper mapper)
        {
           _mapper = mapper;
        }
        
        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            return TokenGenerator.CreateToken(authClaims);
        }

        public string GenerateRefreshToken()
        {
            return TokenGenerator.GenerateRefreshToken();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            return TokenGenerator.GetPrincipalFromExpiredToken(token);
        }

        public APITokenDTO GetToken(string username)
        {
            APITokenDTO apiToken = new APITokenDTO();
            apiToken.AccessToken = TokenGenerator.GenerateTokenJWT(username);
            return _mapper.Map<APITokenDTO>(apiToken);
        }

    }
}
