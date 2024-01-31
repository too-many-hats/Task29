using Emulator;
using Xunit;

namespace EmulatorTests;

public class ProgramStopTests
{
    [Fact]
    public void ProgramStopStopsMachine()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");


    }
}