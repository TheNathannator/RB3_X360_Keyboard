=== RB3 X360 Keyboard ===
- About -
This program allows you to use an Xbox 360 RB3 keyboard for things while connected to an Xbox 360 wireless receiver, via either an emulated Xbox 360 controller, keypresses, or MIDI inputs.


- Notes - 
Currently, the touch strip is not supported.

Install the ViGEmBus driver from https://github.com/ViGEm/ViGEmBus/releases/latest if you wish to use Xbox 360 controller emulation.
Install MIDI loopback software such as loopMIDI from https://www.tobias-erichsen.de/software/loopmidi.html if you wish to use MIDI output.


- Modes -
In all modes, pressing Start, Back, and the Guide button at the same time will disable all outputs immediately.
It doesn't wait before re-allowing inputs, though.

Xbox 360:
    Face buttons are mapped 1:1, minus guide button
    C1 & C2: A button
    D1 & D2: B button
    E1 & E2: Y button
    F1 & F2: X button
    G1 & G2: Left bumper
    A1 & A2: Right bumper
    B1 & B2: Left stick click
    C3: Right stick click
    Overdrive button: Back button
    Pedal digital: Back button in standard mode, left bumper in drum mode

Keyboard:
    The keys that correspond to the RB keyboard keys are arranged roughly like the RB keyboard keys, but split into 2 halves on top of each other.
     2 3   5 6 7
    Q W E R T Y U I
     S D   G H J
    Z X C V B N M

    Overdrive: O
    Pedal digital: P
    Start: Enter/Return
    Back: Escape
    D-pad: Arrow keys

MIDI:
    Keys correspond to a range of 25 MIDI notes from an octave offset:
    Octave 0: MIDI notes 0 through 24
    each octave shifts this range by 12
    Drum Mode has specific notes for the bottom 12 notes:
    C1: 35               C#1/Db1: 36    D1: 38           D#1/Eb1:  40     E1: 41
    Acoustic Bass Drum   Bass Drum 1    Acoustic Snare   Electric Snare   Low Floor Tom 
    F1 = 47              F#1/Gb1 = 50   G1 = 42          G#1/Ab1 = 46     A1 = 49          A#1/Bb1 = 51    B1 = 53
    Low Mid Tom n        High Tom       Closed Hi Hat    Open Hi Hat      Crash Cymbal 1   Ride Cymbal 1   Ride Bell
    
    Back: Stop message
    Guide: Continue message
    Start: Start message
    
    Pedal digital: Control channel 64 (Damper pedal)
    Pedal analog:
        Expression pedal mode: Control channel 11 (Expression)
        Channel volume pedal mode: Control channel 7 (Channel volume)
        Foot controller pedal mode: Control channel 4 (Foot controller)
    
    The touch strip does nothing here at the moment, still need to add support for that.	


- License -
This project is licensed under the MIT License. See LICENSE for details.
