using Emulator.Devices.Computer;
using Emulator;
using SDL2;

namespace ReplicaConsole.Windows;

public class LeftConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    private IndicatorRenderer IndicatorRenderer { get; set; }

    public LeftConsolePanel(Cpu cpu, Configuration configuration) : base(configuration)
    {
        Console = cpu.Console;
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

        IndicatorRenderer = new IndicatorRenderer(Renderer, LightTexturesLoader.Load(Renderer), Console);

        return this;
    }

    public override void Update()
    {
        // Clears the current render surface.
        if (SDL.SDL_RenderClear(Renderer) < 0)
        {
            // Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
        }

        IndicatorRenderer.RenderIndicators(Console.IOBIndicators, 0, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTapeRegisterIndicators, 0, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTapeControlIndicators, 0, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBlockCounterIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 13, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcSprocketDelayIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 26, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcStopControlIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 28, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcLeaderDelayIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 30, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcInitialDelayIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 32, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcStartControlIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 34, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 3, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBTKIndicators, 0, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcLKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 8, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTSKIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 11, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWriteResumeIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 15, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcMtWriteControlIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 17, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcReadWriteSync, Console.MtcTskControl, Console.MtcBlShift, Console.MtcBlEnd, Console.MtcNotReady], IndicatorRenderer.IndicatorWidthAndMargin * 19, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCkErrorParityIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 24, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCkErrorParityIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 26, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcAlignInputRegisterIndicators, IndicatorRenderer.IndicatorWidthAndMargin * 29, 450, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.MtcBlockEnd, 0, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcRecordEnd, IndicatorRenderer.IndicatorWidthAndMargin * 2, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcFaultControl, Console.MtcBccError], IndicatorRenderer.IndicatorWidthAndMargin * 4, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBccControl, IndicatorRenderer.IndicatorWidthAndMargin * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBccControl, IndicatorRenderer.IndicatorWidthAndMargin * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTrControl, IndicatorRenderer.IndicatorWidthAndMargin * 9, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcTrControlTcrSync], 53 * 11, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBsk, IndicatorRenderer.IndicatorWidthAndMargin * 13, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcReadControl], IndicatorRenderer.IndicatorWidthAndMargin * 16, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWrite, IndicatorRenderer.IndicatorWidthAndMargin * 17, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcSubt, IndicatorRenderer.IndicatorWidthAndMargin * 19, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcAdd, IndicatorRenderer.IndicatorWidthAndMargin * 21, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcDelay, IndicatorRenderer.IndicatorWidthAndMargin * 23, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCenterDriveControl, IndicatorRenderer.IndicatorWidthAndMargin * 26, 600, true, 3, true);


        IndicatorRenderer.RenderIndicators([Console.ExtFaultIoA1, Console.ExtFaultIoB1], IndicatorRenderer.IndicatorWidthAndMargin * 1, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Console.WaitIoARead, Console.WaitIoBRead, Console.WaitIoBWrite, Console.WaitIoBRead], IndicatorRenderer.IndicatorWidthAndMargin * 4, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Console.IoaWrite, Console.IoBWrite, Console.Select], IndicatorRenderer.IndicatorWidthAndMargin * 9, 750, true, 3);
        IndicatorRenderer.RenderIndicators(Console.IoA, IndicatorRenderer.IndicatorWidthAndMargin * 13, 750, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpSRegister, IndicatorRenderer.IndicatorWidthAndMargin * 1, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpDRegister, IndicatorRenderer.IndicatorWidthAndMargin * 12, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpCRegister, IndicatorRenderer.IndicatorWidthAndMargin * 3, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.FpNormExit, Console.FpMooMrp, Console.FpUV, Console.FpAddSubt, Console.FpMulti, Console.FpDiv, Console.FpSign, Console.FpDelayShiftA], IndicatorRenderer.IndicatorWidthAndMargin * 12, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpSequenceGates, IndicatorRenderer.IndicatorWidthAndMargin * 1, 1200, true, 3, true);
        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}