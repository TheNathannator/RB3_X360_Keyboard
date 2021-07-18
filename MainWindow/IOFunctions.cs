using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Exceptions;
using Melanchall.DryWetMidi.Devices;
using WindowsInput;
using InOut;

namespace Program
{
	public partial class MainWindow
	{
		/// <summary>
		/// Initializes XInput polling.
		/// </summary>
		void Initialize()
		{
			inputController = input.Initialize();
			if(inputController != null)
			{
				playerIndex = (byte)inputController.UserIndex;
				controllerConnected = true;
			}
		}

		/// <summary>
		/// Polls inputs.
		/// </summary>
		void Input()
		{
			// Check if the controller has been disconnected.
			if(!inputController.IsConnected)
			{
				if(outputStarted) OutputToggle(false);

				controllerConnected = false;
				playerIndex = -1;
				return;
			}

			// Store current state into previous and poll for current inputs.
			statePrevious = stateCurrent.Clone();
			input.Poll(inputController, ref stateCurrent, ref statePrevious);
		}

		/// <summary>
		/// Outputs to one of the various output methods.
		/// </summary>
		void Output()
		{
			switch(outputMode)
			{
				case 1:
					if(drumMode) xinput.DrumModeOutput(output360, stateCurrent);
					else xinput.StandardOutput(output360, ref stateCurrent);
					break;
				case 2:
					key.Output(outputKey, ref stateCurrent, ref statePrevious);
					break;
				case 3:
					mid.Output(outputMidi, ref stateCurrent, ref statePrevious, pedalMode, octave, program, drumMode);
					break;
			}
		}

		void OutputToggle(bool state)
		{
			switch(state)
			{
				case true:	// Start output
				{
					if(playerIndex == -1)
					{
						button_Start.Text = "Start";
						MessageBox.Show("No Xbox 360 controllers detected!", "No Controller", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					switch(outputMode)
					{
						default:
							break;

						// Xbox 360 output
						case 1:
						{
							client = new ViGEmClient();
							output360 = client.CreateXbox360Controller();
							try{output360.Connect();}
							catch(VigemNoFreeSlotException)
							{
								MessageBox.Show("No available XInput slots to start the virtual controller on./r/nPlease disconnect an Xbox 360/Xbox One controller.", "No Free XInput Slots", MessageBoxButtons.OK);
								return;
							}

							// Disable all settings except for Drum Mode
							radio_Pedal_Expression.Enabled = false;
							radio_Pedal_ChannelVolume.Enabled = false;
							radio_Pedal_FootController.Enabled = false;

							numUpDown_Setting_Octave.Enabled = false;
							numUpDown_Setting_Program.Enabled = false;
							checkbox_Setting_DrumMode.Enabled = true;
							label_Setting_Octave.Enabled = false;
							label_Setting_Program.Enabled = false;

							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							dropdown_Output_MidiDevice.Enabled = false;

							break;
						}

						// Keyboard output
						case 2:
						{
							outputKey = simulator.Keyboard;

							// Disable all settings and output type selection
							radio_Pedal_Expression.Enabled = false;
							radio_Pedal_ChannelVolume.Enabled = false;
							radio_Pedal_FootController.Enabled = false;

							numUpDown_Setting_Octave.Enabled = false;
							numUpDown_Setting_Program.Enabled = false;
							checkbox_Setting_DrumMode.Enabled = false;
							label_Setting_Octave.Enabled = false;
							label_Setting_Program.Enabled = false;

							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							dropdown_Output_MidiDevice.Enabled = false;

							break;
						}

						// MIDI output
						case 3:
						{
							try{dropdown_Output_MidiDevice.SelectedItem.ToString();}
							catch(NullReferenceException)
							{
								MessageBox.Show("Please select a MIDI device to output to.", "No Device Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							string name = dropdown_Output_MidiDevice.SelectedItem.ToString();
							if(name == "No Device")
							{
								MessageBox.Show("Please select a MIDI device to output to.", "No Device Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
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
							label_Setting_Octave.Enabled = true;
							label_Setting_Program.Enabled = true;

							radio_Output_Xbox360.Enabled = false;
							radio_Output_Keyboard.Enabled = false;
							radio_Output_Midi.Enabled = false;
							dropdown_Output_MidiDevice.Enabled = false;
							
							break;
						}
					}
					outputStarted = true;
					button_Start.Text = "Stop";
					return;
				}

				// Stop output
				case false:
				{
					// Disable settings, enable output type selection
					radio_Pedal_Expression.Enabled = false;
					radio_Pedal_ChannelVolume.Enabled = false;
					radio_Pedal_FootController.Enabled = false;

					numUpDown_Setting_Octave.Enabled = false;
					numUpDown_Setting_Program.Enabled = false;
					checkbox_Setting_DrumMode.Enabled = false;
					label_Setting_Octave.Enabled = false;
					label_Setting_Program.Enabled = false;

					radio_Output_Xbox360.Enabled = vigem;
					radio_Output_Keyboard.Enabled = true;
					radio_Output_Midi.Enabled = true;
					dropdown_Output_MidiDevice.Enabled = true;

					image_MidiLight1.BackColor = Color.Black;
					image_MidiLight2.BackColor = Color.Black;
					image_MidiLight3.BackColor = Color.Black;
					image_MidiLight4.BackColor = Color.Black;

					switch(outputMode)
					{
						default:
							break;
						case 1:
							xinput.Panic(output360);
							output360.Disconnect();
							client.Dispose();
							break;
						case 2:
							key.Panic(outputKey);
							break;
						case 3:
							outputMidi.TurnAllNotesOff();
							outputMidi.Dispose();
							break;
					}
					outputStarted = false;
					button_Start.Text = "Start";
					return;
				}
			}
		}
	}
}