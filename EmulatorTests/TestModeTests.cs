using Emulator;
using FluentAssertions;
using Xunit;

namespace EmulatorTests;

public class TestModeTests
{
    [Fact]
    public void TestNormalSwitchUpEnablesTestMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.TestNormalSwitch = true;

        cpuUnderTest.IsTestCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("maint manual page 50 states that *any* switch in the up position triggers ABNORMAL CONDITION");
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DrumAbnormalNormalSwitchUpEnablesTestMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.AbnormalNormalDrumSwitch = true;

        cpuUnderTest.IsTestCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("maint manual page 50 states that *any* switch in the up position triggers ABNORMAL CONDITION");
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ReturningDrumAbnormalNormalSwitchDownEnablesNormalMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.AbnormalNormalDrumSwitch = true;

        cpuUnderTest.IsTestCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();

        cpuUnderTest.AbnormalNormalDrumSwitch = false;

        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ReturningTestNormalSwitchDownEnablesNormalMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.TestNormalSwitch = true;

        cpuUnderTest.IsTestCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();

        cpuUnderTest.TestNormalSwitch = false;

        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void AbnormalConditionRemainsOnWhenInTestMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.CL_TCRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();

        cpuUnderTest.TestNormalSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("ABNORMAL CONDITION lights up if any test switch is up, no matter if the processor is in NORMAL or TEST modes (maint manual page 49)");
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsTestCondition.Should().BeTrue();
    }
}