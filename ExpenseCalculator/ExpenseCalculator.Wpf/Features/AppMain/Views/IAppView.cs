namespace ExpenseCalculator.Wpf.Features.AppMain.Views;

/// <summary>
///     Describes the <see cref="IAppView" />.
/// </summary>
public interface IAppView
{
    /// <summary>
    ///     Gets or sets the data context for an element when it participates in data binding.
    /// </summary>
    object? DataContext { get; set; }

    /// <summary>
    ///     Opens a window and returns only when the newly opened window is closed.
    /// </summary>
    /// <returns>
    ///     A <see cref="Nullable{T}" /> value of type <see cref="bool" /> that specifies whether the activity was
    ///     accepted (<c>true</c>) or canceled (<c>false</c>). The return value is the value of the
    ///     <see cref="System.Windows.Window.DialogResult" /> property before a window closes.
    /// </returns>
    bool? ShowDialog();
}
