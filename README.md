# Audio Source Switcher

A simple C# program for switching between audio output devices. 
using [CoreAudio](https://www.nuget.org/packages/CoreAudio).

## How It Works

This tool uses **environment variables** to determine which action to perform when running the executable:

- `-2`: Switch to the **previous** audio device  
- `-1`: Switch to the **next** audio device  
- `0` or higher: Select a specific audio device by its **index**

After performing the action, the executable will return the new **current audio device** and then exit.

## Recommended Setup

For quick audio switching, it's highly recommended to use an [AutoHotkey (AHK)](https://www.autohotkey.com/) script to launch the executable with the appropriate environment variable. This allows binding audio switching to hotkeys or other triggers.

 Example Usage

An example AHK script is included in the repository. This script looks for the executable, `AudioSwitch.exe`, in the same directory and runs it with the specified environment variable when a hotkey is pressed.

The hotkeys work as follows:

- **Ctrl + Shift + Alt + F1**: Switch to the previous audio device
- **Ctrl + Shift + Alt + F2**: Switch to the next audio device
- **Ctrl + Shift + Alt + F3 - F12**: Select the audio device by its index (0-9)