# RB3 X360 Keyboard

## About

This program allows you to use an Xbox 360 RB3 keyboard for things while connected to an Xbox 360 wireless receiver, via either an emulated Xbox 360 controller, keypresses, or MIDI inputs.

The touch strip is not supported, as it is not reported through the standard input APIs.

## Installation

1. Download the latest version from [the Releases page](../../releases/latest).
2. Extract the contents of the .zip into a new folder.
3. Install the [ViGEmBus driver](https://github.com/ViGEm/ViGEmBus/releases/latest) if you wish to use Xbox 360 controller emulation.
4. Install MIDI loopback software such as [loopMIDI](https://www.tobias-erichsen.de/software/loopmidi.html) if you wish to use MIDI output.

## Research

I have documented everything I know about the keyboard inputs in [a Markdown file included in this repo](doc/Xbox%20360%20Keyboard%20Inputs.md). For the most part everything should be correct, but there are a couple things missing or potentially incorrect.

## Similar Projects

Jason Harley's [RB3KB-USB2MIDI](https://jasonharley2o.com/wiki/doku.php?id=rb3keyboard), [RB3KB-USB2PSKB](https://jasonharley2o.com/wiki/doku.php?id=rb3keyboardps), and [RB3M-USB2MIDI](https://jasonharley2o.com/wiki/doku.php?id=rb3mustang) programs for PS3 and Wii keyboards and Mustang guitars.

martinjos's [rb3_driver](https://github.com/martinjos/rb3_driver) for Wii (and likely PS3) keyboards.

## Acknowledgements

bearzly's [RockBandPiano](https://github.com/bearzly/RockBandPiano) project for pointing me in the right direction with how the inputs work

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
