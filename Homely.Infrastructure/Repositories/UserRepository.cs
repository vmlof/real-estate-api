using Homely.Domain.Entities;
using Homely.Domain.Repositories;
using Homely.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Homely.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }
}