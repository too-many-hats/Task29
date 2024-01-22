
namespace Emulator.Devices.Computer;

public class Console
{
    public Indicator[] AIndicators { get; } = Enumerable.Range(0, 72).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] QIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] XIndicators { get; } = Enumerable.Range(0, 36).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] MCRIndicators { get; } = Enumerable.Range(0, 6).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] VAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] UAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] PAKIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();
    public Indicator[] SARIndicators { get; } = Enumerable.Range(0, 15).Select(_ => new Indicator(true)).ToArray();

    internal void EndFrame()
    {
        EndFramesOfAll(AIndicators);
        EndFramesOfAll(QIndicators);
        EndFramesOfAll(XIndicators);
        EndFramesOfAll(MCRIndicators);
        EndFramesOfAll(VAKIndicators);
        EndFramesOfAll(UAKIndicators);
        EndFramesOfAll(PAKIndicators);
        EndFramesOfAll(SARIndicators);
    }

    private static void EndFramesOfAll(Indicator[] indicators)
    {
        foreach (var indicator in indicators)
        {
            indicator.EndFrame();
        }
    }

    internal void UpdateBinaryIndicator(Indicator[] indicators, UInt128 value)
    {
        UInt128 mask = 1;
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update((value & mask) > 0 ? (ulong)1 : 0);
            mask = mask << 1;
        }
    }

    internal void UpdateBinaryIndicator(Indicator[] indicators, ulong value)
    {
        ulong mask = 1;
        for (var j = 0; j < indicators.Length; j++)
        {
            indicators[j].Update((value & mask) >> j);
            mask = mask << 1;
        }
    }
}