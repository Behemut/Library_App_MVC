using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IBookRepository
    {
        public Task<BooksDTO> CreateUpdateBook(BooksDTO book);

        public Task<Books> GetBookById(int id);

        public Task<List<Books>> GetBooksByAuthor(int authorId);

        public Task<BooksDTO> DeleteBook(int id);

        public Task<IEnumerable<Books>> GetBooks();
    }
}
