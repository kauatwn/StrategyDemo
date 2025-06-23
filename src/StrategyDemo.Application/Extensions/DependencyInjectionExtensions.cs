using Microsoft.Extensions.DependencyInjection;
using StrategyDemo.Application.Contexts;
using StrategyDemo.Application.Interfaces.Contexts;
using StrategyDemo.Application.Interfaces.UseCases;
using StrategyDemo.Application.UseCases;

namespace StrategyDemo.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddContexts(services);
        AddUseCases(services);
    }

    private static void AddContexts(IServiceCollection services) =>
        services.AddScoped<IShippingContext, ShippingContext>();

    private static void AddUseCases(IServiceCollection services) =>
        services.AddScoped<ICalculateShippingCostUseCase, CalculateShippingCostUseCase>();
}