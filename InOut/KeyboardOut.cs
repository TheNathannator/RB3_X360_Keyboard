using System.Diagnostics;
using WindowsInput;
using keyCode = WindowsInput.Native.VirtualKeyCode;

namespace InOut
{
	/// <summary>
	/// Keyboard output code.
	/// </summary>
	public class Keyboard
	{
		/// <summary>
		/// The virtual keycodes to be used.
		/// </summary>
		/// <remarks>
		/// <para>C1 = VK_Z, Db1 = VK_S, D1 = VK_X, Eb1 = VK_D, E1 = VK_C,</para>
		/// <para>F1 = VK_V, Gb1 = VK_G, G1 = VK_B, Ab1 = VK_H, A1 = VK_N, Bb1 = VK_J, B1 = VK_M,</para>
		/// <para>C2 = VK_Q, Db2 = VK_2, D2 = VK_W, Eb2 = VK_3, E2 = VK_E,</para>
		/// <para>F2 = VK_R, Gb2 = VK_5, G2 = VK_T, Ab2 = VK_6, A2 = VK_Y, Bb2 = VK_7, B2 = VK_U,</para>
		/// <para>C3 = VK_I</para>
		/// </remarks>
		keyCode[] keys = 
		{
			keyCode.VK_Z, keyCode.VK_S, keyCode.VK_X, keyCode.VK_D, keyCode.VK_C,
			keyCode.VK_V, keyCode.VK_G, keyCode.VK_B, keyCode.VK_H, keyCode.VK_N, keyCode.VK_J, keyCode.VK_M,
			keyCode.VK_Q, keyCode.VK_2, keyCode.VK_W, keyCode.VK_3, keyCode.VK_E,
			keyCode.VK_R, keyCode.VK_5, keyCode.VK_T, keyCode.VK_6, keyCode.VK_Y, keyCode.VK_7, keyCode.VK_U,
			keyCode.VK_I
		};

		/// <summary>
		/// Outputs keypresses.
		/// </summary>	
		public void Output(IKeyboardSimulator outputKey, ref InputState stateCurrent, ref InputState statePrevious)
		{
			// The keys that correspond to the RB keyboard keys are arranged roughly like an actual MIDI keyboard, but split into 2 halves on top of each other.
			//  2 3   5 6 7
			// Q W E R T Y U I
			//  S D   G H J
			// Z X C V B N M

			if(stateCurrent.btnBk && stateCurrent.btnGuide && stateCurrent.btnSt) Panic(outputKey);

			// Sends keypresses.
			for(int i = 0; i < 25; i++)
			{
				if(stateCurrent.key[i] != statePrevious.key[i])
				{
					if(stateCurrent.key[i]) outputKey.KeyDown(keys[i]);
					else outputKey.KeyUp(keys[i]);
				}
			}

			// Overdrive = O
			if(stateCurrent.overdrive != statePrevious.overdrive)	
			{
				if(stateCurrent.overdrive) outputKey.KeyDown(keyCode.VK_O);
				else outputKey.KeyUp(keyCode.VK_O);
			}

			// Pedal = P
			if(stateCurrent.pedalDigital != statePrevious.pedalDigital)	
			{
				if(stateCurrent.pedalDigital) outputKey.KeyDown(keyCode.VK_P);
				else outputKey.KeyUp(keyCode.VK_P);
			}

			// Start = Enter/Return
			if(stateCurrent.btnSt != statePrevious.btnSt)	
			{
				if(stateCurrent.btnSt) outputKey.KeyDown(keyCode.RETURN);
				else outputKey.KeyUp(keyCode.RETURN);
			}

			// Back = Escape
			if(stateCurrent.btnBk != statePrevious.btnBk)	
			{
				if(stateCurrent.btnBk) outputKey.KeyDown(keyCode.ESCAPE);
				else outputKey.KeyUp(keyCode.ESCAPE);
			}

			// D-pad Up = Up Arrow
			if(stateCurrent.dpadU != statePrevious.dpadU)
			{
				if(stateCurrent.dpadU) outputKey.KeyDown(keyCode.UP); else outputKey.KeyUp(keyCode.UP);
			}

			// D-pad Down = Down Arrow
			if(stateCurrent.dpadD != statePrevious.dpadD)
			{
				if(stateCurrent.dpadD) outputKey.KeyDown(keyCode.DOWN); else outputKey.KeyUp(keyCode.DOWN);
			}

			// D-pad Left = Left Arrow
			if(stateCurrent.dpadL != statePrevious.dpadL)
			{
				if(stateCurrent.dpadL) outputKey.KeyDown(keyCode.LEFT); else outputKey.KeyUp(keyCode.LEFT);
			}

			// D-pad Right = Right Arrow
			if(stateCurrent.dpadR != statePrevious.dpadR)
			{
				if(stateCurrent.dpadR) outputKey.KeyDown(keyCode.RIGHT); else outputKey.KeyUp(keyCode.RIGHT);
			}
		}

		/// <summary>
		/// Zeroes out all outputs.
		/// </summary>
		public void Panic(IKeyboardSimulator outputKey)	
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
		}
	}
}