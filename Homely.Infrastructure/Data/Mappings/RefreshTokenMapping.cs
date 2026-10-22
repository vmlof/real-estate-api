using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshTokens>
{
    public void Configure(EntityTypeBuilder<RefreshTokens> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.SessionId).IsRequired();

        builder.Property(r => r.TokenHash)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.ExpiresAt)
            .IsRequired();

        builder.Property(r => r.IsRevoked)
            .IsRequired();


        builder.HasIndex(r => r.TokenHash).IsUnique();
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.SessionId);

        builder.HasOne<RefreshSession>().WithMany().HasForeignKey(r => r.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
