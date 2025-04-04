using System.Runtime.InteropServices;
using CoreAudio;

namespace AudioSwitch
{
    internal partial class Controller
    {
        // All playback devices
        private readonly MMDeviceCollection _mMDevices;
        // Current playback device
        private int _current;

        // Constructor
        public Controller()
        {
            MMDeviceEnumerator deviceEnum = new(Guid.NewGuid());
            // All playback devices
            _mMDevices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            // Current playback device
            for (int i = 0; i < _mMDevices.Count; i++)
                if (_mMDevices[i].Selected)
                    _current = i;
        }
        
        // Switches to the next device and returns its index
        public int Next()
        {
            if (_mMDevices.Count == 0) return -1;

            int newIndex = Interlocked.Increment(ref _current) % _mMDevices.Count;
            SetDefault(newIndex);
            return newIndex;
        }

        // Switches to the previous device and returns its index
        public int Previous()
        {
            if (_mMDevices.Count == 0) return -1;

            int newIndex = Interlocked.Decrement(ref _current);
            if (newIndex < 0) newIndex = _mMDevices.Count - 1;

            SetDefault(newIndex);
            return newIndex;
        }

        // Switches to the selected device and returns its index
        public int Select(int selectedDevice)
        {
            if (selectedDevice < 0 || selectedDevice >= _mMDevices.Count) return -1;

            Interlocked.Exchange(ref _current, selectedDevice);
            SetDefault(selectedDevice);
            return selectedDevice;
        }

        // Sets the selected device
        private void SetDefault(int index)
        {
            if (index < 0 || index >= _mMDevices.Count) return;

            MMDevice device = _mMDevices[index];
            device.Selected = true;
        }
    }
}
