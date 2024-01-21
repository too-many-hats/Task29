using BinUtils;

namespace BinUtilsTests;

public static class WordPrettyPrint
{
    public static string Octal(ulong word)
    {
        // casting to long here will never sign extend because the 1103 is 36bit so the high bits of word are always zero.
        var rawString = Convert.ToString((long)(word & Constants.WordMask), 8);
        var result = new List<char>();

        var spaceCounter = 0;
        for (int i = rawString.Length - 1; i > -1; i--)
        {
            if ((spaceCounter % 3) == 0 && i != rawString.Length - 1)
            {
                result.Add(' ');
            }

            spaceCounter++;

            result.Add(rawString[i]);
        }

        result.Reverse();

        return new string(result.ToArray());
    }

    public static string Binary(ulong word)
    {
        // casting to long here will never sign extend because the 1103 is 36bit so the high bits of word are always zero.
        var rawString = Convert.ToString((long)(word & Constants.WordMask), 2);
        var result = new List<char>();

        var spaceCounter = 0;
        for (int i = rawString.Length - 1; i > -1; i--)
        {
            if ((spaceCounter % 3) == 0 && i != rawString.Length - 1)
            {
                result.Add(' ');
            }

            spaceCounter++;

            result.Add(rawString[i]);
        }

        while (result.Count < 47)
        {
            if ((spaceCounter % 3) == 0)
            {
                result.Add(' ');
            }

            spaceCounter++;

            result.Add('0');
        }

        result.Reverse();

        return new string(result.ToArray());
    }

    public static string Decimal(ulong word)
    {
        var signedValue = Decode.SignedNumber(word);
        return signedValue.ToString("N0");
    }

    public static string InstructionDecimal(ulong word)
    {
        var opcode = (long)(word >> 30);
        var u = (word >> 15) & Constants.AddressMask;
        var v = word & Constants.AddressMask;
        return $"{Convert.ToString(opcode, 8)}.u({Decimal(u)}) v({Decimal(v)})";
    }

    public static string InstructionOctal(ulong word)
    {
        var opcode = (long)(word >> 30);
        var u = (word >> 15) & Constants.AddressMask;
        var v = word & Constants.AddressMask;
        return $"{Convert.ToString(opcode, 8)}.u({Octal(u)}) v({Octal(v)})";
    }
}