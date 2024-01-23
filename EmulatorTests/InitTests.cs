using Emulator;

namespace EmulatorTests;

public class InitTests
{
    public void PowersOnWithCorrectValues()
    {
        var installation = new Installation();
        installation.Cpu.Console.PowerOnPressed();
    }
}