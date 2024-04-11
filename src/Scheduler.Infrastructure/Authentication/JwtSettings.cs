namespace Scheduler.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SECTION_NAME = "JwtSettings";

    public string Issuer { get; private set; } = null!;
    public string Audience { get; private set; } = null!;
    public int Expire { get; private set; }
    public string Secret { get; private set; } = null!;
}