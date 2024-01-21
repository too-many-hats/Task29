namespace BinUtils;

public static class Constants
{
    /// <summary>
    /// The 36th bit in a word.
    /// </summary>
    public readonly static ulong SignBitMask = (long)1 << 35;

    /// <summary>
    /// The low 35bits in a word.
    /// </summary>
    public readonly static ulong MagnitudeBitMask = 34359738367;

    /// <summary>
    /// All 36bits in a world.
    /// </summary>
    public readonly static ulong WordMask = SignBitMask | MagnitudeBitMask;

    /// <summary>
    /// Mask for the low 15 bits of a word.
    /// </summary>
    public readonly static ulong AddressMask = 0b111_111_111_111_111;
}