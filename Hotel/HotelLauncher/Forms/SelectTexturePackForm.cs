using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelLauncher
{
    public partial class SelectTexturePackForm : Form
    {
        private const int OFFSETX = 12;
        private const int OFFSETY = 12;

        private const int ITEMSIZEX = 240;
        private const int ITEMSIZEY = 80;

        private const int PADDINGX = 12;
        private const int PADDINGY = 12;

        public string SelectedPack { get; set; }

        private string _contentFolderPath;
        private string[] _subFolders;

        private int _selectedPackIndex;

        public SelectTexturePackForm()
        {
            InitializeComponent();

            string @executablePath = Path.GetDirectoryName(Application.ExecutablePath);
            _contentFolderPath = executablePath + @"\Content";

            _subFolders = Directory.GetDirectories(_contentFolderPath, "*", SearchOption.AllDirectories);

            // Add buttons for each texturepack
            for (int i = 0; i < _subFolders.Length; i++)
            {
                AddButton(i, new Point(i % 2, i / 2));
            }
        }

        private void AddButton(int index, Point position)
        {
            // The position at which the button must be placed.
            Point relativePosition = new Point(OFFSETX + position.X * ITEMSIZEX + position.X * PADDINGX, OFFSETY + position.Y * ITEMSIZEY + position.Y * PADDINGY);

            // Get some aditional data from the folder.
            Uri contentUri = new Uri(_contentFolderPath);
            Uri packUri = new Uri(_subFolders[index]);
            Uri result = contentUri.MakeRelativeUri(packUri);
            string packName = result.OriginalString.Split('/')[1];
            string packPath = result.OriginalString;

            // Button
            Button btnSelectPack = new Button();
            btnSelectPack.Location = relativePosition;
            btnSelectPack.Name = "btn_SelectPack" + index;
            btnSelectPack.Tag = "btn_SelectPack" + index;
            btnSelectPack.Size = new Size(240, 80);
            btnSelectPack.TabIndex = 2 + index;
            btnSelectPack.UseVisualStyleBackColor = true;
            btnSelectPack.Click += BtnSelectPack_Click;
            Controls.Add(btnSelectPack);
            Controls.SetChildIndex(btnSelectPack, 1);
            
            // Pack thumbnail label
            PictureBox pbThumbnail = new PictureBox();
            pbThumbnail.Parent = btnSelectPack;
            pbThumbnail.Location = new Point(relativePosition.X + 10, relativePosition.Y + 10);
            pbThumbnail.Name = "pb_PackThumbnail" + index;
            pbThumbnail.Tag = "pb_PackThumbnail" + index;
            pbThumbnail.Size = new Size(60, 60);
            pbThumbnail.TabStop = false;
            pbThumbnail.ImageLocation = _subFolders[index] + @"\Thumbnail.png";
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(pbThumbnail);
            Controls.SetChildIndex(pbThumbnail, 0);

            // Pack name label
            Label lblName = new Label();
            lblName.Parent = btnSelectPack;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblName.Location = new Point(relativePosition.X + 76, relativePosition.Y + 10);
            lblName.Name = "lbl_PackName" + index;
            lblName.Tag = "lbl_PackName" + index;
            lblName.Size = new Size(152, 20);
            lblName.Text = packName;
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            lblName.Visible = true;
            Controls.Add(lblName);
            Controls.SetChildIndex(lblName, 0);

            // Pack location label
            Label lblLocation = new Label();
            lblLocation.Parent = btnSelectPack;
            lblLocation.BackColor = Color.Transparent;
            lblLocation.Location = new Point(relativePosition.X + 76, relativePosition.Y + 34);
            lblLocation.Name = "lbl_PackLocation" + index;
            lblLocation.Tag = "lbl_PackLocation" + index;
            lblLocation.Size = new Size(152, 36);
            lblLocation.Text = packPath;
            Controls.Add(lblLocation);
            Controls.SetChildIndex(lblLocation, 0);
        }

        private void BtnSelectPack_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _selectedPackIndex = Int32.Parse(Regex.Split(button.Tag.ToString(), "btn_SelectPack")[1]);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SelectedPack = _subFolders[_selectedPackIndex];
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
