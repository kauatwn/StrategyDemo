using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Factories;

public class ShippingStrategyFactory(IServiceProvider serviceProvider) : IShippingStrategyFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IShippingStrategy? Create(ShippingMethod method)
    {
        return _serviceProvider.GetKeyedService<IShippingStrategy>(method);
    }
}