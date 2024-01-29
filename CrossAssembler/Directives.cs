using BinUtils;

namespace CrossAssembler;

public static class Directives
{
    public readonly static Directive Val = new()
    {
        Mnemonic = "SVAL",
        Name = "Signed Value",
        Description = "Stores a one signed 36bit number in memory",
        Validate = (SourceStatement sourceStatement) =>
        {
            var errors = new List<Error>();

            if (sourceStatement.Parameters.Count != 1)
            {
                errors.Add(new Error(sourceStatement.LineNumber, "sval directive requires exactly one parameter"));
                return errors;
            }

            if (TokenParsers.TryParseNumber(sourceStatement.Parameters[0].RawText, sourceStatement.LineNumber, errors, out var number) == false)
            {
                errors.Add(new Error(sourceStatement.LineNumber, "sval directive requires a valid numeric value"));
            }

            return errors;
        },
        UpdateOnStatement = (sourceStatement) =>
        {
            TokenParsers.TryParseNumber(sourceStatement.Parameters[0].RawText, sourceStatement.LineNumber, [], out var number);
            sourceStatement.Data.Add(Encode.SignedNumber(number));
        }
    };

    public readonly static IReadOnlyList<Directive> All = new List<Directive>
    {
        Val
    };
}