using Emulator;

namespace ReplicaConsole.Windows;

public class Window
{
    protected Configuration Configuration { get; set; }

    public Window(Configuration configuration)
    {
        Configuration = configuration;
    }

    public int Dimension(double value)
    {
        return (int)(value * Configuration.UiScaleFactor);
    }
}