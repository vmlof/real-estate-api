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

        builder.Property(e => e.PropertyId) // FK
            .HasColumnName("property_id");

        builder.Property(e => e.TenantCognitoId)
            .HasColumnName("tenant_cognito_id");

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired();

        builder.Property(e => e.Message)
            .HasColumnName("message");

        builder.Property(e => e.LeaseId) // FK
            .HasColumnName("lease_id");
    }
}