﻿using SDL2;

namespace ReplicaConsole;

public interface IWindow
{
    public nint WindowId { get; }
    public void HandleEvent(SDL.SDL_Event e);
    public void Update();
    public void Close();
}