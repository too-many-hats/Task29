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
        var logicalHeight = 1300;

        CreateDesktopWindow("Task29 Left Console", logicalWidth, logicalHeight);

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        Renderer = CreateRenderer(logicalWidth, logicalHeight);

        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            // Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }

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

        IndicatorRenderer.RenderIndicators(Console.IOBIndicators, 0, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTapeRegisterIndicators, 0, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTapeControlIndicators, 0, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBlockCounterIndicators, 53 * 13, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcSprocketDelayIndicators, 53 * 26, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcStopControlIndicators, 53 * 28, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcLeaderDelayIndicators, 53 * 30, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcInitialDelayIndicators, 53 * 32, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcStartControlIndicators, 53 * 34, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWKIndicators, 53 * 3, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBTKIndicators, 0, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcLKIndicators, 53 * 8, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTSKIndicators, 53 * 11, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWriteResumeIndicators, 53 * 15, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcMtWriteControlIndicators, 53 * 17, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcReadWriteSync, Console.MtcTskControl, Console.MtcBlShift, Console.MtcBlEnd, Console.MtcNotReady], 53 * 19, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCkErrorParityIndicators, 53 * 24, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCkErrorParityIndicators, 53 * 26, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcAlignInputRegisterIndicators, 53 * 29, 450, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.MtcBlockEnd, 0, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcRecordEnd, 53 * 2, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcFaultControl, Console.MtcBccError], 53 * 4, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBccControl, 53 * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBccControl, 53 * 6, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcTrControl, 53 * 9, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcTrControlTcrSync], 53 * 11, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcBsk, 53 * 13, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.MtcReadControl], 53 * 16, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcWrite, 53 * 17, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcSubt, 53 * 19, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcAdd, 53 * 21, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcDelay, 53 * 23, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.MtcCenterDriveControl, 53 * 26, 600, true, 3, true);


        IndicatorRenderer.RenderIndicators([Console.ExtFaultIoA1, Console.ExtFaultIoB1], 53 * 1, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Console.WaitIoARead, Console.WaitIoBRead, Console.WaitIoBWrite, Console.WaitIoBRead], 53 * 4, 750, true, 3);
        IndicatorRenderer.RenderIndicators([Console.IoaWrite, Console.IoBWrite, Console.Select], 53 * 9, 750, true, 3);
        IndicatorRenderer.RenderIndicators(Console.IoA, 53 * 13, 750, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpSRegister, 53 * 1, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpDRegister, 53 * 12, 900, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpCRegister, 53 * 3, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.FpNormExit, Console.FpMooMrp, Console.FpUV, Console.FpAddSubt, Console.FpMulti, Console.FpDiv, Console.FpSign, Console.FpDelayShiftA], 53 * 12, 1050, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.FpSequenceGates, 53 * 1, 1200, true, 3, true);
        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}