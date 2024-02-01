using SDL2;

namespace ReplicaConsole.Windows;

public class LargeButton
{
    public SDL.SDL_Rect Hitbox { get; }
    public Action OnClick { get; }

    public LargeButton(SDL.SDL_Rect hitbox, Action onClick)
    {
        Hitbox = hitbox;
        OnClick = onClick;
    }
}