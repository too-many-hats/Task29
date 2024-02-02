using Emulator;
using FluentAssertions;
using Xunit;

namespace EmulatorTests;

public class ClearAFaultTests
{
    [Fact]
    public void ClearAButtonClearsAFaults()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;
        cpuUnderTest.StartPressed();

        // type A faults (should be cleared).
        cpuUnderTest.DivFault = true;
        cpuUnderTest.SccFault = true;
        cpuUnderTest.OverflowFault = true;
        cpuUnderTest.PrintFault = true;
        cpuUnderTest.TempFault = true;
        cpuUnderTest.WaterFault = true;
        cpuUnderTest.FpCharOverflow = true;

        // type B faults (should not be cleared).
        cpuUnderTest.MatrixDriveFault = true;
        cpuUnderTest.MctFault = true;
        cpuUnderTest.TapeFault = true;
        cpuUnderTest.IOFault = true;
        cpuUnderTest.VoltageFault = true;

        cpuUnderTest.TypeAFault.Should().BeTrue();
        cpuUnderTest.TypeBFault.Should().BeTrue();

        cpuUnderTest.ClearAFaultPressed();

        cpuUnderTest.DivFault.Should().BeFalse();
        cpuUnderTest.SccFault.Should().BeFalse();
        cpuUnderTest.OverflowFault.Should().BeFalse();
        cpuUnderTest.PrintFault.Should().BeFalse();
        cpuUnderTest.TempFault.Should().BeFalse();
        cpuUnderTest.WaterFault.Should().BeFalse();
        cpuUnderTest.FpCharOverflow.Should().BeFalse();

        cpuUnderTest.MatrixDriveFault.Should().BeTrue();
        cpuUnderTest.MctFault.Should().BeTrue();
        cpuUnderTest.TapeFault.Should().BeTrue();
        cpuUnderTest.IOFault.Should().BeTrue();
        cpuUnderTest.VoltageFault.Should().BeTrue();

        cpuUnderTest.TypeAFault.Should().BeFalse();
        cpuUnderTest.TypeBFault.Should().BeTrue();
    }
}