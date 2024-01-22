using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public class CenterConsolePanel : IWindow
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    public nint WindowId { get; private set; }
    private nint TestTexture;

    public CenterConsolePanel(Cpu cpu)
    {
        Console = cpu.Console;
    }

    public CenterConsolePanel Init()
    {
        WindowId = SDL.SDL_CreateWindow("Project701 Main Console", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 3500, 938, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (WindowId == IntPtr.Zero)
        {
            //Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        }

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = SDL.SDL_CreateRenderer(WindowId, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (Renderer == IntPtr.Zero)
        {
            //Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        }

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }

        TestTexture = SDL_image.IMG_LoadTexture(Renderer, @"C:\wd\Project702\Docs\consoletest.png");

        return this;
    }

    public void HandleEvent(SDL.SDL_Event e)
    {

    }

    public void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        SDL.SDL_RenderCopy(Renderer, TestTexture, 0, 0);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public void Close()
    {
        SDL.SDL_DestroyTexture(TestTexture);
        SDL.SDL_DestroyRenderer(Renderer);
    }
}