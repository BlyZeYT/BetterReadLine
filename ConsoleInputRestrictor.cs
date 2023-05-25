namespace BetterReadLine;

using System;

/// <summary>
/// A better Console.ReadLine();
/// </summary>
public sealed class ConsoleInputRestrictor : ConsoleInputRestrictorBase
{
    /// <summary>
    /// <see langword="true"/> if the user input is shown in Console, otherwise <see langword="false"/>
    /// </summary>
    public bool InputVisible { get; }

    /// <summary></summary>
    public ConsoleInputRestrictor(bool inputVisible, ConsoleColor? foreground, ConsoleColor? background, ConsoleKey returnKey, bool backspaceEnabled, int minInputLength, int maxInputLength)
        : base(foreground, background, returnKey, backspaceEnabled, minInputLength, maxInputLength)
    {
        InputVisible = inputVisible;
    }

    /// <summary></summary>
    public ConsoleInputRestrictor(bool inputVisible, ConsoleInputRestrictorBaseConfig config)
        : base(config)
    {
        InputVisible = inputVisible;
    }

    /// <summary></summary>
    public ConsoleInputRestrictor(bool inputVisible, Action<ConsoleInputRestrictorBaseConfig> config)
        : base(config)
    {
        InputVisible = inputVisible;
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction
    /// </summary>
    /// <param name="validInputs">A <see cref="string"/> that contains all valid <see cref="char"/></param>
    /// <returns><see cref="string"/></returns>
    public string Read(string validInputs)
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (validInputs.Any(x => x == key.KeyChar) && sb.Length < MaxInputLength)
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
    /// <param name="validInputs">A <see cref="string"/> that contains all valid <see cref="char"/></param>
    public T Read<T>(string validInputs)
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (validInputs.Any(x => x == key.KeyChar) && sb.Length < MaxInputLength)
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

    /// <summary>
    /// Console.ReadLine(); with input restriction
    /// </summary>
    /// <param name="validInputs">An array that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
    /// <returns><see cref="string"/></returns>
    public string Read(params string[] validInputs)
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (validInputs.Any(x => x.Any(x => x == key.KeyChar)) && sb.Length < MaxInputLength)
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
    /// <param name="validInputs">An array that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
    public T Read<T>(params string[] validInputs)
    {
        ConsoleKeyInfo key;
        var sb = new SpanBuilder();
        do
        {
            key = Console.ReadKey(true);

            if (validInputs.Any(x => x.Any(x => x == key.KeyChar)) && sb.Length < MaxInputLength)
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