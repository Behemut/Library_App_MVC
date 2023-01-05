using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IBorrowRepository
    {
        public Task<BorrowsDTO> CreateUpdateBorrow(BorrowsDTO borrow);

        public Task<Borrows> GetBorrowById(int id);

        public Task<BorrowsDTO> DeleteBorrow(int id);

        public Task<IEnumerable<Borrows>> GetBorrows(string? username);
    }
}
