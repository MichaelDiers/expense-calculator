namespace ExpenseCalculator.Wpf.Base.Commands;

using System.ComponentModel;

/// <summary>
///     Grants or denies permission to execute a command.
/// </summary>
public interface ICommandSync : INotifyPropertyChanged
{
    /// <summary>
    ///     Gets a value that indicates weather a command is active (<c>true</c>) or no command is running (<c>false</c>).
    /// </summary>
    public bool IsActive { get; }

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    bool Enter(bool force = false);

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="millisecondsTimeout">The permission request is aborted after this amount of milliseconds.</param>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    bool Enter(int millisecondsTimeout, bool force = false);

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="timeout">The permission request is aborted after this <see cref="TimeSpan" />.</param>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    bool Enter(TimeSpan timeout, bool force = false);

    /// <summary>
    ///     Indicates that a command execution terminated.
    /// </summary>
    void Exit();
}
