using AutoMapper;
using Library_API.DBContext;
using Library_API.Models;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Library_API.Repository
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public BorrowRepository(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BorrowsDTO> CreateUpdateBorrow(BorrowsDTO borrow)
        {
            var borrowEntity = _mapper.Map<Borrows>(borrow);
            if (borrow.BookId == 0 &&  borrow.UserId== 0)
            {
                _db.Borrows.Add(borrowEntity);
            }
            else
            {
                _db.Borrows.Update(borrowEntity);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<BorrowsDTO>(borrowEntity);
        }

        public async Task<BorrowsDTO> DeleteBorrow(int id)
        {
            var borrow = await _db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return null;
            }
            _db.Borrows.Remove(borrow);
            await _db.SaveChangesAsync();
            return _mapper.Map<BorrowsDTO>(borrow);
        }

        public async Task<Borrows> GetBorrowById(int id)
        {
            var borrow = await _db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return null;
            }
            return _mapper.Map<Borrows>(borrow);
        }

        public async Task<IEnumerable<Borrows>> GetBorrows(string? username)
        {
            var borrows = await _db.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.User.Username == username)
                .ToListAsync();
            return _mapper.Map<List<Borrows>>(borrows);
        }
    }

}
