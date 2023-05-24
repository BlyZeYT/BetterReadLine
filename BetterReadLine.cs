namespace BetterReadLine
{
    using Console = Colorful.Console;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A better Console.ReadLine(); Docs: <see href="https://github.com/BlyZeYT/BetterReadLine"/>
    /// </summary>
    public class ConsoleInputReader
    {
        /// <summary>
        /// The <see cref="bool"/> if the user input is shown in Console
        /// </summary>
        public bool ShowInput { get; init; }

        /// <summary>
        /// The <see cref="Color"/> that the user input has
        /// </summary>
        public InputReaderColor InputColor { get; init; }

        /// <summary>
        /// The <see cref="ConsoleKey"/> that functions as Enter
        /// </summary>
        public ConsoleKey EnterKey { get; init; }

        /// <summary>
        /// The <see cref="bool"/> if the Backspace Key is enabled
        /// </summary>
        public bool BackspaceEnabled { get; init; }

        /// <summary>
        /// The <see cref="int"/> value that functions as the minimum input length for the input; Minimum Value = 0
        /// </summary>
        public int MinInputLength { get; init; }

        /// <summary>
        /// The <see cref="int"/> value that functions as the maximum input length for the input; Minimum Value = 1
        /// </summary>
        public int MaxInputLength { get; init; }

        /// <summary>
        /// A better Console.ReadLine(); Docs: <see href="https://github.com/BlyZeYT/BetterReadLine"/>
        /// </summary>
        /// <param name="showInput">The <see cref="bool"/> if the user input is shown in Console</param>
        /// <param name="inputColor">The <see cref="Color"/> that the user input has</param>
        /// <param name="enterKey">The <see cref="ConsoleKey"/> that functions as Enter</param>
        /// <param name="backspaceEnabled">The <see cref="bool"/> if the Backspace Key is enabled</param>
        /// <param name="minInputLength">The <see cref="int"/> value that functions as the minimum input length for the input; Minimum Value = 0</param>
        /// <param name="maxInputLength">The <see cref="int"/> value that functions as the maximum input length for the input; Maximum Value = 1</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ConsoleInputReader(bool showInput, Color inputColor, ConsoleKey enterKey, bool backspaceEnabled, int minInputLength, int maxInputLength)
        {
            ShowInput = showInput;
            InputColor = new InputReaderColor(inputColor.ToArgb());
            EnterKey = enterKey;
            BackspaceEnabled = backspaceEnabled;

            if (maxInputLength < 1) throw new ArgumentOutOfRangeException(nameof(maxInputLength), "The " + nameof(maxInputLength) + " can't be smaller than 1");
            if (minInputLength < 0) throw new ArgumentOutOfRangeException(nameof(minInputLength), "The " + nameof(minInputLength) + " can't be smaller than 0");

            if (minInputLength > maxInputLength) throw new ArgumentException("The " + nameof(minInputLength) + " can't be bigger than " + nameof(maxInputLength), nameof(minInputLength));

            MinInputLength = minInputLength;
            MaxInputLength = maxInputLength;
        }

        /// <summary>
        /// A better Console.ReadLine(); Docs: <see href="https://github.com/BlyZeYT/BetterReadLine"/>
        /// </summary>
        /// <param name="showInput">The <see cref="bool"/> if the user input is shown in Console</param>
        /// <param name="inputColor">The <see cref="InputReaderColor"/> that the user input has</param>
        /// <param name="enterKey">The <see cref="ConsoleKey"/> that functions as Enter</param>
        /// <param name="backspaceEnabled">The <see cref="bool"/> if the Backspace Key is enabled</param>
        /// <param name="minInputLength">The <see cref="int"/> value that functions as the minimum input length for the input; Minimum Value = 0</param>
        /// <param name="maxInputLength">The <see cref="int"/> value that functions as the maximum input length for the input; Maximum Value = 1</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ConsoleInputReader(bool showInput, InputReaderColor inputColor, ConsoleKey enterKey, bool backspaceEnabled, int minInputLength, int maxInputLength)
        {
            ShowInput = showInput;
            InputColor = inputColor;
            EnterKey = enterKey;
            BackspaceEnabled = backspaceEnabled;

            if (maxInputLength < 1) throw new ArgumentOutOfRangeException(nameof(maxInputLength), "The " + nameof(maxInputLength) + " can't be smaller than 1");
            if (minInputLength < 0) throw new ArgumentOutOfRangeException(nameof(minInputLength), "The " + nameof(minInputLength) + " can't be smaller than 0");

            if (minInputLength > maxInputLength) throw new ArgumentException("The " + nameof(minInputLength) + " can't be bigger than " + nameof(maxInputLength), nameof(minInputLength));

            MinInputLength = minInputLength;
            MaxInputLength = maxInputLength;
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="validInputs">A <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string Read(string validInputs)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x == key.KeyChar) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);

                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="validInputs">A <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <returns>Returns the input</returns>
        public T Read<T>(string validInputs)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x == key.KeyChar) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);

                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return (T)Convert.ChangeType(sb.ToString(), typeof(T));
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="validInputs">A <see cref="IEnumerable{T}"/> that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string Read(IEnumerable<string> validInputs)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x.Any(x => x == key.KeyChar)) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="validInputs">A <see cref="IEnumerable{T}"/> that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <returns>Returns the input</returns>
        public T Read<T>(IEnumerable<string> validInputs)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x.Any(x => x == key.KeyChar)) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return (T)Convert.ChangeType(sb.ToString(), typeof(T));
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="validInputs">A <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadAsPassword(string validInputs, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x == key.KeyChar) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="validInputs">A <see cref="IEnumerable{T}"/> that contains <see cref="string"/> that contains all valid <see cref="char"/></param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadAsPassword(IEnumerable<string> validInputs, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (validInputs.Any(x => x.Any(x => x == key.KeyChar)) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegex(string regularExpression)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, RegexOptions.None) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexOptions">The <see cref="RegexOptions"/> the <see cref="Regex"/> should use</param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegex(string regularExpression, RegexOptions regexOptions)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, regexOptions) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexOptions">The <see cref="RegexOptions"/> the <see cref="Regex"/> should use</param>
        /// <param name="regexTimeout">The <see cref="TimeSpan"/> after that the <see cref="Regex"/> should timeout</param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegex(string regularExpression, RegexOptions regexOptions, TimeSpan regexTimeout)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, regexOptions, regexTimeout) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexTimeout">The <see cref="TimeSpan"/> after that the <see cref="Regex"/> should timeout</param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegex(string regularExpression, TimeSpan regexTimeout)
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, RegexOptions.None, regexTimeout) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    if (ShowInput) Console.Write(key.KeyChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    if (ShowInput) Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegexAsPassword(string regularExpression, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, RegexOptions.None) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexOptions">The <see cref="RegexOptions"/> the <see cref="Regex"/> should use</param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegexAsPassword(string regularExpression, RegexOptions regexOptions, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, regexOptions) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexOptions">The <see cref="RegexOptions"/> the <see cref="Regex"/> should use</param>
        /// <param name="regexTimeout">The <see cref="TimeSpan"/> after that the <see cref="Regex"/> should timeout</param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegexAsPassword(string regularExpression, RegexOptions regexOptions, TimeSpan regexTimeout, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, regexOptions, regexTimeout) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }

        /// <summary>
        /// Console.ReadLine(); with input restriction that replaces every input char with <paramref name="passwordChar"/>
        /// </summary>
        /// <param name="regularExpression">A <see cref="Regex.pattern"/> as a <see cref="string"/></param>
        /// <param name="regexTimeout">The <see cref="TimeSpan"/> after that the <see cref="Regex"/> should timeout</param>
        /// <param name="passwordChar">The <see cref="char"/> that replaces every input<see cref="char"/></param>
        /// <returns>Returns the input as <see cref="string"/></returns>
        public string ReadWithRegexAsPassword(string regularExpression, TimeSpan regexTimeout, char passwordChar = '*')
        {
            ConsoleKeyInfo key;
            var sb = new StringBuilder();
            do
            {
                key = Console.ReadKey(true);

                if (Regex.IsMatch(key.KeyChar.ToString(), regularExpression, RegexOptions.None, regexTimeout) && sb.Length < MaxInputLength)
                {
                    sb.Append(key.KeyChar);
                    Console.Write(passwordChar, InputColor.BaseColor);
                }
                else if (key.Key is ConsoleKey.Backspace && sb.Length > 0 && BackspaceEnabled)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }

            } while (!(key.Key == EnterKey && sb.Length >= MinInputLength));

            return sb.ToString();
        }
    }

    /// <summary>
    /// An alternative for <see cref="Color"/>; Docs <see href="https://github.com/BlyZeYT/BetterReadLine"/>
    /// </summary>
    public readonly struct InputReaderColor
    {
        private readonly Color _color;

        /// <summary>
        /// <see cref="Color"/> value
        /// </summary>
        public Color BaseColor => _color;

        /// <summary>
        /// Alpha value
        /// </summary>
        public byte A => _color.A;

        /// <summary>
        /// Red Value
        /// </summary>
        public byte R => _color.R;

        /// <summary>
        /// Green Value
        /// </summary>
        public byte G => _color.G;

        /// <summary>
        /// Blue Value
        /// </summary>
        public byte B => _color.B;

        /// <summary>Returns a HEX color <see cref="string"/></summary>
        public string ToHEX() => $"#{_color.R:X2}{_color.G:X2}{_color.B:X2}";

        /// <summary>Returns a color as <see cref="int"/></summary>
        public int ToArgb() => _color.ToArgb();

        /// <summary>
        /// An alternative for <see cref="Color"/>; Docs <see href=""/>
        /// </summary>
        /// <param name="argb">ARGB Color value</param>
        public InputReaderColor(int argb)
        {
            _color = Color.FromArgb(argb);
        }

        /// <summary>
        /// An alternative for <see cref="Color"/>; Docs <see href="https://github.com/BlyZeYT/BetterReadLine"/>
        /// </summary>
        /// <param name="alpha">Alpha value</param>
        /// <param name="red">Red Value</param>
        /// <param name="green">Green Value</param>
        /// <param name="blue">Blue value</param>
        public InputReaderColor(byte alpha, byte red, byte green, byte blue)
        {
            _color = Color.FromArgb(alpha, red, green, blue);
        }

        /// <summary>
        /// An alternative for <see cref="Color"/>; Docs <see href="https://github.com/BlyZeYT/BetterReadLine"/>
        /// </summary>
        /// <param name="red">Red Value</param>
        /// <param name="green">Green Value</param>
        /// <param name="blue">Blue value</param>
        public InputReaderColor(byte red, byte green, byte blue)
        {
            _color = Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// An alternative for <see cref="Color"/>; Docs <see href="https://github.com/BlyZeYT/BetterReadLine"/>
        /// </summary>
        /// <param name="HEXValue">HEX Color value</param>
        /// <exception cref="ArgumentException"></exception>
        public InputReaderColor(string HEXValue)
        {
            try
            {
                _color = ColorTranslator.FromHtml(HEXValue);
            }
            catch (Exception) { throw new FormatException("The HEX Value isn't a color"); }
        }
    }
}