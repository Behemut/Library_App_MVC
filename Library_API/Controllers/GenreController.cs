using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    // [Authorize]
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        protected Response _response;
        private IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
            _response = new Response();
        }

        [HttpGet("GetGenres")]
        public async Task<object> GetGenres()
        {
            try
            {
                IEnumerable<GenresDTO> genres = await _genreRepository.GetGenres();
                _response.Result = genres;
                _response.DisplayMessage = "Genres retrieved successfully";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = ex.Message;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<object> CreateOrUpdate([FromBody] GenresDTO genre)
        {
            try
            {
                GenresDTO genreDTO = await _genreRepository.CreateUpdateGenre(genre);
                _response.IsSuccess = true;
                _response.Result = genreDTO;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete("DeleteGenre/{id}")]
        public async Task<object> DeleteGenre(int id)
        {
            try
            {
                GenresDTO genre = await _genreRepository.GetGenreById(id);
                _response.IsSuccess = true;
                _response.Result = genre;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetGenreById/{id}")]
        public async Task<object> GetGenre(int id)
        {
            try
            {
                GenresDTO genre = await _genreRepository.GetGenreById(id);
                _response.IsSuccess = true;
                _response.Result = genre;
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
