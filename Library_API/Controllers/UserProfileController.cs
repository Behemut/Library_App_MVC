using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{

    [Authorize]
    [Route("api/userprofile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        protected Response _response;
        private IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _response = new Response();
        }



        [HttpGet]
        public async Task<object> GetUserProfiles(bool status)
        {
            try
            {
                IEnumerable<UsersPersonDTO> users = await _userProfileRepository.GetUserProfiles(status);
                _response.IsSuccess = true;
                _response.Result = users;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id}")]

        public async Task<object> GetUserProfileById(int id)
        {
            try
            {
                UsersPersonDTO user = await _userProfileRepository.GetUserProfileById(id);
                _response.IsSuccess = true;
                _response.Result = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpGet("username")]
        public async Task<object> GetUserProfileByUserName(string userName)
        {
            try
            {
                UsersPersonDTO user = await _userProfileRepository.GetUserByUsername(userName);
                _response.IsSuccess = true;
                _response.Result = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }



        [HttpPost]
        public async Task<object> CreateUpdateUserProfile(UsersPersonDTO user)
        {
            try
            {
                UsersPersonDTO userEntity = await _userProfileRepository.CreateUpdateUserProfile(user);
                _response.IsSuccess = true;
                _response.Result = userEntity;
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
