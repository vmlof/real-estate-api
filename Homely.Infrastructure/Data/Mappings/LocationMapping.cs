using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class LocationMapping : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Address)
            .HasColumnName("address")
            .IsRequired();

        builder.Property(e => e.City)
            .HasColumnName("city")
            .IsRequired();

        builder.Property(e => e.State)
            .HasColumnName("state")
            .IsRequired();

        builder.Property(e => e.Country)
            .HasColumnName("country")
            .IsRequired();

        builder.Property(e => e.PostalCode)
            .HasColumnName("postal_code")
            .IsRequired();

        builder.ComplexProperty(e => e.Coordinates, cb =>
        {
            cb.Property(e => e.Latitude)
                .HasColumnName("latitude");


            cb.Property(e => e.Longitude)
                .HasColumnName("longitude");
        });
    }
}