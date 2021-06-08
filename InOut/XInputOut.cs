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
		static IXbox360Controller outputController;
		static ViGEmClient vigem;
		InputState prevState;
		InputState currentState;

		/// <summary>
		/// Initializes the ViGEmBus client.
		/// </summary>
		static void Initialize(string[] args)
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
				currentState.key[0]  ||
				currentState.key[12] ||
				currentState.btnA);

			outputController.SetButtonState(Xbox360Button.B,	//	D1, D2, B = B
				currentState.key[2]  ||
				currentState.key[14] ||
				currentState.btnB);

			outputController.SetButtonState(Xbox360Button.Y,	//	E1, E2, Y = Y
				currentState.key[4]  ||
				currentState.key[16] ||
				currentState.btnY);

			outputController.SetButtonState(Xbox360Button.X,	//	F1, F2, X = X
				currentState.key[5]  ||
				currentState.key[17] ||
				currentState.btnX);


			outputController.SetButtonState(Xbox360Button.LeftShoulder, 	//	G1, G2, LB, pedal = LB
				currentState.key[7]  ||
				currentState.key[19] ||
				currentState.btnLB   ||
				currentState.pedal);

			outputController.SetButtonState(Xbox360Button.RightShoulder,	//	A1, A2, RB = RB
				currentState.key[9]  ||
				currentState.key[21] ||
				currentState.btnRB);


			if(currentState.overdrive) outputController.SetAxisValue(Xbox360Axis.RightThumbY, 32767);
			else outputController.SetAxisValue(Xbox360Axis.RightThumbY, 0);

			outputController.SetButtonState(Xbox360Button.Up,    currentState.dpadU);	//	D-pad Up = D-pad Up
			outputController.SetButtonState(Xbox360Button.Down,  currentState.dpadD);	//	D-pad Down = D-pad Down
			outputController.SetButtonState(Xbox360Button.Left,  currentState.dpadL);	//	D-pad Left = D-pad Left
			outputController.SetButtonState(Xbox360Button.Right, currentState.dpadR);	//	D-pad Right = D-pad Right

			outputController.SetButtonState(Xbox360Button.Start, currentState.btnSt);	//	Start = Start
			outputController.SetButtonState(Xbox360Button.Back,  currentState.btnBk);	//	OD button, Back = Back
		}

		public void Reset()
		{
			// Zeroes out all the inputs.
			outputController.SetButtonState(Xbox360Button.A, false);
			outputController.SetButtonState(Xbox360Button.B, false);
			outputController.SetButtonState(Xbox360Button.Y, false);
			outputController.SetButtonState(Xbox360Button.X, false);

			outputController.SetButtonState(Xbox360Button.LeftShoulder, false);
			outputController.SetButtonState(Xbox360Button.RightShoulder, false);

			outputController.SetButtonState(Xbox360Button.Up,    false);
			outputController.SetButtonState(Xbox360Button.Down,  false);
			outputController.SetButtonState(Xbox360Button.Left,  false);
			outputController.SetButtonState(Xbox360Button.Right, false);

			outputController.SetButtonState(Xbox360Button.Start, false);
			outputController.SetButtonState(Xbox360Button.Back,  false);

			outputController.SetAxisValue(Xbox360Axis.LeftThumbX,  0);
			outputController.SetAxisValue(Xbox360Axis.LeftThumbY,  0);
			outputController.SetAxisValue(Xbox360Axis.RightThumbX, 0);
			outputController.SetAxisValue(Xbox360Axis.RightThumbY, 0);

			outputController.SetSliderValue(Xbox360Slider.LeftTrigger,  0);
			outputController.SetSliderValue(Xbox360Slider.RightTrigger, 0);
		}
	}
}