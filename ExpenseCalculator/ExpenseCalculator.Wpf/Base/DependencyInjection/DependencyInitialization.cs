namespace ExpenseCalculator.Wpf.Base.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Initialization of the dependencies of the application.
/// </summary>
public static class DependencyInitialization
{
    /// <summary>
    ///     Initialize a <seealso cref="IServiceProvider" /> that provides the dependencies of the application.
    /// </summary>
    /// <param name="dependencies">The <paramref name="dependencies" /> are added to new <see cref="IServiceCollection" />.</param>
    /// <returns>The initialized <see cref="IServiceProvider" />.</returns>
    public static IServiceProvider InitializeDependencies(
        params Func<IServiceCollection, IServiceCollection>[] dependencies
    )
    {
        if (dependencies.Length == 0)
        {
            throw new ArgumentException(
                "No dependencies specified.",
                nameof(dependencies));
        }

        var services = new ServiceCollection();

        foreach (var dependency in dependencies)
        {
            dependency(services);
        }

        return services.BuildServiceProvider();
    }
}
