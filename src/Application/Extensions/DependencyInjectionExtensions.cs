using Application.Contexts;
using Application.Interfaces.Contexts;
using Application.Interfaces.UseCases;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

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