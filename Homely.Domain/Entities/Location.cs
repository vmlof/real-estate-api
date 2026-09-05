namespace Homely.Domain.Entities;

public record Coordinate(double Latitude, double Longitude);

public class Location
{
    protected Location()
    {
    }

    public int Id { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;
    public Coordinate? Coordinates { get; private set; }

    public void SetCoordinates(double latitude, double longitude)
    {
        Coordinates = new Coordinate(latitude, longitude);
    }
}