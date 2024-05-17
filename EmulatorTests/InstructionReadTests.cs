using Emulator.Devices.Computer;
using Emulator;
using Xunit;

namespace EmulatorTests;

public class InstructionReadTests
{
    // MP6 and MP7 are the same for all instructions unless a REPEAT is used so they are tested here, and not on each instruction.
    // This also tests

    [Fact]
    public void CanReadInstruction()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,1,2");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[1] = cpuUnderTest.Memory[0];
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Memory[1] = cpuUnderTest.Memory[0];

        referenceCpu.SetPAKto(2);
        referenceCpu.IsOperating = true;
        referenceCpu.SetSARto(1);
        referenceCpu.SccInitRead = true; // on the first cycle the core memory read for the next instruction is started.
        referenceCpu.PdcWaitInternal = true; // on the first cycle the core memory read for the next instruction is started.
        referenceCpu.RunningTimeCycles = 1;
        referenceCpu.McsAddressRegister[0] = 1;
        cpuUnderTest.SetPAKto(1); // tests that the AdvancePAK command was executed.
        cpuUnderTest.SetMCRto(5); // tests that the ClearPCR command was executed.
        cpuUnderTest.SetVAKto(5); // tests that the ClearPCR command was executed.
        cpuUnderTest.SetUAKto(5); // tests that the ClearPCR command was executed.
        cpuUnderTest.SetXto(5); // tests that the ClearX command was executed.

        cpuUnderTest.StartPressed();
        cpuUnderTest.Cycle(1);

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        // the next four cycles are core memory reads, so skip over them
        cpuUnderTest.Cycle(4);

        referenceCpu.SccInitRead = false;
        referenceCpu.PdcWaitInternal = false;
        referenceCpu.RunningTimeCycles = 5;
        referenceCpu.McsAddressRegister[0] = 1;
        referenceCpu.McsWaitInit[0] = true;
        referenceCpu.X = referenceCpu.Memory[1];// x should have been set to the value in memory
        referenceCpu.MainPulseDistributor = 7; //next pulse should be MP7

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        // MP7
        referenceCpu.McsWaitInit[0] = false;
        referenceCpu.MainPulseDistributor = 7; // stay on 7 until the 1 clock delay has passed.
        referenceCpu.RunningTimeCycles = 6;
        referenceCpu.MCR = 47; // tests that X to PCR updated MCR
        referenceCpu.VAK = 2; // tests that X to PCR updated VAK
        referenceCpu.UAK = 1; // tests that X to PCR updated UAK
        referenceCpu.SetSARto(0); // tests that ClearSAR command executed.

        cpuUnderTest.Cycle(1);

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        // after MP7 there is a 1 clock delay, test that the delay is consumed without any other CPU execution.

        referenceCpu.RunningTimeCycles = 7; // this matches our on paper simulation of the machine.
        referenceCpu.MainPulseDistributor = 0;

        cpuUnderTest.Cycle(1);

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }

    [Fact]
    public void SccFaultWhenReadingInstructionFromAccumulator()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,1,2");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];

        referenceCpu.IsOperating = true;
        referenceCpu.PAK = 13313; // accumulator address + 1
        referenceCpu.SetSARto(0); // according to the reference manual paragraph  3-43 there is no indication of the location of the faulty instruction, so SAR must be cleared.
        referenceCpu.SccInitRead = false; // the SCC fault ends the read command
        referenceCpu.PdcWaitInternal = false; // the SCC fault ends the read command.
        referenceCpu.SccFault = true;
        referenceCpu.SctA = false; // according to the reference manual paragraph  3-43 there is no indication of the location of the faulty instruction, so Storage Class Translator must be cleared of the original A reference.
        referenceCpu.MainPulseDistributor = 7; // according to the reference manual paragraph  3-43 SCC fault when reading an instruction leaves MP7 light on.
        referenceCpu.RunningTimeCycles = 1;
        cpuUnderTest.PAK = 13312; // tests that the AdvancePAK command was executed.
        cpuUnderTest.MCR = 5; // tests that the ClearPCR command was executed.
        cpuUnderTest.VAK = 5; // tests that the ClearPCR command was executed.
        cpuUnderTest.UAK = 5; // tests that the ClearPCR command was executed.
        cpuUnderTest.X = 5; // tests that the ClearX command was executed.
        cpuUnderTest.StartPressed();

        cpuUnderTest.Cycle(1);

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }
}