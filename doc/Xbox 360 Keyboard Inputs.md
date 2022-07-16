# Xbox 360 Rock Band 3 Keyboard Inputs

Thanks to this project for pointing me in the right direction in my initial research:
https://github.com/bearzly/RockBandPiano

## Controller Info

- Type: Gamepad (1)
- Subtype: 15, not part of XInput standards
- Vendor ID: `0x1BAD`
- Product ID: `0x0719`

## Input Info

Face buttons work like a standard Xbox 360 controller.

Binary representations key:

- `x` = This bit doesn't matter.
- `H` = This bit is active when equal to 1.
- `L` = This bit is active when equal to 0.

### Keys

The keys are sent as a bitmask, which gets split up across several of the XInput axes.

In the following table, C1 is the leftmost key, C3 is the rightmost key.

| Key | Input         | Bits                     |
| :-- | :----         | :--:                     |
| C1  | Left trigger  | `0b_Hxxx_xxxx`           |
| Db1 | Left trigger  | `0b_xHxx_xxxx`           |
| D1  | Left trigger  | `0b_xxHx_xxxx`           |
| Eb1 | Left trigger  | `0b_xxxH_xxxx`           |
| E1  | Left trigger  | `0b_xxxx_Hxxx`           |
| F1  | Left trigger  | `0b_xxxx_xHxx`           |
| Gb1 | Left trigger  | `0b_xxxx_xxHx`           |
| G1  | Left trigger  | `0b_xxxx_xxxH`           |
|     |               |                          |
| Ab1 | Right trigger | `0b_Hxxx_xxxx`           |
| A1  | Right trigger | `0b_xHxx_xxxx`           |
| Bb1 | Right trigger | `0b_xxHx_xxxx`           |
| B1  | Right trigger | `0b_xxxH_xxxx`           |
| C2  | Right trigger | `0b_xxxx_Hxxx`           |
| Db2 | Right trigger | `0b_xxxx_xHxx`           |
| D2  | Right trigger | `0b_xxxx_xxHx`           |
| Eb2 | Right trigger | `0b_xxxx_xxxH`           |
|     |               |                          |
| E2  | Left stick X  | `0b_xxxx_xxxx_Hxxx_xxxx` |
| F2  | Left stick X  | `0b_xxxx_xxxx_xHxx_xxxx` |
| Gb2 | Left stick X  | `0b_xxxx_xxxx_xxHx_xxxx` |
| G2  | Left stick X  | `0b_xxxx_xxxx_xxxH_xxxx` |
| Ab2 | Left stick X  | `0b_xxxx_xxxx_xxxx_Hxxx` |
| A2  | Left stick X  | `0b_xxxx_xxxx_xxxx_xHxx` |
| Bb2 | Left stick X  | `0b_xxxx_xxxx_xxxx_xxHx` |
| B2  | Left stick X  | `0b_xxxx_xxxx_xxxx_xxxH` |
|     |               |                          |
| C3  | Left stick X  | `0b_Hxxx_xxxx_xxxx_xxxx` |

### Velocity

Velocities also get registered across several of the XInput axes.

| Key     | Input         | Bits                     |
| :--     | :----         | :--:                     |
| 1st key | Left stick X  | `0b_xHHH_HHHH_xxxx_xxxx` |
| 2nd key | Left stick Y  | `0b_xxxx_xxxx_xHHH_HHHH` |
| 3rd key | Left stick Y  | `0b_xHHH_HHHH_xxxx_xxxx` |
| 4th key | Right stick X | `0b_xxxx_xxxx_xHHH_HHHH` |
| 5th key | Right stick X | `0b_xHHH_HHHH_xxxx_xxxx` |

When a key is pressed or released, if there are 5 or less keys pressed, the velocities of the currently pressed keys will be assigned from left to right into the velocity slots. If there are more than 5 keys pressed, the velocity values do not change from what they were previously, and the velocity of the 6th+ keys pressed will not register until one of the other keys is released, at which point it will register as a velocity of 64.

There is an issue, at least on my keyboard, where if a key is pressed, then released only slightly such that it stops registering, then pressed again, it will not register a velocity whatsoever. This can wreak havoc on velocity recognition and is probably difficult, if not impossible, to account for.

### Other

Overdrive button: Right Stick Y, `0b_xxxx_xxxx_Hxxx_xxxx`

Pedal port digital input: Right Stick Y, `0b_Hxxx_xxxx_xxxx_xxxx`

Pedal port analog input: Right Stick Y, `0b_xLLL_LLLL_xxxx_xxxx`

- Not entirely sure if this is actually an active-low value. I don't have an actual analog pedal to test with, so I used a pair of headphones with a built-in microphone plus a splitter that splits the input and output into separate jacks.
- These bits are all 1 when nothing is plugged in.

Effects touchpad: Present in a byte beyond the standard XUSB data. Likely requires interfacing with the XUSB driver directly through IOCTL functions, or hacking into the [OpenXinput](https://github.com/Nemirtingas/OpenXinput) project to make an XInput function that provides extended input data.
