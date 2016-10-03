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

        public HotelLauncherForm()
        {
            // DO DIS FIRST!!!
            InitializeComponent();

            /* file dialoge shizz
            _fileDialog = new OpenFileDialog();
            _fileDialog.RestoreDirectory = true;
            _fileDialog.Multiselect = false;
            _fileDialog.Filter = "layout files (*.layout)|*.layout|All files (*.*)|*.*";
            */
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
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
            using(Settings settings = new Settings())
            {
                settings.ShowDialog();
            }
        }
    }
}
