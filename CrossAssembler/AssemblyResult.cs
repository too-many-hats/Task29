namespace CrossAssembler;

public class AssemblyResult
{
    public List<Error> Errors { get; set; } = [];
    public List<ListingLine> ListingLines { get; set; } = [];
    public bool Success => Errors.Any() == false;
    public List<ulong> Data { get; set; } = [];
}