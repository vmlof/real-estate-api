
using Microsoft.Extensions.DependencyInjection;

namespace Homely.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}