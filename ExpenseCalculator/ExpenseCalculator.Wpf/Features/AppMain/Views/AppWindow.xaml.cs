namespace ExpenseCalculator.Wpf.Features.AppMain.Views;

using ExpenseCalculator.Wpf.Features.AppMain.ViewModels;

/// <summary>
///     Interaction logic for AppWindow.xaml
/// </summary>
public partial class AppWindow : IAppView
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AppWindow" /> class.
    /// </summary>
    /// <param name="viewModel">The data context of the view.</param>
    public AppWindow(IAppViewModel viewModel)
    {
        this.InitializeComponent();
        this.DataContext = viewModel;
    }
}
