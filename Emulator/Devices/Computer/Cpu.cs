namespace Emulator.Devices.Computer;

public class Cpu
{
    public ulong[] Memory { get; private set; } = new ulong[12288];
}