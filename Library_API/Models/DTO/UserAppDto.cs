namespace Library_API.Models.DTO
{
    public class UserAppDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
