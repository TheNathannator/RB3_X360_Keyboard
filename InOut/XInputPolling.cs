using System;
using System.Threading;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using WindowsInput;
using keyCode = WindowsInput.Native.VirtualKeyCode;
using Melanchall.DryWetMidi.Devices;

/*
This code is not all that great lol
It's just kinda put together in a way such that it'll at the very least work.
I'll try and optimize and clean it up later once I learn more about C#.
*/

namespace InOut
{
	/// <summary>
	/// XInput reading code.
	/// </summary>

	//	planning out code that'll go in other files eventually
	//	public class Main
	//	{
	//		static void InitializeXInput()
	//		{
	//			InputOutput.Input.Initialize();
	//			InputOutput.Output.InitializeViGEmBus();			
	//		}
	//
	//		static void Loop()
	//		{
	//			InputOutput.Input.Poll
	//			switch (outputType)
	//			{
	//				case 1:	InputOutput.Output.ViGEmBus
	//				case 2:	InputOutput.Output.Keyboard
	//				case 3: InputOutput.Output.Midi
	//				default: break;
	//			}
	//		}
	//	}

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
		public static byte[] vel = new byte[5];
		public static int modulator;
		public static bool overdrive;
		public static bool pedal;
		public static bool	btnA, btnB, btnX, btnY, btnLB, btnRB, btnLS, btnRS, btnSt, btnBk;
		public static bool dpadU, dpadD, dpadL, dpadR;

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

			// TODO: figure out how to read the bitmasks in an efficient manner.
			// I'm doing everything individually just so I have something that works.

			// get the current gamepad state
			currentState = inputController.GetState();
			inputGamepad = currentState.Gamepad;

			// check if the state's changed, to prevent unnecessary polling of inputs
			if(currentState.PacketNumber != previousState.PacketNumber)
			{
				// allocate the key ranges from their respective sources
				range[0] = inputGamepad.LeftTrigger;
				range[1] = inputGamepad.RightTrigger;
				range[2] = inputGamepad.LeftThumbX & 0xFF;
				range[3] = (inputGamepad.LeftThumbX & 0x8000);

				// get the velocity values for the first 5 held keys
				vel[0] = (byte)((inputGamepad.LeftThumbX  & 0xFF00) >> 8);	// shift right 8 because it's just the top 8 that are needed here
				vel[1] = (byte)(inputGamepad.LeftThumbY  & 0xFF);			// no shift since it's just the bottom 8 that are needed here
				vel[2] = (byte)((inputGamepad.LeftThumbY  & 0xFF00) >> 8);
				vel[3] = (byte)(inputGamepad.RightThumbX & 0xFF);
				vel[4] = (byte)((inputGamepad.RightThumbX & 0xFF00) >> 8);

				// get the state of the overdrive button and pedal port
				overdrive = (inputGamepad.RightThumbY & 0xFF) == 0xFF;
				pedal = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

				// convert the key ranges into key booleans for ease of use
				// i need to learn how to use bitmasks more effectively lol
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

				// get the state of the face buttons
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

				// set the current state to the previous state for the next poll
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


			outputController.SetButtonState(Xbox360Button.Up,   	//	D-pad Up = D-pad Up
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
			if(Input.key[0])  keyOut.KeyDown(keyCode.VK_Z); else keyOut.KeyUp(keyCode.VK_Z);	// C1  = Z
			if(Input.key[1])  keyOut.KeyDown(keyCode.VK_S); else keyOut.KeyUp(keyCode.VK_S);	// C#1 = S
			if(Input.key[2])  keyOut.KeyDown(keyCode.VK_X); else keyOut.KeyUp(keyCode.VK_X);	// D1  = X
			if(Input.key[3])  keyOut.KeyDown(keyCode.VK_D); else keyOut.KeyUp(keyCode.VK_D);	// D#1 = D
			if(Input.key[4])  keyOut.KeyDown(keyCode.VK_C); else keyOut.KeyUp(keyCode.VK_C);	// E1  = C
			if(Input.key[5])  keyOut.KeyDown(keyCode.VK_V); else keyOut.KeyUp(keyCode.VK_V);	// F1  = V
			if(Input.key[6])  keyOut.KeyDown(keyCode.VK_G); else keyOut.KeyUp(keyCode.VK_G);	// F#1 = G
			if(Input.key[7])  keyOut.KeyDown(keyCode.VK_B); else keyOut.KeyUp(keyCode.VK_B);	// G1  = B
			if(Input.key[8])  keyOut.KeyDown(keyCode.VK_H); else keyOut.KeyUp(keyCode.VK_H);	// G#1 = H
			if(Input.key[9])  keyOut.KeyDown(keyCode.VK_N); else keyOut.KeyUp(keyCode.VK_N);	// A1  = N
			if(Input.key[10]) keyOut.KeyDown(keyCode.VK_J); else keyOut.KeyUp(keyCode.VK_J);	// A#1 = J
			if(Input.key[11]) keyOut.KeyDown(keyCode.VK_M); else keyOut.KeyUp(keyCode.VK_M);	// B1  = M
			if(Input.key[12]) keyOut.KeyDown(keyCode.VK_Q); else keyOut.KeyUp(keyCode.VK_Q);	// C2  = Q
			if(Input.key[13]) keyOut.KeyDown(keyCode.VK_2); else keyOut.KeyUp(keyCode.VK_2);	// C#2 = 2
			if(Input.key[14]) keyOut.KeyDown(keyCode.VK_W); else keyOut.KeyUp(keyCode.VK_W);	// D2  = W
			if(Input.key[15]) keyOut.KeyDown(keyCode.VK_3); else keyOut.KeyUp(keyCode.VK_3);	// D#2 = 3
			if(Input.key[16]) keyOut.KeyDown(keyCode.VK_E); else keyOut.KeyUp(keyCode.VK_E);	// E2  = E
			if(Input.key[17]) keyOut.KeyDown(keyCode.VK_R); else keyOut.KeyUp(keyCode.VK_R);	// F2  = R
			if(Input.key[18]) keyOut.KeyDown(keyCode.VK_5); else keyOut.KeyUp(keyCode.VK_5);	// F#2 = 5
			if(Input.key[19]) keyOut.KeyDown(keyCode.VK_T); else keyOut.KeyUp(keyCode.VK_T);	// G2  = T
			if(Input.key[20]) keyOut.KeyDown(keyCode.VK_6); else keyOut.KeyUp(keyCode.VK_6);	// G#2 = 6
			if(Input.key[21]) keyOut.KeyDown(keyCode.VK_Y); else keyOut.KeyUp(keyCode.VK_Y);	// A2  = Y
			if(Input.key[22]) keyOut.KeyDown(keyCode.VK_7); else keyOut.KeyUp(keyCode.VK_7);	// A#2 = 7
			if(Input.key[23]) keyOut.KeyDown(keyCode.VK_U); else keyOut.KeyUp(keyCode.VK_U);	// B2  = U
			if(Input.key[24]) keyOut.KeyDown(keyCode.VK_I); else keyOut.KeyUp(keyCode.VK_I);	// C3  = I
		}

		static void Midi()
		{
			
		}
	}
}