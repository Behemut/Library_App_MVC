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

        public async Task<BooksGenresDTO> CreateBookGenre(int genre, int book, int created)
        {
            BooksGenresDTO model = new BooksGenresDTO();
            model.GenreId = genre;
            model.BookId = book;
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = created;
            _db.BooksGenres.Add(_mapper.Map<BooksGenres>(model));
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksGenresDTO>(model);
        }


        public async Task<BooksGenresDTO> UpdateBookGenre(int genre, int book, int created)
        {
            var model = await _db.BooksGenres.Where(x => x.GenreId == genre && x.BookId == book).FirstOrDefaultAsync();
            model.GenreId = genre;
            model.BookId = book;
            model.CreatedBy = created;
            model.UpdatedAt = DateTime.Now;
            _db.BooksGenres.Update(_mapper.Map<BooksGenres>(model));
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksGenresDTO>(model);
        }

        public async Task<BooksGenresDTO> DeleteBookGenre(int genre, int book, int created)
        {
            var bookGenre = await _db.BooksGenres.Where(x => x.GenreId == genre && x.BookId == book).FirstOrDefaultAsync();
            if (bookGenre == null)
            {
                return _mapper.Map<BooksGenresDTO>(null);
            }
            else
            {
                _db.BooksGenres.Remove(bookGenre);
                await _db.SaveChangesAsync();
                return _mapper.Map<BooksGenresDTO>(bookGenre);
            }
        }




    }
}
