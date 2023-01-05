using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<AuthorsDTO> CreateUpdateAuthor(AuthorsDTO author);

        public Task<Authors> GetAuthorById(int id);
        
        public Task<AuthorsDTO> DeleteAuthor(int id);

        public Task<IEnumerable <Authors>> GetAuthors();


    }
}
