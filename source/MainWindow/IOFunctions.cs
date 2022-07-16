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
				case (int)OutputModes.Xbox360:
					xinput.Output(output360, ref stateCurrent, drumMode);
					break;
				case (int)OutputModes.Keyboard:
					key.Output(outputKey, ref stateCurrent, ref statePrevious);
					break;
				case (int)OutputModes.MIDI:
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

						case (int)OutputModes.Xbox360:
						{
							client = new ViGEmClient();
							output360 = client.CreateXbox360Controller();
							output360.AutoSubmitReport = false;
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

							// Display mappings on the key images
							switch(drumMode)
							{
								case true:
								{
									image_KeyC1.Text = " B\r\n";
									image_KeyDb1.Text = string.Empty;
									image_KeyD1.Text = " Y\r\n";
									image_KeyEb1.Text = string.Empty;
									image_KeyE1.Text = " X\r\n";
									image_KeyF1.Text = " A\r\n";
									image_KeyGb1.Text = string.Empty;
									image_KeyG1.Text = " LB\r\n";
									image_KeyAb1.Text = string.Empty;
									image_KeyA1.Text = " RB\r\n";
									image_KeyBb1.Text = string.Empty;
									image_KeyB1.Text = "LS\r\nClk\r\n";
									image_KeyC2.Text = " B\r\n";
									image_KeyDb2.Text = string.Empty;
									image_KeyD2.Text = " Y\r\n";
									image_KeyEb2.Text = string.Empty;
									image_KeyE2.Text = " X\r\n";
									image_KeyF2.Text = " A\r\n";
									image_KeyGb2.Text = string.Empty;
									image_KeyG2.Text = " LB\r\n";
									image_KeyAb2.Text = string.Empty;
									image_KeyA2.Text = " RB\r\n";
									image_KeyBb2.Text = string.Empty;
									image_KeyB2.Text = "LS\r\nClk\r\n";
									image_KeyC3.Text = "RS\r\nClk\r\n";
									break;
								}
		
								case false:
								{
									image_KeyC1.Text = " A\r\n";
									image_KeyDb1.Text = string.Empty;
									image_KeyD1.Text = " B\r\n";
									image_KeyEb1.Text = string.Empty;
									image_KeyE1.Text = " Y\r\n";
									image_KeyF1.Text = " X\r\n";
									image_KeyGb1.Text = string.Empty;
									image_KeyG1.Text = " LB\r\n";
									image_KeyAb1.Text = string.Empty;
									image_KeyA1.Text = " RB\r\n";
									image_KeyBb1.Text = string.Empty;
									image_KeyB1.Text = "LS\r\nClk\r\n";
									image_KeyC2.Text = " A\r\n";
									image_KeyDb2.Text = string.Empty;
									image_KeyD2.Text = " B\r\n";
									image_KeyEb2.Text = string.Empty;
									image_KeyE2.Text = " Y\r\n";
									image_KeyF2.Text = " X\r\n";
									image_KeyGb2.Text = string.Empty;
									image_KeyG2.Text = " LB\r\n";
									image_KeyAb2.Text = string.Empty;
									image_KeyA2.Text = " RB\r\n";
									image_KeyBb2.Text = string.Empty;
									image_KeyB2.Text = "LS\r\nClk\r\n";
									image_KeyC3.Text = "RS\r\nClk\r\n";
									break;
								}
							}

							break;
						}

						case (int)OutputModes.Keyboard:
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

							// Display mappings on the key images
							image_KeyC1.Text = " Z\r\n";
							image_KeyDb1.Text = "S\r\n";
							image_KeyD1.Text = " X\r\n";
							image_KeyEb1.Text = "D\r\n";
							image_KeyE1.Text = " C\r\n";
							image_KeyF1.Text = " V\r\n";
							image_KeyGb1.Text = "G\r\n";
							image_KeyG1.Text = " B\r\n";
							image_KeyAb1.Text = "H\r\n";
							image_KeyA1.Text = " N\r\n";
							image_KeyBb1.Text = "J\r\n";
							image_KeyB1.Text = " M\r\n";
							image_KeyC2.Text = " Q\r\n";
							image_KeyDb2.Text = "2\r\n";
							image_KeyD2.Text = " W\r\n";
							image_KeyEb2.Text = "3\r\n";
							image_KeyE2.Text = " E\r\n";
							image_KeyF2.Text = " R\r\n";
							image_KeyGb2.Text = "5\r\n";
							image_KeyG2.Text = " T\r\n";
							image_KeyAb2.Text = "6\r\n";
							image_KeyA2.Text = " Y\r\n";
							image_KeyBb2.Text = "7\r\n";
							image_KeyB2.Text = " U\r\n";
							image_KeyC3.Text = " I\r\n";

							break;
						}

						case (int)OutputModes.MIDI:
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

							label_Dpad_DrumMode.Enabled = true;
							label_Dpad_Expression.Enabled = true;
							label_Dpad_ChannelVolume.Enabled = true;
							label_Dpad_FootController.Enabled = true;
							label_OctaveIncrement.Enabled = true;
							label_OctaveDecrement.Enabled = true;
							label_ProgramIncrement.Enabled = true;
							label_ProgramDecrement.Enabled = true;
							
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

					label_Dpad_DrumMode.Enabled = false;
					label_Dpad_Expression.Enabled = false;
					label_Dpad_ChannelVolume.Enabled = false;
					label_Dpad_FootController.Enabled = false;
					label_OctaveIncrement.Enabled = false;
					label_OctaveDecrement.Enabled = false;
					label_ProgramIncrement.Enabled = false;
					label_ProgramDecrement.Enabled = false;

					image_MidiLight1.BackColor = Color.Black;
					image_MidiLight2.BackColor = Color.Black;
					image_MidiLight3.BackColor = Color.Black;
					image_MidiLight4.BackColor = Color.Black;

					image_KeyC1.Text = " C1\r\n";
					image_KeyDb1.Text = "C#\r\nDb\r\n1\r\n";
					image_KeyD1.Text = " D1\r\n";
					image_KeyEb1.Text = "D#\r\nEb\r\n1\r\n";
					image_KeyE1.Text = " E1\r\n";
					image_KeyF1.Text = " F1\r\n";
					image_KeyGb1.Text = "F#\r\nGb\r\n1\r\n";
					image_KeyG1.Text = " G1\r\n";
					image_KeyAb1.Text = "G#\r\nAb\r\n1\r\n";
					image_KeyA1.Text = " A1\r\n";
					image_KeyBb1.Text = "A#\r\nBb\r\n1\r\n";
					image_KeyB1.Text = " B1\r\n";
					image_KeyC2.Text = " C2\r\n";
					image_KeyDb2.Text = "C#\r\nDb\r\n2\r\n";
					image_KeyD2.Text = " D2\r\n";
					image_KeyEb2.Text = "D#\r\nEb\r\n2\r\n";
					image_KeyE2.Text = " E2\r\n";
					image_KeyF2.Text = " F2\r\n";
					image_KeyGb2.Text = "F#\r\nGb\r\n2\r\n";
					image_KeyG2.Text = " G2\r\n";
					image_KeyAb2.Text = "G#\r\nAb\r\n2\r\n";
					image_KeyA2.Text = " A2\r\n";
					image_KeyBb2.Text = "A#\r\nBb\r\n2\r\n";
					image_KeyB2.Text = " B2\r\n";
					image_KeyC3.Text = " C3\r\n";

					switch(outputMode)
					{
						default:
							break;
						case (int)OutputModes.Xbox360:
							xinput.Panic(output360);
							output360.Disconnect();
							client.Dispose();
							break;
						case (int)OutputModes.Keyboard:
							key.Panic(outputKey);
							break;
						case (int)OutputModes.MIDI:
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