using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public static class ConsoleIndicatorsCommon
{
    // the area of the image which we are pulling the indicator texture out of.
    public static SDL.SDL_Rect indicatorSource = new()
    {
        h = 113,
        w = 113,
        x = 0,
        y = 0,
    };

    public static void RenderIndicators(nint renderer, List<nint> indicatorTextures, Indicator[] indicators, int x, int y, bool splitInGroupsOfThree, int splitOffsetLeft)
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
            SDL.SDL_SetTextureAlphaMod(indicatorTextures[i], (byte)(totalCyclesIndicatorOn / 133.33));
            //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(renderer, indicatorTextures[i], ref indicatorSource, ref destRect);

            if (indicator.HasHighAndLowLight)
            {
                // then render the bottom indicator if it exists.
                destRect.y += indicatorWidthAndMargin;
                SDL.SDL_SetTextureAlphaMod(indicatorTextures[i + 1], (byte)((34000 - totalCyclesIndicatorOn) / 133.33));
                SDL.SDL_RenderCopy(renderer, indicatorTextures[i + 1], ref indicatorSource, ref destRect);
            }
        }

    }
}