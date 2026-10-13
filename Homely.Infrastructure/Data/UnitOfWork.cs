using Homely.Domain.Abstractions;

namespace Homely.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync() => await context.SaveChangesAsync();
}