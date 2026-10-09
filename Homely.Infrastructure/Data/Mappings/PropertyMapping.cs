using Homely.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homely.Infrastructure.Data.Mappings;

public class PropertyMapping : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("properties");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(e => e.PricePerMonth)
            .HasColumnName("price_per_month")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.SecurityDeposit)
            .HasColumnName("security_deposit")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.ApplicationFee)
            .HasColumnName("application_fee")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.PhotoUrls)
            .HasColumnName("photo_urls")
            .HasField("_photoUrls")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(e => e.Amenities)
            .HasColumnName("amenities")
            .HasField("_amenities")
            .HasColumnType("amenity[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(e => e.HighLights)
            .HasColumnName("highlights")
            .HasField("_highlights")
            .HasColumnType("highlight[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(e => e.IsPetsAllowed)
            .HasColumnName("is_pets_allowed")
            .IsRequired();

        builder.Property(e => e.IsParkingIncluded)
            .HasColumnName("is_parking_included")
            .IsRequired();

        builder.Property(e => e.Beds)
            .HasColumnName("beds")
            .IsRequired();

        builder.Property(e => e.Baths)
            .HasColumnName("baths")
            .IsRequired();

        builder.Property(e => e.SquareFeet)
            .HasColumnName("square_feet")
            .IsRequired();

        builder.Property(e => e.PropertyType)
            .HasColumnName("property_type")
            .IsRequired();

        builder.Property(e => e.PostedDate)
            .HasColumnName("posted_date")
            .IsRequired();

        builder.Property(e => e.AverageRating)
            .HasColumnName("average_rating");

        builder.Property(e => e.NumberOfReviews)
            .HasColumnName("number_of_reviews");

        builder.Property(e => e.LocationId)
            .HasColumnName("location_id")
            .IsRequired();

        builder.Property(e => e.ManagerId)
            .HasColumnName("manager_id")
            .IsRequired();

        builder.HasOne(p => p.Manager)
            .WithMany(m => m.Properties)
            .HasForeignKey(p => p.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Location)
            .WithOne(l => l.Property)
            .HasForeignKey<Property>(p => p.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.ManagerId)
            .IsUnique()
            .HasDatabaseName("ix_properties_manager_id");

        builder.HasIndex(p => p.LocationId)
            .IsUnique()
            .HasDatabaseName("ix_properties_location_id");
    }
}