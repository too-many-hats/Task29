using Emulator.Devices.Computer;

namespace Emulator;

public class Configuration
{
    /// <summary>
    /// If true, slows the emulator down to match actual execution time of a real 1103. Not recommended for testing.
    /// </summary>
    public bool SimulateActualTime { get; set; } = false;

    /// <summary>
    /// The factor to scale the console and device UI by. 1.0 is life-size and very large. 0.5 is half of life size and so on. Cannot be less than 0.1.
    /// </summary>
    public double UiScaleFactor { get; set; } = 0.1;

    /// <summary>
    /// The drum address interlace setting, must be either 4, 8, 16, 32 or 64. See reference manual for details. Just like real machine, interlace selection is hardware circuitry and cannot be changed while the machine is running.
    /// </summary>
    public DrumInterlace DrumInterlace { get; set; } = DrumInterlace.Addresses16;
}