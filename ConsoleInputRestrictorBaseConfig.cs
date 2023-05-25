namespace BetterReadLine;

/// <summary>
/// Config for <see cref="ConsoleInputRestrictorBase"/>
/// </summary>
public sealed record ConsoleInputRestrictorBaseConfig
{
    /// <summary>
    /// Foreground color of the input, <see langword="null"/> if it should use the standard console color
    /// </summary>
    public ConsoleColor? Foreground { get; set; }

    /// <summary>
    /// Background color of the input, <see langword="null"/> if it should use the standard console color
    /// </summary>
    public ConsoleColor? Background { get; set; }

    /// <summary>
    /// The key that functions as Return
    /// </summary>
    public ConsoleKey ReturnKey { get; set; }

    /// <summary>
    /// <see langword="true"/> if the Backspace Key is enabled, otherwise <see langword="false"/>
    /// </summary>
    public bool BackspaceEnabled { get; set; }

    /// <summary>
    /// Minimum input length, Minimum Value = 0
    /// </summary>
    public int MinInputLength { get; set; }

    /// <summary>
    /// Maximum input length, Minimum Value = 1
    /// </summary>
    public int MaxInputLength { get; set; }

    /// <summary></summary>
    public ConsoleInputRestrictorBaseConfig(ConsoleColor? foreground, ConsoleColor? background, ConsoleKey returnKey, bool backspaceEnabled, int minInputLength, int maxInputLength)
    {
        Foreground = foreground;
        Background = background;
        ReturnKey = returnKey;
        BackspaceEnabled = backspaceEnabled;
        MinInputLength = minInputLength;
        MaxInputLength = maxInputLength;
    }

    internal ConsoleInputRestrictorBaseConfig() { }
}