namespace Emulator.Devices.Computer;

public class Indicator
{
    public int OnIntensity { get; set; }
    public int OffIntensity { get; set; }
    public bool HasHighAndLowLight { get; }

    private ulong CyclesOnSinceLastLastFrame;
    private List<uint> IntensityLastFrames = new();
    private int UpdateCounter;

    public Indicator(bool hasHighAndLowLight)
    {
        HasHighAndLowLight = hasHighAndLowLight;
        IntensityLastFrames = Enumerable.Range(0, 4).Select(_ => (uint)0).ToList();
    }

    public void Update(ulong isOn)
    {
        CyclesOnSinceLastLastFrame += isOn;
    }

    public void EndFrame()
    {
        IntensityLastFrames[UpdateCounter++] = (uint)CyclesOnSinceLastLastFrame;
        CyclesOnSinceLastLastFrame = 0;
        UpdateCounter = UpdateCounter % 4;
    }

    public long SumIntensityRecordedFrames()
    {
        return IntensityLastFrames.Sum(x => x);
    }
}