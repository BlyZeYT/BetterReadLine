namespace BetterReadLine;

using System;

/// <summary>
/// A better Console.ReadLine(); for passwords
/// </summary>
public sealed class ConsolePasswordInputRestrictor : ConsoleInputRestrictorBase
{
    /// <summary>
    /// The character that should be written instead of the actual character
    /// </summary>
    public char Mask { get; }

    /// <summary></summary>
    public ConsolePasswordInputRestrictor(ConsoleColor? foreground, ConsoleColor? background, ConsoleKey returnKey, bool backspaceEnabled, int minInputLength, int maxInputLength, char mask = '*')
        : base(foreground, background, returnKey, backspaceEnabled, minInputLength, maxInputLength)
    {
        Mask = mask;
    }

    /// <summary></summary>
    public ConsolePasswordInputRestrictor(ConsoleInputRestrictorBaseConfig config, char mask = '*')
        : base(config)
    {
        Mask = mask;
    }

    /// <summary></summary>
    public ConsolePasswordInputRestrictor(Action<ConsoleInputRestrictorBaseConfig> config, char mask = '*')
        : base(config)
    {
        Mask = mask;
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction that replaces every input char/>
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
                Console.Write(Mask);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return sb.ToString();
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction that replaces every input char/>
    /// </summary>
    /// <param name="validInputs">A <see cref="IEnumerable{T}"/> that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
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
                Console.Write(Mask);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return sb.ToString();
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction that replaces every input char/>
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
                Console.Write(Mask);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return (T)Convert.ChangeType(sb.ToString(), typeof(T));
    }

    /// <summary>
    /// Console.ReadLine(); with input restriction that replaces every input char/>
    /// </summary>
    /// <param name="validInputs">A <see cref="IEnumerable{T}"/> that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
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
                Console.Write(Mask);
            }
            else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
            {
                sb.Length--;
                Console.Write("\b \b");
            }

        } while (!(key.Key == ReturnKey && sb.Length >= MinInputLength));

        return (T)Convert.ChangeType(sb.ToString(), typeof(T));
    }
}