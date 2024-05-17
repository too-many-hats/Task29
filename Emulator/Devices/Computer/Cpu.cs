using BinUtils;
using System.Diagnostics;

namespace Emulator.Devices.Computer;

public class Cpu
{
    public static uint ClockCycleMicroseconds => 2; // the machine operates at 500,000 clock cycles per second. So 2 microseconds per clock cycle. All timing in the machine is a multiple of this value.

    private uint _pak;

    public Indicators Indicators { get; private set; }

    public ulong[] Memory { get; set; } = new ulong[8192];
    public Drum Drum { get; set; }
    public Console Console { get; set; }

    //main operating registers
    // top left of the center console panel
    public ulong Q { get; set; }
    public ulong X { get; set; }
    public UInt128 A { get; set; }
    public uint MCR { get; set; } //Master Control Register (holds opcode during execution)
    public uint VAK { get; set; } // V Address Kounter 
    public uint UAK { get; set; } // U Address Kounter
    public uint SAR { get; set; } // Storage Address Register
    public uint PAK { get => _pak; set { _pak = value; Indicators.Update(Indicators.PAK, value); } } // Program Address Kounter
    public bool Halt { get; set; }
    public bool Interrupt { get; set; }

    public bool OverflowFault { get; set; }
    public bool PrintFault { get; set; }
    public bool TempFault { get; set; }
    public bool WaterFault { get; set; }
    public bool MatrixDriveFault { get; set; }
    public bool TapeFault { get; set; }
    public bool IOFault { get; set; }
    public bool VoltageFault { get; set; }

    public bool IsForceStopped { get; set; }


    // storage class control state
    public bool SccInitRead { get; set; }
    public bool SccInitWrite { get; set; }
    public bool SccInitIw0_14 { get; set; }
    public bool SccInitIw15_29 { get; set; }
    public bool SccReadQ { get; set; }
    public bool SccWriteAorQ { get; set; }
    public bool SccClearA { get; set; }

    //storage class control translator state. Managed by the CPU so other devices (console) don't need internal details of the processor.
    public bool SctA { get; set; }
    public bool SctQ { get; set; }
    public bool SctMD { get; set; }
    public bool SctMcs0 { get; set; }
    public bool SctMcs1 { get; set; }

    //arithmetic sequence control state
    public bool AscDelAdd { get; set; }
    public bool AscSpSubt { get; set; }
    public bool AscProbeAL { get; set; }
    public bool AscProbeAR { get; set; }
    public bool AscProbeB { get; set; }
    public bool AscProbeC { get; set; }
    public bool AscProbeD { get; set; }
    public bool AscProbeE { get; set; }
    public bool InitArithSequenceLog { get; set; }
    public bool InitArithSequenceA_1 { get; set; }
    public bool InitArithSequenceSP { get; set; }
    public bool InitArithSequenceAL { get; set; }
    public bool InitArithSequenceQL { get; set; }
    public bool InitArithSequenceDiv { get; set; }
    public bool InitArithSequenceMult { get; set; }
    public bool InitArithSequenceSeq { get; set; }
    public bool InitArithSequenceStep { get; set; }
    public bool InitArithSequenceCase { get; set; }
    public bool InitArithSequenceCkI { get; set; }
    public bool InitArithSequenceCkII { get; set; }
    public bool InitArithSequenceRestX { get; set; }
    public bool InitArithSequenceMultiStep { get; set; }
    public bool InitArithSequenceExtSeq { get; set; }

    //repeat sequence control state
    public bool Rsc75 { get; set; }
    public bool RscHoldRpt { get; set; }
    public bool RscJumpTerm { get; set; }
    public bool RscInitRpt { get; set; }
    public bool RscInitTest { get; set; }
    public bool RscEndRpt { get; set; }
    public bool RscDelayTest { get; set; }
    public bool RscAdvAdd { get; set; }

    //faults and tape state (center left section, center console panel)
    public bool StopTape { get; set; }
    public bool SccFault { get; set; }
    public bool MctFault { get; set; }
    public bool DivFault { get; set; }
    public bool AZero { get; set; }
    public bool TapeFeed { get; set; }

    //master clock state
    public bool MasterClockCSSI { get; set; }
    public bool MasterClockCSSII { get; set; }
    public bool MasterClockCRCI { get; set; }
    public bool MasterClockCRCII { get; set; }

    //pulse distribution control state
    public bool PdcHpc { get; set; }
    public bool PdcTwc { get; set; }
    public bool PdcWaitInternal { get; set; }
    public bool PdcWaitExternal { get; set; }
    public bool PdcWaitRsc { get; set; }
    public bool PdcStop { get; set; }

    //MPD state
    public int Mpd { get; set; }

    // center console panel lower section state.
    public ExecuteMode ExecuteMode { get; set; }
    public int OperatingRate { get; set; } = int.MaxValue; // the number of cycles executed per-second, between 5-35. Only used when in TEST mode and the START key is pressed.

    public bool[] SelectiveJumps { get; set; } = new bool[3];

    public bool IsProgramStopped { get; set; }
    public bool[] Stops { get; set; } = new bool[4];
    public bool[] SelectiveStopSelected { get; set; } = new bool[3];

    public bool IsManualInterruptArmed { get; set; }

    public bool IsNormalCondition => TypeAFault == false && TypeBFault == false && IsTestCondition == false;
    public bool IsTestCondition => ExecuteMode != ExecuteMode.HighSpeed || TestNormalSwitch == true || AbnormalNormalDrumSwitch == true;

    // as per maint manual, any switch set to up in the Test Switch Group or MD Disconnect group enables ABNORMAL CONDITION.
    public bool IsAbnormalCondition => CL_TCRDisconnectSwitch || CL_TRDisconnectSwitch || IOB_TCRDisconnectSwitch || IOB_TRDisconnectSwitch || IOB_BKDisconnectSwitch || TR_IOBDisconnectSwitch || StartDisconnectSwitch || ErrorSignalDisconnectSwitch || ReadDisconnectSwitch || WriteDisconnectSwitch || DisconnectMdWriteVoltage4Switch || DisconnectMdWriteVoltage5Switch || DisconnectMdWriteVoltage6Switch || DisconnectMdWriteVoltage7Switch || DisconnectClearASwitch || DisconnectClearXSwitch || DisconnectClearQSwitch || DisconnectClearSARSwitch || DisconnectClearPAKSwitch || DisconnectClearPCRSwitch || DisconnectInitiateWrite0_35Switch || DisconnectInitiateWrite0_14Switch || DisconnectInitiateWrite15_29Switch || F140001_00000Switch || SingleMcsSelectionSwitch || OscDrumSwitch || DisconnectStopSwitch || DisconnectSARToPAKSwitch || DisconnectVAKToSARSwitch || DisconnectQ1ToX1Switch || DisconnectXToPCRSwitch || DisconnectAdvPAKSwitch || DisconnectBackSKSwitch || DisconnectWaitInitSwitch || ForceMcZeroSwitch || ForceMcOneSwitch || PtAmpMarginalCheckSwitch || Mcs0AmpMarginalCheckSwitch || Mcs1AmpMarginalCheckSwitch || MDAmpMarginalCheckSwitch || MTAmpMarginalCheckSwitch || ContReduceHeaterVoltageSwitch || ArithReduceHeaterVoltageSwitch || Mcs0ReduceHeaterVoltageSwitch || Mcs1ReduceHeaterVoltageSwitch || MTReduceHeaterVoltageSwitch || MDReduceHeaterVoltageSwitch || DisconnectPAKToSARSwitch || TestNormalSwitch || AbnormalNormalDrumSwitch;

    public bool IsOperating { get; set; }
    public bool TypeAFault => DivFault || SccFault || OverflowFault || PrintFault || TempFault || WaterFault || FpCharOverflow;
    public bool TypeBFault => MatrixDriveFault || TapeFault || MctFault || IOFault || VoltageFault;

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
    public ulong IOB { get; set; }

    //magnetic tape control
    public ulong MtcTapeRegister { get; set; }
    public ulong MtcTapeControlRegister { get; set; }
    public ulong MtcBlockCounter { get; set; }
    public uint MtcSprocketDelay { get; set; }
    public uint MtcControlStop { get; set; }
    public uint MtcLeaderDelay { get; set; }
    public uint MtcInitialDelay { get; set; }
    public uint MtcStartControl { get; set; }
    public uint MtcBTK { get; set; }
    public uint MtcWK { get; set; }
    public uint MtcLK { get; set; }
    public uint MtcTSK { get; set; }
    public uint MtcWriteResume { get; set; }
    public uint MtcMtWriteControl { get; set; }
    public bool MtcReadWriteSync { get; set; }
    public bool MtcTskControl { get; set; }
    public bool MtcBlShift { get; set; }
    public bool MtcBlEnd { get; set; }
    public bool MtcNotReady { get; set; }
    public uint MtcCkParityError { get; set; }
    public uint MtcControlSyncSprocketTest { get; set; }
    public uint MtcAlignInputRegister { get; set; }
    public uint MtcBlockEnd { get; set; }
    public uint MtcEnd { get; set; }
    public uint MtcRecordEnd { get; set; }
    public bool MtcFaultControl { get; set; }
    public bool MtcBccError { get; set; }
    public uint MtcBccControl { get; set; }
    public uint MtcTrControl { get; set; }
    public bool MtcTrControlTcrSync { get; set; }
    public uint MtcBsk { get; set; }
    public bool MtcReadControl { get; set; }
    public uint MtcWrite { get; set; }
    public uint MtcSubt { get; set; }
    public uint MtcAdd { get; set; }
    public uint MtcDelay { get; set; }
    public uint MtcCenterDriveControl { get; set; }

    // IOA section state
    public bool ExtFaultIoA1 { get; set; }
    public bool ExtFaultIoB1 { get; set; }
    public bool WaitIoARead { get; set; }
    public bool WaitIoBRead { get; set; }
    public bool WaitIoAWrite { get; set; }
    public bool WaitIoBWrite { get; set; }
    public bool IoaWrite { get; set; }
    public bool IoBWrite { get; set; }
    public bool Select { get; set; }
    public uint IoA { get; set; }

    // floating point section state.
    public uint FpSRegister { get; set; }
    public uint FpDRegister { get; set; }
    public uint FpCRegister { get; set; }
    public bool FpNormExit { get; set; }
    public bool FpMooMrp { get; set; }
    public bool FpUV { get; set; }
    public bool FpAddSubt { get; set; }
    public bool FpMulti { get; set; }
    public bool FpDiv { get; set; }
    public bool FpSign { get; set; }
    public bool FpDelayShiftA { get; set; }
    public bool FpCharOverflow { get; set; }
    public uint FpSequenceGates { get; set; }

    // Console left side panel state
    //Magnetic Core Storage section. There are two core storage units, hence 2 copies of each circuit.
    public uint[] McsAddressRegister { get; set; } = new uint[2];
    public uint[] McsPulseDistributor { get; set; } = new uint[2];
    public bool[] McsMonInit { get; set; } = new bool[2];
    public bool[] McsRead { get; set; } = new bool[2];
    public bool[] McsWrite { get; set; } = new bool[2];
    public bool[] McsWaitInit { get; set; } = new bool[2];
    public bool[] McsReadWriteEnable { get; set; } = new bool[2];
    public bool[] McsEnableInhibitDisturb { get; set; } = new bool[2]; //can't properly read the label
    public bool[] McsWr0_14 { get; set; } = new bool[2];
    public bool[] McsWr15_29 { get; set; } = new bool[2];
    public bool[] McsWr30_35 { get; set; } = new bool[2];
    public bool[] McsHoldWaitInitNextCycle { get; set; } = new bool[2];

    public uint HsPunchRegister { get; set; }
    public bool HsPunchInit { get; set; }
    public bool HsPunchRes { get; set; }

    public uint TypewriterRegister { get; set; }

    // magnetic drum section is managed by the magnetic drum device.

    public ulong RunningTimeCycles { get; set; }

    /// <summary>
    /// Tracks real-world time elapsed since the emulator began to cycle, used
    /// for timing IO devices, indicators, and anything else which has events outside the CPU clock. 
    /// </summary>
    public ulong WorldClock { get; set; } // as a ulong this is sufficient capacity for 584,942 years running time, at the risk of a Y2K event, I think this is enough for our project.

    public ulong Delay { get; set; } // Number of cycles to wait for machine generated waits.
    public int MainPulseDistributor { get; set; }
    private bool CanExecuteNextCycle =>
        TypeAFault == false && TypeBFault == false && IsForceStopped == false && IsProgramStopped == false && Stops.All(x => x == false)
        && (IsAbnormalCondition == false || IsAbnormalCondition && IsTestCondition); // an abnormal condition if the machine is not in test mode stops execution. (maint manual page 50)

    private bool StartPressedInTestMode;

    // The 1103A commands. These are named to match the Timing Sequences manual and Maint manual as closely as possible.
    private readonly Command ClearX;
    private readonly Command ClearPCR;
    private readonly Command SARtoF3;
    private readonly Command PAKtoSAR;
    private readonly Command AdvancePAK;
    private readonly Command ClearSAR;
    private readonly Command XtoPCR;
    private readonly Command InitiateRead_WaitInternalReference;

    private readonly List<Command> Commands;

    public Cpu(Configuration configuration)
    {
        Console = new Console(this);
        Drum = new Drum(configuration);
        Indicators = new Indicators(this);

        // command implementations.
        ClearX = new Command((command) =>
        {
            X = 0;

            command.Complete();
        });

        ClearPCR = new Command((command) =>
        {
            VAK = 0;
            UAK = 0;
            MCR = 0;

            command.Complete();
        });

        SARtoF3 = new Command((command) =>
        {
            throw new NotImplementedException();
        });

        PAKtoSAR = new Command((command) =>
        {
            SetSAR(PAK);

            command.Complete();
        });

        AdvancePAK = new Command((command) =>
        {
            PAK++;

            command.Complete();
        });

        ClearSAR = new Command((command) =>
        {
            SetSAR(0);

            command.Complete();
        });

        XtoPCR = new Command((command) =>
        {
            MCR = (uint)(X >> 30 & Constants.SixBitMask);
            UAK = (uint)(X >> 15 & Constants.AddressMask);
            VAK = (uint)(X & Constants.AddressMask);

            command.Complete();
        });

        InitiateRead_WaitInternalReference = new Command((command) =>
        {
            if (SccInitRead == false && PdcWaitInternal == false) // first cycle just sets init read.
            {
                SccInitRead = true;
                PdcWaitInternal = true;

                if (SctMcs0 == true || SctMcs1 == true)
                {
                    var coreBank = SctMcs0 == true ? 0 : 1;

                    // MC address registers are 12 bit, and wrap around when a larger address is given. See reference manual page 1-6.
                    McsAddressRegister[coreBank] = SAR & 0b111_111_111_111;
                }

                return;
            }

            if (SctMcs0 == true || SctMcs1 == true)
            {
                // see page 66 timing manual for details of the below logic.
                var coreBank = SctMcs0 == true ? 0 : 1;
                var executeAllInAutomatic = ExecuteMode == ExecuteMode.Clock || ExecuteMode == ExecuteMode.AutomaticStepClock;

                if (McsWaitInit[coreBank] == true && McsPulseDistributor[coreBank] == 0)
                {
                    return; // 1 cycle delay because we've tried to start a core reference back-to-back with the last reference without the required 1 cycle wait. See timing manual page 70.
                }

                SccInitRead = false; // timing manual, page 61. By this point the read has been initialised and we are executing. PDC continues to stop the CPU clock until the read is completed. This must occur after the WaitInit lockout check above.

                if (executeAllInAutomatic) // skip straight to the end state when executing in automatic.
                {
                    PdcWaitInternal = false;
                    McsHoldWaitInitNextCycle[coreBank] = true;
                    McsWaitInit[coreBank] = true; //one cycle lockout for core memory references.
                    X = Memory[McsAddressRegister[coreBank] + (coreBank * 4096)];
                    RunningTimeCycles += 3; // reads take four cycles, one is already accounted for at the start of ExecuteSingleCycle which invoked this command.
                    return;
                }

                if (McsWaitInit[coreBank] == false) // MCP-0
                {
                    McsWaitInit[coreBank] = true;
                    McsReadWriteEnable[coreBank] = true;
                    McsPulseDistributor[coreBank] = 1;
                    return;
                }
                else if (McsMonInit[coreBank] == false) // MCP-1
                {
                    McsRead[coreBank] = true;
                    McsMonInit[coreBank] = true;
                    McsPulseDistributor[coreBank] = 2;
                    X = Memory[McsAddressRegister[coreBank] + (coreBank * 4096)];
                    return;
                }
                else if (McsRead[coreBank] == true) // MCP-2
                {
                    McsRead[coreBank] = false;
                    McsPulseDistributor[coreBank] = 3;
                    McsEnableInhibitDisturb[coreBank] = true;

                    // MCP-3. Magnetic Core Pulse 3 is merged into MCP-2. This is not realistic, however the core clock is faster than the CPU clock, there are 5 core cycles in the space of 4 CPU cycles. The complexity involved for just one minor indicator isn't worth it.
                    McsPulseDistributor[coreBank] = 4;
                    McsWrite[coreBank] = true;
                }
                //else if (McsEnId[coreBank] == true) // MCP-3
                //{
                //    McsPulseDistributor[coreBank] = 4;
                //    McsWrite[coreBank] = true;
                //}
                else if (McsWrite[coreBank] == true) // MCP-4
                {
                    McsPulseDistributor[coreBank] = 0;
                    McsWrite[coreBank] = false;
                    McsMonInit[coreBank] = false;
                    McsEnableInhibitDisturb[coreBank] = false;
                    McsReadWriteEnable[coreBank] = false;
                    PdcWaitInternal = false;
                    McsHoldWaitInitNextCycle[coreBank] = true;
                }
            }
        });

        Commands = [ClearX, ClearPCR, SARtoF3, PAKtoSAR, AdvancePAK, ClearSAR, XtoPCR, InitiateRead_WaitInternalReference];
    }

    public ulong Cycle(uint targetCycles)
    {
        Cycle(targetCycles, false);

        return 0;
    }

    private ulong Cycle(uint targetCycles, bool step)
    {
        Debug.WriteLine("Target cycles: " + targetCycles);

        Indicators.StartBrightnessTrackInFrame((int)targetCycles);

        for (var i = 0; i < targetCycles; i++)
        {
            if (CanExecuteNextCycle == false) // a fault or software stop means the CPU cannot execute this cycle, no matter what.
            {
                IsOperating = false;
                // TODO: This needs to set clock release flip-flop properly.
            }

            if (IsTestCondition == false)
            {
                StartPressedInTestMode = false;
            }

            if (IsOperating)
            {
                ExecuteSingleCycle();
            }

            // core memory has a 1 cycle lockout after the last magnetic core pulse. Clear the lockout once we no longer need to hold it. Core memory has a separate clock from the CPU, which is why this code is separate from the CPU cycle execution.
            for (var j = 0; j < McsWaitInit.Length; j++)
            {
                if (McsPulseDistributor[j] == 0)
                {
                    if (McsHoldWaitInitNextCycle[j] == true)
                    {
                        McsHoldWaitInitNextCycle[j] = false;
                    }
                    else
                    {
                        McsWaitInit[j] = false;
                    }
                }
            }

            WorldClock++;
            
            // update the IO devices.
        }

        Indicators.UpdateStatusEndOfCycle();

        return 0;
    }

    // Executes a single 2 microsecond cycle. This method doesn't check pre-cycle conditions. Use Cycle(uint) as the entry point.
    private void ExecuteSingleCycle()
    {
        RunningTimeCycles += 1;

        if (MainPulseDistributor == 0)
        {
            if (MCR == Instructions.ProgramStop.Opcode)
            {
                ClearX.Execute();
                IsProgramStopped = true;
                IsOperating = false;
            }
            else
            {
                MctFault = true;
                return;
            }
        }
        else if (MainPulseDistributor == 6)
        {
            ClearPCR.Execute();

            if (Interrupt)
            {
                SARtoF3.Execute();
            }
            else
            {
                PAKtoSAR.Execute();
                AdvancePAK.Execute();
            }

            ClearX.Execute();
            InitiateRead_WaitInternalReference.Execute();

            // special case when fetching an instruction from A or an unassigned address. An SCC fault occurs, MPD is 7 and "no indication is given of the faulty instruction" (reference manual paragraph 3-43). The manual does not say exactly how this condition is determined or what MP it occurs in. That the MP is 7 and SAR is cleared makes me think that the read occurs normally on MP6 and the error is detected in MP7. But this doesn't match the behaviour of a normal read.
            if (SctA || SccFault)
            {
                SccFault = true;
                SetSAR(0);
                SetNextMainPulse(7);
                PdcWaitInternal = false;
                SccInitRead = false;
                return;
            }

            if (PdcWaitInternal == false) // the read has completed so we can advance.
            {
                SetNextMainPulse(7);
            }

            if (ExecuteMode == ExecuteMode.Operation)
            {
                IsOperating = false;
            }
        }
        else if (MainPulseDistributor == 7)
        {
            if (ClearSAR.IsComplete) // in the first cycle of MP7, the ClearSAR command is incomplete, so the main pulse does not advance, but the commands are executed. In the second cycle of MP7, ClearSAR is complete and we advance the main pulse. This ensures MP7 has a delay after the first cycle. Timing manual page 11 describes the delay.
            {
                SetNextMainPulse(0);
            }

            ClearSAR.Execute();
            XtoPCR.Execute();
        }

        if (ExecuteMode == ExecuteMode.Clock)
        {
            IsOperating = false;
        }
    }

    public void MasterClearPressed()
    {
        if (IsOperating)
        {
            return;
        }

        SetNextMainPulse(6);
        PAK = 16384;
        A = 0;
        Q = 0;
        //X register intentionally missing. Maint manual states MASTER CLEAR effects all flip-flop except the X register for some reason.
        MCR = 0;
        VAK = 0;
        UAK = 0;
        SetSAR(0);
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

        IsProgramStopped = false;

        for (var i = 0; i < Stops.Length; i++)
        {
            Stops[i] = false;
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

        StartPressedInTestMode = false;
        
        for (int i = 0; i < SelectiveStopSelected.Length; i++)
        {
            SelectiveStopSelected[i] = false;
        }
    }

    private void SetSAR(uint address)
    {
        SAR = address; // reference manual (paragraph 1-23) states an address is loaded into SAR, even in the case of a SCC fault. Because Storage Class Control receives its input from SAR in order to determine if a fault is required.

        SctMcs0 = false;
        SctMcs1 = false;
        SctQ = false;
        SctA = false;
        SctMD = false;

        if (SAR >= 0 && SAR <= 4095)
        {
            SctMcs0 = true;
            return;
        }
        else if (SAR >= 4096 && SAR <= 8191)
        {
            SctMcs1 = true;
            return;
        }
        else if (SAR >= 8192 && SAR <= 12287) // referencing non-existent memory bank. Wraps around to zero. See reference manual paragraph 1-21.
        {
            SctMcs0 = true;
            return;
        }
        else if (SAR >= 12288 && SAR <= 12799) // these addresses are "Unassigned" and SCC fault (timing manual pages 60-63)
        {
            SccFault = true;
            return;
        }
        else if (SAR >= 12800 && SAR <= 13311)
        {
            SctQ = true;
            return;
        }
        else if (SAR >= 13312 && SAR <= 16383)
        {
            SctA = true;
            return;
        }
        else if (SAR >= 16384 && SAR <= 32767)
        {
            SctMD = true;
            return;
        }
    }

    public void SetNextMainPulse(int nextMainPulse)
    {
        MainPulseDistributor = nextMainPulse;

        // with the main pulse completed, reset each command for the next main pulse.
        foreach (var command in Commands)
        {
            command.IsComplete = false;
        }

        if (ExecuteMode == ExecuteMode.Distributor)
        {
            IsOperating = false;
        }
    }

    public void PowerOnPressed()
    {
        MasterClearPressed();
    }

    public void ClearPAK()
    {
        if (IsOperating) // cannot be changed, even while running in TEST mode, reference manual paragraph 3-18.
        {
            return;
        }

        PAK = 0;
    }

    public void SetAto(UInt128 value)
    {
        if (IsOperating == false || IsTestCondition) // can be changed while operating in test mode, reference manual paragraph 3-18.
        {
            A = value & Constants.WordMask;
        }
    }

    public void SetQto(ulong value)
    {
        if (IsOperating == false || IsTestCondition) // can be changed while operating in test mode, reference manual paragraph 3-18.
        {
            Q = value & Constants.WordMask;
        }
    }

    public void SetXto(ulong value)
    {
        if (IsOperating == false || IsTestCondition)
        {
            X = value & Constants.WordMask;
        }
    }

    public void SetPAKto(uint value)
    {
        if (IsOperating) // cannot be changed, even while running in TEST mode, reference manual paragraph 3-18.
        {
            return;
        }

        PAK = value & (uint)Constants.AddressMask;
    }

    public void SetSARto(uint value)
    {
        SetSAR(value);
    }

    public void SetMCRto(uint value)
    {
        if (IsOperating == false || IsTestCondition) // can be changed while operating in test mode, reference manual paragraph 3-18.
        {
            MCR = value & (uint)Constants.SixBitMask;
        }
    }

    public void StartPressed()
    {
        IsForceStopped = false;

        if (ExecuteMode != ExecuteMode.HighSpeed)
        {
            StartPressedInTestMode = true;
        }
        else // else in normal mode.
        {
            IsOperating = true; // IsOperating may immediately be set back to false before the next cycle if a fault or stop is preventing execution.
        }
    }

    public void StepPressed()
    {
        if (IsTestCondition && ExecuteMode != ExecuteMode.HighSpeed && StartPressedInTestMode)
        {
            IsOperating = true; // IsOperating may immediately be set back to false before the next cycle if a fault or stop is preventing execution.
        }
    }

    public void ForceStopPressed()
    {
        IsForceStopped = true;
    }

    public void SetUAKto(uint value)
    {
        if (IsOperating == false || IsTestCondition) // can be changed while operating in test mode, reference manual paragraph 3-18.
        {
            UAK = value & (uint)Constants.AddressMask;
        }
    }

    public void SetVAKto(uint value)
    {
        if (IsOperating == false || IsTestCondition) // can be changed while operating in test mode, reference manual paragraph 3-18.
        {
            VAK = value & (uint)Constants.AddressMask;
        }
    }

    public void SetExecuteMode(ExecuteMode executeMode)
    {
        if (IsOperating) // cannot be changed, even while running in TEST mode, reference manual paragraph 3-18.
        {
            return;
        }

        ExecuteMode = executeMode;
    }

    public void SelectSelectiveJumpPressed(uint jumpNumber)
    {
        if (IsOperating) // cannot be changed, even while running in TEST mode, reference manual paragraph 3-18.
        {
            return;
        }

        if (jumpNumber > 3 || jumpNumber < 1)
        {
            throw new Exception("Jump number must be between 1 and 3");
        }

        SelectiveJumps[--jumpNumber] = true; //-- to convert to 0 based array index.
    }

    public void ReleaseSelectiveJumpPressed(uint jumpNumber)
    {
        if (IsOperating) // cannot be changed, even while running in TEST mode, reference manual paragraph 3-18.
        {
            return;
        }

        if (jumpNumber > 3 || jumpNumber < 1)
        {
            throw new Exception("Jump number must be between 1 and 3");
        }

        SelectiveJumps[--jumpNumber] = false; //-- to convert to 0 based array index.
    }

    public void SelectSelectiveStopPressed(uint stopNumber)
    { 
        // selective stops can be changed while operating, reference manual paragraph 3-22.

        if (stopNumber > 3 || stopNumber < 1)
        {
            throw new Exception("Stop number must be between 1 and 3");
        }

        SelectiveStopSelected[--stopNumber] = true;
    }

    public void ReleaseSelectiveStopPressed(uint stopNumber)
    {
        // selective stops can be changed while operating, reference manual paragraph 3-22.

        if (stopNumber > 3 || stopNumber < 1)
        {
            throw new Exception("Stop number must be between 1 and 3");
        }

        SelectiveStopSelected[--stopNumber] = false;
    }

    public void ClearAFaultPressed()
    {
        if (IsOperating && IsNormalCondition)
        {
            return;
        }

        DivFault = false;
        SccFault = false;
        OverflowFault = false;
        FpCharOverflow = false;
        TempFault = false;
        WaterFault = false;
        PrintFault = false;
    }
}