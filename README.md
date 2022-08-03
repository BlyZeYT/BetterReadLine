# BetterReadLine
An easy to use and highly customizable alternative for Console.ReadLine();
## How to use
Initialize a new ConsoleInputReader instance
```
var inputReader = new ConsoleInputReader(true, new InputReaderColor(255, 100, 69), ConsoleKey.Enter, true, 1, 20);
```
## Then use it like Console.ReadLine();
```
string text = inputReader.Read("abc123"); //This enables the user to input 'a', 'b', 'c', '1', '2', '3' but nothing else
```
## The user should input a password? Here:
```
string text = inputReader.ReadAsPassword("abc123", '*'); //Does exactly the same as the method above but replaces every char with the '*'
```
## You can also use Regex for input restriction
```
string text = inputReader.ReadWithRegex("[a-z]", System.Text.RegularExpressions.RegexOptions.IgnoreCase); //This enables the user to input every letter of the alphabet and ignores the case
```
## Need to use Regex in combination with password? Here:
```
string text = inputReader.ReadWithRegexAsPassword("[a-z]", '?'); Does the same as the method above but it only accepts lowercase letters and replaces every char with the '?'
```
## You can get information about the ConsoleInputReader instance
```
bool showInput = inputReader.BackspaceEnabled;
InputReaderColor inputColor = inputReader.InputColor;
ConsoleKey enterKey = inputReader.EnterKey;
bool backspaceEnabled = inputReader.BackspaceEnabled;
int minInputLength = inputReader.MinInputLength;
int maxInputLength = inputReader.MaxInputLength;
```
