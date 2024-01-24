using Emulator.Devices.Computer;

namespace ReplicaConsole.Windows;

public class LightOnOffTexture
{
    public nint OnTexture {  get; set; }
    public nint OffTexture {  get; set; }
    public IndicatorType IndicatorType { get; set; }

    public LightOnOffTexture(nint onTexture, nint offTexture, IndicatorType indicatorType)
    {
        OnTexture = onTexture;
        OffTexture = offTexture;
        IndicatorType = indicatorType;
    }
}