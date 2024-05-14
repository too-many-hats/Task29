using SDL2;

namespace ReplicaConsole.Windows;

public class ButtonTextures
{
    public nint ClearButton {  get; set; }
    public nint SetBlackButton { get; set;}
    public nint SetBlueButton { get; set;}
    public nint LargeButtonTexture { get; set;}
    public static SDL.SDL_Rect SourceRect = new()
    {
        x = 0,
        y = 0,
        h = 26,
        w = 26,
    };

    public static SDL.SDL_Rect LargeButtonSourceRect = new()
    {
        x = 0,
        y = 0,
        h = 26,
        w = 26,
    };

    public ButtonTextures Load(nint renderer)
    {
        var texturesDirectory = Path.Combine(AppContext.BaseDirectory, "Images");
        ClearButton = SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "clearbutton.png"));
        SetBlackButton = SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoron.png"));
        SetBlueButton = SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "indicatoron.png"));
        LargeButtonTexture = SdlHelpers.LoadTexture(renderer, Path.Combine(texturesDirectory, "clearbutton.png"));

        return this;
    }
}