using Emulator.Devices.Computer;
using Emulator;
using SDL2;

namespace ReplicaConsole.Windows;

public class RightConsolePanel : Window
{
    public Emulator.Devices.Computer.Console Console { get; set; }
    private nint Renderer;
    private IndicatorRenderer IndicatorRenderer { get; set; }
    private Cpu Cpu { get; set; }

    public RightConsolePanel(Cpu cpu, Configuration configuration) : base(configuration)
    {
        Console = cpu.Console;
        Cpu = cpu;
    }

    public RightConsolePanel Init()
    {
        var logicalWidth = 1920;
        var logicalHeight = 1350;

        CreateDesktopWindow("Task29 Right Console", logicalWidth, logicalHeight);

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

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsAddressRegisters[1], 0, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.McsMonInit[1], Cpu.Indicators.McsRead[1], Cpu.Indicators.McsWrite[1], Cpu.Indicators.McsWaitInit[1], Cpu.Indicators.McsReadWriteEnable[1], Cpu.Indicators.McsEnId[1], Cpu.Indicators.McsWr0_14[1], Cpu.Indicators.McsWr30_35[1], Cpu.Indicators.McsWr15_29[1]], IndicatorRenderer.IndicatorWidthAndMargin * 12 + 15, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsMainPulseDistributorTranslators[1], IndicatorRenderer.IndicatorWidthAndMargin * 23 + 15, 0, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsPulseDistributors[1], IndicatorRenderer.IndicatorWidthAndMargin * 29 + 15, 0, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.HsPunchRegister, IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 150, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.DrumGs, IndicatorRenderer.IndicatorWidthAndMargin * 22 + 15, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.DrumAngularIndexCounter, IndicatorRenderer.IndicatorWidthAndMargin * 24 + 15, 150, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsAddressRegisters[0], 0, 150, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.McsMonInit[0], Cpu.Indicators.McsRead[0], Cpu.Indicators.McsWrite[0], Cpu.Indicators.McsWaitInit[0], Cpu.Indicators.McsReadWriteEnable[0], Cpu.Indicators.McsEnId[0], Cpu.Indicators.McsWr0_14[0], Cpu.Indicators.McsWr30_35[0], Cpu.Indicators.McsWr15_29[0]], IndicatorRenderer.IndicatorWidthAndMargin * 2, 450, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsMainPulseDistributorTranslators[0], IndicatorRenderer.IndicatorWidthAndMargin * 2, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.McsPulseDistributors[0], IndicatorRenderer.IndicatorWidthAndMargin * 9, 600, true, 3, true);

        IndicatorRenderer.RenderIndicators([Cpu.Indicators.HsPunchInit, Cpu.Indicators.HsPunchRes], IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 600, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.TypewriterRegister, IndicatorRenderer.IndicatorWidthAndMargin * 14 + 15, 300, true, 3, true);
        IndicatorRenderer.RenderIndicators([Cpu.Indicators.DrumInitWrite, Cpu.Indicators.DrumInitWrite0_14, Cpu.Indicators.DrumInitWrite15_29, Cpu.Indicators.DrumInitRead, Cpu.Indicators.DrumInitDelayedRead, Cpu.Indicators.DrumReadLockoutIII, Cpu.Indicators.DrumReadLockoutII, Cpu.Indicators.DrumReadLockoutI, Cpu.Indicators.DrumConincLockout, Cpu.Indicators.DrumPreset, Cpu.Indicators.DrumAdvanceAik, Cpu.Indicators.DrumCpdI, Cpu.Indicators.DrumCpdII], 53 * 23 + 15, 300, true, 3, true);

        IndicatorRenderer.RenderIndicators(Cpu.Indicators.DrumGroup, IndicatorRenderer.IndicatorWidthAndMargin * 23 + 15, 600, true, 3, true);
        IndicatorRenderer.RenderIndicators(Cpu.Indicators.DrumInterlace, IndicatorRenderer.IndicatorWidthAndMargin * 31 + 15, 600, true, 3, true);

        SDL.SDL_RenderPresent(Renderer);
    }

    public override void Close()
    {
        SDL.SDL_DestroyRenderer(Renderer);
    }
}