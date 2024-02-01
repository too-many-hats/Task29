using CrossAssembler;
using Emulator;

namespace EmulatorTests;

public static class TestUtils
{
    public static void PowerOnAndLoad(Installation installation, string sourceText)
    {
        var result = new Assembler().Assemble(sourceText);

        if (result.Errors.Count > 0)
        {
            throw new Exception("Assembly failed.");
        }

        installation.Cpu.Console.PowerOnPressed();
        installation.Cpu.Console.StartPressed();

        for (int i = 0; i < result.Data.Count; i++)
        {
            installation.Cpu.Memory[i] = result.Data[i];
        }
    }

    public static Configuration GetDefaultConfig()
    {
        return new Configuration();
    }
}