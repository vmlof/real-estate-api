using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class TenantMapping : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithOne()
            .HasForeignKey<Tenant>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.FavoritedProperties)
            .WithMany() // Depois, se precisar, você pode mapear na Property um IReadOnlyCollection<Tenant> FavoritedBy
            .UsingEntity("tenant_favorite_properties");

        builder.Metadata.FindNavigation(nameof(Tenant.Applications))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Tenant.Leases))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Tenant.FavoritedProperties))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(t => t.UserId)
            .IsUnique()
            .HasDatabaseName("ix_tenants_user_id");
    }
}