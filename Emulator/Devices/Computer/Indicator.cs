namespace Emulator.Devices.Computer;

public class Indicator
{
    public bool HasHighAndLowLight { get; }

    private ulong CyclesOnSinceLastLastFrame;
    private List<uint> IntensityLastFrames = [];
    private int UpdateCounter;
    public IndicatorType Type { get; init; }

    public Indicator(bool hasHighAndLowLight, IndicatorType indicatorType = IndicatorType.Basic)
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
        UpdateCounter = UpdateCounter % 4;
    }

    public long SumIntensityRecordedFrames()
    {
        return IntensityLastFrames.Sum(x => x);
    }
}