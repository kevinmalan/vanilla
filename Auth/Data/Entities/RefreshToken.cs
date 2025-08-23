using Auth.Core.Models;

namespace Auth.Data.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; } = default!;
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedBy { get; set; }
        public string? RevokedByIp { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
