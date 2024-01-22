namespace Emulator.Devices.Computer;

public class Cpu
{
    public static uint ClockCycleMicroseconds => 2; // the machine operates at 500,000 clock cycles per second. So 2 microseconds per clock cycle. All timing in the machine is a multiple of this value.

    public ulong[] Memory { get; private set; } = new ulong[12288];
    public Drum Drum { get; private set; }
    public Console Console { get; private set; }

    //main operating registers
    public ulong Q { get; private set; }
    public ulong X { get; private set; }
    public UInt128 A { get; private set; }
    public uint MCR { get; private set; } //Master Control Register (holds opcode during execution)
    public uint VAK { get; private set; } // V Address Kounter 
    public uint UAK { get; private set; } // U Address Kounter
    public uint SAR { get; private set; } // Storage Address Register
    public uint PAK { get; private set; } // Program Address Kounter
    public uint IOA { get; private set; }
    public ulong IOB { get; private set; }
    public bool IsOperating { get; private set; }

    public ExecuteMode ExecuteMode { get; private set; }
    public int OperatingRate { get; private set; } = int.MaxValue; // the number of cycles executed per-second, between 5-35. Only used when in TEST mode and the START key is pressed.
    public ulong RunningTimeMicroseconds { get; private set; } // as a ulong this is sufficient capacity for 584,942 years running time, at the risk of a Y2K event, I think this is enough for our project.
    public int MainPulseDistributor { get; private set; }

    public ulong Cycle(uint targetCycles)
    {
        return 0;
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
        IOA = 0;
        IOB = 0;
        ExecuteMode = ExecuteMode.HighSpeed;
        RunningTimeMicroseconds = 0;
    }
}