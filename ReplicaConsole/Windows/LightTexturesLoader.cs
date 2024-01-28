using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public class LightTexturesLoader
{
    public static List<LightOnOffTexture> Load(nint renderer)
    {
        var texturesDirectory = Path.Combine(AppContext.BaseDirectory, "Images");
        var lightTextures = new List<LightOnOffTexture>
        {
            new(
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoron.png")),
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoroff.png")),
                LightType.Basic),
            new(
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "whitelighton.png")),
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "whitelightoff.png")),
                LightType.White),
            new(
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "redlighton.png")),
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "redlightoff.png")),
                LightType.Red),
            new(
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "greenlighton.png")),
                SDL_image.IMG_LoadTexture(renderer, Path.Combine(texturesDirectory, "greenlightoff.png")),
                LightType.Green),
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