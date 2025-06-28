namespace Common.Auth.Dtos
{
    public class CreateRefreshTokenDto
    {
        public string ClientIp { get; set; }
        public Guid UniqueUserId { get; set; }
        public TimeSpan Expires { get; set; }
    }
}