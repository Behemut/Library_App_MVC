using Domain;

namespace Library_API.Models.DTO
{
    public class RefreshToken
    {
        public int id { get; set; }
       // public UsersPerson UserPerson { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(3);      
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
