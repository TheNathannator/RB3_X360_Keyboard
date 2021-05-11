# RB3-X360-Keyboard
## About
This project aims to allow you to use the Rock Band 3 Xbox 360 keyboard with other programs while it's connected to an Xbox 360 wireless receiver, via either an emulated Xbox 360 controller, keypresses or MIDI inputs.

I have documented everything I've learned so far in [a .txt file included in this repo.](https://github.com/TheNathannator/RB3-X360-Keyboard/blob/main/X360%20Keys%20Inputs.txt) Note that I may update it with new info as I progress through development.

**Note: The code is unfinished, I am still working on structuring things and making a functional first version.**

## Roadmap
Here's what needs to be done that I can think of so far:
- Framework for the input data
- Keypress output stuff
- MIDI output stuff
- UI

## Similar Projects
Jason Harley's [RB3KB-USB2MIDI](https://jasonharley2o.com/wiki/doku.php?id=rb3keyboard),
 [RB3KB-USB2PSKB](https://jasonharley2o.com/wiki/doku.php?id=rb3keyboardps),
 and [RB3M-USB2MIDI](https://jasonharley2o.com/wiki/doku.php?id=rb3mustang) 

martinjos's [rb3_driver](https://github.com/martinjos/rb3_driver)

## Building

Requires SharpDX.XInput, ViGEm.NET, and DryWetMIDI from NuGet.

## Acknowledgements
bearzly's [RockBandPiano](https://github.com/bearzly/RockBandPiano) project for keyboard input info

[SharpDX](http://sharpdx.org/) for XInput reading stuff

[ViGEm.NET](https://github.com/ViGEm/ViGEm.NET) and [ViGEmBus](https://github.com/ViGEm/ViGEmBus) for XInput device emulation

[DryWetMIDI](https://github.com/melanchall/drywetmidi) for MIDI output

## License
This project is licensed under the MIT License. See [LICENSE](https://github.com/TheNathannator/RB3-X360-Keyboard/blob/main/LICENSE) for details.
