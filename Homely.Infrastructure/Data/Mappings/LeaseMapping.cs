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
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.Deposit)
            .HasColumnName("deposit")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.PropertyId)
            .HasColumnName("property_id")
            .IsRequired();

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id")
            .IsRequired();


        builder.HasOne(l => l.Property)
            .WithMany()
            .HasForeignKey(l => l.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Tenant)
            .WithMany(t => t.Leases)
            .HasForeignKey(l => l.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(l => l.PropertyId)
            .HasDatabaseName("ix_leases_property_id");

        builder.HasIndex(l => l.TenantId)
            .HasDatabaseName("ix_leases_tenant_id");
    }
}