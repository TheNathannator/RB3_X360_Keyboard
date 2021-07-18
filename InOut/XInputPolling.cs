using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpDX.XInput;

namespace InOut
{
	/// <summary>
	/// XInput device reading code.
	/// </summary>
	public class Input
	{
		/// <summary>
		/// The currently connected controller.
		/// </summary>
		Controller inputController;
		/// <summary>
		/// The current state of the current controller.
		/// </summary>
		Gamepad inputGamepad;
		/// <summary>
		/// The current controller state.
		/// </summary>
		State inputState;
		/// <summary>
		/// The previous controller state.
		/// </summary>
		State previousState;


		/// <summary>
		/// Initializes XInput polling.
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
					// Only connect the controller if it is a keyboard.
					Capabilities capabilities = controller.GetCapabilities(0);
					if((int)capabilities.SubType == 15)
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
		/// Bitmask constants.
		/// </summary>
		public int[] Bits = 
		{
			0x1,    0x2,    0x4,    0x8,
			0x10,   0x20,   0x40,   0x80,
			0x100,  0x200,  0x400,  0x800,
			0x1000, 0x2000, 0x4000, 0x8000
		};

		/// <summary>
		/// Polls the keyboard's inputs.
		/// </summary>
		public void Poll(Controller inputController, ref InputState stateCurrent, ref InputState statePrevious)
		{
			inputState = inputController.GetState();

			// Only poll if the packet number has incremented.
			if(inputState.PacketNumber != previousState.PacketNumber)
			{
				inputGamepad = inputState.Gamepad;
				previousState = inputState;

				// Allocate the key ranges from their respective sources.
				byte[] range = new byte[4];
				range[0] = inputGamepad.LeftTrigger;
				range[1] = inputGamepad.RightTrigger;
				range[2] = (byte)(inputGamepad.LeftThumbX & 0xFF);
				range[3] = (byte)((inputGamepad.LeftThumbX & 0x8000) >> 8);

				// Get the velocity values for the first 5 held keys.
				stateCurrent.vel[0] = (byte)((inputGamepad.LeftThumbX  & 0x7F00) >> 8);
				stateCurrent.vel[1] = (byte)(inputGamepad.LeftThumbY   & 0x7F);
				stateCurrent.vel[2] = (byte)((inputGamepad.LeftThumbY  & 0x7F00) >> 8);
				stateCurrent.vel[3] = (byte)(inputGamepad.RightThumbX  & 0x7F);
				stateCurrent.vel[4] = (byte)((inputGamepad.RightThumbX & 0x7F00) >> 8);

				// Convert the key ranges into key booleans for ease of use.
				byte o = 0;
				byte pressed = 0;
				for(byte r = 0; r < 4; r++)
				{
					o = (byte)(r * 8);
					for(byte i = 0; i < 8; i++)
					{
						stateCurrent.key[o + i] = (range[r] & Bits[7 - i]) == Bits[7 - i];
						if(stateCurrent.key[o + i])
						{
							pressed += 1;
						}
						if(o + i == 24) break;
					}
				}

				// Assign velocities from the velocity array to keys.
				// Note: The keyboard has an issue where if you press a key in the right way, it won't register a velocity for it.
				// It doesn't account for this, and just fills up the velocity array as it normally would when additional keys are pressed.
				// I don't think that can be accounted for here, unfortunately.
				byte v = 0;
				for(byte i = 0; i < 25; i++)
				{
					if(pressed < 6)
					{
						if(stateCurrent.key[i])
						{
							if(stateCurrent.key[i] != statePrevious.key[i])
							{
								stateCurrent.velocity[i] = stateCurrent.vel[v];
							}
							if(v < 4)
							{
								v += 1;
							}
						}
					}

					if(stateCurrent.key[i] && stateCurrent.velocity[i] == 0)
					{
						stateCurrent.velocity[i] = 100;
					}

					if(!stateCurrent.key[i])
					{
						stateCurrent.velocity[i] = 0;
					}
				}

				// Allocate the rest of the button states.
				stateCurrent.overdrive    = (inputGamepad.RightThumbY & 0x80)   == 0x80;
				stateCurrent.pedalDigital = (inputGamepad.RightThumbY & 0x8000) == 0x8000;

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
		}

		// Code modified from reply #10 of https://forums.tigsource.com/index.php?topic=26792.0
		// I don't necessarily *need* the guide button, but the keyboard's MIDI mode's panic function uses it, so why not lol
		// I ported the panic functionality to the other output types as well
		// TODO: Consider porting things to the XInput code directly, instead of using SharpDX.XInput

		[DllImport("xinput1_3.dll", EntryPoint = "#100")]
		static extern int secret_get_gamepad(int userIndex, out XINPUT_GAMEPAD_SECRET secret);

		/// <summary>
		/// Secret XInput state struct.
		/// </summary>
		private struct XINPUT_GAMEPAD_SECRET
		{
			/// <summary>
			/// The current packet number.
			/// </summary>
			public uint eventCount;
			/// <summary>
			/// Button bitmask.
			/// </summary>
			public ushort wButtons;
			/// <summary>
			/// Left trigger axis.
			/// </summary>
			public byte bLeftTrigger;
			/// <summary>
			/// Right trigger axis.
			/// </summary>
			public byte bRightTrigger;
			/// <summary>
			/// Left stick X axis.
			/// </summary>
			public short sThumbLX;
			/// <summary>
			/// Left stick Y axis.
			/// </summary>
			public short sThumbLY;
			/// <summary>
			/// Right stick X axis.
			/// </summary>
			public short sThumbRX;
			/// <summary>
			/// Right stick Y axis.
			/// </summary>
			public short sThumbRY;
		}

		/// <summary>
		/// The secret state of the controller.
		/// </summary>
		private XINPUT_GAMEPAD_SECRET gamepadSecret;

		/// <summary>
		/// Gets the guide button state.
		/// </summary>
		public bool testGuideButton(byte userIndex)
		{
			int state;
			bool guide;

			state = secret_get_gamepad(userIndex, out gamepadSecret);

			if(state != 0)
				return false;

			guide = (gamepadSecret.wButtons & 0x0400) != 0;

			if(guide)
				return true;
			else
				return false;
		}
	}
}
