namespace Library_API.Models.DTO
{
    public class BorrowsDTO
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public int Quantity { get; set; }

        public int CreatedBy { get; set; }

    }
}
