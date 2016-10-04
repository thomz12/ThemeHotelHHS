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
            this.pb_Splash = new System.Windows.Forms.PictureBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Settings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Splash)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Splash
            // 
            this.pb_Splash.Image = ((System.Drawing.Image)(resources.GetObject("pb_Splash.Image")));
            this.pb_Splash.Location = new System.Drawing.Point(15, 12);
            this.pb_Splash.Name = "pb_Splash";
            this.pb_Splash.Size = new System.Drawing.Size(612, 382);
            this.pb_Splash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Splash.TabIndex = 0;
            this.pb_Splash.TabStop = false;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(12, 418);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(200, 50);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start Simulation";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.StartSimulationButton_Click);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Location = new System.Drawing.Point(424, 418);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(200, 50);
            this.btn_Settings.TabIndex = 2;
            this.btn_Settings.Text = "Simulation Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // HotelLauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 478);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.pb_Splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HotelLauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulation Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Splash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Splash;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Settings;
    }
}

