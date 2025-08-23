namespace Auth.Common.Dtos
{
    public class CreateRefreshTokenDto
    {
        public string ClientIp { get; set; } = default!;
        public Guid UniqueUserId { get; set; }
        public TimeSpan Expires { get; set; }
    }
}