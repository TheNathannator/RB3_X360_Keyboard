using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nefarius.ViGEm.Client;

namespace Program
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool vigem;
			try
            {
                var client = new ViGEmClient();
                client.Dispose();
				vigem = true;
            }
			catch (Nefarius.ViGEm.Client.Exceptions.VigemBusNotFoundException)
            {
                MessageBox.Show("ViGEmBus was not found on the system./r/nXbox 360 controller output functionality will be unavailable until you install it.", "ViGEmBus not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vigem = false;
            }

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow(vigem));
		}
	}
}
