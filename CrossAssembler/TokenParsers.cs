namespace CrossAssembler;

public static class TokenParsers
{
    public static bool TryParseNumber(string number, int lineNumber, List<Error> errorList, out long value)
    {
        var numberNoWhitespace = new string(number.Where(x => char.IsWhiteSpace(x) == false).ToArray());
        var fromBase = 10;
        var numberWithoutPostfix = number;
        value = 0;

        if (number.Length == 0)
        {
            return true;
        }

        if (number.EndsWith('B')) //binary input string
        {
            numberWithoutPostfix = numberNoWhitespace.Substring(0, numberNoWhitespace.Length - 1);
            fromBase = 2;
        }
        else if (number.EndsWith('O')) // octal input string
        {
            numberWithoutPostfix = numberNoWhitespace.Substring(0, numberNoWhitespace.Length - 1);
            fromBase = 8;
        }
        else
        {
            fromBase = 10;
        }

        try
        {
            value = Convert.ToInt64(numberWithoutPostfix, fromBase);
            return true;
        }
        catch (Exception)
        {
            errorList.Add(new Error(lineNumber, $"Could not parse {number} as a valid number in the specified base"));
            return false;
        }
    }
}