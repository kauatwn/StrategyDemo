using Microsoft.Extensions.DependencyInjection;
using Strategy_Demo.Application.Contexts;
using Strategy_Demo.Application.Interfaces.Contexts;
using Strategy_Demo.Application.Interfaces.UseCases;
using Strategy_Demo.Application.UseCases;

namespace Strategy_Demo.Application.Extensions;

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