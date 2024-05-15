using Emulator.Devices.Computer;
using Emulator;
using SDL2;

namespace ReplicaConsole.Windows;

public class LeftConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    public Cpu Cpu { get; set; }
    private nint Renderer;
    private IndicatorRenderer IndicatorRenderer { get; set; }

    public LeftConsolePanel(Cpu cpu, Configuration configuration) : base(configuration)
    {
        Console = cpu.Console;
        Cpu = cpu;
    }

    public LeftConsolePanel Init()
    {
        var logicalWidth = 1920;
        var logicalHeight = 1350;

        CreateDesktopWindow("Task29 Left Console", logicalWidth, logicalHeight);

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = SdlHelpers.CreateRenderer(logicalWidth, logicalHeight, WindowHandle);

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }

        IndicatorRenderer = new IndicatorRenderer(Renderer, LightTexturesLoader.Load(Renderer), Cpu);

        return this;
    }

    public override void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.IOBIndicators, 0, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcTapeRegister, 0, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcTapeControl, 0, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBlockCounter, IndicatorRenderer.IndicatorWidthAndMargin * 13, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcSprocketDelay, IndicatorRenderer.IndicatorWidthAndMargin * 26, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcStopControl, IndicatorRenderer.IndicatorWidthAndMargin * 28, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcLeaderDelay, IndicatorRenderer.IndicatorWidthAndMargin * 30, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcInitialDelay, IndicatorRenderer.IndicatorWidthAndMargin * 32, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcStartControl, IndicatorRenderer.IndicatorWidthAndMargin * 34, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcWK, IndicatorRenderer.IndicatorWidthAndMargin * 3, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBTK, 0, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcLK, IndicatorRenderer.IndicatorWidthAndMargin * 8, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcTSK, IndicatorRenderer.IndicatorWidthAndMargin * 11, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcWriteResume, IndicatorRenderer.IndicatorWidthAndMargin * 15, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcMtWriteControl, IndicatorRenderer.IndicatorWidthAndMargin * 17, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.MtcReadWriteSync, Cpu.Indicators.MtcTskControl, Cpu.Indicators.MtcBlShift, Cpu.Indicators.MtcBlEnd, Cpu.Indicators.MtcNotReady], IndicatorRenderer.IndicatorWidthAndMargin * 19, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcCkErrorParity, IndicatorRenderer.IndicatorWidthAndMargin * 24, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcCkErrorParity, IndicatorRenderer.IndicatorWidthAndMargin * 26, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcAlignInputRegister, IndicatorRenderer.IndicatorWidthAndMargin * 29, 450, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBlockEnd, 0, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcRecordEnd, IndicatorRenderer.IndicatorWidthAndMargin * 2, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.MtcFaultControl, Cpu.Indicators.MtcBccError], IndicatorRenderer.IndicatorWidthAndMargin * 4, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBccControl, IndicatorRenderer.IndicatorWidthAndMargin * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBccControl, IndicatorRenderer.IndicatorWidthAndMargin * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcTrControl, IndicatorRenderer.IndicatorWidthAndMargin * 9, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.MtcTrControlTcrSync], 53 * 11, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcBsk, IndicatorRenderer.IndicatorWidthAndMargin * 13, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.MtcReadControl], IndicatorRenderer.IndicatorWidthAndMargin * 16, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcWrite, IndicatorRenderer.IndicatorWidthAndMargin * 17, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcSubt, IndicatorRenderer.IndicatorWidthAndMargin * 19, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcAdd, IndicatorRenderer.IndicatorWidthAndMargin * 21, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcDelay, IndicatorRenderer.IndicatorWidthAndMargin * 23, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.MtcCenterDriveControl, IndicatorRenderer.IndicatorWidthAndMargin * 26, 600, true, 3, true);


        IndicatorRenderer.RenderIndicators([Cpu.Indicators.ExtFaultIoA1, Cpu.Indicators.ExtFaultIoB1], IndicatorRenderer.IndicatorWidthAndMargin * 1, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.WaitIoARead, Cpu.Indicators.WaitIoBRead, Cpu.Indicators.WaitIoBWrite, Cpu.Indicators.WaitIoBRead], IndicatorRenderer.IndicatorWidthAndMargin * 4, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.IoaWrite, Cpu.Indicators.IoBWrite, Cpu.Indicators.Select], IndicatorRenderer.IndicatorWidthAndMargin * 9, 750, true, 3);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.IoA, IndicatorRenderer.IndicatorWidthAndMargin * 13, 750, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.FpSRegister, IndicatorRenderer.IndicatorWidthAndMargin * 1, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.FpDRegister, IndicatorRenderer.IndicatorWidthAndMargin * 12, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.FpCRegister, IndicatorRenderer.IndicatorWidthAndMargin * 3, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.FpNormExit, Cpu.Indicators.FpMooMrp, Cpu.Indicators.FpUV, Cpu.Indicators.FpAddSubt, Cpu.Indicators.FpMulti, Cpu.Indicators.FpDiv, Cpu.Indicators.FpSign, Cpu.Indicators.FpDelayShiftA], IndicatorRenderer.IndicatorWidthAndMargin * 12, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.FpSequenceGates, IndicatorRenderer.IndicatorWidthAndMargin * 1, 1200, true, 3, true);
        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}