using System.ComponentModel.DataAnnotations;

namespace Library_API.Models.DTO
{
    public class AuthorsDTO
    {
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
    }
}
