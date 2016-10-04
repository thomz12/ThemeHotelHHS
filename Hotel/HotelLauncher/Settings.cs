using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace HotelLauncher
{
    public partial class Settings : Form
    {
        public string FilePath { get; private set; }

        private OpenFileDialog _fileDialog;
        private ConfigModel _model;
        private JsonSerializer _jsonSerializer;

        public Settings()
        {
            InitializeComponent();

            _jsonSerializer = new JsonSerializer();
            FilePath = null;

            _fileDialog = new OpenFileDialog();
            _fileDialog.RestoreDirectory = true;
            _fileDialog.Multiselect = false;
            _fileDialog.Filter = "layout files (*.layout)|*.layout";

            try
            {
                using (StreamReader sr = new StreamReader(@"Config.cfg"))
                using (JsonReader jsonReader = new JsonTextReader(sr))
                {
                    // Instantiate an object of type model and fill it.
                    _model = _jsonSerializer.Deserialize<ConfigModel>(jsonReader);

                    // Fill the num boxes with values.
                    num_HTETimespan.Value = (decimal)_model.HTELength;
                    num_ElevatorSpeed.Value = (decimal)_model.ElevatorSpeed;
                    num_WalkingSpeed.Value = (decimal)_model.WalkingSpeed;
                    num_FilmDuration.Value = (int)_model.FilmDuration;
                    num_CleaningDuration.Value = (int)_model.CleaningDuration;
                    num_Survivability.Value = (int)_model.Survivability;
                    num_StairsWeight.Value = (decimal)_model.StaircaseWeight;
                    tb_Layout.Text = _model.LayoutPath;
                }
            }
            catch
            {
                _model = new ConfigModel();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // Save the entered data in a model.
            _model.HTELength = (float)num_HTETimespan.Value;
            _model.ElevatorSpeed = (float)num_ElevatorSpeed.Value;
            _model.WalkingSpeed = (float)num_WalkingSpeed.Value;
            _model.FilmDuration = (int)num_FilmDuration.Value;
            _model.CleaningDuration = (int)num_CleaningDuration.Value;
            _model.Survivability = (int)num_Survivability.Value;
            _model.StaircaseWeight = (float)num_StairsWeight.Value;

            if (_model.LayoutPath == null)
            {
                // Throw an error
                MessageBox.Show("Not all fields have been filled in!", "404", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            { 
                // Save the entered data in a settings file.
                StreamWriter sw = new StreamWriter(@"Config.cfg");
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                _jsonSerializer.Serialize(jsonWriter, _model);
                jsonWriter.Close();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if(_fileDialog.ShowDialog() == DialogResult.OK)
            {
                // This is the path of the file that needs to be opened.
                FilePath = _fileDialog.FileName;
                tb_Layout.Text = FilePath;
                _model.LayoutPath = FilePath;
            }
        }
    }
}
