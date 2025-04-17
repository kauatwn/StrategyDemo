using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.Contexts;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddContexts(services);
        AddUseCases(services);

        return services;
    }

    private static void AddContexts(IServiceCollection services)
    {
        services.AddScoped<IShippingContext, ShippingContext>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICalculateShippingCostUseCase, CalculateShippingCostUseCase>();
    }
}