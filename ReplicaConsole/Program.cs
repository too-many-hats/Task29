using SDL2;

if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
{
    Console.WriteLine($"There was an issue initilizing SDL. {SDL.SDL_GetError()}");
}
//3000, 700
var window = SDL.SDL_CreateWindow("Project701 Main Console", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 3500, 938, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

if (window == IntPtr.Zero)
{
    Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
}

// Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
var renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

if (renderer == IntPtr.Zero)
{
    Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
}

// Initilizes SDL_image for use with png files.
if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
{
    // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
}

if (SDL_ttf.TTF_Init() == 0)
{
    // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
}


var texture = SDL_image.IMG_LoadTexture(renderer, @"C:\wd\Project702\Docs\consoletest.png");
//var font = SDL_ttf.TTF_OpenFont(@"C:\wd\Project702\Roboto-Regular.ttf", 12);
//var textSurface = SDL_ttf.TTF_RenderText_Solid(font, "Message", new SDL.SDL_Color { b = 210, g = 50, r = 255});
//var textTexture = SDL.SDL_CreateTextureFromSurface(renderer, textSurface);
//var textRect = new SDL.SDL_Rect
//{
//    h = 20, w = 70, x= 10, y= 10
//};

var running = true;

// Main loop for the program
while (running)
{
    // Check to see if there are any events and continue to do so until the queue is empty.
    while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
    {
        if (e.type == SDL.SDL_EventType.SDL_QUIT)
        {
            break;
        }


        switch (e.type)
        {
            case SDL.SDL_EventType.SDL_KEYDOWN:
                running = true;
                break;
        }
    }

    // Clears the current render surface.
    if (SDL.SDL_RenderClear(renderer) < 0)
    {
        Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
    }

    SDL.SDL_RenderCopy(renderer, texture, 0, 0);
    //SDL.SDL_RenderCopy(renderer, textTexture, 0, ref textRect);

    // Switches out the currently presented render surface with the one we just did work on.
    SDL.SDL_RenderPresent(renderer);
}

// Clean up the resources that were created.
// SDL_ttf.TTF_CloseFont(font);
SDL_ttf.TTF_Quit();
// SDL.SDL_FreeSurface(textSurface);
SDL.SDL_DestroyTexture(texture);
SDL.SDL_DestroyRenderer(renderer);
SDL.SDL_Quit();