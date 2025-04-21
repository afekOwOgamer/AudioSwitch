using CoreAudio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioSwitch
{
    public class Controller
    {
        private readonly MMDevice[] _devices;
        private readonly int count;
        private int _current;

        public Controller(string[] deviceOrder)
        {
            MMDeviceEnumerator deviceEnum = new(Guid.NewGuid());
            MMDeviceCollection devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            count = devices.Count;

            _devices = new MMDevice[count];

            Dictionary<string, MMDevice> lookup = new(count);

            for (int i = 0; i < count; i++)
                lookup[devices[i].DeviceFriendlyName] = devices[i];

            int itemInserted = 0;
            HashSet<string> added = [];
            foreach (string name in deviceOrder)
                if (lookup.TryGetValue(name, out var dev) && added.Add(dev.ID))
                    _devices[itemInserted++] = dev;

            foreach (var dev in devices)
                if (added.Add(dev.ID))
                    _devices[itemInserted++] = dev;

            _current = GetCurrent();
        }

        public int Next()
        {
            if (count == 0 || _current == -1)
                return -1;

            _current = (_current + 1) % count;
            return SetDefault();
        }

        public int Previous()
        {
            if (count == 0 || _current == -1)
                return -1;

            _current = (_current - 1 + count) % count;
            return SetDefault();
        }

        public int Select(int selectedDevice)
        {
            if ((uint)selectedDevice >= (uint)count) // faster bounds check
                return -1;

            _current = selectedDevice;
            return SetDefault();
        }

        private int SetDefault()
        {
            var device = _devices[_current];
            if (!device.Selected)
                device.Selected = true;

            return _current;
        }

        private int GetCurrent()
        {
            for (int i = 0; i < count; i++)
                if (_devices[i].Selected)
                    return i;

            return -1;
        }
    }
}
