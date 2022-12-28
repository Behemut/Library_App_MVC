namespace Library_API.Models.DTO
{
    public class GenresDTO
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
    }
}
