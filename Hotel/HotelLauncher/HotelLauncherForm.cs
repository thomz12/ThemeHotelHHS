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
        private OpenFileDialog _fileDialog;
        private string _openFileDialogDirectory;

        private Process _process;

        public HotelLauncherForm()
        {
            // DO DIS FIRST!!!
            InitializeComponent();

            _fileDialog = new OpenFileDialog();
            _fileDialog.RestoreDirectory = true;
            _fileDialog.Multiselect = false;
            _fileDialog.Filter = "layout files (*.layout)|*.layout|All files (*.*)|*.*";
            //_fileDialog.InitialDirectory = @"\..\";
        }

        private void LoadLayoutButton_Click(object sender, EventArgs e)
        {
            // Open the file dialog and wait for an ok.
            if (_fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Give some feedback.
                FilePathLabel.Text = $"File Opened: {_fileDialog.FileName}";
                // Set the file to use.
            }
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            // FIRE IT UP
            _process = Process.Start("Hotel Simulator.exe");
            this.WindowState = FormWindowState.Minimized;
        }

        private void HTEButton_Click(object sender, EventArgs e)
        {

        }
    }
}
