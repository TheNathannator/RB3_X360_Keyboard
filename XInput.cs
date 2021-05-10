using System;
using System.Threading;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace XInputIO
{
	public class Input
	{
        /// <summary>
        ///  XInput reading code.
        /// </summary>
		SharpDX.XInput.Controller inputController;
		SharpDX.XInput.Gamepad inputGamepad;
		public bool connected = false;
		public int range1, range2, range3, range4;
		public int vel1, vel2, vel3, vel4, vel5;
		public int overdrive;

		public void CheckConnected()
		{
			inputController = new Controller(UserIndex.One);
			connected = inputController.IsConnected;
		}

		public void Update()
		{
			connected = inputController.IsConnected;
			if (connected == false)
			{
				return;
			}
			inputGamepad = inputController.GetState().Gamepad;

			range1 = inputGamepad.LeftTrigger;
			range2 = inputGamepad.RightTrigger;
			range3 = inputGamepad.LeftThumbX & 0xFF;
			range4 = inputGamepad.LeftThumbX & 0x8000;

			vel1 = inputGamepad.LeftThumbX & 0xFF00;
			vel2 = inputGamepad.LeftThumbY & 0xFF;
			vel3 = inputGamepad.LeftThumbY & 0xFF00;
			vel4 = inputGamepad.RightThumbX & 0xFF;
			vel5 = inputGamepad.RightThumbX & 0xFF00;

			overdrive = inputGamepad.RightThumbY & 0xFF;
		}
	}

    class Output
    {
		[Flags]
		public enum range
		{
			key1 = 0x1,
			key2 = 0x2,
			key3 = 0x4,
			key4 = 0x8,
			key5 = 0x10,
			key6 = 0x20,
			key7 = 0x40,
			key8 = 0x80
		}
        /// <summary>
        ///  XInput output code.
        /// </summary>
        static void Main(string[] args)
        {
            ViGEmClient client = new ViGEmClient();
            IXbox360Controller outputController = client.CreateXbox360Controller();
			Input Input = new Input();
            outputController.Connect();

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
    }
}