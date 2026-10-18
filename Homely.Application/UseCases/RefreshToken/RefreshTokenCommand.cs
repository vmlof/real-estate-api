using Homely.Application.UseCases.Login;
using MediatR;

namespace Homely.Application.UseCases.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponse>;