using StrategyDemo.Domain.Enums;
using StrategyDemo.Domain.Interfaces.Factories;
using StrategyDemo.Domain.Interfaces.Strategies;
using Microsoft.Extensions.DependencyInjection;
using StrategyDemo.Infrastructure.Factories;
using StrategyDemo.Infrastructure.Strategies.Shipping;

namespace StrategyDemo.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddStrategies(services);
        AddFactories(services);
    }

    private static void AddStrategies(IServiceCollection services)
    {
        services.AddKeyedTransient<IShippingStrategy, StandardShippingStrategy>(ShippingMethod.Standard);
        services.AddKeyedTransient<IShippingStrategy, ExpressShippingStrategy>(ShippingMethod.Express);
    }

    private static void AddFactories(IServiceCollection services)
    {
        services.AddSingleton<IShippingStrategyFactory, ShippingStrategyFactory>(provider =>
        {
            return new ShippingStrategyFactory(method =>
                provider.GetRequiredKeyedService<IShippingStrategy>(method));
        });
    }
}