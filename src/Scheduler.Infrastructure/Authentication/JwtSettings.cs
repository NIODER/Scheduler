namespace Scheduler.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SECTION_NAME = "JwtSettings";

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int Expire { get; init; }
    public string Secret { get; init; } = null!;
}