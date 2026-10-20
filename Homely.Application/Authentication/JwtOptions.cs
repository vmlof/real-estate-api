namespace Homely.Application.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public string SecretBase64 { get; init; } = "";
    public int AccessTokenMinutes { get; init; }
    public int RefreshTokenDays { get; init; }
}