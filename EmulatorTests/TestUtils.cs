using CrossAssembler;
using Emulator;
using Emulator.Devices.Computer;
using FluentAssertions;
using FluentAssertions.Equivalency;

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

        for (int i = 0; i < result.Data.Count; i++)
        {
            installation.Cpu.Memory[i] = result.Data[i];
        }
    }
    
    public static Configuration GetDefaultConfig()
    {
        return new Configuration();
    }

    public static Func<EquivalencyAssertionOptions<Cpu>, EquivalencyAssertionOptions<Cpu>> NormalCpuConfig()
    {
        return (options) => options.Excluding(su => su.Indicators).Excluding(x => x.WorldClock);
    }

    public static void AssertEquivalent(Cpu cpuUnderTest, Cpu referenceCpu)
    {
        cpuUnderTest.Should().BeEquivalentTo(referenceCpu, NormalCpuConfig());
    }
}