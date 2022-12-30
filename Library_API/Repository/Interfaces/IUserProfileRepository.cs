using Library_API.Models;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UsersPersonDTO>> GetUserProfiles(bool status);
        Task<UsersPersonDTO> GetUserProfileById(int id);
        Task<UsersPersonDTO> CreateUpdateUserProfile(UsersPersonDTO user);
        Task<UsersPersonDTO> GetUserByUsername(string username);
    }
}
