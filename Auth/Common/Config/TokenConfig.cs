namespace Auth.Common.Config
{
    public class TokenConfig
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public AccessTokenConfig Access { get; set; } = default!;
        public RefreshTokenConfig Refresh { get; set; } = default!;
    }

    public class AccessTokenConfig
    {
        public string Secret { get; set; } = default!;
        public TimeSpan Expires { get; set; }
    }

    public class RefreshTokenConfig
    {
        public TimeSpan Expires { get; set; }
    }
}