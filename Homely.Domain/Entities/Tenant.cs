namespace Homely.Domain.Entities;

public class Tenant
{
    public int Id { get; private set; }
    public string CognitoId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
}