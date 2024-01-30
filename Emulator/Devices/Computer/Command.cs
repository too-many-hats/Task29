namespace Emulator.Devices.Computer;

/// <summary>
/// 1103A commands and subcommands. Each command has an action which is called every cycle of the main pulse that the command is called from. Commands take a variable number of cycles so we track when each command is complete, and do not call the command again in the same main pulse after it has completed.
/// </summary>
public class Command
{
    public bool IsComplete { get; set; }
    public Action Execute {  get; set; }

    public Command(Action<Command> action)
    {
        Execute = () => 
        {
            if (IsComplete)
            {
                return;
            }

            action(this);
        };
    }

    public void Complete()
    {
        IsComplete = true;
    }
}