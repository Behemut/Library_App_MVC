using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        public BookAuthorRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BooksAuthorsDTO> CreateBookAuthor(int author, int book, int created)
        {
            BooksAuthorsDTO model = new BooksAuthorsDTO();
            model.AuthorId = author;
            model.BookId = book;
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = created;
            _db.BooksAuthors.Add(_mapper.Map<BooksAuthors>(model));
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksAuthorsDTO>(model);
        }

        public async Task<BooksAuthorsDTO> UpdateBookAuthor(int author, int book, int created)
        {
           // BooksAuthorsDTO model = new BooksAuthorsDTO();
            var model = await _db.BooksAuthors.Where(x => x.AuthorId == author && x.BookId == book).FirstOrDefaultAsync();
            model.AuthorId = author;
            model.BookId = book;
            model.CreatedBy = created;
            model.UpdatedAt = DateTime.Now;
            _db.BooksAuthors.Update(_mapper.Map<BooksAuthors>(model));
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksAuthorsDTO>(model);
        }


        public async Task<BooksAuthorsDTO> DeleteBookAuthor(int author, int book, int created)
        {
            var bookAuthor = await _db.BooksAuthors.Where(x => x.AuthorId == author && x.BookId == book).FirstOrDefaultAsync();
            if (bookAuthor == null)
            {
                return _mapper.Map<BooksAuthorsDTO>(null);
            }
            else
            {
                _db.BooksAuthors.Remove(bookAuthor);
                await _db.SaveChangesAsync();
                return _mapper.Map<BooksAuthorsDTO>(bookAuthor);
            }
           
        }

    }
}
