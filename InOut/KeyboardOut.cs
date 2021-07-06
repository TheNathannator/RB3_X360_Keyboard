using System.Threading;
using WindowsInput;
using keyCode = WindowsInput.Native.VirtualKeyCode;

namespace InOut
{
	/// <summary>
	///  Keyboard output code.
	/// </summary>
	public class Keyboard
	{
		IKeyboardSimulator outputKey;

		/// <summary>
		/// Array containing the virtual keycodes to be used.
		/// </summary>
		/// <remarks>
		/// <para>C1 = VK_Z, Db1 = VK_S, D1 = VK_X, Eb1 = VK_D, E1 = VK_C,</para>
		/// <para>F1 = VK_V, Gb1 = VK_G, G1 = VK_B, Ab1 = VK_H, A1 = VK_N, Bb1 = VK_J, B1 = VK_M,</para>
		/// <para>C2 = VK_Q, Db2 = VK_2, D2 = VK_W, Eb2 = VK_3, E2 = VK_E,</para>
		/// <para>F2 = VK_R, Gb2 = VK_5, G2 = VK_T, Ab2 = VK_6, A2 = VK_Y, Bb2 = VK_7, B2 = VK_U, C3 = VK_I</para>
		/// </remarks>
		keyCode[] keys = 
		{
			keyCode.VK_Z, keyCode.VK_S, keyCode.VK_X, keyCode.VK_D, keyCode.VK_C,
			keyCode.VK_V, keyCode.VK_G, keyCode.VK_B, keyCode.VK_H, keyCode.VK_N, keyCode.VK_J, keyCode.VK_M,
			keyCode.VK_Q, keyCode.VK_2, keyCode.VK_W, keyCode.VK_3, keyCode.VK_E,
			keyCode.VK_R, keyCode.VK_5, keyCode.VK_T, keyCode.VK_6, keyCode.VK_Y, keyCode.VK_7, keyCode.VK_U,
			keyCode.VK_I
		};

		public void Output(InputState currentState, InputState prevState)
		{
			// The keys that correspond to the RB keyboard keys are arranged roughly like the RB keyboard.
			//  2 3   5 6 7
			// Q W E R T Y U I
			//  S D   G H J
			// Z X C V B N M

			if(currentState.btnBk && currentState.btnGuide && currentState.btnSt) Panic();

			// Loop through the current and previous state of the key array to check if the state has changed.
			// Only change the keypress state if there's a change in the state of the input.
			for(int i = 0; i < 25; i++)
			{
				if(currentState.key[i] != prevState.key[i])
				{
					if(currentState.key[i]) outputKey.KeyDown(keys[i]);
					else outputKey.KeyUp(keys[i]);
				}
			}

			// Overdrive = O
			if(currentState.overdrive != prevState.overdrive)	
			{
				if(currentState.overdrive) outputKey.KeyDown(keyCode.VK_O);
				else outputKey.KeyUp(keyCode.VK_O);
			}

			// Pedal = P
			if(currentState.pedal != prevState.pedal)	
			{
				if(currentState.pedal) outputKey.KeyDown(keyCode.VK_P);
				else outputKey.KeyUp(keyCode.VK_P);
			}

			// Start = Enter/Return
			if(currentState.btnSt != prevState.btnSt)	
			{
				if(currentState.btnSt) outputKey.KeyDown(keyCode.RETURN);
				else outputKey.KeyUp(keyCode.RETURN);
			}

			// Back = Escape
			if(currentState.btnBk != prevState.btnBk)	
			{
				if(currentState.btnBk) outputKey.KeyDown(keyCode.ESCAPE);
				else outputKey.KeyUp(keyCode.ESCAPE);
			}

			// D-pad Up = Up Arrow
			if(currentState.dpadU != prevState.dpadU)
			{
				if(currentState.dpadU) outputKey.KeyDown(keyCode.UP); else outputKey.KeyUp(keyCode.UP);
			}

			// D-pad Down = Down Arrow
			if(currentState.dpadD != prevState.dpadD)
			{
				if(currentState.dpadD) outputKey.KeyDown(keyCode.DOWN); else outputKey.KeyUp(keyCode.DOWN);
			}

			// D-pad Left = Left Arrow
			if(currentState.dpadL != prevState.dpadL)
			{
				if(currentState.dpadL) outputKey.KeyDown(keyCode.LEFT); else outputKey.KeyUp(keyCode.LEFT);
			}

			// D-pad Right = Right Arrow
			if(currentState.dpadR != prevState.dpadR)
			{
				if(currentState.dpadR) outputKey.KeyDown(keyCode.RIGHT); else outputKey.KeyUp(keyCode.RIGHT);
			}

			// old code
			//	if(currentState.key[0])  outputKey.KeyDown(keyCode.VK_Z); else outputKey.KeyUp(keyCode.VK_Z);	// C1  = Z
			//	if(currentState.key[1])  outputKey.KeyDown(keyCode.VK_S); else outputKey.KeyUp(keyCode.VK_S);	// C#1 = S
			//	if(currentState.key[2])  outputKey.KeyDown(keyCode.VK_X); else outputKey.KeyUp(keyCode.VK_X);	// D1  = X
			//	if(currentState.key[3])  outputKey.KeyDown(keyCode.VK_D); else outputKey.KeyUp(keyCode.VK_D);	// D#1 = D
			//	if(currentState.key[4])  outputKey.KeyDown(keyCode.VK_C); else outputKey.KeyUp(keyCode.VK_C);	// E1  = C
			//	if(currentState.key[5])  outputKey.KeyDown(keyCode.VK_V); else outputKey.KeyUp(keyCode.VK_V);	// F1  = V
			//	if(currentState.key[6])  outputKey.KeyDown(keyCode.VK_G); else outputKey.KeyUp(keyCode.VK_G);	// F#1 = G
			//	if(currentState.key[7])  outputKey.KeyDown(keyCode.VK_B); else outputKey.KeyUp(keyCode.VK_B);	// G1  = B
			//	if(currentState.key[8])  outputKey.KeyDown(keyCode.VK_H); else outputKey.KeyUp(keyCode.VK_H);	// G#1 = H
			//	if(currentState.key[9])  outputKey.KeyDown(keyCode.VK_N); else outputKey.KeyUp(keyCode.VK_N);	// A1  = N
			//	if(currentState.key[10]) outputKey.KeyDown(keyCode.VK_J); else outputKey.KeyUp(keyCode.VK_J);	// A#1 = J
			//	if(currentState.key[11]) outputKey.KeyDown(keyCode.VK_M); else outputKey.KeyUp(keyCode.VK_M);	// B1  = M
			//	if(currentState.key[12]) outputKey.KeyDown(keyCode.VK_Q); else outputKey.KeyUp(keyCode.VK_Q);	// C2  = Q
			//	if(currentState.key[13]) outputKey.KeyDown(keyCode.VK_2); else outputKey.KeyUp(keyCode.VK_2);	// C#2 = 2
			//	if(currentState.key[14]) outputKey.KeyDown(keyCode.VK_W); else outputKey.KeyUp(keyCode.VK_W);	// D2  = W
			//	if(currentState.key[15]) outputKey.KeyDown(keyCode.VK_3); else outputKey.KeyUp(keyCode.VK_3);	// D#2 = 3
			//	if(currentState.key[16]) outputKey.KeyDown(keyCode.VK_E); else outputKey.KeyUp(keyCode.VK_E);	// E2  = E
			//	if(currentState.key[17]) outputKey.KeyDown(keyCode.VK_R); else outputKey.KeyUp(keyCode.VK_R);	// F2  = R
			//	if(currentState.key[18]) outputKey.KeyDown(keyCode.VK_5); else outputKey.KeyUp(keyCode.VK_5);	// F#2 = 5
			//	if(currentState.key[19]) outputKey.KeyDown(keyCode.VK_T); else outputKey.KeyUp(keyCode.VK_T);	// G2  = T
			//	if(currentState.key[20]) outputKey.KeyDown(keyCode.VK_6); else outputKey.KeyUp(keyCode.VK_6);	// G#2 = 6
			//	if(currentState.key[21]) outputKey.KeyDown(keyCode.VK_Y); else outputKey.KeyUp(keyCode.VK_Y);	// A2  = Y
			//	if(currentState.key[22]) outputKey.KeyDown(keyCode.VK_7); else outputKey.KeyUp(keyCode.VK_7);	// A#2 = 7
			//	if(currentState.key[23]) outputKey.KeyDown(keyCode.VK_U); else outputKey.KeyUp(keyCode.VK_U);	// B2  = U
			//	if(currentState.key[24]) outputKey.KeyDown(keyCode.VK_I); else outputKey.KeyUp(keyCode.VK_I);	// C3  = I
		}

		/// <summary>
		/// Disable all outputs and wait 1 second before resuming.
		/// </summary>
		public void Panic()	
		{
			for(int i = 0; i < 25; i++)
			{
				outputKey.KeyUp(keys[i]);
			}

			outputKey.KeyUp(keyCode.VK_O);  	// Overdrive
			outputKey.KeyUp(keyCode.VK_P);  	// Pedal
			outputKey.KeyUp(keyCode.RETURN);	// Start = Enter/Return
			outputKey.KeyUp(keyCode.ESCAPE);	// Back  = Escape
			outputKey.KeyUp(keyCode.UP);     	// D-pad Up    = Up Arrow
			outputKey.KeyUp(keyCode.DOWN);   	// D-pad Down  = Down Arrow
			outputKey.KeyUp(keyCode.LEFT);   	// D-pad Left  = Left Arrow
			outputKey.KeyUp(keyCode.RIGHT);  	// D-pad Right = Right Arrow

			Thread.Sleep(1000);
		}
	}
}