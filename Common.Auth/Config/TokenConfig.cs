namespace Common.Auth.Config
{
    public class TokenConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public AccessTokenConfig Access { get; set; }
        public RefreshTokenConfig Refresh { get; set; }
    }

    public class AccessTokenConfig
    {
        public string Secret { get; set; }
        public TimeSpan Expires { get; set; }
    }

    public class RefreshTokenConfig
    {
        public TimeSpan Expires { get; set; }
    }
}