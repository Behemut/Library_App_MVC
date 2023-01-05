using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
   // [Authorize]
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        protected Response _response;
        private IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _response = new Response();
        }

        [HttpGet("GetAuthors")]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                IEnumerable<Authors> authors = await _authorRepository.GetAuthors();
                _response.Result = authors;
                _response.DisplayMessage = "Authors retrieved successfully";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = ex.Message;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return Ok(_response);
        }


        [HttpPost("CreateOrUpdate")]
        public async Task<object> CreateOrUpdate([FromBody] AuthorsDTO author)
        {
            try
            {
                AuthorsDTO authorDTO = await _authorRepository.CreateUpdateAuthor(author);
                _response.IsSuccess = true;
                _response.Result= authorDTO;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>(){ ex.Message};
        
            }
            return _response;
        }


        [HttpGet("GetAuthorById/{id}")]
        public async Task<object> GetAuthorById(int id)
        {
            try
            {
                Authors author = await _authorRepository.GetAuthorById(id);
                _response.IsSuccess = true;
                _response.Result = author;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };

            }
            return _response;
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<object> DeleteAuthor(int id)
        {
            try
            {
                AuthorsDTO authorDTO = await _authorRepository.DeleteAuthor(id);
                _response.IsSuccess = true;
                _response.Result = authorDTO;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };

            }
            return _response;
        }

    }
}
