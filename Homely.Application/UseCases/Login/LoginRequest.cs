using MediatR;

namespace Homely.Application.UseCases;

public sealed record LoginRequest(string Email, string Password) : IRequest<LoginResponse>;