
using BinUtils;

namespace Emulator.Devices.Computer;

public class Console
{
    public Indicator[] AIndicators { get; } = Enumerable.Range(0, 72).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] QIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] XIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MCRIndicators { get; } = Enumerable.Range(0, 6).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] VAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] UAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] PAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] SARIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MpdIndicators { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] SkTranslatorIndicators { get; } = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] JTranslatorIndicators { get; } = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] MainPulseTranslatorIndicators { get; } = Enumerable.Range(0, 8).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] MainControlTranslatorIndicators { get; } = Enumerable.Range(0, 64).Select(_ => new Indicator(false)).ToArray();
    public Indicator AscDelAdd { get; } = new Indicator(true);
    public Indicator AscSpSubt { get; } = new Indicator(true);
    public Indicator AscOverflow { get; } = new Indicator(true);
    public Indicator AscAL { get; } = new Indicator(true);
    public Indicator AscAR { get; } = new Indicator(true);
    public Indicator AscB { get; } = new Indicator(true);
    public Indicator AscC { get; } = new Indicator(true);
    public Indicator AscD { get; } = new Indicator(true);
    public Indicator AscE { get; } = new Indicator(true);
    public Indicator InitArithSequenceLog { get; } = new Indicator(true);
    public Indicator InitArithSequenceA_1 { get; } = new Indicator(true);
    public Indicator InitArithSequenceSP { get; } = new Indicator(true);
    public Indicator InitArithSequenceA1 { get; } = new Indicator(true);
    public Indicator InitArithSequenceQL { get; } = new Indicator(true);
    public Indicator InitArithSequenceDiv { get; } = new Indicator(true);
    public Indicator InitArithSequenceMult { get; } = new Indicator(true);
    public Indicator InitArithSequenceSEQ { get; } = new Indicator(true);
    public Indicator InitArithSequenceStep { get; } = new Indicator(true);
    public Indicator InitArithSequenceCase { get; } = new Indicator(true);
    public Indicator InitArithSequenceCKI { get; } = new Indicator(true);
    public Indicator InitArithSequenceCKII { get; } = new Indicator(true);
    public Indicator InitArithSequenceRestX { get; } = new Indicator(true);
    public Indicator InitArithSequenceMultiStep { get; } = new Indicator(true);
    public Indicator InitArithSequenceExtSeq { get; } = new Indicator(true);
    public Indicator StopTape { get; } = new Indicator(true);
    public Indicator SccFault { get; } = new Indicator(true);
    public Indicator MctFault { get; } = new Indicator(true);
    public Indicator DivFault { get; } = new Indicator(true);
    public Indicator AZero { get; } = new Indicator(true);
    public Indicator TapeFeed { get; } = new Indicator(true);
    public Indicator Rsc75 { get; } = new Indicator(true);
    public Indicator RscHoldRpt { get; } = new Indicator(true);
    public Indicator RscJumpTerm { get; } = new Indicator(true);
    public Indicator RscInitRpt { get; } = new Indicator(true);
    public Indicator RscInitTest { get; } = new Indicator(true);
    public Indicator RscEndRpt { get; } = new Indicator(true);
    public Indicator RscDelayTest { get; } = new Indicator(true);
    public Indicator RscAdvAdd { get; } = new Indicator(true);
    public Indicator SccInitRead { get; } = new Indicator(true);
    public Indicator SccInitWrite { get; } = new Indicator(true);
    public Indicator SccInitIw0_14 { get; } = new Indicator(true);
    public Indicator SccInitIw15_29 { get; } = new Indicator(true);
    public Indicator SccReadQ { get; } = new Indicator(true);
    public Indicator SccWriteAorQ { get; } = new Indicator(true);
    public Indicator SccClearA { get; } = new Indicator(true);
    public Indicator MasterClockCSSI { get; } = new Indicator(true);
    public Indicator MasterClockCSSII { get; } = new Indicator(true);
    public Indicator MasterClockCRCI { get; } = new Indicator(true);
    public Indicator MasterClockCRCII { get; } = new Indicator(true);
    public Indicator PdcHpc { get; } = new Indicator(true);
    public Indicator PdcTwc { get; } = new Indicator(true);
    public Indicator PdcWaitInternal { get; } = new Indicator(true);
    public Indicator PdcWaitExternal { get; } = new Indicator(true);
    public Indicator PdcWaitRsc { get; } = new Indicator(true);
    public Indicator PdcStop { get; } = new Indicator(true);
    public Indicator SctA { get; } = new Indicator(false);
    public Indicator SctQ { get; } = new Indicator(false);
    public Indicator SctMD { get; } = new Indicator(false);
    public Indicator SctMcs0 { get; } = new Indicator(false);
    public Indicator SctMcs1 { get; } = new Indicator(false);
    public Indicator SctMcs2 { get; } = new Indicator(false);
    public Indicator Halt { get; } = new Indicator(true);
    public Indicator Interrupt { get; } = new Indicator(true);

    public Indicator[] OperatingRateIndicators {  get; } = Enumerable.Range(0, 6).Select(_ => new Indicator(false, LightType.White)).ToArray();
    public Indicator[] SelectiveJumpIndicators {  get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(false, LightType.White)).ToArray();
    public Indicator[] SelectiveStopsIndicators {  get; } = Enumerable.Range(0, 5).Select(_ => new Indicator(false, LightType.Red)).ToArray();
    public Indicator IndicateEnableLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator AbnormalConditionLight {  get; } = new Indicator(false, LightType.White);
    public Indicator NormalLight {  get; } = new Indicator(false, LightType.Green);
    public Indicator TestLight {  get; } = new Indicator(false, LightType.White);
    public Indicator OperatingLight {  get; } = new Indicator(false, LightType.Green);
    public Indicator ForceStopLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator MatrixDriveFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator MtFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator IOFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator VoltageFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator PrintFault {  get; } = new Indicator(false, LightType.Red);
    public Indicator TempFault {  get; } = new Indicator(false, LightType.Red);
    public Indicator WaterFault {  get; } = new Indicator(false, LightType.Red);
    public Indicator CharOverflowLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator MctFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator SccFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator DivFaultLight {  get; } = new Indicator(false, LightType.Red);
    public Indicator OverflowFaultLight {  get; } = new Indicator(false, LightType.Red);

    //LeftConsolePanel
    public Indicator[] IOBIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTapeRegisterIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTapeControlIndicators { get; } = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBlockCounterIndicators { get; } = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcSprocketDelayIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcStopControlIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcLeaderDelayIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcInitialDelayIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcStartControlIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcWKIndicators { get; } = Enumerable.Range(0, 5).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBTKIndicators { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcLKIndicators { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTSKIndicators { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcWriteResumeIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcMtWriteControlIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator MtcReadWriteSync { get; } = new Indicator(true);
    public Indicator MtcTskControl { get; } = new Indicator(true);
    public Indicator MtcBlShift { get; } = new Indicator(true);
    public Indicator MtcBlEnd { get; } = new Indicator(true);
    public Indicator MtcNotReady { get; } = new Indicator(true);
    public Indicator MtcBccError { get; } = new Indicator(true);
    public Indicator MtcFaultControl { get; } = new Indicator(true);
    public Indicator[] MtcCkErrorParityIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcControlSyncSprocketTestIndicators { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcAlignInputRegisterIndicators { get; } = Enumerable.Range(0, 7).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBlockEnd { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcRecordEnd { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBccControl { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTrControl { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator MtcTrControlTcrSync { get; } = new Indicator(true);
    public Indicator[] MtcBsk { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcWrite { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcSubt { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcAdd { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcDelay { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcCenterDriveControl { get; } = Enumerable.Range(0, 10).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] IoA { get; } = Enumerable.Range(0, 8).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] FpSRegister { get; } = Enumerable.Range(0, 10).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] FpDRegister { get; } = Enumerable.Range(0, 8).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] FpCRegister { get; } = Enumerable.Range(0, 8).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] FpSequenceGates { get; } = Enumerable.Range(0, 20).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsMonInit { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsRead { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsWrite { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsReadWriteEnable { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsWaitInit { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsEnId { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsWr0_14 { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsWr30_35 { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] McsWr15_29 { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] HsPunchRegister { get; } = Enumerable.Range(0, 7).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] TypewriterRegister { get; } = Enumerable.Range(0, 7).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] DrumGs { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] DrumAngularIndexCounter { get; } = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] DrumInterlace { get; } = Enumerable.Range(0, 5).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] DrumGroup { get; } = Enumerable.Range(0, 4).Select(_ => new Indicator(true)).ToArray();
    public Indicator[][] McsAddressRegisters { get; private set; } = new Indicator[2][];
    public Indicator[][] McsMainPulseDistributorTranslators { get; private set; } = new Indicator[2][];
    public Indicator[][] McsPulseDistributors { get; private set; } = new Indicator[2][];
    public Indicator MtcReadControl { get; } = new Indicator(true);
    public Indicator ExtFaultIoA1 { get; } = new Indicator(true);
    public Indicator ExtFaultIoB1 { get; } = new Indicator(true);
    public Indicator WaitIoARead { get; } = new Indicator(true);
    public Indicator WaitIoBRead { get; } = new Indicator(true);
    public Indicator WaitIoAWrite { get; } = new Indicator(true);
    public Indicator WaitIoBWrite { get; } = new Indicator(true);
    public Indicator IoaWrite { get; } = new Indicator(true);
    public Indicator IoBWrite { get; } = new Indicator(true);
    public Indicator Select { get; } = new Indicator(true);
    public Indicator FpNormExit { get; } = new Indicator(true);
    public Indicator FpMooMrp { get; } = new Indicator(true);
    public Indicator FpUV { get; } = new Indicator(true);
    public Indicator FpAddSubt { get; } = new Indicator(true);
    public Indicator FpMulti { get; } = new Indicator(true);
    public Indicator FpDiv { get; } = new Indicator(true);
    public Indicator FpSign { get; } = new Indicator(true);
    public Indicator FpDelayShiftA { get; } = new Indicator(true);
    public Indicator HsPunchRes { get; } = new Indicator(true);
    public Indicator HsPunchInit { get; } = new Indicator(true);
    public Indicator DrumInitWrite { get; } = new Indicator(true);
    public Indicator DrumInitWrite0_14 { get; } = new Indicator(true);
    public Indicator DrumInitRead { get; } = new Indicator(true);
    public Indicator DrumInitDelayedRead { get; } = new Indicator(true);
    public Indicator DrumInitWrite15_29 { get; } = new Indicator(true);
    public Indicator DrumReadLockoutI { get; } = new Indicator(true);
    public Indicator DrumReadLockoutII { get; } = new Indicator(true);
    public Indicator DrumReadLockoutIII { get; } = new Indicator(true);
    public Indicator DrumConincLockout { get; } = new Indicator(true);
    public Indicator DrumAdvanceAik { get; } = new Indicator(true);
    public Indicator DrumPreset { get; } = new Indicator(true);
    public Indicator DrumCpdI { get; } = new Indicator(true);
    public Indicator DrumCpdII { get; } = new Indicator(true);
    
    //RightConsolePanel

    private Cpu Cpu { get; set; }

    public Console(Cpu cpu)
    {
        Cpu = cpu;
        
        for (int i = 0; i < McsAddressRegisters.Length; i++)
        {
            McsAddressRegisters[i] = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
            McsMainPulseDistributorTranslators[i] = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
            McsPulseDistributors[i] = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
        }
    }

    internal void EndFrame()
    {
        EndFramesOfAll(AIndicators);
        EndFramesOfAll(QIndicators);
        EndFramesOfAll(XIndicators);
        EndFramesOfAll(MCRIndicators);
        EndFramesOfAll(VAKIndicators);
        EndFramesOfAll(UAKIndicators);
        EndFramesOfAll(PAKIndicators);
        EndFramesOfAll(SARIndicators);
        EndFramesOfAll(MpdIndicators);
        EndFramesOfAll(JTranslatorIndicators);
        EndFramesOfAll(SkTranslatorIndicators);
        EndFramesOfAll(MainPulseTranslatorIndicators);
        EndFramesOfAll(MainControlTranslatorIndicators);
        EndFramesOfAll(OperatingRateIndicators);
        EndFramesOfAll(SelectiveJumpIndicators);
        EndFramesOfAll(SelectiveStopsIndicators);
        EndFramesOfAll(IOBIndicators);
        EndFramesOfAll(MtcTapeRegisterIndicators);
        EndFramesOfAll(MtcTapeControlIndicators);
        EndFramesOfAll(MtcBlockCounterIndicators);
        EndFramesOfAll(MtcSprocketDelayIndicators);
        EndFramesOfAll(MtcStopControlIndicators);
        EndFramesOfAll(MtcLeaderDelayIndicators);
        EndFramesOfAll(MtcInitialDelayIndicators);
        EndFramesOfAll(MtcStartControlIndicators);
        EndFramesOfAll(MtcBTKIndicators);
        EndFramesOfAll(MtcWKIndicators);
        EndFramesOfAll(MtcTSKIndicators);
        EndFramesOfAll(MtcLKIndicators);
        EndFramesOfAll(MtcWriteResumeIndicators);
        EndFramesOfAll(MtcMtWriteControlIndicators);
        EndFramesOfAll(MtcCkErrorParityIndicators);
        EndFramesOfAll(MtcControlSyncSprocketTestIndicators);
        EndFramesOfAll(MtcAlignInputRegisterIndicators);
        EndFramesOfAll(MtcBlockEnd);
        EndFramesOfAll(MtcRecordEnd);
        EndFramesOfAll(MtcBccControl);
        EndFramesOfAll(MtcTrControl);
        EndFramesOfAll(MtcBsk);
        EndFramesOfAll(MtcSubt);
        EndFramesOfAll(MtcWrite);
        EndFramesOfAll(MtcAdd);
        EndFramesOfAll(MtcDelay);
        EndFramesOfAll(MtcCenterDriveControl);
        EndFramesOfAll(IoA);
        EndFramesOfAll(FpSRegister);
        EndFramesOfAll(FpDRegister);
        EndFramesOfAll(FpCRegister);
        EndFramesOfAll(FpSequenceGates);
        EndFramesOfAll(McsAddressRegisters[1]);
        EndFramesOfAll(McsAddressRegisters[0]);
        EndFramesOfAll(McsMonInit);
        EndFramesOfAll(McsRead);
        EndFramesOfAll(McsRead);
        EndFramesOfAll(McsWrite);
        EndFramesOfAll(McsReadWriteEnable);
        EndFramesOfAll(McsWaitInit);
        EndFramesOfAll(McsEnId);
        EndFramesOfAll(McsWr0_14);
        EndFramesOfAll(McsWr30_35);
        EndFramesOfAll(McsWr15_29);
        EndFramesOfAll(McsPulseDistributors[1]);
        EndFramesOfAll(McsPulseDistributors[0]);
        EndFramesOfAll(McsMainPulseDistributorTranslators[1]);
        EndFramesOfAll(McsMainPulseDistributorTranslators[0]);
        EndFramesOfAll(McsPulseDistributors[0]);
        EndFramesOfAll(McsPulseDistributors[0]);
        EndFramesOfAll(TypewriterRegister);
        EndFramesOfAll(HsPunchRegister);
        EndFramesOfAll(DrumGs);
        EndFramesOfAll(DrumAngularIndexCounter);
        EndFramesOfAll(DrumInterlace);
        EndFramesOfAll(DrumGroup);
        MtcReadControl.EndFrame();
        DrumInitWrite.EndFrame();
        DrumInitWrite0_14.EndFrame();
        DrumInitWrite15_29.EndFrame();
        DrumInitRead.EndFrame();
        DrumInitDelayedRead.EndFrame();
        DrumReadLockoutI.EndFrame();
        DrumReadLockoutII.EndFrame();
        DrumReadLockoutIII.EndFrame();
        DrumConincLockout.EndFrame();
        DrumPreset.EndFrame();
        DrumAdvanceAik.EndFrame();
        DrumCpdI.EndFrame();
        DrumCpdII.EndFrame();
        HsPunchRes.EndFrame();
        HsPunchInit.EndFrame();
        FpDelayShiftA.EndFrame();
        FpSign.EndFrame();
        FpDiv.EndFrame();
        FpMulti.EndFrame();
        FpAddSubt.EndFrame();
        FpUV.EndFrame();
        FpMooMrp.EndFrame();
        FpNormExit.EndFrame();
        WaitIoARead.EndFrame();
        WaitIoBRead.EndFrame();
        WaitIoBWrite.EndFrame();
        WaitIoBRead.EndFrame();
        ExtFaultIoA1.EndFrame();
        ExtFaultIoB1.EndFrame();
        MtcNotReady.EndFrame();
        MtcReadWriteSync.EndFrame();
        MtcBlShift.EndFrame();
        MtcTskControl.EndFrame();
        MtcBlEnd.EndFrame();
        MtcFaultControl.EndFrame();
        MtcBccError.EndFrame();
        MtcTrControlTcrSync.EndFrame();
        IndicateEnableLight.EndFrame();
        AbnormalConditionLight.EndFrame();
        NormalLight.EndFrame();
        TestLight.EndFrame();
        OperatingLight.EndFrame();
        ForceStopLight.EndFrame();
        MatrixDriveFaultLight.EndFrame();
        MtFaultLight.EndFrame();
        IOFaultLight.EndFrame();
        VoltageFaultLight.EndFrame();
        PrintFault.EndFrame();
        TempFault.EndFrame();
        WaterFault.EndFrame();
        CharOverflowLight.EndFrame();
        IoaWrite.EndFrame();
        IoBWrite.EndFrame();
        Select.EndFrame();

        Halt.EndFrame();
        Interrupt.EndFrame();

        AscDelAdd.EndFrame();
        AscSpSubt.EndFrame();
        AscOverflow.EndFrame();
        OverflowFaultLight.EndFrame();
        AscAL.EndFrame();
        AscAR.EndFrame();
        AscB.EndFrame();
        AscC.EndFrame();
        AscD.EndFrame();
        AscE.EndFrame();
        InitArithSequenceLog.EndFrame();
        InitArithSequenceA_1.EndFrame();
        InitArithSequenceSP.EndFrame();
        InitArithSequenceA1.EndFrame();
        InitArithSequenceQL.EndFrame();
        InitArithSequenceDiv.EndFrame();
        InitArithSequenceMult.EndFrame();
        InitArithSequenceSEQ.EndFrame();
        InitArithSequenceStep.EndFrame();
        InitArithSequenceCase.EndFrame();
        InitArithSequenceCKI.EndFrame();
        InitArithSequenceCKII.EndFrame();
        InitArithSequenceRestX.EndFrame();
        InitArithSequenceMultiStep.EndFrame();
        InitArithSequenceExtSeq.EndFrame();

        StopTape.EndFrame();
        SccFault.EndFrame();
        SccFaultLight.EndFrame();
        MctFault.EndFrame();
        MctFaultLight.EndFrame();
        DivFault.EndFrame();
        DivFaultLight.EndFrame();
        AZero.EndFrame();
        TapeFeed.EndFrame();
        Rsc75.EndFrame();
        RscHoldRpt.EndFrame();
        RscJumpTerm.EndFrame();
        RscInitRpt.EndFrame();
        RscInitTest.EndFrame();
        RscEndRpt.EndFrame();
        RscDelayTest.EndFrame();
        RscAdvAdd.EndFrame();

        SccInitRead.EndFrame();
        SccInitWrite.EndFrame();
        SccInitIw0_14.EndFrame();
        SccInitIw15_29.EndFrame();
        SccReadQ.EndFrame();
        SccWriteAorQ.EndFrame();
        SccClearA.EndFrame();

        MasterClockCSSI.EndFrame();
        MasterClockCSSII.EndFrame();
        MasterClockCRCI.EndFrame();
        MasterClockCRCII.EndFrame();

        PdcHpc.EndFrame();
        PdcTwc.EndFrame();
        PdcWaitInternal.EndFrame();
        PdcWaitExternal.EndFrame();
        PdcWaitRsc.EndFrame();
        PdcStop.EndFrame();

        SctA.EndFrame();
        SctQ.EndFrame();
        SctMD.EndFrame();
        SctMcs0.EndFrame();
        SctMcs1.EndFrame();
        SctMcs2.EndFrame();
    }

    private static void EndFramesOfAll(Indicator[] indicators)
    {
        foreach (var indicator in indicators)
        {
            indicator.EndFrame();
        }
    }

    private static void UpdateIndicator(Indicator[] indicators, UInt128 value)
    {
        // split the 128bit register into two 64bit halves. Greatly improves performance when everything is in one CPU register.
        ulong lowHalf = (ulong)value & Constants.WordMask;
        ulong highHalf = (ulong)(value >> 36) & Constants.WordMask;
        for (var j = 0; j < 36; j++)
        {
            indicators[j].Update(lowHalf & 1);
            lowHalf = lowHalf >> 1;
        }

        for (var j = 36; j < 36; j++)
        {
            indicators[j].Update(highHalf & 1);
            highHalf = highHalf >> 1;
        }
    }

    private static void UpdateIndicator(Indicator[] indicators, ulong value)
    {
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update(value & 1);
            value = value >> 1;
        }
    }

    private static void UpdateIndicator(Indicator indicator, bool value)
    {
        indicator.Update(value ? (ulong)1 : 0);
    }

    public void PowerOnPressed()
    {
        Cpu.PowerOnPressed();
    }

    internal void UpdateIndicatorStatusEndOfCycle()
    {
        UpdateIndicator(AIndicators, Cpu.A);
        UpdateIndicator(QIndicators, Cpu.Q);
        UpdateIndicator(XIndicators, Cpu.X);
        UpdateIndicator(MCRIndicators, Cpu.MCR);
        UpdateIndicator(UAKIndicators, Cpu.UAK);
        UpdateIndicator(VAKIndicators, Cpu.VAK);
        UpdateIndicator(PAKIndicators, Cpu.PAK);
        UpdateIndicator(SARIndicators, Cpu.SAR);
        UpdateIndicator(MpdIndicators, (uint)Cpu.Mpd);
        UpdateIndicator(AscDelAdd, Cpu.AscDelAdd);
        UpdateIndicator(AscSpSubt, Cpu.AscSpSubt);
        UpdateIndicator(AscOverflow, Cpu.OverflowFault);
        UpdateIndicator(OverflowFaultLight, Cpu.OverflowFault);
        UpdateIndicator(AscAL, Cpu.AscProbeAL);
        UpdateIndicator(AscAR, Cpu.AscProbeAR);
        UpdateIndicator(AscB, Cpu.AscProbeB);
        UpdateIndicator(AscC, Cpu.AscProbeC);
        UpdateIndicator(AscD, Cpu.AscProbeD);
        UpdateIndicator(AscE, Cpu.AscProbeE);
        UpdateIndicator(InitArithSequenceLog, Cpu.InitArithSequenceLog);
        UpdateIndicator(InitArithSequenceA_1, Cpu.InitArithSequenceA_1);
        UpdateIndicator(InitArithSequenceSP, Cpu.InitArithSequenceSP);
        UpdateIndicator(InitArithSequenceA1, Cpu.InitArithSequenceAL);
        UpdateIndicator(InitArithSequenceQL, Cpu.InitArithSequenceQL);
        UpdateIndicator(InitArithSequenceDiv, Cpu.InitArithSequenceDiv);
        UpdateIndicator(InitArithSequenceMult, Cpu.InitArithSequenceMult);
        UpdateIndicator(InitArithSequenceSEQ, Cpu.InitArithSequenceSeq);
        UpdateIndicator(InitArithSequenceStep, Cpu.InitArithSequenceStep);
        UpdateIndicator(InitArithSequenceCase, Cpu.InitArithSequenceCase);
        UpdateIndicator(InitArithSequenceCKI, Cpu.InitArithSequenceCkI);
        UpdateIndicator(InitArithSequenceCKII, Cpu.InitArithSequenceCkII);
        UpdateIndicator(InitArithSequenceRestX, Cpu.InitArithSequenceRestX);
        UpdateIndicator(InitArithSequenceMultiStep, Cpu.InitArithSequenceMultiStep);
        UpdateIndicator(InitArithSequenceExtSeq, Cpu.InitArithSequenceExtSeq);
        UpdateIndicator(StopTape, Cpu.StopTape);
        UpdateIndicator(SccFault, Cpu.SccFault);
        UpdateIndicator(SccFaultLight, Cpu.SccFault);
        UpdateIndicator(MctFault, Cpu.MctFault);
        UpdateIndicator(MctFaultLight, Cpu.MctFault);
        UpdateIndicator(DivFault, Cpu.DivFault);
        UpdateIndicator(DivFaultLight, Cpu.DivFault);
        UpdateIndicator(AZero, Cpu.AZero);
        UpdateIndicator(TapeFeed, Cpu.TapeFeed);
        UpdateIndicator(Rsc75, Cpu.Rsc75);
        UpdateIndicator(RscHoldRpt, Cpu.RscHoldRpt);
        UpdateIndicator(RscJumpTerm, Cpu.RscJumpTerm);
        UpdateIndicator(RscInitRpt, Cpu.RscInitRpt);
        UpdateIndicator(RscInitTest, Cpu.RscInitTest);
        UpdateIndicator(RscEndRpt, Cpu.RscEndRpt);
        UpdateIndicator(RscDelayTest, Cpu.RscDelayTest);
        UpdateIndicator(RscAdvAdd, Cpu.RscAdvAdd);
        UpdateIndicator(SccInitRead, Cpu.SccInitRead);
        UpdateIndicator(SccInitWrite, Cpu.SccInitWrite);
        UpdateIndicator(SccInitIw0_14, Cpu.SccInitIw0_14);
        UpdateIndicator(SccInitIw15_29, Cpu.SccInitIw15_29);
        UpdateIndicator(SccReadQ, Cpu.SccReadQ);
        UpdateIndicator(SccWriteAorQ, Cpu.SccWriteAorQ);
        UpdateIndicator(SccClearA, Cpu.SccClearA);
        UpdateIndicator(MasterClockCSSI, Cpu.MasterClockCSSI);
        UpdateIndicator(MasterClockCSSII, Cpu.MasterClockCSSII);
        UpdateIndicator(MasterClockCRCI, Cpu.MasterClockCRCI);
        UpdateIndicator(MasterClockCRCII, Cpu.MasterClockCRCII);
        UpdateIndicator(PdcHpc, Cpu.PdcHpc);
        UpdateIndicator(PdcTwc, Cpu.PdcTwc);
        UpdateIndicator(PdcWaitInternal, Cpu.PdcWaitInternal);
        UpdateIndicator(PdcWaitExternal, Cpu.PdcWaitExternal);
        UpdateIndicator(PdcWaitRsc, Cpu.PdcWaitRsc);
        UpdateIndicator(PdcStop, Cpu.PdcStop);
        UpdateIndicator(SctA, Cpu.SctA);
        UpdateIndicator(SctQ, Cpu.SctQ);
        UpdateIndicator(SctMD, Cpu.SctMD);
        UpdateIndicator(SctMcs0, Cpu.SctMcs0);
        UpdateIndicator(SctMcs1, Cpu.SctMcs1);
        UpdateIndicator(IOBIndicators, Cpu.IOB);
        UpdateIndicator(MtcTapeRegisterIndicators, Cpu.MtcTapeRegister);
        UpdateIndicator(MtcTapeControlIndicators, Cpu.MtcTapeControlRegister);
        UpdateIndicator(MtcBlockCounterIndicators, Cpu.MtcBlockCounter);
        UpdateIndicator(MtcSprocketDelayIndicators, Cpu.MtcSprocketDelay);
        UpdateIndicator(MtcStopControlIndicators, Cpu.MtcControlStop);
        UpdateIndicator(MtcLeaderDelayIndicators, Cpu.MtcLeaderDelay);
        UpdateIndicator(MtcInitialDelayIndicators, Cpu.MtcInitialDelay);
        UpdateIndicator(MtcStartControlIndicators, Cpu.MtcStartControl);
        UpdateIndicator(MtcWKIndicators, Cpu.MtcBTK);
        UpdateIndicator(MtcBTKIndicators, Cpu.MtcWK);
        UpdateIndicator(MtcLKIndicators, Cpu.MtcLK);
        UpdateIndicator(MtcTSKIndicators, Cpu.MtcTSK);
        UpdateIndicator(MtcWriteResumeIndicators, Cpu.MtcWriteResume);
        UpdateIndicator(MtcMtWriteControlIndicators, Cpu.MtcMtWriteControl);
        UpdateIndicator(MtcCkErrorParityIndicators, Cpu.MtcCkParityError);
        UpdateIndicator(MtcControlSyncSprocketTestIndicators, Cpu.MtcControlSyncSprocketTest);
        UpdateIndicator(MtcAlignInputRegisterIndicators, Cpu.MtcAlignInputRegister);
        UpdateIndicator(MtcBlockEnd, Cpu.MtcBlockEnd);
        UpdateIndicator(MtcRecordEnd, Cpu.MtcRecordEnd);
        UpdateIndicator(MtcFaultControl, Cpu.MtcFaultControl);
        UpdateIndicator(MtcBccError, Cpu.MtcBccError);
        UpdateIndicator(MtcBccControl, Cpu.MtcBccControl);
        UpdateIndicator(ExtFaultIoA1, Cpu.ExtFaultIoA1);
        UpdateIndicator(ExtFaultIoB1, Cpu.ExtFaultIoB1);

        UpdateIndicator(Halt, Cpu.Halt);
        UpdateIndicator(Interrupt, Cpu.Interrupt);
        UpdateIndicator(MtcReadWriteSync, Cpu.MtcReadWriteSync);
        UpdateIndicator(MtcTskControl, Cpu.MtcTskControl);
        UpdateIndicator(MtcBlShift, Cpu.MtcBlShift);
        UpdateIndicator(MtcBlEnd, Cpu.MtcBlEnd);
        UpdateIndicator(MtcNotReady, Cpu.MtcNotReady);
        UpdateIndicator(MtcTrControl, Cpu.MtcTrControl);
        UpdateIndicator(MtcTrControlTcrSync, Cpu.MtcTrControlTcrSync);
        UpdateIndicator(MtcBsk, Cpu.MtcBsk);
        UpdateIndicator(MtcReadControl, Cpu.MtcReadControl);
        UpdateIndicator(MtcWrite, Cpu.MtcWrite);
        UpdateIndicator(MtcSubt, Cpu.MtcSubt);
        UpdateIndicator(MtcAdd, Cpu.MtcAdd);
        UpdateIndicator(MtcDelay, Cpu.MtcDelay);
        UpdateIndicator(MtcCenterDriveControl, Cpu.MtcCenterDriveControl);
        UpdateIndicator(WaitIoARead, Cpu.WaitIoARead);
        UpdateIndicator(WaitIoBRead, Cpu.WaitIoBRead);
        UpdateIndicator(WaitIoAWrite, Cpu.WaitIoAWrite);
        UpdateIndicator(WaitIoBWrite, Cpu.WaitIoBWrite);
        UpdateIndicator(IoaWrite, Cpu.IoaWrite);
        UpdateIndicator(IoBWrite, Cpu.IoBWrite);
        UpdateIndicator(Select, Cpu.Select);
        UpdateIndicator(IoA, Cpu.IoA);
        UpdateIndicator(FpSRegister, Cpu.FpSRegister);
        UpdateIndicator(FpCRegister, Cpu.FpCRegister);
        UpdateIndicator(FpDRegister, Cpu.FpDRegister);
        UpdateIndicator(FpSequenceGates, Cpu.FpSequenceGates);
        UpdateIndicator(FpNormExit, Cpu.FpNormExit);
        UpdateIndicator(FpMooMrp, Cpu.FpMooMrp);
        UpdateIndicator(FpUV, Cpu.FpUV);
        UpdateIndicator(FpAddSubt, Cpu.FpAddSubt);
        UpdateIndicator(FpMulti, Cpu.FpMulti);
        UpdateIndicator(FpDiv, Cpu.FpDiv);
        UpdateIndicator(FpSign, Cpu.FpSign);
        UpdateIndicator(FpDelayShiftA, Cpu.FpDelayShiftA);
        UpdateIndicator(McsAddressRegisters[1], Cpu.McsAddressRegister[1]);
        UpdateIndicator(McsAddressRegisters[0], Cpu.McsAddressRegister[0]);
        UpdateIndicator(McsMonInit[0], Cpu.McsMonInit[0]);
        UpdateIndicator(McsMonInit[1], Cpu.McsMonInit[1]);
        UpdateIndicator(McsRead[0], Cpu.McsRead[0]);
        UpdateIndicator(McsRead[1], Cpu.McsRead[1]);
        UpdateIndicator(McsWrite[0], Cpu.McsWrite[0]);
        UpdateIndicator(McsWrite[1], Cpu.McsWrite[1]);
        UpdateIndicator(McsWaitInit[0], Cpu.McsWaitInit[0]);
        UpdateIndicator(McsWaitInit[1], Cpu.McsWaitInit[1]);
        UpdateIndicator(McsReadWriteEnable[0], Cpu.McsReadWriteEnable[0]);
        UpdateIndicator(McsReadWriteEnable[1], Cpu.McsReadWriteEnable[1]);
        UpdateIndicator(McsEnId[0], Cpu.McsEnId[0]);
        UpdateIndicator(McsEnId[1], Cpu.McsEnId[1]);
        UpdateIndicator(McsWr0_14[0], Cpu.McsWr0_14[0]);
        UpdateIndicator(McsWr0_14[1], Cpu.McsWr0_14[1]);
        UpdateIndicator(McsWr30_35[1], Cpu.McsWr30_35[1]);
        UpdateIndicator(McsWr30_35[0], Cpu.McsWr30_35[0]);
        UpdateIndicator(McsWr15_29[0], Cpu.McsWr15_29[0]);
        UpdateIndicator(McsWr15_29[1], Cpu.McsWr15_29[1]);
        UpdateIndicator(McsPulseDistributors[0], Cpu.McsPulseDistributor[0]);
        UpdateIndicator(McsPulseDistributors[1], Cpu.McsPulseDistributor[1]);
        UpdateIndicator(HsPunchRes, Cpu.HsPunchRes);
        UpdateIndicator(HsPunchInit, Cpu.HsPunchInit);
        UpdateIndicator(TypewriterRegister, Cpu.TypewriterRegister);
        UpdateIndicator(HsPunchRegister, Cpu.HsPunchRegister);
        UpdateIndicator(DrumGs, Cpu.Drum.Gs);
        UpdateIndicator(DrumAngularIndexCounter, Cpu.Drum.AngularIndexCounter);
        UpdateIndicator(DrumInitWrite, Cpu.Drum.InitWrite);
        UpdateIndicator(DrumInitWrite0_14, Cpu.Drum.InitWrite0_14);
        UpdateIndicator(DrumInitWrite15_29, Cpu.Drum.InitWrite15_29);
        UpdateIndicator(DrumInitRead, Cpu.Drum.InitRead);
        UpdateIndicator(DrumInitDelayedRead, Cpu.Drum.InitDelayedRead);
        UpdateIndicator(DrumReadLockoutI, Cpu.Drum.ReadLockoutI);
        UpdateIndicator(DrumReadLockoutII, Cpu.Drum.ReadLockoutII);
        UpdateIndicator(DrumReadLockoutIII, Cpu.Drum.ReadLockoutIII);
        UpdateIndicator(DrumConincLockout, Cpu.Drum.ConincLockout);
        UpdateIndicator(DrumPreset, Cpu.Drum.Preset);
        UpdateIndicator(DrumAdvanceAik, Cpu.Drum.AdvanceAik);
        UpdateIndicator(DrumCpdI, Cpu.Drum.CpdI);
        UpdateIndicator(DrumCpdII, Cpu.Drum.CpdII);
        UpdateIndicator(DrumInterlace, Cpu.Drum.Interlace);
        UpdateIndicator(DrumGroup, Cpu.Drum.Group);

        IndicateEnableLight.Update(Cpu.IsManualInterruptArmed ? (ulong)1 : 0);
        AbnormalConditionLight.Update(Cpu.IsAbnormalCondition ? (ulong)1 : 0);
        NormalLight.Update(Cpu.IsNormalCondition ? (ulong)1 : 0);
        TestLight.Update(Cpu.IsTestCondition ? (ulong)1 : 0);
        OperatingLight.Update(Cpu.IsOperating ? (ulong)1 : 0);
        ForceStopLight.Update(Cpu.IsForceStopped ? (ulong)1 : 0);

        MatrixDriveFaultLight.Update(Cpu.MatrixDriveFault ? (ulong)1 : 0);
        MtFaultLight.Update(Cpu.TapeFault ? (ulong)1 : 0);
        IOFaultLight.Update(Cpu.IOFault ? (ulong)1 : 0);
        VoltageFaultLight.Update(Cpu.VoltageFault ? (ulong)1 : 0);
        PrintFault.Update(Cpu.PrintFault ? (ulong)1 : 0);
        TempFault.Update(Cpu.TempFault ? (ulong)1 : 0);
        WaterFault.Update(Cpu.WaterFault ? (ulong)1 : 0);
        CharOverflowLight.Update(Cpu.FpCharOverflow ? (ulong)1 : 0);

        UpdateIndicator(OperatingRateIndicators[5], Cpu.ExecuteMode == ExecuteMode.HighSpeed);
        UpdateIndicator(OperatingRateIndicators[4], Cpu.ExecuteMode == ExecuteMode.AutomaticStepOperation);
        UpdateIndicator(OperatingRateIndicators[3], Cpu.ExecuteMode == ExecuteMode.AutomaticStepClock);
        UpdateIndicator(OperatingRateIndicators[2], Cpu.ExecuteMode == ExecuteMode.Operation);
        UpdateIndicator(OperatingRateIndicators[1], Cpu.ExecuteMode == ExecuteMode.Distributor);
        UpdateIndicator(OperatingRateIndicators[0], Cpu.ExecuteMode == ExecuteMode.Clock);

        MainPulseTranslatorIndicators[0].Update(Cpu.MainPulseDistributor == 0 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[1].Update(Cpu.MainPulseDistributor == 1 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[2].Update(Cpu.MainPulseDistributor == 2 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[3].Update(Cpu.MainPulseDistributor == 3 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[4].Update(Cpu.MainPulseDistributor == 4 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[5].Update(Cpu.MainPulseDistributor == 5 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[6].Update(Cpu.MainPulseDistributor == 6 ? (ulong)1 : 0);
        MainPulseTranslatorIndicators[7].Update(Cpu.MainPulseDistributor == 7 ? (ulong)1 : 0);
    }
}