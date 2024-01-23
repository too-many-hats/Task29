using Emulator.Devices.Computer;
using SDL2;
using System.Diagnostics;

namespace ReplicaConsole.Windows;

public class CenterConsolePanel : IWindow
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    public nint WindowId { get; private set; }
    private readonly List<nint> ARegisterTextures = [];

    public CenterConsolePanel(Cpu cpu)
    {
        Console = cpu.Console;
    }

    public CenterConsolePanel Init()
    {
        WindowId = SDL.SDL_CreateWindow("Task29 Main Console", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 3840, 800, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

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

        var aRegisterIndicatorCount = Console.AIndicators.Length * (Console.AIndicators[0].HasHighAndLowLight ? 2 : 1);

        for (int i = 0; i < aRegisterIndicatorCount; i++)
        {
            ARegisterTextures.Add(SDL_image.IMG_LoadTexture(Renderer, AppContext.BaseDirectory + @"\Images\bitmap.png"));
        }

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

        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.AIndicators, 0, 150, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.QIndicators, 53 * 36, 0, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.XIndicators, 53 * 36, 300, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.MCRIndicators, 53 * 36, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.UAKIndicators, 53 * 42, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.VAKIndicators, 53 * 57, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.PAKIndicators, 53 * 36, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.SARIndicators, 53 * 57, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.AscDelAdd, 
                Console.AscSpSubt, 
                Console.AscOverflow, 
                Console.AscAL, 
                Console.AscAR, 
                Console.AscB, 
                Console.AscC, 
                Console.AscD, 
                Console.AscE
            ], 0, 0, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.InitArithSequenceLog, 
                Console.InitArithSequenceA_1, 
                Console.InitArithSequenceSP, 
                Console.InitArithSequenceA1, 
                Console.InitArithSequenceQL, 
                Console.InitArithSequenceDiv, 
                Console.InitArithSequenceMult, 
                Console.InitArithSequenceSEQ, 
                Console.InitArithSequenceStep, 
                Console.InitArithSequenceCase, 
                Console.InitArithSequenceCKI, 
                Console.InitArithSequenceCKII, 
                Console.InitArithSequenceRestX, 
                Console.InitArithSequenceMultiStep, 
                Console.InitArithSequenceExtSeq
            ], 53 * 13, 0, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.StopTape, 
                Console.SccFault, 
                Console.MctFault, 
                Console.DivFault, 
                Console.AZero, 
                Console.TapeFeed,
                Console.Rsc75,
                Console.RscHoldRpt, 
                Console.RscJumpTerm,
                Console.RscInitRpt, 
                Console.RscInitTest,
                Console.RscEndRpt,
                Console.RscDelayTest,
                Console.RscAdvAdd,
                Console.SccInitRead,
                Console.SccInitWrite,
                Console.SccInitIw0_14,
                Console.SccInitIw15_29,
                Console.SccReadQ,
                Console.SccWriteAorQ, 
                Console.SccClearA
            ], 0, 300, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.MasterClockCSSI,
                Console.MasterClockCSSII,
                Console.MasterClockCRCI,
                Console.MasterClockCRCII
            ], 0, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.PdcHpc,
                Console.PdcTwc,
                Console.PdcWaitInternal,
                Console.PdcWaitExternal,
                Console.PdcWaitRsc,
                Console.PdcStop
            ], 53 * 8, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.MpdIndicators, 53 * 18, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, 
            [
                Console.SctA,
                Console.SctQ,
                Console.SctMD,
                Console.SctMcs0,
                Console.SctMcs1
            ], 53 * 16, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.SkTranslatorIndicators, 0, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.JTranslatorIndicators, 53 * 8, 450, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, Console.MainPulseTranslatorIndicators, 53 * 24, 600, true, 3);
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, [Console.Halt, Console.Interrupt], 53 * 24, 600, true, 3);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}