namespace Emulator.Devices.Computer;

public class Drum
{
    public uint AngularIndexCounter { get; private set; }
    public uint Gs { get; private set; }
    public uint Group { get; private set; }
    public uint Interlace { get; private set; }
    public bool InitWrite {  get; private set; }
    public bool InitWrite0_14 { get; private set; }
    public bool InitRead { get; private set; }
    public bool InitDelayedRead { get; private set; }
    public bool ReadLockoutIII { get; private set; }
    public bool ReadLockoutII { get; private set; }
    public bool ReadLockoutI { get; private set; }
    public bool ConincLockout { get; private set; }
    public bool Preset { get; private set; }
    public bool AdvanceAik { get; private set; }
    public bool CpdI { get; private set; }
    public bool CpdII { get; private set; }
}