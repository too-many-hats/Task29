namespace CrossAssembler;

public class Directive
{
    public string Name { get; set; }
    public string Mnemonic { get; set; }
    public Func<SourceStatement, List<Error>> Validate { get; set; }
    public Action<SourceStatement> UpdateOnStatement { get; set; }
    public string Description { get; internal set; }
}