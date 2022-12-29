using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class UserRepository  //: IUserRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        //public async Task<UsersDTO> ValidateUser(LoginDTO login)
        //{

        //    UserProfile userprofile = await _db.UserPersons.Where

        //    return _mapper.Map<UsersDTO>(user);
        //}





    }
}
