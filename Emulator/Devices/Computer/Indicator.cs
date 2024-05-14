namespace Emulator.Devices.Computer;

public class Indicator
{
    public bool HasHighAndLowLight { get; }
    public int[] CyclesLitInFrame { get; } = new int[6];
    public int FrameIndex;
    public LightType Type { get; init; }

    public Indicator(bool hasHighAndLowLight, LightType indicatorType = LightType.Basic)
    {
        HasHighAndLowLight = hasHighAndLowLight;
        Type = indicatorType;
    }

    public void Update(int energisedFor)
    {
        CyclesLitInFrame[FrameIndex] += energisedFor;
    }
}