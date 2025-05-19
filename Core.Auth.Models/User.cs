using Shared.Enums;

namespace Core.Auth.Models
{
    public class User
    {
        public Guid UniqueId { get; set; }
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public UserStatus Status { get; set; }
        public string? StatusReason { get; set; }
        public UserRole Role { get; set; }
    }
}