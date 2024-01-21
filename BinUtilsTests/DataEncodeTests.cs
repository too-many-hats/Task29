using BinUtils;
using Xunit;

namespace BinUtilsTests;

public class DataEncodeTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(34359738367, 34359738367)]//35bit number is passed through unmodified
    [InlineData(34359738369, 1)]//36bit number is truncated to a 35bit positive number
    [InlineData(-1, 68719476734)]//-1 in ones complement.
    [InlineData(-5, 68719476730)]
    [InlineData(-34359738367, 34359738368)]//largest negative 35bit number is passed through unmodified
    [InlineData(-68719476737, 68719476734)]//largest negative 35bit number is passed through unmodified
    public void CanEncodeNumber(long input, ulong expected)
    {
        var result = Encode.SignedNumber(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(68719476735, 68719476735)]//largest unsigned number is passed through
    [InlineData(137438953471, 68719476735)]//number larger than 36bits is truncated
    public void CanEncodeUnsignedNumber(ulong input, ulong expected)
    {
        var result = Encode.UnsignedNumber(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 1, 1, 1073774593)]
    [InlineData(63, 32767, 32767, 68719476735)]
    [InlineData(63, 0, 32767, 67645767679)]
    public void CanEncodeInstruction(uint opcode, uint u, uint v, ulong expected)
    {
        var result = Encode.Instruction(opcode, u, v);
        Assert.Equal(expected, result);
    }
}