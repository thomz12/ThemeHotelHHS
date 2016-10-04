using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelLauncher
{
    public partial class Settings : Form
    {
        public string FilePath { get; private set; }

        private OpenFileDialog _fileDialog;
        private ConfigModel model;

        public Settings()
        {
            InitializeComponent();

            model = new ConfigModel();
            FilePath = null;

            _fileDialog = new OpenFileDialog();
            _fileDialog.RestoreDirectory = true;
            _fileDialog.Multiselect = false;
            _fileDialog.Filter = "layout files (*.layout)|*.layout[";
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // TODO
            // Save the entered data in a settings file.
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if(_fileDialog.ShowDialog() == DialogResult.OK)
            {
                // This is the path of the file that needs to be opened.
                FilePath = _fileDialog.FileName;
                tb_Layout.Text = FilePath;
                model.LayoutPath = FilePath;
            }
        }
    }
}
