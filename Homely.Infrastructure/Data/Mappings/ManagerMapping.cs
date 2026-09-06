using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class ManagerMapping : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable("managers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

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

        builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Manager>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata.FindNavigation(nameof(Manager.Properties))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(m => m.UserId)
            .IsUnique()
            .HasDatabaseName("ix_managers_user_id");
    }
}