namespace CrossAssembler;

public class Assembler
{
    // the cross assembler has a simple format.
    // A source line may be blank, a comment (denoted by ';'), a label or instruction or directive.
    // Everything on a line after the comment character ';' is ignored, this may turn a line a blank line, in which case it is ignored.
    // Labels must be on their own line, with an optional comment afterwards. 
    // A label must begin with an alphabetic character followed by 0-31 alphabetic, numeric, minus (-) or underscore characters and is terminated with a full stop '.'. The full stop character is not part of the label.

    // An instruction or directive consists of at least the lowercase mnemonic of the instruction or directive referenced and zero or more parameters required by the instruction or directive. Parameters are separated by the comma ',' character. A parameter maybe a label or number. Decimal numbers are default, postfixing a 'b' for binary or 'o' for octal causes the number to be treated as binary or octal. An instruction or directive may have an optional comment on the same line.

    public void Assemble(string sourceText)
    {

    }
}
