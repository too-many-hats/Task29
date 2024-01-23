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

    private SDL.SDL_Rect indicatorSource = new()
    {
        h = 113,
        w = 113,
        x = 0,
        y = 0,
    };

    public void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        RenderIndicatorCollection(Console.AIndicators, 0, 150, true, 3);
        RenderIndicatorCollection(Console.QIndicators, 53 * 36, 0, true, 3);
        RenderIndicatorCollection(Console.XIndicators, 53 * 36, 300, true, 3);
        RenderIndicatorCollection(Console.MCRIndicators, 53 * 36, 450, true, 3);
        RenderIndicatorCollection(Console.UAKIndicators, 53 * 42, 450, true, 3);
        RenderIndicatorCollection(Console.VAKIndicators, 53 * 57, 450, true, 3);
        RenderIndicatorCollection(Console.PAKIndicators, 53 * 36, 600, true, 3);
        RenderIndicatorCollection(Console.SARIndicators, 53 * 57, 600, true, 3);
        RenderIndicatorCollection([Console.AscDelAddIndicator, Console.AscSpSubtIndicator, Console.AscOverflowIndicator, Console.AscALIndicator, Console.AscARIndicator, Console.AscBIndicator, Console.AscCIndicator, Console.AscDIndicator, Console.AscEIndicator], 0, 0, true, 3);
        RenderIndicatorCollection([Console.InitArithSequenceLogIndicator, Console.InitArithSequenceA_1Indicator, Console.InitArithSequenceSPIndicator, Console.InitArithSequenceA1Indicator, Console.InitArithSequenceQLIndicator, Console.InitArithSequenceDivIndicator, Console.InitArithSequenceMultIndicator, Console.InitArithSequenceSEQIndicator, Console.InitArithSequenceStepIndicator, Console.InitArithSequenceCaseIndicator, Console.InitArithSequenceCKIndicator, Console.InitArithSequenceCKIIndicator, Console.InitArithSequenceRestXIndicator, Console.InitArithSequenceMultiStepIndicator, Console.InitArithSequenceExtSeoIndicator], 53 * 13, 0, true, 3);
        RenderIndicatorCollection([Console.StopTapeIndicator, Console.SccFaultIndicator, Console.MctFaultIndicator, Console.DivFaultIndicator, Console.AZeroIndicator, Console.TapeFeedIndicator, Console.Rsc75Indicator, Console.RscHoldRptIndicator, Console.RscJumpTermIndicator, Console.RscInitRptIndicator, Console.RscInitTestIndicator, Console.RscEndRptIndicator, Console.RscDelayTestIndicator, Console.RscAdvAddIndicator, Console.SccInitReadIndicator, Console.SccInitWriteIndicator, Console.SccInitIw0_14Indicator, Console.SccInitIw15_29Indicator, Console.SccReadQIndicator, Console.SccWriteAorQIndicator, Console.SccClearAIndicator], 0, 300, true, 3);
        RenderIndicatorCollection([Console.MasterClockCSSIIndicator, Console.MasterClockCSSIIIndicator, Console.MasterClockCRCIIndicator, Console.MasterClockCRCIIIndicator], 0, 600, true, 3);
        RenderIndicatorCollection([Console.PdcHpcIndicator, Console.PdcTwcIndicator, Console.PdcWaitInternalIndicator, Console.PdcWaitExternalIndicator, Console.PdcWaitRscIndicator, Console.PdcStop], 53 * 8, 600, true, 3);
        RenderIndicatorCollection(Console.MpdIndicators, 53 * 18, 600, true, 3);
        RenderIndicatorCollection([Console.SctA, Console.SctQ, Console.SctMD, Console.SctMcs0, Console.SctMcs1], 53 * 16, 450, true, 3);
        RenderIndicatorCollection(Console.SkTranslatorIndicators, 0, 450, true, 3);
        RenderIndicatorCollection(Console.JTranslatorIndicators, 53 * 8, 450, true, 3);
        RenderIndicatorCollection(Console.MainPulseTranslatorIndicators, 53 * 24, 600, true, 3);
        RenderIndicatorCollection([Console.Halt, Console.Interrupt], 53 * 53, 600, true, 1);
        //TODO: Add all the other register and flip flop indicators.

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }

    private void RenderIndicatorCollection(Indicator[] indicators, int x, int y, bool splitInGroupsOfThree, int splitOffsetLeft)
    {
        const int indicatorDiameter = 43;
        const int indicatorWidthAndMargin = 53;

        var yInternal = y;

        if (indicators[0].HasHighAndLowLight is false) // when an indicator is only single row, always put the indicator on the bottom row.
        {
            yInternal = yInternal + indicatorWidthAndMargin;
        }

        for (int i = 0; i < indicators.Length; i++)
        {
            var indicator = indicators[i];

            var destRect = new SDL.SDL_Rect()// where to place the indictor on the window.
            {
                h = indicatorDiameter,
                w = indicatorDiameter,
                x = x + i * indicatorWidthAndMargin,
                y = yInternal,
            };

            // render the top indicator first
            var totalCyclesIndicatorOn = indicator.SumIntensityRecordedFrames();
            SDL.SDL_SetTextureAlphaMod(ARegisterTextures[i], (byte)(totalCyclesIndicatorOn / 133.33));
       //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(Renderer, ARegisterTextures[i], ref indicatorSource, ref destRect);

            if (indicator.HasHighAndLowLight)
            {
                // then render the bottom indicator if it exists.
                destRect.y += indicatorWidthAndMargin;
                SDL.SDL_SetTextureAlphaMod(ARegisterTextures[i + 1], (byte)((34000 - totalCyclesIndicatorOn) / 133.33));
                SDL.SDL_RenderCopy(Renderer, ARegisterTextures[i + 1], ref indicatorSource, ref destRect);
            }
        }

    }
}