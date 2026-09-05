using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class PaymentMapping : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.AmountDue)
            .HasColumnName("amount_due");

        builder.Property(e => e.AmountPaid)
            .HasColumnName("amount_paid");

        builder.Property(e => e.DueDate)
            .HasColumnName("due_date");
        
        
        builder.Property(e => e.PaymentDate)
            .HasColumnName("payment_date");
        
        builder.Property(e => e.PaymentStatus)
            .HasColumnName("payment_status");

        builder.Property(e => e.LeaseId) // FK
        .HasColumnName("lease_id");
    }
}