using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class LeaseMapping : IEntityTypeConfiguration<Lease>
{
    public void Configure(EntityTypeBuilder<Lease> builder)
    {
        builder.ToTable("leases");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.StartDate)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Property(e => e.EndDate)
            .HasColumnName("end_date")
            .IsRequired();

        builder.Property(e => e.Rent)
            .HasColumnName("rent")
            .IsRequired();

        builder.Property(e => e.Deposit)
            .HasColumnName("deposit")
            .IsRequired();

        builder.Property(e => e.PropertyId) // FK
            .HasColumnName("property_id")
            .IsRequired();

        builder.Property(e => e.TenantCognitoId)
            .HasColumnName("tenant_cognito_id")
            .IsRequired();
    }
}