namespace Emulator.Devices.Computer;

public class Drum
{
    public Drum(Configuration configuration)
    {
        Interlace = (uint)configuration.DrumInterlace;
    }

    public uint AngularIndexCounter { get; set; }
    public uint Gs { get; set; }
    public uint Group { get; set; } = 4; // as per image in reference manual, page 3-3.
    public uint Interlace { get; private set; }
    public bool InitWrite {  get; set; }
    public bool InitWrite0_14 { get; set; }
    public bool InitRead { get; set; }
    public bool InitDelayedRead { get; set; }
    public bool ReadLockoutIII { get; set; }
    public bool ReadLockoutII { get; set; }
    public bool ReadLockoutI { get; set; }
    public bool ConincLockout { get; set; }
    public bool Preset { get; set; }
    public bool AdvanceAik { get; set; }
    public bool CpdI { get; set; }
    public bool CpdII { get; set; }
    public bool InitWrite15_29 { get; set; }
}