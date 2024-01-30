using Emulator;
using FluentAssertions;
using Xunit;

namespace EmulatorTests;

public class AbnormalConditionTests
{
    [Fact]
    public void AllTestSwitchesInNormalPositionIsNormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();
    }

    [Fact]
    public void ReturningSwitchToNormalEndsAbnormalCondition()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.CL_TCRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();

        cpuUnderTest.CL_TCRDisconnectSwitch = false;

        cpuUnderTest.IsAbnormalCondition.Should().BeFalse();
    }

    [Fact]
    public void MasterClearDoesNotAffectThrowSwitches()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.CL_TCRDisconnectSwitch = true;
        cpuUnderTest.CL_TRDisconnectSwitch = true;
        cpuUnderTest.IOB_TCRDisconnectSwitch = true;
        cpuUnderTest.IOB_TRDisconnectSwitch = true;
        cpuUnderTest.IOB_BKDisconnectSwitch = true;
        cpuUnderTest.TR_IOBDisconnectSwitch = true;
        cpuUnderTest.StartDisconnectSwitch = true;
        cpuUnderTest.ErrorSignalDisconnectSwitch = true;
        cpuUnderTest.ReadDisconnectSwitch = true;
        cpuUnderTest.WriteDisconnectSwitch = true;
        cpuUnderTest.DisconnectMdWriteVoltage4Switch = true;
        cpuUnderTest.DisconnectMdWriteVoltage5Switch = true;
        cpuUnderTest.DisconnectMdWriteVoltage6Switch = true;
        cpuUnderTest.DisconnectMdWriteVoltage7Switch = true;
        cpuUnderTest.DisconnectClearASwitch = true;
        cpuUnderTest.DisconnectClearXSwitch = true;
        cpuUnderTest.DisconnectClearQSwitch = true;
        cpuUnderTest.DisconnectClearSARSwitch = true;
        cpuUnderTest.DisconnectClearPAKSwitch = true;
        cpuUnderTest.DisconnectClearPCRSwitch = true;
        cpuUnderTest.DisconnectInitiateWrite0_35Switch = true;
        cpuUnderTest.DisconnectInitiateWrite0_14Switch = true;
        cpuUnderTest.DisconnectInitiateWrite15_29Switch = true;
        cpuUnderTest.F140001_00000Switch = true;
        cpuUnderTest.SingleMcsSelectionSwitch = true;
        cpuUnderTest.OscDrumSwitch = true;
        cpuUnderTest.DisconnectStopSwitch = true;
        cpuUnderTest.DisconnectSARToPAKSwitch = true;
        cpuUnderTest.DisconnectPAKToSARSwitch = true;
        cpuUnderTest.DisconnectVAKToSARSwitch = true;
        cpuUnderTest.DisconnectQ1ToX1Switch = true;
        cpuUnderTest.DisconnectXToPCRSwitch = true;
        cpuUnderTest.DisconnectAdvPAKSwitch = true;
        cpuUnderTest.DisconnectBackSKSwitch = true;
        cpuUnderTest.DisconnectWaitInitSwitch = true;
        cpuUnderTest.ForceMcZeroSwitch = true;
        cpuUnderTest.ForceMcOneSwitch = true;
        cpuUnderTest.PtAmpMarginalCheckSwitch = true;
        cpuUnderTest.Mcs0AmpMarginalCheckSwitch = true;
        cpuUnderTest.Mcs1AmpMarginalCheckSwitch = true;
        cpuUnderTest.MDAmpMarginalCheckSwitch = true;
        cpuUnderTest.MTAmpMarginalCheckSwitch = true;
        cpuUnderTest.ContReduceHeaterVoltageSwitch = true;
        cpuUnderTest.ArithReduceHeaterVoltageSwitch = true;
        cpuUnderTest.Mcs0ReduceHeaterVoltageSwitch = true;
        cpuUnderTest.Mcs1ReduceHeaterVoltageSwitch = true;
        cpuUnderTest.MTReduceHeaterVoltageSwitch = true;
        cpuUnderTest.MDReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("all Test Switch Group and MT Disconnect Switch Group switches are set to abnormal and TEST/NORMAL switch is NORMAL, maint manual page 50");

        cpuUnderTest.MasterClearPressed();

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue("MASTER CLEAR does not enable test mode or change the physical position of switches (maint manual page 49)");

        //ensure no abnormal switch modified by master clear.
        cpuUnderTest.CL_TCRDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.CL_TRDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.IOB_TCRDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.IOB_TRDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.IOB_BKDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.TR_IOBDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.StartDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.ErrorSignalDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.ReadDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.WriteDisconnectSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectMdWriteVoltage4Switch.Should().BeTrue();
        cpuUnderTest.DisconnectMdWriteVoltage5Switch.Should().BeTrue();
        cpuUnderTest.DisconnectMdWriteVoltage6Switch.Should().BeTrue();
        cpuUnderTest.DisconnectMdWriteVoltage7Switch.Should().BeTrue();
        cpuUnderTest.DisconnectClearASwitch.Should().BeTrue();
        cpuUnderTest.DisconnectClearXSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectClearQSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectClearSARSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectClearPAKSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectClearPCRSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectInitiateWrite0_35Switch.Should().BeTrue();
        cpuUnderTest.DisconnectInitiateWrite0_14Switch.Should().BeTrue();
        cpuUnderTest.DisconnectInitiateWrite15_29Switch.Should().BeTrue();
        cpuUnderTest.F140001_00000Switch.Should().BeTrue();
        cpuUnderTest.SingleMcsSelectionSwitch.Should().BeTrue();
        cpuUnderTest.OscDrumSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectStopSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectSARToPAKSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectPAKToSARSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectVAKToSARSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectQ1ToX1Switch.Should().BeTrue();
        cpuUnderTest.DisconnectXToPCRSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectAdvPAKSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectBackSKSwitch.Should().BeTrue();
        cpuUnderTest.DisconnectWaitInitSwitch.Should().BeTrue();
        cpuUnderTest.ForceMcZeroSwitch.Should().BeTrue();
        cpuUnderTest.ForceMcOneSwitch.Should().BeTrue();
        cpuUnderTest.PtAmpMarginalCheckSwitch.Should().BeTrue();
        cpuUnderTest.Mcs0AmpMarginalCheckSwitch.Should().BeTrue();
        cpuUnderTest.Mcs1AmpMarginalCheckSwitch.Should().BeTrue();
        cpuUnderTest.MDAmpMarginalCheckSwitch.Should().BeTrue();
        cpuUnderTest.MTAmpMarginalCheckSwitch.Should().BeTrue();
        cpuUnderTest.ContReduceHeaterVoltageSwitch.Should().BeTrue();
        cpuUnderTest.ArithReduceHeaterVoltageSwitch.Should().BeTrue();
        cpuUnderTest.Mcs0ReduceHeaterVoltageSwitch.Should().BeTrue();
        cpuUnderTest.Mcs1ReduceHeaterVoltageSwitch.Should().BeTrue();
        cpuUnderTest.MTReduceHeaterVoltageSwitch.Should().BeTrue();
        cpuUnderTest.MDReduceHeaterVoltageSwitch.Should().BeTrue();
    }

    // test that any switch in the Test Switch Group or the MT Disconnect Switch Group creates an abnormal condition when enabled, except TEST/NORMAL and NORMAL/ABNORMAL DRUM.

    [Fact]
    public void CL_TCRDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.CL_TCRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void CL_TRDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.CL_TRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void IOB_TCRDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.IOB_TCRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void IOB_TRDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.IOB_TRDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void IOB_BKDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.IOB_BKDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void TR_IOBDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.TR_IOBDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void StartDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.StartDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ErrorSignalDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ErrorSignalDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ReadDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ErrorSignalDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void WriteDisconnectSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ErrorSignalDisconnectSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectMdWriteVoltage4SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectMdWriteVoltage4Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectMdWriteVoltage5SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectMdWriteVoltage5Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectMdWriteVoltage6SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectMdWriteVoltage6Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectMdWriteVoltage7SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectMdWriteVoltage7Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearASwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearASwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearXSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearXSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearQSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearQSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearSARSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearSARSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearPAKSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearPAKSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectClearPCRSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectClearPCRSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectInitiateWrite0_35SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectInitiateWrite0_35Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectInitiateWrite0_14SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectInitiateWrite0_14Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectInitiateWrite15_29SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectInitiateWrite15_29Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void F140001_00000SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.F140001_00000Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void SingleMcsSelectionSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.SingleMcsSelectionSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void OscDrumSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.OscDrumSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectStopSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectStopSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectSARToPAKSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectSARToPAKSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectPAKToSARSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectPAKToSARSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectVAKToSARSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectVAKToSARSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectQ1ToX1SwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectQ1ToX1Switch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectXToPCRSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectXToPCRSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectAdvPAKSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectAdvPAKSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectBackSKSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectBackSKSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void DisconnectWaitInitSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.DisconnectWaitInitSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ForceMcZeroSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ForceMcZeroSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ForceMcOneSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ForceMcOneSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void PtAmpMarginalCheckSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.PtAmpMarginalCheckSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void Mcs0AmpMarginalCheckSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.Mcs0AmpMarginalCheckSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void Mcs1AmpMarginalCheckSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.Mcs1AmpMarginalCheckSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void MDAmpMarginalCheckSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.MDAmpMarginalCheckSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void MTAmpMarginalCheckSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.MTAmpMarginalCheckSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ContReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ContReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void ArithReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.ArithReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void Mcs0ReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.Mcs0ReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void Mcs1ReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.Mcs1ReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void MTReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.MTReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }

    [Fact]
    public void MDReduceHeaterVoltageSwitchUpIsAbnormal()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        cpuUnderTest.MDReduceHeaterVoltageSwitch = true;

        cpuUnderTest.IsAbnormalCondition.Should().BeTrue();
        cpuUnderTest.IsNormalCondition.Should().BeTrue();
        cpuUnderTest.IsTestCondition.Should().BeFalse();
        cpuUnderTest.IsOperating.Should().BeFalse();
    }
}