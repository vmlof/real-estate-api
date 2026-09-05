namespace Homely.Domain.Entities;

public class Payment
{
    public int Id { get; private set; }
    public decimal AmountDue { get; private set; }
    public decimal AmountPaid { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public int PaymentStatus { get; private set; } // PaymentStatus
    public int LeaseId { get; private set; } // FK
}