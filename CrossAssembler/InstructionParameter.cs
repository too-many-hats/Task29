namespace CrossAssembler;

public class InstructionParameter
{
    public string RawText { get; set; }
    public string Label { get; set; } = "";
    public ulong Data { get; set; }
    public bool IsLabelResolved { get; set; } // true if the label exists and it's address has been put into the Data property.

    public void ResolveLabel(uint address)
    {
        Data = address;
        IsLabelResolved = true;
    }
}