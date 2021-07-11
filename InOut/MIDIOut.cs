using System;
using System.Threading;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Common;
using sevenbit = Melanchall.DryWetMidi.Common.SevenBitNumber;

namespace InOut
{
	/// <summary>
	///  MIDI output code.
	/// </summary>
	public class Midi
	{
		// TODO: I need to track the first 5 notes before I can be able to do MIDI output properly, otherwise notes won't have velocity.
		// For now, they'll just output at velocity 100.
		
		/// <summary>
		/// Array of MIDI notes that correspond to each key on the keyboard.
		/// </summary>
		/// <remarks>
		/// <para>Indexing:</para>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public byte[] midiNum = new byte[25];
		public byte midiChannel = 1;

		/// <summary>
		/// Output to a MIDI device.
		/// </summary>
		public void Output(InputState currentState, InputState prevState, OutputDevice outputMidi, int pedalMode, int octave, int program, bool drumMode)
		{
			// Stop all MIDI notes and wait 1 second before resuming.
			if(currentState.btnBk && currentState.btnGuide && currentState.btnSt)
			{
				outputMidi.TurnAllNotesOff();
			}

			SetOctave(octave, drumMode);


			// Sends a Stop message.
			if(currentState.btnBk != prevState.btnBk)
			{
				if(currentState.btnBk) outputMidi.SendEvent(new StopEvent());
			}

			// Sends a Continue message.
			if(currentState.btnGuide != prevState.btnGuide)
			{
				if(currentState.btnGuide) outputMidi.SendEvent(new ContinueEvent());
			}

			// Sends a Start message.
			if(currentState.btnSt != prevState.btnSt)
			{
				if(currentState.btnSt) outputMidi.SendEvent(new StartEvent());
			}


			// Loop through the current and previous state of the key array to check if the state has changed.
			// Only send a new event if there's a change in the state of the input.
			for(int i = 0; i < 25; i++)
			{
				// Find a way to set the channel number
				if(currentState.key[i] != prevState.key[i])
				{
					if(currentState.key[i]) outputMidi.SendEvent(new NoteOnEvent((sevenbit)midiNum[i], (sevenbit)currentState.velocity[i]));
					else outputMidi.SendEvent(new NoteOffEvent((sevenbit)midiNum[i], (sevenbit)0));
				}
			}

			//
			// Touch strip code goes here, once i find its input
			//
		}

		/// <summary>
		/// Sets the array of MIDI notes for each key.
		/// </summary>
		public void SetOctave(int octave, bool drumMode)
		{
			// Procedurally set the MIDI numbers
			for(byte i = 0; i < 25; i++)
			{
				midiNum[i] = (byte)((octave * 12) + i);
			}

			// If Drum Mode is set, use these numbers for the bottom octave instead
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