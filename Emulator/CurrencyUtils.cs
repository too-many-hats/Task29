namespace Emulator;

public static class CurrencyUtils
{
    /// <summary>
    /// The current value of a historical dollar amount by accounting for inflation.
    /// </summary>
    /// <param name="value">The amount to adjust</param>
    /// <param name="inflationMultiplier">The ratio of inflation.</param>
    /// <returns></returns>
    public static decimal ValueInflationAdjusted(decimal value, decimal inflationMultiplier)
    {
        var unroundedValue = value * inflationMultiplier;
        return Math.Round(unroundedValue, 2, MidpointRounding.AwayFromZero);
    }
}