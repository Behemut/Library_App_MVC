using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IGenreRepository
    {
        public Task<GenresDTO> CreateUpdateGenre(GenresDTO genre);

        public Task<GenresDTO> GetGenreById(int id);

        public Task<GenresDTO> DeleteGenre(int id);

        public Task<IEnumerable<GenresDTO>> GetGenres();
    }
}
