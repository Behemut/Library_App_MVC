using System.ComponentModel.DataAnnotations;

namespace Library_API.Models.Auth
{
    public class RegisterModel
    {
   

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [Required]
        public string RoleName { get; set; }

    }
}
