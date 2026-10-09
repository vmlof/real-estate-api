using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class Property
{
    protected Property()
    {
        _photoUrls = new List<string>();
        _amenities = new List<Amenity>();
        _highlights = new List<HighLight>();
    }

    public Property(
        string name,
        string description,
        decimal pricePerMonth,
        decimal securityDeposit,
        decimal applicationFee,
        List<string> photosUrls,
        List<Amenity> amenities,
        List<HighLight> highlights,
        bool isPetsAllowed,
        bool isParkingIncluded,
        int beds,
        float baths,
        int squareFeet,
        PropertyType propertyType,
        DateTime postedDate,
        Location location,
        int managerId)
    {
        Name = name;
        Description = description;
        PricePerMonth = pricePerMonth;
        SecurityDeposit = securityDeposit;
        ApplicationFee = applicationFee;
        _photoUrls = new List<string>(photosUrls);
        _amenities = new List<Amenity>(amenities);
        _highlights = new List<HighLight>(highlights);
        IsPetsAllowed = isPetsAllowed;
        IsParkingIncluded = isParkingIncluded;
        Beds = beds;
        Baths = baths;
        SquareFeet = squareFeet;
        PropertyType = propertyType;
        PostedDate = postedDate;
        AverageRating = 0;
        NumberOfReviews = 0;
        Location = location;
        ManagerId = managerId;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal PricePerMonth { get; private set; }
    public decimal SecurityDeposit { get; private set; }
    public decimal ApplicationFee { get; private set; }
    private readonly List<string> _photoUrls;
    private readonly List<Amenity> _amenities;
    private readonly List<HighLight> _highlights;
    public bool IsPetsAllowed { get; private set; }
    public bool IsParkingIncluded { get; private set; }
    public int Beds { get; private set; }
    public float Baths { get; private set; }
    public int SquareFeet { get; private set; }
    public PropertyType PropertyType { get; private set; }
    public DateTime PostedDate { get; private set; }
    public float? AverageRating { get; private set; }
    public int? NumberOfReviews { get; private set; }
    public int LocationId { get; private set; }
    public Location Location { get; private set; } = null!;
    public int ManagerId { get; private set; }
    public Manager Manager { get; private set; } = null!;
    public IReadOnlyCollection<string> PhotoUrls => _photoUrls.AsReadOnly();
    public IReadOnlyCollection<Amenity> Amenities => _amenities.AsReadOnly();
    public IReadOnlyCollection<HighLight> HighLights => _highlights.AsReadOnly();
}