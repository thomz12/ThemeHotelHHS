using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace HotelLauncher
{
    public partial class Settings : Form
    {
        private OpenFileDialog _fileDialog;
        private ConfigModel _model;
        private JsonSerializer _jsonSerializer;

        public Settings()
        {
            InitializeComponent();

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
                    _model = _jsonSerializer.Deserialize<ConfigModel>(jsonReader);

                    // Fill the num boxes with values.
                    num_HTETimespan.Value = (decimal)_model.HTELength;
                    num_ElevatorSpeed.Value = (decimal)_model.ElevatorSpeed;
                    num_WalkingSpeed.Value = (decimal)_model.WalkingSpeed;
                    num_FilmDuration.Value = (int)_model.FilmDuration;
                    num_CleaningDuration.Value = (int)_model.CleaningDuration;
                    num_Survivability.Value = (int)_model.Survivability;
                    num_StairsWeight.Value = (decimal)_model.StaircaseWeight;
                    num_ReceptionistWorkSpeed.Value = (decimal)_model.ReceptionistWorkLenght;
                    num_NumberOfCleaners.Value = (decimal)_model.NumberOfCleaners;
                    tb_Layout.Text = _model.LayoutPath;
                    tb_TexturePack.Text = _model.TexturePack;
                }
            }
            catch
            {
                _model = new ConfigModel();
                tb_TexturePack.Text = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\DefaultTexturePack";
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
            _model.ReceptionistWorkLenght = (float)num_ReceptionistWorkSpeed.Value;
            _model.NumberOfCleaners = (int)num_NumberOfCleaners.Value;

            // Select default texturepack if nothing is in the model
            if (_model.TexturePack == null || _model.TexturePack == "")
                _model.TexturePack = @"Content\DefaultTexturePack";

            if (_model.LayoutPath == null || !File.Exists(_model.LayoutPath))
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
                _jsonSerializer.Serialize(jsonWriter, _model);
                jsonWriter.Close();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_SelectLayout_Click(object sender, EventArgs e)
        {
            if(_fileDialog.ShowDialog() == DialogResult.OK)
            {
                // This is the path of the file that needs to be opened.
                tb_Layout.Text = _fileDialog.FileName;
                _model.LayoutPath = _fileDialog.FileName;
            }
        }

        private void btn_SelectTexturepack_Click(object sender, EventArgs e)
        {
            string @executablePath = Path.GetDirectoryName(Application.ExecutablePath);
            string @contentFolderPath = @executablePath + @"\Content";

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.SelectedPath = @contentFolderPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of the texture pack folder relative to the executable's file path. 
                Uri contentUri = new Uri(@contentFolderPath);
                Uri packUri = new Uri(folderBrowserDialog.SelectedPath);
                Uri result = contentUri.MakeRelativeUri(packUri);
                // yes this works
                string texturepackRelativePath = result.OriginalString;
                
                // Check if the selected folder contains the right objects
                // We can check for everything but that would be inefficient.
                // Since our program will automatically close when it crashes not checking for every file will not be a problem.
                // The real problem will be displaying a message when a file could not be found.
                if (File.Exists($@"{folderBrowserDialog.SelectedPath}\1x1Cafe.xnb"))
                {
                    tb_TexturePack.Text = folderBrowserDialog.SelectedPath;
                    _model.TexturePack = texturepackRelativePath;
                }
                
            }
        }
    }
}
