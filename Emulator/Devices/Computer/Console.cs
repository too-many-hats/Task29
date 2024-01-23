
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
    public Indicator AscDelAddIndicator { get; } = new Indicator(true);
    public Indicator AscSpSubtIndicator { get; } = new Indicator(true);
    public Indicator AscOverflowIndicator { get; } = new Indicator(true);
    public Indicator AscALIndicator { get; } = new Indicator(true);
    public Indicator AscARIndicator { get; } = new Indicator(true);
    public Indicator AscBIndicator { get; } = new Indicator(true);
    public Indicator AscCIndicator { get; } = new Indicator(true);
    public Indicator AscDIndicator { get; } = new Indicator(true);
    public Indicator AscEIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceLogIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceA_1Indicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceSPIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceA1Indicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceQLIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceDivIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceMultIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceSEQIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceStepIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceCaseIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceCKIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceCKIIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceRestXIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceMultiStepIndicator { get; } = new Indicator(true);
    public Indicator InitArithSequenceExtSeoIndicator { get; } = new Indicator(true);
    public Indicator StopTapeIndicator { get; } = new Indicator(true);
    public Indicator SccFaultIndicator { get; } = new Indicator(true);
    public Indicator MctFaultIndicator { get; } = new Indicator(true);
    public Indicator DivFaultIndicator { get; } = new Indicator(true);
    public Indicator AZeroIndicator { get; } = new Indicator(true);
    public Indicator TapeFeedIndicator { get; } = new Indicator(true);
    public Indicator Rsc75Indicator { get; } = new Indicator(true);
    public Indicator RscHoldRptIndicator { get; } = new Indicator(true);
    public Indicator RscJumpTermIndicator { get; } = new Indicator(true);
    public Indicator RscInitRptIndicator { get; } = new Indicator(true);
    public Indicator RscInitTestIndicator { get; } = new Indicator(true);
    public Indicator RscEndRptIndicator { get; } = new Indicator(true);
    public Indicator RscDelayTestIndicator { get; } = new Indicator(true);
    public Indicator RscAdvAddIndicator { get; } = new Indicator(true);
    public Indicator SccInitReadIndicator { get; } = new Indicator(true);
    public Indicator SccInitWriteIndicator { get; } = new Indicator(true);
    public Indicator SccInitIw0_14Indicator { get; } = new Indicator(true);
    public Indicator SccInitIw15_29Indicator { get; } = new Indicator(true);
    public Indicator SccReadQIndicator { get; } = new Indicator(true);
    public Indicator SccWriteAorQIndicator { get; } = new Indicator(true);
    public Indicator SccClearAIndicator { get; } = new Indicator(true);
    public Indicator MasterClockCSSIIndicator { get; } = new Indicator(true);
    public Indicator MasterClockCSSIIIndicator { get; } = new Indicator(true);
    public Indicator MasterClockCRCIIndicator { get; } = new Indicator(true);
    public Indicator MasterClockCRCIIIndicator { get; } = new Indicator(true);
    public Indicator PdcHpcIndicator { get; } = new Indicator(true);
    public Indicator PdcTwcIndicator { get; } = new Indicator(true);
    public Indicator PdcWaitInternalIndicator { get; } = new Indicator(true);
    public Indicator PdcWaitExternalIndicator { get; } = new Indicator(true);
    public Indicator PdcWaitRscIndicator { get; } = new Indicator(true);
    public Indicator PdcStop { get; } = new Indicator(true);
    public Indicator SctA { get; } = new Indicator(false);
    public Indicator SctQ { get; } = new Indicator(false);
    public Indicator SctMD { get; } = new Indicator(false);
    public Indicator SctMcs0 { get; } = new Indicator(false);
    public Indicator SctMcs1 { get; } = new Indicator(false);
    public Indicator SctMcs2 { get; } = new Indicator(false);
    public Indicator Halt { get; } = new Indicator(true);
    public Indicator Interrupt { get; } = new Indicator(true);

    private Cpu Cpu { get; set; }

    public Console(Cpu cpu)
    {
        Cpu = cpu;
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

        Halt.EndFrame();
        Interrupt.EndFrame();

        AscDelAddIndicator.EndFrame();
        AscSpSubtIndicator.EndFrame();
        AscOverflowIndicator.EndFrame();
        AscALIndicator.EndFrame();
        AscARIndicator.EndFrame();
        AscBIndicator.EndFrame();
        AscCIndicator.EndFrame();
        AscDIndicator.EndFrame();
        AscEIndicator.EndFrame();
        InitArithSequenceLogIndicator.EndFrame();
        InitArithSequenceA_1Indicator.EndFrame();
        InitArithSequenceSPIndicator.EndFrame();
        InitArithSequenceA1Indicator.EndFrame();
        InitArithSequenceQLIndicator.EndFrame();
        InitArithSequenceDivIndicator.EndFrame();
        InitArithSequenceMultIndicator.EndFrame();
        InitArithSequenceSEQIndicator.EndFrame();
        InitArithSequenceStepIndicator.EndFrame();
        InitArithSequenceCaseIndicator.EndFrame();
        InitArithSequenceCKIndicator.EndFrame();
        InitArithSequenceCKIIndicator.EndFrame();
        InitArithSequenceRestXIndicator.EndFrame();
        InitArithSequenceMultiStepIndicator.EndFrame();
        InitArithSequenceExtSeoIndicator.EndFrame();

        StopTapeIndicator.EndFrame();
        SccFaultIndicator.EndFrame();
        MctFaultIndicator.EndFrame();
        DivFaultIndicator.EndFrame();
        AZeroIndicator.EndFrame();
        TapeFeedIndicator.EndFrame();
        Rsc75Indicator.EndFrame();
        RscHoldRptIndicator.EndFrame();
        RscJumpTermIndicator.EndFrame();
        RscInitRptIndicator.EndFrame();
        RscInitTestIndicator.EndFrame();
        RscEndRptIndicator.EndFrame();
        RscDelayTestIndicator.EndFrame();
        RscAdvAddIndicator.EndFrame();

        SccInitReadIndicator.EndFrame();
        SccInitWriteIndicator.EndFrame();
        SccInitIw0_14Indicator.EndFrame();
        SccInitIw15_29Indicator.EndFrame();
        SccReadQIndicator.EndFrame();
        SccWriteAorQIndicator.EndFrame();
        SccClearAIndicator.EndFrame();

        MasterClockCSSIIndicator.EndFrame();
        MasterClockCSSIIIndicator.EndFrame();
        MasterClockCRCIIndicator.EndFrame();
        MasterClockCRCIIIndicator.EndFrame();

        PdcHpcIndicator.EndFrame();
        PdcTwcIndicator.EndFrame();
        PdcWaitInternalIndicator.EndFrame();
        PdcWaitExternalIndicator.EndFrame();
        PdcWaitRscIndicator.EndFrame();
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

    internal void UpdateBinaryIndicator(Indicator[] indicators, UInt128 value)
    {
        // split the 128bit register into two 64bit halves. Greatly improves performance when everything is in one CPU register.
        ulong mask = 1;
        ulong lowHalf = (ulong)value & Constants.WordMask;
        ulong highHalf = (ulong)(value >> 36) & Constants.WordMask;
        for (var j = 0; j < 36; j++)
        {
            indicators[j].Update((lowHalf & mask) >> j);
            mask = mask << 1;
        }
        mask = 1;
        for (var j = 36; j < 36; j++)
        {
            indicators[j].Update((highHalf & mask) >> j);
            mask = mask << 1;
        }
    }

    internal void UpdateBinaryIndicator(Indicator[] indicators, ulong value)
    {
        ulong mask = 1;
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update((value & mask) >> j);
            mask = mask << 1;
        }
    }

    internal void UpdateBinaryIndicator(Indicator indicator, bool value)
    {
        indicator.Update(value ? (ulong)1 : 0);
    }

    public void PowerOnPressed()
    {
        Cpu.PowerOnPressed();
    }

    internal void UpdateIndicatorStatusEndOfCycle()
    {
        UpdateBinaryIndicator(AIndicators, Cpu.A);
        UpdateBinaryIndicator(QIndicators, Cpu.Q);
        UpdateBinaryIndicator(XIndicators, Cpu.X);
        UpdateBinaryIndicator(MCRIndicators, Cpu.MCR);
        UpdateBinaryIndicator(UAKIndicators, Cpu.UAK);
        UpdateBinaryIndicator(VAKIndicators, Cpu.VAK);
        UpdateBinaryIndicator(PAKIndicators, Cpu.PAK);
        UpdateBinaryIndicator(SARIndicators, Cpu.SAR);
        UpdateBinaryIndicator(MpdIndicators, (uint)Cpu.Mpd);
        UpdateBinaryIndicator(AscDelAddIndicator, Cpu.AscDelAdd);
        UpdateBinaryIndicator(AscSpSubtIndicator, Cpu.AscSpSubt);
        UpdateBinaryIndicator(AscOverflowIndicator, Cpu.OverflowFault);
        UpdateBinaryIndicator(AscALIndicator, Cpu.AscProbeAL);
        UpdateBinaryIndicator(AscARIndicator, Cpu.AscProbeAR);
        UpdateBinaryIndicator(AscBIndicator, Cpu.AscProbeB);
        UpdateBinaryIndicator(AscCIndicator, Cpu.AscProbeC);
        UpdateBinaryIndicator(AscDIndicator, Cpu.AscProbeD);
        UpdateBinaryIndicator(AscEIndicator, Cpu.AscProbeE);
        UpdateBinaryIndicator(InitArithSequenceLogIndicator, Cpu.InitArithSequenceLog);
        UpdateBinaryIndicator(InitArithSequenceA_1Indicator, Cpu.InitArithSequenceA_1);
        UpdateBinaryIndicator(InitArithSequenceSPIndicator, Cpu.InitArithSequenceSP);
        UpdateBinaryIndicator(InitArithSequenceA1Indicator, Cpu.InitArithSequenceAL);
        UpdateBinaryIndicator(InitArithSequenceQLIndicator, Cpu.InitArithSequenceQL);
        UpdateBinaryIndicator(InitArithSequenceDivIndicator, Cpu.InitArithSequenceDiv);
        UpdateBinaryIndicator(InitArithSequenceMultIndicator, Cpu.InitArithSequenceMult);
        UpdateBinaryIndicator(InitArithSequenceSEQIndicator, Cpu.InitArithSequenceSeq);
        UpdateBinaryIndicator(InitArithSequenceStepIndicator, Cpu.InitArithSequenceStep);
        UpdateBinaryIndicator(InitArithSequenceCaseIndicator, Cpu.InitArithSequenceCase);
        UpdateBinaryIndicator(InitArithSequenceCKIndicator, Cpu.InitArithSequenceCkI);
        UpdateBinaryIndicator(InitArithSequenceCKIIndicator, Cpu.InitArithSequenceCkII);
        UpdateBinaryIndicator(InitArithSequenceRestXIndicator, Cpu.InitArithSequenceRestX);
        UpdateBinaryIndicator(InitArithSequenceMultiStepIndicator, Cpu.InitArithSequenceMultiStep);
        UpdateBinaryIndicator(InitArithSequenceExtSeoIndicator, Cpu.InitArithSequenceExtSeq);
        UpdateBinaryIndicator(StopTapeIndicator, Cpu.StopTape);
        UpdateBinaryIndicator(SccFaultIndicator, Cpu.SccFault);
        UpdateBinaryIndicator(MctFaultIndicator, Cpu.MctFault);
        UpdateBinaryIndicator(DivFaultIndicator, Cpu.DivFault);
        UpdateBinaryIndicator(AZeroIndicator, Cpu.AZero);
        UpdateBinaryIndicator(TapeFeedIndicator, Cpu.TapeFeed);
        UpdateBinaryIndicator(Rsc75Indicator, Cpu.Rsc75);
        UpdateBinaryIndicator(RscHoldRptIndicator, Cpu.RscHoldRpt);
        UpdateBinaryIndicator(RscJumpTermIndicator, Cpu.RscJumpTerm);
        UpdateBinaryIndicator(RscInitRptIndicator, Cpu.RscInitRpt);
        UpdateBinaryIndicator(RscInitTestIndicator, Cpu.RscInitTest);
        UpdateBinaryIndicator(RscEndRptIndicator, Cpu.RscEndRpt);
        UpdateBinaryIndicator(RscDelayTestIndicator, Cpu.RscDelayTest);
        UpdateBinaryIndicator(RscAdvAddIndicator, Cpu.RscAdvAdd);
        UpdateBinaryIndicator(SccInitReadIndicator, Cpu.SccInitRead);
        UpdateBinaryIndicator(SccInitWriteIndicator, Cpu.SccInitWrite);
        UpdateBinaryIndicator(SccInitIw0_14Indicator, Cpu.SccInitIw0_14);
        UpdateBinaryIndicator(SccInitIw15_29Indicator, Cpu.SccInitIw15_29);
        UpdateBinaryIndicator(SccReadQIndicator, Cpu.SccReadQ);
        UpdateBinaryIndicator(SccWriteAorQIndicator, Cpu.SccWriteAorQ);
        UpdateBinaryIndicator(SccClearAIndicator, Cpu.SccClearA);
        UpdateBinaryIndicator(MasterClockCSSIIndicator, Cpu.MasterClockCSSI);
        UpdateBinaryIndicator(MasterClockCSSIIIndicator, Cpu.MasterClockCSSII);
        UpdateBinaryIndicator(MasterClockCRCIIndicator, Cpu.MasterClockCRCI);
        UpdateBinaryIndicator(MasterClockCRCIIIndicator, Cpu.MasterClockCRCII);
        UpdateBinaryIndicator(PdcHpcIndicator, Cpu.PdcHpc);
        UpdateBinaryIndicator(PdcTwcIndicator, Cpu.PdcTwc);
        UpdateBinaryIndicator(PdcWaitInternalIndicator, Cpu.PdcWaitInternal);
        UpdateBinaryIndicator(PdcWaitExternalIndicator, Cpu.PdcWaitExternal);
        UpdateBinaryIndicator(PdcWaitRscIndicator, Cpu.PdcWaitRsc);
        UpdateBinaryIndicator(PdcStop, Cpu.PdcStop);
        UpdateBinaryIndicator(SctA, Cpu.SctA);
        UpdateBinaryIndicator(SctQ, Cpu.SctQ);
        UpdateBinaryIndicator(SctMD, Cpu.SctMD);
        UpdateBinaryIndicator(SctMcs0, Cpu.SctMcs0);
        UpdateBinaryIndicator(SctMcs1, Cpu.SctMcs1);
        UpdateBinaryIndicator(SctMcs2, Cpu.SctMcs2);

        JTranslatorIndicators[0].Update(1);
        JTranslatorIndicators[3].Update(1);

        SkTranslatorIndicators[0].Update(1);
        SkTranslatorIndicators[3].Update(1);

        MainPulseTranslatorIndicators[0].Update(1);
        MainPulseTranslatorIndicators[7].Update(1);

        UpdateBinaryIndicator(Halt, Cpu.Halt);
        UpdateBinaryIndicator(Interrupt, Cpu.Interrupt);
    }
}