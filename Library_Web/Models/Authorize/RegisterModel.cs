using System.ComponentModel.DataAnnotations;

namespace Library_Web.Models.Authorize
{
    public class RegisterModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? RoleName { get; set; }


    }
}
