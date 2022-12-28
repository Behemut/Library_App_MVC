namespace Library_API.Models.Auth
{
    public class UserAppDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public Boolean Active { get; set; } 
    }
}
