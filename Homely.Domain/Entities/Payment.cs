namespace Homely.Domain.Entities;

public class Payment
{
    protected Payment()
    {
    }


    public Payment(int leaseId, decimal amountDue, DateTime dueDate, int paymentStatus)
    {
        LeaseId = leaseId;
        AmountDue = amountDue;
        DueDate = dueDate;
        PaymentStatus = paymentStatus;
        AmountPaid = 0;
    }

    public int Id { get; private set; }
    public decimal AmountDue { get; private set; }
    public decimal AmountPaid { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public int PaymentStatus { get; private set; } // PaymentStatus
    public int LeaseId { get; private set; }
    public Lease Lease { get; private set; } = null!;

    public void ProcessPayment(decimal amountPaid, DateTime paymentDate)
    {
        AmountPaid = amountPaid;
        PaymentDate = paymentDate;
    }
}