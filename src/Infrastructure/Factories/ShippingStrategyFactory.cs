using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Factories;

public class ShippingStrategyFactory(IServiceProvider serviceProvider) : IShippingStrategyFactory
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;

    public IShippingStrategy? Create(ShippingMethod method)
    {
        return ServiceProvider.GetKeyedService<IShippingStrategy>(method);
    }
}