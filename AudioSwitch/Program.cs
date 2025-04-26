using AudioSwitch;
using System;
using System.IO;
using System.Text.Json;

#region Logic
int action = GetAction(args);
if (action <= -3)
    return -1;

string[] devices = GetDevices(args);
return MainLogic(action, devices);
#endregion

#region Functions
static int MainLogic(int act, string[] deviceOrder)
{
    Controller control = deviceOrder.Length == 0 ? new() : new(deviceOrder);

    return act switch
    {
        -2 => control.Previous(),
        -1 => control.Next(),
        _ => control.Select(act)
    };
}

static int GetAction(string[] args) =>
    (args.Length == 0 || !int.TryParse(args[0], out int action)) ? -3 : action;

static string[] GetDevices(string[] args)
{
    if (args.Length > 1)
        return args[1..];

    try
    {
        string path = AppContext.BaseDirectory + "config.json";
        return File.Exists(path) ?
            (JsonSerializer.Deserialize<string[]>(File.ReadAllText(path)) ?? Array.Empty<string>()) :
            Array.Empty<string>();
    }
    catch (Exception)
    {
        return Array.Empty<string>();
    }
}
#endregion