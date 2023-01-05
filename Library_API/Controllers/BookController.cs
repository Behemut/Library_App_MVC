using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library_API.Controllers
{
    // [Authorize]
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected Response _response;
        private IBookRepository _bookRepository;
        private IBookAuthorRepository _bookAuthorRepository;
        private IBookGenreRepository _bookGenreRepository;

        public BookController(
            IBookRepository bookRepository, 
            IBookAuthorRepository bookAuthorRepository, 
            IBookGenreRepository bookGenreRepository)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookGenreRepository = bookGenreRepository;
            _response = new Response();
        }

        [HttpGet("GetBooks")]
        public async Task<object> GetBooks()
        {
                try
                {
                    IEnumerable<Books> books = await _bookRepository.GetBooks();
                    _response.Result = books;
                    _response.DisplayMessage = "Books retrieved successfully";
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
        public async Task<object> CreateOrUpdate([FromBody] BooksDTO book)
        {
            try
            {
                BooksDTO bookDTO = await _bookRepository.CreateUpdateBook(book);
                _response.IsSuccess = true;
                _response.Result = bookDTO;
                _response.DisplayMessage = "Book created successfully";
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };

            }
            return _response;
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<object> GetBookById(int id)
        {
            try
            {
                Books book = await _bookRepository.GetBookById(id);
                _response.Result = book;
                _response.DisplayMessage = "Book retrieved successfully";
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

        [HttpGet("GetBooksByAuthor/{authorId}")]
        public async Task<object> GetBooksByAuthor(int authorId)
        {
            try
            {
                IEnumerable<Books> books = await _bookRepository.GetBooksByAuthor(authorId);
                _response.Result = books;
                _response.DisplayMessage = "Books retrieved successfully";
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

        [HttpDelete("DeleteBook/{id}")]
        public async Task<object> DeleteBook(int id)
        {
            try
            {
                await _bookRepository.DeleteBook(id);
                _response.DisplayMessage = "Book deleted successfully";
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


        [HttpPost("AddBookAuthor")]
        public async Task<object> AddBookAuthor(int author, int book, int created)
        {
            try
            {
                BooksAuthorsDTO BkAModel = await _bookAuthorRepository.CreateBookAuthor(author, book,created);
                _response.IsSuccess = true;
                _response.Result = BkAModel;
                _response.DisplayMessage = "Book - Author created successfully";

            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }


            return _response;
        }


        [HttpPut("UpdateBookAuthor")]
        public async Task<object> UpdateBookAuthor(int author, int book, int created)
        {
            try
            {
                BooksAuthorsDTO BkAModel = await _bookAuthorRepository.UpdateBookAuthor(author, book, created);
                _response.IsSuccess = true;
                _response.Result = BkAModel;
                _response.DisplayMessage = "Book - Author updated successfully";

            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpDelete("DeleteBookAuthor")]
        public async Task<object> DeleteBookAuthor(int author, int book, int created)
        {
                try
                {
                    await _bookAuthorRepository.DeleteBookAuthor(author, book, created);
                    _response.DisplayMessage = "Book - Author deleted successfully";
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
    }
}
