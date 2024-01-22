namespace Emulator.Devices.Computer;

public class Cpu
{
    public static uint ClockCycleMicroseconds => 2; // the machine operates at 500,000 clock cycles per second. So 2 microseconds per clock cycle. All timing in the machine is a multiple of this value.

    public ulong[] Memory { get; private set; } = new ulong[12288];
    public Drum Drum { get; private set; } = new Drum();
    public Console Console { get; private set; } = new Console();

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

    private Random random = new Random();

    public ulong Cycle(uint targetCycles)
    {
        for (var i = 0; i < targetCycles; i++)
        {
            if (true)
            {
                ExecuteSingleCycle();
            }

            if ((i % 18) == 0)
            {
                X = (ulong)random.NextInt64(0, 688888888888);
            }

            // at the end of each cycle record which flip-flop circuits are HIGH. Essentially each flip-flop is connected to an indicator on the console. By counting for how many cycles a flip-flop is high, we can calculate the brightness of each indicator blub in each frame.
            Console.UpdateBinaryIndicator(Console.AIndicators, A);
            Console.UpdateBinaryIndicator(Console.QIndicators, Q);
            Console.UpdateBinaryIndicator(Console.XIndicators, X);
            Console.UpdateBinaryIndicator(Console.MCRIndicators, MCR);
            Console.UpdateBinaryIndicator(Console.UAKIndicators, UAK);
            Console.UpdateBinaryIndicator(Console.VAKIndicators, VAK);
            Console.UpdateBinaryIndicator(Console.PAKIndicators, PAK);
            Console.UpdateBinaryIndicator(Console.SARIndicators, SAR);
        }

        Console.EndFrame();

        return 0;
    }

    // Executes a single 2 microsecond cycle. This method doesn't check pre-cycle conditions. Use Cycle(uint) as the entry point.
    private void ExecuteSingleCycle()
    {
        RunningTimeMicroseconds += 2;
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