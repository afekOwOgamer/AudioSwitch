using AudioSwitch;

bool isValid = int.TryParse(args[0], out int device);

if (isValid)
{
    Controller controller = new();
    switch (device)
    {
        case -2:
            Environment.Exit(controller.Previous());
            break;

        case -1:
            Environment.Exit(controller.Next());
            break;

        default:
            Environment.Exit(controller.Select(device));
            break;
    }
}
else
    Environment.Exit(-1);