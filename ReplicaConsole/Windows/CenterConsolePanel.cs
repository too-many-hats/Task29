using Emulator;
using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public class CenterConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    public Cpu Cpu { get; set; }
    private nint Renderer;
    private nint MctLightOnTexture;
    private nint MctLightOffTexture;
    private IndicatorRenderer IndicatorRenderer { get; set; }
    private ButtonTextures ButtonTextures { get; set; }
    private LargeButtonRenderer LargeButtonRenderer { get; set; }

    public CenterConsolePanel(Cpu cpu, Configuration configuration):base(configuration)
    {
        Console = cpu.Console;
        Cpu = cpu;
    }

    public CenterConsolePanel Init()
    {
        var logicalWidth = 3840;
        var logicalHeight = 1350;

        CreateDesktopWindow("Task29 Main Console", logicalWidth, logicalHeight);

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = SdlHelpers.CreateRenderer(logicalWidth, logicalHeight, WindowHandle);

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }

        MctLightOnTexture = SdlHelpers.LoadTexture(Renderer, AppContext.BaseDirectory + @"\Images\indicatoron.png");
        MctLightOffTexture = SdlHelpers.LoadTexture(Renderer, AppContext.BaseDirectory + @"\Images\indicatoroff.png");
        IndicatorRenderer = new IndicatorRenderer(Renderer, LightTexturesLoader.Load(Renderer), Cpu);

        ButtonTextures = new ButtonTextures().Load(Renderer);
        LargeButtonRenderer = new LargeButtonRenderer(ButtonTextures);

        LargeButtonRenderer.CreateMultiple(2300, 1090, Console.MasterClearPressed, Console.StepPressed, Console.StartPressed, Console.ForceStopPressed);
        LargeButtonRenderer.CreateMultiple(60 + IndicatorRenderer.LargeIndicatorDiameter - 1 + 28, 950, Console.ManualClockPressed, Console.ManualDistributorPressed, Console.ManualOperationPressed, Console.AutomaticStepClockPressed, Console.AutomaticStepOperationPressed);
        
        LargeButtonRenderer.CreateMultiple(240, 1110, Console.ReleaseOperatingRateSelection);

        LargeButtonRenderer.CreateMultiple(830, 950, () => Console.SelectiveJumpSelectPressed(3), () => Console.SelectiveJumpSelectPressed(2), () => Console.SelectiveJumpSelectPressed(1));
        LargeButtonRenderer.CreateMultiple(830, 1110, () => Console.SelectiveJumpReleasePressed(3), () => Console.SelectiveJumpReleasePressed(2), () => Console.SelectiveJumpReleasePressed(1));

        LargeButtonRenderer.CreateMultiple(1594, 1110, () => Console.SelectiveStopSelectPressed(3), () => Console.SelectiveStopSelectPressed(2), () => Console.SelectiveStopSelectPressed(1));
        LargeButtonRenderer.CreateMultiple(1594, 1270, () => Console.ReleaseSelectiveStopPressed(3), () => Console.ReleaseSelectiveStopPressed(2), () => Console.ReleaseSelectiveStopPressed(1));

        LargeButtonRenderer.CreateMultiple(2860 + 1 * (65 + 28), 1180, Console.ClearAFaultPressed);

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

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.A, 0, 150, true, 3, true);

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

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.Q, IndicatorRenderer.IndicatorWidthAndMargin * 36, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.X, IndicatorRenderer.IndicatorWidthAndMargin * 36, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MCR, IndicatorRenderer.IndicatorWidthAndMargin * 36, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.UAK, IndicatorRenderer.IndicatorWidthAndMargin * 42, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.VAK, IndicatorRenderer.IndicatorWidthAndMargin * 57, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.PAK, IndicatorRenderer.IndicatorWidthAndMargin * 36, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.SAR, IndicatorRenderer.IndicatorWidthAndMargin * 57, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.AscDelAdd,
                Cpu.Indicators.AscSpSubt,
                Cpu.Indicators.AscOverflow,
                Cpu.Indicators.AscAL,
                Cpu.Indicators.AscAR,
                Cpu.Indicators.AscB,
                Cpu.Indicators.AscC,
                Cpu.Indicators.AscD,
                Cpu.Indicators.AscE
            ], 0, 0, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.InitArithSequenceLog,
                Cpu.Indicators.InitArithSequenceA_1,
                Cpu.Indicators.InitArithSequenceSP,
                Cpu.Indicators.InitArithSequenceA1,
                Cpu.Indicators.InitArithSequenceQL,
                Cpu.Indicators.InitArithSequenceDiv,
                Cpu.Indicators.InitArithSequenceMult,
                Cpu.Indicators.InitArithSequenceSEQ,
                Cpu.Indicators.InitArithSequenceStep,
                Cpu.Indicators.InitArithSequenceCase,
                Cpu.Indicators.InitArithSequenceCKI,
                Cpu.Indicators.InitArithSequenceCKII,
                Cpu.Indicators.InitArithSequenceRestX,
                Cpu.Indicators.InitArithSequenceMultiStep,
                Cpu.Indicators.InitArithSequenceExtSeq
            ], IndicatorRenderer.IndicatorWidthAndMargin * 13, 0, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.StopTape,
                Cpu.Indicators.SccFault,
                Cpu.Indicators.MctFault,
                Cpu.Indicators.DivFault,
                Cpu.Indicators.AZero,
                Cpu.Indicators.TapeFeed,
                Cpu.Indicators.Rsc75,
                Cpu.Indicators.RscHoldRpt,
                Cpu.Indicators.RscJumpTerm,
                Cpu.Indicators.RscInitRpt,
                Cpu.Indicators.RscInitTest,
                Cpu.Indicators.RscEndRpt,
                Cpu.Indicators.RscDelayTest,
                Cpu.Indicators.RscAdvAdd,
                Cpu.Indicators.SccInitRead,
                Cpu.Indicators.SccInitWrite,
                Cpu.Indicators.SccInitIw0_14,
                Cpu.Indicators.SccInitIw15_29,
                Cpu.Indicators.SccReadQ,
                Cpu.Indicators.SccWriteAorQ,
                Cpu.Indicators.SccClearA
            ], 0, 300, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.MasterClockCSSI,
                Cpu.Indicators.MasterClockCSSII,
                Cpu.Indicators.MasterClockCRCI,
                Cpu.Indicators.MasterClockCRCII
            ], 0, 600, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.PdcHpc,
                Cpu.Indicators.PdcTwc,
                Cpu.Indicators.PdcWaitInternal,
                Cpu.Indicators.PdcWaitExternal,
                Cpu.Indicators.PdcWaitRsc,
                Cpu.Indicators.PdcStop
            ], IndicatorRenderer.IndicatorWidthAndMargin * 8, 600, true, 3);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.Mpd, IndicatorRenderer.IndicatorWidthAndMargin * 18, 600, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Cpu.Indicators.SctA,
                Cpu.Indicators.SctQ,
                Cpu.Indicators.SctMD,
                Cpu.Indicators.SctMcs0,
                Cpu.Indicators.SctMcs1
            ], IndicatorRenderer.IndicatorWidthAndMargin * 16, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.SkTranslator, 0, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.JTranslator, IndicatorRenderer.IndicatorWidthAndMargin * 8, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MainPulseTranslator, IndicatorRenderer.IndicatorWidthAndMargin * 24, 600, true, 3);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.Halt, Cpu.Indicators.Interrupt], IndicatorRenderer.IndicatorWidthAndMargin * 53, 600, true, 3);

        RenderMctIndicators(Cpu.Indicators.MainControlTranslator, IndicatorRenderer.IndicatorWidthAndMargin * 24, 280);

        for (int i = 0; i < Cpu.Indicators.OperatingRate.Length; i++)
        {
            IndicatorRenderer.RenderLight(Cpu.Indicators.OperatingRate[i], 150 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Cpu.Indicators.SelectiveJump.Length; i++)
        {
            IndicatorRenderer.RenderLight(Cpu.Indicators.SelectiveJump[i], 830 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Cpu.Indicators.Stop.Length; i++)
        {
            IndicatorRenderer.RenderLight(Cpu.Indicators.Stop[i], 1500 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Cpu.Indicators.SelectiveStopSelected.Length; i++)
        {
            IndicatorRenderer.RenderLight(Cpu.Indicators.SelectiveStopSelected[i], 1592 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 950);
        }

        IndicatorRenderer.RenderLight(Cpu.Indicators.IndicateEnableLight, 2100, 780);

        IndicatorRenderer.RenderLight(Cpu.Indicators.AbnormalConditionLight, 2350, 880);
        IndicatorRenderer.RenderLight(Cpu.Indicators.NormalLight, 2300, 980);
        IndicatorRenderer.RenderLight(Cpu.Indicators.TestLight, 2300 + 1 * (65 + 28), 980);
        IndicatorRenderer.RenderLight(Cpu.Indicators.OperatingLight, 2300 + 2 * (65 + 28), 980);
        IndicatorRenderer.RenderLight(Cpu.Indicators.ForceStopLight, 2300 + 3 * (65 + 28), 980);

        IndicatorRenderer.RenderLight(Cpu.Indicators.MatrixDriveFaultLight, 2860 + 0 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Cpu.Indicators.MtFaultLight, 2860 + 1 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Cpu.Indicators.MctFaultLight, 2860 + 2 * (65 + 28), 800);
        IndicatorRenderer.RenderLight(Cpu.Indicators.IOFaultLight, 2860 + 0 * (65 + 28), 890);
        IndicatorRenderer.RenderLight(Cpu.Indicators.VoltageFaultLight, 2860 + 2 * (65 + 28), 890);

        IndicatorRenderer.RenderLight(Cpu.Indicators.DivFaultLight, 2860 + 0 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Cpu.Indicators.SccFaultLight, 2860 + 1 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Cpu.Indicators.OverflowFaultLight, 2860 + 2 * (65 + 28), 1000);
        IndicatorRenderer.RenderLight(Cpu.Indicators.PrintFault, 2860 + 0 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Cpu.Indicators.TempFault, 2860 + 1 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Cpu.Indicators.WaterFault, 2860 + 2 * (65 + 28), 1090);
        IndicatorRenderer.RenderLight(Cpu.Indicators.CharOverflowLight, 2860 + 2 * (65 + 28), 1180);

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

            var engerisedRatio = IndicatorRenderer.GetIndicatorEnergisedRatio(indicator, Cpu.Indicators.TotalCyclesLast6Frames);
            SDL.SDL_RenderCopy(Renderer, MctLightOffTexture, ref IndicatorRenderer.indicatorSource, ref destRect);

            SDL.SDL_SetTextureAlphaMod(MctLightOnTexture, (byte)(engerisedRatio * 255));
            //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(Renderer, MctLightOnTexture, ref IndicatorRenderer.indicatorSource, ref destRect);
        }

    }
}