using System.Diagnostics;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace InOut
{
	/// <summary>
	/// ViGEm output code.
	/// </summary>
	public class ViGEm
	{
		/// <summary>
		/// Outputs to a ViGEmBus controller device.
		/// </summary>
		public void Output(IXbox360Controller output360, ref InputState currentState, bool drumMode)
		{
			if(currentState.btnBk && currentState.btnGuide && currentState.btnSt) Panic(output360);
			
			// C1, C2, A = A
			output360.SetButtonState(Xbox360Button.A,
				currentState.key[0]  ||
				currentState.key[12] ||
				currentState.btnA);

			// D1, D2, B = B
			output360.SetButtonState(Xbox360Button.B,
				currentState.key[2]  ||
				currentState.key[14] ||
				currentState.btnB);

			// E1, E2, Y = Y
			output360.SetButtonState(Xbox360Button.Y,
				currentState.key[4]  ||
				currentState.key[16] ||
				currentState.btnY);

			// F1, F2, X = X
			output360.SetButtonState(Xbox360Button.X,
				currentState.key[5]  ||
				currentState.key[17] ||
				currentState.btnX);

			// G1, G2, pedal (drum mode enabled) = LB
			output360.SetButtonState(Xbox360Button.LeftShoulder,
				currentState.key[7]  ||
				currentState.key[19] ||
				(currentState.pedalDigital & drumMode));

			// A1, A2 = RB
			output360.SetButtonState(Xbox360Button.RightShoulder,
				currentState.key[9]  ||
				currentState.key[21]);

			// B1, B2 = L Stick Click
			output360.SetButtonState(Xbox360Button.LeftThumb,
				currentState.key[11]  ||
				currentState.key[23]);

			// C3 = R Stick Click
			output360.SetButtonState(Xbox360Button.RightThumb,
				currentState.key[24]);

			// D-pad is mapped 1:1
			output360.SetButtonState(Xbox360Button.Up,    currentState.dpadU);
			output360.SetButtonState(Xbox360Button.Down,  currentState.dpadD);
			output360.SetButtonState(Xbox360Button.Left,  currentState.dpadL);
			output360.SetButtonState(Xbox360Button.Right, currentState.dpadR);

			// Start is mapped 1:1
			output360.SetButtonState(Xbox360Button.Start, currentState.btnSt);

			// Overdrive button, Back, pedal (drum mode disabled) = Back
			output360.SetButtonState(Xbox360Button.Back,
				currentState.btnBk     ||
				currentState.overdrive ||
				(currentState.pedalDigital & !drumMode));
		}

		/// <summary>
		/// Zeroes out all outputs.
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
		}
	}
}