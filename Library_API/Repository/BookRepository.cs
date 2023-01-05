using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public BookRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BooksDTO> CreateUpdateBook(BooksDTO book)
        {
            var bookEntity = _mapper.Map<BooksDTO,Books>(book);
            if (book.BookId == 0)
            {
                _db.Books.Add(bookEntity);
            }
            else
            {
                _db.Books.Update(bookEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksDTO>(bookEntity);
        }


        public async Task<BooksDTO> DeleteBook(int id)
        {
            Books book = await _db.Books.Where(x => x.BookId == id).FirstOrDefaultAsync();
            book.Active = false;
            book.DeletedAt = DateTime.Now;
            _db.Books.Update(book);
            _db.SaveChanges();
            await _db.SaveChangesAsync();
            return _mapper.Map<BooksDTO>(book);
        }

        public async Task<Books> GetBookById(int id)
        {
            var book = await _db.Books.Where(x => x.BookId == id).FirstOrDefaultAsync();
            return _mapper.Map<Books>(book);
        }

        public async Task<IEnumerable<Books>> GetBooks()
        {
            var books = await _db.Books.Where(x => x.Active == true).ToListAsync();
            return _mapper.Map<IEnumerable<Books>>(books);
        }

        public async Task<List<Books>> GetBooksByAuthor(int authorId)
        {
            var books = await _db.Authors
                .Where(x => x.AuthorId == authorId)
                .Include(x => x.BooksAuthors).ThenInclude(x => x.Book)
                .SelectMany(x => x.BooksAuthors.Select(y => y.Book))
                .ToListAsync();

            return _mapper.Map<List<Books>>(books);
        }

 
    }
}
