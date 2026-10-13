namespace Homely.Domain.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
}