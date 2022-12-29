using Library_API.Models.Auth;
using Library_API.Models.DTO;

namespace Library_API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UsersDTO> ValidateUser(LoginDTO login);

        Task<IEnumerable<UserAppDTO>> GetUsers();

        Task<UserAppDTO> GetUserByUsername(string username);

        Task<UsersDTO> CreateUpdateUser(UsersDTO user);

       
    }
}