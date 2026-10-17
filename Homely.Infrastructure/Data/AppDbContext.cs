using Homely.Domain.Entities;
using Homely.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Homely.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<Lease> Leases { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<RentalApplication> Applications { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Amenity>();
        modelBuilder.HasPostgresEnum<HighLight>(name: "highlight");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}