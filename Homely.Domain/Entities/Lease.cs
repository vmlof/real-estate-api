namespace Homely.Domain.Entities;

public class Lease
{
    public int Id { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal Rent { get; private set; }
    public decimal Deposit { get; private set; }
    public int PropertyId { get; private set; } // FK
    public string TenantCognitoId { get; private set; } = string.Empty; // FK
}