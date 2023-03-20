using Library_API.AppUserAccesor;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Library_API.Repository
{
    public class UserAccesor : IUserAccesor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserAccesor(IHttpContextAccessor httpContextAccessor)
        {
           _httpContextAccessor = httpContextAccessor;    
        }

        public string GetUsername()
        {
           return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        
    }
}
