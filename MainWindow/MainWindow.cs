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
		public InputState stateCurrent = new InputState(true);
		/// <summary>
		/// Represents the previous input state.
		/// </summary>
		public InputState statePrevious = new InputState(true);


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
		InputSimulator simulator = new InputSimulator();
		/// <summary>
		/// Represents the keyboard simulator service.
		/// </summary>
		IKeyboardSimulator outputKey;


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
		public int pedalMode = 1;
		/// <summary>
		/// The current octave offset.
		/// </summary>
		public int octave = 4;
		/// <summary>
		/// The current program number.
		/// </summary>
		public byte program = 1;
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

		/// <summary>
		/// Timer for MIDI light animations.
		/// </summary>
		System.Timers.Timer animationTimer;

		/// <summary>
		/// Window that shows some debug information.
		/// </summary>
		DebugWindow debug = new DebugWindow();


		public MainWindow()
		{
			InitializeComponent();

			// Only allow Xbox 360 controller output if ViGEmBus is found.
			vigem = ViGEmCheck();
			radio_Output_Xbox360.Enabled = vigem;
#if DEBUG
			// Only show the debug button and group if current mode is Debug.
			group_Debug.Visible = true;
			button_Debug.Visible = true;
#endif

			// Initialize the animation timer.
			animationTimer = new System.Timers.Timer(100);
			animationTimer.Elapsed += animationTimer_Elapsed;
			animationTimer.AutoReset = true;
		}

		/// <summary>
		/// Checks if ViGEmBus is installed.
		/// </summary>
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

		/// <summary>
		/// Handles things that needs to be taken care of when the app closes.
		/// </summary>
		private void appClosing(object sender, FormClosingEventArgs e)
		{
			if(outputStarted)
			{
				switch(outputMode)
				{
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
				return;
			}

			try{output360.Disconnect();}
			catch{}
			
			try{client.Dispose();}
			catch{}
			
			try{outputMidi.TurnAllNotesOff();}
			catch{}

			try{outputMidi.Dispose();}
			catch{}
		}

		/// <summary>
		/// Functionality loop.
		/// </summary>
		private void timer_IOLoop_Tick(object sender, EventArgs e)
		{
			if(controllerConnected)
			{
				Input();
				UpdateValues();
				UpdateImages();
				if(outputStarted) Output();
#if DEBUG
				if(debug.Visible) debug.UpdateWindow(ref stateCurrent);
#endif
			}
			else Initialize();
		}

		/// <summary>
		/// Changes the pedal mode.
		/// </summary>
		private void radio_PedalMode_Change(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;

			if(rb.Checked)
			{
				switch (rb.Text)
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

		/// <summary>
		/// Changes the output type.
		/// </summary>
		private void radio_OutputType_Change(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			
			dropdown_Output_MidiDevice.Items.Clear();

			if(rb.Checked)
			{
				switch (rb.Text)
				{
					case "Xbox 360 Controller":
						if(rb.Checked) outputMode = 1;
						dropdown_Output_MidiDevice.Enabled = false;
						break;
					default:
					case "Keyboard":
						if(rb.Checked) outputMode = 2;
						dropdown_Output_MidiDevice.Enabled = false;
						break;
					case "MIDI":
						if(rb.Checked) outputMode = 3;
						dropdown_Output_MidiDevice.Enabled = true;
						break;
				}
			}
		}

		/// <summary>
		/// Changes the octave setting.
		/// </summary>
		private void numUpDown_SettingOctave_Change(object sender, EventArgs e)
		{
			NumericUpDown ud = sender as NumericUpDown;

			octave = (int)ud.Value;
		}

		/// <summary>
		/// Changes the program number.
		/// </summary>
		private void numUpDown_SettingProgram_Change(object sender, EventArgs e)
		{
			NumericUpDown ud = sender as NumericUpDown;

			program = (byte)ud.Value;
		}

		/// <summary>
		/// Toggles drum mode.
		/// </summary>
		private void checkbox_SettingDrumMode_Changed(object sender, EventArgs e)
		{
			drumMode = !drumMode;
		}

		/// <summary>
		/// Populates the MIDI device list.
		/// </summary>
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

		/// <summary>
		/// Starts output functionality.
		/// </summary>
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
					label_Setting_Octave.Enabled = false;
					label_Setting_Program.Enabled = false;

					radio_Output_Xbox360.Enabled = vigem;
					radio_Output_Keyboard.Enabled = true;
					radio_Output_Midi.Enabled = true;
					dropdown_Output_MidiDevice.Enabled = true;

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
					b.Text = "Start";
					return;
				}
			}
		}

		/// <summary>
		/// Opens the debug window.
		/// </summary>
		private void button_Debug_Click(object sender, EventArgs e)
		{
			if(debug.IsDisposed)
			{
				debug = new DebugWindow();
			}
			debug.Show();
		}
	}
}
