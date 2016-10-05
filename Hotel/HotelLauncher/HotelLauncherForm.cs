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
using Newtonsoft.Json;
using System.IO;

namespace HotelLauncher
{
    public partial class HotelLauncherForm : Form
    {
        private Process _process;
        private Settings _settings;

        private string _configFilePath;

        public HotelLauncherForm()
        {
            // DO DIS FIRST!!!
            InitializeComponent();

            // General instantiations
            _settings = new Settings();

            // Configure variables
            _configFilePath = @"Config.cfg";
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            // Check if we have a file to load the hotel from.
            // Check if a config exists.
            if (File.Exists(_configFilePath))
            {
                // The config file is present so we can read it
                using (StreamReader sr = new StreamReader(_configFilePath))
                using (JsonReader jsonReader = new JsonTextReader(sr))
                {
                    // Create a serializer
                    JsonSerializer js = new JsonSerializer();
                    // Instantiate an object of type model and fill it.
                    ConfigModel model = js.Deserialize<ConfigModel>(jsonReader);

                    // Check if we at least have a file to load.
                    if (model.LayoutPath == null)
                    {
                        // We dont so throw a message at the user.
                        MessageBox.Show("Please select a hotel layout file in the 'Settings' menu!", "No file loaded!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }
            else
            {
                // No config is present so there wont be a file to load the hotel from.
                MessageBox.Show("Please select a hotel layout file in the 'Settings' menu!", "No file loaded!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            // Checks completed.

            // FIRE IT UP
            _process = Process.Start("Hotel Simulator.exe");

            // Minimize the launcher
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            while(!_process.HasExited)
            {
                
            }
            // The process has ended, return the window state to normal.
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            // Open the settings dialog.
            _settings.ShowDialog();
        }
    }
}
