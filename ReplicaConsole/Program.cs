﻿using CrossAssembler;
using Emulator;
using Emulator.Devices.Computer;
using ReplicaConsole;
using ReplicaConsole.Windows;
using SDL2;
using System.Diagnostics;

if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
{
    System.Console.WriteLine($"There was an issue initilizing SDL. {SDL.SDL_GetError()}");
}

SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1");

var configuration = ConfigurationLoader.Load("");

var installation = new Installation().Init(configuration);

# region forTestingOnly

var assemblyResult = new Assembler().Assemble("ps,0,0");

for (int i = 0; i < assemblyResult.Data.Count; i++)
{
    installation.Cpu.Memory[i] = assemblyResult.Data[i];
}

installation.Cpu.Console.SetPAKTo(0);

#endregion

var centerConsoleWindow = new CenterConsolePanel(installation.Cpu, configuration).Init();

var windows = new List<IWindow>
{
    centerConsoleWindow,
};

if (configuration.HideConsoleSideWings == false)
{
    var leftConsoleWindow = new LeftConsolePanel(installation.Cpu, configuration).Init();
    var rightConsoleWindow = new RightConsolePanel(installation.Cpu, configuration).Init();

    windows.Add(leftConsoleWindow);
    windows.Add(rightConsoleWindow);

    var centerWindowPosition = centerConsoleWindow.GetPositionSize();
    var leftWindowPosition = leftConsoleWindow.GetPositionSize();

    // TODO: this code does not handle display scaling at all, It also only works if you have a 4k monitor to the left of your primary display. It'll do for testing.
    centerConsoleWindow.SetPosition(1080, centerWindowPosition.Y);

    var centerWindowPositionUpdated = centerConsoleWindow.GetPositionSize(); // get the updated position

    leftConsoleWindow.SetPosition(centerWindowPositionUpdated.X - leftWindowPosition.Width, centerWindowPositionUpdated.Y);
    rightConsoleWindow.SetPosition(centerWindowPositionUpdated.X + centerWindowPositionUpdated.Width, centerWindowPositionUpdated.Y);
}

//var font = SDL_ttf.TTF_OpenFont(@"C:\wd\Project702\Roboto-Regular.ttf", 12);
//var textSurface = SDL_ttf.TTF_RenderText_Solid(font, "Message", new SDL.SDL_Color { b = 210, g = 50, r = 255});
//var textTexture = SDL.SDL_CreateTextureFromSurface(renderer, textSurface);
//var textRect = new SDL.SDL_Rect
//{
//    h = 20, w = 70, x= 10, y= 10
//};

var running = true;
var lastFrameRenderedAt = SDL.SDL_GetTicks();
var targetFPS = 60;

// Main loop for the program
while (running)
{
    var currentTime = SDL.SDL_GetTicks();

    // limit ourselves to 60 frames per second. At the end of each frame, process all window events
    // that occurred during the previous frame and then advance the emulator.

    // this means input events (like pressing FORCE STOP) are delayed to the end of the frame. 
    // Then the emulator runs for 16ms, except nothing will execute because FORCE STOP was pressed.
    //So if you press FORCE STOP part way through an executing frame, the executing frame will complete
    // anyway, and the stop only takes affect for the next frame. I'm not sure if this really matters,
    // since it will be hard for a human to notice the <16ms delay anyway. Essentially the last frame's
    // events are used as input for the next frame's emulation.

    var millisecondsSinceLastFrame = currentTime - (double)lastFrameRenderedAt;
    if (millisecondsSinceLastFrame > 1000 / targetFPS)
    {
        Debug.WriteLine("FPS:" + (1000 / millisecondsSinceLastFrame));

        lastFrameRenderedAt = currentTime;

        // Check to see if there are any events and continue to do so until the queue is empty.
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
        {
            // find the window the event is targeting, and let that window handle it.
            foreach (var window in windows)
            {
                if (window.WindowId == centerConsoleWindow.WindowId && e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE)
                {
                    running = false;
                    break;
                }

                if (window.WindowId == e.window.windowID)
                {
                    window.HandleEvent(e);
                    break;
                }
            }
        }

        var cyclesInFrame = (uint)(millisecondsSinceLastFrame * 1000 / Cpu.ClockCycleMicroseconds);
        installation.Cpu.Cycle(cyclesInFrame);
        
        // update the UI for each window.
        foreach (var window in windows)
        {
            window.Update();
        }
    }
}

foreach (var window in windows)
{
    window.Close();
}

// Clean up the resources that were created.
SDL.SDL_Quit();