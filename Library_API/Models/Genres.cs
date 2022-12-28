using System.ComponentModel.DataAnnotations;

namespace Library_API.Models
{
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }

        //OPTIONS FOR RELATIONSHIPS BETWEEN MODELS  
        public virtual ICollection<BooksGenres> BooksGenres { get; set; }

    }
}
