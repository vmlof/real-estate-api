using Homely.Application.UseCases;
using Homely.Application.UseCases.RefreshToken;
using Homely.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Login(RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}