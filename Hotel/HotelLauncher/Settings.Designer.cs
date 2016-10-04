namespace HotelLauncher
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.lbl_HTETimespan = new System.Windows.Forms.Label();
            this.lbl_ElevatorSpeed = new System.Windows.Forms.Label();
            this.lbl_WalkingSpeed = new System.Windows.Forms.Label();
            this.num_HTETimespan = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.lbl_01 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.lbl_FilmDuration = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Select = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tb_Layout = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_HTETimespan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_HTETimespan
            // 
            this.lbl_HTETimespan.Location = new System.Drawing.Point(66, 9);
            this.lbl_HTETimespan.Name = "lbl_HTETimespan";
            this.lbl_HTETimespan.Size = new System.Drawing.Size(100, 19);
            this.lbl_HTETimespan.TabIndex = 0;
            this.lbl_HTETimespan.Text = "Length of HTE";
            this.lbl_HTETimespan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_ElevatorSpeed
            // 
            this.lbl_ElevatorSpeed.Location = new System.Drawing.Point(6, 35);
            this.lbl_ElevatorSpeed.Name = "lbl_ElevatorSpeed";
            this.lbl_ElevatorSpeed.Size = new System.Drawing.Size(160, 19);
            this.lbl_ElevatorSpeed.TabIndex = 1;
            this.lbl_ElevatorSpeed.Text = "Elevator Speed";
            this.lbl_ElevatorSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_WalkingSpeed
            // 
            this.lbl_WalkingSpeed.Location = new System.Drawing.Point(66, 61);
            this.lbl_WalkingSpeed.Name = "lbl_WalkingSpeed";
            this.lbl_WalkingSpeed.Size = new System.Drawing.Size(100, 19);
            this.lbl_WalkingSpeed.TabIndex = 2;
            this.lbl_WalkingSpeed.Text = "Walking Speed";
            this.lbl_WalkingSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // num_HTETimespan
            // 
            this.num_HTETimespan.DecimalPlaces = 1;
            this.num_HTETimespan.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_HTETimespan.Location = new System.Drawing.Point(172, 8);
            this.num_HTETimespan.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num_HTETimespan.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.num_HTETimespan.Name = "num_HTETimespan";
            this.num_HTETimespan.Size = new System.Drawing.Size(69, 20);
            this.num_HTETimespan.TabIndex = 0;
            this.num_HTETimespan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(172, 34);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(69, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 1;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(172, 60);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(69, 20);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl_01
            // 
            this.lbl_01.AutoSize = true;
            this.lbl_01.Location = new System.Drawing.Point(247, 12);
            this.lbl_01.Name = "lbl_01";
            this.lbl_01.Size = new System.Drawing.Size(47, 13);
            this.lbl_01.TabIndex = 6;
            this.lbl_01.Text = "seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "units/HTE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "units/HTE";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(172, 86);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown3.TabIndex = 3;
            this.numericUpDown3.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lbl_FilmDuration
            // 
            this.lbl_FilmDuration.Location = new System.Drawing.Point(66, 85);
            this.lbl_FilmDuration.Name = "lbl_FilmDuration";
            this.lbl_FilmDuration.Size = new System.Drawing.Size(100, 19);
            this.lbl_FilmDuration.TabIndex = 10;
            this.lbl_FilmDuration.Text = "Film Duration";
            this.lbl_FilmDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "HTE(s)";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(207, 279);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(90, 32);
            this.btn_Save.TabIndex = 8;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(66, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Cleaning Duration";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(172, 112);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown4.TabIndex = 4;
            this.numericUpDown4.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "HTE(s)";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(-48, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "Layout";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(382, 253);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(26, 20);
            this.btn_Select.TabIndex = 6;
            this.btn_Select.Text = "...";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(111, 279);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(90, 32);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // tb_Layout
            // 
            this.tb_Layout.Location = new System.Drawing.Point(58, 253);
            this.tb_Layout.Name = "tb_Layout";
            this.tb_Layout.ReadOnly = true;
            this.tb_Layout.Size = new System.Drawing.Size(318, 20);
            this.tb_Layout.TabIndex = 5;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 323);
            this.Controls.Add(this.tb_Layout);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_FilmDuration);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_01);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.num_HTETimespan);
            this.Controls.Add(this.lbl_WalkingSpeed);
            this.Controls.Add(this.lbl_ElevatorSpeed);
            this.Controls.Add(this.lbl_HTETimespan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.num_HTETimespan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_HTETimespan;
        private System.Windows.Forms.Label lbl_ElevatorSpeed;
        private System.Windows.Forms.Label lbl_WalkingSpeed;
        private System.Windows.Forms.NumericUpDown num_HTETimespan;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label lbl_01;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label lbl_FilmDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox tb_Layout;
    }
}