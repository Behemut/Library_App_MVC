namespace Domain
{
    public class Books
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ISBN { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int Stock { get; set; } = 0;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; } = true;

        public ICollection<BooksAuthors> Authors { get; set; }

        public ICollection<BooksGenres> Genres { get; set; }

        public ICollection<Borrows> Borrows { get; set; }


    }
}
