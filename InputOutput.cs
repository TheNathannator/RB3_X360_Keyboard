using System;
using System.Threading;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using WindowsInput.Native; 
using WindowsInput;
using keyCode = WindowsInput.Native.VirtualKeyCode;

/*
This code is not all that great lol
It's just kinda put together in a way such that it'll at the very least work.
I'll try and optimize and clean it up later once I learn more about C#.
*/

namespace InputOutput
{
	/// <summary>
	/// XInput reading code.
	/// </summary>
	public class Input
	{
		SharpDX.XInput.Controller inputController = null;
		SharpDX.XInput.Gamepad inputGamepad;
		State previousState;
		State currentState;
		public bool connected = false;
		// TODO: figure out how to read the bitmasks in a better manner
		public int[] range = new int[4];
		public static bool[] key = new bool[25];
		public static int[] vel = new int[5];
		public static int modulator;
		public static bool overdrive;
		public static bool pedal;
		public static bool btnA, btnB, btnX, btnY, btnLB, btnRB, btnLS, btnRS, btnSt, btnBk;
		public static bool dpadU, dpadD, dpadL, dpadR;

		/// <summary>
		/// Enum for bitmask constants.
		/// </summary>
		[Flags]
		public enum Bits
		{
			bit1 = 0x1,     bit2 = 0x2,     bit3 = 0x4,     bit4 = 0x8,
			bit5 = 0x10,    bit6 = 0x20,    bit7 = 0x40,    bit8 = 0x80,
			bit9 = 0x100,   bit10 = 0x200,  bit11 = 0x400,  bit12 = 0x800,
			bit13 = 0x1000, bit14 = 0x2000, bit15 = 0x4000, bit16 = 0x8000
		}
	
		/// <summary>
		/// Initializes XInput polling.
		/// </summary>
		public void Initialize()
		{
			var controllers = new[]
			{
				new Controller(UserIndex.One),
				new Controller(UserIndex.Two),
				new Controller(UserIndex.Three),
				new Controller(UserIndex.Four)
			};

			foreach (var selectController in controllers)
			{
				if (selectController.IsConnected)
				{
					inputController = selectController;
					break;
				}
			}

			if (inputController == null)
			{
				Console.WriteLine("No XInput controllers detected");
			}
			else
			{
				previousState = inputController.GetState();
				currentState  = inputController.GetState();
			}
		}
		
		/// <summary>
		/// Polls the keyboard's inputs.
		/// </summary>
		public void Poll()
		{
			currentState = inputController.GetState();
			inputGamepad = currentState.Gamepad;

			// TODO: figure out how to read the bitmasks in an efficient manner.
			// I'm doing everything individually just so I have something that works.
			if(currentState.PacketNumber != previousState.PacketNumber)
			{
				range[0] = inputGamepad.LeftTrigger;
				range[1] = inputGamepad.RightTrigger;
				range[2] = inputGamepad.LeftThumbX & 0xFF;
				range[3] = inputGamepad.LeftThumbX & 0x8000;

				vel[0] = inputGamepad.LeftThumbX & 0xFF00;
				vel[1] = inputGamepad.LeftThumbY & 0xFF;
				vel[2] = inputGamepad.LeftThumbY & 0xFF00;
				vel[3] = inputGamepad.RightThumbX & 0xFF;
				vel[4] = inputGamepad.RightThumbX & 0xFF00;

				overdrive = (inputGamepad.RightThumbY & 0xFF) == 0xFF;
				pedal = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

				key[0]  = (range[0] & (int)Bits.bit1)  == (int)Bits.bit1;	// C1	= inputGamepad.LeftTrigger & 0x1
				key[1]  = (range[0] & (int)Bits.bit2)  == (int)Bits.bit2;	// C#1	= inputGamepad.LeftTrigger & 0x2
				key[2]  = (range[0] & (int)Bits.bit3)  == (int)Bits.bit3;	// D1	= inputGamepad.LeftTrigger & 0x4
				key[3]  = (range[0] & (int)Bits.bit4)  == (int)Bits.bit4;	// D#1	= inputGamepad.LeftTrigger & 0x8
				key[4]  = (range[0] & (int)Bits.bit5)  == (int)Bits.bit5;	// E1	= inputGamepad.LeftTrigger & 0x10
				key[5]  = (range[0] & (int)Bits.bit6)  == (int)Bits.bit6;	// F1	= inputGamepad.LeftTrigger & 0x20
				key[6]  = (range[0] & (int)Bits.bit7)  == (int)Bits.bit7;	// F#1	= inputGamepad.LeftTrigger & 0x40
				key[7]  = (range[0] & (int)Bits.bit8)  == (int)Bits.bit8;	// G1	= inputGamepad.LeftTrigger & 0x80

				key[8]  = (range[1] & (int)Bits.bit1)  == (int)Bits.bit1;	// G#1	= inputGamepad.RightTrigger & 0x1
				key[9]  = (range[1] & (int)Bits.bit2)  == (int)Bits.bit2;	// A1	= inputGamepad.RightTrigger & 0x2
				key[10] = (range[1] & (int)Bits.bit3)  == (int)Bits.bit3;	// A#1	= inputGamepad.RightTrigger & 0x4
				key[11] = (range[1] & (int)Bits.bit4)  == (int)Bits.bit4;	// B1	= inputGamepad.RightTrigger & 0x8
				key[12] = (range[1] & (int)Bits.bit5)  == (int)Bits.bit5;	// C2	= inputGamepad.RightTrigger & 0x10
				key[13] = (range[1] & (int)Bits.bit6)  == (int)Bits.bit6;	// C#2	= inputGamepad.RightTrigger & 0x20
				key[14] = (range[1] & (int)Bits.bit7)  == (int)Bits.bit7;	// D2	= inputGamepad.RightTrigger & 0x40
				key[15] = (range[1] & (int)Bits.bit8)  == (int)Bits.bit8;	// D#2	= inputGamepad.RightTrigger & 0x80

				key[16] = (range[2] & (int)Bits.bit1)  == (int)Bits.bit1;	// E2	= inputGamepad.LeftThumbX & 0x1
				key[17] = (range[2] & (int)Bits.bit2)  == (int)Bits.bit2;	// F2	= inputGamepad.LeftThumbX & 0x2
				key[18] = (range[2] & (int)Bits.bit3)  == (int)Bits.bit3;	// F#2	= inputGamepad.LeftThumbX & 0x4
				key[19] = (range[2] & (int)Bits.bit4)  == (int)Bits.bit4;	// G2	= inputGamepad.LeftThumbX & 0x8
				key[20] = (range[2] & (int)Bits.bit5)  == (int)Bits.bit5;	// G#2	= inputGamepad.LeftThumbX & 0x10
				key[21] = (range[2] & (int)Bits.bit6)  == (int)Bits.bit6;	// A2	= inputGamepad.LeftThumbX & 0x20
				key[22] = (range[2] & (int)Bits.bit7)  == (int)Bits.bit7;	// A#2	= inputGamepad.LeftThumbX & 0x40
				key[23] = (range[2] & (int)Bits.bit8)  == (int)Bits.bit8;	// B2	= inputGamepad.LeftThumbX & 0x80

				key[24] = (range[3] & (int)Bits.bit16) == (int)Bits.bit16;	// C2	= inputGamepad.LeftThumbX & 0x8000

				dpadU = ((int)inputGamepad.Buttons & (int)Bits.bit1)  == (int)Bits.bit1;	// XINPUT_GAMEPAD_DPAD_UP        = state & 0x0001
				dpadD = ((int)inputGamepad.Buttons & (int)Bits.bit2)  == (int)Bits.bit2;	// XINPUT_GAMEPAD_DPAD_DOWN      = state & 0x0002
				dpadL = ((int)inputGamepad.Buttons & (int)Bits.bit3)  == (int)Bits.bit3;	// XINPUT_GAMEPAD_DPAD_LEFT      = state & 0x0004
				dpadR = ((int)inputGamepad.Buttons & (int)Bits.bit4)  == (int)Bits.bit4;	// XINPUT_GAMEPAD_DPAD_RIGHT     = state & 0x0008

				btnSt = ((int)inputGamepad.Buttons & (int)Bits.bit5)  == (int)Bits.bit5;	// XINPUT_GAMEPAD_START          = state & 0x0010
				btnBk = ((int)inputGamepad.Buttons & (int)Bits.bit6)  == (int)Bits.bit6;	// XINPUT_GAMEPAD_BACK           = state & 0x0020

				btnLS = ((int)inputGamepad.Buttons & (int)Bits.bit7)  == (int)Bits.bit7;	// XINPUT_GAMEPAD_LEFT_THUMB     = state & 0x0040
				btnRS = ((int)inputGamepad.Buttons & (int)Bits.bit8)  == (int)Bits.bit8;	// XINPUT_GAMEPAD_RIGHT_THUMB    = state & 0x0080
				btnLB = ((int)inputGamepad.Buttons & (int)Bits.bit9)  == (int)Bits.bit9;	// XINPUT_GAMEPAD_LEFT_SHOULDER  = state & 0x0100
				btnRB = ((int)inputGamepad.Buttons & (int)Bits.bit10) == (int)Bits.bit10;	// XINPUT_GAMEPAD_RIGHT_SHOULDER = state & 0x0200

				btnA  = ((int)inputGamepad.Buttons & (int)Bits.bit13) == (int)Bits.bit13;	// XINPUT_GAMEPAD_A              = state & 0x1000
				btnB  = ((int)inputGamepad.Buttons & (int)Bits.bit14) == (int)Bits.bit14;	// XINPUT_GAMEPAD_B              = state & 0x2000
				btnX  = ((int)inputGamepad.Buttons & (int)Bits.bit15) == (int)Bits.bit15;	// XINPUT_GAMEPAD_X              = state & 0x4000
				btnY  = ((int)inputGamepad.Buttons & (int)Bits.bit16) == (int)Bits.bit16;	// XINPUT_GAMEPAD_Y              = state & 0x8000

				previousState = currentState;
			}
			Thread.Sleep(1);
		}
	}

	class Output
	{
		/// <summary>
		///  Output code (ViGEmBus, keypresses, MIDI notes).
		/// </summary>

		static IXbox360Controller outputController;
		static IKeyboardSimulator keyOut;
		static ViGEmClient vigem;

		/// <summary>
		/// Initializes the ViGEmBus client.
		/// </summary>
		static void InitializeViGEmBus(string[] args)
		{
			outputController = vigem.CreateXbox360Controller();
			outputController.Connect();
		}

		/// <summary>
		/// Output code for ViGEmBus.
		/// </summary>
		public void ViGEmBus()
		{
			outputController.SetButtonState(Xbox360Button.A,	//	C1, C2, A = A
				Input.key[0]  ||
				Input.key[12] ||
				Input.btnA);

			outputController.SetButtonState(Xbox360Button.B,	//	D1, D2, B = B
				Input.key[2]  ||
				Input.key[14] ||
				Input.btnB);

			outputController.SetButtonState(Xbox360Button.Y,	//	E1, E2, Y = Y
				Input.key[4]  ||
				Input.key[16] ||
				Input.btnY);

			outputController.SetButtonState(Xbox360Button.X,	//	F1, F2, X = X
				Input.key[5]  ||
				Input.key[17] ||
				Input.btnX);


			outputController.SetButtonState(Xbox360Button.LeftShoulder, 	//	G1, G2, LB, pedal = LB
				Input.key[7]  ||
				Input.key[19] ||
				Input.btnLB   ||
				Input.pedal);

			outputController.SetButtonState(Xbox360Button.RightShoulder,	//	A1, A2, RB = RB
				Input.key[9]  ||
				Input.key[21] ||
				Input.btnRB);


			outputController.SetButtonState(Xbox360Button.Start,	//	Start = Start
				Input.btnSt);	

			outputController.SetButtonState(Xbox360Button.Back, 	//	OD button, Back = Back
				Input.overdrive ||
				Input.btnBk);				


			outputController.SetButtonState(Xbox360Button.Up,		//	D-pad Up = D-pad Up
				Input.dpadU);

			outputController.SetButtonState(Xbox360Button.Down, 	//	D-pad Down = D-pad Down
				Input.dpadD);

			outputController.SetButtonState(Xbox360Button.Left, 	//	D-pad Left = D-pad Left
				Input.dpadL);

			outputController.SetButtonState(Xbox360Button.Right,	//	D-pad Right = D-pad Right
				Input.dpadR);


			Thread.Sleep(1);
		}

		static void Keyboard()
		{
			/*
			C1  = Z
			C#1 = S
			D1  = X
			D#1 = D
			E1  = C
			F1  = V
			F#1 = G
			G1  = B
			G#1 = H
			A1  = N
			A#1 = J
			B1  = M
			C2  = Q
			C#2 = 2
			D2  = W
			D#2 = 3
			E2  = E
			F2  = R
			F#2 = 5
			G2  = T
			G#2 = 6
			A2  = Y
			A#2 = 7
			B2  = U
			C3  = I
			*/

			// keyOut.KeyDown();


			//	switch ()
			//	{
			//	case
			//	default: break;
			//	}
		}

		static void Midi()
		{
		
		}
	}
}