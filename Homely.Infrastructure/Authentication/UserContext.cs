using System.IdentityModel.Tokens.Jwt;
using Homely.Application.Authentication;
using Microsoft.AspNetCore.Http;

namespace Homely.Infrastructure.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => int.Parse(
        _httpContextAccessor.HttpContext?.User
            .FindFirst(JwtRegisteredClaimNames.Sub)?.Value
        ?? throw new UnauthorizedAccessException());

    public string Email =>
        _httpContextAccessor.HttpContext?.User
            .FindFirst(JwtRegisteredClaimNames.Email)?.Value
        ?? throw new UnauthorizedAccessException();

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}