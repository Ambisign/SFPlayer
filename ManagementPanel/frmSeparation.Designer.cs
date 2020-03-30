namespace ManagementPanel
{
    partial class frmSeparation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoGenre = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefersh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSpType = new System.Windows.Forms.ComboBox();
            this.cmbSpTime = new System.Windows.Forms.ComboBox();
            this.rdoYear = new System.Windows.Forms.RadioButton();
            this.rdoAlbum = new System.Windows.Forms.RadioButton();
            this.rdoArtist = new System.Windows.Forms.RadioButton();
            this.rdoTitle = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPlaylist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgSp = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSp)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.rdoGenre);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRefersh);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbSpType);
            this.panel1.Controls.Add(this.cmbSpTime);
            this.panel1.Controls.Add(this.rdoYear);
            this.panel1.Controls.Add(this.rdoAlbum);
            this.panel1.Controls.Add(this.rdoArtist);
            this.panel1.Controls.Add(this.rdoTitle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbPlaylist);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbFormat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1197, 135);
            this.panel1.TabIndex = 0;
            // 
            // rdoGenre
            // 
            this.rdoGenre.AutoSize = true;
            this.rdoGenre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoGenre.ForeColor = System.Drawing.Color.White;
            this.rdoGenre.Location = new System.Drawing.Point(394, 57);
            this.rdoGenre.Name = "rdoGenre";
            this.rdoGenre.Size = new System.Drawing.Size(64, 23);
            this.rdoGenre.TabIndex = 238;
            this.rdoGenre.TabStop = true;
            this.rdoGenre.Text = "Genre";
            this.rdoGenre.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Yellow;
            this.btnSave.Location = new System.Drawing.Point(739, 89);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 34);
            this.btnSave.TabIndex = 237;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefersh
            // 
            this.btnRefersh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefersh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefersh.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnRefersh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnRefersh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnRefersh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefersh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefersh.ForeColor = System.Drawing.Color.Yellow;
            this.btnRefersh.Location = new System.Drawing.Point(818, 89);
            this.btnRefersh.Name = "btnRefersh";
            this.btnRefersh.Size = new System.Drawing.Size(73, 34);
            this.btnRefersh.TabIndex = 236;
            this.btnRefersh.Text = "Refresh";
            this.btnRefersh.UseVisualStyleBackColor = false;
            this.btnRefersh.Click += new System.EventHandler(this.btnRefersh_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(471, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 19);
            this.label4.TabIndex = 235;
            this.label4.Text = "Separation Time";
            // 
            // cmbSpType
            // 
            this.cmbSpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSpType.FormattingEnabled = true;
            this.cmbSpType.Items.AddRange(new object[] {
            "",
            "min",
            "hour",
            "days"});
            this.cmbSpType.Location = new System.Drawing.Point(739, 58);
            this.cmbSpType.Name = "cmbSpType";
            this.cmbSpType.Size = new System.Drawing.Size(152, 25);
            this.cmbSpType.TabIndex = 234;
            // 
            // cmbSpTime
            // 
            this.cmbSpTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSpTime.FormattingEnabled = true;
            this.cmbSpTime.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100"});
            this.cmbSpTime.Location = new System.Drawing.Point(582, 58);
            this.cmbSpTime.Name = "cmbSpTime";
            this.cmbSpTime.Size = new System.Drawing.Size(151, 25);
            this.cmbSpTime.TabIndex = 233;
            // 
            // rdoYear
            // 
            this.rdoYear.AutoSize = true;
            this.rdoYear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoYear.ForeColor = System.Drawing.Color.White;
            this.rdoYear.Location = new System.Drawing.Point(335, 57);
            this.rdoYear.Name = "rdoYear";
            this.rdoYear.Size = new System.Drawing.Size(53, 23);
            this.rdoYear.TabIndex = 230;
            this.rdoYear.TabStop = true;
            this.rdoYear.Text = "Year";
            this.rdoYear.UseVisualStyleBackColor = true;
            // 
            // rdoAlbum
            // 
            this.rdoAlbum.AutoSize = true;
            this.rdoAlbum.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAlbum.ForeColor = System.Drawing.Color.White;
            this.rdoAlbum.Location = new System.Drawing.Point(262, 57);
            this.rdoAlbum.Name = "rdoAlbum";
            this.rdoAlbum.Size = new System.Drawing.Size(67, 23);
            this.rdoAlbum.TabIndex = 229;
            this.rdoAlbum.TabStop = true;
            this.rdoAlbum.Text = "Album";
            this.rdoAlbum.UseVisualStyleBackColor = true;
            // 
            // rdoArtist
            // 
            this.rdoArtist.AutoSize = true;
            this.rdoArtist.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoArtist.ForeColor = System.Drawing.Color.White;
            this.rdoArtist.Location = new System.Drawing.Point(196, 57);
            this.rdoArtist.Name = "rdoArtist";
            this.rdoArtist.Size = new System.Drawing.Size(60, 23);
            this.rdoArtist.TabIndex = 228;
            this.rdoArtist.TabStop = true;
            this.rdoArtist.Text = "Artist";
            this.rdoArtist.UseVisualStyleBackColor = true;
            // 
            // rdoTitle
            // 
            this.rdoTitle.AutoSize = true;
            this.rdoTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoTitle.ForeColor = System.Drawing.Color.White;
            this.rdoTitle.Location = new System.Drawing.Point(138, 57);
            this.rdoTitle.Name = "rdoTitle";
            this.rdoTitle.Size = new System.Drawing.Size(52, 23);
            this.rdoTitle.TabIndex = 227;
            this.rdoTitle.TabStop = true;
            this.rdoTitle.Text = "Title";
            this.rdoTitle.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 19);
            this.label2.TabIndex = 226;
            this.label2.Text = "Separation Type";
            // 
            // cmbPlaylist
            // 
            this.cmbPlaylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaylist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlaylist.FormattingEnabled = true;
            this.cmbPlaylist.Location = new System.Drawing.Point(582, 25);
            this.cmbPlaylist.Name = "cmbPlaylist";
            this.cmbPlaylist.Size = new System.Drawing.Size(309, 25);
            this.cmbPlaylist.TabIndex = 225;
            this.cmbPlaylist.SelectedIndexChanged += new System.EventHandler(this.cmbPlaylist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(471, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 224;
            this.label1.Text = "Playlist Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(10, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 19);
            this.label9.TabIndex = 221;
            this.label9.Text = "Format";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(138, 25);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(320, 25);
            this.cmbFormat.TabIndex = 220;
            this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dgSp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1197, 378);
            this.panel2.TabIndex = 1;
            // 
            // dgSp
            // 
            this.dgSp.AllowUserToAddRows = false;
            this.dgSp.AllowUserToDeleteRows = false;
            this.dgSp.AllowUserToResizeColumns = false;
            this.dgSp.AllowUserToResizeRows = false;
            this.dgSp.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSp.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSp.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgSp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSp.GridColor = System.Drawing.Color.Gainsboro;
            this.dgSp.Location = new System.Drawing.Point(0, 0);
            this.dgSp.Name = "dgSp";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSp.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgSp.RowHeadersVisible = false;
            this.dgSp.RowTemplate.Height = 30;
            this.dgSp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSp.Size = new System.Drawing.Size(1193, 374);
            this.dgSp.TabIndex = 8;
            this.dgSp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSp_CellClick);
            // 
            // frmSeparation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1197, 513);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSeparation";
            this.Text = "Separation";
            this.Load += new System.EventHandler(this.frmSeparation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPlaylist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoYear;
        private System.Windows.Forms.RadioButton rdoAlbum;
        private System.Windows.Forms.RadioButton rdoArtist;
        private System.Windows.Forms.RadioButton rdoTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSpType;
        private System.Windows.Forms.ComboBox cmbSpTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefersh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgSp;
        private System.Windows.Forms.RadioButton rdoGenre;
    }
}