using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Exceptions;
using Melanchall.DryWetMidi.Devices;
using WindowsInput;
using InOut;
using image = RB3_X360_Keyboard.Properties.Resources;

// TODO (Big task): Maybe support connecting more than one keyboard at a time.

namespace Program
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// Represents the currently connected controller.
		/// </summary>
		public Controller inputController = new Controller();
		/// <summary>
		/// Represents the current input state.
		/// </summary>
		public InputState stateCurrent = new InputState();
		/// <summary>
		/// Represents the previous input state.
		/// </summary>
		public InputState statePrevious = new InputState();


		/// <summary>
		/// Represents the ViGEmBus client.
		/// </summary>
		public ViGEmClient client;
		/// <summary>
		/// Represents a ViGEmBus controller.
		/// </summary>
		public IXbox360Controller output360;
		/// <summary>
		/// Represents a MIDI device which accepts an input.
		/// </summary>
		public OutputDevice outputMidi;
		/// <summary>
		/// Represents the keyboard simulator.
		/// </summary>
		public IKeyboardSimulator outputKey;


		/// <summary>
		/// XInput device reading class.
		/// </summary>
		Input input = new Input();
		/// <summary>
		/// ViGEm device output class.
		/// </summary>
		ViGEm xinput = new ViGEm();
		/// <summary>
		/// Keyboard output class.
		/// </summary>
		Keyboard key = new Keyboard();
		/// <summary>
		/// MIDI output class.
		/// </summary>
		Midi mid = new Midi();


		/// <summary>
		/// Indicates whether or not Drum Mode is toggled.
		/// </summary>
		public bool drumMode = false;
		/// <summary>
		/// The current pedal mode.
		/// </summary>
		/// <remarks>
		/// 1 = expression, 2 = channel volume, 3 = foot controller.
		/// </remarks>
		public int pedalMode = 0;
		/// <summary>
		/// The current octave offset.
		/// </summary>
		public int octave = 4;
		/// <summary>
		/// The current program number.
		/// </summary>
		public int program = 1;
		/// <summary>
		/// The player index of the connected controller.
		/// </summary>
		public int playerIndex = -1;


		/// <summary>
		/// Indicates whether or not the output loop is enabled.
		/// </summary>
		bool outputStarted = false;
		/// <summary>
		/// Indicates whether or not a controller is connected.
		/// </summary>
		bool controllerConnected = false;
		/// <summary>
		/// Indicates whether or not ViGEmBus is installed.
		/// </summary>
		bool vigem = false;
		/// <summary>
		/// The currently selected output mode.
		/// </summary>
		/// <remarks>
		/// 1 = Xbox 360, 2 = keyboard, 3 = MIDI.
		/// </remarks>
		int outputMode = 2;


		public MainWindow()
		{
			InitializeComponent();

			vigem = ViGEmCheck();
			radio_Output_Xbox360.Enabled = vigem;
		}

		private bool ViGEmCheck()
		{
			try
			{
				var client = new ViGEmClient();
				client.Dispose();
				return true;
			}
			catch (VigemBusNotFoundException)
			{
				MessageBox.Show("ViGEmBus was not found on the system./r/nXbox 360 controller output functionality will be unavailable until you install it.", "ViGEmBus not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		private void appClosing(object sender, FormClosingEventArgs e)
		{
			try{output360.Disconnect();}
			catch{}
			
			try{client.Dispose();}
			catch{}
			
			try{outputMidi.TurnAllNotesOff();}
			catch{}

			try{outputMidi.Dispose();}
			catch{}
		}

		private void timer_IOLoop_Tick(object sender, EventArgs e)
        {
			if(controllerConnected)
			{
				Input();
				UpdateWindow();
				if(outputStarted) Output();
			}
			else Initialize();
        }

		private void radio_PedalMode_Change(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;

			if(rb.Checked)
			{
				switch (rb.Name)
				{
					default:
					case "Expression":
						pedalMode = 1;
						return;
					case "Channel Volume":
						pedalMode = 2;
						return;
					case "Foot Controller":
						pedalMode = 3;
						return;
				}
			}
		}

		private void radio_OutputType_Change(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			
			switch (rb.Text)
			{
				case "Xbox 360 Controller":
					if(rb.Checked) outputMode = 1;
					break;
				case "Keyboard":
					if(rb.Checked) outputMode = 2;
					break;
				case "MIDI":
					if(rb.Checked) outputMode = 3;
					break;
				default: break;
			}
			
		}

		private void numUpDown_SettingOctave_Change(object sender, EventArgs e)
		{
			NumericUpDown ud = sender as NumericUpDown;

			octave = (int)ud.Value;
		}

		private void numUpDown_SettingProgram_Change(object sender, EventArgs e)
		{
			NumericUpDown ud = sender as NumericUpDown;

			program = (int)ud.Value;
		}

		private void checkbox_SettingDrumMode_Changed(object sender, EventArgs e)
		{
			drumMode = !drumMode;
		}

		private void dropdown_OutputMidi_Device_Open(object sender, EventArgs e)
		{
			ComboBox cb = sender as ComboBox;

			cb.Items.Clear();
			cb.Items.Add($"No Device");

			foreach (var device in OutputDevice.GetAll())
			{
				string name = device.Name.ToString();
				cb.Items.Add(name);
			}
		}

		private void button_Start_Click(object sender, EventArgs e)
		{
			Button b = sender as Button;

			switch(outputStarted)
			{
				case false:
				{
					if(playerIndex == -1)
					{
						b.Text = "Start";
						MessageBox.Show("No Xbox 360 controllers detected!", "No Controller", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					radio_Output_Xbox360.Enabled = false;
					radio_Output_Keyboard.Enabled = false;
					radio_Output_Midi.Enabled = false;
					dropdown_Output_MidiDevice.Enabled = false;
					image_MidiLight1.BackColor = Color.Red;

					OutputPrep(true);
					outputStarted = true;
					b.Text = "Stop";
					return;
				}

				case true:
				{
					// Disable settings, enable output type selection
					radio_Pedal_Expression.Enabled = false;
					radio_Pedal_ChannelVolume.Enabled = false;
					radio_Pedal_FootController.Enabled = false;

					numUpDown_Setting_Octave.Enabled = false;
					numUpDown_Setting_Program.Enabled = false;
					checkbox_Setting_DrumMode.Enabled = false;

					radio_Output_Xbox360.Enabled = vigem;
					radio_Output_Keyboard.Enabled = true;
					radio_Output_Midi.Enabled = true;

					image_MidiLight1.BackColor = Color.Black;

					switch(outputMode)
					{
						default:
							break;
						case 1:
							output360.Disconnect();
							client.Dispose();
							break;
						case 2:
							break;
						case 3:
							outputMidi.TurnAllNotesOff();
							outputMidi.Dispose();
							break;
					}
					outputStarted = false;
					b.Text = "Start";
					return;
				}
			}
		}

		public void OutputPrep(bool type)
		{
			switch(outputMode)
			{
				default:
					break;
				case 1:
				{
					client = new ViGEmClient();
					output360 = client.CreateXbox360Controller();
					try{output360.Connect();}
					catch(VigemNoFreeSlotException)
					{
						MessageBox.Show("No available XInput slots to start the virtual controller on./r/nPlease disconnect an Xbox 360/Xbox One controller.", "No Free XInput Slots", MessageBoxButtons.OK);
					}

					// Disable all settings except for Drum Mode
					radio_Pedal_Expression.Enabled = false;
					radio_Pedal_ChannelVolume.Enabled = false;
					radio_Pedal_FootController.Enabled = false;

					label_Setting_Octave.Enabled = false;
					label_Setting_Program.Enabled = false;
					numUpDown_Setting_Octave.Enabled = false;
					numUpDown_Setting_Program.Enabled = false;
					checkbox_Setting_DrumMode.Enabled = true;

					break;
				}
				case 2:
				{
					// Disable all settings and output type selection
					radio_Pedal_Expression.Enabled = false;
					radio_Pedal_ChannelVolume.Enabled = false;
					radio_Pedal_FootController.Enabled = false;

					label_Setting_Octave.Enabled = false;
					label_Setting_Program.Enabled = false;
					numUpDown_Setting_Octave.Enabled = false;
					numUpDown_Setting_Program.Enabled = false;
					checkbox_Setting_DrumMode.Enabled = false;

					break;
				}
				case 3:
				{
					string name = dropdown_Output_MidiDevice.SelectedItem.ToString();
					if(name == "No Device")
					{
						MessageBox.Show("Please select a MIDI device to output to.", "No Device Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					}
					outputMidi = OutputDevice.GetByName(name);
					outputMidi.PrepareForEventsSending();

					// Enable all settings, disable output type selection
					radio_Pedal_Expression.Enabled = true;
					radio_Pedal_ChannelVolume.Enabled = true;
					radio_Pedal_FootController.Enabled = true;

					numUpDown_Setting_Octave.Enabled = true;
					numUpDown_Setting_Program.Enabled = true;
					checkbox_Setting_DrumMode.Enabled = true;
					break;
				}
			}
		}

		void UpdateWindow()
		{
			#region Images
			// Update the state of the button/key images based on the current state of each button.
			// Buttons
			if(stateCurrent.btnA) image_AButton.Image = (Image)image.aPressed_cropped;
			else image_AButton.Image = (Image)image.a_cropped;

			if(stateCurrent.btnB) image_BButton.Image = (Image)image.bPressed_cropped;
			else image_BButton.Image = (Image)image.b_cropped;

			if(stateCurrent.btnX) image_XButton.Image = (Image)image.xPressed_cropped;
			else image_XButton.Image = (Image)image.x_cropped;

			if(stateCurrent.btnY) image_YButton.Image = (Image)image.yPressed_cropped;
			else image_YButton.Image = (Image)image.y_cropped;

			if(stateCurrent.btnSt) image_StartButton.Image = (Image)image.startPressed_cropped;
			else image_StartButton.Image = (Image)image.start_cropped;

			if(stateCurrent.btnBk) image_BackButton.Image = (Image)image.backPressed_cropped;
			else image_BackButton.Image = (Image)image.back_cropped;

			if(stateCurrent.overdrive) image_OverdriveButton.Image = (Image)image.overdrivePressed_cropped;
			else image_OverdriveButton.Image = (Image)image.overdrive_cropped;

			// D-pad
			switch(stateCurrent.dpadU, stateCurrent.dpadD, stateCurrent.dpadL, stateCurrent.dpadR)
			{
				// Not pressed
				default:
				case (false, false, false, false):
					image_Dpad.Image = (Image)image.dpad_cropped;
					break;

				// Up
				case (true, false, false, false):
					image_Dpad.Image = (Image)image.dpadUp_cropped;
					break;
				// Down
				case (false, true, false, false):
					image_Dpad.Image = (Image)image.dpadDown_cropped;
					break;
				// Left
				case (false, false, true, false):
					image_Dpad.Image = (Image)image.dpadLeft_cropped;
					break;
				// Right
				case (false, false, false, true):
					image_Dpad.Image = (Image)image.dpadRight_cropped;
					break;
				
				// Up + Down (impossible under normal circumstances)
				case (true, true, false, false):
					image_Dpad.Image = (Image)image.dpadUpDown_cropped;
					break;
				// Up + Left
				case (true, false, true, false):
					image_Dpad.Image = (Image)image.dpadUpLeft_cropped;
					break;
				// Up + Right
				case (true, false, false, true):
					image_Dpad.Image = (Image)image.dpadUpRight_cropped;
					break;
				// Down + Left
				case (false, true, true, false):
					image_Dpad.Image = (Image)image.dpadDownLeft_cropped;
					break;
				// Down + Right
				case (false, true, false, true):
					image_Dpad.Image = (Image)image.dpadDownRight_cropped;
					break;
				// Left + Right (impossible under normal circumstances)
				case (false, false, true, true):
					image_Dpad.Image = (Image)image.dpadDownRight_cropped;
					break;

				// Up + Down + Left (impossible under normal circumstances)
				case (true, true, true, false):
					image_Dpad.Image = (Image)image.dpadDownLeftUp_cropped;
					break;
				// Up + Down + Right (impossible under normal circumstances)
				case (true, true, false, true):
					image_Dpad.Image = (Image)image.dpadUpRightDown_cropped;
					break;
				// Up + Left + Right (impossible under normal circumstances)
				case (true, false, true, true):
					image_Dpad.Image = (Image)image.dpadUpLeftRight_cropped;
					break;
				// Down + Left + Right (impossible under normal circumstances)
				case (false, true, true, true):
					image_Dpad.Image = (Image)image.dpadDownLeftRight_cropped;
					break;

				// Up + Down + Left + Right (impossible under normal circumstances)
				case (true, true, true, true):
					image_Dpad.Image = (Image)image.dpadUpDownLeftRight_cropped;
					break;
			}

			// Guide button
			label_guideConnectionStatus.Visible = !controllerConnected;
			switch (playerIndex)
			{
				default:
				case -1:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedLightsOff_cropped;
					else image_GuideButton.Image = (Image)image.guideLightsOff_cropped;
					break;
				case 0:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer1_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer1_cropped;
					break;
				case 1:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer2_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer2_cropped;
					break;
				case 2:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer3_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer3_cropped;
					break;
				case 3:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer4_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer4_cropped;
					break;
			}

			// Keys
			if(stateCurrent.key[0]) image_KeyC1.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyC1.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[1]) image_KeyDb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyDb1.Image = (Image)image.keyBlack_cropped.Clone();
			
			if(stateCurrent.key[2]) image_KeyD1.Image = (Image)image.keyCenterPressed_cropped.Clone();
			else image_KeyD1.Image = (Image)image.keyCenter_cropped.Clone();

			if(stateCurrent.key[3]) image_KeyEb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyEb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[4]) image_KeyE1.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyE1.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[5]) image_KeyF1.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyF1.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[6]) image_KeyGb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyGb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[7]) image_KeyG1.Image = (Image)image.keyMiddleLeftPressed_cropped.Clone();
			else image_KeyG1.Image = (Image)image.keyMiddleLeft_cropped.Clone();

			if(stateCurrent.key[8]) image_KeyAb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyAb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[9]) image_KeyA1.Image = (Image)image.keyMiddleRightPressed_cropped.Clone();
			else image_KeyA1.Image = (Image)image.keyMiddleRight_cropped.Clone();

			if(stateCurrent.key[10]) image_KeyBb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyBb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[11]) image_KeyB1.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyB1.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[12]) image_KeyC2.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyC2.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[13]) image_KeyDb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyDb2.Image = (Image)image.keyBlack_cropped.Clone();
			
			if(stateCurrent.key[14]) image_KeyD2.Image = (Image)image.keyCenterPressed_cropped.Clone();
			else image_KeyD2.Image = (Image)image.keyCenter_cropped.Clone();

			if(stateCurrent.key[15]) image_KeyEb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyEb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[16]) image_KeyE2.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyE2.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[17]) image_KeyF2.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyF2.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[18]) image_KeyGb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyGb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[19]) image_KeyG2.Image = (Image)image.keyMiddleLeftPressed_cropped.Clone();
			else image_KeyG2.Image = (Image)image.keyMiddleLeft_cropped.Clone();

			if(stateCurrent.key[20]) image_KeyAb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyAb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[21]) image_KeyA2.Image = (Image)image.keyMiddleRightPressed_cropped.Clone();
			else image_KeyA2.Image = (Image)image.keyMiddleRight_cropped.Clone();

			if(stateCurrent.key[22]) image_KeyBb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyBb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[23]) image_KeyB2.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyB2.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[24]) image_KeyC3.Image = (Image)image.keyEndPressed_cropped;
			else image_KeyC3.Image = (Image)image.keyEnd_cropped;
			#endregion
		
			#region Values
			// Toggle drum mode.
			if(checkbox_Setting_DrumMode.Enabled)
			{
				if(stateCurrent.dpadU != statePrevious.dpadU)
				{
					if(stateCurrent.dpadU) drumMode = !drumMode;
					checkbox_Setting_DrumMode.Checked = drumMode;

					if(drumMode) image_MidiLight4.BackColor = Color.Red;
					else image_MidiLight4.BackColor = Color.Black;
				}
			}

			// Set the analog pedal mode.
			if(radio_Pedal_Expression.Enabled)
			{
				if(stateCurrent.dpadL != statePrevious.dpadL)
				{
					if(stateCurrent.dpadL)
					{
						pedalMode = 1;
						radio_Pedal_Expression.Checked = true;
						image_MidiLight2.BackColor = Color.Black;
						image_MidiLight3.BackColor = Color.Black;
					}
				}
			}
			if(radio_Pedal_ChannelVolume.Enabled)
			{
				if(stateCurrent.dpadD != statePrevious.dpadD)
				{
					if(stateCurrent.dpadD)
					{
						pedalMode = 2;
						radio_Pedal_ChannelVolume.Checked = true;
						image_MidiLight2.BackColor = Color.Black;
						image_MidiLight3.BackColor = Color.Red;
					}
				}
			}
			if(radio_Pedal_FootController.Enabled)
			{
				if(stateCurrent.dpadR != statePrevious.dpadR)
				{
					if(stateCurrent.dpadR)
					{
						pedalMode = 3;
						radio_Pedal_FootController.Checked = true;
						image_MidiLight2.BackColor = Color.Red;
						image_MidiLight3.BackColor = Color.Black;
					}
				}
			}

			// Switch the program number.
			if(numUpDown_Setting_Program.Enabled)
			{
				if((stateCurrent.btnA != statePrevious.btnA) || (stateCurrent.btnY != statePrevious.btnY))
				{
					if(stateCurrent.btnA && stateCurrent.btnY)
					{
						program = 1;
					}
					else
					{
						if(stateCurrent.btnA)	
						{
							program -= 1;
						}

						if(stateCurrent.btnY)	
						{
							program += 1;
						}
					}

					Math.Clamp(program, 1, 128);
					numUpDown_Setting_Program.Value = program;
				}
			}

			// Switch the octave number.
			if(numUpDown_Setting_Octave.Enabled)
			{
				if((stateCurrent.btnB != statePrevious.btnB) || (stateCurrent.btnX != statePrevious.btnX))
				{
					if(stateCurrent.btnB && stateCurrent.btnX)
					{
						octave = 4;
					}
					else
					{
						if(stateCurrent.btnX)
						{
							octave -= 1;
						}

						if(stateCurrent.btnB)
						{
							octave += 1;
						}
					}

					Math.Clamp(octave, 0, 8);
					numUpDown_Setting_Octave.Value = octave;
				}
			}
			#endregion
		}
    }
}
