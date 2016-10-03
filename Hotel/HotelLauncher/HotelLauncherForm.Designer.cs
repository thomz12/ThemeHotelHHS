namespace HotelLauncher
{
    partial class HotelLauncherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotelLauncherForm));
            this.ScreenPictureBox = new System.Windows.Forms.PictureBox();
            this.StartSimulationButton = new System.Windows.Forms.Button();
            this.HTEButton = new System.Windows.Forms.Button();
            this.LoadLayoutButton = new System.Windows.Forms.Button();
            this.FilePathLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenPictureBox
            // 
            this.ScreenPictureBox.Location = new System.Drawing.Point(12, 12);
            this.ScreenPictureBox.Name = "ScreenPictureBox";
            this.ScreenPictureBox.Size = new System.Drawing.Size(612, 382);
            this.ScreenPictureBox.TabIndex = 0;
            this.ScreenPictureBox.TabStop = false;
            // 
            // StartSimulationButton
            // 
            this.StartSimulationButton.Location = new System.Drawing.Point(218, 418);
            this.StartSimulationButton.Name = "StartSimulationButton";
            this.StartSimulationButton.Size = new System.Drawing.Size(200, 50);
            this.StartSimulationButton.TabIndex = 1;
            this.StartSimulationButton.Text = "Start Simulation";
            this.StartSimulationButton.UseVisualStyleBackColor = true;
            this.StartSimulationButton.Click += new System.EventHandler(this.StartSimulationButton_Click);
            // 
            // HTEButton
            // 
            this.HTEButton.Location = new System.Drawing.Point(424, 418);
            this.HTEButton.Name = "HTEButton";
            this.HTEButton.Size = new System.Drawing.Size(200, 50);
            this.HTEButton.TabIndex = 2;
            this.HTEButton.Text = "HTE Settings";
            this.HTEButton.UseVisualStyleBackColor = true;
            this.HTEButton.Click += new System.EventHandler(this.HTEButton_Click);
            // 
            // LoadLayoutButton
            // 
            this.LoadLayoutButton.Location = new System.Drawing.Point(12, 418);
            this.LoadLayoutButton.Name = "LoadLayoutButton";
            this.LoadLayoutButton.Size = new System.Drawing.Size(200, 50);
            this.LoadLayoutButton.TabIndex = 3;
            this.LoadLayoutButton.Text = "Load Hotel Layout File";
            this.LoadLayoutButton.UseVisualStyleBackColor = true;
            this.LoadLayoutButton.Click += new System.EventHandler(this.LoadLayoutButton_Click);
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.Location = new System.Drawing.Point(12, 397);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(612, 18);
            this.FilePathLabel.TabIndex = 4;
            // 
            // HotelLauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 478);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.LoadLayoutButton);
            this.Controls.Add(this.HTEButton);
            this.Controls.Add(this.StartSimulationButton);
            this.Controls.Add(this.ScreenPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HotelLauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulation Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.ScreenPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ScreenPictureBox;
        private System.Windows.Forms.Button StartSimulationButton;
        private System.Windows.Forms.Button HTEButton;
        private System.Windows.Forms.Button LoadLayoutButton;
        private System.Windows.Forms.Label FilePathLabel;
    }
}

