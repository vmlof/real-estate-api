using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class RentalApplicationMapping : IEntityTypeConfiguration<RentalApplication>
{
    public void Configure(EntityTypeBuilder<RentalApplication> builder)
    {
        builder.ToTable("applications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.ApplicationDate)
            .HasColumnName("application_date");

        builder.Property(e => e.Status)
            .HasColumnName("status");

        builder.Property(e => e.PropertyId)
            .HasColumnName("property_id");

        builder.Property(e => e.TenantId)
            .HasColumnName("tenant_id");

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.Message)
            .HasColumnName("message")
            .HasMaxLength(1000);

        builder.Property(e => e.LeaseId)
            .HasColumnName("lease_id");

        builder.HasOne(a => a.Property)
            .WithMany()
            .HasForeignKey(a => a.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Tenant)
            .WithMany(t => t.Applications)
            .HasForeignKey(a => a.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Lease)
            .WithOne()
            .HasForeignKey<RentalApplication>(a => a.LeaseId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(a => a.PropertyId)
            .HasDatabaseName("ix_applications_property_id");

        builder.HasIndex(a => a.TenantId)
            .HasDatabaseName("ix_applications_tenant_id");

        builder.HasIndex(a => a.LeaseId)
            .IsUnique()
            .HasDatabaseName("ix_applications_lease_id");
    }
}