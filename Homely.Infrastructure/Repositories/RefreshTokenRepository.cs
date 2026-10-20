using Homely.Domain.Entities;
using Homely.Domain.Repositories;
using Homely.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Homely.Infrastructure.Repositories;

// TODO: verificar implementacao completa da classe
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task AddAsync(RefreshTokens tokens, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(tokens, cancellationToken);
    }

    public async Task<RefreshTokens?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(
                t => t.TokenHash == tokenHash, cancellationToken);
    }

    public async Task<bool> RotateAsync(Guid currentTokenId, DateTime now, RefreshTokens replacement,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);

        var affected = await _context.RefreshTokens
            .Where(t =>
                t.Id == currentTokenId &&
                !t.IsRevoked &&
                t.ExpiresAt > now)
            .ExecuteUpdateAsync(
                updates => updates.SetProperty(t => t.IsRevoked, true),
                cancellationToken);

        if (affected != 1)
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }

        await _context.RefreshTokens.AddAsync(
            replacement, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return true;
    }

    public async Task RevokeAsync(string tokenHash, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens
            .Where(t =>
                t.TokenHash == tokenHash && !t.IsRevoked)
            .ExecuteUpdateAsync(
                updates =>
                    updates.SetProperty(t => t.IsRevoked, true), cancellationToken);
    }
}