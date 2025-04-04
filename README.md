A simple c# project for switching between audio sources, using CoreAudio, a wrapper for windows Core Audio apis.

This uses environment variables to determine what action to use (when running the executable):
-2 - previous audio source
-1 - next audio source
0+ - select a specific audio source by his index

running the executable will try to run the action and then close, and returns the new current audio device.

Highly recommand using AHK script to launch the executable with env variables for quick audio switching