
using BinUtils;

namespace Emulator.Devices.Computer;

public class Console
{
    

    //RightConsolePanel

    private Cpu Cpu { get; set; }

    public Console(Cpu cpu)
    {
        Cpu = cpu;
    }

    public void PowerOnPressed()
    {
        Cpu.PowerOnPressed();
    }


    public void ClearPAK()
    {
        Cpu.ClearPAK();
    }

    public void SetXTo(ulong value)
    {
        Cpu.SetXto(value);
    }

    public void SetPAKTo(uint value)
    {
        Cpu.SetPAKto(value);
    }

    public void StartPressed()
    {
        Cpu.StartPressed();
    }

    public void MasterClearPressed()
    {
        Cpu.MasterClearPressed();
    }

    public void StepPressed()
    {
        Cpu.StepPressed();
    }

    public void ForceStopPressed()
    {
        Cpu.ForceStopPressed();
    }

    public void ReleaseOperatingRateSelection()
    {
        Cpu.SetExecuteMode(ExecuteMode.HighSpeed); // functionally the same as calling SetExecuteMode(ExecuteMode executeMode), but a nicer API for consumers.
    }

    public void ManualClockPressed()
    {
        Cpu.SetExecuteMode(ExecuteMode.Clock);
    }

    public void ManualDistributorPressed()
    {
        Cpu.SetExecuteMode(ExecuteMode.Distributor);
    }

    public void ManualOperationPressed()
    {
        Cpu.SetExecuteMode(ExecuteMode.Operation);
    }

    public void AutomaticStepClockPressed()
    {
        Cpu.SetExecuteMode(ExecuteMode.AutomaticStepClock);
    }

    public void AutomaticStepOperationPressed()
    {
        Cpu.SetExecuteMode(ExecuteMode.AutomaticStepOperation);
    }

    public void ReleaseSelectiveStopPressed(uint stopNumber)
    {
        Cpu.ReleaseSelectiveStopPressed(stopNumber);
    }

    public void SelectiveStopSelectPressed(uint stopNumber)
    {
        Cpu.SelectSelectiveStopPressed(stopNumber);
    }

    public void SelectiveJumpSelectPressed(uint jumpNumber)
    {
        Cpu.SelectSelectiveJumpPressed(jumpNumber);
    }

    public void SelectiveJumpReleasePressed(uint jumpNumber)
    {
        Cpu.ReleaseSelectiveJumpPressed(jumpNumber);
    }

    public void ClearAFaultPressed()
    {
        Cpu.ClearAFaultPressed();
    }
}