using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.Contexts;
using Application.UseCases;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Infrastructure.Strategies.Shipping;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.IoC;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddContexts(services);
        AddUseCases(services);
    }

    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddStrategies(services);
        AddFactories(services);
    }

    private static void AddContexts(IServiceCollection services)
    {
        services.AddScoped<IShippingContext, ShippingContext>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICalculateShippingCostUseCase, CalculateShippingCostUseCase>();
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