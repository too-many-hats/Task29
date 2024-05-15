namespace Emulator.Devices.Computer;

public class Indicator
{
    public bool HasHighAndLowLight { get; }
    public int[] CyclesLitInFrame { get; } = new int[6];
    public int FrameIndex;
    public LightType Type { get; init; }
    public bool IsEnergised { get; set; }
    public ulong LastUpdatedAt { get; set; }

    public Indicator(bool hasHighAndLowLight, LightType indicatorType = LightType.Basic)
    {
        HasHighAndLowLight = hasHighAndLowLight;
        Type = indicatorType;
    }

    public void Update(bool isEnergised, ulong currentCycle)
    {
        var timeBetweenCurrentAndLastUpdate = (currentCycle - 1) - LastUpdatedAt;
        CyclesLitInFrame[FrameIndex] += IsEnergised ? (int)timeBetweenCurrentAndLastUpdate : ((int)timeBetweenCurrentAndLastUpdate * -1);
        IsEnergised = isEnergised;
        LastUpdatedAt = currentCycle;
    }

    public void Update(uint isEnergised, ulong currentCycle)
    {
        Update(isEnergised > 0, currentCycle);
    }
}