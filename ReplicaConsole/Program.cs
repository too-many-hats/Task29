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

var configuration = ConfigurationLoader.Load("");

var installation = new Installation().Init(configuration);
var windows = new List<IWindow>
{
    new CenterConsolePanel(installation.Cpu, configuration).Init()
};


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
            // find the window that the event is targeting, and let that window handle it's event.
            foreach (var window in windows)
            {
                if (window.WindowId == e.window.windowID)
                {
                    window.HandleEvent(e);
                    break;
                }
            }
        }

        installation.Cpu.Cycle((uint)millisecondsSinceLastFrame * 1000 / Cpu.ClockCycleMicroseconds);

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