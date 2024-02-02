using Emulator.Devices.Computer;
using Emulator;
using SDL2;

namespace ReplicaConsole.Windows;

public class RightConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    private IndicatorRenderer IndicatorRenderer { get; set; }

    public RightConsolePanel(Cpu cpu, Configuration configuration) : base(configuration)
    {
        Console = cpu.Console;
    }

    public RightConsolePanel Init()
    {
        var logicalWidth = 1920;
        var logicalHeight = 1350;

        CreateDesktopWindow("Task29 Right Console", logicalWidth, logicalHeight);

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

        IndicatorRenderer.RenderIndicators(Console.McsAddressRegisters[1], 0, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.McsMonInit[1], Console.McsRead[1], Console.McsWrite[1], Console.McsWaitInit[1], Console.McsReadWriteEnable[1], Console.McsEnId[1], Console.McsWr0_14[1], Console.McsWr30_35[1], Console.McsWr15_29[1]], IndicatorRenderer.IndicatorWidthAndMargin * 12 + 15, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.McsMainPulseDistributorTranslators[1], IndicatorRenderer.IndicatorWidthAndMargin * 23 + 15, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.McsPulseDistributors[1], IndicatorRenderer.IndicatorWidthAndMargin * 29 + 15, 0, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.HsPunchRegister, IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 150, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.DrumGs, IndicatorRenderer.IndicatorWidthAndMargin * 22 + 15, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.DrumAngularIndexCounter, IndicatorRenderer.IndicatorWidthAndMargin * 24 + 15, 150, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.McsAddressRegisters[0], 0, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.McsMonInit[0], Console.McsRead[0], Console.McsWrite[0], Console.McsWaitInit[0], Console.McsReadWriteEnable[0], Console.McsEnId[0], Console.McsWr0_14[0], Console.McsWr30_35[0], Console.McsWr15_29[0]], IndicatorRenderer.IndicatorWidthAndMargin * 2, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.McsMainPulseDistributorTranslators[0], IndicatorRenderer.IndicatorWidthAndMargin * 2, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.McsPulseDistributors[0], IndicatorRenderer.IndicatorWidthAndMargin * 9, 600, true, 3, true);

        IndicatorRenderer.RenderIndicators([Console.HsPunchInit, Console.HsPunchRes], IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 600, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.TypewriterRegister, IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators([Console.DrumInitWrite, Console.DrumInitWrite0_14, Console.DrumInitWrite15_29, Console.DrumInitRead, Console.DrumInitDelayedRead, Console.DrumReadLockoutIII, Console.DrumReadLockoutII, Console.DrumReadLockoutI, Console.DrumConincLockout, Console.DrumPreset, Console.DrumAdvanceAik, Console.DrumCpdI, Console.DrumCpdII], 53 * 23 + 15, 300, true, 3, true);

        IndicatorRenderer.RenderIndicators(Console.DrumGroup, IndicatorRenderer.IndicatorWidthAndMargin * 23 + 15, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Console.DrumInterlace, IndicatorRenderer.IndicatorWidthAndMargin * 31 + 15, 600, true, 3, true);

        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}