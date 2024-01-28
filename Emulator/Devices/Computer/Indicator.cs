namespace Emulator.Devices.Computer;

public class Indicator
{
    public bool HasHighAndLowLight { get; }

    private ulong CyclesOnSinceLastLastFrame;
    private List<uint> IntensityLastFrames = [];
    private int UpdateCounter;
    public LightType Type { get; init; }

    public Indicator(bool hasHighAndLowLight, LightType indicatorType = LightType.Basic)
    {
        HasHighAndLowLight = hasHighAndLowLight;
        IntensityLastFrames = Enumerable.Range(0, 4).Select(_ => (uint)0).ToList();
        Type = indicatorType;
    }

    public void Update(ulong isOn)
    {
        CyclesOnSinceLastLastFrame += isOn;
    }

    public void EndFrame()
    {
        IntensityLastFrames[UpdateCounter++] = (uint)CyclesOnSinceLastLastFrame;
        CyclesOnSinceLastLastFrame = 0;
        UpdateCounter = UpdateCounter % 4; // this modulo step is separate from the increment above to prevent integer wrap-around. This method is called many times per second.
    }

    public long SumIntensityRecordedFrames()
    {
        return IntensityLastFrames.Sum(x => x);
    }
}