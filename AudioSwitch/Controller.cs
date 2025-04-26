using CoreAudio;
using System;
using System.Collections.Generic;

namespace AudioSwitch
{
    public class Controller
    {
        private readonly List<MMDevice> _orderedDevices = [];
        private readonly int count = 0;

        #region Constructors
        public Controller()
        {
            MMDeviceEnumerator deviceEnum = new(Guid.NewGuid());
            MMDeviceCollection devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            count = devices.Count;

            _orderedDevices = [.. devices];
        }

        public Controller(string[] requestedDeviceOrder)
        {
            MMDeviceEnumerator deviceEnum = new(Guid.NewGuid());
            MMDeviceCollection devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            count = devices.Count;

            InsertDevicesInOrder(devices, requestedDeviceOrder);
        }
        #endregion

        #region Public
        public int Next()
        {
            int current = GetCurrent();
            if (count == 0 || current == -1)
                return -1;

            current = (current + 1) % count;
            return SetDefault(current);
        }

        public int Previous()
        {
            int current = GetCurrent();
            if (count == 0 || current == -1)
                return -1;

            current = (current - 1 + count) % count;
            return SetDefault(current);
        }

        public int Select(int selectedDevice)
        {
            if (selectedDevice >= count)
                return -1;

            return SetDefault(selectedDevice);
        }
        #endregion

        #region Private
        private int SetDefault(int current)
        {
            var device = _orderedDevices[current];
            if (!device.Selected)
                device.Selected = true;

            return current;
        }

        private int GetCurrent()
        {
            for (int i = 0; i < count; i++)
                if (_orderedDevices[i].Selected)
                    return i;

            return -1;
        }

        private void InsertDevicesInOrder(MMDeviceCollection devices, string[] deviceOrder)
        {
            // Create lookup table
            Dictionary<string, MMDevice> lookup = new(count);
            foreach (MMDevice device in devices)
                lookup.Add(device.DeviceInterfaceFriendlyName, device);

            // add all devices in order
            foreach (string name in deviceOrder)
                if (lookup.Remove(name, out var dev))
                    _orderedDevices.Add(dev);

            // add all other devices
            foreach (var dev in lookup)
                _orderedDevices.Add(dev.Value);
        }
        #endregion
    }
}