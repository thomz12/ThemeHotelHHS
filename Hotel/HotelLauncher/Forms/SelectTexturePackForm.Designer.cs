namespace HotelLauncher
{
    public partial class SelectTexturePackForm
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.pnl_GreenPanel = new System.Windows.Forms.Panel();
            this.pnl_GreenPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Save.Location = new System.Drawing.Point(3, 373);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(115, 23);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Parent = this;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(3, 344);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Parent = this;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // pnl_GreenPanel
            // 
            this.pnl_GreenPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnl_GreenPanel.BackColor = System.Drawing.Color.LimeGreen;
            this.pnl_GreenPanel.Controls.Add(this.btn_Save);
            this.pnl_GreenPanel.Controls.Add(this.btn_Cancel);
            this.pnl_GreenPanel.Location = new System.Drawing.Point(519, 0);
            this.pnl_GreenPanel.Name = "pnl_GreenPanel";
            this.pnl_GreenPanel.Size = new System.Drawing.Size(121, 405);
            this.pnl_GreenPanel.TabIndex = 2;
            // 
            // SelectTexturePackForm
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(639, 405);
            this.Controls.Add(this.pnl_GreenPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTexturePackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Texture Pack";
            this.pnl_GreenPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void Pnl_GreenPanel_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Panel pnl_GreenPanel;
    }
}