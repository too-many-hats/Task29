namespace BinUtils;

public static class Decode
{
    public static long SignedNumber(ulong word)
    {
        if ((word & Constants.SignBitMask) == 0) // is positive
        {
            return (long)(word & Constants.MagnitudeBitMask);
        }

        var magnitude = (~(word & Constants.MagnitudeBitMask) & Constants.MagnitudeBitMask);

        // sign extend and make negative
        return ((long)magnitude) * -1;
    }

    //NOTE there is no decoder for unsigned words, because the value doesn't change. There is also no decoder for instructions, use a disassembler.
}