using Emulator;
using Emulator.Devices.Computer;
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

        cpuUnderTest.CL_TCRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();

        cpuUnderTest.TestNormalSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("ABNORMAL CONDITION lights up if any test switch is up, no matter if the processor is in NORMAL or TEST modes (maint manual page 49)");
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsTestCondition.Should().BeTrue();
    }

    [Theory]
    [InlineData(ExecuteMode.Clock)]
    [InlineData(ExecuteMode.Distributor)]
    [InlineData(ExecuteMode.Operation)]
    [InlineData(ExecuteMode.AutomaticStepOperation)]
    [InlineData(ExecuteMode.AutomaticStepClock)]
    public void TestModeEnabledWhenOperatingRateGroupButtonPressed(ExecuteMode executeMode)
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.ExecuteMode = executeMode;

        cpuUnderTest.IsTestCondition.Should().BeTrue("Any selection in Operating Rate Group enables TEST mode (reference manual paragraph 3-8)");
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();
    }

    [Fact]
    public void ReleasingOperatingRateGroupSelectionDisablesTestMode()
    {
        var cpuUnderTest = new Installation()
            .Init(TestUtils.GetDefaultConfig()).Cpu;

        cpuUnderTest.ExecuteMode = ExecuteMode.Clock;

        cpuUnderTest.IsTestCondition.Should().BeTrue("Any selection in Operating Rate Group enables TEST mode (reference manual paragraph 3-8)");
        cpuUnderTest.IsNormalCondition.Should().BeFalse();
        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();

        cpuUnderTest.ExecuteMode = ExecuteMode.HighSpeed;

        cpuUnderTest.IsTestCondition.Should().BeFalse("HighSpeed is required for NORMAL operation and disables TEST mode");
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();
    }
}