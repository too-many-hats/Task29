using Emulator.Devices.Computer;
using SDL2;

namespace ReplicaConsole.Windows;

public class IndicatorRenderer
{
    private readonly nint Renderer;
    private readonly List<LightOnOffTexture> LightOffTextures;

    public IndicatorRenderer(nint renderer, List<LightOnOffTexture> lightOnOffTextures)
    {
        Renderer = renderer;
        LightOffTextures = lightOnOffTextures;
    }

    // the area of the image which we are pulling the indicator texture out of.
    public static SDL.SDL_Rect indicatorSource = new()
    {
        h = 113,
        w = 113,
        x = 0,
        y = 0,
    };

    public static int IndicatorWidthAndMargin => 53;

    public void RenderIndicators(Indicator[] indicators, int x, int y, bool splitInGroupsOfThree, int splitOffsetLeft, bool firstIndicatorOnRight = false)
    {
        const int indicatorDiameter = 43;

        var yInternal = y;

        if (indicators[0].HasHighAndLowLight is false) // when an indicator is only single row, always put the indicator on the bottom row.
        {
            yInternal = yInternal + IndicatorWidthAndMargin;
        }

        var renderIndicator = (Indicator indicator, int xCoordinate) =>
        {
            
        };

        if (firstIndicatorOnRight)
        {
            for (int i = indicators.Length - 1; i > -1; i--)
            {
                renderIndicator(indicators[i], x + (indicators.Length - i) * IndicatorWidthAndMargin);
            }
        }
        else
        {
            for (int i = 0; i < indicators.Length; i++)
            {
                renderIndicator(indicators[i], x + i * IndicatorWidthAndMargin);
            }
        }

        for (int i = 0; i < indicators.Length; i++)
        {
            var indicator = indicators[i];

            var xCoordinate = x;
             
            //if the low order digit of a bank of indicators is the right hand side of the screen
            //then put the first indicator at the right hand side of the bank.
            if (firstIndicatorOnRight)
            {
                xCoordinate += (indicators.Length - 1 - i) * IndicatorWidthAndMargin;
            }
            else
            {
                xCoordinate += i * IndicatorWidthAndMargin;
            }

            var destRect = new SDL.SDL_Rect()// where to place the indictor on the window.
            {
                h = indicatorDiameter,
                w = indicatorDiameter,
                x = xCoordinate,
                y = yInternal,
            };

            // render the top indicator first
            var totalCyclesIndicatorOn = indicator.SumIntensityRecordedFrames();
            var onIndictorAlpha = (byte)(totalCyclesIndicatorOn / 133.33);
            var onOffTextures = LightOffTextures.First(x => x.IndicatorType == indicator.Type);

            SDL.SDL_RenderCopy(Renderer, onOffTextures.OffTexture, ref indicatorSource, ref destRect);
            SDL.SDL_SetTextureAlphaMod(onOffTextures.OnTexture, onIndictorAlpha);

            //     Debug.WriteLine("Alpha: " + (byte)(totalCyclesIndicatorOn / 266.66));
            SDL.SDL_RenderCopy(Renderer, onOffTextures.OnTexture, ref indicatorSource, ref destRect);

            if (indicator.HasHighAndLowLight)
            {
                // then render the bottom indicator if it exists.
                destRect.y += IndicatorWidthAndMargin;

                SDL.SDL_RenderCopy(Renderer, onOffTextures.OffTexture, ref indicatorSource, ref destRect);

                SDL.SDL_SetTextureAlphaMod(onOffTextures.OnTexture, (byte)((34000 - totalCyclesIndicatorOn) / 133.33));
                SDL.SDL_RenderCopy(Renderer, onOffTextures.OnTexture, ref indicatorSource, ref destRect);
            }
        }
    }

    public static int LargeIndicatorDiameter => 65;

    public void RenderLight(Indicator indicator, int x, int y)
    {
        var destRect = new SDL.SDL_Rect()// where to place the light on the window.
        {
            h = LargeIndicatorDiameter,
            w = LargeIndicatorDiameter,
            x = x,
            y = y,
        };

        var onOffTextures = LightOffTextures.First(x => x.IndicatorType == indicator.Type);

        var totalCyclesIndicatorOn = indicator.SumIntensityRecordedFrames();
        SDL.SDL_RenderCopy(Renderer, onOffTextures.OffTexture, ref indicatorSource, ref destRect);
        SDL.SDL_SetTextureAlphaMod(onOffTextures.OnTexture, (byte)(totalCyclesIndicatorOn / 133.33));
        SDL.SDL_RenderCopy(Renderer, onOffTextures.OnTexture, ref indicatorSource, ref destRect);
    }
}