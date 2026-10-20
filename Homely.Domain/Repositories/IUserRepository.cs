using Homely.Domain.Entities;

namespace Homely.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> DeleteAsync(User user);
}