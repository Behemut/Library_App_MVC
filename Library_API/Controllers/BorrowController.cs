using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    // [Authorize]
    [Route("api/genre")]
    [ApiController]
    public class BorrowController : ControllerBase
    {

        protected Response _response;
        private IBorrowRepository _borrowRepository;

        public BorrowController(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
            _response = new Response();
        }

        [HttpGet("GetBorrows")]
        public async Task<object> GetBorrows(string? username)
        {
            try
            {
                IEnumerable<Borrows> borrows = await _borrowRepository.GetBorrows(username);
                _response.Result = borrows;
                _response.DisplayMessage = "Borrows retrieved successfully";
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

        [HttpGet("GetBorrow/{id}")]
        public async Task<object> GetBorrow(int id)
        {
            try
            {
                Borrows borrow = await _borrowRepository.GetBorrowById(id);
                _response.Result = borrow;
                _response.DisplayMessage = "Borrow retrieved successfully";
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



        [HttpPost]
        public async Task<object> CreateOrUpdate([FromBody] BorrowsDTO borrow)
        {
            try
            {
                BorrowsDTO borrowDTO = await _borrowRepository.CreateUpdateBorrow(borrow);
                _response.IsSuccess = true;
                _response.Result = borrowDTO;
            }
            catch (Exception ex)
            {
                _response.DisplayMessage = "Error Occured";
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteBorrow(int id)
        {
            try
            {
                await _borrowRepository.DeleteBorrow(id);
                _response.IsSuccess = true;
                _response.DisplayMessage = "Borrow deleted successfully";
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
