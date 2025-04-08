namespace ExpenseCalculator.Wpf.Features.AppMain;

using ExpenseCalculator.Wpf.Features.AppMain.ViewModels;
using ExpenseCalculator.Wpf.Features.AppMain.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class AppMainServiceCollectionExtensions
{
    /// <summary>
    ///     Adds all supported dependencies to the given <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddAppMain(this IServiceCollection services)
    {
        services.TryAddAppView();
        services.TryAddAppViewModel();

        return services;
    }

    /// <summary>
    ///     Adds the <see cref="IAppView" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddAppView(this IServiceCollection services)
    {
        services.TryAddSingleton<IAppView, AppWindow>();

        return services;
    }

    /// <summary>
    ///     Adds the <see cref="IAppViewModel" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddAppViewModel(this IServiceCollection services)
    {
        services.TryAddSingleton<IAppViewModel, AppViewModel>();

        return services;
    }
}
