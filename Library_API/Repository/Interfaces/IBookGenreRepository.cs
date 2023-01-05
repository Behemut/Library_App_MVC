using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IBookGenreRepository
    {
        public Task<BooksGenresDTO> CreateUpdateBookGenre(BooksGenresDTO bookGenre);

        public Task<BooksGenresDTO> DeleteBookGenre(int id);
    }
}
