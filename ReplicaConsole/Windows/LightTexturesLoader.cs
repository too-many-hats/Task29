using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public class LightTexturesLoader
{
    public static List<LightOnOffTexture> Load(nint renderer)
    {
        var lightTextures = new List<LightOnOffTexture>
        {
            new(
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\indicatoron.png"),
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\indicatoroff.png"),
                IndicatorType.Basic),
            new(
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\whitelighton.png"),
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\whitelightoff.png"),
                IndicatorType.WhiteLight),
            new(
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\redlighton.png"),
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\redlightoff.png"),
                IndicatorType.RedLight),
            new(
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\greenlighton.png"),
                SDL_image.IMG_LoadTexture(renderer, AppContext.BaseDirectory + @"\Images\greenlightoff.png"),
                IndicatorType.GreenLight),
        };

        foreach (var texture in lightTextures)
        {
            if (texture.OffTexture == 0 || texture.OnTexture == 0)
            {
                throw new Exception("Texture failed to load");
            }
        }

        return lightTextures;
    }
}