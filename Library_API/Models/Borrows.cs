using Microsoft.EntityFrameworkCore;

namespace Library_API.Models
{
    public class Borrows
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public int Quantity { get; set; }

        public int CreatedBy { get; set; }

        public  Books Book { get; set; }
        public virtual UsersPerson User { get; set; }


    }
}
