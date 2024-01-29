namespace Emulator.Devices.Computer;

public enum ExecuteMode
{
    HighSpeed,
    AutomaticStepOperation, // at the automatic step rate speed
    AutomaticStepClock, // at the automatic step rate speed
    Clock, // a single cycle
    Distributor, // a single main pulse
    Operation // a single instruction
}