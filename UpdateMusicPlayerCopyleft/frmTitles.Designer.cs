namespace UpdateNativePlayerCopyright
{
    partial class frmTitles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTitles));
            this.picSanjivani = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSanjivani)).BeginInit();
            this.SuspendLayout();
            // 
            // picSanjivani
            // 
            this.picSanjivani.BackColor = System.Drawing.Color.Transparent;
            this.picSanjivani.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSanjivani.BackgroundImage")));
            this.picSanjivani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSanjivani.Location = new System.Drawing.Point(77, 30);
            this.picSanjivani.Name = "picSanjivani";
            this.picSanjivani.Size = new System.Drawing.Size(92, 86);
            this.picSanjivani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSanjivani.TabIndex = 90;
            this.picSanjivani.TabStop = false;
            // 
            // frmTitles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(65)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(252, 152);
            this.Controls.Add(this.picSanjivani);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTitles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTitles";
            this.Load += new System.EventHandler(this.frmTitles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSanjivani)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSanjivani;
    }
}