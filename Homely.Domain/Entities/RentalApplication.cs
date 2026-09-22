using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class RentalApplication
{
    protected RentalApplication()
    {
    }

    public RentalApplication(int propertyId, int tenantId, string name, string email, string phoneNumber,
        string? message = null)
    {
        PropertyId = propertyId;
        TenantId = tenantId;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Message = message;

        ApplicationDate = DateTime.UtcNow;
        Status = ApplicationStatus.Pending;
    }

    public int Id { get; private set; }
    public DateTime ApplicationDate { get; private set; }
    public ApplicationStatus Status { get; private set; }
    public int PropertyId { get; private set; }
    public Property Property { get; private set; } = null!;
    public int TenantId { get; private set; }
    public Tenant Tenant { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? Message { get; private set; }
    public int? LeaseId { get; private set; }
    public Lease? Lease { get; private set; }
}