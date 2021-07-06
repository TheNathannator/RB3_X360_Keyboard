namespace InOut
{
	/// <summary>
	/// Class representing the state of a keyboard.
	/// </summary>
	public class InputState
	{
		/// <summary>
		/// Array of the four ranges the keyboard splits its key bitmask into.
		/// </summary>
		public byte[] range = new byte[4];

		/// <summary>
		/// Array of the keyboard's bitmask.
		/// </summary>
		/// <remarks>
		/// <para>Indexing:</para>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public bool[] key = new bool[25];

		/// <summary>
		/// Array of the keyboard's five velocity values,
		/// plus a constant 100 value for any additional keys pressed.
		/// </summary>
		public byte[] vel = 
		{
			0, 0, 0, 0, 0,
			100
		};

		/// <summary>
		/// Array representing the velocity of a specified key.
		/// </summary>
		/// <remarks>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public byte[] velocity = new byte[25];

		/// <summary>
		/// Integer representing the state of the touch modulator. 
		/// </summary>
		/// <remarks>
		/// Unused because I haven't found the input it corresponds to yet, it's hidden like the Guide button is.
		/// </remarks>
		public int modulator;

		/// <summary>
		/// Boolean representing the overdrive button.
		/// </summary>
		public bool overdrive;

		/// <summary>
		/// Boolean representing the pedal port.
		/// </summary>
		public bool pedal;

		/// <summary>
		/// Booleans representing the face buttons.
		/// </summary>
		public bool btnA, btnB, btnX, btnY, btnSt, btnBk, btnGuide;

		/// <summary>
		/// Booleans representing the D-pad.
		/// </summary>
		public bool dpadU, dpadD, dpadL, dpadR;
	}
}