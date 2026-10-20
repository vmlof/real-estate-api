using Homely.Application.Authentication;
using Homely.Domain.Repositories;
using MediatR;

namespace Homely.Application.UseCases.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest;

public sealed class LogoutCommandHandler(IRefreshTokenRepository repository) : IRequestHandler<LogoutCommand>
{
    public Task Handle(LogoutCommand request,
        CancellationToken cancellationToken)
        => repository.RevokeAsync(RefreshTokenHasher.Hash(request.RefreshToken),
            cancellationToken);
}