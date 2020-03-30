namespace ManagementPanel
{
    partial class frmMoblieAppStreams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoblieAppStreams));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAppRefersh = new System.Windows.Forms.Button();
            this.btnAppSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel32 = new System.Windows.Forms.Panel();
            this.lblAdvtPercentage = new System.Windows.Forms.Label();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.btnDialog = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAppStreamLink = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAppStreamName = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSearchCustomer = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgAppStream = new System.Windows.Forms.DataGridView();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel3.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAppStream)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.btnAppRefersh);
            this.panel3.Controls.Add(this.btnAppSave);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbCustomer);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.panel32);
            this.panel3.Controls.Add(this.btnDialog);
            this.panel3.Controls.Add(this.txtPath);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtAppStreamLink);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtAppStreamName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1264, 165);
            this.panel3.TabIndex = 14;
            // 
            // btnAppRefersh
            // 
            this.btnAppRefersh.BackColor = System.Drawing.Color.Transparent;
            this.btnAppRefersh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAppRefersh.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnAppRefersh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnAppRefersh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnAppRefersh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppRefersh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppRefersh.ForeColor = System.Drawing.Color.Yellow;
            this.btnAppRefersh.Location = new System.Drawing.Point(897, 108);
            this.btnAppRefersh.Name = "btnAppRefersh";
            this.btnAppRefersh.Size = new System.Drawing.Size(95, 38);
            this.btnAppRefersh.TabIndex = 17;
            this.btnAppRefersh.Text = "Refresh";
            this.btnAppRefersh.UseVisualStyleBackColor = false;
            this.btnAppRefersh.Click += new System.EventHandler(this.btnAppRefersh_Click);
            // 
            // btnAppSave
            // 
            this.btnAppSave.BackColor = System.Drawing.Color.Transparent;
            this.btnAppSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAppSave.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnAppSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnAppSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnAppSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppSave.ForeColor = System.Drawing.Color.Yellow;
            this.btnAppSave.Location = new System.Drawing.Point(796, 108);
            this.btnAppSave.Name = "btnAppSave";
            this.btnAppSave.Size = new System.Drawing.Size(95, 38);
            this.btnAppSave.TabIndex = 16;
            this.btnAppSave.Text = "Save";
            this.btnAppSave.UseVisualStyleBackColor = false;
            this.btnAppSave.Click += new System.EventHandler(this.btnAppSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 19);
            this.label1.TabIndex = 224;
            this.label1.Text = "Stream Owner";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(120, 61);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(295, 25);
            this.cmbCustomer.TabIndex = 223;
            this.cmbCustomer.Click += new System.EventHandler(this.cmbCustomer_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label11.ForeColor = System.Drawing.Color.Yellow;
            this.label11.Location = new System.Drawing.Point(3, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 21);
            this.label11.TabIndex = 181;
            this.label11.Text = "Moblie App Streams";
            // 
            // panel32
            // 
            this.panel32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel32.Controls.Add(this.lblAdvtPercentage);
            this.panel32.Controls.Add(this.progressBar3);
            this.panel32.Location = new System.Drawing.Point(508, 114);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(253, 21);
            this.panel32.TabIndex = 180;
            // 
            // lblAdvtPercentage
            // 
            this.lblAdvtPercentage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAdvtPercentage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvtPercentage.ForeColor = System.Drawing.Color.White;
            this.lblAdvtPercentage.Location = new System.Drawing.Point(207, 0);
            this.lblAdvtPercentage.Name = "lblAdvtPercentage";
            this.lblAdvtPercentage.Size = new System.Drawing.Size(44, 19);
            this.lblAdvtPercentage.TabIndex = 0;
            this.lblAdvtPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar3
            // 
            this.progressBar3.Dock = System.Windows.Forms.DockStyle.Left;
            this.progressBar3.ForeColor = System.Drawing.Color.Yellow;
            this.progressBar3.Location = new System.Drawing.Point(0, 0);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(201, 19);
            this.progressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar3.TabIndex = 0;
            // 
            // btnDialog
            // 
            this.btnDialog.BackColor = System.Drawing.Color.Transparent;
            this.btnDialog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDialog.BackgroundImage")));
            this.btnDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDialog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDialog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PowderBlue;
            this.btnDialog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDialog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDialog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.btnDialog.Location = new System.Drawing.Point(963, 61);
            this.btnDialog.Name = "btnDialog";
            this.btnDialog.Size = new System.Drawing.Size(29, 25);
            this.btnDialog.TabIndex = 177;
            this.btnDialog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDialog.UseVisualStyleBackColor = false;
            this.btnDialog.Click += new System.EventHandler(this.btnDialog_Click);
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(508, 61);
            this.txtPath.MaxLength = 3;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(449, 23);
            this.txtPath.TabIndex = 176;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(421, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 136;
            this.label8.Text = "Stream Link";
            // 
            // txtAppStreamLink
            // 
            this.txtAppStreamLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppStreamLink.Location = new System.Drawing.Point(508, 33);
            this.txtAppStreamLink.Name = "txtAppStreamLink";
            this.txtAppStreamLink.Size = new System.Drawing.Size(484, 23);
            this.txtAppStreamLink.TabIndex = 135;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.ForeColor = System.Drawing.Color.Yellow;
            this.label10.Location = new System.Drawing.Point(505, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(358, 15);
            this.label10.TabIndex = 134;
            this.label10.Text = "Image Limit: Image should be 122x122 and maximum 35kb size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(423, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 19);
            this.label5.TabIndex = 134;
            this.label5.Text = "Image Path";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(6, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 19);
            this.label9.TabIndex = 134;
            this.label9.Text = "Stream Name";
            // 
            // txtAppStreamName
            // 
            this.txtAppStreamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppStreamName.Location = new System.Drawing.Point(120, 33);
            this.txtAppStreamName.Name = "txtAppStreamName";
            this.txtAppStreamName.Size = new System.Drawing.Size(295, 23);
            this.txtAppStreamName.TabIndex = 132;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.cmbSearchCustomer);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 165);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1264, 62);
            this.panel5.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(5, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 19);
            this.label6.TabIndex = 227;
            this.label6.Text = "Search";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(5, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 19);
            this.label4.TabIndex = 226;
            this.label4.Text = "Stream Owner";
            // 
            // cmbSearchCustomer
            // 
            this.cmbSearchCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSearchCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchCustomer.FormattingEnabled = true;
            this.cmbSearchCustomer.Location = new System.Drawing.Point(120, 27);
            this.cmbSearchCustomer.Name = "cmbSearchCustomer";
            this.cmbSearchCustomer.Size = new System.Drawing.Size(452, 25);
            this.cmbSearchCustomer.TabIndex = 225;
            this.cmbSearchCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbSearchCustomer_SelectedIndexChanged);
            this.cmbSearchCustomer.Click += new System.EventHandler(this.cmbSearchCustomer_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.dgAppStream);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 227);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1264, 259);
            this.panel6.TabIndex = 17;
            // 
            // dgAppStream
            // 
            this.dgAppStream.AllowUserToAddRows = false;
            this.dgAppStream.AllowUserToDeleteRows = false;
            this.dgAppStream.AllowUserToResizeColumns = false;
            this.dgAppStream.AllowUserToResizeRows = false;
            this.dgAppStream.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAppStream.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAppStream.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAppStream.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAppStream.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgAppStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAppStream.GridColor = System.Drawing.Color.Gainsboro;
            this.dgAppStream.Location = new System.Drawing.Point(0, 0);
            this.dgAppStream.Name = "dgAppStream";
            this.dgAppStream.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAppStream.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgAppStream.RowHeadersVisible = false;
            this.dgAppStream.RowTemplate.Height = 40;
            this.dgAppStream.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAppStream.Size = new System.Drawing.Size(1260, 255);
            this.dgAppStream.TabIndex = 3;
            this.dgAppStream.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAppStream_CellClick);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // frmMoblieAppStreams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1264, 486);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMoblieAppStreams";
            this.Text = "frmMoblieAppStreams";
            this.Load += new System.EventHandler(this.frmMoblieAppStreams_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel32.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAppStream)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.Label lblAdvtPercentage;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Button btnDialog;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAppStreamLink;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAppStreamName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAppRefersh;
        private System.Windows.Forms.Button btnAppSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgAppStream;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSearchCustomer;
    }
}