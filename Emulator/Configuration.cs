namespace Emulator;

public class Configuration
{
    public bool SimulateActualTime { get; set; } = false;

    // factor to scale all windows and their content by. 1.0 is life-size. 0.5 is half of life size, etc.
    public double UiScaleFactor { get; set; } = 0.5;
}