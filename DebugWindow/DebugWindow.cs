using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InOut;

namespace Program
{
	public partial class DebugWindow : Form
	{
		public DebugWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Updates the window values.
		/// </summary>
		public void UpdateWindow(ref InputState stateCurrent)
		{
			if(stateCurrent.key[0]) text_Key0.Text = "1";
			else text_Key0.Text = "0";

			if(stateCurrent.key[1]) text_Key1.Text = "1";
			else text_Key1.Text = "0";

			if(stateCurrent.key[2]) text_Key2.Text = "1";
			else text_Key2.Text = "0";

			if(stateCurrent.key[3]) text_Key3.Text = "1";
			else text_Key3.Text = "0";

			if(stateCurrent.key[4]) text_Key4.Text = "1";
			else text_Key4.Text = "0";

			if(stateCurrent.key[5]) text_Key5.Text = "1";
			else text_Key5.Text = "0";

			if(stateCurrent.key[6]) text_Key6.Text = "1";
			else text_Key6.Text = "0";

			if(stateCurrent.key[7]) text_Key7.Text = "1";
			else text_Key7.Text = "0";

			if(stateCurrent.key[8]) text_Key8.Text = "1";
			else text_Key8.Text = "0";

			if(stateCurrent.key[9]) text_Key9.Text = "1";
			else text_Key9.Text = "0";

			if(stateCurrent.key[10]) text_Key10.Text = "1";
			else text_Key10.Text = "0";

			if(stateCurrent.key[11]) text_Key11.Text = "1";
			else text_Key11.Text = "0";
			
			if(stateCurrent.key[12]) text_Key12.Text = "1";
			else text_Key12.Text = "0";
			
			if(stateCurrent.key[13]) text_Key13.Text = "1";
			else text_Key13.Text = "0";

			if(stateCurrent.key[14]) text_Key14.Text = "1";
			else text_Key14.Text = "0";

			if(stateCurrent.key[15]) text_Key15.Text = "1";
			else text_Key15.Text = "0";

			if(stateCurrent.key[16]) text_Key16.Text = "1";
			else text_Key16.Text = "0";

			if(stateCurrent.key[17]) text_Key17.Text = "1";
			else text_Key17.Text = "0";

			if(stateCurrent.key[18]) text_Key18.Text = "1";
			else text_Key18.Text = "0";

			if(stateCurrent.key[19]) text_Key19.Text = "1";
			else text_Key19.Text = "0";

			if(stateCurrent.key[20]) text_Key20.Text = "1";
			else text_Key20.Text = "0";

			if(stateCurrent.key[21]) text_Key21.Text = "1";
			else text_Key21.Text = "0";

			if(stateCurrent.key[22]) text_Key22.Text = "1";
			else text_Key22.Text = "0";

			if(stateCurrent.key[23]) text_Key23.Text = "1";
			else text_Key23.Text = "0";

			if(stateCurrent.key[24]) text_Key24.Text = "1";
			else text_Key24.Text = "0";

			text_Velocity0.Text = stateCurrent.velocity[0].ToString();

			text_Velocity1.Text = stateCurrent.velocity[1].ToString();

			text_Velocity2.Text = stateCurrent.velocity[2].ToString();

			text_Velocity3.Text = stateCurrent.velocity[3].ToString();

			text_Velocity4.Text = stateCurrent.velocity[4].ToString();

			text_Velocity5.Text = stateCurrent.velocity[5].ToString();

			text_Velocity6.Text = stateCurrent.velocity[6].ToString();

			text_Velocity7.Text = stateCurrent.velocity[7].ToString();

			text_Velocity8.Text = stateCurrent.velocity[8].ToString();

			text_Velocity9.Text = stateCurrent.velocity[9].ToString();

			text_Velocity10.Text = stateCurrent.velocity[10].ToString();

			text_Velocity11.Text = stateCurrent.velocity[11].ToString();

			text_Velocity12.Text = stateCurrent.velocity[12].ToString();

			text_Velocity13.Text = stateCurrent.velocity[13].ToString();

			text_Velocity14.Text = stateCurrent.velocity[14].ToString();

			text_Velocity15.Text = stateCurrent.velocity[15].ToString();

			text_Velocity16.Text = stateCurrent.velocity[16].ToString();

			text_Velocity17.Text = stateCurrent.velocity[17].ToString();

			text_Velocity18.Text = stateCurrent.velocity[18].ToString();

			text_Velocity19.Text = stateCurrent.velocity[19].ToString();

			text_Velocity20.Text = stateCurrent.velocity[20].ToString();

			text_Velocity21.Text = stateCurrent.velocity[21].ToString();

			text_Velocity22.Text = stateCurrent.velocity[22].ToString();

			text_Velocity23.Text = stateCurrent.velocity[23].ToString();

			text_Velocity24.Text = stateCurrent.velocity[24].ToString();

			text_Vel0.Text = stateCurrent.vel[0].ToString();

			text_Vel1.Text = stateCurrent.vel[1].ToString();

			text_Vel2.Text = stateCurrent.vel[2].ToString();

			text_Vel3.Text = stateCurrent.vel[3].ToString();

			text_Vel4.Text = stateCurrent.vel[4].ToString();
		}
	}
}
