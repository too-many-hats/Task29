using BinUtils;

namespace Emulator.Devices.Computer;

public class Indicators
{
    public Indicator[] A { get; } = Enumerable.Range(0, 72).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] Q { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] X { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MCR { get; } = Enumerable.Range(0, 6).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] VAK { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] UAK { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] PAK { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] SAR { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] Mpd { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] SkTranslator { get; } = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] JTranslator { get; } = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] MainPulseTranslator { get; } = Enumerable.Range(0, 8).Select(_ => new Indicator(false)).ToArray();
    public Indicator[] MainControlTranslator { get; } = Enumerable.Range(0, 64).Select(_ => new Indicator(false)).ToArray();
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

    public Indicator[] OperatingRate { get; } = Enumerable.Range(0, 6).Select(_ => new Indicator(false, LightType.White)).ToArray();
    public Indicator[] SelectiveJump { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(false, LightType.White)).ToArray();
    public Indicator[] Stop { get; } = Enumerable.Range(0, 5).Select(_ => new Indicator(false, LightType.Red)).ToArray();
    public Indicator[] SelectiveStopSelected { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(false, LightType.White)).ToArray();
    public Indicator IndicateEnableLight { get; } = new Indicator(false, LightType.Red);
    public Indicator AbnormalConditionLight { get; } = new Indicator(false, LightType.White);
    public Indicator NormalLight { get; } = new Indicator(false, LightType.Green);
    public Indicator TestLight { get; } = new Indicator(false, LightType.White);
    public Indicator OperatingLight { get; } = new Indicator(false, LightType.Green);
    public Indicator ForceStopLight { get; } = new Indicator(false, LightType.Red);
    public Indicator MatrixDriveFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator MtFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator IOFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator VoltageFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator PrintFault { get; } = new Indicator(false, LightType.Red);
    public Indicator TempFault { get; } = new Indicator(false, LightType.Red);
    public Indicator WaterFault { get; } = new Indicator(false, LightType.Red);
    public Indicator CharOverflowLight { get; } = new Indicator(false, LightType.Red);
    public Indicator MctFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator SccFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator DivFaultLight { get; } = new Indicator(false, LightType.Red);
    public Indicator OverflowFaultLight { get; } = new Indicator(false, LightType.Red);

    //LeftConsolePanel
    public Indicator[] IOBIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTapeRegister { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTapeControl { get; } = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBlockCounter { get; } = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcSprocketDelay { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcStopControl { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcLeaderDelay { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcInitialDelay { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcStartControl { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcWK { get; } = Enumerable.Range(0, 5).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcBTK { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcLK { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcTSK { get; } = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcWriteResume { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcMtWriteControl { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator MtcReadWriteSync { get; } = new Indicator(true);
    public Indicator MtcTskControl { get; } = new Indicator(true);
    public Indicator MtcBlShift { get; } = new Indicator(true);
    public Indicator MtcBlEnd { get; } = new Indicator(true);
    public Indicator MtcNotReady { get; } = new Indicator(true);
    public Indicator MtcBccError { get; } = new Indicator(true);
    public Indicator MtcFaultControl { get; } = new Indicator(true);
    public Indicator[] MtcCkErrorParity { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcControlSyncSprocketTest { get; } = Enumerable.Range(0, 2).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MtcAlignInputRegister { get; } = Enumerable.Range(0, 7).Select(_ => new Indicator(true)).ToArray();
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
    private readonly List<Indicator> _allIndicators;
    public int TotalCyclesLast6Frames { get; private set; }

    private int indicatorFrameIndex = 0;
    const int framesToTrackLightBrightness = 6;
    private int[] lastFramesDuration = new int[framesToTrackLightBrightness]; // six frames chosen based on frame-by-frame analysis of video of a real UNIVAC mainframe

    private Cpu Cpu;

    public Indicators(Cpu cpu)
    {
        Cpu = cpu;

        for (int i = 0; i < McsAddressRegisters.Length; i++)
        {
            McsAddressRegisters[i] = Enumerable.Range(0, 12).Select(_ => new Indicator(true)).ToArray();
            McsMainPulseDistributorTranslators[i] = Enumerable.Range(0, 4).Select(_ => new Indicator(false)).ToArray();
            McsPulseDistributors[i] = Enumerable.Range(0, 3).Select(_ => new Indicator(true)).ToArray();
        }

        _allIndicators = [
            DrumCpdI,
            DrumCpdII,
            DrumPreset,
            DrumAdvanceAik,
            DrumConincLockout,
            DrumConincLockout,
            DrumReadLockoutIII,
            DrumReadLockoutII,
            DrumReadLockoutI,
            DrumInitWrite15_29,
            DrumInitDelayedRead,
            DrumInitRead,
            DrumInitWrite0_14,
            DrumInitWrite,
            HsPunchInit,
            HsPunchRes,
            FpDelayShiftA,
            FpSign,
            FpDiv,
            FpMulti,
            FpAddSubt,
            FpUV,
            FpMooMrp,
            FpNormExit,
            Select,
            IoBWrite,
            IoaWrite,
            WaitIoBWrite,
            WaitIoAWrite,
            WaitIoBRead,
            WaitIoARead,
            ExtFaultIoB1,
            ExtFaultIoA1,
            MtcReadControl,
            ..McsPulseDistributors[0],
            ..McsMainPulseDistributorTranslators[0],
            ..McsAddressRegisters[0],
            ..McsPulseDistributors[1],
            ..McsMainPulseDistributorTranslators[1],
            ..McsAddressRegisters[1],
            ..DrumGroup,
            ..DrumInterlace,
            ..DrumAngularIndexCounter,
            ..MtcBsk,
            ..MtcWrite,
            ..MtcSubt,
            ..MtcAdd,
            ..MtcDelay,
            ..MtcCenterDriveControl,
            ..IoA,
            ..FpSRegister,
            ..FpDRegister,
            ..FpCRegister,
            ..FpSequenceGates,
            ..McsMonInit,
            ..McsRead,
            ..McsWrite,
            ..McsReadWriteEnable,
            ..McsWaitInit,
            ..McsEnId,
            ..McsWr0_14,
            ..McsWr30_35,
            ..McsWr15_29,
            ..HsPunchRegister,
            ..TypewriterRegister,
            ..DrumGs,
            MtcTrControlTcrSync,
            ..MtcCkErrorParity,
            ..MtcControlSyncSprocketTest,
            ..MtcAlignInputRegister,
            ..MtcBlockEnd,
            ..MtcRecordEnd,
            ..MtcBccControl,
            ..MtcTrControl,
            MtcReadWriteSync,
            MtcTskControl,
            MtcBlShift,
            MtcBlEnd,
            MtcNotReady,
            MtcBccError,
            MtcFaultControl,
            ..IOBIndicators,
            ..MtcTapeRegister,
            ..MtcTapeControl,
            ..MtcBlockCounter,
            ..MtcSprocketDelay,
            ..MtcStopControl,
            ..MtcLeaderDelay,
            ..MtcInitialDelay,
            ..MtcStartControl,
            ..MtcWK,
            ..MtcBTK,
            ..MtcLK,
            ..MtcTSK,
            ..MtcWriteResume,
            ..MtcMtWriteControl,
            IndicateEnableLight,
            AbnormalConditionLight,
            NormalLight,
            TestLight,
            OperatingLight,
            ForceStopLight,
            MatrixDriveFaultLight,
            MtFaultLight,
            IOFaultLight,
            VoltageFaultLight,
            PrintFault,
            TempFault,
            WaterFault,
            CharOverflowLight,
            MctFaultLight,
            SccFaultLight,
            DivFaultLight,
            OverflowFaultLight,
            ..OperatingRate,
            ..SelectiveJump,
            ..Stop,
            ..SelectiveStopSelected,
            AscDelAdd,
            AscSpSubt,
            AscOverflow,
            AscAL,
            AscAR,
            AscB,
            AscC,
            AscD,
            AscE,
            InitArithSequenceLog,
            InitArithSequenceA_1,
            InitArithSequenceSP,
            InitArithSequenceA1,
            InitArithSequenceQL,
            InitArithSequenceDiv,
            InitArithSequenceMult,
            InitArithSequenceSEQ,
            InitArithSequenceStep,
            InitArithSequenceCase,
            InitArithSequenceCKI,
            InitArithSequenceCKII,
            InitArithSequenceRestX,
            InitArithSequenceMultiStep,
            InitArithSequenceExtSeq,
            StopTape,
            SccFault,
            MctFault,
            DivFault,
            AZero,
            TapeFeed,
            Rsc75,
            RscHoldRpt,
            RscJumpTerm,
            RscInitRpt,
            RscInitTest,
            RscEndRpt,
            RscDelayTest,
            RscAdvAdd,
            SccInitRead,
            SccInitWrite,
            SccInitIw0_14,
            SccInitIw15_29,
            SccReadQ,
            SccWriteAorQ,
            SccClearA,
            MasterClockCSSI,
            MasterClockCSSII,
            MasterClockCRCI,
            MasterClockCRCII,
            PdcHpc,
            PdcTwc,
            PdcWaitInternal,
            PdcWaitExternal,
            PdcWaitRsc,
            PdcStop,
            SctA,
            SctQ,
            SctMD,
            SctMcs0,
            SctMcs1,
            SctMcs2,
            Halt,
            Interrupt,
            ..A,
            ..Q,
            ..X,
            ..MCR,
            ..VAK,
            ..UAK,
            ..PAK,
            ..SAR,
            ..Mpd,
            ..SkTranslator,
            ..JTranslator,
            ..MainPulseTranslator,
            ..MainControlTranslator,
            ];
    }

    public void Update(Indicator[] indicators, UInt128 value)
    {
        // split the 128bit register into two 64bit halves. Greatly improves performance when everything is in one CPU register.
        ulong lowHalf = (ulong)value & Constants.WordMask;
        ulong highHalf = (ulong)(value >> 36) & Constants.WordMask;
        for (var j = 0; j < 36; j++)
        {
            indicators[j].Update((uint)(lowHalf & 1), Cpu.WorldClock);
            lowHalf = lowHalf >> 1;
        }

        for (var j = 36; j < 72; j++)
        {
            indicators[j].Update((uint)(highHalf & 1), Cpu.WorldClock);
            highHalf = highHalf >> 1;
        }
    }

    public void Update(Indicator[] indicators, ulong value)
    {
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update((uint)(value & 1), Cpu.WorldClock);
            value = value >> 1;
        }
    }

    private void Update(Indicator indicator, bool value)
    {
        indicator.Update(value, Cpu.WorldClock);
    }

    internal void StartBrightnessTrackInFrame(int cyclesExecuted)
    {
        indicatorFrameIndex = (indicatorFrameIndex + 1) % framesToTrackLightBrightness;

        foreach (var indicator in _allIndicators)
        {
            indicator.FrameIndex = indicatorFrameIndex;
            indicator.CyclesLitInFrame[indicatorFrameIndex] = 0; //clear state from last time this frame was recorded.
        }

        lastFramesDuration[indicatorFrameIndex] = cyclesExecuted;
        TotalCyclesLast6Frames = 0;
        foreach (var frame in lastFramesDuration)
        {
            TotalCyclesLast6Frames += frame;
        }
    }

    internal void UpdateStatusEndOfCycle()
    {
        Update(A, Cpu.A);
        Update(Q, Cpu.Q);
        Update(X, Cpu.X);
        Update(MCR, Cpu.MCR);
        Update(UAK, Cpu.UAK);
        Update(VAK, Cpu.VAK);
        Update(PAK, Cpu.PAK);
        Update(SAR, Cpu.SAR);
        Update(Mpd, (uint)Cpu.Mpd);
        Update(AscDelAdd, Cpu.AscDelAdd);
        Update(AscSpSubt, Cpu.AscSpSubt);
        Update(AscOverflow, Cpu.OverflowFault);
        Update(OverflowFaultLight, Cpu.OverflowFault);
        Update(AscAL, Cpu.AscProbeAL);
        Update(AscAR, Cpu.AscProbeAR);
        Update(AscB, Cpu.AscProbeB);
        Update(AscC, Cpu.AscProbeC);
        Update(AscD, Cpu.AscProbeD);
        Update(AscE, Cpu.AscProbeE);
        Update(InitArithSequenceLog, Cpu.InitArithSequenceLog);
        Update(InitArithSequenceA_1, Cpu.InitArithSequenceA_1);
        Update(InitArithSequenceSP, Cpu.InitArithSequenceSP);
        Update(InitArithSequenceA1, Cpu.InitArithSequenceAL);
        Update(InitArithSequenceQL, Cpu.InitArithSequenceQL);
        Update(InitArithSequenceDiv, Cpu.InitArithSequenceDiv);
        Update(InitArithSequenceMult, Cpu.InitArithSequenceMult);
        Update(InitArithSequenceSEQ, Cpu.InitArithSequenceSeq);
        Update(InitArithSequenceStep, Cpu.InitArithSequenceStep);
        Update(InitArithSequenceCase, Cpu.InitArithSequenceCase);
        Update(InitArithSequenceCKI, Cpu.InitArithSequenceCkI);
        Update(InitArithSequenceCKII, Cpu.InitArithSequenceCkII);
        Update(InitArithSequenceRestX, Cpu.InitArithSequenceRestX);
        Update(InitArithSequenceMultiStep, Cpu.InitArithSequenceMultiStep);
        Update(InitArithSequenceExtSeq, Cpu.InitArithSequenceExtSeq);
        Update(StopTape, Cpu.StopTape);
        Update(SccFault, Cpu.SccFault);
        Update(SccFaultLight, Cpu.SccFault);
        Update(MctFault, Cpu.MctFault);
        Update(MctFaultLight, Cpu.MctFault);
        Update(DivFault, Cpu.DivFault);
        Update(DivFaultLight, Cpu.DivFault);
        Update(AZero, Cpu.AZero);
        Update(TapeFeed, Cpu.TapeFeed);
        Update(Rsc75, Cpu.Rsc75);
        Update(RscHoldRpt, Cpu.RscHoldRpt);
        Update(RscJumpTerm, Cpu.RscJumpTerm);
        Update(RscInitRpt, Cpu.RscInitRpt);
        Update(RscInitTest, Cpu.RscInitTest);
        Update(RscEndRpt, Cpu.RscEndRpt);
        Update(RscDelayTest, Cpu.RscDelayTest);
        Update(RscAdvAdd, Cpu.RscAdvAdd);
        Update(SccInitRead, Cpu.SccInitRead);
        Update(SccInitWrite, Cpu.SccInitWrite);
        Update(SccInitIw0_14, Cpu.SccInitIw0_14);
        Update(SccInitIw15_29, Cpu.SccInitIw15_29);
        Update(SccReadQ, Cpu.SccReadQ);
        Update(SccWriteAorQ, Cpu.SccWriteAorQ);
        Update(SccClearA, Cpu.SccClearA);
        Update(MasterClockCSSI, Cpu.MasterClockCSSI);
        Update(MasterClockCSSII, Cpu.MasterClockCSSII);
        Update(MasterClockCRCI, Cpu.MasterClockCRCI);
        Update(MasterClockCRCII, Cpu.MasterClockCRCII);
        Update(PdcHpc, Cpu.PdcHpc);
        Update(PdcTwc, Cpu.PdcTwc);
        Update(PdcWaitInternal, Cpu.PdcWaitInternal);
        Update(PdcWaitExternal, Cpu.PdcWaitExternal);
        Update(PdcWaitRsc, Cpu.PdcWaitRsc);
        Update(PdcStop, Cpu.PdcStop);
        Update(SctA, Cpu.SctA);
        Update(SctQ, Cpu.SctQ);
        Update(SctMD, Cpu.SctMD);
        Update(SctMcs0, Cpu.SctMcs0);
        Update(SctMcs1, Cpu.SctMcs1);
        Update(IOBIndicators, Cpu.IOB);
        Update(MtcTapeRegister, Cpu.MtcTapeRegister);
        Update(MtcTapeControl, Cpu.MtcTapeControlRegister);
        Update(MtcBlockCounter, Cpu.MtcBlockCounter);
        Update(MtcSprocketDelay, Cpu.MtcSprocketDelay);
        Update(MtcStopControl, Cpu.MtcControlStop);
        Update(MtcLeaderDelay, Cpu.MtcLeaderDelay);
        Update(MtcInitialDelay, Cpu.MtcInitialDelay);
        Update(MtcStartControl, Cpu.MtcStartControl);
        Update(MtcWK, Cpu.MtcBTK);
        Update(MtcBTK, Cpu.MtcWK);
        Update(MtcLK, Cpu.MtcLK);
        Update(MtcTSK, Cpu.MtcTSK);
        Update(MtcWriteResume, Cpu.MtcWriteResume);
        Update(MtcMtWriteControl, Cpu.MtcMtWriteControl);
        Update(MtcCkErrorParity, Cpu.MtcCkParityError);
        Update(MtcControlSyncSprocketTest, Cpu.MtcControlSyncSprocketTest);
        Update(MtcAlignInputRegister, Cpu.MtcAlignInputRegister);
        Update(MtcBlockEnd, Cpu.MtcBlockEnd);
        Update(MtcRecordEnd, Cpu.MtcRecordEnd);
        Update(MtcFaultControl, Cpu.MtcFaultControl);
        Update(MtcBccError, Cpu.MtcBccError);
        Update(MtcBccControl, Cpu.MtcBccControl);
        Update(ExtFaultIoA1, Cpu.ExtFaultIoA1);
        Update(ExtFaultIoB1, Cpu.ExtFaultIoB1);

        Update(Halt, Cpu.Halt);
        Update(Interrupt, Cpu.Interrupt);
        Update(MtcReadWriteSync, Cpu.MtcReadWriteSync);
        Update(MtcTskControl, Cpu.MtcTskControl);
        Update(MtcBlShift, Cpu.MtcBlShift);
        Update(MtcBlEnd, Cpu.MtcBlEnd);
        Update(MtcNotReady, Cpu.MtcNotReady);
        Update(MtcTrControl, Cpu.MtcTrControl);
        Update(MtcTrControlTcrSync, Cpu.MtcTrControlTcrSync);
        Update(MtcBsk, Cpu.MtcBsk);
        Update(MtcReadControl, Cpu.MtcReadControl);
        Update(MtcWrite, Cpu.MtcWrite);
        Update(MtcSubt, Cpu.MtcSubt);
        Update(MtcAdd, Cpu.MtcAdd);
        Update(MtcDelay, Cpu.MtcDelay);
        Update(MtcCenterDriveControl, Cpu.MtcCenterDriveControl);
        Update(WaitIoARead, Cpu.WaitIoARead);
        Update(WaitIoBRead, Cpu.WaitIoBRead);
        Update(WaitIoAWrite, Cpu.WaitIoAWrite);
        Update(WaitIoBWrite, Cpu.WaitIoBWrite);
        Update(IoaWrite, Cpu.IoaWrite);
        Update(IoBWrite, Cpu.IoBWrite);
        Update(Select, Cpu.Select);
        Update(IoA, Cpu.IoA);
        Update(FpSRegister, Cpu.FpSRegister);
        Update(FpCRegister, Cpu.FpCRegister);
        Update(FpDRegister, Cpu.FpDRegister);
        Update(FpSequenceGates, Cpu.FpSequenceGates);
        Update(FpNormExit, Cpu.FpNormExit);
        Update(FpMooMrp, Cpu.FpMooMrp);
        Update(FpUV, Cpu.FpUV);
        Update(FpAddSubt, Cpu.FpAddSubt);
        Update(FpMulti, Cpu.FpMulti);
        Update(FpDiv, Cpu.FpDiv);
        Update(FpSign, Cpu.FpSign);
        Update(FpDelayShiftA, Cpu.FpDelayShiftA);
        Update(McsAddressRegisters[1], Cpu.McsAddressRegister[1]);
        Update(McsAddressRegisters[0], Cpu.McsAddressRegister[0]);
        Update(McsMonInit[0], Cpu.McsMonInit[0]);
        Update(McsMonInit[1], Cpu.McsMonInit[1]);
        Update(McsRead[0], Cpu.McsRead[0]);
        Update(McsRead[1], Cpu.McsRead[1]);
        Update(McsWrite[0], Cpu.McsWrite[0]);
        Update(McsWrite[1], Cpu.McsWrite[1]);
        Update(McsWaitInit[0], Cpu.McsWaitInit[0]);
        Update(McsWaitInit[1], Cpu.McsWaitInit[1]);
        Update(McsReadWriteEnable[0], Cpu.McsReadWriteEnable[0]);
        Update(McsReadWriteEnable[1], Cpu.McsReadWriteEnable[1]);
        Update(McsEnId[0], Cpu.McsEnableInhibitDisturb[0]);
        Update(McsEnId[1], Cpu.McsEnableInhibitDisturb[1]);
        Update(McsWr0_14[0], Cpu.McsWr0_14[0]);
        Update(McsWr0_14[1], Cpu.McsWr0_14[1]);
        Update(McsWr30_35[1], Cpu.McsWr30_35[1]);
        Update(McsWr30_35[0], Cpu.McsWr30_35[0]);
        Update(McsWr15_29[0], Cpu.McsWr15_29[0]);
        Update(McsWr15_29[1], Cpu.McsWr15_29[1]);
        Update(McsPulseDistributors[0], Cpu.McsPulseDistributor[0]);
        Update(McsPulseDistributors[1], Cpu.McsPulseDistributor[1]);
        Update(HsPunchRes, Cpu.HsPunchRes);
        Update(HsPunchInit, Cpu.HsPunchInit);
        Update(TypewriterRegister, Cpu.TypewriterRegister);
        Update(HsPunchRegister, Cpu.HsPunchRegister);
        Update(DrumGs, Cpu.Drum.Gs);
        Update(DrumAngularIndexCounter, Cpu.Drum.AngularIndexCounter);
        Update(DrumInitWrite, Cpu.Drum.InitWrite);
        Update(DrumInitWrite0_14, Cpu.Drum.InitWrite0_14);
        Update(DrumInitWrite15_29, Cpu.Drum.InitWrite15_29);
        Update(DrumInitRead, Cpu.Drum.InitRead);
        Update(DrumInitDelayedRead, Cpu.Drum.InitDelayedRead);
        Update(DrumReadLockoutI, Cpu.Drum.ReadLockoutI);
        Update(DrumReadLockoutII, Cpu.Drum.ReadLockoutII);
        Update(DrumReadLockoutIII, Cpu.Drum.ReadLockoutIII);
        Update(DrumConincLockout, Cpu.Drum.ConincLockout);
        Update(DrumPreset, Cpu.Drum.Preset);
        Update(DrumAdvanceAik, Cpu.Drum.AdvanceAik);
        Update(DrumCpdI, Cpu.Drum.CpdI);
        Update(DrumCpdII, Cpu.Drum.CpdII);
        Update(DrumInterlace, Cpu.Drum.Interlace);
        Update(DrumGroup, Cpu.Drum.Group);

        Update(IndicateEnableLight, Cpu.IsManualInterruptArmed);
        Update(AbnormalConditionLight, Cpu.IsAbnormalCondition);
        Update(NormalLight, Cpu.IsNormalCondition);
        Update(TestLight, Cpu.IsTestCondition);
        Update(OperatingLight, Cpu.IsOperating);
        Update(ForceStopLight, Cpu.IsForceStopped);

        Update(MatrixDriveFaultLight, Cpu.MatrixDriveFault);
        Update(MtFaultLight, Cpu.TapeFault);
        Update(IOFaultLight, Cpu.IOFault);
        Update(VoltageFaultLight, Cpu.VoltageFault);
        Update(PrintFault, Cpu.PrintFault);
        Update(TempFault, Cpu.TempFault);
        Update(WaterFault, Cpu.WaterFault);
        Update(CharOverflowLight, Cpu.FpCharOverflow);

        Update(OperatingRate[5], Cpu.ExecuteMode == ExecuteMode.HighSpeed);
        Update(OperatingRate[4], Cpu.ExecuteMode == ExecuteMode.AutomaticStepOperation);
        Update(OperatingRate[3], Cpu.ExecuteMode == ExecuteMode.AutomaticStepClock);
        Update(OperatingRate[2], Cpu.ExecuteMode == ExecuteMode.Operation);
        Update(OperatingRate[1], Cpu.ExecuteMode == ExecuteMode.Distributor);
        Update(OperatingRate[0], Cpu.ExecuteMode == ExecuteMode.Clock);

        Update(MainPulseTranslator[0], Cpu.MainPulseDistributor == 0);
        Update(MainPulseTranslator[1], Cpu.MainPulseDistributor == 1);
        Update(MainPulseTranslator[2], Cpu.MainPulseDistributor == 2);
        Update(MainPulseTranslator[3], Cpu.MainPulseDistributor == 3);
        Update(MainPulseTranslator[4], Cpu.MainPulseDistributor == 4);
        Update(MainPulseTranslator[5], Cpu.MainPulseDistributor == 5);
        Update(MainPulseTranslator[6], Cpu.MainPulseDistributor == 6);
        Update(MainPulseTranslator[7], Cpu.MainPulseDistributor == 7);

        Update(Stop[0], Cpu.IsProgramStopped);
        Update(Stop[1], Cpu.Stops[0]);
        Update(Stop[2], Cpu.Stops[1]);
        Update(Stop[3], Cpu.Stops[2]);
        Update(Stop[4], Cpu.Stops[3]);

        Update(SelectiveStopSelected[0], Cpu.SelectiveStopSelected[2]);
        Update(SelectiveStopSelected[1], Cpu.SelectiveStopSelected[1]);
        Update(SelectiveStopSelected[2], Cpu.SelectiveStopSelected[0]);

        Update(SelectiveJump[0], Cpu.SelectiveJumps[2]);
        Update(SelectiveJump[1], Cpu.SelectiveJumps[1]);
        Update(SelectiveJump[2], Cpu.SelectiveJumps[0]);

        Update(McsMainPulseDistributorTranslators[0][0], Cpu.McsPulseDistributor[0] == 1);
        Update(McsMainPulseDistributorTranslators[0][1], Cpu.McsPulseDistributor[0] == 2);
        Update(McsMainPulseDistributorTranslators[0][2], Cpu.McsPulseDistributor[0] == 3);
        Update(McsMainPulseDistributorTranslators[0][3], Cpu.McsPulseDistributor[0] == 4);

        Update(McsMainPulseDistributorTranslators[1][0], Cpu.McsPulseDistributor[1] == 1);
        Update(McsMainPulseDistributorTranslators[1][1], Cpu.McsPulseDistributor[1] == 2);
        Update(McsMainPulseDistributorTranslators[1][2], Cpu.McsPulseDistributor[1] == 3);
        Update(McsMainPulseDistributorTranslators[1][3], Cpu.McsPulseDistributor[1] == 4);

        foreach (var indicator in MainControlTranslator)
        {
            Update(indicator, false);
        }
    }
}
