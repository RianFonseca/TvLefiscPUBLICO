namespace TvLefisc.Models
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string JwtToken { get; set; }
    }

    public class Chave
    {
        private readonly JwtSettings _jwtSettings;

        public Chave(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        }

        public string GetJwtToken()
        {
            return _jwtSettings.JwtToken;
        }
    }
}

