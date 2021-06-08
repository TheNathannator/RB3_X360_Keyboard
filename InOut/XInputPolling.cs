using System;
using SharpDX.XInput;
/*
This code is not all that great lol
It's just kinda put together in a way such that it'll at the very least work.
I'll try and optimize and clean it up later once I learn more about C#.
*/

//	planning out code that calls the input and output functions in other files
//	/// <summary>
//	///  Calls the input and output code.
//	/// </summary>
//	public class h
//	{
//		static void Initialize()
//		{
//			InputOutput.Input.Initialize();
//			InputOutput.Output.InitializeViGEmBus();			
//		}
//
//		public int outputType = 0;
//		static void Loop()
//		{
//			InputOutput.Input.Poll
//			switch (outputType)
//			{
//				case 1:	InputOutput.Output.ViGEmBus();
//				case 2:	InputOutput.Output.Keyboard();
//				case 3: InputOutput.Output.Midi();
//				default: break;
//			}
//			Thread.Sleep(1);
//		}
//	}

namespace InOut
{
	/// <summary>
	/// XInput reading code.
	/// </summary>

	public class InputState
	{
		public int[] range = new int[4];
		public bool[] key = new bool[25];
		//	Indexing:
		//	C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,
		//	F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,
		//	C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,
		//	F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24
		public byte[] vel = new byte[5];
		public int modulator;
		public bool overdrive;
		public bool pedal;
		public bool btnA, btnB, btnX, btnY, btnLB, btnRB, btnLS, btnRS, btnSt, btnBk;
		public bool dpadU, dpadD, dpadL, dpadR;
	}

	public class Input
	{
		SharpDX.XInput.Controller inputController = null;
		SharpDX.XInput.Gamepad inputGamepad;
		State previousState;
		State state;
		InputState prevState;
		InputState currentState;
		public bool connected = false;
		// TODO: figure out how to read the bitmasks in a better manner

		/// <summary>
		/// Enum for bitmask constants.
		/// </summary>
		[Flags]
		public enum Bits
		{
			bit1  = 0x1,    bit2  = 0x2,    bit3  = 0x4,    bit4  = 0x8,
			bit5  = 0x10,   bit6  = 0x20,   bit7  = 0x40,   bit8  = 0x80,
			bit9  = 0x100,  bit10 = 0x200,  bit11 = 0x400,  bit12 = 0x800,
			bit13 = 0x1000, bit14 = 0x2000, bit15 = 0x4000, bit16 = 0x8000
		}
	
		/// <summary>
		/// Initializes XInput polling.
		/// </summary>
		public void InitializeXInput()
		{
			var controllers = new[]
			{
				new Controller(UserIndex.One),
				new Controller(UserIndex.Two),
				new Controller(UserIndex.Three),
				new Controller(UserIndex.Four)
			};

			foreach (var controller in controllers)
			{
				if (controller.IsConnected)
				{
					inputController = controller;
					break;
				}
			}

			if (controllers == null)
			{
				Console.WriteLine("No XInput controllers detected");
			}
			else
			{
				previousState = inputController.GetState();
				state  = inputController.GetState();
			}
		}
		
		/// <summary>
		/// Polls the keyboard's inputs.
		/// </summary>
		public void Poll()
		{
			// TODO: figure out how to read the bitmasks in an efficient manner.
			// I'm doing everything individually just so I have something that works.

			// TODO: Track which notes are the first 5 so I can do MIDI output properly

			// TODO: Just sending inputs based on the current state is naive and probably won't work correctly.
			// I need to keep track of both the previous state and the current state,
			// and only send inputs when the input being checked for actually changes.

			// Get the current gamepad state.
			state = inputController.GetState();
			inputGamepad = state.Gamepad;

			// Check if the state's changed, to prevent unnecessary polling of inputs.
			if(state.PacketNumber != previousState.PacketNumber)
			{
				currentState = prevState;
				// Allocate the key ranges from their respective sources.
				currentState.range[0] = inputGamepad.LeftTrigger;
				currentState.range[1] = inputGamepad.RightTrigger;
				currentState.range[2] = inputGamepad.LeftThumbX & 0xFF;
				currentState.range[3] = (inputGamepad.LeftThumbX & 0x8000);

				// Get the velocity values for the first 5 held keys.
				currentState.vel[0] = (byte)((inputGamepad.LeftThumbX  & 0xFF00) >> 8);	// shift right 8 because it's just the top 8 that are needed here
				currentState.vel[1] = (byte)( inputGamepad.LeftThumbY  & 0xFF);			// no shift since it's just the bottom 8 that are needed here
				currentState.vel[2] = (byte)((inputGamepad.LeftThumbY  & 0xFF00) >> 8);
				currentState.vel[3] = (byte)( inputGamepad.RightThumbX & 0xFF);
				currentState.vel[4] = (byte)((inputGamepad.RightThumbX & 0xFF00) >> 8);

				// Get the state of the overdrive button and pedal port.
				currentState.overdrive = (inputGamepad.RightThumbY & 0xFF) == 0xFF;
				currentState.pedal = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

				// Convert the key ranges into key booleans for ease of use.
				// i need to learn how to use bitmasks more effectively lol
				currentState.key[0]  = (currentState.range[0] & (int)Bits.bit1)  == (int)Bits.bit1; 	// C1  = inputGamepad.LeftTrigger & 0x1
				currentState.key[1]  = (currentState.range[0] & (int)Bits.bit2)  == (int)Bits.bit2; 	// C#1 = inputGamepad.LeftTrigger & 0x2
				currentState.key[2]  = (currentState.range[0] & (int)Bits.bit3)  == (int)Bits.bit3; 	// D1  = inputGamepad.LeftTrigger & 0x4
				currentState.key[3]  = (currentState.range[0] & (int)Bits.bit4)  == (int)Bits.bit4; 	// D#1 = inputGamepad.LeftTrigger & 0x8
				currentState.key[4]  = (currentState.range[0] & (int)Bits.bit5)  == (int)Bits.bit5; 	// E1  = inputGamepad.LeftTrigger & 0x10
				currentState.key[5]  = (currentState.range[0] & (int)Bits.bit6)  == (int)Bits.bit6; 	// F1  = inputGamepad.LeftTrigger & 0x20
				currentState.key[6]  = (currentState.range[0] & (int)Bits.bit7)  == (int)Bits.bit7; 	// F#1 = inputGamepad.LeftTrigger & 0x40
				currentState.key[7]  = (currentState.range[0] & (int)Bits.bit8)  == (int)Bits.bit8; 	// G1  = inputGamepad.LeftTrigger & 0x80

				currentState.key[8]  = (currentState.range[1] & (int)Bits.bit1)  == (int)Bits.bit1; 	// G#1 = inputGamepad.RightTrigger & 0x1
				currentState.key[9]  = (currentState.range[1] & (int)Bits.bit2)  == (int)Bits.bit2; 	// A1  = inputGamepad.RightTrigger & 0x2
				currentState.key[10] = (currentState.range[1] & (int)Bits.bit3)  == (int)Bits.bit3; 	// A#1 = inputGamepad.RightTrigger & 0x4
				currentState.key[11] = (currentState.range[1] & (int)Bits.bit4)  == (int)Bits.bit4; 	// B1  = inputGamepad.RightTrigger & 0x8
				currentState.key[12] = (currentState.range[1] & (int)Bits.bit5)  == (int)Bits.bit5; 	// C2  = inputGamepad.RightTrigger & 0x10
				currentState.key[13] = (currentState.range[1] & (int)Bits.bit6)  == (int)Bits.bit6; 	// C#2 = inputGamepad.RightTrigger & 0x20
				currentState.key[14] = (currentState.range[1] & (int)Bits.bit7)  == (int)Bits.bit7; 	// D2  = inputGamepad.RightTrigger & 0x40
				currentState.key[15] = (currentState.range[1] & (int)Bits.bit8)  == (int)Bits.bit8; 	// D#2 = inputGamepad.RightTrigger & 0x80
 
				currentState.key[16] = (currentState.range[2] & (int)Bits.bit1)  == (int)Bits.bit1; 	// E2  = inputGamepad.LeftThumbX & 0x1
				currentState.key[17] = (currentState.range[2] & (int)Bits.bit2)  == (int)Bits.bit2; 	// F2  = inputGamepad.LeftThumbX & 0x2
				currentState.key[18] = (currentState.range[2] & (int)Bits.bit3)  == (int)Bits.bit3; 	// F#2 = inputGamepad.LeftThumbX & 0x4
				currentState.key[19] = (currentState.range[2] & (int)Bits.bit4)  == (int)Bits.bit4; 	// G2  = inputGamepad.LeftThumbX & 0x8
				currentState.key[20] = (currentState.range[2] & (int)Bits.bit5)  == (int)Bits.bit5; 	// G#2 = inputGamepad.LeftThumbX & 0x10
				currentState.key[21] = (currentState.range[2] & (int)Bits.bit6)  == (int)Bits.bit6; 	// A2  = inputGamepad.LeftThumbX & 0x20
				currentState.key[22] = (currentState.range[2] & (int)Bits.bit7)  == (int)Bits.bit7; 	// A#2 = inputGamepad.LeftThumbX & 0x40
				currentState.key[23] = (currentState.range[2] & (int)Bits.bit8)  == (int)Bits.bit8; 	// B2  = inputGamepad.LeftThumbX & 0x80

				currentState.key[24] = (currentState.range[3] & (int)Bits.bit16) == (int)Bits.bit16;	// C2  = inputGamepad.LeftThumbX & 0x8000

				// Get the state of the face buttons.
				currentState.dpadU = ((int)inputGamepad.Buttons & (int)Bits.bit1)  == (int)Bits.bit1;	// XINPUT_GAMEPAD_DPAD_UP        = state & 0x0001
				currentState.dpadD = ((int)inputGamepad.Buttons & (int)Bits.bit2)  == (int)Bits.bit2;	// XINPUT_GAMEPAD_DPAD_DOWN      = state & 0x0002
				currentState.dpadL = ((int)inputGamepad.Buttons & (int)Bits.bit3)  == (int)Bits.bit3;	// XINPUT_GAMEPAD_DPAD_LEFT      = state & 0x0004
				currentState.dpadR = ((int)inputGamepad.Buttons & (int)Bits.bit4)  == (int)Bits.bit4;	// XINPUT_GAMEPAD_DPAD_RIGHT     = state & 0x0008

				currentState.btnSt = ((int)inputGamepad.Buttons & (int)Bits.bit5)  == (int)Bits.bit5;	// XINPUT_GAMEPAD_START          = state & 0x0010
				currentState.btnBk = ((int)inputGamepad.Buttons & (int)Bits.bit6)  == (int)Bits.bit6;	// XINPUT_GAMEPAD_BACK           = state & 0x0020

				currentState.btnLS = ((int)inputGamepad.Buttons & (int)Bits.bit7)  == (int)Bits.bit7;	// XINPUT_GAMEPAD_LEFT_THUMB     = state & 0x0040
				currentState.btnRS = ((int)inputGamepad.Buttons & (int)Bits.bit8)  == (int)Bits.bit8;	// XINPUT_GAMEPAD_RIGHT_THUMB    = state & 0x0080
				currentState.btnLB = ((int)inputGamepad.Buttons & (int)Bits.bit9)  == (int)Bits.bit9;	// XINPUT_GAMEPAD_LEFT_SHOULDER  = state & 0x0100
				currentState.btnRB = ((int)inputGamepad.Buttons & (int)Bits.bit10) == (int)Bits.bit10;	// XINPUT_GAMEPAD_RIGHT_SHOULDER = state & 0x0200

				currentState.btnA  = ((int)inputGamepad.Buttons & (int)Bits.bit13) == (int)Bits.bit13;	// XINPUT_GAMEPAD_A              = state & 0x1000
				currentState.btnB  = ((int)inputGamepad.Buttons & (int)Bits.bit14) == (int)Bits.bit14;	// XINPUT_GAMEPAD_B              = state & 0x2000
				currentState.btnX  = ((int)inputGamepad.Buttons & (int)Bits.bit15) == (int)Bits.bit15;	// XINPUT_GAMEPAD_X              = state & 0x4000
				currentState.btnY  = ((int)inputGamepad.Buttons & (int)Bits.bit16) == (int)Bits.bit16;	// XINPUT_GAMEPAD_Y              = state & 0x8000

				// Set the current state to the previous state for the next poll.
				previousState = state;
			}
		}
	}
}
