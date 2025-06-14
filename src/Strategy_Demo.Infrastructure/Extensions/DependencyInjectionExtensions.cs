using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Domain.Interfaces.Factories;
using Strategy_Demo.Domain.Interfaces.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Strategy_Demo.Infrastructure.Factories;
using Strategy_Demo.Infrastructure.Strategies.Shipping;

namespace Strategy_Demo.Infrastructure.Extensions;

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

    private static void AddFactories(IServiceCollection services) =>
        services.AddSingleton<IShippingStrategyFactory, ShippingStrategyFactory>(provider =>
            new ShippingStrategyFactory(method => provider.GetRequiredKeyedService<IShippingStrategy>(method)));
}