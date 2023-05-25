namespace BetterReadLine;

/// <summary>
/// Base class for all console input restrictors
/// </summary>
public abstract class ConsoleInputRestrictorBase
{
    /// <summary>
    /// Foreground color of the input, <see langword="null"/> if it should use the standard console color
    /// </summary>
    public ConsoleColor? Foreground { get; }

    /// <summary>
    /// Background color of the input, <see langword="null"/> if it should use the standard console color
    /// </summary>
    public ConsoleColor? Background { get; }

    /// <summary>
    /// The key that functions as Return
    /// </summary>
    public ConsoleKey ReturnKey { get; }

    /// <summary>
    /// <see langword="true"/> if the Backspace Key is enabled, otherwise <see langword="false"/>
    /// </summary>
    public bool BackspaceEnabled { get; }

    /// <summary>
    /// Minimum input length, Minimum Value = 0
    /// </summary>
    public int MinInputLength { get; }

    /// <summary>
    /// Maximum input length, Minimum Value = 1
    /// </summary>
    public int MaxInputLength { get; }

    /// <summary></summary>
    protected ConsoleInputRestrictorBase(ConsoleColor? foreground, ConsoleColor? background, ConsoleKey returnKey, bool backspaceEnabled, int minInputLength, int maxInputLength)
    {
        Foreground = foreground;
        Background = background;
        ReturnKey = returnKey;
        BackspaceEnabled = backspaceEnabled;

        if (maxInputLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxInputLength), $"The {nameof(maxInputLength)} can't be smaller than 1");
        if (minInputLength < 0)
            throw new ArgumentOutOfRangeException(nameof(minInputLength), $"The {nameof(minInputLength)} can't be smaller than 0");

        if (minInputLength > maxInputLength)
            throw new ArgumentException($"The {nameof(minInputLength)} can't be bigger than {nameof(maxInputLength)}", nameof(minInputLength));

        MinInputLength = minInputLength;
        MaxInputLength = maxInputLength;
    }

    /// <summary></summary>
    protected ConsoleInputRestrictorBase(ConsoleInputRestrictorBaseConfig config)
        : this(config.Foreground, config.Background, config.ReturnKey, config.BackspaceEnabled, config.MinInputLength, config.MaxInputLength) { }

    /// <summary></summary>
    protected ConsoleInputRestrictorBase(Action<ConsoleInputRestrictorBaseConfig> config)
    {
        ConsoleInputRestrictorBaseConfig configured = new();
        config.Invoke(configured);

        Foreground = configured.Foreground;
        Background = configured.Background;
        ReturnKey = configured.ReturnKey;
        BackspaceEnabled = configured.BackspaceEnabled;

        if (configured.MaxInputLength < 1)
            throw new ArgumentOutOfRangeException(nameof(configured.MaxInputLength), $"The {nameof(configured.MaxInputLength)} can't be smaller than 1");
        if (configured.MinInputLength < 0)
            throw new ArgumentOutOfRangeException(nameof(configured.MinInputLength), $"The {nameof(configured.MinInputLength)} can't be smaller than 0");

        if (configured.MinInputLength > configured.MaxInputLength)
            throw new ArgumentException($"The {nameof(configured.MinInputLength)} can't be bigger than {nameof(configured.MaxInputLength)}", nameof(config));

        MinInputLength = configured.MinInputLength;
        MaxInputLength = configured.MaxInputLength;
    }

    /// <summary>
    /// Writes colored to the console
    /// </summary>
    /// <param name="character">The character to write</param>
    protected void WriteColored(char character)
    {
        var fg = Console.ForegroundColor;
        var bg = Console.BackgroundColor;

        Console.ForegroundColor = Foreground ?? fg;
        Console.BackgroundColor = Background ?? bg;

        Console.Write(character);

        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
    }
}