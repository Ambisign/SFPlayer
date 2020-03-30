namespace ManagementPanel
{
    partial class frmDealerContent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDealerContent));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoVideoType = new System.Windows.Forms.RadioButton();
            this.rdoRoyalty = new System.Windows.Forms.RadioButton();
            this.rdoCopyright = new System.Windows.Forms.RadioButton();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgPlaylist = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgCommanGrid = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdoPlaylist = new System.Windows.Forms.RadioButton();
            this.rdoCategory = new System.Windows.Forms.RadioButton();
            this.rdoTempo = new System.Windows.Forms.RadioButton();
            this.rdoGenre = new System.Windows.Forms.RadioButton();
            this.cmbAlbum = new System.Windows.Forms.ComboBox();
            this.rdoAlbum = new System.Windows.Forms.RadioButton();
            this.rdoArtist = new System.Windows.Forms.RadioButton();
            this.rdoTitle = new System.Windows.Forms.RadioButton();
            this.picAddtoPlaylist = new System.Windows.Forms.PictureBox();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.rdoVideo = new System.Windows.Forms.RadioButton();
            this.rdoAudio = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.picDeleteSongs = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaylist)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommanGrid)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAddtoPlaylist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteSongs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rdoVideoType);
            this.panel1.Controls.Add(this.rdoRoyalty);
            this.panel1.Controls.Add(this.rdoCopyright);
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 52);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(455, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 19);
            this.label2.TabIndex = 235;
            this.label2.Text = "Content Type";
            // 
            // rdoVideoType
            // 
            this.rdoVideoType.AutoSize = true;
            this.rdoVideoType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoVideoType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoVideoType.ForeColor = System.Drawing.Color.Yellow;
            this.rdoVideoType.Location = new System.Drawing.Point(752, 13);
            this.rdoVideoType.Name = "rdoVideoType";
            this.rdoVideoType.Size = new System.Drawing.Size(62, 23);
            this.rdoVideoType.TabIndex = 234;
            this.rdoVideoType.Text = "Video";
            this.rdoVideoType.UseVisualStyleBackColor = true;
            this.rdoVideoType.CheckedChanged += new System.EventHandler(this.rdoVideoType_CheckedChanged);
            // 
            // rdoRoyalty
            // 
            this.rdoRoyalty.AutoSize = true;
            this.rdoRoyalty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoRoyalty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoRoyalty.ForeColor = System.Drawing.Color.Yellow;
            this.rdoRoyalty.Location = new System.Drawing.Point(644, 12);
            this.rdoRoyalty.Name = "rdoRoyalty";
            this.rdoRoyalty.Size = new System.Drawing.Size(102, 23);
            this.rdoRoyalty.TabIndex = 234;
            this.rdoRoyalty.Text = "Royalty Free";
            this.rdoRoyalty.UseVisualStyleBackColor = true;
            this.rdoRoyalty.CheckedChanged += new System.EventHandler(this.rdoRoyalty_CheckedChanged);
            // 
            // rdoCopyright
            // 
            this.rdoCopyright.AutoSize = true;
            this.rdoCopyright.Checked = true;
            this.rdoCopyright.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoCopyright.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoCopyright.ForeColor = System.Drawing.Color.Yellow;
            this.rdoCopyright.Location = new System.Drawing.Point(550, 12);
            this.rdoCopyright.Name = "rdoCopyright";
            this.rdoCopyright.Size = new System.Drawing.Size(88, 23);
            this.rdoCopyright.TabIndex = 233;
            this.rdoCopyright.TabStop = true;
            this.rdoCopyright.Text = "Copyright";
            this.rdoCopyright.UseVisualStyleBackColor = true;
            this.rdoCopyright.CheckedChanged += new System.EventHandler(this.rdoCopyright_CheckedChanged);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(125, 10);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(301, 25);
            this.cmbCustomer.TabIndex = 224;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.Click += new System.EventHandler(this.cmbCustomer_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(10, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 19);
            this.label7.TabIndex = 223;
            this.label7.Text = "Customer Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dgPlaylist);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1084, 221);
            this.panel2.TabIndex = 1;
            // 
            // dgPlaylist
            // 
            this.dgPlaylist.AllowUserToAddRows = false;
            this.dgPlaylist.AllowUserToDeleteRows = false;
            this.dgPlaylist.AllowUserToResizeColumns = false;
            this.dgPlaylist.AllowUserToResizeRows = false;
            this.dgPlaylist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.dgPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPlaylist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dgPlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPlaylist.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPlaylist.DefaultCellStyle = dataGridViewCellStyle32;
            this.dgPlaylist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPlaylist.Location = new System.Drawing.Point(0, 0);
            this.dgPlaylist.Name = "dgPlaylist";
            this.dgPlaylist.ReadOnly = true;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPlaylist.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.dgPlaylist.RowHeadersVisible = false;
            this.dgPlaylist.RowTemplate.Height = 30;
            this.dgPlaylist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPlaylist.Size = new System.Drawing.Size(1080, 217);
            this.dgPlaylist.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dgCommanGrid);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 273);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1084, 238);
            this.panel3.TabIndex = 2;
            // 
            // dgCommanGrid
            // 
            this.dgCommanGrid.AllowUserToAddRows = false;
            this.dgCommanGrid.AllowUserToDeleteRows = false;
            this.dgCommanGrid.AllowUserToResizeColumns = false;
            this.dgCommanGrid.AllowUserToResizeRows = false;
            this.dgCommanGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.dgCommanGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCommanGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.dgCommanGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCommanGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCommanGrid.DefaultCellStyle = dataGridViewCellStyle35;
            this.dgCommanGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCommanGrid.GridColor = System.Drawing.Color.Gainsboro;
            this.dgCommanGrid.Location = new System.Drawing.Point(0, 46);
            this.dgCommanGrid.Name = "dgCommanGrid";
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCommanGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.dgCommanGrid.RowHeadersVisible = false;
            this.dgCommanGrid.RowTemplate.Height = 30;
            this.dgCommanGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCommanGrid.Size = new System.Drawing.Size(1080, 188);
            this.dgCommanGrid.TabIndex = 105;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.rdoPlaylist);
            this.panel4.Controls.Add(this.rdoCategory);
            this.panel4.Controls.Add(this.rdoTempo);
            this.panel4.Controls.Add(this.rdoGenre);
            this.panel4.Controls.Add(this.cmbAlbum);
            this.panel4.Controls.Add(this.rdoAlbum);
            this.panel4.Controls.Add(this.rdoArtist);
            this.panel4.Controls.Add(this.rdoTitle);
            this.panel4.Controls.Add(this.picAddtoPlaylist);
            this.panel4.Controls.Add(this.picSearch);
            this.panel4.Controls.Add(this.txtSearch);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1080, 46);
            this.panel4.TabIndex = 0;
            // 
            // rdoPlaylist
            // 
            this.rdoPlaylist.AutoSize = true;
            this.rdoPlaylist.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoPlaylist.ForeColor = System.Drawing.Color.Yellow;
            this.rdoPlaylist.Location = new System.Drawing.Point(823, 10);
            this.rdoPlaylist.Name = "rdoPlaylist";
            this.rdoPlaylist.Size = new System.Drawing.Size(69, 23);
            this.rdoPlaylist.TabIndex = 52;
            this.rdoPlaylist.Text = "Playlist";
            this.rdoPlaylist.UseVisualStyleBackColor = true;
            this.rdoPlaylist.CheckedChanged += new System.EventHandler(this.rdoPlaylist_CheckedChanged);
            // 
            // rdoCategory
            // 
            this.rdoCategory.AutoSize = true;
            this.rdoCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoCategory.ForeColor = System.Drawing.Color.Yellow;
            this.rdoCategory.Location = new System.Drawing.Point(738, 10);
            this.rdoCategory.Name = "rdoCategory";
            this.rdoCategory.Size = new System.Drawing.Size(83, 23);
            this.rdoCategory.TabIndex = 51;
            this.rdoCategory.Text = "Category";
            this.rdoCategory.UseVisualStyleBackColor = true;
            this.rdoCategory.CheckedChanged += new System.EventHandler(this.rdoCategory_CheckedChanged);
            // 
            // rdoTempo
            // 
            this.rdoTempo.AutoSize = true;
            this.rdoTempo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoTempo.ForeColor = System.Drawing.Color.Yellow;
            this.rdoTempo.Location = new System.Drawing.Point(663, 10);
            this.rdoTempo.Name = "rdoTempo";
            this.rdoTempo.Size = new System.Drawing.Size(68, 23);
            this.rdoTempo.TabIndex = 50;
            this.rdoTempo.Text = "Tempo";
            this.rdoTempo.UseVisualStyleBackColor = true;
            this.rdoTempo.CheckedChanged += new System.EventHandler(this.rdoTempo_CheckedChanged);
            // 
            // rdoGenre
            // 
            this.rdoGenre.AutoSize = true;
            this.rdoGenre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoGenre.ForeColor = System.Drawing.Color.Yellow;
            this.rdoGenre.Location = new System.Drawing.Point(593, 10);
            this.rdoGenre.Name = "rdoGenre";
            this.rdoGenre.Size = new System.Drawing.Size(64, 23);
            this.rdoGenre.TabIndex = 49;
            this.rdoGenre.Text = "Genre";
            this.rdoGenre.UseVisualStyleBackColor = true;
            this.rdoGenre.CheckedChanged += new System.EventHandler(this.rdoGenre_CheckedChanged);
            // 
            // cmbAlbum
            // 
            this.cmbAlbum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlbum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlbum.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlbum.FormattingEnabled = true;
            this.cmbAlbum.Location = new System.Drawing.Point(137, 9);
            this.cmbAlbum.Name = "cmbAlbum";
            this.cmbAlbum.Size = new System.Drawing.Size(218, 27);
            this.cmbAlbum.TabIndex = 48;
            this.cmbAlbum.Visible = false;
            this.cmbAlbum.SelectedIndexChanged += new System.EventHandler(this.cmbAlbum_SelectedIndexChanged);
            // 
            // rdoAlbum
            // 
            this.rdoAlbum.AutoSize = true;
            this.rdoAlbum.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoAlbum.ForeColor = System.Drawing.Color.Yellow;
            this.rdoAlbum.Location = new System.Drawing.Point(520, 10);
            this.rdoAlbum.Name = "rdoAlbum";
            this.rdoAlbum.Size = new System.Drawing.Size(67, 23);
            this.rdoAlbum.TabIndex = 47;
            this.rdoAlbum.Text = "Album";
            this.rdoAlbum.UseVisualStyleBackColor = true;
            this.rdoAlbum.CheckedChanged += new System.EventHandler(this.rdoAlbum_CheckedChanged);
            // 
            // rdoArtist
            // 
            this.rdoArtist.AutoSize = true;
            this.rdoArtist.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoArtist.ForeColor = System.Drawing.Color.Yellow;
            this.rdoArtist.Location = new System.Drawing.Point(455, 10);
            this.rdoArtist.Name = "rdoArtist";
            this.rdoArtist.Size = new System.Drawing.Size(60, 23);
            this.rdoArtist.TabIndex = 46;
            this.rdoArtist.Text = "Artist";
            this.rdoArtist.UseVisualStyleBackColor = true;
            this.rdoArtist.CheckedChanged += new System.EventHandler(this.rdoArtist_CheckedChanged);
            // 
            // rdoTitle
            // 
            this.rdoTitle.AutoSize = true;
            this.rdoTitle.Checked = true;
            this.rdoTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoTitle.ForeColor = System.Drawing.Color.Yellow;
            this.rdoTitle.Location = new System.Drawing.Point(397, 10);
            this.rdoTitle.Name = "rdoTitle";
            this.rdoTitle.Size = new System.Drawing.Size(52, 23);
            this.rdoTitle.TabIndex = 45;
            this.rdoTitle.TabStop = true;
            this.rdoTitle.Text = "Title";
            this.rdoTitle.UseVisualStyleBackColor = true;
            this.rdoTitle.CheckedChanged += new System.EventHandler(this.rdoTitle_CheckedChanged);
            // 
            // picAddtoPlaylist
            // 
            this.picAddtoPlaylist.BackgroundImage = global::ManagementPanel.Properties.Resources.upload;
            this.picAddtoPlaylist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picAddtoPlaylist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAddtoPlaylist.Location = new System.Drawing.Point(897, 7);
            this.picAddtoPlaylist.Name = "picAddtoPlaylist";
            this.picAddtoPlaylist.Size = new System.Drawing.Size(42, 29);
            this.picAddtoPlaylist.TabIndex = 43;
            this.picAddtoPlaylist.TabStop = false;
            this.picAddtoPlaylist.Click += new System.EventHandler(this.picAddtoPlaylist_Click);
            // 
            // picSearch
            // 
            this.picSearch.BackColor = System.Drawing.Color.Transparent;
            this.picSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSearch.Image = ((System.Drawing.Image)(resources.GetObject("picSearch.Image")));
            this.picSearch.Location = new System.Drawing.Point(359, 8);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(34, 28);
            this.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearch.TabIndex = 44;
            this.picSearch.TabStop = false;
            this.picSearch.Click += new System.EventHandler(this.picSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.AutoCompleteCustomSource.AddRange(new string[] {
            "gghg"});
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(137, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(218, 25);
            this.txtSearch.TabIndex = 42;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.rdoVideo);
            this.panel9.Controls.Add(this.rdoAudio);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(134, 42);
            this.panel9.TabIndex = 40;
            // 
            // rdoVideo
            // 
            this.rdoVideo.AutoSize = true;
            this.rdoVideo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoVideo.ForeColor = System.Drawing.Color.Yellow;
            this.rdoVideo.Location = new System.Drawing.Point(71, 9);
            this.rdoVideo.Name = "rdoVideo";
            this.rdoVideo.Size = new System.Drawing.Size(62, 23);
            this.rdoVideo.TabIndex = 35;
            this.rdoVideo.Text = "Video";
            this.rdoVideo.UseVisualStyleBackColor = true;
            // 
            // rdoAudio
            // 
            this.rdoAudio.AutoSize = true;
            this.rdoAudio.Checked = true;
            this.rdoAudio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoAudio.ForeColor = System.Drawing.Color.Yellow;
            this.rdoAudio.Location = new System.Drawing.Point(4, 9);
            this.rdoAudio.Name = "rdoAudio";
            this.rdoAudio.Size = new System.Drawing.Size(63, 23);
            this.rdoAudio.TabIndex = 34;
            this.rdoAudio.TabStop = true;
            this.rdoAudio.Text = "Audio";
            this.rdoAudio.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.picDeleteSongs);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(1033, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(47, 48);
            this.panel8.TabIndex = 236;
            // 
            // picDeleteSongs
            // 
            this.picDeleteSongs.BackgroundImage = global::ManagementPanel.Properties.Resources._256;
            this.picDeleteSongs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picDeleteSongs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDeleteSongs.Location = new System.Drawing.Point(3, 14);
            this.picDeleteSongs.Name = "picDeleteSongs";
            this.picDeleteSongs.Size = new System.Drawing.Size(35, 30);
            this.picDeleteSongs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDeleteSongs.TabIndex = 228;
            this.picDeleteSongs.TabStop = false;
            this.picDeleteSongs.Click += new System.EventHandler(this.picDeleteSongs_Click);
            // 
            // frmDealerContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1084, 511);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDealerContent";
            this.Text = "Dealer Content";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaylist)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCommanGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAddtoPlaylist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteSongs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoVideoType;
        private System.Windows.Forms.RadioButton rdoRoyalty;
        private System.Windows.Forms.RadioButton rdoCopyright;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgCommanGrid;
        private System.Windows.Forms.DataGridView dgPlaylist;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton rdoVideo;
        private System.Windows.Forms.RadioButton rdoAudio;
        private System.Windows.Forms.RadioButton rdoPlaylist;
        private System.Windows.Forms.RadioButton rdoCategory;
        private System.Windows.Forms.RadioButton rdoTempo;
        private System.Windows.Forms.RadioButton rdoGenre;
        private System.Windows.Forms.ComboBox cmbAlbum;
        private System.Windows.Forms.RadioButton rdoAlbum;
        private System.Windows.Forms.RadioButton rdoArtist;
        private System.Windows.Forms.RadioButton rdoTitle;
        private System.Windows.Forms.PictureBox picAddtoPlaylist;
        private System.Windows.Forms.PictureBox picSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.PictureBox picDeleteSongs;
    }
}