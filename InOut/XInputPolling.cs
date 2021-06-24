using System;
//	using System.Threading;
using System.Runtime.InteropServices;
using SharpDX.XInput;

/*
This code is not all that great lol
It's just kinda put together in a way such that it'll at the very least work.
I'll try and optimize and clean it up later once I learn more about C#.
*/

namespace InOut
{
	/// <summary>
	/// Class representing the state of a keyboard.
	/// </summary>
	public class InputState
	{
		/// <summary>
		/// Array of the four ranges the keyboard splits its key bitmask into.
		/// </summary>
		public int[] range = new int[4];

		/// <summary>
		/// Array of the keyboard's bitmask.
		/// </summary>
		/// <remarks>
		/// <para>Indexing:</para>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public bool[] key = new bool[25];

		/// <summary>
		/// Array of the keyboard's five velocity values,
		/// plus a constant 100 value for any additional keys pressed.
		/// </summary>
		public byte[] vel = 
		{
			0, 0, 0, 0, 0,
			100
		};

		/// <summary>
		/// Array representing the velocity of a specified key.
		/// </summary>
		/// <remarks>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public byte[] velocity = new byte[25];

		/// <summary>
		/// Integer representing the state of the touch modulator. 
		/// </summary>
		/// <remarks>
		/// Unused because I haven't found the input it corresponds to yet, it's hidden like the Guide button is.
		/// </remarks>
		public int modulator;

		/// <summary>
		/// Boolean representing the overdrive button.
		/// </summary>
		public bool overdrive;

		/// <summary>
		/// Boolean representing the pedal port.
		/// </summary>
		public bool pedal;

		/// <summary>
		/// Booleans representing the face buttons.
		/// </summary>
		public bool btnA, btnB, btnX, btnY, btnSt, btnBk, btnGuide;

		/// <summary>
		/// Booleans representing the D-pad.
		/// </summary>
		public bool dpadU, dpadD, dpadL, dpadR;
	}

	/// <summary>
	/// Class containing XInput device reading code.
	/// </summary>
	public class Input
	{
		/// <summary>
		/// Class representing the currently connected controller.
		/// </summary>
		Controller inputController;
		/// <summary>
		/// Struct representing the current state of the current controller.
		/// </summary>
		Gamepad inputGamepad;
		//	Thread inputThread;
	
		/// <summary>
		/// Initialize XInput polling.
		/// </summary>
		/// <returns>
		/// True if connection was successful, false if no controllers were found.
		/// </returns>
		public bool InitializePoll(int indexSelect)
		{
			var controllers = new[]
			{
				new Controller(UserIndex.One),
				new Controller(UserIndex.Two),
				new Controller(UserIndex.Three),
				new Controller(UserIndex.Four)
			};

			Math.Clamp(indexSelect, 0, 4);

			if(indexSelect == 0)
			{
				inputController = null;
			}
			else
			{
				if (controllers[indexSelect].IsConnected)
				{
					inputController = controllers[indexSelect];
				}
			}

			if (inputController == null)
			{
				return false;
			}
			else
			{
				// Get an initial state
				previousState = inputController.GetState();
				return true;
			}

			//	inputThread = new System.Threading.Thread(new System.Threading.ThreadStart(Poll));
            //	inputThread.Start();
		}

		// TODO: figure out how to read the bitmasks in a better manner

		/// <summary>
		/// Array of bitmask constants.
		/// </summary>
		public int[] Bits = 
		{
			0x1,    0x2,    0x4,    0x8,
			0x10,   0x20,   0x40,   0x80,
			0x100,  0x200,  0x400,  0x800,
			0x1000, 0x2000, 0x4000, 0x8000
		};
		State previousState;
		State state;
		InputState prevState;
		InputState currentState;
		
		/// <summary>
		/// Poll the keyboard's inputs.
		/// </summary>
		public void Poll()
		{
			// TODO: figure out how to read the bitmasks in an efficient manner.
			// Converting to 4 ints and then into booleans probably isn't the most efficient thing I can do.

			// TODO (Done): Track which notes are the first 5 so I can do MIDI output velocity properly.

			// Get the current gamepad state.
			state = inputController.GetState();
			inputGamepad = state.Gamepad;

			// Store the previous allocated input state.
			prevState = currentState;

			// Allocate the key ranges from their respective sources.
			currentState.range[0] = inputGamepad.LeftTrigger;
			currentState.range[1] = inputGamepad.RightTrigger;
			currentState.range[2] = inputGamepad.LeftThumbX & 0xFF;
			currentState.range[3] = (inputGamepad.LeftThumbX & 0x8000);

			// Get the velocity values for the first 5 held keys.
			currentState.vel[0] = (byte)((inputGamepad.LeftThumbX  & 0xFF00) >> 8);	// shift right 8 because it's just the top 8 that are needed here
			currentState.vel[1] = (byte)( inputGamepad.LeftThumbY  & 0xFF        );	// no shift since it's just the bottom 8 that are needed here
			currentState.vel[2] = (byte)((inputGamepad.LeftThumbY  & 0xFF00) >> 8);
			currentState.vel[3] = (byte)( inputGamepad.RightThumbX & 0xFF        );
			currentState.vel[4] = (byte)((inputGamepad.RightThumbX & 0xFF00) >> 8);

			// Convert the key ranges into key booleans for ease of use.
			int o = 0;	// key array offset
			int v = 0;	// vel array index
			for(int r = 0; r < 4; r++)	// loop through the ranges
			{
				for(int i = 0; i < 8; i++)	// loop through the keys
				{
					currentState.key[o + i]  = (currentState.range[r] & Bits[i]) == Bits[i];
					if(currentState.key[o + i] != prevState.key[o + i])
					{
						currentState.velocity[o + i] = currentState.vel[v];
						Math.Clamp(++v, 0, 5);
					}
				}
				o += 8;
			}

			// old code, only keeping in case the above doesn't work and i can't make it work properly
			//	currentState.key[0]  = (currentState.range[0] & Bits[0])  == Bits[0]; 	// C1  = inputGamepad.LeftTrigger & 0x1
			//	currentState.key[1]  = (currentState.range[0] & Bits[1])  == Bits[1]; 	// C#1 = inputGamepad.LeftTrigger & 0x2
			//	currentState.key[2]  = (currentState.range[0] & Bits[2])  == Bits[2]; 	// D1  = inputGamepad.LeftTrigger & 0x4
			//	currentState.key[3]  = (currentState.range[0] & Bits[3])  == Bits[3]; 	// D#1 = inputGamepad.LeftTrigger & 0x8
			//	currentState.key[4]  = (currentState.range[0] & Bits[4])  == Bits[4]; 	// E1  = inputGamepad.LeftTrigger & 0x10
			//	currentState.key[5]  = (currentState.range[0] & Bits[5])  == Bits[5]; 	// F1  = inputGamepad.LeftTrigger & 0x20
			//	currentState.key[6]  = (currentState.range[0] & Bits[6])  == Bits[6]; 	// F#1 = inputGamepad.LeftTrigger & 0x40
			//	currentState.key[7]  = (currentState.range[0] & Bits[7])  == Bits[7]; 	// G1  = inputGamepad.LeftTrigger & 0x80
			//	
			//	currentState.key[8]  = (currentState.range[1] & Bits[0])  == Bits[0]; 	// G#1 = inputGamepad.RightTrigger & 0x1
			//	currentState.key[9]  = (currentState.range[1] & Bits[1])  == Bits[1]; 	// A1  = inputGamepad.RightTrigger & 0x2
			//	currentState.key[10] = (currentState.range[1] & Bits[2])  == Bits[2]; 	// A#1 = inputGamepad.RightTrigger & 0x4
			//	currentState.key[11] = (currentState.range[1] & Bits[3])  == Bits[3]; 	// B1  = inputGamepad.RightTrigger & 0x8
			//	currentState.key[12] = (currentState.range[1] & Bits[4])  == Bits[4]; 	// C2  = inputGamepad.RightTrigger & 0x10
			//	currentState.key[13] = (currentState.range[1] & Bits[5])  == Bits[5]; 	// C#2 = inputGamepad.RightTrigger & 0x20
			//	currentState.key[14] = (currentState.range[1] & Bits[6])  == Bits[6]; 	// D2  = inputGamepad.RightTrigger & 0x40
			//	currentState.key[15] = (currentState.range[1] & Bits[7])  == Bits[7]; 	// D#2 = inputGamepad.RightTrigger & 0x80
			//	
			//	currentState.key[16] = (currentState.range[2] & Bits[0])  == Bits[0]; 	// E2  = inputGamepad.LeftThumbX & 0x1
			//	currentState.key[17] = (currentState.range[2] & Bits[1])  == Bits[1]; 	// F2  = inputGamepad.LeftThumbX & 0x2
			//	currentState.key[18] = (currentState.range[2] & Bits[2])  == Bits[2]; 	// F#2 = inputGamepad.LeftThumbX & 0x4
			//	currentState.key[19] = (currentState.range[2] & Bits[3])  == Bits[3]; 	// G2  = inputGamepad.LeftThumbX & 0x8
			//	currentState.key[20] = (currentState.range[2] & Bits[4])  == Bits[4]; 	// G#2 = inputGamepad.LeftThumbX & 0x10
			//	currentState.key[21] = (currentState.range[2] & Bits[5])  == Bits[5]; 	// A2  = inputGamepad.LeftThumbX & 0x20
			//	currentState.key[22] = (currentState.range[2] & Bits[6])  == Bits[6]; 	// A#2 = inputGamepad.LeftThumbX & 0x40
			//	currentState.key[23] = (currentState.range[2] & Bits[7])  == Bits[7]; 	// B2  = inputGamepad.LeftThumbX & 0x80
			//	
			//	currentState.key[24] = (currentState.range[3] & Bits[15]) == Bits[15];	// C2  = inputGamepad.LeftThumbX & 0x8000


			// Get the state of the overdrive button and the digital part of the pedal port.
			currentState.overdrive = (inputGamepad.RightThumbY & 0xFF)   == 0xFF;
			currentState.pedal     = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

			// Get the state of the face buttons.
			currentState.btnA  = ((int)inputGamepad.Buttons & Bits[13]) == Bits[13];	// XINPUT_GAMEPAD_A = state.Buttons & 0x1000
			currentState.btnB  = ((int)inputGamepad.Buttons & Bits[14]) == Bits[14];	// XINPUT_GAMEPAD_B = state.Buttons & 0x2000
			currentState.btnX  = ((int)inputGamepad.Buttons & Bits[15]) == Bits[15];	// XINPUT_GAMEPAD_X = state.Buttons & 0x4000
			currentState.btnY  = ((int)inputGamepad.Buttons & Bits[16]) == Bits[16];	// XINPUT_GAMEPAD_Y = state.Buttons & 0x8000

			currentState.dpadU = ((int)inputGamepad.Buttons & Bits[1])  == Bits[1]; 	// XINPUT_GAMEPAD_DPAD_UP    = state.Buttons & 0x0001
			currentState.dpadD = ((int)inputGamepad.Buttons & Bits[2])  == Bits[2]; 	// XINPUT_GAMEPAD_DPAD_DOWN  = state.Buttons & 0x0002
			currentState.dpadL = ((int)inputGamepad.Buttons & Bits[3])  == Bits[3]; 	// XINPUT_GAMEPAD_DPAD_LEFT  = state.Buttons & 0x0004
			currentState.dpadR = ((int)inputGamepad.Buttons & Bits[4])  == Bits[4]; 	// XINPUT_GAMEPAD_DPAD_RIGHT = state.Buttons & 0x0008

			currentState.btnSt = ((int)inputGamepad.Buttons & Bits[5])  == Bits[5]; 	// XINPUT_GAMEPAD_START = state.Buttons & 0x0010
			currentState.btnBk = ((int)inputGamepad.Buttons & Bits[6])  == Bits[6]; 	// XINPUT_GAMEPAD_BACK  = state.Buttons & 0x0020

			currentState.btnGuide = testHomeButton();

			
			
		}

		// Took the following code from reply #10 of https://forums.tigsource.com/index.php?topic=26792.0
		// I don't necessarily *need* the guide button, but the keyboard's MIDI mode uses it, so why not lol

		[DllImport("xinput1_4.dll", EntryPoint = "#100")]
		static extern int secret_get_gamepad(int playerIndex, out XINPUT_GAMEPAD_SECRET struc);

		public struct XINPUT_GAMEPAD_SECRET
		{
			public UInt32 eventCount;
			public ushort wButtons;
			public Byte bLeftTrigger;
			public Byte bRightTrigger;
			public short sThumbLX;
			public short sThumbLY;
			public short sThumbRX;
			public short sThumbRY;
		}

		public XINPUT_GAMEPAD_SECRET xgs;

		public bool testHomeButton()
		{
			int stat;
			bool value;

			for (int i = 0; i < 4; i++)
			{
				stat = secret_get_gamepad(0, out xgs);

				if (stat != 0) continue;

				value = ((xgs.wButtons & 0x0400) != 0);

				if (value) return true;
			}
			return false;
		}
	}
}
