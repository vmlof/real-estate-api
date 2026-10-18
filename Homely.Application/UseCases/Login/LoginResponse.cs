namespace Homely.Application.UseCases.Login;

public record LoginResponse(string AccessToken, string RefreshToken);