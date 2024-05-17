using Emulator;
using Emulator.Devices.Computer;
using Xunit;

namespace EmulatorTests;

public class TestCoreMemoryRead
{
    // test reading from core memory
    [Theory]
    [InlineData(0, 0, 0)] // can read from first address in first bank.
    [InlineData(0, 4095, 4095)] // can read from last address in first bank.
    [InlineData(1, 4096, 4096)] // can read from first address in second bank.
    [InlineData(0, 8192, 0)] // trying to read into the non-existent third bank of memory wraps around to the first bank.
    [InlineData(0, 8193, 1)] // trying to read into the non-existent third bank of memory wraps around to the first bank.
    public void CanReadWordToX(int coreBank, uint startAddress, uint physicalStartAddress)
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        cpuUnderTest.StartPressed();
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.IsOperating = true;
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];

        cpuUnderTest.Memory[physicalStartAddress] = cpuUnderTest.Memory[0];
        referenceCpu.Memory[physicalStartAddress] = referenceCpu.Memory[0];

        // attempt to read word at address 0 of memory
        cpuUnderTest.PAK = startAddress;
        referenceCpu.PAK = startAddress;
        cpuUnderTest.X = 5; // random value to test that the ClearX command occurs.

        cpuUnderTest.Cycle(1); // should start the internal reference sub command.

        referenceCpu.PAK = startAddress + 1;
        referenceCpu.SctMcs0 = coreBank == 0;
        referenceCpu.SetSARto(startAddress);
        referenceCpu.SctMcs1 = coreBank == 1;
        referenceCpu.SccInitRead = true;
        referenceCpu.RunningTimeCycles = 1;
        referenceCpu.PdcWaitInternal = true;
        referenceCpu.McsAddressRegister[coreBank] = startAddress & 0b111_111_111_111;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        cpuUnderTest.Cycle(1); //MCP-0

        referenceCpu.SccInitRead = false;
        referenceCpu.RunningTimeCycles = 2;
        referenceCpu.PdcWaitInternal = true;
        referenceCpu.McsPulseDistributor[coreBank] = 1;
        referenceCpu.McsWaitInit[coreBank] = true;
        referenceCpu.McsReadWriteEnable[coreBank] = true;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        cpuUnderTest.Cycle(1); //MCP-1

        referenceCpu.RunningTimeCycles = 3;
        referenceCpu.PdcWaitInternal = true;
        referenceCpu.McsPulseDistributor[coreBank] = 2;
        referenceCpu.McsWaitInit[coreBank] = true;
        referenceCpu.McsReadWriteEnable[coreBank] = true;
        referenceCpu.McsMonInit[coreBank] = true;
        referenceCpu.X = referenceCpu.Memory[physicalStartAddress];
        referenceCpu.McsRead[coreBank] = true;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        cpuUnderTest.Cycle(1); //MCP-2/3

        referenceCpu.McsPulseDistributor[coreBank] = 3;
        referenceCpu.McsEnableInhibitDisturb[coreBank] = true;
        referenceCpu.McsRead[coreBank] = false;
        
        // the emulator merges MCP-2 and MCP-3 so test that here as well
        referenceCpu.McsPulseDistributor[coreBank] = 4;
        referenceCpu.McsWrite[coreBank] = true;
        referenceCpu.RunningTimeCycles = 4;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        cpuUnderTest.Cycle(1); //MCP-4

        referenceCpu.McsPulseDistributor[coreBank] = 0;
        referenceCpu.RunningTimeCycles = 5;
        referenceCpu.McsWrite[coreBank] = false;
        referenceCpu.McsMonInit[coreBank] = false;
        referenceCpu.McsEnableInhibitDisturb[coreBank] = false;
        referenceCpu.McsReadWriteEnable[coreBank] = false;
        referenceCpu.McsWaitInit[coreBank] = true; // wait init remains high for another cycle. A new core cycle can't start while it is high. This creates our 1 cycle delay required as part of page 70 in the timing manual.
        referenceCpu.PdcWaitInternal = false; // data is ready in X and the read core subcommand has signalled we are ready to continue on the next cycle.
        referenceCpu.MainPulseDistributor = 7;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        // check that McsWaitInit remained high for one extra cycle, even though the rest of the CPU continued into MP7.
        cpuUnderTest.Cycle(1);

        referenceCpu.RunningTimeCycles = 6;
        referenceCpu.McsWaitInit[coreBank] = false;
        referenceCpu.SetSARto(0);
        referenceCpu.MCR = 47;
        referenceCpu.MainPulseDistributor = 7; // MP7 lasts for two cycles so we haven't advanced yet.

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }

    //TODO: Test the one cycle lockout actually introduces a delay. Requires an instruction that writes to core.
}