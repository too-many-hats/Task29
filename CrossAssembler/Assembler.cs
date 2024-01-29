using BinUtils;

namespace CrossAssembler;

public class Assembler
{
    // the cross assembler has a simple format.
    // A source line may be blank, a comment (denoted by ';'), a label or instruction or directive.
    // Everything on a line after the comment character ';' is ignored, this may turn a line a blank line, in which case it is ignored.

    // Labels must be on their own line or at the start of an instruction or directive line, with an optional comment afterwards. 
    // A label must begin with an alphabetic character followed by 0-31 alphabetic, numeric, minus (-) or underscore characters and is terminated with a full stop '.'. The full stop character is not part of the label.
    // Labels must be unique.

    // An instruction or directive consists of at least the lowercase mnemonic of the instruction or directive referenced and zero or more parameters required by the instruction or directive. Parameters are separated by the comma ',' character. A parameter maybe a label or number. Decimal numbers are default, postfixing a 'b' for binary or 'o' for octal causes the number to be treated as binary or octal. An instruction or directive may have an optional comment on the same line.

    // Whitespace, anywhere that it appears, is ignored. Whitespace can appear inside numbers and is ignored.

    public AssemblyResult Assemble(string sourceText)
    {
        // Famous last words time! "Lets see if we can do this without having to write a proper parser."

        var assemblyResult = new AssemblyResult();

        if (string.IsNullOrEmpty(sourceText))
        {
            assemblyResult.Errors.Add(new Error(0, "Input null or empty"));
            return assemblyResult;
        }

        sourceText = sourceText.ToUpperInvariant();

        // perform the most basic validation on each line and split the lines into their main parts before detailed parsing.
        var sourceStatements = new List<SourceStatement>();
        var rawLines = sourceText.Split('\n');
        var lineNumber = 0;
        foreach (var line in rawLines)
        {
            var trimmedLine = line.Trim();
            ++lineNumber;

            // ignore blank lines.
            if (trimmedLine.Length == 0)
            {
                continue;
            }

            var sourceStatement = new SourceStatement(lineNumber);
            var statementCommentPair = SplitLineParts(trimmedLine);
            sourceStatement.Comment = statementCommentPair.Comment;

            if (statementCommentPair.Statement.Length == 0) // the line only contained a comment. There is nothing more to do.
            {
                continue;
            }

            if (statementCommentPair.Statement.EndsWith('.')) // label statements end with a '.'
            {
                if (TryParseLabel(statementCommentPair.Statement, lineNumber, assemblyResult, out var label))
                {
                    sourceStatement.Type = StatementType.Label;
                    sourceStatement.Label = label;
                }
                else
                {
                    break;
                }
            }
            else // if not label and not a comment, must be an instruction.
            {
                if (SplitInstructionParts(statementCommentPair.Statement, lineNumber, assemblyResult, sourceStatement) == false)
                {
                    break;
                }

                if (statementCommentPair.Label.Length > 0)
                {
                    if (TryParseIdentifier(statementCommentPair.Label, lineNumber, assemblyResult.Errors, out var identifier))
                    {
                        sourceStatement.Label = identifier;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            sourceStatements.Add(sourceStatement);
        }

        if (assemblyResult.Errors.Count != 0)
        {
            return assemblyResult;
        }

        //Start detailed analysis of each line, validate and parse values that we know are correct.
        // We can't assign addresses at this point because directives are variable length. However at this
        // point we have the information we need to execute directives so their length becomes known, as input
        // to the next step.
        foreach (var item in sourceStatements)
        {
            if (item.Type == StatementType.Label)
            {
                continue;
            }

            var directive = Directives.All.FirstOrDefault(x => x.Mnemonic == item.InstructionMnemonic);

            if (directive is not null)
            {
                item.Type = StatementType.Directive;

                assemblyResult.Errors.AddRange(directive.Validate(item));

                if (assemblyResult.Errors.Count > 0)
                {
                    break;
                }

                directive.UpdateOnStatement(item);

                continue;
            }

            var instruction = Instructions.All.FirstOrDefault(x => x.Mnemonic.Substring(0, 2) == item.InstructionMnemonic);

            if (instruction is not null)
            {
                item.Type = StatementType.Instruction;

                if (item.Parameters.Count != 2)
                {
                    assemblyResult.Errors.Add(new Error(item.LineNumber, $"Instruction must have two parameters, even if one, or both are empty"));
                    break;
                }

                foreach (var parameter in item.Parameters)
                {
                    if (TryParseIdentifier(parameter.RawText, item.LineNumber, [], out var identifier))
                    {
                        parameter.Label = identifier;
                        continue;
                    }
                    else if (TokenParsers.TryParseNumber(parameter.RawText, item.LineNumber, [], out var number))
                    {
                        parameter.Data = (uint)number;
                        parameter.IsLabelResolved = true; // there wasn't a label to resolve.
                        continue;
                    }
                }

                continue;
            }

            assemblyResult.Errors.Add(new Error(item.LineNumber, $"{item.InstructionMnemonic} is not a known instruction or directive"));

            break;
        }

        //Create symbol table.
        int addressCounter = 0;
        var symbolTable = new List<Symbol>();
        foreach (var sourceStatement in sourceStatements)
        {
            var dataLength = 0;

            if (sourceStatement.Type == StatementType.Directive)
            {
                dataLength = sourceStatement.Data.Count;
            }
            else if (sourceStatement.Type == StatementType.Instruction)
            {
                dataLength = 1;
            }

            if (sourceStatement.Label.Length > 0)
            {
                symbolTable.Add(new Symbol
                {
                    Label = sourceStatement.Label,
                    Address = (uint)addressCounter,
                });
            }

            addressCounter += dataLength;
        }

        // check that labels are unique
        var duplicateLabelNames = new List<string>();
        foreach (var symbol in symbolTable)
        {
            if (symbolTable.Count(x => x.Label == symbol.Label) > 1)
            {
                duplicateLabelNames.Add(symbol.Label);
            }
        }

        if (duplicateLabelNames.Count > 0)
        {
            assemblyResult.Errors.Add(new Error(0, $"The following symbols are defined more than once: {string.Join(',', duplicateLabelNames.Distinct())}"));
        }

        // resolve labels in instruction parameters and encode instructions.
        foreach (var sourceStatement in sourceStatements)
        {
            if (sourceStatement.Type != StatementType.Instruction)
            {
                continue;
            }

            foreach (var parameter in sourceStatement.Parameters)
            {
                if (parameter.IsLabelResolved == false)
                {
                    var symbol = symbolTable.FirstOrDefault(x => x.Label == parameter.Label);

                    if (symbol is null)
                    {
                        assemblyResult.Errors.Add(new Error(sourceStatement.LineNumber, $"No label \"{parameter.Label}\" found"));
                        continue;
                    }

                    parameter.IsLabelResolved = true;
                    parameter.Data = symbol.Address;
                }
            }

            if (sourceStatement.Parameters.Any(x => x.IsLabelResolved == false))
            {
                break;
            }

            var instruction = Instructions.All.FirstOrDefault(x => x.Mnemonic.Substring(0, 2) == sourceStatement.InstructionMnemonic);
            sourceStatement.Data.Add(Encode.Instruction(instruction.Opcode, (uint)sourceStatement.Parameters[0].Data, (uint)sourceStatement.Parameters[1].Data));
        }

        if (assemblyResult.Errors.Count != 0)
        {
            return assemblyResult;
        }

        foreach (var sourceStatement in sourceStatements)
        {
            if (sourceStatement.Data.Count > 0)
            {
                assemblyResult.Data.AddRange(sourceStatement.Data);
            }
        }

        return assemblyResult;
    }

    private static LineMainParts SplitLineParts(string line)
    {
        var commentCharacterIndex = line.IndexOf(';');
        var labelEndIndex = line.IndexOf('.');
        var label = "";

        if (labelEndIndex > 0)
        {
            label = line.Substring(0, labelEndIndex);
            line = line.Substring(labelEndIndex + 1);
        }

        // there is no comment.
        if (commentCharacterIndex == -1)
        {
            return new LineMainParts(label, line, "");
        }

        return new LineMainParts(label, line.Substring(0, commentCharacterIndex - label.Length - 1).Trim(), line.Substring(commentCharacterIndex - label.Length));
    }

    private static bool TryParseLabel(string line, int lineNumber, AssemblyResult assemblyResult, out string label)
    {
        label = "";

        if (line.EndsWith('.') == false)
        {
            assemblyResult.Errors.Add(new Error(lineNumber, "Label must end with a '.' character"));
            return false;
        }

        var rawLabel = line.Substring(0, line.Length - 1);
        var result = TryParseIdentifier(rawLabel, lineNumber, assemblyResult.Errors, out var identifier);

        label = identifier;
        return result;
    }

    private static bool TryParseIdentifier(string rawIdentifier, int lineNumber, List<Error> errorsList, out string identifier)
    {
        identifier = "";

        if (rawIdentifier.Length == 0 || rawIdentifier.Length > 32)
        {
            errorsList.Add(new Error(lineNumber, "Identifier must be between 1 and 32 characters in length"));
            return false;
        }

        if (IsStartValidIdentifierCharacter(rawIdentifier[0]) == false)
        {
            errorsList.Add(new Error(lineNumber, "Identifier start with a letter"));
            return false;
        }

        if (rawIdentifier.Any(x => IsValidIdentifierCharacter(x) == false))
        {
            errorsList.Add(new Error(lineNumber, "Identifier must contain only letters, numbers or the underscore character"));
            return false;
        }

        identifier = rawIdentifier;
        return true;
    }

    private bool SplitInstructionParts(string line, int lineNumber, AssemblyResult assemblyResult, SourceStatement sourceStatement)
    {
        if (line.Length == 0)
        {
            assemblyResult.Errors.Add(new Error(lineNumber, "Instruction line must not be empty"));
            return false;
        }

        var parts = line.Split(',');

        if (parts.Length == 0)
        {
            assemblyResult.Errors.Add(new Error(lineNumber, "Instruction line must contain an instruction or directive mnemonic"));
            return false;
        }

        var instruction = parts[0].Trim();

        if (instruction.Length == 0)
        {
            assemblyResult.Errors.Add(new Error(lineNumber, "Instruction or directive mnemonic is required"));
            return false;
        }

        sourceStatement.InstructionMnemonic = instruction;

        for (var i = 1; i < parts.Length; i++)
        {
            sourceStatement.Parameters.Add(new InstructionParameter
            {
                RawText = parts[i].Trim(),
            });
        }

        return true;
    }

    private static bool IsValidIdentifierCharacter(char x)
    {
        return char.IsDigit(x) || char.IsLetter(x) || x == '_' || x == '-';
    }

    private static bool IsStartValidIdentifierCharacter(char x)
    {
        return char.IsLetter(x);
    }
}
