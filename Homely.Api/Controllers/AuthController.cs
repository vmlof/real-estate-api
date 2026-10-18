using Homely.Api.Extensions;
using Homely.Application.UseCases.Login;
using Homely.Application.UseCases.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Homely.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(request);

        return result.Match<LoginResponse, ActionResult>(
            onSuccess: response => Ok(response),
            onFailure: error => Unauthorized(new
            {
                error.Code,
                error.Description
            })
        );
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Login(RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}