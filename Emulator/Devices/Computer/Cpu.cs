using System.Diagnostics;

namespace Emulator.Devices.Computer;

public class Cpu
{
    public static uint ClockCycleMicroseconds => 2; // the machine operates at 500,000 clock cycles per second. So 2 microseconds per clock cycle. All timing in the machine is a multiple of this value.

    public ulong[] Memory { get; private set; } = new ulong[12288];
    public Drum Drum { get; private set; } = new Drum();
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


    // storage class control state
    public bool SccInitRead { get; private set; }
    public bool SccInitWrite { get; private set; }
    public bool SccInitIw0_14 { get; private set; }
    public bool SccInitIw15_29 { get; private set; }
    public bool SccReadQ { get; private set; }
    public bool SccWriteAorQ { get; private set; }
    public bool SccClearA { get; private set; }

    //storage class control translator state. Managed by the CPU so other devices (console) don't need internal details of the processor.
    public bool SctA { get; private set; } = true;
    public bool SctQ { get; private set; } = true;
    public bool SctMD { get; private set; } = true;
    public bool SctMcs0 { get; private set; } = true;
    public bool SctMcs1 { get; private set; } = true;
    public bool SctMcs2 { get; private set; } = true;

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
    public bool[] SelectiveStops { get; private set; } = new bool[3];

    public bool IsManualInterruptArmed { get; private set; }

    public bool IsNormalCondition { get; private set; }
    public bool IsTestCondition { get; private set; }
    public bool IsAbnormalCondition { get; private set; }
    public bool IsOperating { get; private set; }
    public bool TypeAFault => DivFault | SccFault | OverflowFault | PrintFault | TempFault | WaterFault | FpCharOverflow;
    public bool TypeBFault => MatrixDriveFault | TapeFault | MctFault | IOFault | VoltageFault;

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
    public uint MtcSTK { get; private set; }
    public uint MtcWK { get; private set; }
    public uint MtcLK { get; private set; }
    public uint MtcTSK { get; private set; }
    public uint MtcWriteResume { get; private set; }
    public uint MtcMtWriteControl { get; private set; }
    public uint MtcReadWriteSync { get; private set; }
    public uint MtcTskControl { get; private set; }
    public uint MtcBlShift { get; private set; }
    public uint MtcBlEnd { get; private set; }
    public uint MtcNotReady { get; private set; }
    public uint MtcCkParityError { get; private set; }
    public uint MtcControlSyncSprocketTest { get; private set; }
    public uint MtcAlignInputRegister { get; private set; }
    public uint MtcBlock { get; private set; }
    public uint MtcEnd { get; private set; }
    public uint MtcRecordEnd { get; private set; }
    public uint MtcFaultControl { get; private set; }
    public uint MtcBccError { get; private set; }
    public uint MtcBccControl { get; private set; }
    public uint MtcTrControl { get; private set; }
    public uint MtcTrControlTcrSync { get; private set; }
    public uint MtcBsk { get; private set; }
    public uint MtcRealControl { get; private set; }
    public uint MtcWrite { get; private set; }
    public uint MtcSubt { get; private set; }
    public uint MtcAdd { get; private set; }
    public uint MtcDelay { get; private set; }
    public uint MtcCenterDriveControl { get; private set; }

    // IOA section state
    public bool ExtFaultIoA1 { get; private set; }
    public bool ExtFaultIoA2 { get; private set; }
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
    //Magnetic Core Storage section. There are 3 core storage units, hence 3 copies of each circuit.
    public uint[] McsAddressRegister { get; private set; } = new uint[3];
    public uint[] McsPulseDistributor { get; private set; } = new uint[3];
    public bool[] McsMonInit { get; private set; } = new bool[3];
    public bool[] McsRead { get; private set; } = new bool[3];
    public bool[] McsWrite { get; private set; } = new bool[3];
    public bool[] McsWaitInit { get; private set; } = new bool[3];
    public bool[] McsReadWriteEnable { get; private set; } = new bool[3];
    public bool[] McsEnId { get; private set; } = new bool[3]; //can't properly read the label
    public bool[] McsWr0_14 { get; private set; } = new bool[3];
    public bool[] McsWr15_29 { get; private set; } = new bool[3];
    public bool[] McsWr30_35 { get; private set; } = new bool[3];

    public uint HsPunchRegister { get; private set; }
    public uint HsPunchInit { get; private set; }
    public uint HsPunchRes { get; private set; }

    public uint TypewriterRegister { get; private set; }

    // magnetic drum section is managed by the magnetic drum device.

    public ulong RunningTimeCycles { get; private set; } // as a ulong this is sufficient capacity for 584,942 years running time, at the risk of a Y2K event, I think this is enough for our project.
    public int MainPulseDistributor { get; private set; }

    private Random random = new Random();

    public Cpu()
    {
        Console = new Console(this);
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

            if ((i % 6) == 0)
            {
                X = (ulong)random.NextInt64(0, 688888888888);
            }

            // at the end of each cycle record which flip-flop circuits are HIGH. Essentially each flip-flop is connected to an indicator on the console. By counting for how many cycles a flip-flop is high, we can calculate the brightness of each indicator blub in each frame.
            Console.UpdateIndicatorStatusEndOfCycle();
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
        MCR = 0;
        VAK = 0;
        UAK = 0;
        SAR = 0;
        IoA = 0;
        IOB = 0;
        ExecuteMode = ExecuteMode.HighSpeed;
        RunningTimeCycles = 0;
        MasterClockCSSI = true;
        MasterClockCSSII = true;
        MasterClockCRCI = true;
        MasterClockCRCI = true;
    }

    public void PowerOnPressed()
    {
        MasterClearPressed();
    }
}