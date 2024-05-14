using SDL2;

namespace ReplicaConsole.Windows;

public static class SdlHelpers
{
    internal static nint CreateDesktopWindow(string title, int width, int height, double uiScaleFactor)
    {
        var scaledWidth = (int)(width * uiScaleFactor);
        var scaledHeight = (int)(height * uiScaleFactor);
        var windowHandle = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, scaledWidth, scaledHeight, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (windowHandle == IntPtr.Zero)
        {
            throw new Exception($"Unable to create SDL window. {SDL.SDL_GetError()}");
        }

        return windowHandle;
    }

    internal static nint CreateRenderer(int logicalWidth, int logicalHeight, nint windowHandle)
    {
        var renderer = SDL.SDL_CreateRenderer(windowHandle, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            throw new Exception($"Unable to create SDL render. {SDL.SDL_GetError()}");
        }

        SDL.SDL_RenderSetLogicalSize(renderer, logicalWidth, logicalHeight);

        return renderer;
    }

    public static WindowPositionSize GetPositionSize(nint windowHandle)
    {
        SDL.SDL_GetWindowPosition(windowHandle, out var x, out var y);
        SDL.SDL_GetWindowSize(windowHandle, out var width, out var height);
        var windowPositionSize = new WindowPositionSize
        {
            Height = height,
            Width = width,
            X = x,
            Y = y
        };

        return windowPositionSize;
    }

    internal static nint LoadTexture(nint renderer, string fileName)
    {
        var texturePath = Path.Combine(AppContext.BaseDirectory, "Textures", fileName);

        if (File.Exists(texturePath) == false)
        {
            throw new Exception($"Texture file {fileName} does not exist");
        }

        var texture = SDL_image.IMG_LoadTexture(renderer, texturePath);

        if (texture == IntPtr.Zero)
        {
            throw new Exception($"Unable to find or load texture {fileName}");
        }

        return texture;
    }

    public class WindowPositionSize
    {
        public required int X { get; init; }
        public required int Y { get; init; }
        public required int Width { get; init; }
        public required int Height { get; init; }
    }
}
