using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public sealed class RefreshSessionMapping : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable("refresh_sessions");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.IsRevoked).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
