namespace Homely.Domain.Entities;

public class Lease
{
    protected Lease()
    {
    }

    public Lease(int propertyId, int tenantId, DateTime startDate, DateTime endDate, decimal rent, decimal deposit)
    {
        PropertyId = propertyId;
        TenantId = tenantId;
        StartDate = startDate;
        EndDate = endDate;
        Rent = rent;
        Deposit = deposit;
    }

    public int Id { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal Rent { get; private set; }
    public decimal Deposit { get; private set; }
    public int PropertyId { get; private set; }
    public Property Property { get; private set; } = null!;
    public int TenantId { get; private set; }
    public Tenant Tenant { get; private set; } = null!;
    public RentalApplication? Application { get; private set; }

    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }
}