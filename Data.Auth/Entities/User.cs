using Shared.Enums;

namespace Data.Auth.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public UserStatus Status { get; set; }
        public string? StatusReason { get; set; }
        public UserRole Role { get; set; }
    }
}