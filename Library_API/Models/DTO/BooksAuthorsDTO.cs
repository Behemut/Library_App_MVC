namespace Library_API.Models.DTO
{
    public class BooksAuthorsDTO

    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
