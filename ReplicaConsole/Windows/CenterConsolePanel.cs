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

        return this;
    }

    public override void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        IndicatorRenderer.RenderIndicators(Console.AIndicators, 0, 150, true, 3);
        IndicatorRenderer.RenderIndicators(Console.QIndicators, 53 * 36, 0, true, 3);
        IndicatorRenderer.RenderIndicators(Console.XIndicators, 53 * 36, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MCRIndicators, 53 * 36, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.UAKIndicators, 53 * 42, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.VAKIndicators, 53 * 57, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.PAKIndicators, 53 * 36, 600, true, 3);
        IndicatorRenderer.RenderIndicators(Console.SARIndicators, 53 * 57, 600, true, 3);
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
            ], 53 * 13, 0, true, 3);
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
            ], 53 * 8, 600, true, 3);
        IndicatorRenderer.RenderIndicators(Console.MpdIndicators, 53 * 18, 600, true, 3);
        IndicatorRenderer.RenderIndicators( 
            [
                Console.SctA,
                Console.SctQ,
                Console.SctMD,
                Console.SctMcs0,
                Console.SctMcs1
            ], 53 * 16, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.SkTranslatorIndicators, 0, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.JTranslatorIndicators, 53 * 8, 450, true, 3);
        IndicatorRenderer.RenderIndicators(Console.MainPulseTranslatorIndicators, 53 * 24, 600, true, 3);
        IndicatorRenderer.RenderIndicators([Console.Halt, Console.Interrupt], 53 * 53, 600, true, 3);

        RenderMctIndicators(Console.MainControlTranslatorIndicators, 53 * 24, 280);

        for (int i = 0; i < Console.OperatingRateIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLights(Console.OperatingRateIndicators[i], 150 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Console.SelectiveJumpIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLights(Console.SelectiveJumpIndicators[i], 830 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        for (int i = 0; i < Console.SelectiveStopsIndicators.Length; i++)
        {
            IndicatorRenderer.RenderLights(Console.SelectiveStopsIndicators[i], 1500 + (i * (IndicatorRenderer.LargeIndicatorDiameter + 28)), 780);
        }

        IndicatorRenderer.RenderLights(Console.IndicateEnableLight, 2100, 780);

        IndicatorRenderer.RenderLights(Console.AbnormalConditionLight, 2350, 880);
        IndicatorRenderer.RenderLights(Console.NormalLight, 2300, 980);
        IndicatorRenderer.RenderLights(Console.TestLight, 2300 + 1 * (65 + 28), 980);
        IndicatorRenderer.RenderLights(Console.OperatingLight, 2300 + 2 * (65 + 28), 980);
        IndicatorRenderer.RenderLights(Console.ForceStopLight, 2300 + 3 * (65 + 28), 980);

        IndicatorRenderer.RenderLights(Console.MatrixDriveFaultLight, 2860 + 0 * (65 + 28), 800);
        IndicatorRenderer.RenderLights(Console.MtFaultLight, 2860 + 1 * (65 + 28), 800);
        IndicatorRenderer.RenderLights(Console.MctFaultLight, 2860 + 2 * (65 + 28), 800);
        IndicatorRenderer.RenderLights(Console.IOFaultLight, 2860 + 0 * (65 + 28), 890);
        IndicatorRenderer.RenderLights(Console.VoltageFaultLight, 2860 + 2 * (65 + 28), 890);

        IndicatorRenderer.RenderLights(Console.DivFaultLight, 2860 + 0 * (65 + 28), 1000);
        IndicatorRenderer.RenderLights(Console.SccFaultLight, 2860 + 1 * (65 + 28), 1000);
        IndicatorRenderer.RenderLights(Console.OverflowFaultLight, 2860 + 2 * (65 + 28), 1000);
        IndicatorRenderer.RenderLights(Console.PrintFault, 2860 + 0 * (65 + 28), 1090);
        IndicatorRenderer.RenderLights(Console.TempFault, 2860 + 1 * (65 + 28), 1090);
        IndicatorRenderer.RenderLights(Console.WaterFault, 2860 + 2 * (65 + 28), 1090);
        IndicatorRenderer.RenderLights(Console.CharOverflowLight, 2860 + 2 * (65 + 28), 1180);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
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