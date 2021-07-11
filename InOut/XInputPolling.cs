using System;
using System.Runtime.InteropServices;
using SharpDX.XInput;

namespace InOut
{
	/// <summary>
	/// Class containing XInput device reading code.
	/// </summary>
	public class Input
	{
		/// <summary>
		/// Represents the currently connected controller.
		/// </summary>
		Controller inputController;
		/// <summary>
		/// Represents the current state of the current controller.
		/// </summary>
		Gamepad inputGamepad;
		/// <summary>
		/// Represents the current controller state.
		/// </summary>
		State inputState;
		/// <summary>
		/// Represents the previous controller state.
		/// </summary>
		State previousState;
		/// <summary>
		/// Represents the current allocated input state.
		/// </summary>
		public InputState stateCurrent = new InputState();


		/// <summary>
		/// Initialize XInput polling.
		/// </summary>
		/// <returns>
		/// The connected controller.
		/// </returns>
		public Controller Initialize()
		{
			Controller[] controllers = 
			{
				new Controller(UserIndex.One),
				new Controller(UserIndex.Two),
				new Controller(UserIndex.Three),
				new Controller(UserIndex.Four)
			};

			inputController = null;
			foreach(Controller controller in controllers)
			{
				if(controller.IsConnected)
				{
					Capabilities capabilities = controller.GetCapabilities(0);
					if((int)capabilities.SubType == 15) // This is the subtype that System.Diagnostics.Debug.WriteLine(capabilities.SubType) reports.
					{
						inputController = controller;
						break;
					}
				}
			}

			if(inputController == null)
			{
				return null;
			}
			else
			{
				try
				{
					State state = inputController.GetState();
				}
				catch(SharpDX.SharpDXException)
				{
					return null;
				}
				
				// Get an initial state
				previousState = inputController.GetState();
				return inputController;
			}
		}


		/// <summary>
		/// Array of bitmask constants.
		/// </summary>
		public int[] Bits = 
		{
			0x1,    0x2,    0x4,    0x8,
			0x10,   0x20,   0x40,   0x80,
			0x100,  0x200,  0x400,  0x800,
			0x1000, 0x2000, 0x4000, 0x8000
		};

		/// <summary>
		/// Poll the keyboard's inputs.
		/// </summary>
		public InputState Poll(Controller inputController, InputState statePrevious, bool force)
		{
			// TODO (Done, check if works): Track which notes are the first 5 so I can do MIDI output velocity properly.
			
			// Get the current gamepad state.
			inputState = inputController.GetState();
			inputGamepad = inputState.Gamepad;

			// Only poll if the packet number has incremented, or if a poll is forced.
			if(inputState.PacketNumber != previousState.PacketNumber || force)
			{
				// Store the previous input states.
				previousState = inputState;

				// Allocate the key ranges from their respective sources.
				stateCurrent.range[0] = inputGamepad.LeftTrigger;
				stateCurrent.range[1] = inputGamepad.RightTrigger;
				stateCurrent.range[2] = (byte)(inputGamepad.LeftThumbX & 0xFF);
				stateCurrent.range[3] = (byte)((inputGamepad.LeftThumbX & 0x8000) >> 8);

				// Get the velocity values for the first 5 held keys.
				stateCurrent.vel[0] = (byte)((inputGamepad.LeftThumbX  & 0xFF00) >> 8);
				stateCurrent.vel[1] = (byte)(inputGamepad.LeftThumbY   & 0xFF);
				stateCurrent.vel[2] = (byte)((inputGamepad.LeftThumbY  & 0xFF00) >> 8);
				stateCurrent.vel[3] = (byte)(inputGamepad.RightThumbX  & 0xFF);
				stateCurrent.vel[4] = (byte)((inputGamepad.RightThumbX & 0xFF00) >> 8);

				// Convert the key ranges into key booleans for ease of use, and assign velocities to keys.
				byte v = 0;	// vel array index
				byte o = 0;	// key array offset for each range
				for(byte r = 0; r < 4; r++)	// loop through the ranges
				{
					o = (byte)(r * 8);
					if(r < 3)
					{
						for(byte i = 0; i < 8; i++)	// loop through the keys
						{
							stateCurrent.key[o + i] = (stateCurrent.range[r] & Bits[7 - i]) == Bits[7 - i];
							if(stateCurrent.key[o + i] != statePrevious.key[o + i])
							{
								stateCurrent.velocity[o + i] = stateCurrent.vel[v];
								v += 1;
								Math.Clamp((byte)v, (byte)0, (byte)5);
							}
						}
					}
					else if(r < 4)
					{
						stateCurrent.key[o] = (stateCurrent.range[r] & Bits[7]) == Bits[7];
						if(stateCurrent.key[o] != statePrevious.key[o])
						{
							stateCurrent.velocity[o] = stateCurrent.vel[v];
							v += 1;
							Math.Clamp((byte)v, (byte)0, (byte)5);
						}
					}
				}

				// old code, only keeping in case the above doesn't work and i can't make it work properly
				//	stateCurrent.key[0]  = (stateCurrent.range[0] & Bits[7])  == Bits[7]; 	// C1  = inputGamepad.LeftTrigger & 0x80
				//	stateCurrent.key[1]  = (stateCurrent.range[0] & Bits[6])  == Bits[6]; 	// C#1 = inputGamepad.LeftTrigger & 0x40
				//	stateCurrent.key[2]  = (stateCurrent.range[0] & Bits[5])  == Bits[5]; 	// D1  = inputGamepad.LeftTrigger & 0x20
				//	stateCurrent.key[3]  = (stateCurrent.range[0] & Bits[4])  == Bits[4]; 	// D#1 = inputGamepad.LeftTrigger & 0x10
				//	stateCurrent.key[4]  = (stateCurrent.range[0] & Bits[3])  == Bits[3]; 	// E1  = inputGamepad.LeftTrigger & 0x8
				//	stateCurrent.key[5]  = (stateCurrent.range[0] & Bits[2])  == Bits[2]; 	// F1  = inputGamepad.LeftTrigger & 0x4
				//	stateCurrent.key[6]  = (stateCurrent.range[0] & Bits[1])  == Bits[1]; 	// F#1 = inputGamepad.LeftTrigger & 0x2
				//	stateCurrent.key[7]  = (stateCurrent.range[0] & Bits[0])  == Bits[0]; 	// G1  = inputGamepad.LeftTrigger & 0x1
				//	
				//	stateCurrent.key[8]  = (stateCurrent.range[1] & Bits[7])  == Bits[7]; 	// G#1 = inputGamepad.RightTrigger & 0x80
				//	stateCurrent.key[9]  = (stateCurrent.range[1] & Bits[6])  == Bits[6]; 	// A1  = inputGamepad.RightTrigger & 0x40
				//	stateCurrent.key[10] = (stateCurrent.range[1] & Bits[5])  == Bits[5]; 	// A#1 = inputGamepad.RightTrigger & 0x20
				//	stateCurrent.key[11] = (stateCurrent.range[1] & Bits[4])  == Bits[4]; 	// B1  = inputGamepad.RightTrigger & 0x10
				//	stateCurrent.key[12] = (stateCurrent.range[1] & Bits[3])  == Bits[3]; 	// C2  = inputGamepad.RightTrigger & 0x8
				//	stateCurrent.key[13] = (stateCurrent.range[1] & Bits[2])  == Bits[2]; 	// C#2 = inputGamepad.RightTrigger & 0x4
				//	stateCurrent.key[14] = (stateCurrent.range[1] & Bits[1])  == Bits[1]; 	// D2  = inputGamepad.RightTrigger & 0x2
				//	stateCurrent.key[15] = (stateCurrent.range[1] & Bits[0])  == Bits[0]; 	// D#2 = inputGamepad.RightTrigger & 0x1
				//	
				//	stateCurrent.key[16] = (stateCurrent.range[2] & Bits[7])  == Bits[7]; 	// E2  = inputGamepad.LeftThumbX & 0x80
				//	stateCurrent.key[17] = (stateCurrent.range[2] & Bits[6])  == Bits[6]; 	// F2  = inputGamepad.LeftThumbX & 0x40
				//	stateCurrent.key[18] = (stateCurrent.range[2] & Bits[5])  == Bits[5]; 	// F#2 = inputGamepad.LeftThumbX & 0x20
				//	stateCurrent.key[19] = (stateCurrent.range[2] & Bits[4])  == Bits[4]; 	// G2  = inputGamepad.LeftThumbX & 0x10
				//	stateCurrent.key[20] = (stateCurrent.range[2] & Bits[3])  == Bits[3]; 	// G#2 = inputGamepad.LeftThumbX & 0x8
				//	stateCurrent.key[21] = (stateCurrent.range[2] & Bits[2])  == Bits[2]; 	// A2  = inputGamepad.LeftThumbX & 0x4
				//	stateCurrent.key[22] = (stateCurrent.range[2] & Bits[1])  == Bits[1]; 	// A#2 = inputGamepad.LeftThumbX & 0x2
				//	stateCurrent.key[23] = (stateCurrent.range[2] & Bits[0])  == Bits[0]; 	// B2  = inputGamepad.LeftThumbX & 0x1
				//	
				//	stateCurrent.key[24] = (stateCurrent.range[3] & Bits[7])  == Bits[7];	// C2  = inputGamepad.LeftThumbX & 0x8000


				// Get the state of the overdrive button and the digital part of the pedal port.
				stateCurrent.overdrive = (inputGamepad.RightThumbY & 0x80)   == 0x80;
				stateCurrent.pedal     = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

				// Get the state of the face buttons.
				stateCurrent.btnA = ((int)inputGamepad.Buttons & Bits[12]) == Bits[12];	// XINPUT_GAMEPAD_A = state.Buttons & 0x1000
				stateCurrent.btnB = ((int)inputGamepad.Buttons & Bits[13]) == Bits[13];	// XINPUT_GAMEPAD_B = state.Buttons & 0x2000
				stateCurrent.btnX = ((int)inputGamepad.Buttons & Bits[14]) == Bits[14];	// XINPUT_GAMEPAD_X = state.Buttons & 0x4000
				stateCurrent.btnY = ((int)inputGamepad.Buttons & Bits[15]) == Bits[15];	// XINPUT_GAMEPAD_Y = state.Buttons & 0x8000
 
				stateCurrent.dpadU = ((int)inputGamepad.Buttons & Bits[0]) == Bits[0]; 	// XINPUT_GAMEPAD_DPAD_UP    = state.Buttons & 0x0001
				stateCurrent.dpadD = ((int)inputGamepad.Buttons & Bits[1]) == Bits[1]; 	// XINPUT_GAMEPAD_DPAD_DOWN  = state.Buttons & 0x0002
				stateCurrent.dpadL = ((int)inputGamepad.Buttons & Bits[2]) == Bits[2]; 	// XINPUT_GAMEPAD_DPAD_LEFT  = state.Buttons & 0x0004
				stateCurrent.dpadR = ((int)inputGamepad.Buttons & Bits[3]) == Bits[3]; 	// XINPUT_GAMEPAD_DPAD_RIGHT = state.Buttons & 0x0008

				stateCurrent.btnSt = ((int)inputGamepad.Buttons & Bits[4]) == Bits[4]; 	// XINPUT_GAMEPAD_START = state.Buttons & 0x0010
				stateCurrent.btnBk = ((int)inputGamepad.Buttons & Bits[5]) == Bits[5]; 	// XINPUT_GAMEPAD_BACK  = state.Buttons & 0x0020

				stateCurrent.btnGuide = testGuideButton((byte)inputController.UserIndex);
			}

			return stateCurrent;
		}

		// Code modified from reply #10 of https://forums.tigsource.com/index.php?topic=26792.0
		// I don't necessarily *need* the guide button, but the keyboard's MIDI mode uses it, so why not lol

		[DllImport("xinput1_3.dll", EntryPoint = "#100")]
		static extern int secret_get_gamepad(int userIndex, out XINPUT_GAMEPAD_SECRET secret);

		private struct XINPUT_GAMEPAD_SECRET
		{
			public uint eventCount;
			public ushort wButtons;
			public byte bLeftTrigger;
			public byte bRightTrigger;
			public short sThumbLX;
			public short sThumbLY;
			public short sThumbRX;
			public short sThumbRY;
		}

		private XINPUT_GAMEPAD_SECRET gamepadSecret;

		public bool testGuideButton(byte userIndex)
		{
			int state;
			bool guide;

			state = secret_get_gamepad(userIndex, out gamepadSecret);

			if(state != 0)
				return false;

			guide = ((gamepadSecret.wButtons & 0x0400) != 0);

			if(guide)
			{
				return true;
			}
			else
				return false;
		}
	}
}
