namespace CrossAssembler;

public class SourceStatement
{
    public string Comment { get; set; } = "";
    public int LineNumber { get; set; }
    public string Label { get; set; } = "";
    public string InstructionMnemonic { get; set; } = "";
    public List<InstructionParameter> Parameters { get; set; } = [];
    public List<ulong> Data { get; set; } = [];
    public int Address { get; set; }
    public bool IsDirective { get; set; }
    public StatementType Type { get; set; }

    public SourceStatement(int lineNumber)
    {
        LineNumber = lineNumber;
    }
}