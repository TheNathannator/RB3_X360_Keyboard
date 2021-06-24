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
		static IKeyboardSimulator keyOut;
		InputState prevState;
		InputState currentState;

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

		public void Output()
		{
			// The keys that correspond to the RB keyboard keys are arranged roughly like the RB keyboard.
			//  2 3   5 6 7
			// Q W E R T Y U I
			//  S D   G H J
			// Z X C V B N M

			// Loop through the current and previous state of the key array to check if the state has changed.
			// Only change the keypress state if there's a change in the state of the input.
			for(int i = 0; i < 25; i++)
			{
				if(currentState.key[i] != prevState.key[i])
				{
					if(currentState.key[i]) keyOut.KeyDown(keys[i]);
					else keyOut.KeyUp(keys[i]);
				}
			}

			if(currentState.overdrive != prevState.overdrive)
			{
				if(currentState.overdrive) keyOut.KeyDown(keyCode.VK_O); else keyOut.KeyUp(keyCode.VK_O);	// Overdrive = O
			}

			if(currentState.pedal != prevState.pedal)
			{
				if(currentState.pedal) keyOut.KeyDown(keyCode.VK_P); else keyOut.KeyUp(keyCode.VK_P);    	// Pedal = P
			}

			if(currentState.btnSt != prevState.btnSt
			|| currentState.btnBk != prevState.btnBk)
			{
				if(currentState.btnSt) keyOut.KeyDown(keyCode.RETURN); else keyOut.KeyUp(keyCode.RETURN);	// Start = Enter/Return
				if(currentState.btnBk) keyOut.KeyDown(keyCode.ESCAPE); else keyOut.KeyUp(keyCode.ESCAPE);	// Back  = Escape
			}

			if(currentState.dpadU != prevState.dpadU
			|| currentState.dpadD != prevState.dpadD
			|| currentState.dpadL != prevState.dpadL
			|| currentState.dpadR != prevState.dpadR)
			{
				if(currentState.dpadU) keyOut.KeyDown(keyCode.UP);    else keyOut.KeyUp(keyCode.UP);     	// D-pad Up    = Up Arrow
				if(currentState.dpadD) keyOut.KeyDown(keyCode.DOWN);  else keyOut.KeyUp(keyCode.DOWN);   	// D-pad Down  = Down Arrow
				if(currentState.dpadL) keyOut.KeyDown(keyCode.LEFT);  else keyOut.KeyUp(keyCode.LEFT);   	// D-pad Left  = Left Arrow
				if(currentState.dpadR) keyOut.KeyDown(keyCode.RIGHT); else keyOut.KeyUp(keyCode.RIGHT);  	// D-pad Right = Right Arrow
			}

			// old code
			//	if(currentState.key[0])  keyOut.KeyDown(keyCode.VK_Z); else keyOut.KeyUp(keyCode.VK_Z);	// C1  = Z
			//	if(currentState.key[1])  keyOut.KeyDown(keyCode.VK_S); else keyOut.KeyUp(keyCode.VK_S);	// C#1 = S
			//	if(currentState.key[2])  keyOut.KeyDown(keyCode.VK_X); else keyOut.KeyUp(keyCode.VK_X);	// D1  = X
			//	if(currentState.key[3])  keyOut.KeyDown(keyCode.VK_D); else keyOut.KeyUp(keyCode.VK_D);	// D#1 = D
			//	if(currentState.key[4])  keyOut.KeyDown(keyCode.VK_C); else keyOut.KeyUp(keyCode.VK_C);	// E1  = C
			//	if(currentState.key[5])  keyOut.KeyDown(keyCode.VK_V); else keyOut.KeyUp(keyCode.VK_V);	// F1  = V
			//	if(currentState.key[6])  keyOut.KeyDown(keyCode.VK_G); else keyOut.KeyUp(keyCode.VK_G);	// F#1 = G
			//	if(currentState.key[7])  keyOut.KeyDown(keyCode.VK_B); else keyOut.KeyUp(keyCode.VK_B);	// G1  = B
			//	if(currentState.key[8])  keyOut.KeyDown(keyCode.VK_H); else keyOut.KeyUp(keyCode.VK_H);	// G#1 = H
			//	if(currentState.key[9])  keyOut.KeyDown(keyCode.VK_N); else keyOut.KeyUp(keyCode.VK_N);	// A1  = N
			//	if(currentState.key[10]) keyOut.KeyDown(keyCode.VK_J); else keyOut.KeyUp(keyCode.VK_J);	// A#1 = J
			//	if(currentState.key[11]) keyOut.KeyDown(keyCode.VK_M); else keyOut.KeyUp(keyCode.VK_M);	// B1  = M
			//	if(currentState.key[12]) keyOut.KeyDown(keyCode.VK_Q); else keyOut.KeyUp(keyCode.VK_Q);	// C2  = Q
			//	if(currentState.key[13]) keyOut.KeyDown(keyCode.VK_2); else keyOut.KeyUp(keyCode.VK_2);	// C#2 = 2
			//	if(currentState.key[14]) keyOut.KeyDown(keyCode.VK_W); else keyOut.KeyUp(keyCode.VK_W);	// D2  = W
			//	if(currentState.key[15]) keyOut.KeyDown(keyCode.VK_3); else keyOut.KeyUp(keyCode.VK_3);	// D#2 = 3
			//	if(currentState.key[16]) keyOut.KeyDown(keyCode.VK_E); else keyOut.KeyUp(keyCode.VK_E);	// E2  = E
			//	if(currentState.key[17]) keyOut.KeyDown(keyCode.VK_R); else keyOut.KeyUp(keyCode.VK_R);	// F2  = R
			//	if(currentState.key[18]) keyOut.KeyDown(keyCode.VK_5); else keyOut.KeyUp(keyCode.VK_5);	// F#2 = 5
			//	if(currentState.key[19]) keyOut.KeyDown(keyCode.VK_T); else keyOut.KeyUp(keyCode.VK_T);	// G2  = T
			//	if(currentState.key[20]) keyOut.KeyDown(keyCode.VK_6); else keyOut.KeyUp(keyCode.VK_6);	// G#2 = 6
			//	if(currentState.key[21]) keyOut.KeyDown(keyCode.VK_Y); else keyOut.KeyUp(keyCode.VK_Y);	// A2  = Y
			//	if(currentState.key[22]) keyOut.KeyDown(keyCode.VK_7); else keyOut.KeyUp(keyCode.VK_7);	// A#2 = 7
			//	if(currentState.key[23]) keyOut.KeyDown(keyCode.VK_U); else keyOut.KeyUp(keyCode.VK_U);	// B2  = U
			//	if(currentState.key[24]) keyOut.KeyDown(keyCode.VK_I); else keyOut.KeyUp(keyCode.VK_I);	// C3  = I
		}

		/// <summary>
		/// Disable all outputs and wait 1 second before resuming.
		/// </summary>
		public void Panic()	
		{
			for(int i = 0; i < 25; i++)
			{
				keyOut.KeyUp(keys[i]);
			}

			keyOut.KeyUp(keyCode.VK_O); 	// Overdrive
			keyOut.KeyUp(keyCode.VK_P); 	// Pedal
			keyOut.KeyUp(keyCode.RETURN);	// Start = Enter/Return
			keyOut.KeyUp(keyCode.ESCAPE);	// Back  = Escape
			keyOut.KeyUp(keyCode.UP);     	// D-pad Up    = Up Arrow
			keyOut.KeyUp(keyCode.DOWN);   	// D-pad Down  = Down Arrow
			keyOut.KeyUp(keyCode.LEFT);   	// D-pad Left  = Left Arrow
			keyOut.KeyUp(keyCode.RIGHT);  	// D-pad Right = Right Arrow

			Thread.Sleep(1000);
		}
	}
}