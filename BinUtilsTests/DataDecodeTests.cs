using BinUtils;
using Xunit;

namespace BinUtilsTests;

public class DataDecodeTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(68719476734, -1)]
    public void CanDecodeSignedWord(ulong value, long expected)
    {
        var result = Decode.SignedNumber(value);
        Assert.Equal(expected, result);
    }
}