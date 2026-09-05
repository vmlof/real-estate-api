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
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(e => e.PricePerMonth)
            .HasColumnName("price_per_month")
            .IsRequired();
        
        builder.Property(e => e.SecurityDeposit)
            .HasColumnName("security_deposit")
            .IsRequired();
        
        builder.Property(e => e.ApplicationFee)
            .HasColumnName("application_fee")
            .IsRequired();
        
        builder.Property(e => e.PhotoUrls)
            .HasColumnName("photo_urls")
            .IsRequired();
        
        builder.Property(e => e.Amenities)
            .HasColumnName("amenities")
            .IsRequired();
        
        builder.Property(e => e.Highlights)
            .HasColumnName("highlights")
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
            .HasColumnName("average_rating")
            .IsRequired();


        builder.Property(e => e.NumberOfReviews)
            .HasColumnName("number_of_reviews");
        
        builder.Property(e => e.LocationId)
            .HasColumnName("location_id")
            .IsRequired();
        
        builder.Property(e => e.ManagerCognitoId)
            .HasColumnName("manager_cognito_id")
            .IsRequired();
    }
}