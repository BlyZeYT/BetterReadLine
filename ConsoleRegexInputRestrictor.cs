namespace BetterReadLine;

using System;
using System.Text.RegularExpressions;

/// <summary>
/// A better Console.ReadLine(); using regex expressions
/// </summary>
public sealed class ConsoleRegexInputRestrictor : ConsoleInputRestrictorBase
{
    private readonly Regex _regex;

    /// <summary>
    /// <see langword="true"/> if the user input is shown in Console, otherwise <see langword="false"/>
    /// </summary>
    public bool InputVisible { get; }

    /// <summary>
    /// The regular expression to match the input
    /// </summary>
    public string RegEx { get; }

    /// <summary></summary>
    public ConsoleRegexInputRestrictor(bool inputVisible, ConsoleColor? foreground, ConsoleColor? background, ConsoleKey returnKey, bool backspaceEnabled, int minInputLength, int maxInputLength, string regEx, RegexOptions regExOptions, TimeSpan regExTimeout)
        : base(foreground, background, returnKey, backspaceEnabled, minInputLength, maxInputLength)
    {
        InputVisible = inputVisible;
        RegEx = regEx;
        _regex = new Regex(RegEx, RegexOptions.Compiled | regExOptions, regExTimeout);
    }

    /// <summary></summary>
    public ConsoleRegexInputRestrictor(bool inputVisible, string regEx, RegexOptions regExOptions, TimeSpan regExTimeout, ConsoleInputRestrictorBaseConfig config)
        : base(config)
    {
        InputVisible = inputVisible;
        RegEx = regEx;
        _regex = new Regex(RegEx, RegexOptions.Compiled | regExOptions, regExTimeout);
    }

    /// <summary></summary>
    public ConsoleRegexInputRestrictor(bool inputVisible, string regEx, RegexOptions regExOptions, TimeSpan regExTimeout, Action<ConsoleInputRestrictorBaseConfig> config)
        : base(config)
    {
        InputVisible = inputVisible;
        RegEx = regEx;
        _regex = new Regex(RegEx, RegexOptions.Compiled | regExOptions, regExTimeout);
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public string Read()
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (_regex.IsMatch(key.KeyChar.ToString().AsSpan()) && sb.Length < MaxInputLength)
            {
                sb.Append(key.KeyChar);
                if (InputVisible) WriteColored(key.KeyChar);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                if (InputVisible) Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return sb.ToString();
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction
    /// </summary>
    public T Read<T>()
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (_regex.IsMatch(key.KeyChar.ToString().AsSpan()) && sb.Length < MaxInputLength)
            {
                sb.Append(key.KeyChar);
                if (InputVisible) WriteColored(key.KeyChar);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                if (InputVisible) Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return (T)Convert.ChangeType(sb.ToString(), typeof(T));
    }
}