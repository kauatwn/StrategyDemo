using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Factories;

public class ShippingStrategyFactory(IServiceProvider serviceProvider) : IShippingStrategyFactory
{
    public IShippingStrategy? Create(ShippingMethod method) =>
        serviceProvider.GetKeyedService<IShippingStrategy>(method);
}