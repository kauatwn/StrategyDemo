using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Infrastructure.Strategies.Shipping;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AddStrategies(services);
        AddFactories(services);

        return services;
    }

    private static void AddStrategies(IServiceCollection services)
    {
        services.AddKeyedTransient<IShippingStrategy, StandardShippingStrategy>(ShippingMethod.Standard);
        services.AddKeyedTransient<IShippingStrategy, ExpressShippingStrategy>(ShippingMethod.Express);
    }

    private static void AddFactories(IServiceCollection services)
    {
        services.AddSingleton<IShippingStrategyFactory, ShippingStrategyFactory>();
    }
}