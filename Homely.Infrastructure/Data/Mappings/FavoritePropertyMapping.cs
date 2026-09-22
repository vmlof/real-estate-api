using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class FavoritePropertyMapping : IEntityTypeConfiguration<FavoriteProperty>
{
    public void Configure(EntityTypeBuilder<FavoriteProperty> builder)
    {
        builder.ToTable("favorite_properties");

        builder.HasKey(fp => new
        {
            fp.TenantId,
            fp.PropertyId
        });

        builder.Property(fp => fp.TenantId)
            .HasColumnName("tenant_id")
            .IsRequired();

        builder.Property(fp => fp.PropertyId)
            .HasColumnName("property_id")
            .IsRequired();

        builder.HasOne(fp => fp.Tenant)
            .WithMany(t => t.FavoriteProperties)
            .HasForeignKey(fp => fp.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fp => fp.Property)
            .WithMany()
            .HasForeignKey(fp => fp.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}