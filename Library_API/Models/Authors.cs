using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_API.Models
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }
      
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<BooksAuthors> BooksAuthors { get; set; }

    }
}
