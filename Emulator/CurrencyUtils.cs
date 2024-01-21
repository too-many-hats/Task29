namespace Emulator;

public static class CurrencyUtils
{
    public static decimal ValueInflationAdjusted(decimal value, decimal inflationMultiplier)
    {
        var unroundedValue = value * inflationMultiplier;
        return Math.Round(unroundedValue, 2, MidpointRounding.AwayFromZero);
    }
}