
namespace Domain
{
    public class Borrows
    {
        public int Id { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public int Quantity { get; set; }

        public int CreatedBy { get; set; }
        
        public int BookId { get; set; }
        public Books Book { get; set; }

        public bool IsHost { get; set; }

    }
}
