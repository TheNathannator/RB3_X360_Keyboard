using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Melanchall.DryWetMidi.Devices;
using InOut;
using image = RB3_X360_Keyboard.Properties.Resources;

namespace Program
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// Represents the currently connected controller.
		/// </summary>
		public Controller inputController;

		/// <summary>
		/// Represents the current input state.
		/// </summary>
		public InputState stateCurrent;
		/// <summary>
		/// Represents the previous input state.
		/// </summary>
		public InputState statePrevious;


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
		/// XInput device reading class.
		/// </summary>
		Input input;
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
		/// Indicates whether or not the output loop has been started.
		/// </summary>
		bool started = false;
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
		/// <summary>
		/// The thread to run the IO loop on.
		/// </summary>
		Thread thread;


		public MainWindow()
		{
			InitializeComponent();
			
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
			catch (Nefarius.ViGEm.Client.Exceptions.VigemBusNotFoundException)
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
			catch(ObjectDisposedException){}
			
			try{outputMidi.Dispose();}
			catch{}
			
		}

		private void radio_PedalMode_Change(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;

			if (rb.Checked)
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

			if (rb.Checked)
			{
				switch (rb.Name)
				{
					case "Xbox 360 Controller":
						outputMode = 1;
						break;
					case "Keyboard":
						outputMode = 2;
						break;
					case "MIDI":
						outputMode = 3;
						break;
					default: break;
				}
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

			foreach (var device in OutputDevice.GetAll())
			{
				string name = device.Name.ToString();
				cb.Items.Add(name);
			}
		}

		private void dropdown_OutputMidi_Device_Changed(object sender, EventArgs e)
		{
			ComboBox cb = sender as ComboBox;

			outputMidi = OutputDevice.GetByName(cb.SelectedValue.ToString());
		}

		private void button_Start_Click(object sender, EventArgs e)
		{
			Button b = sender as Button;

			switch(started)
			{
				case false:
				{
					inputController = input.InitializePoll();
					playerIndex = (byte)inputController.UserIndex;

					if(playerIndex == -1)
					{
						started = false;
						b.Text = "Start";
						MessageBox.Show("No Xbox 360 controllers detected!", "No Controller", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					switch(outputMode)
					{
						default:
							break;
						case 1:
						{
							output360 = xinput.Initialize();
							radio_Pedal_Expression.Enabled = false;
							radio_Pedal_ChannelVolume.Enabled = false;
							radio_Pedal_FootController.Enabled = false;
							numUpDown_Setting_Octave.Enabled = false;
							numUpDown_Setting_Program.Enabled = false;
							checkbox_Setting_DrumMode.Enabled = true;
							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							break;
						}
						case 2:
						{
							radio_Pedal_Expression.Enabled = false;
							radio_Pedal_ChannelVolume.Enabled = false;
							radio_Pedal_FootController.Enabled = false;
							numUpDown_Setting_Octave.Enabled = false;
							numUpDown_Setting_Program.Enabled = false;
							checkbox_Setting_DrumMode.Enabled = false;
							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							break;
						}
						case 3:
						{
							outputMidi.PrepareForEventsSending();
							radio_Pedal_Expression.Enabled = true;
							radio_Pedal_ChannelVolume.Enabled = true;
							radio_Pedal_FootController.Enabled = true;
							numUpDown_Setting_Octave.Enabled = true;
							numUpDown_Setting_Program.Enabled = true;
							checkbox_Setting_DrumMode.Enabled = true;
							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							break;
						}
					}

					switch (playerIndex)
					{
						default:
						case -1:
						{
							image_GuideButton.Image = image.guideLightsOff_cropped;
							break;
						}
						case 0:
						{
							image_GuideButton.Image = image.guidePlayer1_cropped;
							break;
						}
						case 1:
						{
							image_GuideButton.Image = image.guidePlayer2_cropped;
							break;
						}
						case 2:
						{
							image_GuideButton.Image = image.guidePlayer3_cropped;
							break;
						}
						case 3:
						{
							image_GuideButton.Image = image.guidePlayer4_cropped;
							break;
						}
					}

					started = true;
					b.Text = "Stop";
					thread = new System.Threading.Thread(new System.Threading.ThreadStart(Loop));
					thread.Start();
					return;
				}

				default:
				case true:
				{
					radio_Pedal_Expression.Enabled = true;
					radio_Pedal_ChannelVolume.Enabled = true;
					radio_Pedal_FootController.Enabled = true;
					numUpDown_Setting_Octave.Enabled = true;
					numUpDown_Setting_Program.Enabled = true;
					checkbox_Setting_DrumMode.Enabled = true;
					radio_Output_Xbox360.Enabled = vigem;
					radio_Output_Keyboard.Enabled = true;
					radio_Output_Midi.Enabled = true;

					switch(outputMode)
					{
						default:
							break;
						case 1:
							output360.Disconnect();
							break;
						case 2:
							break;
						case 3:
							outputMidi.TurnAllNotesOff();
							outputMidi.Dispose();
							break;
					}

					started = false;
					b.Text = "Start";
					return;
				}
			}
		}

		void Loop()
		{
			while(started)
			{
				(stateCurrent, statePrevious) = input.Poll(inputController);
				updateWindow();
				switch(outputMode)
				{
					case 1:
						if(drumMode) xinput.DrumModeOutput(output360, stateCurrent);
						else xinput.Output(output360, stateCurrent);
						break;
					case 2:
						key.Output(stateCurrent, statePrevious);
						break;
					case 3:
						mid.Output(stateCurrent, statePrevious, outputMidi, pedalMode, octave, program, drumMode);
						break;
				}
			}
		}

		void updateWindow()
		{
			// Update the state of the button/key labels based on the current state of each button.
			// Buttons
			if(stateCurrent.btnA) image_AButton.Image = image.aPressed_cropped;
			else image_AButton.Image = image.a_cropped;

			if(stateCurrent.btnB) image_BButton.Image = image.bPressed_cropped;
			else image_BButton.Image = image.b_cropped;

			if(stateCurrent.btnX) image_XButton.Image = image.xPressed_cropped;
			else image_XButton.Image = image.x_cropped;

			if(stateCurrent.btnY) image_YButton.Image = image.yPressed_cropped;
			else image_YButton.Image = image.y_cropped;

			if(stateCurrent.btnSt) image_StartButton.Image = image.startPressed_cropped;
			else image_StartButton.Image = image.start_cropped;

			if(stateCurrent.btnBk) image_BackButton.Image = image.backPressed_cropped;
			else image_BackButton.Image = image.back_cropped;

			if(stateCurrent.btnBk) image_BackButton.Image = image.backPressed_cropped;
			else image_BackButton.Image = image.back_cropped;

			if(stateCurrent.btnGuide)
			{
				switch (playerIndex)
				{
					default:
					case -1:
					{
						image_GuideButton.Image = image.guidePressedLightsOff_cropped;
						break;
					}
					case 0:
					{
						image_GuideButton.Image = image.guidePressedPlayer1_cropped;
						break;
					}
					case 1:
					{
						image_GuideButton.Image = image.guidePressedPlayer2_cropped;
						break;
					}
					case 2:
					{
						image_GuideButton.Image = image.guidePressedPlayer3_cropped;
						break;
					}
					case 3:
					{
						image_GuideButton.Image = image.guidePressedPlayer4_cropped;
						break;
					}
				}
			}
			else
			{
				switch (playerIndex)
				{
					default:
					case -1:
					{
						image_GuideButton.Image = image.guideLightsOff_cropped;
						break;
					}
					case 0:
					{
						image_GuideButton.Image = image.guidePlayer1_cropped;
						break;
					}
					case 1:
					{
						image_GuideButton.Image = image.guidePlayer2_cropped;
						break;
					}
					case 2:
					{
						image_GuideButton.Image = image.guidePlayer3_cropped;
						break;
					}
					case 3:
					{
						image_GuideButton.Image = image.guidePlayer4_cropped;
						break;
					}
				}
			}

			if(stateCurrent.overdrive) image_OverdriveButton.Image = image.overdrivePressed_cropped;
			else image_OverdriveButton.Image = image.overdrive_cropped;

			// Keys
			if(stateCurrent.key[0]) image_KeyC1.Image = image.keyLeftPressed_cropped;
			else image_KeyC1.Image = image.keyLeft_cropped;

			if(stateCurrent.key[1]) image_KeyDb1.Image = image.keyBlackPressed_cropped;
			else image_KeyDb1.Image = image.keyBlack_cropped;
			
			if(stateCurrent.key[2]) image_KeyD1.Image = image.keyCenterPressed_cropped;
			else image_KeyD1.Image = image.keyCenter_cropped;

			if(stateCurrent.key[3]) image_KeyEb1.Image = image.keyBlackPressed_cropped;
			else image_KeyEb1.Image = image.keyBlack_cropped;

			if(stateCurrent.key[4]) image_KeyE1.Image = image.keyRightPressed_cropped;
			else image_KeyE1.Image = image.keyRight_cropped;

			if(stateCurrent.key[5]) image_KeyF1.Image = image.keyLeftPressed_cropped;
			else image_KeyF1.Image = image.keyLeft_cropped;

			if(stateCurrent.key[6]) image_KeyGb1.Image = image.keyBlackPressed_cropped;
			else image_KeyGb1.Image = image.keyBlack_cropped;

			if(stateCurrent.key[7]) image_KeyG1.Image = image.keyMiddleLeftPressed_cropped;
			else image_KeyG1.Image = image.keyMiddleLeft_cropped;

			if(stateCurrent.key[8]) image_KeyAb1.Image = image.keyBlackPressed_cropped;
			else image_KeyAb1.Image = image.keyBlack_cropped;

			if(stateCurrent.key[9]) image_KeyA1.Image = image.keyMiddleRightPressed_cropped;
			else image_KeyA1.Image = image.keyMiddleRight_cropped;

			if(stateCurrent.key[10]) image_KeyBb1.Image = image.keyBlackPressed_cropped;
			else image_KeyBb1.Image = image.keyBlack_cropped;

			if(stateCurrent.key[11]) image_KeyB1.Image = image.keyRightPressed_cropped;
			else image_KeyB1.Image = image.keyRight_cropped;

			if(stateCurrent.key[12]) image_KeyC2.Image = image.keyLeftPressed_cropped;
			else image_KeyC2.Image = image.keyLeft_cropped;

			if(stateCurrent.key[13]) image_KeyDb2.Image = image.keyBlackPressed_cropped;
			else image_KeyDb2.Image = image.keyBlack_cropped;
			
			if(stateCurrent.key[14]) image_KeyD2.Image = image.keyCenterPressed_cropped;
			else image_KeyD2.Image = image.keyCenter_cropped;

			if(stateCurrent.key[15]) image_KeyEb2.Image = image.keyBlackPressed_cropped;
			else image_KeyEb2.Image = image.keyBlack_cropped;

			if(stateCurrent.key[16]) image_KeyE2.Image = image.keyRightPressed_cropped;
			else image_KeyE2.Image = image.keyRight_cropped;

			if(stateCurrent.key[17]) image_KeyF2.Image = image.keyLeftPressed_cropped;
			else image_KeyF2.Image = image.keyLeft_cropped;

			if(stateCurrent.key[18]) image_KeyGb2.Image = image.keyBlackPressed_cropped;
			else image_KeyGb2.Image = image.keyBlack_cropped;

			if(stateCurrent.key[19]) image_KeyG2.Image = image.keyMiddleLeftPressed_cropped;
			else image_KeyG2.Image = image.keyMiddleLeft_cropped;

			if(stateCurrent.key[20]) image_KeyAb2.Image = image.keyBlackPressed_cropped;
			else image_KeyAb2.Image = image.keyBlack_cropped;

			if(stateCurrent.key[21]) image_KeyA2.Image = image.keyMiddleRightPressed_cropped;
			else image_KeyA2.Image = image.keyMiddleRight_cropped;

			if(stateCurrent.key[22]) image_KeyBb2.Image = image.keyBlackPressed_cropped;
			else image_KeyBb2.Image = image.keyBlack_cropped;

			if(stateCurrent.key[23]) image_KeyB2.Image = image.keyRightPressed_cropped;
			else image_KeyB2.Image = image.keyRight_cropped;

			if(stateCurrent.key[24]) image_KeyC3.Image = image.keyEndPressed_cropped;
			else image_KeyC3.Image = image.keyEnd_cropped;

			// Toggle drum mode.
			if(stateCurrent.dpadU != statePrevious.dpadU)
			{
				if(stateCurrent.dpadU) drumMode = !drumMode;
				checkbox_Setting_DrumMode.Checked = drumMode;
			}

			// Set the analog pedal mode.
			if(stateCurrent.dpadL != statePrevious.dpadL)
			{
				if(stateCurrent.dpadL) pedalMode = 1;
				radio_Pedal_Expression.Checked = true;
			}
			if(stateCurrent.dpadD != statePrevious.dpadD)
			{
				if(stateCurrent.dpadD) pedalMode = 2;
				radio_Pedal_ChannelVolume.Checked = true;
			}
			if(stateCurrent.dpadR != statePrevious.dpadR)
			{
				if(stateCurrent.dpadR) pedalMode = 3;
				radio_Pedal_FootController.Checked = true;
			}

			// Switch the program number.
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

			// Switch the octave number.
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
	}
}
