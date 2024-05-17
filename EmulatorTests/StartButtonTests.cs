using Emulator.Devices.Computer;
using Emulator;
using Xunit;

namespace EmulatorTests;

public class StartButtonTests
{
    [Theory]
    [InlineData(ExecuteMode.Clock)]
    [InlineData(ExecuteMode.Distributor)]
    [InlineData(ExecuteMode.Operation)]
    [InlineData(ExecuteMode.AutomaticStepOperation)]
    [InlineData(ExecuteMode.AutomaticStepClock)]
    public void StartDoesNotBeginExecutionWhenNotHighSpeed(ExecuteMode executeMode)
    {
        // see reference manual paragraph 3-11.
        var installation = new Installation()
           .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.PAK = 0;
        referenceCpu.PAK = 0;

        cpuUnderTest.SetExecuteMode(executeMode);
        referenceCpu.SetExecuteMode(executeMode);

        cpuUnderTest.StartPressed(); // despite manual clock rate, step is not pressed
        cpuUnderTest.Cycle(1); // no progress should be made, because step is required to be pressed when an operating rate selection is made.

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }

    [Fact]
    public void StartBeginsExecution()
    {
        // start button executes at the machine's full speed.
        // test that the Program Stop instruction is read, interpreted and executed all without interruption after pressing START.
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        cpuUnderTest.StartPressed();
        cpuUnderTest.PAK = 0;

        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        referenceCpu.PAK = 1;

        cpuUnderTest.Cycle(100);

        referenceCpu.RunningTimeCycles = 8;
        referenceCpu.IsProgramStopped = true;
        referenceCpu.MCR = 47;
        referenceCpu.MainPulseDistributor = 0;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }

    [Fact]
    public void PressingStartWhileAlreadyOperatingHasNoEffect()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        cpuUnderTest.StartPressed();
        cpuUnderTest.PAK = 0;

        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        referenceCpu.PAK = 1;
        referenceCpu.StartPressed();

        cpuUnderTest.Cycle(1);

        referenceCpu.RunningTimeCycles = 1;
        referenceCpu.SccInitRead = true;
        referenceCpu.PdcWaitInternal = true;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);

        cpuUnderTest.StartPressed(); // press start a second time.
        cpuUnderTest.Cycle(1); // execution should continue as if nothing happened.

        referenceCpu.RunningTimeCycles = 2;
        referenceCpu.PAK = 1;
        referenceCpu.PdcWaitInternal = true;
        referenceCpu.McsPulseDistributor[0] = 1;
        referenceCpu.McsWaitInit[0] = true;
        referenceCpu.McsReadWriteEnable[0] = true;
        referenceCpu.SccInitRead = false;

        TestUtils.AssertEquivalent(cpuUnderTest, referenceCpu);
    }
}