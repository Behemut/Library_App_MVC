using System.ComponentModel.DataAnnotations;

namespace Library_API.Models.DTO
{
    public class TokenRefreshDTO
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
