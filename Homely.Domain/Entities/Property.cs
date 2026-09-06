using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class Property
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal PricePerMonth { get; private set; }
    public decimal SecurityDeposit { get; private set; }
    public decimal ApplicationFee { get; private set; }
    public string PhotoUrls { get; private set; } = string.Empty; // string[]
    public string Amenities { get; private set; } = string.Empty; // amenity[]
    public string Highlights { get; private set; } = string.Empty; // highlight[]
    public bool IsPetsAllowed { get; private set; }
    public bool IsParkingIncluded { get; private set; }
    public int Beds { get; private set; }
    public float Baths { get; private set; }
    public int SquareFeet { get; private set; }
    public PropertyType PropertyType { get; private set; } 
    public DateTime PostedDate { get; private set; }
    public float AverageRating { get; private set; }
    public int? NumberOfReviews { get; private set; }
    public int LocationId { get; private set; } // FK
    public Location Location { get; private set; } = null!;
    public int ManagerId { get; private set; } // FK
    public Manager Manager { get; private set; } = null!;
}