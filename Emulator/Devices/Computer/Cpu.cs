using System.Diagnostics;

namespace Emulator.Devices.Computer;

public class Cpu
{
    public static uint ClockCycleMicroseconds => 2; // the machine operates at 500,000 clock cycles per second. So 2 microseconds per clock cycle. All timing in the machine is a multiple of this value.

    public ulong[] Memory { get; private set; } = new ulong[8192];
    public Drum Drum { get; private set; }
    public Console Console { get; private set; }

    //main operating registers
    // top left of the center console panel
    public ulong Q { get; private set; }
    public ulong X { get; private set; }
    public UInt128 A { get; private set; }
    public uint MCR { get; private set; } //Master Control Register (holds opcode during execution)
    public uint VAK { get; private set; } // V Address Kounter 
    public uint UAK { get; private set; } // U Address Kounter
    public uint SAR { get; private set; } // Storage Address Register
    public uint PAK { get; private set; } // Program Address Kounter
    public bool Halt { get; private set; }
    public bool Interrupt { get; private set; }

    public bool OverflowFault { get; private set; }
    public bool PrintFault { get; private set; }
    public bool TempFault { get; private set; }
    public bool WaterFault { get; private set; }
    public bool MatrixDriveFault { get; private set; }
    public bool TapeFault { get; private set; }
    public bool IOFault { get; private set; }
    public bool VoltageFault { get; private set; }

    public bool IsForceStopped { get; private set; }


    // storage class control state
    public bool SccInitRead { get; private set; }
    public bool SccInitWrite { get; private set; }
    public bool SccInitIw0_14 { get; private set; }
    public bool SccInitIw15_29 { get; private set; }
    public bool SccReadQ { get; private set; }
    public bool SccWriteAorQ { get; private set; }
    public bool SccClearA { get; private set; }

    //storage class control translator state. Managed by the CPU so other devices (console) don't need internal details of the processor.
    public bool SctA { get; private set; }
    public bool SctQ { get; private set; }
    public bool SctMD { get; private set; }
    public bool SctMcs0 { get; private set; }
    public bool SctMcs1 { get; private set; }

    //arithmetic sequence control state
    public bool AscDelAdd { get; private set; }
    public bool AscSpSubt { get; private set; }
    public bool AscProbeAL { get; private set; }
    public bool AscProbeAR { get; private set; }
    public bool AscProbeB { get; private set; }
    public bool AscProbeC { get; private set; }
    public bool AscProbeD { get; private set; }
    public bool AscProbeE { get; private set; }
    public bool InitArithSequenceLog { get; private set; }
    public bool InitArithSequenceA_1 { get; private set; }
    public bool InitArithSequenceSP { get; private set; }
    public bool InitArithSequenceAL { get; private set; }
    public bool InitArithSequenceQL { get; private set; }
    public bool InitArithSequenceDiv { get; private set; }
    public bool InitArithSequenceMult { get; private set; }
    public bool InitArithSequenceSeq { get; private set; }
    public bool InitArithSequenceStep { get; private set; }
    public bool InitArithSequenceCase { get; private set; }
    public bool InitArithSequenceCkI { get; private set; }
    public bool InitArithSequenceCkII { get; private set; }
    public bool InitArithSequenceRestX { get; private set; }
    public bool InitArithSequenceMultiStep { get; private set; }
    public bool InitArithSequenceExtSeq { get; private set; }

    //repeat sequence control state
    public bool Rsc75 { get; private set; }
    public bool RscHoldRpt { get; private set; }
    public bool RscJumpTerm { get; private set; }
    public bool RscInitRpt { get; private set; }
    public bool RscInitTest { get; private set; }
    public bool RscEndRpt { get; private set; }
    public bool RscDelayTest { get; private set; }
    public bool RscAdvAdd { get; private set; }

    //faults and tape state (center left section, center console panel)
    public bool StopTape { get; private set; }
    public bool SccFault { get; private set; }
    public bool MctFault { get; private set; }
    public bool DivFault { get; private set; }
    public bool AZero { get; private set; }
    public bool TapeFeed { get; private set; }

    //master clock state
    public bool MasterClockCSSI { get; private set; }
    public bool MasterClockCSSII { get; private set; }
    public bool MasterClockCRCI { get; private set; }
    public bool MasterClockCRCII { get; private set; }

    //pulse distribution control state
    public bool PdcHpc { get; private set; }
    public bool PdcTwc { get; private set; }
    public bool PdcWaitInternal { get; private set; }
    public bool PdcWaitExternal { get; private set; }
    public bool PdcWaitRsc { get; private set; }
    public bool PdcStop { get; private set; }

    //MPD state
    public int Mpd { get; private set; }

    // center console panel lower section state.
    public ExecuteMode ExecuteMode { get; private set; }
    public int OperatingRate { get; private set; } = int.MaxValue; // the number of cycles executed per-second, between 5-35. Only used when in TEST mode and the START key is pressed.

    public bool[] SelectiveJumps { get; private set; } = new bool[3];

    public bool IsFinalStopped { get; private set; }
    public bool[] SelectiveStops { get; private set; } = new bool[4];

    public bool IsManualInterruptArmed { get; private set; }

    public bool IsNormalCondition => TypeAFault == false && TypeBFault == false && IsTestCondition == false;
    public bool IsTestCondition => ExecuteMode != ExecuteMode.HighSpeed || TestNormalSwitch == true || AbnormalNormalDrumSwitch == true;

    // as per maint manual, any which in the Test Switch Group or MD Disconnect group enables ABNORMAL CONDITION.
    public bool IsAbnormalCondition => CL_TCRDisconnectSwitch || CL_TRDisconnectSwitch || IOB_TCRDisconnectSwitch || IOB_TRDisconnectSwitch || IOB_BKDisconnectSwitch || TR_IOBDisconnectSwitch || StartDisconnectSwitch || ErrorSignalDisconnectSwitch || ReadDisconnectSwitch || WriteDisconnectSwitch || DisconnectMdWriteVoltage4Switch || DisconnectMdWriteVoltage5Switch || DisconnectMdWriteVoltage6Switch || DisconnectMdWriteVoltage7Switch || DisconnectClearASwitch || DisconnectClearXSwitch || DisconnectClearQSwitch || DisconnectClearSARSwitch || DisconnectClearPAKSwitch || DisconnectClearPCRSwitch || DisconnectInitiateWrite0_35Switch || DisconnectInitiateWrite0_14Switch || DisconnectInitiateWrite15_29Switch || F140001_00000Switch || SingleMcsSelectionSwitch || OscDrumSwitch || DisconnectStopSwitch || DisconnectSARToPAKSwitch || DisconnectVAKToSARSwitch || DisconnectQ1ToX1Switch || DisconnectXToPCRSwitch || DisconnectAdvPAKSwitch || DisconnectBackSKSwitch || DisconnectWaitInitSwitch || ForceMcZeroSwitch || ForceMcOneSwitch || PtAmpMarginalCheckSwitch || Mcs0AmpMarginalCheckSwitch || Mcs1AmpMarginalCheckSwitch || MDAmpMarginalCheckSwitch || MTAmpMarginalCheckSwitch || ContReduceHeaterVoltageSwitch || ArithReduceHeaterVoltageSwitch || Mcs0ReduceHeaterVoltageSwitch || Mcs1ReduceHeaterVoltageSwitch || MTReduceHeaterVoltageSwitch || MDReduceHeaterVoltageSwitch || DisconnectPAKToSARSwitch || TestNormalSwitch || AbnormalNormalDrumSwitch;

    public bool IsOperating { get; private set; }
    public bool TypeAFault => DivFault | SccFault | OverflowFault | PrintFault | TempFault | WaterFault | FpCharOverflow;
    public bool TypeBFault => MatrixDriveFault | TapeFault | MctFault | IOFault | VoltageFault;

    // throw switches.
    //
    // NOTE: the naming convention for throw switches is: the on position label is the first part of the identifier. The off position label as the second part of the identifier, followed by the word switch.

    // NOTE: There are no event handlers for throw switches, there are pure physical devices with only an on/off state. On the next cycle the CPU can act on the new state. These are not affected by power status or MASTER CLEAR.
    public bool TestNormalSwitch { get; set; }
    public bool AbnormalNormalDrumSwitch { get; set; }
    public bool CL_TCRDisconnectSwitch { get; set; }
    public bool CL_TRDisconnectSwitch { get; set; }
    public bool IOB_TCRDisconnectSwitch { get; set; }
    public bool IOB_TRDisconnectSwitch { get; set; }
    public bool IOB_BKDisconnectSwitch { get; set; }
    public bool TR_IOBDisconnectSwitch { get; set; }
    public bool StartDisconnectSwitch { get; set; }
    public bool ErrorSignalDisconnectSwitch { get; set; }
    public bool ReadDisconnectSwitch { get; set; }
    public bool WriteDisconnectSwitch { get; set; }

    public bool DisconnectMdWriteVoltage4Switch { get; set; }
    public bool DisconnectMdWriteVoltage5Switch { get; set; }
    public bool DisconnectMdWriteVoltage6Switch { get; set; }
    public bool DisconnectMdWriteVoltage7Switch { get; set; }

    public bool DisconnectClearASwitch { get; set; }
    public bool DisconnectClearXSwitch { get; set; }
    public bool DisconnectClearQSwitch { get; set; }
    public bool DisconnectClearSARSwitch { get; set; }
    public bool DisconnectClearPAKSwitch { get; set; }
    public bool DisconnectClearPCRSwitch { get; set; }
    public bool DisconnectInitiateWrite0_35Switch { get; set; }
    public bool DisconnectInitiateWrite0_14Switch { get; set; }
    public bool DisconnectInitiateWrite15_29Switch { get; set; }
    public bool F140001_00000Switch { get; set; }
    public bool SingleMcsSelectionSwitch { get; set; }
    public bool OscDrumSwitch { get; set; }
    public bool DisconnectStopSwitch { get; set; }
    public bool DisconnectSARToPAKSwitch { get; set; }
    public bool DisconnectPAKToSARSwitch { get; set; }
    public bool DisconnectVAKToSARSwitch { get; set; }
    public bool DisconnectQ1ToX1Switch { get; set; } // what looks like a '1' in the image probably stands for complement.
    public bool DisconnectXToPCRSwitch { get; set; }
    public bool DisconnectAdvPAKSwitch { get; set; }
    public bool DisconnectBackSKSwitch { get; set; }
    public bool DisconnectWaitInitSwitch { get; set; }
    public bool ForceMcZeroSwitch { get; set; }
    public bool ForceMcOneSwitch { get; set; }

    public bool PtAmpMarginalCheckSwitch { get; set; }
    public bool Mcs0AmpMarginalCheckSwitch { get; set; }
    public bool Mcs1AmpMarginalCheckSwitch { get; set; }
    public bool MDAmpMarginalCheckSwitch { get; set; }
    public bool MTAmpMarginalCheckSwitch { get; set; }

    public bool ContReduceHeaterVoltageSwitch { get; set; }
    public bool ArithReduceHeaterVoltageSwitch { get; set; }
    public bool Mcs0ReduceHeaterVoltageSwitch { get; set; }
    public bool Mcs1ReduceHeaterVoltageSwitch { get; set; }
    public bool MTReduceHeaterVoltageSwitch { get; set; }
    public bool MDReduceHeaterVoltageSwitch { get; set; }

    //flip-flop indicators Right side Console Panel
    public ulong IOB { get; private set; }

    //magnetic tape control
    public ulong MtcTapeRegister { get; private set; }
    public ulong MtcTapeControlRegister { get; private set; }
    public ulong MtcBlockCounter { get; private set; }
    public uint MtcSprocketDelay { get; private set; }
    public uint MtcControlStop { get; private set; }
    public uint MtcLeaderDelay { get; private set; }
    public uint MtcInitialDelay { get; private set; }
    public uint MtcStartControl { get; private set; }
    public uint MtcBTK { get; private set; }
    public uint MtcWK { get; private set; }
    public uint MtcLK { get; private set; }
    public uint MtcTSK { get; private set; }
    public uint MtcWriteResume { get; private set; }
    public uint MtcMtWriteControl { get; private set; }
    public bool MtcReadWriteSync { get; private set; }
    public bool MtcTskControl { get; private set; }
    public bool MtcBlShift { get; private set; }
    public bool MtcBlEnd { get; private set; }
    public bool MtcNotReady { get; private set; }
    public uint MtcCkParityError { get; private set; }
    public uint MtcControlSyncSprocketTest { get; private set; }
    public uint MtcAlignInputRegister { get; private set; }
    public uint MtcBlockEnd { get; private set; }
    public uint MtcEnd { get; private set; }
    public uint MtcRecordEnd { get; private set; }
    public bool MtcFaultControl { get; private set; }
    public bool MtcBccError { get; private set; }
    public uint MtcBccControl { get; private set; }
    public uint MtcTrControl { get; private set; }
    public bool MtcTrControlTcrSync { get; private set; }
    public uint MtcBsk { get; private set; }
    public bool MtcReadControl { get; private set; }
    public uint MtcWrite { get; private set; }
    public uint MtcSubt { get; private set; }
    public uint MtcAdd { get; private set; }
    public uint MtcDelay { get; private set; }
    public uint MtcCenterDriveControl { get; private set; }

    // IOA section state
    public bool ExtFaultIoA1 { get; private set; }
    public bool ExtFaultIoB1 { get; private set; }
    public bool WaitIoARead { get; private set; }
    public bool WaitIoBRead { get; private set; }
    public bool WaitIoAWrite { get; private set; }
    public bool WaitIoBWrite { get; private set; }
    public bool IoaWrite { get; private set; }
    public bool IoBWrite { get; private set; }
    public bool Select { get; private set; }
    public uint IoA { get; private set; }

    // floating point section state.
    public uint FpSRegister { get; private set; }
    public uint FpDRegister { get; private set; }
    public uint FpCRegister { get; private set; }
    public bool FpNormExit { get; private set; }
    public bool FpMooMrp { get; private set; }
    public bool FpUV { get; private set; }
    public bool FpAddSubt { get; private set; }
    public bool FpMulti { get; private set; }
    public bool FpDiv { get; private set; }
    public bool FpSign { get; private set; }
    public bool FpDelayShiftA { get; private set; }
    public bool FpCharOverflow { get; private set; }
    public uint FpSequenceGates { get; private set; }

    // Console left side panel state
    //Magnetic Core Storage section. There are two core storage units, hence 2 copies of each circuit.
    public uint[] McsAddressRegister { get; private set; } = new uint[2];
    public uint[] McsPulseDistributor { get; private set; } = new uint[2];
    public bool[] McsMonInit { get; private set; } = new bool[2];
    public bool[] McsRead { get; private set; } = new bool[2];
    public bool[] McsWrite { get; private set; } = new bool[2];
    public bool[] McsWaitInit { get; private set; } = new bool[2];
    public bool[] McsReadWriteEnable { get; private set; } = new bool[2];
    public bool[] McsEnId { get; private set; } = new bool[2]; //can't properly read the label
    public bool[] McsWr0_14 { get; private set; } = new bool[2];
    public bool[] McsWr15_29 { get; private set; } = new bool[2];
    public bool[] McsWr30_35 { get; private set; } = new bool[2];

    public uint HsPunchRegister { get; private set; }
    public bool HsPunchInit { get; private set; }
    public bool HsPunchRes { get; private set; }

    public uint TypewriterRegister { get; private set; }

    // magnetic drum section is managed by the magnetic drum device.

    public ulong RunningTimeCycles { get; private set; } // as a ulong this is sufficient capacity for 584,942 years running time, at the risk of a Y2K event, I think this is enough for our project.
    public int MainPulseDistributor { get; private set; }

    private readonly Random random = new();

    public Cpu(Configuration configuration)
    {
        Console = new Console(this);
        Drum = new Drum(configuration);
    }

    public ulong Cycle(uint targetCycles)
    {
        Debug.WriteLine("Target cycles: " + targetCycles);

        for (var i = 0; i < targetCycles; i++)
        {
            if (true)
            {
                ExecuteSingleCycle();
            }

            // test the blinking lights on the console.
            if ((i % 8000) == 0)
            {
                X = (ulong)random.NextInt64(0, 688888888888);
            }

            // at the end of each cycle record which flip-flop circuits are HIGH. Essentially each flip-flop is connected to an indicator on the console. By counting for how many cycles a flip-flop is high, we can calculate the brightness of each indicator blub in each frame.
            if ((i % 2) == 0)
            {
                Console.UpdateIndicatorStatusEndOfCycle();
            }
        }

        Console.EndFrame();

        return 0;
    }

    // Executes a single 2 microsecond cycle. This method doesn't check pre-cycle conditions. Use Cycle(uint) as the entry point.
    private void ExecuteSingleCycle()
    {
        RunningTimeCycles += 2;
    }

    public void MasterClearPressed()
    {
        if (IsOperating)
        {
            return;
        }

        MainPulseDistributor = 6;
        PAK = 16384;
        A = 0;
        Q = 0;
        //X register intentionally missing. Maint manual states MASTER CLEAR effects all flip-flop except the X register for some reason.
        MCR = 0;
        VAK = 0;
        UAK = 0;
        SAR = 0;
        ExecuteMode = ExecuteMode.HighSpeed; // as per reference manual paragraph 3-8.
        RunningTimeCycles = 0;
        MasterClockCSSI = true;
        MasterClockCSSII = true;
        MasterClockCRCI = true;
        MasterClockCRCI = true;

        PdcHpc = false;
        PdcTwc = false;
        PdcWaitInternal = false;
        PdcWaitExternal = false;
        PdcWaitRsc = false;
        PdcStop = true; // pulse distribution is stopped while the clock is stopped.

        IsOperating = false;
        StopTape = false;
        SccFault = false;
        MctFault = false;
        DivFault = false;
        AZero = false;
        TapeFeed = false;

        Rsc75 = false;
        RscHoldRpt = false;
        RscJumpTerm = false;
        RscInitRpt = false;
        RscInitTest = false;
        RscEndRpt = false;
        RscDelayTest = false;
        RscAdvAdd = false;

        SccInitRead = false;
        SccInitWrite = false;
        SccInitIw0_14 = false;
        SccInitIw15_29 = false;
        SccReadQ = false;
        SccWriteAorQ = false;
        SccClearA = false;

        SctA = false;
        SctQ = false;
        SctMD = false;
        SctMcs0 = true;// set to true in the image on page 3-3. I believe this is because SAR is reset to 0 and 0 is a core memory address in MCS bank 0
        SctMcs1 = false;

        AscDelAdd = false;
        AscSpSubt = false;
        OverflowFault = false;
        AscProbeAL = false;
        AscProbeAR = false;
        AscProbeB = false;
        AscProbeC = false;
        AscProbeD = false;
        AscProbeE = false;

        InitArithSequenceA_1 = false;
        InitArithSequenceSP = false;
        InitArithSequenceAL = false;
        InitArithSequenceQL = false;
        InitArithSequenceDiv = false;
        InitArithSequenceMult = false;
        InitArithSequenceSeq = false;
        InitArithSequenceStep = false;
        InitArithSequenceCase = false;
        InitArithSequenceCkI = false;
        InitArithSequenceCkII = false;
        InitArithSequenceRestX = false;
        InitArithSequenceMultiStep = false;
        InitArithSequenceExtSeq = false;

        Halt = false;
        Interrupt = false;

        for (var i = 0; i < SelectiveJumps.Length; i++)
        {
            SelectiveJumps[i] = false;
        }

        IsFinalStopped = false;

        for (var i = 0; i < SelectiveStops.Length; i++)
        {
            SelectiveStops[i] = false;
        }

        IsManualInterruptArmed = false;

        MatrixDriveFault = false;
        MctFault = false;
        TapeFault = false;
        IOFault = false;
        VoltageFault = false;
        DivFault = false;
        SccFault = false;
        OverflowFault = false;
        PrintFault = false;
        TempFault = false;
        WaterFault = false;
        FpCharOverflow = false;

        IsForceStopped = false;


        IOB = 0;
        MtcTapeRegister = 0;
        MtcTapeControlRegister = 0;
        MtcBlockCounter = 0;
        MtcSprocketDelay = 0;
        MtcControlStop = 0;
        MtcLeaderDelay = 0;
        MtcInitialDelay = 0;
        MtcStartControl = 0;
        MtcBTK = 0;
        MtcWK = 0;
        MtcLK = 0;
        MtcTSK = 0;
        MtcWriteResume = 0;
        MtcMtWriteControl = 0;
        MtcReadWriteSync = false;
        MtcTskControl = false;
        MtcBlShift = false;
        MtcBlEnd = false;
        MtcNotReady = false;
        MtcCkParityError = 0;
        MtcControlSyncSprocketTest = 0;
        MtcAlignInputRegister = 0;
        MtcBlockEnd = 0;
        MtcRecordEnd = 0;
        MtcFaultControl = false;
        MtcBccError = false;
        MtcBccControl = 0;
        MtcTrControl = 0;
        MtcTrControlTcrSync = false;
        MtcBsk = 0;
        MtcReadControl = false;
        MtcWrite = 0;
        MtcSubt = 0;
        MtcAdd = 0;
        MtcDelay = 0;
        MtcCenterDriveControl = 0;

        ExtFaultIoA1 = false;
        ExtFaultIoB1 = false;
        WaitIoARead = false;
        WaitIoBRead = false;
        WaitIoAWrite = false;
        WaitIoBWrite = false;
        IoaWrite = false;
        IoBWrite = false;
        Select = false;
        IoA = 0;

        FpSRegister = 0;
        FpDRegister = 0;
        FpCRegister = 0;
        FpNormExit = false;
        FpMooMrp = false;
        FpUV = false;
        FpAddSubt = false;
        FpMulti = false;
        FpDiv = false;
        FpSign = false;
        FpDelayShiftA = false;
        FpSequenceGates = 0;

        McsAddressRegister[0] = 0;
        McsAddressRegister[1] = 0;
        McsPulseDistributor[0] = 0;
        McsPulseDistributor[1] = 0;
        McsMonInit[0] = false;
        McsMonInit[1] = false;
        McsRead[0] = false;
        McsRead[1] = false;
        McsWrite[0] = false;
        McsWrite[1] = false;
        McsWaitInit[0] = false;
        McsWaitInit[1] = false;
        McsReadWriteEnable[0] = false;
        McsReadWriteEnable[1] = false;
        McsWr0_14[0] = false;
        McsWr0_14[1] = false;
        McsWr15_29[0] = false;
        McsWr15_29[1] = false;
        McsWr30_35[0] = false;
        McsWr30_35[1] = false;
        HsPunchRegister = 0;
        TypewriterRegister = 0;
        HsPunchInit = false;
        HsPunchRes = false;

        Drum.Group = 4;
        Drum.Gs = 0;
        Drum.AngularIndexCounter = 0;
        Drum.InitWrite = false;
        Drum.InitWrite0_14 = false;
        Drum.InitWrite15_29 = false;
        Drum.InitRead = false;
        Drum.InitDelayedRead = false;
        Drum.ReadLockoutIII = false;
        Drum.ReadLockoutII = false;
        Drum.ReadLockoutI = false;
        Drum.ConincLockout = false;
        Drum.Preset = false;
        Drum.AdvanceAik = false;
        Drum.CpdI = false;
        Drum.CpdII = false;
    }

    public void PowerOnPressed()
    {
        MasterClearPressed();
    }
}