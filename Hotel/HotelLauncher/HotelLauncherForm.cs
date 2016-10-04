using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotelLauncher
{
    public partial class HotelLauncherForm : Form
    {
        private Process _process;
        private Settings _settings;

        public HotelLauncherForm()
        {
            // DO DIS FIRST!!!
            InitializeComponent();

            _settings = new Settings();
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            if(_settings.FilePath == null)
            {
                MessageBox.Show("Please select a hotel layout file in the 'Settings' menu!", "No file loaded!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // FIRE IT UP
            _process = Process.Start("Hotel Simulator.exe");

            this.WindowState = FormWindowState.Minimized;
            while(!_process.HasExited)
            {
                
            }
            this.WindowState = FormWindowState.Normal;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            _settings.ShowDialog();
            
        }
    }
}
