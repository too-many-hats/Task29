using Emulator;
using Emulator.Devices.Computer;
using FluentAssertions;
using Xunit;

namespace EmulatorTests;

public class InitTests
{
    [Fact]
    public void PowersOnWithCorrectValues()
    {
        var installation = new Installation()
            .Init(TestUtils.GetDefaultConfig());

        var cpuUnderTest = installation.Cpu;

        cpuUnderTest.Console.PowerOnPressed();

        // all flip-flop statuses as per reference manual pages 3-3 and 3-4.
        // We don't have any specific information on the machine state when first powered on. I'm assuming it's the same state as a master reset.

        cpuUnderTest.IsOperating.Should().Be(false); // at reset the clock is stopped, so we aren't operating.
        cpuUnderTest.IsNormalCondition.Should().Be(true);
        cpuUnderTest.IsTestCondition.Should().Be(false);
        cpuUnderTest.IsAbnormalCondition.Should().Be(false);

        Assert.True(cpuUnderTest.MasterClockCSSI);
        Assert.True(cpuUnderTest.MasterClockCSSII);
        Assert.True(cpuUnderTest.MasterClockCRCI);
        Assert.True(cpuUnderTest.MasterClockCRCI);

        cpuUnderTest.PdcHpc.Should().Be(false);
        cpuUnderTest.PdcTwc.Should().Be(false);
        cpuUnderTest.PdcWaitInternal.Should().Be(false);
        cpuUnderTest.PdcWaitExternal.Should().Be(false);
        cpuUnderTest.PdcWaitRsc.Should().Be(false);
        Assert.True(cpuUnderTest.PdcStop);

        cpuUnderTest.StopTape.Should().Be(false);
        cpuUnderTest.SccFault.Should().Be(false);
        cpuUnderTest.MctFault.Should().Be(false);
        cpuUnderTest.DivFault.Should().Be(false);
        cpuUnderTest.AZero.Should().Be(false);
        cpuUnderTest.TapeFeed.Should().Be(false);

        cpuUnderTest.Rsc75.Should().Be(false);
        cpuUnderTest.RscHoldRpt.Should().Be(false);
        cpuUnderTest.RscJumpTerm.Should().Be(false);
        cpuUnderTest.RscInitRpt.Should().Be(false);
        cpuUnderTest.RscInitTest.Should().Be(false);
        cpuUnderTest.RscEndRpt.Should().Be(false);
        cpuUnderTest.RscDelayTest.Should().Be(false);
        cpuUnderTest.RscAdvAdd.Should().Be(false);

        cpuUnderTest.SccInitRead.Should().Be(false);
        cpuUnderTest.SccInitWrite.Should().Be(false);
        cpuUnderTest.SccInitIw0_14.Should().Be(false);
        cpuUnderTest.SccInitIw15_29.Should().Be(false);
        cpuUnderTest.SccReadQ.Should().Be(false);
        cpuUnderTest.SccWriteAorQ.Should().Be(false);
        cpuUnderTest.SccClearA.Should().Be(false);

        cpuUnderTest.SctA.Should().Be(false);
        cpuUnderTest.SctQ.Should().Be(false);
        cpuUnderTest.SctMD.Should().Be(false);
        cpuUnderTest.SctMcs0.Should().BeTrue("set to true in the image on page 3-3. I believe this is because SAR is reset to 0 and 0 is a core memory address in MCS bank 0");
        cpuUnderTest.SctMcs1.Should().Be(false);

        cpuUnderTest.AscDelAdd.Should().Be(false);
        cpuUnderTest.AscSpSubt.Should().Be(false);
        cpuUnderTest.OverflowFault.Should().Be(false);
        cpuUnderTest.AscProbeAL.Should().Be(false);
        cpuUnderTest.AscProbeAR.Should().Be(false);
        cpuUnderTest.AscProbeB.Should().Be(false);
        cpuUnderTest.AscProbeC.Should().Be(false);
        cpuUnderTest.AscProbeD.Should().Be(false);
        cpuUnderTest.AscProbeE.Should().Be(false);

        cpuUnderTest.InitArithSequenceA_1.Should().Be(false);
        cpuUnderTest.InitArithSequenceSP.Should().Be(false);
        cpuUnderTest.InitArithSequenceAL.Should().Be(false);
        cpuUnderTest.InitArithSequenceQL.Should().Be(false);
        cpuUnderTest.InitArithSequenceDiv.Should().Be(false);
        cpuUnderTest.InitArithSequenceMult.Should().Be(false);
        cpuUnderTest.InitArithSequenceSeq.Should().Be(false);
        cpuUnderTest.InitArithSequenceStep.Should().Be(false);
        cpuUnderTest.InitArithSequenceCase.Should().Be(false);
        cpuUnderTest.InitArithSequenceCkI.Should().Be(false);
        cpuUnderTest.InitArithSequenceCkII.Should().Be(false);
        cpuUnderTest.InitArithSequenceRestX.Should().Be(false);
        cpuUnderTest.InitArithSequenceMultiStep.Should().Be(false);
        cpuUnderTest.InitArithSequenceExtSeq.Should().Be(false);

        Assert.Equal(6, cpuUnderTest.MainPulseDistributor); // as per paragraph 3-10, reference manual
        Assert.Equal((uint)16384, cpuUnderTest.PAK); // as per paragraph 3-10, reference manual

        Assert.Equal((UInt128)0, cpuUnderTest.A);
        Assert.Equal((ulong)0, cpuUnderTest.Q);
        Assert.Equal((ulong)0, cpuUnderTest.X);
        Assert.Equal((ulong)0, cpuUnderTest.MCR);
        Assert.Equal((ulong)0, cpuUnderTest.UAK);
        Assert.Equal((ulong)0, cpuUnderTest.VAK);
        Assert.Equal((ulong)0, cpuUnderTest.SAR);

        cpuUnderTest.Halt.Should().Be(false);
        cpuUnderTest.Interrupt.Should().Be(false);

        Assert.Equal(ExecuteMode.HighSpeed, cpuUnderTest.ExecuteMode);

        Assert.False(cpuUnderTest.SelectiveJumps[0]);
        Assert.False(cpuUnderTest.SelectiveJumps[1]);
        Assert.False(cpuUnderTest.SelectiveJumps[2]);

        cpuUnderTest.IsFinalStopped.Should().Be(false);
        Assert.False(cpuUnderTest.SelectiveStops[0]);
        Assert.False(cpuUnderTest.SelectiveStops[1]);
        Assert.False(cpuUnderTest.SelectiveStops[2]);
        Assert.False(cpuUnderTest.SelectiveStops[3]);

        cpuUnderTest.IsManualInterruptArmed.Should().Be(false);

        cpuUnderTest.MatrixDriveFault.Should().Be(false);
        cpuUnderTest.MctFault.Should().Be(false);
        cpuUnderTest.TapeFault.Should().Be(false);
        cpuUnderTest.IOFault.Should().Be(false);
        cpuUnderTest.VoltageFault.Should().Be(false);
        cpuUnderTest.DivFault.Should().Be(false);
        cpuUnderTest.SccFault.Should().Be(false);
        cpuUnderTest.OverflowFault.Should().Be(false);
        cpuUnderTest.PrintFault.Should().Be(false);
        cpuUnderTest.TempFault.Should().Be(false);
        cpuUnderTest.WaterFault.Should().Be(false);
        cpuUnderTest.FpCharOverflow.Should().Be(false);

        cpuUnderTest.TypeAFault.Should().Be(false);
        cpuUnderTest.TypeBFault.Should().Be(false);

        cpuUnderTest.RunningTimeCycles.Should().Be(0);
        cpuUnderTest.Delay.Should().Be(0);

        // right side panel
        cpuUnderTest.IOB.Should().Be(0);
        cpuUnderTest.MtcTapeRegister.Should().Be(0);
        cpuUnderTest.MtcTapeControlRegister.Should().Be(0);
        cpuUnderTest.MtcBlockCounter.Should().Be(0);
        cpuUnderTest.MtcSprocketDelay.Should().Be(0);
        cpuUnderTest.MtcControlStop.Should().Be(0);
        cpuUnderTest.MtcLeaderDelay.Should().Be(0);
        cpuUnderTest.MtcInitialDelay.Should().Be(0);
        cpuUnderTest.MtcStartControl.Should().Be(0);
        cpuUnderTest.MtcBTK.Should().Be(0);
        cpuUnderTest.MtcWK.Should().Be(0);
        cpuUnderTest.MtcLK.Should().Be(0);
        cpuUnderTest.MtcTSK.Should().Be(0);
        cpuUnderTest.MtcWriteResume.Should().Be(0);
        cpuUnderTest.MtcMtWriteControl.Should().Be(0);
        cpuUnderTest.MtcReadWriteSync.Should().Be(false);
        cpuUnderTest.MtcTskControl.Should().Be(false);
        cpuUnderTest.MtcBlShift.Should().Be(false);
        cpuUnderTest.MtcBlEnd.Should().Be(false);
        cpuUnderTest.MtcNotReady.Should().Be(false);
        cpuUnderTest.MtcCkParityError.Should().Be(0);
        cpuUnderTest.MtcControlSyncSprocketTest.Should().Be(0);
        cpuUnderTest.MtcAlignInputRegister.Should().Be(0);
        cpuUnderTest.MtcBlockEnd.Should().Be(0);
        cpuUnderTest.MtcRecordEnd.Should().Be(0);
        cpuUnderTest.MtcFaultControl.Should().Be(false);
        cpuUnderTest.MtcBccError.Should().Be(false);
        cpuUnderTest.MtcBccControl.Should().Be(0);
        cpuUnderTest.MtcTrControl.Should().Be(0);
        cpuUnderTest.MtcTrControlTcrSync.Should().Be(false);
        cpuUnderTest.MtcBsk.Should().Be(0);
        cpuUnderTest.MtcReadControl.Should().Be(false);
        cpuUnderTest.MtcWrite.Should().Be(0);
        cpuUnderTest.MtcSubt.Should().Be(0);
        cpuUnderTest.MtcAdd.Should().Be(0);
        cpuUnderTest.MtcDelay.Should().Be(0);
        cpuUnderTest.MtcCenterDriveControl.Should().Be(0);
        
        cpuUnderTest.ExtFaultIoA1.Should().Be(false);
        cpuUnderTest.ExtFaultIoB1.Should().Be(false);
        cpuUnderTest.WaitIoARead.Should().Be(false);
        cpuUnderTest.WaitIoBRead.Should().Be(false);
        cpuUnderTest.WaitIoAWrite.Should().Be(false);
        cpuUnderTest.WaitIoBWrite.Should().Be(false);
        cpuUnderTest.IoaWrite.Should().Be(false);
        cpuUnderTest.IoBWrite.Should().Be(false);
        cpuUnderTest.Select.Should().Be(false);
        cpuUnderTest.IoA.Should().Be(0);

        cpuUnderTest.FpSRegister.Should().Be(0);
        cpuUnderTest.FpDRegister.Should().Be(0);
        cpuUnderTest.FpCRegister.Should().Be(0);
        cpuUnderTest.FpNormExit.Should().Be(false);
        cpuUnderTest.FpMooMrp.Should().Be(false);
        cpuUnderTest.FpUV.Should().Be(false);
        cpuUnderTest.FpAddSubt.Should().Be(false);
        cpuUnderTest.FpMulti.Should().Be(false);
        cpuUnderTest.FpDiv.Should().Be(false);
        cpuUnderTest.FpSign.Should().Be(false);
        cpuUnderTest.FpDelayShiftA.Should().Be(false);
        cpuUnderTest.FpSequenceGates.Should().Be(0);

        //left side panel
        cpuUnderTest.McsAddressRegister[0].Should().Be(0);
        cpuUnderTest.McsAddressRegister[1].Should().Be(0);
        cpuUnderTest.McsPulseDistributor[0].Should().Be(0);
        cpuUnderTest.McsPulseDistributor[1].Should().Be(0);
        cpuUnderTest.McsMonInit[0].Should().Be(false);
        cpuUnderTest.McsMonInit[1].Should().Be(false);
        cpuUnderTest.McsRead[0].Should().Be(false);
        cpuUnderTest.McsRead[1].Should().Be(false);
        cpuUnderTest.McsWrite[0].Should().Be(false);
        cpuUnderTest.McsWrite[1].Should().Be(false);
        cpuUnderTest.McsWaitInit[0].Should().Be(false);
        cpuUnderTest.McsWaitInit[1].Should().Be(false);
        cpuUnderTest.McsReadWriteEnable[0].Should().Be(false);
        cpuUnderTest.McsReadWriteEnable[1].Should().Be(false);
        cpuUnderTest.McsWr0_14[0].Should().Be(false);
        cpuUnderTest.McsWr0_14[1].Should().Be(false);
        cpuUnderTest.McsWr15_29[0].Should().Be(false);
        cpuUnderTest.McsWr15_29[1].Should().Be(false);
        cpuUnderTest.McsWr30_35[0].Should().Be(false);
        cpuUnderTest.McsWr30_35[1].Should().Be(false);
        cpuUnderTest.HsPunchRegister.Should().Be(0);
        cpuUnderTest.TypewriterRegister.Should().Be(0);
        cpuUnderTest.HsPunchInit.Should().Be(false);
        cpuUnderTest.HsPunchRes.Should().Be(false);

        cpuUnderTest.Drum.Group.Should().Be(4);
        cpuUnderTest.Drum.Gs.Should().Be(0);
        cpuUnderTest.Drum.AngularIndexCounter.Should().Be(0);
        cpuUnderTest.Drum.Interlace.Should().Be((uint)TestUtils.GetDefaultConfig().DrumInterlace);
        cpuUnderTest.Drum.InitWrite.Should().Be(false);
        cpuUnderTest.Drum.InitWrite0_14.Should().Be(false);
        cpuUnderTest.Drum.InitWrite15_29.Should().Be(false);
        cpuUnderTest.Drum.InitRead.Should().Be(false);
        cpuUnderTest.Drum.InitDelayedRead.Should().Be(false);
        cpuUnderTest.Drum.ReadLockoutIII.Should().Be(false);
        cpuUnderTest.Drum.ReadLockoutII.Should().Be(false);
        cpuUnderTest.Drum.ReadLockoutI.Should().Be(false);
        cpuUnderTest.Drum.ConincLockout.Should().Be(false);
        cpuUnderTest.Drum.Preset.Should().Be(false);
        cpuUnderTest.Drum.AdvanceAik.Should().Be(false);
        cpuUnderTest.Drum.CpdI.Should().Be(false);
        cpuUnderTest.Drum.CpdII.Should().Be(false);
    }

    private void AssertCpusEqual(Cpu sut, Cpu knownCorrect)
    {
        Assert.Equal(sut.IsOperating, knownCorrect.IsOperating);
    }
}