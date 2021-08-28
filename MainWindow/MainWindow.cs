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
		/// 1 = expression, 2 = channel volume, 3 = foot controller.
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
			if(!vigem)
			{
				radio_Output_Xbox360.Text = "ViGEmBus not found";
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
				if(outputMode == 1)
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
