using System;

namespace InOut
{
	/// <summary>
	/// Struct that holds allocated inputs.
	/// </summary>
	public struct InputState
	{
		/// <summary>
		/// The keyboard's bitmask.
		/// </summary>
		/// <remarks>
		/// <para>Indexing:</para>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public bool[] key;

		/// <summary>
		/// The velocities of the keyboard.
		/// </summary>
		/// <remarks>
		/// <para>C1 = 0,  Db1 = 1,  D1 = 2,  Eb1 = 3,  E1 = 4,</para>
		/// <para>F1 = 5,  Gb1 = 6,  G1 = 7,  Ab1 = 8,  A1 = 9,  Bb1 = 10, B1 = 11,</para>
		/// <para>C2 = 12, Db2 = 13, D2 = 14, Eb2 = 15, E2 = 16,</para>
		/// <para>F2 = 17, Gb2 = 18, G2 = 19, Ab2 = 20, A2 = 21, Bb2 = 22, B2 = 23, C3 = 24</para>
		/// </remarks>
		public byte[] velocity;

		/// <summary>
		/// The reported key velocities.
		/// </summary>
		public byte[] vel;

		/// <summary>
		/// The state of the touch modulator. 
		/// </summary>
		/// <remarks>
		/// Unused because I haven't found the input it corresponds to yet, it's hidden like the Guide button is.
		/// </remarks>
		public int modulator;

		/// <summary>
		/// The overdrive button.
		/// </summary>
		public bool overdrive;

		/// <summary>
		/// (probably) The digital portion of the pedal port.
		/// </summary>
		public bool pedalDigital;

		/// <summary>
		/// (probably) The digital portion of the pedal port.
		/// </summary>
		/// <remarks>
		/// Unused because I don't know how this works, or if this is even a functionality lol, don't have an analog pedal to test with.
		/// </remarks>
		public byte pedalAnalog;	

		/// <summary>
		/// The face buttons.
		/// </summary>
		public bool btnA, btnB, btnX, btnY, btnSt, btnBk, btnGuide;

		/// <summary>
		/// The D-pad.
		/// </summary>
		public bool dpadU, dpadD, dpadL, dpadR;

		/// <summary>
		/// Constructs a new InputState instance.
		/// </summary>
		/// <remarks>
		/// The parameter is a dummy parameter to work around structs being unable to have explicit parameterless structs.
		/// </remarks>
		public InputState(bool parameter)
		{
			byte i = 0;
			key = new bool[25];
			foreach(bool b in key)
			{
				key[i] = false;
				i += 1;
			}

			i = 0;
			velocity = new byte[25];
			foreach(byte b in velocity)
			{
				velocity[i] = 0;
				i += 1;
			}

			i = 0;
			vel = new byte[5];
			foreach(byte b in velocity)
			{
				velocity[i] = 0;
				i += 1;
			}

			modulator = 0;
			overdrive = false;
			pedalDigital = false;
			pedalAnalog = 0;

			btnA = false;
			btnB = false;
			btnX = false;
			btnY = false;
			
			btnSt = false;
			btnBk = false;
			btnGuide = false;

			dpadU = false;
			dpadD = false;
			dpadL = false;
			dpadR = false;
		}

		/// <summary>
		/// Clones this instance of the struct.
		/// </summary>		
		public InputState Clone()
		{
			InputState clone = new InputState();

			clone.key = Array.ConvertAll(this.key, a => (bool)a);
			clone.velocity = Array.ConvertAll(this.velocity, a => (byte)a);
			clone.vel = Array.ConvertAll(this.vel, a => (byte)a);

			clone.modulator    = this.modulator;
			clone.overdrive    = this.overdrive;
			clone.pedalDigital = this.pedalDigital;
			clone.pedalAnalog  = this.pedalAnalog;

			clone.btnA = this.btnA;
			clone.btnB = this.btnB;
			clone.btnX = this.btnX;
			clone.btnY = this.btnY;

			clone.btnSt    = this.btnSt;
			clone.btnBk    = this.btnBk;
			clone.btnGuide = this.btnGuide;

			clone.dpadU = this.dpadU;
			clone.dpadD = this.dpadD;
			clone.dpadL = this.dpadL;
			clone.dpadR = this.dpadR;

			return clone;
		}
	}
}