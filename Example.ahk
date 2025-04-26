#Requires AutoHotkey v2.0

global audioSwitcherPath := A_ScriptDir . "\AudioSwitch.exe"

for i, key in ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"] {
    adjustedValue := i - 3
    Hotkey("^+!" key, RunWait(audioSwitcherPath))
}

OnExit(*) {
}
