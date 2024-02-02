using Emulator;
using Emulator.Devices.Computer;
using FluentAssertions;
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

        var cpuUnderTest = installation.Cpu;
        cpuUnderTest.StartPressed();

        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];

        referenceCpu.IsOperating = true;
        referenceCpu.SetPAKto(1);
        referenceCpu.RunningTimeCycles = 7;
        referenceCpu.MainPulseDistributor = 0; // next cycle will be to execute instruction.
        referenceCpu.SetMCRto(47);
        referenceCpu.SetSARto(0);
        referenceCpu.SetXto(referenceCpu.Memory[0]);

        cpuUnderTest.SetPAKto(0);

        cpuUnderTest.Cycle(7); // the 7 cycles required to read an instruction from memory and load PCR

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);

        referenceCpu.IsProgramStopped = true;
        referenceCpu.IsOperating = false;
        referenceCpu.SetXto(0); // test that clear X was executed.
        referenceCpu.RunningTimeCycles = 8;

        cpuUnderTest.Cycle(1);

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);

        // The CPU should not change state, since we are stopped, even though the environment continues to cycle.
        cpuUnderTest.Cycle(10);
        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }
}