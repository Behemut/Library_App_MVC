namespace Library_API.Models.DTO
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAttempt { get; set; } = false;
    }
}
