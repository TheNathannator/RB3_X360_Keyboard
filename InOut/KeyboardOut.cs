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
		public void Output()
		{
			// The keys that coorespond to the keyboard keys are arranged roughly like a keyboard.
			//  2 3   5 6 7
			// Q W E R T Y U I
			//  S D   G H J
			// Z X C V B N M

			// TODO: Just sending inputs based on the current state is naive and probably won't work correctly.
			// I need to keep track of both the previous state and the current state,
			// and only send inputs when the input being checked for actually changes.
			if(currentState.key[0])  keyOut.KeyDown(keyCode.VK_Z); else keyOut.KeyUp(keyCode.VK_Z);	// C1  = Z
			if(currentState.key[1])  keyOut.KeyDown(keyCode.VK_S); else keyOut.KeyUp(keyCode.VK_S);	// C#1 = S
			if(currentState.key[2])  keyOut.KeyDown(keyCode.VK_X); else keyOut.KeyUp(keyCode.VK_X);	// D1  = X
			if(currentState.key[3])  keyOut.KeyDown(keyCode.VK_D); else keyOut.KeyUp(keyCode.VK_D);	// D#1 = D
			if(currentState.key[4])  keyOut.KeyDown(keyCode.VK_C); else keyOut.KeyUp(keyCode.VK_C);	// E1  = C
			if(currentState.key[5])  keyOut.KeyDown(keyCode.VK_V); else keyOut.KeyUp(keyCode.VK_V);	// F1  = V
			if(currentState.key[6])  keyOut.KeyDown(keyCode.VK_G); else keyOut.KeyUp(keyCode.VK_G);	// F#1 = G
			if(currentState.key[7])  keyOut.KeyDown(keyCode.VK_B); else keyOut.KeyUp(keyCode.VK_B);	// G1  = B
			if(currentState.key[8])  keyOut.KeyDown(keyCode.VK_H); else keyOut.KeyUp(keyCode.VK_H);	// G#1 = H
			if(currentState.key[9])  keyOut.KeyDown(keyCode.VK_N); else keyOut.KeyUp(keyCode.VK_N);	// A1  = N
			if(currentState.key[10]) keyOut.KeyDown(keyCode.VK_J); else keyOut.KeyUp(keyCode.VK_J);	// A#1 = J
			if(currentState.key[11]) keyOut.KeyDown(keyCode.VK_M); else keyOut.KeyUp(keyCode.VK_M);	// B1  = M
			if(currentState.key[12]) keyOut.KeyDown(keyCode.VK_Q); else keyOut.KeyUp(keyCode.VK_Q);	// C2  = Q
			if(currentState.key[13]) keyOut.KeyDown(keyCode.VK_2); else keyOut.KeyUp(keyCode.VK_2);	// C#2 = 2
			if(currentState.key[14]) keyOut.KeyDown(keyCode.VK_W); else keyOut.KeyUp(keyCode.VK_W);	// D2  = W
			if(currentState.key[15]) keyOut.KeyDown(keyCode.VK_3); else keyOut.KeyUp(keyCode.VK_3);	// D#2 = 3
			if(currentState.key[16]) keyOut.KeyDown(keyCode.VK_E); else keyOut.KeyUp(keyCode.VK_E);	// E2  = E
			if(currentState.key[17]) keyOut.KeyDown(keyCode.VK_R); else keyOut.KeyUp(keyCode.VK_R);	// F2  = R
			if(currentState.key[18]) keyOut.KeyDown(keyCode.VK_5); else keyOut.KeyUp(keyCode.VK_5);	// F#2 = 5
			if(currentState.key[19]) keyOut.KeyDown(keyCode.VK_T); else keyOut.KeyUp(keyCode.VK_T);	// G2  = T
			if(currentState.key[20]) keyOut.KeyDown(keyCode.VK_6); else keyOut.KeyUp(keyCode.VK_6);	// G#2 = 6
			if(currentState.key[21]) keyOut.KeyDown(keyCode.VK_Y); else keyOut.KeyUp(keyCode.VK_Y);	// A2  = Y
			if(currentState.key[22]) keyOut.KeyDown(keyCode.VK_7); else keyOut.KeyUp(keyCode.VK_7);	// A#2 = 7
			if(currentState.key[23]) keyOut.KeyDown(keyCode.VK_U); else keyOut.KeyUp(keyCode.VK_U);	// B2  = U
			if(currentState.key[24]) keyOut.KeyDown(keyCode.VK_I); else keyOut.KeyUp(keyCode.VK_I);	// C3  = I

			if(currentState.overdrive) keyOut.KeyDown(keyCode.VK_O); else keyOut.KeyUp(keyCode.VK_O);	// Overdrive = O

			if(currentState.btnSt) keyOut.KeyDown(keyCode.RETURN); else keyOut.KeyUp(keyCode.RETURN);	// Start = Enter/Return
			if(currentState.btnBk) keyOut.KeyDown(keyCode.ESCAPE); else keyOut.KeyUp(keyCode.ESCAPE);	// Back  = Escape

			if(currentState.dpadU) keyOut.KeyDown(keyCode.UP);    else keyOut.KeyUp(keyCode.UP);   	// D-pad Up    = Up Arrow
			if(currentState.dpadD) keyOut.KeyDown(keyCode.DOWN);  else keyOut.KeyUp(keyCode.DOWN); 	// D-pad Down  = Down Arrow
			if(currentState.dpadL) keyOut.KeyDown(keyCode.LEFT);  else keyOut.KeyUp(keyCode.LEFT); 	// D-pad Left  = Left Arrow
			if(currentState.dpadR) keyOut.KeyDown(keyCode.RIGHT); else keyOut.KeyUp(keyCode.RIGHT);	// D-pad Right = Right Arrow
		}

		public void KeySet()
		{
			
		}
	}
}