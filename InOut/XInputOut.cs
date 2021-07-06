using System.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace InOut
{
	/// <summary>
	///  ViGEm output code.
	/// </summary>
	public class ViGEm
	{
		/// <summary>
		/// Represents the ViGEmBus client.
		/// </summary>
		static ViGEmClient client;


		/// <summary>
		/// Initialize the ViGEmBus client.
		/// </summary>
		public IXbox360Controller Initialize()
		{
			var output360 = client.CreateXbox360Controller();
			output360.Connect();
			return output360;
		}

		/// <summary>
		/// Output to a ViGEmBus controller device.
		/// </summary>
		public void Output(IXbox360Controller output360, InputState currentState)
		{
			if(currentState.btnBk && currentState.btnGuide && currentState.btnSt) Panic(output360);
			
			output360.SetButtonState(Xbox360Button.A,	//	C1, C2, A = A
				currentState.key[0]  ||
				currentState.key[12] ||
				currentState.btnA);

			output360.SetButtonState(Xbox360Button.B,	//	D1, D2, B = B
				currentState.key[2]  ||
				currentState.key[14] ||
				currentState.btnB);

			output360.SetButtonState(Xbox360Button.Y,	//	E1, E2, Y = Y
				currentState.key[4]  ||
				currentState.key[16] ||
				currentState.btnY);

			output360.SetButtonState(Xbox360Button.X,	//	F1, F2, X = X
				currentState.key[5]  ||
				currentState.key[17] ||
				currentState.btnX);

			output360.SetButtonState(Xbox360Button.LeftShoulder, 	//	G1, G2 = LB
				currentState.key[7]  ||
				currentState.key[19]);
			
			output360.SetButtonState(Xbox360Button.RightShoulder,	//	A1, A2 = RB
				currentState.key[9]  ||
				currentState.key[21]);

			output360.SetButtonState(Xbox360Button.Up,    currentState.dpadU);	//	D-pad Up = D-pad Up
			output360.SetButtonState(Xbox360Button.Down,  currentState.dpadD);	//	D-pad Down = D-pad Down
			output360.SetButtonState(Xbox360Button.Left,  currentState.dpadL);	//	D-pad Left = D-pad Left
			output360.SetButtonState(Xbox360Button.Right, currentState.dpadR);	//	D-pad Right = D-pad Right

			output360.SetButtonState(Xbox360Button.Start, currentState.btnSt);	//	Start = Start
			output360.SetButtonState(Xbox360Button.Back,	//	OD button, Back, pedal = Back
				currentState.btnBk     ||
				currentState.overdrive ||
				currentState.pedal);
		}

		public void DrumModeOutput(IXbox360Controller output360, InputState currentState)
		{
			if(currentState.btnBk && currentState.btnGuide && currentState.btnSt) Panic(output360);
			
			output360.SetButtonState(Xbox360Button.A,	//	C1, C2, A = A
				currentState.key[0]  ||
				currentState.key[12] ||
				currentState.btnA);

			output360.SetButtonState(Xbox360Button.B,	//	D1, D2, B = B
				currentState.key[2]  ||
				currentState.key[14] ||
				currentState.btnB);

			output360.SetButtonState(Xbox360Button.Y,	//	E1, E2, Y = Y
				currentState.key[4]  ||
				currentState.key[16] ||
				currentState.btnY);

			output360.SetButtonState(Xbox360Button.X,	//	F1, F2, X = X
				currentState.key[5]  ||
				currentState.key[17] ||
				currentState.btnX);

			output360.SetButtonState(Xbox360Button.LeftShoulder, 	//	G1, G2, pedal = LB
				currentState.key[7]  ||
				currentState.key[19] ||
				currentState.pedal);

			output360.SetButtonState(Xbox360Button.RightShoulder,	//	A1, A2, RB = RB
				currentState.key[9]  ||
				currentState.key[21]);

			output360.SetButtonState(Xbox360Button.Up,    currentState.dpadU);	//	D-pad Up = D-pad Up
			output360.SetButtonState(Xbox360Button.Down,  currentState.dpadD);	//	D-pad Down = D-pad Down
			output360.SetButtonState(Xbox360Button.Left,  currentState.dpadL);	//	D-pad Left = D-pad Left
			output360.SetButtonState(Xbox360Button.Right, currentState.dpadR);	//	D-pad Right = D-pad Right

			output360.SetButtonState(Xbox360Button.Start, currentState.btnSt);	//	Start = Start
			output360.SetButtonState(Xbox360Button.Back,	//	OD button, Back = Back
				currentState.btnBk ||
				currentState.overdrive);
		}

		/// <summary>
		/// Disable all outputs and wait 1 second before resuming.
		/// </summary>
		public void Panic(IXbox360Controller output360)
		{
			output360.SetButtonState(Xbox360Button.A, false);
			output360.SetButtonState(Xbox360Button.B, false);
			output360.SetButtonState(Xbox360Button.Y, false);
			output360.SetButtonState(Xbox360Button.X, false);

			output360.SetButtonState(Xbox360Button.LeftShoulder,  false);
			output360.SetButtonState(Xbox360Button.RightShoulder, false);
			output360.SetButtonState(Xbox360Button.LeftThumb,     false);
			output360.SetButtonState(Xbox360Button.RightThumb,    false);

			output360.SetButtonState(Xbox360Button.Up,    false);
			output360.SetButtonState(Xbox360Button.Down,  false);
			output360.SetButtonState(Xbox360Button.Left,  false);
			output360.SetButtonState(Xbox360Button.Right, false);

			output360.SetButtonState(Xbox360Button.Start, false);
			output360.SetButtonState(Xbox360Button.Back,  false);

			output360.SetAxisValue(Xbox360Axis.LeftThumbX,  0);
			output360.SetAxisValue(Xbox360Axis.LeftThumbY,  0);
			output360.SetAxisValue(Xbox360Axis.RightThumbX, 0);
			output360.SetAxisValue(Xbox360Axis.RightThumbY, 0);

			output360.SetSliderValue(Xbox360Slider.LeftTrigger,  0);
			output360.SetSliderValue(Xbox360Slider.RightTrigger, 0);

			Thread.Sleep(1000);
		}
	}
}