using Emulator;
using FluentAssertions;
using Xunit;

namespace EmulatorTests;

public class NormalModeTests
{
    [Fact]
    public void CannotSetA_Q_X_PCR_WhileOperatingInNormalMode()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,1,2");
        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.PowerOnPressed();
        cpuUnderTest.StartPressed();

        cpuUnderTest.IsOperating.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();

        cpuUnderTest.Cycle(1);

        cpuUnderTest.SetAto(1);
        cpuUnderTest.SetQto(1);
        cpuUnderTest.SetXto(1);
        cpuUnderTest.SetMCRto(1);
        cpuUnderTest.SetVAKto(1);
        cpuUnderTest.SetUAKto(1);

        cpuUnderTest.VAK.Should().Be(0);
        cpuUnderTest.UAK.Should().Be(0);
        cpuUnderTest.MCR.Should().Be(0);
        cpuUnderTest.X.Should().Be(0);
        cpuUnderTest.Q.Should().Be(0);
        cpuUnderTest.A.Should().Be(0);
    }
}