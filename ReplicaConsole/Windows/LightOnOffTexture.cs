using Emulator.Devices.Computer;

namespace ReplicaConsole.Windows;

public class LightOnOffTexture
{
    public nint OnTexture {  get; set; }
    public nint OffTexture {  get; set; }
    public LightType LightType { get; set; }

    public LightOnOffTexture(nint onTexture, nint offTexture, LightType lightType)
    {
        OnTexture = onTexture;
        OffTexture = offTexture;
        LightType = lightType;
    }
}