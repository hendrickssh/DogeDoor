# DogeDoor
DogeDoor is the newest addition to any smart home setup.

Using open source hardware such as Arduino and HC-SR501 motion sensor, the door has the option to be manually or automatically opened.
The door uses a simple Android application to:
  - Open the door
  - Close the door
  - Lock the door
Alternatively, the door includes a motion sensor so that pets can simply walk up to the door to have it open!

## Tech Used
The core of the DogeDoor uses an Arduino Uno, HC-SR501 motion sensor, and a stepper motor. We included an HC-05 Bluetooth module to allow
remote access of the door from a smartphone.

The smartphone application is developed using Xamarin in C# for easy portability between different devices.

## Future Plans
The DogeDoor is currently in a beta state, and although it's base design works there are a few features yet to be implemented.

These include:
  - Android application redesign
  - IOS application
  - Addition of outside motion sensor
  - WiFi hookup rather than Bluetooth for access anywhere
  
## Contributors
- Cody Blunt
- Aaron Hietpas
- Kyle Hoffhein
- Shane Hendricks
- Brian McColgan
