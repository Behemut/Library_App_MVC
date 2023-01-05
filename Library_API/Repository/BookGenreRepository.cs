using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class BookGenreRepository : IBookGenreRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        public BookGenreRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BooksGenresDTO> CreateUpdateBookGenre(BooksGenresDTO bookGenre)
        {
            var bookGenreEntity = _mapper.Map<BooksGenresDTO,BooksGenres>(bookGenre);
            if (bookGenre.BookId == 0 && bookGenre.GenreId == 0)
            {
                _db.BooksGenres.Add(bookGenreEntity);
            }
            else
            {
                _db.BooksGenres.Update(bookGenreEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksGenresDTO>(bookGenreEntity);
        }

        public async Task<BooksGenresDTO> DeleteBookGenre(int id)
        {
            var bookGenre = await _db.BooksGenres.FindAsync(id);

            if (bookGenre == null)
            {
                return null;
            }
            _db.BooksGenres.Remove(bookGenre);
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksGenresDTO>(bookGenre);
        }
    }
}
