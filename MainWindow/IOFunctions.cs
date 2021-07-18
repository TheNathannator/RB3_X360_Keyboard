using System.Windows.Forms;

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
				controllerConnected = false;
				playerIndex = -1;
				outputStarted = false;
				button_Start.Text = "Start";
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
	}
}