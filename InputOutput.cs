using System;
using System.Threading;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

/*
This code is kinda trash lol.
It's just kinda put together in a way such that it'll at the very least work.
I'll try and optimize and clean it up later once I learn more about C#.
*/

namespace InputOutput
{
	public class Input
	{
        /// <summary>
        ///  XInput reading code.
        /// </summary>

		SharpDX.XInput.Controller inputController = null;
		SharpDX.XInput.Gamepad inputGamepad;
		public bool connected = false;
		// TODO: figure out how to read the bitmasks in an efficient manner
		public static int[] range = new int[4];
		public static bool[] key = new bool[25];
		public static int[] vel = new int[5];
		public static bool overdrive;
		public static bool btnA, btnB, btnX, btnY, btnLB, btnRB, btnLS, btnRS, btnSt, btnBk;
		public static bool dpadU, dpadD, dpadL, dpadR;
		

		[Flags]
		public enum Bits
		{
			bit1 = 0x1,
			bit2 = 0x2,
			bit3 = 0x4,
			bit4 = 0x8,
			bit5 = 0x10,
			bit6 = 0x20,
			bit7 = 0x40,
			bit8 = 0x80,
			bit9 = 0x100,
			bit10 = 0x200,
			bit11 = 0x400,
			bit12 = 0x800,
			bit13 = 0x1000,
			bit14 = 0x2000,
			bit15 = 0x4000,
			bit16 = 0x8000
		}
	
		public void Main()
		{
			// Initialize XInput
			var controllers = new[]
			{
				new Controller(UserIndex.One),
				new Controller(UserIndex.Two),
				new Controller(UserIndex.Three),
				new Controller(UserIndex.Four)
			};

			// Get 1st controller available
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
				var previousState = inputController.GetState();
                while (inputController.IsConnected)
                {
                    var state = inputController.GetState();
                    if (previousState.PacketNumber != state.PacketNumber)
					{
                        Console.WriteLine(state.Gamepad);
					}
                    Thread.Sleep(10);
                    previousState = state;
                }
			}

			inputGamepad = inputController.GetState().Gamepad;

			// TODO: figure out how to read the bitmasks in an efficient manner.
			// I'm doing everything individually just so I have something that works.
			
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

			key[0] = (range[0] & (int)Bits.bit1) == (int)Bits.bit1;
			key[1] = (range[0] & (int)Bits.bit2) == (int)Bits.bit2;
			key[2] = (range[0] & (int)Bits.bit3) == (int)Bits.bit3;
			key[3] = (range[0] & (int)Bits.bit4) == (int)Bits.bit4;
			key[4] = (range[0] & (int)Bits.bit5) == (int)Bits.bit5;
			key[5] = (range[0] & (int)Bits.bit6) == (int)Bits.bit6;
			key[6] = (range[0] & (int)Bits.bit7) == (int)Bits.bit7;
			key[7] = (range[0] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[0] = inputGamepad.LeftTrigger & 0x1;
			//	key[1] = inputGamepad.LeftTrigger & 0x2;
			//	key[2] = inputGamepad.LeftTrigger & 0x4;
			//	key[3] = inputGamepad.LeftTrigger & 0x8;
			//	key[4] = inputGamepad.LeftTrigger & 0x10;
			//	key[5] = inputGamepad.LeftTrigger & 0x20;
			//	key[6] = inputGamepad.LeftTrigger & 0x40;
			//	key[7] = inputGamepad.LeftTrigger & 0x80;

			key[8] = (range[1] & (int)Bits.bit1) == (int)Bits.bit1;
			key[9] = (range[1] & (int)Bits.bit2) == (int)Bits.bit2;
			key[10] = (range[1] & (int)Bits.bit3) == (int)Bits.bit3;
			key[11] = (range[1] & (int)Bits.bit4) == (int)Bits.bit4;
			key[12] = (range[1] & (int)Bits.bit5) == (int)Bits.bit5;
			key[13] = (range[1] & (int)Bits.bit6) == (int)Bits.bit6;
			key[14] = (range[1] & (int)Bits.bit7) == (int)Bits.bit7;
			key[15] = (range[1] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[8] = inputGamepad.RightTrigger & 0x1;
			//	key[9] = inputGamepad.RightTrigger & 0x2;
			//	key[10] = inputGamepad.RightTrigger & 0x4;
			//	key[11] = inputGamepad.RightTrigger & 0x8;
			//	key[12] = inputGamepad.RightTrigger & 0x10;
			//	key[13] = inputGamepad.RightTrigger & 0x20;
			//	key[14] = inputGamepad.RightTrigger & 0x40;
			//	key[15] = inputGamepad.RightTrigger & 0x80;

			key[16] = (range[2] & (int)Bits.bit1) == (int)Bits.bit1;
			key[17] = (range[2] & (int)Bits.bit2) == (int)Bits.bit2;
			key[18] = (range[2] & (int)Bits.bit3) == (int)Bits.bit3;
			key[19] = (range[2] & (int)Bits.bit4) == (int)Bits.bit4;
			key[20] = (range[2] & (int)Bits.bit5) == (int)Bits.bit5;
			key[21] = (range[2] & (int)Bits.bit6) == (int)Bits.bit6;
			key[22] = (range[2] & (int)Bits.bit7) == (int)Bits.bit7;
			key[23] = (range[2] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[16] = inputGamepad.LeftThumbX & 0x1;
			//	key[17] = inputGamepad.LeftThumbX & 0x2;
			//	key[18] = inputGamepad.LeftThumbX & 0x4;
			//	key[19] = inputGamepad.LeftThumbX & 0x8;
			//	key[20] = inputGamepad.LeftThumbX & 0x10;
			//	key[21] = inputGamepad.LeftThumbX & 0x20;
			//	key[22] = inputGamepad.LeftThumbX & 0x40;
			//	key[23] = inputGamepad.LeftThumbX & 0x80;

			key[24] = (range[3] & (int)Bits.bit16) == (int)Bits.bit16;

			//	key[24] = inputGamepad.LeftThumbX & 8000;

			dpadU = ((int)inputGamepad.Buttons & (int)Bits.bit1) == (int)Bits.bit1;
			dpadD = ((int)inputGamepad.Buttons & (int)Bits.bit2) == (int)Bits.bit2;
			dpadL = ((int)inputGamepad.Buttons & (int)Bits.bit3) == (int)Bits.bit3;
			dpadR = ((int)inputGamepad.Buttons & (int)Bits.bit4) == (int)Bits.bit4;

			//	XINPUT_GAMEPAD_DPAD_UP 0x0001
			//	XINPUT_GAMEPAD_DPAD_DOWN 0x0002
			//	XINPUT_GAMEPAD_DPAD_LEFT 0x0004
			//	XINPUT_GAMEPAD_DPAD_RIGHT 0x0008

			btnSt = ((int)inputGamepad.Buttons & (int)Bits.bit5) == (int)Bits.bit5;
			btnBk = ((int)inputGamepad.Buttons & (int)Bits.bit6) == (int)Bits.bit6;

			//	XINPUT_GAMEPAD_START 0x0010
			//	XINPUT_GAMEPAD_BACK 0x0020

			btnLS = ((int)inputGamepad.Buttons & (int)Bits.bit7) == (int)Bits.bit7;
			btnRS = ((int)inputGamepad.Buttons & (int)Bits.bit8) == (int)Bits.bit8;
			btnLB = ((int)inputGamepad.Buttons & (int)Bits.bit9) == (int)Bits.bit9;
			btnRB = ((int)inputGamepad.Buttons & (int)Bits.bit10) == (int)Bits.bit10;

			//	XINPUT_GAMEPAD_LEFT_THUMB 0x0040
			//	XINPUT_GAMEPAD_RIGHT_THUMB 0x0080
			//	XINPUT_GAMEPAD_LEFT_SHOULDER 0x0100
			//	XINPUT_GAMEPAD_RIGHT_SHOULDER 0x0200

			btnA = ((int)inputGamepad.Buttons & (int)Bits.bit13) == (int)Bits.bit13;
			btnB = ((int)inputGamepad.Buttons & (int)Bits.bit14) == (int)Bits.bit14;
			btnX = ((int)inputGamepad.Buttons & (int)Bits.bit15) == (int)Bits.bit15;
			btnY = ((int)inputGamepad.Buttons & (int)Bits.bit16) == (int)Bits.bit16;

			//	XINPUT_GAMEPAD_A 0x1000
			//	XINPUT_GAMEPAD_B 0x2000
			//	XINPUT_GAMEPAD_X 0x4000
			//	XINPUT_GAMEPAD_Y 0x8000
		}
	}

    class Output
    {
		/// <summary>
        ///  Output code (XInput, keypresses, MIDI notes).
        /// </summary>

        static void XInput(string[] args)
        {
			ViGEmClient client = new ViGEmClient();
			IXbox360Controller outputController = client.CreateXbox360Controller();
			outputController.Connect();

			if(Input.key[0] || Input.key[12] || Input.btnA)
			{
				outputController.SetButtonState(Xbox360Button.A, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.A, false);
			}

			if(Input.key[2] || Input.key[14] || Input.btnB)
			{
				outputController.SetButtonState(Xbox360Button.B, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.B, false);
			}

			if(Input.key[4] || Input.key[16] || Input.btnY)
			{
				outputController.SetButtonState(Xbox360Button.Y, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Y, false);
			}

			if(Input.key[5] || Input.key[17] || Input.btnX)
			{
				outputController.SetButtonState(Xbox360Button.X, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.X, false);
			}

			if(Input.key[7] || Input.key[19] || Input.btnLB)
			{
				outputController.SetButtonState(Xbox360Button.LeftShoulder, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.LeftShoulder, false);
			}
			
			if(Input.key[9] || Input.key[21] || Input.btnRB)
			{
				outputController.SetButtonState(Xbox360Button.RightShoulder, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.RightShoulder, false);
			}

			//	C1, C2, A = A
			//	D1, D2, B = B
			//	E1, E2, Y = Y
			//	F1, F2, X = X
			//	G1, G2, LB = LB
			//	A1, A2, RB = RB

			if(Input.btnSt)
			{
				outputController.SetButtonState(Xbox360Button.Start, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Start, false);
			}

			if(Input.overdrive || Input.btnBk)
			{
				outputController.SetButtonState(Xbox360Button.Back, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Back, false);
			}

			//	Start = Start
			//	OD button, Back = Back

			if(Input.dpadU)
			{
				outputController.SetButtonState(Xbox360Button.Up, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Up, false);
			}
			
			if(Input.dpadD)
			{
				outputController.SetButtonState(Xbox360Button.Down, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Down, false);
			}
			
			if(Input.dpadL)
			{
				outputController.SetButtonState(Xbox360Button.Left, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Left, false);
			}
			
			if(Input.dpadR)
			{
				outputController.SetButtonState(Xbox360Button.Right, true);
			}
			else
			{
				outputController.SetButtonState(Xbox360Button.Right, false);
			}

			//	D-pad = D-pad

			//minimum of -32768, maximum of 32767
			outputController.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
			outputController.SetAxisValue(Xbox360Axis.LeftThumbY, 0);
			outputController.SetAxisValue(Xbox360Axis.RightThumbX, 0);
			outputController.SetAxisValue(Xbox360Axis.RightThumbY, 0);

			//minimum of 0, maximum of 255
			outputController.SetSliderValue(Xbox360Slider.LeftTrigger, 0);
			outputController.SetSliderValue(Xbox360Slider.RightTrigger, 0);

			Thread.Sleep(5000);
		}


		//	static void ViGEmButtonSet(string Button, bool btnVal)
		//	{
		//		outputController.SetAxisValue(Xbox360Button.Button, btnVal)
		//	}

		//	static void Keyboard()
		//	{
		//	
		//	}

		//	static void Midi()
		//	{
		//	
		//	}
    }
}