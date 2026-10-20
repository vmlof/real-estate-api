using System.IdentityModel.Tokens.Jwt;
using Homely.Application.Authentication;
using Homely.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Homely.Infrastructure.Authentication;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public int UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true ||
                !int.TryParse(user.FindFirst("sub")?.Value, out var id))
            {
                throw new UnauthorizedAccessException();
            }

            return id;
        }
    }

    public string Email
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException();

            return user.FindFirst("email")?.Value
                   ?? throw new UnauthorizedAccessException();
        }
    }
}