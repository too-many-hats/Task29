using Xunit;

namespace BinUtilsTests;

public class WordPrettyPrintTests
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(8, "10")]
    [InlineData(5417034, "24 524 112")] // separates every group of three octal digits
    [InlineData(137438953465, "777 777 777 771")] // prints negative numbers (-1 in ones complement), truncated to 36bits.
    public void CanPrintOctal(ulong value, string expected)
    {
        var result = WordPrettyPrint.Octal(value);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, "000 000 000 000 000 000 000 000 000 000 000 000")]
    [InlineData(1, "000 000 000 000 000 000 000 000 000 000 000 001")]
    [InlineData(8, "000 000 000 000 000 000 000 000 000 000 001 000")]
    [InlineData(137438953465, "111 111 111 111 111 111 111 111 111 111 111 001")] // prints negative numbers (-1 in ones complement), truncated to 36bits.
    public void CanPrintBinary(ulong value, string expected)
    {
        var result = WordPrettyPrint.Binary(value);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(12345, "12,345")]
    [InlineData(68719464390, "-12,345")]// -12,345 in ones complement
    public void CanPrintDecimal(ulong value, string expected)
    {
        var result = WordPrettyPrint.Decimal(value);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, "0.u(0) v(0)")]
    [InlineData(1073774593, "1.u(1) v(1)")]
    [InlineData(68719476735, "77.u(32,767) v(32,767)")]
    [InlineData(67645767679, "77.u(0) v(32,767)")]
    public void CanPrintDecimalInstruction(ulong value, string expected)
    {
        var result = WordPrettyPrint.InstructionDecimal(value);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, "0.u(0) v(0)")]
    [InlineData(1073774593, "1.u(1) v(1)")]
    [InlineData(68719476735, "77.u(77 777) v(77 777)")]
    [InlineData(67645767679, "77.u(0) v(77 777)")]
    public void CanPrintOctalInstruction(ulong value, string expected)
    {
        var result = WordPrettyPrint.InstructionOctal(value);
        Assert.Equal(expected, result);
    }
}