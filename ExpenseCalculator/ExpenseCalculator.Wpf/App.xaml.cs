namespace ExpenseCalculator.Wpf;

using System.Windows;
using ExpenseCalculator.Wpf.Base.DependencyInjection;
using ExpenseCalculator.Wpf.Features.AppMain.Views;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    ///     Handles the <seealso cref="Application.Startup" /> event.
    /// </summary>
    /// <param name="sender">The <see cref="object" /> that raised the event.</param>
    /// <param name="e">The data of the event.</param>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        var provider = DependencyInitialization.InitializeDependencies(AppServiceCollectionExtensions.TryAddApp);
        var window = provider.GetRequiredService<IAppView>();
        window.ShowDialog();
    }
}
