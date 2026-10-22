namespace Homely.Domain.Entities;

public sealed class RefreshSession
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public bool IsRevoked { get; set; }
}
