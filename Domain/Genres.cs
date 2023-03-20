namespace Domain
{
    public class Genres
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }

        public ICollection<BooksGenres> Books { get; set; } = new List<BooksGenres>();

    }
}
