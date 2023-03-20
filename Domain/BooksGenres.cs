namespace Domain
{
    public class BooksGenres
    {
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int GenreId { get; set; }
        public Genres Genre { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
