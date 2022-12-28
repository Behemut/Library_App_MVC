using System.ComponentModel.DataAnnotations;

namespace Library_API.Models
{
    public class UsersPerson
    {
        [Key]
        public int UserPersonId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int QuantityBorrows { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }

        public ICollection<Borrows> Borrows { get; set; }
    }
}
