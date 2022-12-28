namespace Library_API.Models.DTO
{
    public class BooksDTO
    {
        public int BookId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ISBN { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int Stock { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
    }
}
