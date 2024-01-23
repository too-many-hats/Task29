using Emulator;
using SDL2;

namespace ReplicaConsole.Windows;

public abstract class Window : IWindow
{
    public nint WindowId { get; private set; }
    protected Configuration Configuration { get; init; }

    public Window(Configuration configuration)
    {
        Configuration = configuration;
    }

    protected void CreateDesktopWindow(string title, int width, int height)
    {
        WindowId = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, (int)(width * Configuration.UiScaleFactor), (int)(height * Configuration.UiScaleFactor), SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (WindowId == IntPtr.Zero)
        {
            //Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        }
    }

    protected nint CreateRenderer(int logicalWidth, int logicalHeight)
    {
        var renderer = SDL.SDL_CreateRenderer(WindowId, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            //Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        }

        SDL.SDL_RenderSetLogicalSize(renderer, logicalWidth, logicalHeight);

        return renderer;
    }

    public void HandleEvent(SDL.SDL_Event e) { }

    public abstract void Update();

    public abstract void Close();
}