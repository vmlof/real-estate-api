namespace Homely.Domain.Entities;

public class RentalApplication
{
    public int Id { get; private set; }
    public DateTime ApplicationDate { get; private set; }
    public int Status { get; private set; } // ApplicationStatus
    public int PropertyId { get; private set; } // FK
    public string TenantCognitoId { get; private set; } = string.Empty; // FK
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? Message { get; private set; }
    public int? LeaseId { get; private set; } // FK
}