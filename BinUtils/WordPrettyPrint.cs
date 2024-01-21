namespace BinUtils;

public static class WordPrettyPrint
{
    public static string Octal(ulong word)
    {
        // casting to long here will never sign extend because the 1103 is 36bit so the high bits of word are always zero.
        var rawString = Convert.ToString((long)(word & Constants.WordMask), 8);
        var result = new List<char>();

        // put a space between each group of three octal digits.
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

        // put a space between each group of 3 binary digits for readability.
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

        // left pad with 0 digits until we have a full 36bit word.
        // "48" is the length of 3binary digits + a space between each group of three digits.
        while (result.Count < 48 - 1)
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
        return signedValue.ToString("N0"); // N0 format adds thousands separators in the current culture.
    }

    public static string InstructionDecimal(ulong word)
    {
        var opcode = (long)(word >> 30);
        var u = (word >> 15) & Constants.AddressMask;
        var v = word & Constants.AddressMask;
        return $"{OpcodeToMnemonic((uint)opcode).Substring(0, 2)}.u({Decimal(u)}) v({Decimal(v)})";
    }

    public static string InstructionOctal(ulong word)
    {
        var opcode = (long)(word >> 30);
        var u = (word >> 15) & Constants.AddressMask;
        var v = word & Constants.AddressMask;
        return $"{OpcodeToMnemonic((uint)opcode).Substring(0, 2)}.u({Octal(u)}) v({Octal(v)})";
    }

    public static string OpcodeToMnemonic(uint opcode)
    {
        var instruction = Instructions.All.FirstOrDefault(x => x.Opcode == opcode)?.Mnemonic ?? "????";

        return instruction;
    }
}