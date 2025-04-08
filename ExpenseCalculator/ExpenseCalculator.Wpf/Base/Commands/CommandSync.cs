namespace ExpenseCalculator.Wpf.Base.Commands;

using System.ComponentModel;

/// <summary>
///     Grants or denies permission to execute a command.
/// </summary>
/// <seealso cref="ICommandSync" />
internal class CommandSync : ICommandSync
{
    /// <summary>
    ///     Protects the access to <see cref="activeCount" />.
    /// </summary>
    private readonly Lock lockObject = new();

    /// <summary>
    ///     Counts the commands that are executed.
    /// </summary>
    private int activeCount;

    /// <summary>
    ///     Indicates weather a command is active (<c>true</c>) or no command is running (<c>false</c>).
    /// </summary>
    private bool isActive;

    /// <summary>
    ///     Gets a value that indicates weather a command is active (<c>true</c>) or no command is running (<c>false</c>).
    /// </summary>
    public bool IsActive
    {
        get => this.isActive;
        private set
        {
            this.isActive = value;
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(CommandSync.IsActive)));
        }
    }

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    /// <remarks>
    ///     If permission is granted, call <see cref="Exit" /> after command termination. The method is request a
    ///     <see cref="Lock" /> without any timeout and can cause deadlocks.
    /// </remarks>
    public bool Enter(bool force = false)
    {
        lock (this.lockObject)
        {
            return this.EnterLocked(force);
        }
    }

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="millisecondsTimeout">The permission request is aborted after this amount of milliseconds.</param>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    public bool Enter(int millisecondsTimeout, bool force = false)
    {
        if (!this.lockObject.TryEnter(millisecondsTimeout))
        {
            return false;
        }

        try
        {
            return this.EnterLocked(force);
        }
        finally
        {
            this.lockObject.Exit();
        }
    }

    /// <summary>
    ///     Request the permission to execute a command.
    /// </summary>
    /// <param name="timeout">The permission request is aborted after this <see cref="TimeSpan" />.</param>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    public bool Enter(TimeSpan timeout, bool force = false)
    {
        if (!this.lockObject.TryEnter(timeout))
        {
            return false;
        }

        try
        {
            return this.EnterLocked(force);
        }
        finally
        {
            this.lockObject.Exit();
        }
    }

    /// <summary>
    ///     Indicates that a command execution terminated.
    /// </summary>
    /// <remarks>
    ///     Call <see cref="Enter(bool)" />, <see cref="Enter(int,bool)" /> or <see cref="Enter(TimeSpan,bool)" /> before
    ///     <see cref="Exit" />.
    /// </remarks>
    public void Exit()
    {
        lock (this.lockObject)
        {
            this.activeCount = Math.Max(
                0,
                this.activeCount - 1);

            if (this.activeCount == 0)
            {
                this.IsActive = false;
            }
        }
    }

    /// <summary>
    ///     Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Execute the enter logic in a locked context.
    /// </summary>
    /// <param name="force">
    ///     Grant access even if a command is running (<c>true</c>); deny the permission if a command is active
    ///     otherwise (<c>false</c>).
    /// </param>
    /// <returns><c>True</c> if permission to execute is granted; <c>false</c> otherwise.</returns>
    private bool EnterLocked(bool force)
    {
        if (this.activeCount != 0 && !force)
        {
            return false;
        }

        // called with a lock statement only
        // ReSharper disable once InconsistentlySynchronizedField
        this.activeCount++;

        if (this.activeCount == 1)
        {
            this.IsActive = true;
        }

        return true;
    }
}
