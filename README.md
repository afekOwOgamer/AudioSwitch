# Audio Source Switcher

A simple C# program for switching between audio output devices. 
using [CoreAudio](https://www.nuget.org/packages/CoreAudio).

## How It Works

This tool uses **environment variables** to determine which action to perform when running the executable:

- `-2`: Switch to the **previous** audio device  
- `-1`: Switch to the **next** audio device  
- `0` or higher: Select a specific audio device by its **index**

### 🆕 Feature: Custom Device Order
- **'Name of your device'**: You can now specify the order in which your audio devices appear in the index.
- If you have devices named 1, 2, and 3, but they appear in a different order in Windows than you prefer, you can define their order manually.
- **You can specify the device order in two ways:**
  - **Command Line Arguments:** List the device names directly when launching the program.
  - **Config File:** Create a JSON config file listing your devices in the desired order, so you don't have to specify them every time you run the program.

**Please Note:** Use only the actual device name (e.g., `"Realtek Audio"`) and not the full description that appears in Windows (e.g., NOT `"Speakers (Realtek Audio)"`).

**Example (Command Line):**
```bash
audioswitcher.exe 0 "BoomBox" "EarBuddies" "ScreenSound"
```

**Example (Config File - JSON):**
```json
[
  "BoomBox",
  "EarBuddies",
  "ScreenSound"
]
```
Save the device names in the file (e.g., `config.json`) placed in the same directory as the executable.  
The program will automatically detect and load the device order from this file if no device names are provided via command line.

After performing the action, the executable will return the new **current audio device** and then exit.

## Recommended Setup

For quick audio switching, it's highly recommended to use an [AutoHotkey (AHK)](https://www.autohotkey.com/) script to launch the executable with the appropriate environment variable. This allows binding audio switching to hotkeys or other triggers.

 Example Usage

An example AHK script is included in the repository. This script looks for the executable, `AudioSwitch.exe`, in the same directory and runs it with the specified environment variable when a hotkey is pressed.

The hotkeys work as follows:

- **Ctrl + Shift + Alt + F1**: Switch to the previous audio device
- **Ctrl + Shift + Alt + F2**: Switch to the next audio device
- **Ctrl + Shift + Alt + F3 - F12**: Select the audio device by its index (0-9)

Optionally: list your device names if you want to order them in a specific way

## License

This project is licensed under the [MIT License](LICENSE).