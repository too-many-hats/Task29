using Emulator.Devices.Computer;

namespace ReplicaConsole.Windows;

public class LightTexturesLoader
{
    public static List<LightOnOffTexture> Load(nint renderer)
    {
        var texturesDirectory = Path.Combine(AppContext.BaseDirectory, "Images");
        var lightTextures = new List<LightOnOffTexture>
        {
            new(
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoron.png")),
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoroff.png")),
                LightType.Basic),
            new(
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "whitelighton.png")),
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "whitelightoff.png")),
                LightType.White),
            new(
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "redlighton.png")),
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "redlightoff.png")),
                LightType.Red),
            new(
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "greenlighton.png")),
                SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "greenlightoff.png")),
                LightType.Green),
        };

        return lightTextures;
    }
}