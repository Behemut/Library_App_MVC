using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IBookAuthorRepository
    {
        public Task<BooksAuthorsDTO> CreateBookAuthor(int author, int book, int created);
        public Task<BooksAuthorsDTO> UpdateBookAuthor(int author, int book, int created);
        public Task<BooksAuthorsDTO> DeleteBookAuthor(int author, int book, int created);

    }
}
