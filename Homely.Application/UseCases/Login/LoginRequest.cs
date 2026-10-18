using Homely.Domain.Common;
using MediatR;

namespace Homely.Application.UseCases.Login;

public sealed record LoginRequest(string Email, string Password) : IRequest<Result<LoginResponse>>;