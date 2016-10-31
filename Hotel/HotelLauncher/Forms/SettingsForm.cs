using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using Hotel;

namespace HotelLauncher
{
    public partial class Settings : Form
    {
        public ConfigModel Model { get; private set; }

        private OpenFileDialog _fileDialog;
        private JsonSerializer _jsonSerializer;
        private SelectTexturePackForm _texturePackForm;

        public Settings()
        {
            InitializeComponent();

            _texturePackForm = new SelectTexturePackForm();

            _jsonSerializer = new JsonSerializer();

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
                    Model = _jsonSerializer.Deserialize<ConfigModel>(jsonReader);

                    // Fill the num boxes with values.
                    num_HTETimespan.Value = (decimal)Model.HTELength;
                    num_ElevatorSpeed.Value = (decimal)Model.ElevatorSpeed;
                    num_WalkingSpeed.Value = (decimal)Model.WalkingSpeed;
                    num_FilmDuration.Value = (int)Model.FilmDuration;
                    num_CleaningDuration.Value = (int)Model.CleaningDuration;
                    num_Survivability.Value = (int)Model.Survivability;
                    num_StairsWeight.Value = (decimal)Model.StaircaseWeight;
                    num_ReceptionistWorkSpeed.Value = (decimal)Model.ReceptionistWorkLenght;
                    num_NumberOfCleaners.Value = (decimal)Model.NumberOfCleaners;
                    tb_Layout.Text = Model.LayoutPath;
                }
            }
            catch
            {
                Model = new ConfigModel();
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
            Model.HTELength = (float)num_HTETimespan.Value;
            Model.ElevatorSpeed = (float)num_ElevatorSpeed.Value;
            Model.WalkingSpeed = (float)num_WalkingSpeed.Value;
            Model.FilmDuration = (int)num_FilmDuration.Value;
            Model.EatingDuration = (int)num_EatingDuration.Value;
            Model.CleaningDuration = (int)num_CleaningDuration.Value;
            Model.Survivability = (int)num_Survivability.Value;
            Model.StaircaseWeight = (float)num_StairsWeight.Value;
            Model.ReceptionistWorkLenght = (float)num_ReceptionistWorkSpeed.Value;
            Model.NumberOfCleaners = (int)num_NumberOfCleaners.Value;

            // Select default texturepack if nothing is in the model
            if (Model.TexturePack == null || Model.TexturePack == "")
                Model.TexturePack = @"Content\DefaultTexturePack";

            if (Model.LayoutPath == null || !File.Exists(Model.LayoutPath))
            {
                // Throw an message box at the user.
                MessageBox.Show("Could not find the layout file for the hotel, please select one in the settings menu.", "Could not find file!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                // Save the entered data in a settings file.
                StreamWriter sw = new StreamWriter(@"Config.cfg");
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                _jsonSerializer.Serialize(jsonWriter, Model);
                jsonWriter.Close();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_SelectLayout_Click(object sender, EventArgs e)
        {
            if (_fileDialog.ShowDialog() == DialogResult.OK)
            {
                // This is the path of the file that needs to be opened.
                tb_Layout.Text = _fileDialog.FileName;
                Model.LayoutPath = _fileDialog.FileName;
            }
        }

        private void btn_SelectTexturepack_Click(object sender, EventArgs e)
        {
            // Open the texturepack dialog form
            if(_texturePackForm.ShowDialog() == DialogResult.OK)
            {
                Model.TexturePack = _texturePackForm.SelectedPack;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
