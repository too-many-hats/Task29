
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
        UpdateBinaryIndicator(AscDelAdd, Cpu.AscDelAdd);
        UpdateBinaryIndicator(AscSpSubt, Cpu.AscSpSubt);
        UpdateBinaryIndicator(AscOverflow, Cpu.OverflowFault);
        UpdateBinaryIndicator(AscAL, Cpu.AscProbeAL);
        UpdateBinaryIndicator(AscAR, Cpu.AscProbeAR);
        UpdateBinaryIndicator(AscB, Cpu.AscProbeB);
        UpdateBinaryIndicator(AscC, Cpu.AscProbeC);
        UpdateBinaryIndicator(AscD, Cpu.AscProbeD);
        UpdateBinaryIndicator(AscE, Cpu.AscProbeE);
        UpdateBinaryIndicator(InitArithSequenceLog, Cpu.InitArithSequenceLog);
        UpdateBinaryIndicator(InitArithSequenceA_1, Cpu.InitArithSequenceA_1);
        UpdateBinaryIndicator(InitArithSequenceSP, Cpu.InitArithSequenceSP);
        UpdateBinaryIndicator(InitArithSequenceA1, Cpu.InitArithSequenceAL);
        UpdateBinaryIndicator(InitArithSequenceQL, Cpu.InitArithSequenceQL);
        UpdateBinaryIndicator(InitArithSequenceDiv, Cpu.InitArithSequenceDiv);
        UpdateBinaryIndicator(InitArithSequenceMult, Cpu.InitArithSequenceMult);
        UpdateBinaryIndicator(InitArithSequenceSEQ, Cpu.InitArithSequenceSeq);
        UpdateBinaryIndicator(InitArithSequenceStep, Cpu.InitArithSequenceStep);
        UpdateBinaryIndicator(InitArithSequenceCase, Cpu.InitArithSequenceCase);
        UpdateBinaryIndicator(InitArithSequenceCKI, Cpu.InitArithSequenceCkI);
        UpdateBinaryIndicator(InitArithSequenceCKII, Cpu.InitArithSequenceCkII);
        UpdateBinaryIndicator(InitArithSequenceRestX, Cpu.InitArithSequenceRestX);
        UpdateBinaryIndicator(InitArithSequenceMultiStep, Cpu.InitArithSequenceMultiStep);
        UpdateBinaryIndicator(InitArithSequenceExtSeq, Cpu.InitArithSequenceExtSeq);
        UpdateBinaryIndicator(StopTape, Cpu.StopTape);
        UpdateBinaryIndicator(SccFault, Cpu.SccFault);
        UpdateBinaryIndicator(MctFault, Cpu.MctFault);
        UpdateBinaryIndicator(DivFault, Cpu.DivFault);
        UpdateBinaryIndicator(AZero, Cpu.AZero);
        UpdateBinaryIndicator(TapeFeed, Cpu.TapeFeed);
        UpdateBinaryIndicator(Rsc75, Cpu.Rsc75);
        UpdateBinaryIndicator(RscHoldRpt, Cpu.RscHoldRpt);
        UpdateBinaryIndicator(RscJumpTerm, Cpu.RscJumpTerm);
        UpdateBinaryIndicator(RscInitRpt, Cpu.RscInitRpt);
        UpdateBinaryIndicator(RscInitTest, Cpu.RscInitTest);
        UpdateBinaryIndicator(RscEndRpt, Cpu.RscEndRpt);
        UpdateBinaryIndicator(RscDelayTest, Cpu.RscDelayTest);
        UpdateBinaryIndicator(RscAdvAdd, Cpu.RscAdvAdd);
        UpdateBinaryIndicator(SccInitRead, Cpu.SccInitRead);
        UpdateBinaryIndicator(SccInitWrite, Cpu.SccInitWrite);
        UpdateBinaryIndicator(SccInitIw0_14, Cpu.SccInitIw0_14);
        UpdateBinaryIndicator(SccInitIw15_29, Cpu.SccInitIw15_29);
        UpdateBinaryIndicator(SccReadQ, Cpu.SccReadQ);
        UpdateBinaryIndicator(SccWriteAorQ, Cpu.SccWriteAorQ);
        UpdateBinaryIndicator(SccClearA, Cpu.SccClearA);
        UpdateBinaryIndicator(MasterClockCSSI, Cpu.MasterClockCSSI);
        UpdateBinaryIndicator(MasterClockCSSII, Cpu.MasterClockCSSII);
        UpdateBinaryIndicator(MasterClockCRCI, Cpu.MasterClockCRCI);
        UpdateBinaryIndicator(MasterClockCRCII, Cpu.MasterClockCRCII);
        UpdateBinaryIndicator(PdcHpc, Cpu.PdcHpc);
        UpdateBinaryIndicator(PdcTwc, Cpu.PdcTwc);
        UpdateBinaryIndicator(PdcWaitInternal, Cpu.PdcWaitInternal);
        UpdateBinaryIndicator(PdcWaitExternal, Cpu.PdcWaitExternal);
        UpdateBinaryIndicator(PdcWaitRsc, Cpu.PdcWaitRsc);
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