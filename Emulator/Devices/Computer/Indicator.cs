namespace Emulator.Devices.Computer;

public class Indicator
{
    public int OnIntensity { get; set; }
    public int OffIntensity { get; set; }
    public bool HasHighAndLowLight { get; }

    public Indicator(bool hasHighAndLowLight)
    {
        HasHighAndLowLight = hasHighAndLowLight;
    }

    public void Update(ulong onForCycles)
    {

    }
}