using SDL2;

namespace ReplicaConsole;

public interface IWindow
{
    public uint WindowId { get; }
    public void HandleEvent(SDL.SDL_Event e);
    public void Update();
    public void Close();
    public WindowPositionSize GetPositionSize();
    public void SetPosition(int x, int y);
}