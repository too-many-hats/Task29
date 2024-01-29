using Emulator;
using Xunit;

namespace EmulatorTests;

public class InitTests
{
    [Fact]
    public void PowersOnWithCorrectValues()
    {
        var installation = new Installation();
        TestUtils.PowerOnAndLoad(installation, @"
start.tp,1,1 ;comment
tm,start,number
number.sval,101b
");

        Assert.False(installation.Cpu.IsOperating);
    }
}