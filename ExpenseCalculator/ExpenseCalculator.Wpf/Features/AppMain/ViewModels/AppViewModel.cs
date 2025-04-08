namespace ExpenseCalculator.Wpf.Features.AppMain.ViewModels;

using ExpenseCalculator.Wpf.Base.Commands;
using ExpenseCalculator.Wpf.Base.ViewModels;

/// <summary>
///     Describes the <see cref="AppViewModel" />.
/// </summary>
/// <param name="commandSync">A command synchronization object.</param>
/// <seealso cref="IAppViewModel" />
/// <seealso cref="ViewModelBase" />
internal class AppViewModel(ICommandSync commandSync) : ViewModelBase(commandSync), IAppViewModel;
