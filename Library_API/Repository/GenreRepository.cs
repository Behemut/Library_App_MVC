using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class GenreRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public GenreRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GenresDTO> CreateUpdateGenre(GenresDTO genre)
        {
            var genreEntity = _mapper.Map<GenresDTO,Genres>(genre);
            if (genre.GenreId == 0)
            {
                _db.Genres.Add(genreEntity);
            }
            else
            {
                _db.Genres.Update(genreEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<GenresDTO>(genreEntity);
        }

        public async Task<GenresDTO> DeleteGenre(int id)
        {
            var genre = await _db.Genres.Where(x => x.GenreId == id).FirstOrDefaultAsync();
            genre.Active = false;
            genre.DeletedAt = DateTime.Now;
            _db.Genres.Update(genre);
            _db.SaveChanges();
            await _db.SaveChangesAsync();
            return _mapper.Map<GenresDTO>(genre);
        }




    }
}
