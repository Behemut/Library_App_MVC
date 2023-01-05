using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public AuthorRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AuthorsDTO> CreateUpdateAuthor(AuthorsDTO author)
        {
            var authorEntity = _mapper.Map<AuthorsDTO,Authors>(author);
            
            if (author.AuthorId == 0)
            {
                _db.Authors.Add(authorEntity);
            }
            else
            {
                _db.Authors.Update(authorEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<AuthorsDTO>(authorEntity);
        }

        public async Task<AuthorsDTO> DeleteAuthor(int id)
        {    
            var author = await _db.Authors.Where(x => x.AuthorId == id).FirstOrDefaultAsync();
            author.Active = false;
            author.DeletedAt = DateTime.Now;
            _db.Authors.Update(author);
            _db.SaveChanges();
            await _db.SaveChangesAsync();
            return _mapper.Map<AuthorsDTO>(author);
        }


        public async Task<Authors> GetAuthorById(int id)
        {
            var author = await _db.Authors.Where(x => x.AuthorId == id).FirstOrDefaultAsync();
            return _mapper.Map<Authors>(author);
        }


        public async Task<IEnumerable<Authors>> GetAuthors()
        {
            var authors = await _db.Authors.ToListAsync();
            return authors;
        }
        
    }
}
