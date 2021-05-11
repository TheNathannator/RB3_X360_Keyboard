using System;
using System.Threading;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

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
		public int[] range = new int[4];
		public bool[] key = new bool[25];
		public int[] vel = new int[5];
		public int overdrive;
		public int btnA, btnB, btnX, btnY, btnLB, btnRB, btnLS, btnRS;
		public int dpadU, dpadD, dpadL, dpadR;
		

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
			
			range[1] = inputGamepad.LeftTrigger;
			range[2] = inputGamepad.RightTrigger;
			range[3] = inputGamepad.LeftThumbX & 0xFF;
			range[4] = inputGamepad.LeftThumbX & 0x8000;

			vel[1] = inputGamepad.LeftThumbX & 0xFF00;
			vel[2] = inputGamepad.LeftThumbY & 0xFF;
			vel[3] = inputGamepad.LeftThumbY & 0xFF00;
			vel[4] = inputGamepad.RightThumbX & 0xFF;
			vel[5] = inputGamepad.RightThumbX & 0xFF00;

			overdrive = inputGamepad.RightThumbY & 0xFF;

			key[1] = (range[1] & (int)Bits.bit1) == (int)Bits.bit1;
			key[2] = (range[1] & (int)Bits.bit2) == (int)Bits.bit2;
			key[3] = (range[1] & (int)Bits.bit3) == (int)Bits.bit3;
			key[4] = (range[1] & (int)Bits.bit4) == (int)Bits.bit4;
			key[5] = (range[1] & (int)Bits.bit5) == (int)Bits.bit5;
			key[6] = (range[1] & (int)Bits.bit6) == (int)Bits.bit6;
			key[7] = (range[1] & (int)Bits.bit7) == (int)Bits.bit7;
			key[8] = (range[1] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[1] = inputGamepad.LeftTrigger & 0x1;
			//	key[2] = inputGamepad.LeftTrigger & 0x2;
			//	key[3] = inputGamepad.LeftTrigger & 0x4;
			//	key[4] = inputGamepad.LeftTrigger & 0x8;
			//	key[5] = inputGamepad.LeftTrigger & 0x10;
			//	key[6] = inputGamepad.LeftTrigger & 0x20;
			//	key[7] = inputGamepad.LeftTrigger & 0x40;
			//	key[8] = inputGamepad.LeftTrigger & 0x80;

			key[9] = (range[2] & (int)Bits.bit1) == (int)Bits.bit1;
			key[10] = (range[2] & (int)Bits.bit2) == (int)Bits.bit2;
			key[11] = (range[2] & (int)Bits.bit3) == (int)Bits.bit3;
			key[12] = (range[2] & (int)Bits.bit4) == (int)Bits.bit4;
			key[13] = (range[2] & (int)Bits.bit5) == (int)Bits.bit5;
			key[14] = (range[2] & (int)Bits.bit6) == (int)Bits.bit6;
			key[15] = (range[2] & (int)Bits.bit7) == (int)Bits.bit7;
			key[16] = (range[2] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[9] = inputGamepad.RightTrigger & 0x1;
			//	key[10] = inputGamepad.RightTrigger & 0x2;
			//	key[11] = inputGamepad.RightTrigger & 0x4;
			//	key[12] = inputGamepad.RightTrigger & 0x8;
			//	key[13] = inputGamepad.RightTrigger & 0x10;
			//	key[14] = inputGamepad.RightTrigger & 0x20;
			//	key[15] = inputGamepad.RightTrigger & 0x40;
			//	key[16] = inputGamepad.RightTrigger & 0x80;

			key[17] = (range[3] & (int)Bits.bit1) == (int)Bits.bit1;
			key[18] = (range[3] & (int)Bits.bit2) == (int)Bits.bit2;
			key[19] = (range[3] & (int)Bits.bit3) == (int)Bits.bit3;
			key[20] = (range[3] & (int)Bits.bit4) == (int)Bits.bit4;
			key[21] = (range[3] & (int)Bits.bit5) == (int)Bits.bit5;
			key[22] = (range[3] & (int)Bits.bit6) == (int)Bits.bit6;
			key[23] = (range[3] & (int)Bits.bit7) == (int)Bits.bit7;
			key[24] = (range[3] & (int)Bits.bit8) == (int)Bits.bit8;

			//	key[17] = inputGamepad.LeftThumbX & 0x1;
			//	key[18] = inputGamepad.LeftThumbX & 0x2;
			//	key[19] = inputGamepad.LeftThumbX & 0x4;
			//	key[20] = inputGamepad.LeftThumbX & 0x8;
			//	key[21] = inputGamepad.LeftThumbX & 0x10;
			//	key[22] = inputGamepad.LeftThumbX & 0x20;
			//	key[23] = inputGamepad.LeftThumbX & 0x40;
			//	key[24] = inputGamepad.LeftThumbX & 0x80;

			key[25] = (range[4] & (int)Bits.bit16) == (int)Bits.bit16;

			//	key[25] = inputGamepad.LeftThumbX & 8000;

			/*
			XINPUT_GAMEPAD_DPAD_UP 0x0001
			XINPUT_GAMEPAD_DPAD_DOWN 0x0002
			XINPUT_GAMEPAD_DPAD_LEFT 0x0004
			XINPUT_GAMEPAD_DPAD_RIGHT 0x0008
			XINPUT_GAMEPAD_START 0x0010
			XINPUT_GAMEPAD_BACK 0x0020
			XINPUT_GAMEPAD_LEFT_THUMB 0x0040
			XINPUT_GAMEPAD_RIGHT_THUMB 0x0080
			XINPUT_GAMEPAD_LEFT_SHOULDER 0x0100
			XINPUT_GAMEPAD_RIGHT_SHOULDER 0x0200
			XINPUT_GAMEPAD_A 0x1000
			XINPUT_GAMEPAD_B 0x2000
			XINPUT_GAMEPAD_X 0x4000
			XINPUT_GAMEPAD_Y 0x8000
			*/
			//	btnA = inputGamepad.Buttons
			//	btnB = 
			//	btnX = 
			//	btnY = 
			//	btnLB = 
			//	btnRB = 
			//	btnLS = 
			//	btnRS = 
		}
	}

    class Output
    {
		/// <summary>
        ///  Output code (XInput, keypresses, MIDI notes).
        /// </summary>

		//	[Flags]
		//	public enum range
		//	{
		//		bit1 = 0x1,
		//		bit2 = 0x2,
		//		bit3 = 0x4,
		//		bit4 = 0x8,
		//		bit5 = 0x10,
		//		bit6 = 0x20,
		//		bit7 = 0x40,
		//		bit8 = 0x80
		//	}

		// public int outputType;

        static void XInput(string[] args)
        {
			ViGEmClient client = new ViGEmClient();
			IXbox360Controller outputController = client.CreateXbox360Controller();
			Input Input = new Input();
			Output Output = new Output();
			outputController.Connect();

			/*
			|	for Xbox 360 controller emulation:
			|
			|	C1 = A
			|	D1 = B
			|	E1 = Y
			|	F1 = X
			|	G1 = LB
			|	A1 = RB
			|
			|	C2 = A
			|	D2 = B
			|	E2 = Y
			|	F2 = X
			|	G2 = LB
			|	A2 = RB
			|
			|	OD button = Back
			|
			|	Start = Start
			|	Back = Back
			|	Xbox = Xbox
			|	A = A
			|	B = B
			|	X = X
			|	Y = Y
			|	D-pad = D-pad
			*/

			

			//true or false
			outputController.SetButtonState(Xbox360Button.A, false);
			outputController.SetButtonState(Xbox360Button.B, false);
			outputController.SetButtonState(Xbox360Button.X, false);
			outputController.SetButtonState(Xbox360Button.Y, false);

			outputController.SetButtonState(Xbox360Button.Up, false);
			outputController.SetButtonState(Xbox360Button.Down, false);
			outputController.SetButtonState(Xbox360Button.Left, false);
			outputController.SetButtonState(Xbox360Button.Right, false);

			outputController.SetButtonState(Xbox360Button.LeftShoulder, false);
			outputController.SetButtonState(Xbox360Button.RightShoulder, false);

			outputController.SetButtonState(Xbox360Button.LeftThumb, false);
			outputController.SetButtonState(Xbox360Button.RightThumb, false);

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

		static void Keyboard()
		{

		}

		static void Midi()
		{

		}
    }
}