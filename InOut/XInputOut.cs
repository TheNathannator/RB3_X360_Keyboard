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
			// TODO: Just sending inputs based on the current state is naive.
			// I need to keep track of both the previous state and the current state,
			// and only send inputs when the input being checked for actually changes.
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


			if(Input.overdrive) outputController.SetAxisValue(Xbox360Axis.RightThumbY, 32767);
			else outputController.SetAxisValue(Xbox360Axis.RightThumbY, 0);

			outputController.SetButtonState(Xbox360Button.Up,    Input.dpadU);	//	D-pad Up = D-pad Up
			outputController.SetButtonState(Xbox360Button.Down,  Input.dpadD);	//	D-pad Down = D-pad Down
			outputController.SetButtonState(Xbox360Button.Left,  Input.dpadL);	//	D-pad Left = D-pad Left
			outputController.SetButtonState(Xbox360Button.Right, Input.dpadR);	//	D-pad Right = D-pad Right

			outputController.SetButtonState(Xbox360Button.Start, Input.btnSt);	//	Start = Start
			outputController.SetButtonState(Xbox360Button.Back,  Input.btnBk);	//	OD button, Back = Back
		}
	}
}