using System;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Common;
using sevenbit = Melanchall.DryWetMidi.Common.SevenBitNumber;

namespace InOut
{
	/// <summary>
	///  MIDI output code.
	/// </summary>
	public class MIDI
	{
		// TODO: I need to track the first 5 notes before I can be able to do MIDI output properly, otherwise notes won't have velocity.
		// For now, they'll just output at velocity 100.
		
		public static byte[] midiNum = new byte[25];
		//	Indexing:
		//	C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,
		//	F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,
		//	C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,
		//	F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23,
		//	C3 = 24

		public static int octave = 5;

		public void Initialize()
		{
			// unfinished, need to be able to select the device to use.
			foreach (var outputDevice in OutputDevice.GetAll())
			{
			    Console.WriteLine(outputDevice.Name);
			}
		}

		public void Output()
		{
			// Octave selector
			switch(octave)
			{
				case 1:
					midiNum[0]  = 0;  midiNum[1]  = 1;  midiNum[2]  = 2;  midiNum[3]  = 3;  midiNum[4] = 4;
					// C1 = 0,        Db1 = 1,          D1 = 2,           Eb1 = 3,          E1 = 4,
					midiNum[5]  = 5;  midiNum[6]  = 6;  midiNum[7]  = 7;  midiNum[8]  = 8;  midiNum[9] = 9;   midiNum[10] = 10; midiNum[11] = 11;
					// F1 = 5,        Gb1 = 6,          G1 = 7,           Ab1 = 8,          A1 = 9,           Bb1 = 10,         B1 = 11,
					midiNum[12] = 12; midiNum[13] = 13; midiNum[14] = 14; midiNum[15] = 15; midiNum[16] = 16;
					// C2 = 12,       Db2 = 13,         D2 = 14,          Eb2 = 15,         E2 = 16,
					midiNum[17] = 17; midiNum[18] = 18; midiNum[19] = 19; midiNum[20] = 20; midiNum[21] = 21; midiNum[22] = 22; midiNum[23] = 23;
					// C2 = 12,       Db2 = 13,         D2 = 14,          Eb2 = 15,         E2 = 16,
					midiNum[24] = 24;
					// C3 = 24
					break;

				case 2:
					midiNum[0]  = 12; midiNum[1]  = 13; midiNum[2]  = 14; midiNum[3]  = 15; midiNum[4]  = 16;
					// C1 = 12,       Db1 = 13,         D1 = 14,          Eb1 = 15,         E1 = 16,
					midiNum[5]  = 17; midiNum[6]  = 18; midiNum[7]  = 19; midiNum[8]  = 20; midiNum[9]  = 21; midiNum[10] = 22; midiNum[11] = 23;
					// F1 = 17,       Gb1 = 18,         G1 = 19,           Ab1 = 20,        A1 = 21,          Bb1 = 22,         B1 = 23,
					midiNum[12] = 24; midiNum[13] = 25; midiNum[14] = 26; midiNum[15] = 27; midiNum[16] = 28;
					// C2 = 24,       Db2 = 25,         D2 = 26,          Eb2 = 27,         E2 = 28,
					midiNum[17] = 29; midiNum[18] = 30; midiNum[19] = 31; midiNum[20] = 32; midiNum[21] = 33; midiNum[22] = 34; midiNum[23] = 35;
					// F2 = 29,       Gb2 = 30,         G2 = 31,          Ab2 = 32,         A2 = 33,          Bb2 = 34,         B2 = 35,
					midiNum[24] = 36;
					// C3 = 36
					break;

				case 3:
					midiNum[0]  = 24; midiNum[1]  = 25; midiNum[2]  = 26; midiNum[3]  = 27; midiNum[4]  = 28;
					// C1 = 24,       Db1 = 25,         D1 = 26,          Eb1 = 27,         E1 = 28,
					midiNum[5]  = 29; midiNum[6]  = 30; midiNum[7]  = 31; midiNum[8]  = 32; midiNum[9]  = 33; midiNum[10] = 34; midiNum[11] = 35;
					// F1 = 29,       Gb1 = 30,         G1 = 31,          Ab1 = 32,         A1 = 33,          Bb1 = 34,         B1 = 35,
					midiNum[12] = 36; midiNum[13] = 37; midiNum[14] = 38; midiNum[15] = 39; midiNum[16] = 40;
					// C2 = 36,       Db2 = 37,         D2 = 38,          Eb2 = 39,         E2 = 40,
					midiNum[17] = 41; midiNum[18] = 42; midiNum[19] = 43; midiNum[20] = 44; midiNum[21] = 45; midiNum[22] = 46; midiNum[23] = 47;
					// F2 = 41,       Gb2 = 42,         G2 = 43,          Ab2 = 44,         A2 = 45,          Bb2 = 46,         B2 = 47,
					midiNum[24] = 48;
					// C3 = 48
					break;

				case 4:
					midiNum[0]  = 36; midiNum[1]  = 37; midiNum[2]  = 38; midiNum[3]  = 39; midiNum[4]  = 40;
					// C1 = 36,       Db1 = 37,         D1 = 38,          Eb1 = 39,         E1 = 40,
					midiNum[5]  = 41; midiNum[6]  = 42; midiNum[7]  = 43; midiNum[8]  = 44; midiNum[9]  = 45; midiNum[10] = 46; midiNum[11] = 47;
					// F1 = 41,       Gb1 = 42,         G1 = 43,          Ab1 = 44,         A1 = 45,          Bb1 = 46,         B1 = 47,
					midiNum[12] = 48; midiNum[13] = 49; midiNum[14] = 50; midiNum[15] = 51; midiNum[16] = 52;
					// C2 = 48,       Db2 = 49,         D2 = 50,          Eb2 = 51,         E2 = 52,
					midiNum[17] = 53; midiNum[18] = 54; midiNum[19] = 55; midiNum[20] = 56; midiNum[21] = 57; midiNum[22] = 58; midiNum[23] = 59;
					// F2 = 53,       Gb2 = 54,         G2 = 55,          Ab2 = 56,         A2 = 57,          Bb2 = 58,         B2 = 59,
					midiNum[24] = 60;
					// C3 = 60
					break;

				case 5:		// Default range.
					midiNum[0]  = 48; midiNum[1]  = 49; midiNum[2]  = 50; midiNum[3]  = 51; midiNum[4]  = 52;
					// C1 = 48,       Db1 = 49,         D1 = 50,          Eb1 = 51,         E1 = 52,
					midiNum[5]  = 53; midiNum[6]  = 54; midiNum[7]  = 55; midiNum[8]  = 56; midiNum[9]  = 57; midiNum[10] = 58; midiNum[11] = 59;
					// F1 = 53,       Gb1 = 54,         G1 = 55,          Ab1 = 56,         A1 = 57,          Bb1 = 58,         B1 = 59,
					midiNum[12] = 60; midiNum[13] = 61; midiNum[14] = 62; midiNum[15] = 63; midiNum[16] = 64;
					// C2 = 60,       Db2 = 61,         D2 = 62,          Eb2 = 63,         E2 = 64,
					midiNum[17] = 65; midiNum[18] = 66; midiNum[19] = 67; midiNum[20] = 68; midiNum[21] = 69; midiNum[22] = 70; midiNum[23] = 71;
					// F2 = 65,       Gb2 = 66,         G2 = 67,          Ab2 = 68,         A2 = 69,          Bb2 = 70,         B2 = 71,
					midiNum[24] = 72;
					// C3 = 72
					break;

				case 6:
					midiNum[0]  = 60; midiNum[1]  = 61; midiNum[2]  = 62; midiNum[3]  = 63; midiNum[4]  = 64;
					// C1 = 60,       Db1 = 61,         D1 = 62,          Eb1 = 63,         E1 = 64,
					midiNum[5]  = 65; midiNum[6]  = 66; midiNum[7]  = 67; midiNum[8]  = 68; midiNum[9]  = 69; midiNum[10] = 70; midiNum[11] = 71;
					// F1 = 65,       Gb1 = 66,         G1 = 67,          Ab2 = 68,         A2 = 69,          Bb2 = 70,         B2 = 71,
					midiNum[12] = 72; midiNum[13] = 73; midiNum[14] = 74; midiNum[15] = 75; midiNum[16] = 76;
					// C2 = 72,       Db2 = 73,         D2 = 74,          Eb2 = 75,         E2 = 76,
					midiNum[17] = 77; midiNum[18] = 78; midiNum[19] = 79; midiNum[20] = 80; midiNum[21] = 81; midiNum[22] = 82; midiNum[23] = 83;
					// F2 = 77,       Gb2 = 78,         G2 = 79,          Ab2 = 80,         A2 = 81,          Bb2 = 82,         B2 = 83,
					midiNum[24] = 84;
					// C3 = 84
					break;

				case 7:
					midiNum[0]  = 72; midiNum[1]  = 73; midiNum[2]  = 74; midiNum[3]  = 75; midiNum[4]  = 76;
					// C1 = 72,       Db1 = 73,         D1 = 74,          Eb1 = 75,         E1 = 76,
					midiNum[5]  = 77; midiNum[6]  = 78; midiNum[7]  = 79; midiNum[8]  = 80; midiNum[9]  = 81; midiNum[10] = 82; midiNum[11] = 83;
					// F1 = 77,       Gb1 = 78,         G1 = 67,          Ab1 = 68,         A1 = 69,          Bb1 = 70,         B1 = 71,
					midiNum[12] = 84; midiNum[13] = 85; midiNum[14] = 86; midiNum[15] = 87; midiNum[16] = 88;
					// C2 = 84,       Db2 = 85,         D2 = 86,          Eb2 = 87,         E2 = 88,
					midiNum[17] = 89; midiNum[18] = 90; midiNum[19] = 91; midiNum[20] = 92; midiNum[21] = 93; midiNum[22] = 94; midiNum[23] = 95;
					// F2 = 89,       Gb2 = 90,         G2 = 91,          Ab2 = 92,         A2 = 93,          Bb2 = 94,         B2 = 95,
					midiNum[24] = 96;
					// C3 = 96
					break;

				case 8:
					midiNum[0]  = 84;  midiNum[1]  = 85;  midiNum[2]  = 86;  midiNum[3]  = 87;  midiNum[4]  = 88;
					// C1 = 84,        Db1 = 85,          D1 = 86,           Eb1 = 87,          E1 = 88,
					midiNum[5]  = 89;  midiNum[6]  = 90;  midiNum[7]  = 91;  midiNum[8]  = 92;  midiNum[9]  = 93;  midiNum[10] = 94;  midiNum[11] = 95;
					// F1 = 89,        Gb1 = 90,          G1 = 91,           Ab1 = 92,          A1 = 93,           Bb1 = 94,          B1 = 95,
					midiNum[12] = 96;  midiNum[13] = 97;  midiNum[14] = 98;  midiNum[15] = 99;  midiNum[16] = 100;
					// C2 = 96,        Db2 = 97,          D2 = 98,           Eb2 = 99,          E2 = 100,
					midiNum[17] = 101; midiNum[18] = 102; midiNum[19] = 103; midiNum[20] = 104; midiNum[21] = 105; midiNum[22] = 106; midiNum[23] = 107;
					// F2 = 101,       Gb2 = 102,         G2 = 103,          Ab2 = 104,         A2 = 105,          Bb2 = 106,         B2 = 107,
					midiNum[24] = 108;
					// C3 = 108
					break;

				case 9:
					midiNum[0]  = 96;  midiNum[1]  = 97;  midiNum[2]  = 98;  midiNum[3]  = 99;  midiNum[4]  = 100;
					// C1 = 96,        Db1 = 97,          D1 = 98,           Eb1 = 99,          E1 = 100,
					midiNum[5]  = 101; midiNum[6]  = 102; midiNum[7]  = 103; midiNum[8]  = 104; midiNum[9]  = 105; midiNum[10] = 106; midiNum[11] = 107;
					// F1 = 101,       Gb1 = 102,         G1 = 103,          Ab1 = 104,         A1 = 105,          Bb1 = 106,         B1 = 107,
					midiNum[12] = 108; midiNum[13] = 109; midiNum[14] = 110; midiNum[15] = 111; midiNum[16] = 112;
					// C2 = 108,       Db2 = 109,         D2 = 110,          Eb2 = 111,         E2 = 112,
					midiNum[17] = 113; midiNum[18] = 114; midiNum[19] = 115; midiNum[20] = 116; midiNum[21] = 117; midiNum[22] = 118; midiNum[23] = 119;
					// F2 = 113,       Gb2 = 114,         G2 = 115,          Ab2 = 116,         A2 = 117,          Bb2 = 118,         B2 = 119,
					midiNum[24] = 120;
					// C3 = 120
					break;

				default:	// Defaults to case 5
					midiNum[0]  = 48; midiNum[1]  = 49; midiNum[2]  = 50; midiNum[3]  = 51; midiNum[4]  = 52;
					// C1 = 48,       Db1 = 49,         D1 = 50,          Eb1 = 51,         E1 = 52,
					midiNum[5]  = 53; midiNum[6]  = 54; midiNum[7]  = 55; midiNum[8]  = 56; midiNum[9]  = 57; midiNum[10] = 58; midiNum[11] = 59;
					// F1 = 53,       Gb1 = 54,         G1 = 55,          Ab1 = 56,         A1 = 57,          Bb1 = 58,         B1 = 59,
					midiNum[12] = 60; midiNum[13] = 61; midiNum[14] = 62; midiNum[15] = 63; midiNum[16] = 64;
					// C2 = 60,       Db2 = 61,         D2 = 62,          Eb2 = 63,         E2 = 64,
					midiNum[17] = 65; midiNum[18] = 66; midiNum[19] = 67; midiNum[20] = 68; midiNum[21] = 69; midiNum[22] = 70; midiNum[23] = 71;
					// F2 = 65,       Gb2 = 66,         G2 = 67,          Ab2 = 68,         A2 = 69,          Bb2 = 70,         B2 = 71,
					midiNum[24] = 72;
					// C3 = 72
					break;
			}

			// TODO: Just sending inputs based on the current state is naive.
			// I need to keep track of both the previous state and the current state,
			// and only send inputs when the input being checked for actually changes.
			using (var outputDevice = OutputDevice.GetByName("Whatever the output device is. this is incomplete."))
			{
				if(Input.key[0])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[0],  (sevenbit)100));	// C1  = 0, 12, 24, 36, 48, 60, 72, 84, 96
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[0],  (sevenbit)0));

				if(Input.key[1])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[1],  (sevenbit)100));	// C#1 = 1, 13, 25, 37, 49, 61, 73, 85, 97
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[1],  (sevenbit)0));

				if(Input.key[2])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[2],  (sevenbit)100));	// D1  = 2, 14, 26, 38, 50, 62, 74, 86, 98
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[2],  (sevenbit)0));

				if(Input.key[3])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[3],  (sevenbit)100));	// D#1 = 3, 15, 27, 39, 51, 63, 75, 87, 99
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[3],  (sevenbit)0));

				if(Input.key[4])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[4],  (sevenbit)100));	// E1  = 4, 16, 28, 40, 52, 64, 76, 88, 100
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[4],  (sevenbit)0));

				if(Input.key[5])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[5],  (sevenbit)100));	// F1  = 5, 17, 29, 41, 53, 65, 77, 89, 101
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[5],  (sevenbit)0));

				if(Input.key[6])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[6],  (sevenbit)100));	// F#1 = 6, 18, 30, 42, 54, 66, 78, 90, 102
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[6],  (sevenbit)0));

				if(Input.key[7])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[7],  (sevenbit)100));	// G1  = 7, 19, 31, 43, 55, 67, 79, 91, 103
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[7],  (sevenbit)0));

				if(Input.key[8])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[8],  (sevenbit)100));	// G#1 = 8, 20, 32, 44, 56, 68, 80, 92, 104
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[8],  (sevenbit)0));

				if(Input.key[9])  outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[9],  (sevenbit)100));	// A1  = 9, 21, 33, 45, 57, 69, 81, 93, 105
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[9],  (sevenbit)0));

				if(Input.key[10]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[10], (sevenbit)100));	// A#1 = 10, 22, 34, 46, 58, 70, 82, 94, 106
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[10], (sevenbit)0));

				if(Input.key[11]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[11], (sevenbit)100));	// B1  = 11, 23, 35, 47, 59, 71, 83, 95, 107
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[11], (sevenbit)0));

				if(Input.key[12]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[12], (sevenbit)100));	// C2  = 12, 24, 36, 48, 60, 72, 84, 96, 108
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[12], (sevenbit)0));

				if(Input.key[13]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[13], (sevenbit)100));	// C#2 = 13, 25, 37, 49, 61, 73, 85, 97, 109
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[13], (sevenbit)0));
				
				if(Input.key[14]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[14], (sevenbit)100));	// D2  = 14, 26, 38, 50, 62, 74, 86, 98, 110
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[14], (sevenbit)0));

				if(Input.key[15]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[15], (sevenbit)100));	// D#2 = 15, 27, 39, 51, 63, 75, 87, 99, 111
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[15], (sevenbit)0));

				if(Input.key[16]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[16], (sevenbit)100));	// E2  = 16, 28, 40, 52, 64, 76, 88, 100, 112
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[16], (sevenbit)0));

				if(Input.key[17]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[17], (sevenbit)100));	// F2  = 17, 29, 41, 53, 65, 77, 89, 101, 113
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[17], (sevenbit)0));

				if(Input.key[18]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[18], (sevenbit)100));	// F#2 = 18, 30, 42, 54, 66, 78, 90, 102, 114
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[18], (sevenbit)0));

				if(Input.key[19]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[19], (sevenbit)100));	// G2  = 19, 31, 43, 55, 67, 79, 91, 103, 115
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[19], (sevenbit)0));

				if(Input.key[20]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[20], (sevenbit)100));	// G#2 = 20, 32, 44, 56, 68, 80, 92, 104, 116
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[20], (sevenbit)0));

				if(Input.key[21]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[21], (sevenbit)100));	// A2  = 21, 33, 45, 57, 69, 81, 93, 105, 117
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[21], (sevenbit)0));

				if(Input.key[22]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[22], (sevenbit)100));	// A#2 = 22, 34, 46, 58, 70, 82, 94, 106, 118
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[22], (sevenbit)0));

				if(Input.key[23]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[23], (sevenbit)100));	// B2  = 23, 35, 47, 59, 71, 83, 95, 107, 119
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[23], (sevenbit)0));

				if(Input.key[24]) outputDevice.SendEvent(new NoteOnEvent ((sevenbit)midiNum[24], (sevenbit)100));	// C3  = 24, 36, 48, 60, 72, 84, 96, 108, 120
				else 			  outputDevice.SendEvent(new NoteOffEvent((sevenbit)midiNum[24], (sevenbit)0));
			}
		}
	}
}