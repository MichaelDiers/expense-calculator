namespace ExpenseCalculator.Wpf;

using ExpenseCalculator.Wpf.Base;
using ExpenseCalculator.Wpf.Features.AppMain;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class AppServiceCollectionExtensions
{
    /// <summary>
    ///     Adds all supported dependencies to the given <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddApp(this IServiceCollection services)
    {
        services.TryAddBase();

        services.TryAddAppMain();

        return services;
    }
}
