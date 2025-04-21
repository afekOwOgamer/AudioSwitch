#Requires AutoHotkey v2.0

global audioSwitcherPath := A_ScriptDir . "\AudioSwitch.exe"
global devices := ["Your Speakers", "Your Headphones","Your Monitor"]

ConvertToCommandLine(action) {
    cmd := audioSwitcherPath . " " . action
    
    for device in devices {
        cmd .= " " . Chr(34) . device . Chr(34)
    }
    
    MsgBox("cmd: " . cmd)
    return cmd
}

for i, key in ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"] {
    adjustedValue := i - 3
    Hotkey("^+!" key, RunWait(ConvertToCommandLine(adjustedValue)))
}

OnExit(*) {
}
