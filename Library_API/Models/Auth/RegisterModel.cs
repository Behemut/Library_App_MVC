using System.ComponentModel.DataAnnotations;

namespace Library_API.Models.Auth
{
    public class RegisterModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        public string RoleName { get; set; }

    }
}
