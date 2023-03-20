using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class UsersPerson : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<Borrows> Borrows { get; set; }

        
        public string? Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(1);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;


        // public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
