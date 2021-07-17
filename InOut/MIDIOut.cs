using System.Diagnostics;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using sevenbit = Melanchall.DryWetMidi.Common.SevenBitNumber;
using fourbit = Melanchall.DryWetMidi.Common.FourBitNumber;

namespace InOut
{
	/// <summary>
	/// MIDI output code.
	/// </summary>
	public class Midi
	{		
		/// <summary>
		/// MIDI notes to be used.
		/// </summary>
		/// <remarks>
		/// <para>Indexing:</para>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		private byte[] midiNum = new byte[25];
		/// <summary>
		/// The previous program number.
		/// </summary>	
		private byte previousProgram = 1;


		/// <summary>
		/// Outputs to a MIDI device.
		/// </summary>
		public void Output(OutputDevice outputMidi, ref InputState stateCurrent, ref InputState statePrevious, int pedalMode, int octave, byte program, bool drumMode)
		{
			// Turns off all MIDI notes.
			if(stateCurrent.btnBk && stateCurrent.btnGuide && stateCurrent.btnSt)
			{
				outputMidi.TurnAllNotesOff();
			}

			SetOctave(octave, drumMode);

			// Sends a Stop message.
			if(stateCurrent.btnBk != statePrevious.btnBk)
			{
				if(stateCurrent.btnBk) outputMidi.SendEvent(new StopEvent());
			}

			// Sends a Continue message.
			if(stateCurrent.btnGuide != statePrevious.btnGuide)
			{
				if(stateCurrent.btnGuide) outputMidi.SendEvent(new ContinueEvent());
			}

			// Sends a Start message.
			if(stateCurrent.btnSt != statePrevious.btnSt)
			{
				if(stateCurrent.btnSt) outputMidi.SendEvent(new StartEvent());
			}

			if(program != previousProgram)
			{
				outputMidi.SendEvent(new ProgramChangeEvent((sevenbit)program));
				previousProgram = program;
			}

			// Sends note on/off events.
			for(int i = 0; i < 25; i++)
			{
				NoteOnEvent noteOn   = new NoteOnEvent((sevenbit)midiNum[i], (sevenbit)stateCurrent.velocity[i]);
				NoteOffEvent noteOff = new NoteOffEvent((sevenbit)midiNum[i], (sevenbit)0);
				if(drumMode && i < 12)
				{
					noteOn.Channel  = (fourbit)9;
					noteOff.Channel = (fourbit)9;
				}
				else
				{
					noteOn.Channel  = (fourbit)0;
					noteOff.Channel = (fourbit)0;
				}
				
				if(stateCurrent.key[i] != statePrevious.key[i])
				{
					if(stateCurrent.key[i]) outputMidi.SendEvent(noteOn);
					else outputMidi.SendEvent(noteOff);
				}
			}

			//
			// Touch strip code goes here, once I find out how to read it.
			//
		}

		/// <summary>
		/// Sets the array of MIDI notes for each key.
		/// </summary>
		public void SetOctave(int octave, bool drumMode)
		{
			// Sets the MIDI numbers.
			for(byte i = 0; i < 25; i++)
			{
				midiNum[i] = (byte)((octave * 12) + i);
			}

			// If Drum Mode is set, use these numbers for the bottom octave instead.
			if(drumMode)
			{
				midiNum[0] = 35;        midiNum[1] = 36; midiNum[2] = 38; midiNum[3] = 40; midiNum[4] = 41;
				// Acoustic Bass Drum,  Bass Drum 1,     Acoustic Snare,  Electric Snare,  Low Floor Tom,
				midiNum[5] = 47;        midiNum[6] = 50; midiNum[7] = 42; midiNum[8] = 46; midiNum[9] = 49; midiNum[10] = 51; midiNum[11] = 53;
				// Low Mid Tom,         High Tom,        Closed Hi Hat,   Open Hi Hat,     Crash Cymbal 1,  Ride Cymbal 1,    Ride Bell
			}
		}
	}
}