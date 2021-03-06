using System;
using System.Drawing;
using System.Diagnostics;
using System.Timers;
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
		/// See the PedalModes enum for available modes.
		/// </remarks>
		public int pedalMode = 1;
		/// <summary>
		/// The current octave offset.
		/// </summary>
		public byte octave = 4;
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
		/// See the OutputModes enum for available modes.
		/// </remarks>
		int outputMode = 2;

		/// <summary>
		/// Available output modes.
		/// </summary>
		enum OutputModes
		{
			Xbox360 = 1,
			Keyboard = 2,
			MIDI = 3
		}

		/// <summary>
		/// Available pedal modes.
		/// </summary>
		enum PedalModes
		{
			Expression = 1,
			ChannelVolume = 2,
			FootController = 3
		}

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
			try
			{
				var client = new ViGEmClient();
				client.Dispose();
				vigem = true;
				radio_Output_Xbox360.Enabled = true;
			}
			catch (VigemBusNotFoundException)
			{
				vigem = false;
				radio_Output_Xbox360.Text = "ViGEmBus not found";
				radio_Output_Xbox360.Enabled = false;
			}

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
		/// Handles things that needs to be taken care of when the app closes.
		/// </summary>
		private void appClosing(object sender, FormClosingEventArgs e)
		{
			if(outputStarted)
			{
				switch(outputMode)
				{
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
			if(!controllerConnected) Initialize();
			else
			{
				Input();
				UpdateValues();
				if(this.Focused) UpdateImages();
				if(outputStarted) Output();
#if DEBUG
				if(debug.Visible) debug.UpdateWindow(ref stateCurrent);
#endif
			}
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
						pedalMode = (int)PedalModes.Expression;
						return;
					case "Channel Volume":
						pedalMode = (int)PedalModes.ChannelVolume;
						return;
					case "Foot Controller":
						pedalMode = (int)PedalModes.FootController;
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
						if(rb.Checked) outputMode = (int)OutputModes.Xbox360;
						dropdown_Output_MidiDevice.Enabled = false;
						break;
					default:
					case "Keyboard":
						if(rb.Checked) outputMode = (int)OutputModes.Keyboard;
						dropdown_Output_MidiDevice.Enabled = false;
						break;
					case "MIDI":
						if(rb.Checked) outputMode = (int)OutputModes.MIDI;
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

			octave = (byte)(Math.Clamp(ud.Value, 0, 8));
		}

		/// <summary>
		/// Changes the program number.
		/// </summary>
		private void numUpDown_SettingProgram_Change(object sender, EventArgs e)
		{
			NumericUpDown ud = sender as NumericUpDown;

			program = (byte)(Math.Clamp((ud.Value - 1), 0, 127));
		}

		/// <summary>
		/// Toggles drum mode.
		/// </summary>
		private void checkbox_SettingDrumMode_Changed(object sender, EventArgs e)
		{
			drumMode = !drumMode;

			if(outputStarted)
			{
				if(outputMode == (int)OutputModes.Xbox360)
				{
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
				}
			}
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
		/// Toggles output functionality.
		/// </summary>
		private void button_Start_Click(object sender, EventArgs e)
		{
			Button b = sender as Button;
			if(b.Text == "Start") OutputToggle(true);
			else if(b.Text == "Stop") OutputToggle(false);
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
