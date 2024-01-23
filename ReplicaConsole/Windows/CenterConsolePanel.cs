using Emulator;
using Emulator.Devices.Computer;
using SDL2;
using System.Diagnostics;

namespace ReplicaConsole.Windows;

public class CenterConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    private readonly List<nint> ARegisterTextures = [];

    public CenterConsolePanel(Cpu cpu, Configuration configuration):base(configuration)
    {
        Console = cpu.Console;
    }

    public CenterConsolePanel Init()
    {
        var logicalWidth = 3840;
        var logicalHeight = 800;

        CreateDesktopWindow("Task29 Main Console", logicalWidth, logicalHeight);

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = CreateRenderer(logicalWidth, logicalHeight);

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

    public override void Update()
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
        ConsoleIndicatorsCommon.RenderIndicators(Renderer, ARegisterTextures, [Console.Halt, Console.Interrupt], 53 * 53, 600, true, 3);

        var finalStopIndicator = Console.MainControlTranslatorIndicators[0];
        RenderMctIndicators(Renderer, ARegisterTextures, Console.MainControlTranslatorIndicators, 53 * 24, 280);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }

    public static void RenderMctIndicators(nint renderer, List<nint> indicatorTextures, Indicator[] indicators, int x, int y)
    {
        const int indicatorDiameter = 32;
        const int indicatorWidth = 53;

        var yInternal = y;

        for (int i = 0; i < indicators.Length; i++)
        {
            if (i != 0 && (i % 8) == 0)
            {
                continue;
            }

            var indicator = indicators[i];

            var destRect = new SDL.SDL_Rect()// where to place the indictor on the window.
            {
                h = indicatorDiameter,
                w = indicatorDiameter,
                x = x + (i % 8) * indicatorWidth,
                y = yInternal + 45 * (i / 8),
            };

            // render the top indicator first
            var totalCyclesIndicatorOn = indicator.SumIntensityRecordedFrames();
            SDL.SDL_SetTextureAlphaMod(indicatorTextures[i], (byte)(totalCyclesIndicatorOn / 133.33));
            //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(renderer, indicatorTextures[i], ref ConsoleIndicatorsCommon.indicatorSource, ref destRect);
        }

    }
}