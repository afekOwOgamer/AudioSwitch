using AudioSwitch;
using System;

if (args.Length == 0)
    Environment.Exit(-1);

int action;
if (!int.TryParse(args[0], out action))
    Environment.Exit(-1);

string[] deviceNames = args.Length > 1 ? args[1..] : [];

Controller controller = new(deviceNames);

int result = action switch
{
    -2 => controller.Previous(),
    -1 => controller.Next(),
    _ => controller.Select(action)
};

Environment.Exit(result);