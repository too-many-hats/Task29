namespace CrossAssembler;

public class LineMainParts(string Label, string Statement, string Comment)
{
    public string Statement { get; } = Statement;
    public string Comment { get; } = Comment;
    public string Label { get; } = Label;
}