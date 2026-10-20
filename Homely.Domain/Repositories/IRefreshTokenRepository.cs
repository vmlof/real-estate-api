using Homely.Domain.Entities;

namespace Homely.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshTokens tokens, CancellationToken cancellationToken);

    Task<RefreshTokens?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken);

    Task<bool> RotateAsync(Guid currentTokenId, DateTime now, RefreshTokens replacement,
        CancellationToken cancellationToken);

    Task RevokeAsync(string tokenHash, CancellationToken cancellationToken);
}