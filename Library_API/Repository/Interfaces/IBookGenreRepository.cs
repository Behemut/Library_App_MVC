using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IBookGenreRepository
    {
        public Task<BooksGenresDTO> CreateBookGenre(int genre, int book, int created);
        public Task<BooksGenresDTO> UpdateBookGenre(int genre, int book, int created);
        public Task<BooksGenresDTO> DeleteBookGenre(int genre, int book, int created);
    }
}
