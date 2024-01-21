namespace BinUtils;

public class Instruction(string Mnemonic, string Name, string Opcode)
{
    public string Mnemonic { get; init; } = Mnemonic;
    public string Name { get; init; } = Name;
    public uint Opcode { get; init; } = Convert.ToUInt32(Opcode, 8); // convert the octal opcode to decimal
}