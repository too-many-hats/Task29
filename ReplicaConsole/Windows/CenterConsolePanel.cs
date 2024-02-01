using Emulator;
using Emulator.Devices.Computer;
using SDL2;
using System.Diagnostics;

namespace ReplicaConsole.Windows;

public class CenterConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    private nint MctLightOnTexture;
    private nint MctLightOffTexture;
    private IndicatorRenderer IndicatorRenderer { get; set; }
    private ButtonTextures ButtonTextures { get; set; }
    private LargeButtonRenderer LargeButtonRenderer { get; set; }

    public CenterConsolePanel(Cpu cpu, Configuration configuration):base(configuration)
    {
        Console = cpu.Console;
    }

    public CenterConsolePanel Init()
    {
        var logicalWidth = 3840;
        var logicalHeight = 1300;

        CreateDesktopWindow("Task29 Main Console", logicalWidth, logicalHeight);

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = CreateRenderer(logicalWidth, logicalHeight);

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }

        MctLightOnTexture = SDL_image.IMG_LoadTexture(Renderer, AppContext.BaseDirectory + @"\Images\indicatoron.png");
        MctLightOffTexture = SDL_image.IMG_LoadTexture(Renderer, AppContext.BaseDirectory + @"\Images\indicatoroff.png");
        IndicatorRenderer = new IndicatorRenderer(Renderer, LightTexturesLoader.Load(Renderer));

        ButtonTextures = new ButtonTextures().Load(Renderer);
        LargeButtonRenderer = new LargeButtonRenderer(ButtonTextures);

        LargeButtonRenderer.CreateMultiple(2300, 1090, Console.MasterClearPressed, Console.StepPressed, Console.StartPressed, Console.ForceStopPressed);

        return this;
    }

    public override void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        LargeButtonRenderer.RenderAll(Renderer);

        IndicatorRenderer.RenderIndicators(Console.AIndicators, 0, 150, true, 3, true);

        //for (int i = 0; i < Console.AIndicators.Length; i++)
        //{
        //    var destRect = new SDL.SDL_Rect
        //    {
        //        h = 26,
        //        w = 26,
        //        x = 0 + 53 * i + 9,
        //        y = 250
        //    };

        //    SDL.SDL_RenderCopy(Renderer, ButtonTextures.ClearButton, ref ButtonTextures.SourceRect, ref destRect);
        //}

        IndicatorRenderer.RenderIndicators(Console.QIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 36, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.XIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 36, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MCRIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 36, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.UAKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 42, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.VAKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 57, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.PAKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 36, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.SARIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 57, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators( 
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
        IndicatorRenderer.RenderIndicators( 
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
            ], IndicatorRenderer.IndicatorWidthAndMargin * 13, 0, true, 3);
        IndicatorRenderer.RenderIndicators( 
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
        IndicatorRenderer.RenderIndicators( 
            [
                Console.MasterClockCSSI,
                Console.MasterClockCSSII,
                Console.MasterClockCRCI,
                Console.MasterClockCRCII
            ], 0, 600, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Console.PdcHpc,
                Console.PdcTwc,
                Console.PdcWaitInternal,
                Console.PdcWaitExternal,
                Console.PdcWaitRsc,
                Console.PdcStop
            ], IndicatorRenderer.IndicatorWidthAndMargin * 8, 600, true, 3);
        IndicatorRenderer.RenderIndicators(Console.MpdIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 18, 600, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Console.SctA,
                Console.SctQ,
                Console.SctMD,
                Console.SctMcs0,
                Console.SctMcs1
            ], IndicatorRenderer.IndicatorWidthAndMargin * 16, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.SkTranslatorIndicators, 0, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.JTranslatorIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 8, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.MainPulseTranslatorIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 24, 600, true, 3);
        IndicatorRenderer.RenderIndicators([Console.Halt, Console.Interrupt], IndicatorRenderer.IndicatorWidthAndMargin * 53, 600, true, 3);

        RenderMctIndicators(Console.MainControlTranslatorIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 24, 280);

        for (int i = 0; i < Console.OperatingRateIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLight(Console.OperatingRateIndicators[i], 150 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Console.SelectiveJumpIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLight(Console.SelectiveJumpIndicators[i], 830 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Console.SelectiveStopsIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLight(Console.SelectiveStopsIndicators[i], 1500 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        IndicatorRenderer.RenderLight(Console.IndicateEnableLight, 2100, 780);

        IndicatorRenderer.RenderLight(Console.AbnormalConditionLight, 2350, 880);
        IndicatorRenderer.RenderLight(Console.NormalLight, 2300, 980);
        IndicatorRenderer.RenderLight(Console.TestLight, 2300 + 1 * (65 + 28), 980);
        IndicatorRenderer.RenderLight(Console.OperatingLight, 2300 + 2 * (65 + 28), 980);
        IndicatorRenderer.RenderLight(Console.ForceStopLight, 2300 + 3 * (65 + 28), 980);

        IndicatorRenderer.RenderLight(Console.MatrixDriveFaultLight, 2860 + 0 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Console.MtFaultLight, 2860 + 1 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Console.MctFaultLight, 2860 + 2 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Console.IOFaultLight, 2860 + 0 * (65 + 28), 890);
        IndicatorRenderer.RenderLight(Console.VoltageFaultLight, 2860 + 2 * (65 + 28), 890);

        IndicatorRenderer.RenderLight(Console.DivFaultLight, 2860 + 0 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Console.SccFaultLight, 2860 + 1 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Console.OverflowFaultLight, 2860 + 2 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Console.PrintFault, 2860 + 0 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Console.TempFault, 2860 + 1 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Console.WaterFault, 2860 + 2 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Console.CharOverflowLight, 2860 + 2 * (65 + 28), 1180);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public override void HandleEvent(SDL.SDL_Event e)
    {
        LargeButtonRenderer.TestHitsAndFireCallbacks(e);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }

    public void RenderMctIndicators(Indicator[] indicators, int x, int y)
    {
        const int indicatorDiameter = 32;
        const int indicatorWidth = 53;

        var yInternal = y;

        for (int i = 0; i < indicators.Length; i++)
        {
            if (i != 0 && (i % 8) == 0) // in every row of 8 indicators, do not render the first in the row, except for the first row.
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

            var totalCyclesIndicatorOn = indicator.SumIntensityRecordedFrames();
            SDL.SDL_RenderCopy(Renderer, MctLightOffTexture, ref IndicatorRenderer.indicatorSource, ref destRect);

            SDL.SDL_SetTextureAlphaMod(MctLightOnTexture, (byte)(totalCyclesIndicatorOn / 133.33));
            //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(Renderer, MctLightOnTexture, ref IndicatorRenderer.indicatorSource, ref destRect);
        }

    }
}