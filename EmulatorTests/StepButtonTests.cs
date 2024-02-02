using Emulator.Devices.Computer;
using Emulator;
using Xunit;
using FluentAssertions;

namespace EmulatorTests;

public class StepButtonTests
{
    [Fact]
    public void StepNotEffectiveWhenInNormalMode()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.StepPressed();

        // IsOperating remains false because the CPU is not in test mode.

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }

    [Fact]
    public void StepButtonNotEffectiveUnlessStartPressedFirstInTestModeWithLimitedRate()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock);

        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        referenceCpu.SetExecuteMode(ExecuteMode.Clock);

        // IsOperating remains false because start must be pressed first when entering test mode. See reference manual paragraph 3-11.

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }

    [Fact]
    public void CanStepOneCycleAfterStartPressed()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock);

        cpuUnderTest.StartPressed();
        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        referenceCpu.SetExecuteMode(ExecuteMode.Clock);
        referenceCpu.RunningTimeCycles = 1;
        referenceCpu.SetPAKto(1);
        referenceCpu.SccInitRead = true;
        referenceCpu.PdcWaitInternal = true;

        // IsOperating remains false because start must be pressed once when entering test mode.

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }

    [Fact]
    public void CanContinueToStepAfterStartPressedOnce()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock);

        cpuUnderTest.StartPressed();
        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        cpuUnderTest.StepPressed(); // step a second time
        cpuUnderTest.Cycle(1);

        referenceCpu.SetExecuteMode(ExecuteMode.Clock);
        referenceCpu.RunningTimeCycles = 2;
        referenceCpu.SetPAKto(1);
        referenceCpu.PdcWaitInternal = true;
        referenceCpu.McsPulseDistributor[0] = 1;
        referenceCpu.McsWaitInit[0] = true;
        referenceCpu.McsReadWriteEnable[0] = true;
        
        // start only needs to be pressed once after entering test mode, then step can be pressed as many times as desired. See example sequence in paragraph 3-55 of reference manual.

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }

    [Fact]
    public void PressingStepCannotStartExecutionAfterExitingManualStep()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock);

        cpuUnderTest.StartPressed();
        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        referenceCpu.SetExecuteMode(ExecuteMode.Clock);
        referenceCpu.RunningTimeCycles = 1;
        referenceCpu.SetPAKto(1);
        referenceCpu.SccInitRead = true;
        referenceCpu.PdcWaitInternal = true;

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);

        cpuUnderTest.SetExecuteMode(ExecuteMode.HighSpeed); // returns to NormalCondition
        referenceCpu.SetExecuteMode(ExecuteMode.HighSpeed);

        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        cpuUnderTest.IsNormalCondition.Should().BeTrue();

        cpuUnderTest.Should().BeEquivalentTo(referenceCpu);
    }

    [Fact]
    public void EnteringTestMode_PressingStartStep_LeavingTestMode_RequiresStartPressAgain()
    {
        // when entering test mode, start must be pressed before STEP. Otherwise STEP has no effect.
        //When going back to NORMAL mode, then back to test mode again, you cannot STEP until START has been pressed again.

        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        TestUtils.PowerOnAndLoad(installation, "ps,0,0");

        var cpuUnderTest = installation.Cpu;
        var referenceCpu = new Cpu(TestUtils.GetDefaultConfig());
        referenceCpu.PowerOnPressed();
        referenceCpu.Memory[0] = cpuUnderTest.Memory[0];
        cpuUnderTest.Console.SetPAKTo(0);
        referenceCpu.Console.SetPAKTo(0);

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock);

        cpuUnderTest.StartPressed();
        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);

        cpuUnderTest.SetExecuteMode(ExecuteMode.HighSpeed); //puts us back to NORMAL mode.

        cpuUnderTest.IsNormalCondition.Should().BeTrue();

        cpuUnderTest.Cycle(1); // should not advance because start was not pressed when we entered into NORMAL mode.

        cpuUnderTest.SetExecuteMode(ExecuteMode.Clock); // back to TEST mode

        cpuUnderTest.IsTestCondition.Should().BeTrue();

        cpuUnderTest.StepPressed();
        cpuUnderTest.Cycle(1);
        
        cpuUnderTest.RunningTimeCycles.Should().Be(1); // did not advance because START was not pressed since entering TEST mode the second time.
    }
}