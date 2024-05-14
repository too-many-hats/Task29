using Emulator;
using SDL2;
using static ReplicaConsole.Windows.SdlHelpers;

namespace ReplicaConsole.Windows;

public abstract class Window : IWindow
{
    public uint WindowId { get; private set; }
    private Configuration Configuration { get; init; }
    public nint WindowHandle { get; private set; }

    public Window(Configuration configuration)
    {
        Configuration = configuration;
    }

    protected void CreateDesktopWindow(string title, int width, int height)
    {
        var scaledWidth = (int)(width * Configuration.UiScaleFactor);
        var scaledHeight = (int)(height * Configuration.UiScaleFactor);
        WindowHandle = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, scaledWidth, scaledHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (WindowHandle == IntPtr.Zero)
        {
            //Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        }

        WindowId = SDL.SDL_GetWindowID(WindowHandle);
    }

    public WindowPositionSize GetPositionSize()
    {
        return SdlHelpers.GetPositionSize(WindowHandle);
    }

    public virtual void HandleEvent(SDL.SDL_Event e) { }

    public abstract void Update();

    public abstract void Close();

    public void SetPosition(int x, int y)
    {
        SDL.SDL_SetWindowPosition(WindowHandle, x, y);
    }
}