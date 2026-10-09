using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class Payment
{
    protected Payment()
    {
    }


    public Payment(decimal amountDue, decimal amountPaid, DateTime dueDate, DateTime? paymentDate,
        PaymentStatus paymentStatus, int leaseId)
    {
        AmountDue = amountDue;
        AmountPaid = amountPaid;
        DueDate = dueDate;
        PaymentStatus = paymentStatus;
        LeaseId = leaseId;
        PaymentDate = paymentDate;
    }

    public int Id { get; private set; }
    public decimal AmountDue { get; private set; }
    public decimal AmountPaid { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public int LeaseId { get; private set; }
    public Lease Lease { get; private set; } = null!;
}