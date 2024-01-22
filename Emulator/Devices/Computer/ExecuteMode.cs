namespace Emulator.Devices.Computer;

public enum ExecuteMode
{
    HighSpeed,
    Clock, // a single cycle
    Distributor, // a single main pulse
    Operation // a single instruction
}