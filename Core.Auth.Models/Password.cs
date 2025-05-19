namespace Core.Auth.Models
{
    public record Password
    {
        public required string Hash { get; set; }
        public required string Salt { get; set; }
    }
}
