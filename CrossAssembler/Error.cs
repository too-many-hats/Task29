namespace CrossAssembler;

public class Error(int LineNumber, string Message)
{
    public string Message { get; } = Message;
    public int LineNumber { get; } = LineNumber;

    public override string ToString()
    {
        return $"Line: {LineNumber}. {Message}";
    }
}