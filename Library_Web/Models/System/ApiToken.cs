namespace Library_Web.Models.System
{
    public class ApiToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Expiration { get; set; }

        public string ClaimName { get; set; }

        public string ClaimCode { get; set; }

        public string NewGUID { get; set; }

        public IList<string> UserRoles { get; set; }


    }
}
