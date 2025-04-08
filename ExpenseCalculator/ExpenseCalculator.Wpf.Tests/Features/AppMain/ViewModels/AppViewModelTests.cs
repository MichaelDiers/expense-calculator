namespace ExpenseCalculator.Wpf.Tests.Features.AppMain.ViewModels;

using ExpenseCalculator.Wpf.Base;
using ExpenseCalculator.Wpf.Base.Commands;
using ExpenseCalculator.Wpf.Base.DependencyInjection;
using ExpenseCalculator.Wpf.Features.AppMain;
using ExpenseCalculator.Wpf.Features.AppMain.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Tests of <see cref="IAppViewModel" />.
/// </summary>
public class AppViewModelTests
{
    /// <summary>
    ///     Tests the initialization of the <seealso cref="IAppViewModel" /> implementing class.
    /// </summary>
    [Fact]
    public void Ctor()
    {
        Assert.IsAssignableFrom<IAppViewModel>(AppViewModelTests.Initialize());
    }

    /// <summary>
    ///     Initialize a new instance of the <seealso cref="IAppViewModel" /> implementing class.
    /// </summary>
    /// <param name="commandSync">
    ///     If specified sets the <seealso cref="ICommandSync" /> as a dependency; sets
    ///     <see cref="BaseServiceCollectionExtensions.TryAddCommandSync" /> otherwise.
    /// </param>
    /// <returns>The initialized <seealso cref="IAppViewModel" />.</returns>
    private static IAppViewModel Initialize(ICommandSync? commandSync = null)
    {
        return DependencyInitialization.InitializeDependencies(
                AppMainServiceCollectionExtensions.TryAddAppViewModel,
                commandSync is not null
                    ? services =>
                    {
                        services.TryAddSingleton(commandSync);
                        return services;
                    }
                    : BaseServiceCollectionExtensions.TryAddCommandSync)
            .GetRequiredService<IAppViewModel>();
    }
}
