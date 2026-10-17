using Homely.Domain.Entities;
using Homely.Domain.Repositories;
using Homely.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Homely.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken> GetRefreshToken(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(r =>
                r.Token == refreshToken
                && !r.IsRevoked &&
                r.ExpiresAt > DateTime.UtcNow);

        return token;
    }
}