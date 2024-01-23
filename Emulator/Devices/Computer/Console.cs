
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
        EndFramesOfAll(MainControlTranslatorIndicators);

        Halt.EndFrame();
        Interrupt.EndFrame();

        AscDelAdd.EndFrame();
        AscSpSubt.EndFrame();
        AscOverflow.EndFrame();
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
        MctFault.EndFrame();
        DivFault.EndFrame();
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

    private static void UpdateIndicator(Indicator[] indicators, ulong value)
    {
        ulong mask = 1;
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update((value & mask) >> j);
            mask = mask << 1;
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
        UpdateIndicator(MctFault, Cpu.MctFault);
        UpdateIndicator(DivFault, Cpu.DivFault);
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
        UpdateIndicator(SctMcs2, Cpu.SctMcs2);

        JTranslatorIndicators[0].Update(1);
        JTranslatorIndicators[3].Update(1);

        SkTranslatorIndicators[0].Update(1);
        SkTranslatorIndicators[3].Update(1);

        MainPulseTranslatorIndicators[0].Update(1);
        MainPulseTranslatorIndicators[7].Update(1);

        UpdateIndicator(Halt, Cpu.Halt);
        UpdateIndicator(Interrupt, Cpu.Interrupt);

        foreach(var indicator in MainControlTranslatorIndicators)
        {
            indicator.Update(1);
        }
    }
}