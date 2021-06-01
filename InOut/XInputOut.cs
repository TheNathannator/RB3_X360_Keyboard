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
		}
	}
}