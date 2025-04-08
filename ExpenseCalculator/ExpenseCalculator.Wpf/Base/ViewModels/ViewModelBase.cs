namespace ExpenseCalculator.Wpf.Base.ViewModels;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseCalculator.Wpf.Base.Commands;

/// <summary>
///     Base class for view models.
/// </summary>
/// <param name="commandSync">A command synchronization object.</param>
/// <seealso cref="IViewModelBase" />
internal class ViewModelBase(ICommandSync commandSync) : IViewModelBase
{
    /// <summary>
    ///     Gets a command synchronization object.
    /// </summary>
    protected ICommandSync CommandSync { get; } = commandSync;

    /// <summary>
    ///     Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Raises the <seealso cref="PropertyChanged" /> event.
    /// </summary>
    /// <param name="propertyName">The name of the property that value changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Sets the value of <paramref name="field" /> to <paramref name="value" /> and raises
    ///     <seealso cref="PropertyChanged" /> if the value of <paramref name="field" /> changed.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="field" />.</typeparam>
    /// <param name="field">The field that is set to a new value.</param>
    /// <param name="value">The new value of <paramref name="field" />.</param>
    /// <param name="propertyName">The name of the property that is changed.</param>
    /// <returns><c>True</c> if the property value changed and <c>false</c> otherwise.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(
                field,
                value))
        {
            return false;
        }

        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}
