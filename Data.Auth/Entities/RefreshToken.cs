﻿namespace Data.Auth.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedBy { get; set; }
        public string? RevokedByIp { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
