using Emulator;
using System.Text.Json;

namespace ReplicaConsole;

public static class ConfigurationLoader
{
    public static string DefaultConfigFile => "DefaultConfiguration.json";

    public static Configuration Load(string path)
    {
        // attempt to load the user defined configuration file, if one is not supplied, use the default config file.
        if (string.IsNullOrWhiteSpace(path))
        {
            path = Path.Combine(AppContext.BaseDirectory, DefaultConfigFile);
        }

        if (File.Exists(path) is false)
        {
            throw new Exception("Unable to load custom emulator configuration. File not found or inaccessible.");
        }

        var configJson = File.ReadAllText(path);

        var config = JsonSerializer.Deserialize<Configuration>(configJson);

        if (config is null)
        {
            throw new Exception("Configuration file contains invalid JSON or one or more property values have an incorrect type.");
        }

        return config;
    }
}