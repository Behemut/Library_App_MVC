namespace Library_API.Models.DTO
{
    public class APITokenDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> UserRoles { get; set; }

        public string ClaimName { get; set; }
        public string ClaimCode { get; set; }
        public string NewGUID { get; set; }

    }
}
