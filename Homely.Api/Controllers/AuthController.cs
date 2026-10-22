using Homely.Api.Extensions;
using Homely.Application.Authentication;
using Homely.Application.UseCases.Login;
using Homely.Application.UseCases.Logout;
using Homely.Application.UseCases.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Homely.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly JwtOptions _jwt;

    public AuthController(IMediator mediator, JwtOptions jwt)
    {
        _mediator = mediator;
        _jwt = jwt;
    }

    [EnableRateLimiting("auth")]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(new
            {
                result.Error.Code,
                result.Error.Description
            });

        SetRefreshTokenCookie(result.Value.RefreshToken);
        return Ok(new { result.Value.AccessToken });
    }

    [EnableRateLimiting("auth")]
    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        var rawToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrWhiteSpace(rawToken))
            return Unauthorized();

        var result = await _mediator.Send(new RefreshTokenCommand(rawToken), cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(new
            {
                result.Error.Code,
                result.Error.Description
            });
        }

        SetRefreshTokenCookie(result.Value.RefreshToken);
        return Ok(new { result.Value.AccessToken });
    }

    [EnableRateLimiting("auth")]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var rawToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrWhiteSpace(rawToken))
            await _mediator.Send(new LogoutCommand(rawToken), cancellationToken);

        DeleteRefreshTokenCookie();
        return NoContent();
    }

    private void SetRefreshTokenCookie(string token)
    {
        Response.Cookies.Append("refreshToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // false -> http
            SameSite = SameSiteMode.Strict,
            Path = "/api/auth",
            MaxAge = TimeSpan.FromDays(_jwt.RefreshTokenDays)
        });
    }

    private void DeleteRefreshTokenCookie()
    {
        Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/api/auth"
        });
    }
}