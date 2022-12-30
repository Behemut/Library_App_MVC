using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public UserProfileRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UsersPersonDTO> GetUserProfileById(int id)
        {
            var user = await _db.UserPersons.Where(x => x.UserPersonId == id).FirstOrDefaultAsync();
            return _mapper.Map<UsersPersonDTO>(user);
        }

        public async Task<IEnumerable<UsersPersonDTO>> GetUserProfiles(bool status)
        {
            //Declaring initial List (Empty)
            IEnumerable<UsersPerson> users = new List<UsersPerson>();
            if (status || !status)
            {
                users = await _db.UserPersons
                    .Where(x => x.Active == status)
                    //Example WHERE IN CLAUSE
                    //IREPOS   params string[] includes
                    //.Where(x => includes.Contains(x.UserPersonId.ToString()))
                    .ToListAsync();
            }
            else
            {
                users = await _db.UserPersons.ToListAsync();
            }

            return _mapper.Map<IEnumerable<UsersPersonDTO>>(users);
        }


        public async Task<UsersPersonDTO> CreateUpdateUserProfile(UsersPersonDTO user)
        {
            var userEntity = _mapper.Map<UsersPerson>(user);
            if (user.UserPersonId == 0)
            {
                _db.UserPersons.Add(userEntity);
            }
            else
            {
                _db.UserPersons.Update(userEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<UsersPersonDTO>(userEntity);
        }

        public async Task<UsersPersonDTO> GetUserByUsername(string username)
        {
            var user = await _db.UserPersons
                .Where(x => x.Username == username)
                .Where(x => x.Active == true) //Only allow to get Active users
                .FirstOrDefaultAsync();
            return _mapper.Map<UsersPersonDTO>(user);
        }


    }
}
