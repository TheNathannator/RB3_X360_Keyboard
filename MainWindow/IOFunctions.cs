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
using Melanchall.DryWetMidi.Devices;
using WindowsInput;
using Program;

namespace Program
{
	public partial class MainWindow
	{
		void Initialize()
		{
			inputController = input.Initialize();
			if(inputController != null)
			{
				playerIndex = (byte)inputController.UserIndex;
				controllerConnected = true;
				stateCurrent = input.Poll(inputController, statePrevious, true);
			}
		}

		void Input()
		{
			if(!inputController.IsConnected)
			{
				controllerConnected = false;
				playerIndex = -1;
				return;
			}
			statePrevious = stateCurrent;
			stateCurrent = input.Poll(inputController, statePrevious, false);
		}

		void Output()
		{
			switch(outputMode)
			{
				case 1:
					if(drumMode) xinput.DrumModeOutput(output360, stateCurrent);
					else xinput.Output(output360, stateCurrent);
					break;
				case 2:
					key.Output(outputKey, stateCurrent, statePrevious);
					break;
				case 3:
					mid.Output(stateCurrent, statePrevious, outputMidi, pedalMode, octave, program, drumMode);
					break;
			}
		}
	}
}