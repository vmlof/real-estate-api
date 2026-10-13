using Homely.Domain.Abstractions;
using Homely.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Homely.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}