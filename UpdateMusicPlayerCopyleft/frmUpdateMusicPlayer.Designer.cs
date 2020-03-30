namespace UpdateNativePlayerCopyright
{
    partial class frmUpdateMusicPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateMusicPlayer));
            this.panMain = new System.Windows.Forms.Panel();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panelMain = new System.Windows.Forms.Panel();
            this.picSanjivani = new System.Windows.Forms.PictureBox();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panMain.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSanjivani)).BeginInit();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.Transparent;
            this.panMain.Controls.Add(this.lblPercentage);
            this.panMain.Controls.Add(this.progressBar1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panMain.Location = new System.Drawing.Point(0, 187);
            this.panMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(764, 53);
            this.panMain.TabIndex = 0;
            // 
            // lblPercentage
            // 
            this.lblPercentage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPercentage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPercentage.ForeColor = System.Drawing.Color.Yellow;
            this.lblPercentage.Location = new System.Drawing.Point(0, 0);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(764, 23);
            this.lblPercentage.TabIndex = 2;
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(65)))), ((int)(((byte)(66)))));
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.ForeColor = System.Drawing.Color.Yellow;
            this.progressBar1.Location = new System.Drawing.Point(0, 30);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(764, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(65)))), ((int)(((byte)(66)))));
            this.panelMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMain.Controls.Add(this.picSanjivani);
            this.panelMain.Controls.Add(this.lblUpdate);
            this.panelMain.Controls.Add(this.panMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(764, 240);
            this.panelMain.TabIndex = 1;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // picSanjivani
            // 
            this.picSanjivani.BackColor = System.Drawing.Color.Transparent;
            this.picSanjivani.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSanjivani.BackgroundImage")));
            this.picSanjivani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSanjivani.Location = new System.Drawing.Point(320, 20);
            this.picSanjivani.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picSanjivani.Name = "picSanjivani";
            this.picSanjivani.Size = new System.Drawing.Size(123, 106);
            this.picSanjivani.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSanjivani.TabIndex = 89;
            this.picSanjivani.TabStop = false;
            // 
            // lblUpdate
            // 
            this.lblUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUpdate.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.ForeColor = System.Drawing.Color.Yellow;
            this.lblUpdate.Location = new System.Drawing.Point(0, 116);
            this.lblUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(764, 71);
            this.lblUpdate.TabIndex = 90;
            this.lblUpdate.Text = "Please wait . We are downloading the updated file";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // frmUpdateMusicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(764, 240);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmUpdateMusicPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alenka-Myclaud player updating";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUpdateMusicPlayer_FormClosing);
            this.Load += new System.EventHandler(this.frmUpdateMusicPlayer_Load);
            this.SizeChanged += new System.EventHandler(this.frmUpdateMusicPlayer_SizeChanged);
            this.panMain.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSanjivani)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.PictureBox picSanjivani;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblUpdate;
    }
}

