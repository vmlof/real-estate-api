namespace Homely.Domain.Entities;

public class FavoriteProperty
{
    protected FavoriteProperty()
    {
    }

    public FavoriteProperty(int propertyId, int tenantId)
    {
        PropertyId = propertyId;
        TenantId = tenantId;
    }

    public int PropertyId { get; private set; }
    public Property Property { get; set; } = null!;

    public int TenantId { get; private set; }
    public Tenant Tenant { get; set; } = null!;
}