namespace BinUtils;

public static class Encode
{
    public static ulong SignedNumber(long value)
    {
        var unsignedValue = (ulong)value;

        if (value >= 0)
        {
            // MagnitudeBitMask ensures the 36th bit is zero for a positive number.
            return unsignedValue & Constants.MagnitudeBitMask;
        }

        var unsigned = (~unsignedValue + 1) & Constants.MagnitudeBitMask;

        return ((~unsigned & Constants.WordMask) | Constants.SignBitMask);
    }

    public static ulong UnsignedNumber(ulong value)
    {
        return value & Constants.WordMask;
    }

    public static ulong Instruction(uint opcode, uint u, uint v)
    {
        ulong result = opcode & 0b111_111;
        result = result << 15;
        result = result | u;
        result = result << 15;
        result = result | v;

        return result;
    }
}