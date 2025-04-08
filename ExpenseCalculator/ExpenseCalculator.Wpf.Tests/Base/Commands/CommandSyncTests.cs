namespace ExpenseCalculator.Wpf.Tests.Base.Commands;

using ExpenseCalculator.Wpf.Base;
using ExpenseCalculator.Wpf.Base.Commands;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="ICommandSync" />.
/// </summary>
public class CommandSyncTests
{
    /// <summary>
    ///     Test data for <see cref="ICommandSync.Enter(bool)" />, <see cref="ICommandSync.Enter(int,bool)" /> and
    ///     <see cref="ICommandSync.Enter(TimeSpan, bool)" />.
    ///     Specification:
    ///     bool? force: The force parameter of the method under test.
    ///     int activeCount: Specifies the number of active commands.
    ///     bool isActiveChanged: specifies if <see cref="ICommandSync.IsActive" /> should change during test execution.
    ///     bool expectedEnterResult: The expected result of the method under test.
    /// </summary>
    public static readonly IEnumerable<ITheoryDataRow> EnterTestData =
    [
        new TheoryDataRow(
            null,
            0,
            true,
            true),
        new TheoryDataRow(
            true,
            0,
            true,
            true),
        new TheoryDataRow(
            false,
            0,
            true,
            true),
        new TheoryDataRow(
            null,
            1,
            false,
            false),
        new TheoryDataRow(
            true,
            1,
            false,
            true),
        new TheoryDataRow(
            false,
            1,
            false,
            false),
        new TheoryDataRow(
            null,
            2,
            false,
            false),
        new TheoryDataRow(
            true,
            2,
            false,
            true),
        new TheoryDataRow(
            false,
            2,
            false,
            false)
    ];

    /// <summary>
    ///     The <see cref="ICommandSync" /> under test.
    /// </summary>
    private readonly ICommandSync commandSync = new ServiceCollection().TryAddCommandSync()
        .BuildServiceProvider()
        .GetRequiredService<ICommandSync>();

    /// <summary>
    ///     Tests of <see cref="ICommandSync.Enter(bool)" />
    /// </summary>
    /// <param name="force">The method under test is called using the force parameter.</param>
    /// <param name="activeCount">Specifies the number of active commands at the start of the test.</param>
    /// <param name="isActiveChanged">Specifies if <see cref="ICommandSync.IsActive" /> should change during test execution.</param>
    /// <param name="expectedEnterResult">The expected result of the method under test.</param>
    [Theory]
    [MemberData(nameof(CommandSyncTests.EnterTestData))]
    public void Enter_Force(
        bool? force,
        int activeCount,
        bool isActiveChanged,
        bool expectedEnterResult
    )
    {
        this.RunEnter(
            () => this.commandSync.Enter(true),
            force is not null ? () => this.commandSync.Enter(force.Value) : () => this.commandSync.Enter(),
            activeCount,
            isActiveChanged,
            expectedEnterResult);
    }

    /// <summary>
    ///     Tests of <see cref="ICommandSync.Enter(int,bool)" />
    /// </summary>
    /// <param name="force">The method under test is called using the force parameter.</param>
    /// <param name="activeCount">Specifies the number of active commands at the start of the test.</param>
    /// <param name="isActiveChanged">Specifies if <see cref="ICommandSync.IsActive" /> should change during test execution.</param>
    /// <param name="expectedEnterResult">The expected result of the method under test.</param>
    [Theory]
    [MemberData(nameof(CommandSyncTests.EnterTestData))]
    public void Enter_MillisecondsTimeout_Force(
        bool? force,
        int activeCount,
        bool isActiveChanged,
        bool expectedEnterResult
    )
    {
        const int millisecondsTimeout = 1000;
        this.RunEnter(
            () => this.commandSync.Enter(
                millisecondsTimeout,
                true),
            force is not null
                ? () => this.commandSync.Enter(
                    millisecondsTimeout,
                    force.Value)
                : () => this.commandSync.Enter(millisecondsTimeout),
            activeCount,
            isActiveChanged,
            expectedEnterResult);
    }

    /// <summary>
    ///     Tests <see cref="ICommandSync.Enter(int,bool)" /> for a timeout.
    /// </summary>
    [Fact]
    public void Enter_MillisecondsTimeout_Force_ShouldTimeout_IfCommandIsActive()
    {
        Assert.True(this.commandSync.Enter());
        Assert.True(this.commandSync.IsActive);

        Assert.False(this.commandSync.Enter(250));
    }

    /// <summary>
    ///     Tests of <see cref="ICommandSync.Enter(TimeSpan,bool)" />
    /// </summary>
    /// <param name="force">The method under test is called using the force parameter.</param>
    /// <param name="activeCount">Specifies the number of active commands at the start of the test.</param>
    /// <param name="isActiveChanged">Specifies if <see cref="ICommandSync.IsActive" /> should change during test execution.</param>
    /// <param name="expectedEnterResult">The expected result of the method under test.</param>
    [Theory]
    [MemberData(nameof(CommandSyncTests.EnterTestData))]
    public void Enter_Timeout_Force(
        bool? force,
        int activeCount,
        bool isActiveChanged,
        bool expectedEnterResult
    )
    {
        var timeout = new TimeSpan(
            0,
            1,
            0);
        this.RunEnter(
            () => this.commandSync.Enter(
                timeout,
                true),
            force is not null
                ? () => this.commandSync.Enter(
                    timeout,
                    force.Value)
                : () => this.commandSync.Enter(timeout),
            activeCount,
            isActiveChanged,
            expectedEnterResult);
    }

    /// <summary>
    ///     Tests <see cref="ICommandSync.Enter(TimeSpan,bool)" /> for a timeout.
    /// </summary>
    [Fact]
    public void Enter_Timeout_Force_ShouldTimeout_IfCommandIsActive()
    {
        Assert.True(this.commandSync.Enter());
        Assert.True(this.commandSync.IsActive);

        Assert.False(
            this.commandSync.Enter(
                new TimeSpan(
                    0,
                    0,
                    250)));
    }

    /// <summary>
    ///     Tests of <see cref="ICommandSync.Exit" />.
    /// </summary>
    /// <param name="activeCommands">The number of active commands before the test execution starts.</param>
    /// <param name="expectedIsActive">The expected value of <see cref="ICommandSync.IsActive" />.</param>
    [Theory]
    [InlineData(
        0,
        false)]
    [InlineData(
        1,
        false)]
    [InlineData(
        2,
        true)]
    public void Exit(int activeCommands, bool expectedIsActive)
    {
        for (var i = 0; i < activeCommands; i++)
        {
            this.commandSync.Enter(true);
        }

        this.commandSync.Exit();

        Assert.Equal(
            expectedIsActive,
            this.commandSync.IsActive);
    }

    /// <summary>
    ///     Tests the value of <see cref="ICommandSync.IsActive" /> before any command runs.
    /// </summary>
    [Fact]
    public void IsActive_ShouldBeFalse_AfterInitialize()
    {
        Assert.False(this.commandSync.IsActive);
    }

    /// <summary>
    ///     Tests the value of <see cref="ICommandSync.IsActive" /> while a command runs.
    /// </summary>
    [Fact]
    public void IsActive_ShouldBeTrue_IfCommandIsActive()
    {
        Assert.True(this.commandSync.Enter());

        Assert.True(this.commandSync.IsActive);
    }

    /// <summary>
    ///     Tests a variant of the enter method.
    /// </summary>
    /// <param name="forceEnterFunc">
    ///     A <see cref="Func{TResult}" /> to start <paramref name="activeCount" /> commands before
    ///     test execution starts.
    /// </param>
    /// <param name="enterFunc">The enter method under test.</param>
    /// <param name="activeCount">The number of active commands before the test starts.</param>
    /// <param name="isActiveChanged">Specifies if <see cref="ICommandSync.IsActive" /> should change during test execution.</param>
    /// <param name="expectedEnterResult">The expected result of the method under test.</param>
    private void RunEnter(
        Func<bool> forceEnterFunc,
        Func<bool> enterFunc,
        int activeCount,
        bool isActiveChanged,
        bool expectedEnterResult
    )
    {
        for (var i = 0; i < activeCount; i++)
        {
            Assert.True(forceEnterFunc());
        }

        var propertyChangedCalls = 0;
        this.commandSync.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(ICommandSync.IsActive))
            {
                propertyChangedCalls++;
            }
        };

        var actual = enterFunc();

        Assert.Equal(
            expectedEnterResult,
            actual);
        Assert.Equal(
            isActiveChanged,
            propertyChangedCalls == 1);
    }
}
