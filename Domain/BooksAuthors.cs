namespace Domain
{
    public class BooksAuthors
    {
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int AuthorId { get; set; }
        public Authors Author { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
