using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{

    [AllowAnonymous]
    [Route("api/authorize")]
    [ApiController]

    public class AuthorizeController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;
        protected Response _response;

        public AuthorizeController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _response = new Response();
        }

        [HttpPost]
        public async Task<object> GetToken(LoginDTO login)
        {
            try
            {
                APITokenDTO apiToken =  _tokenRepository.GetToken(login.UserName);
                _response.IsSuccess = true;
                _response.Result = apiToken;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
    
            }
            return _response;
        }
    }
}
