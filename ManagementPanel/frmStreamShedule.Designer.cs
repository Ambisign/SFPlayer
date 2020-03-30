namespace ManagementPanel
{
    partial class frmStreamShedule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStreamShedule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefersh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSplPlaylist = new System.Windows.Forms.ComboBox();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.panMenu = new System.Windows.Forms.Panel();
            this.btnMenuSearch = new System.Windows.Forms.Button();
            this.btnMenuAddNew = new System.Windows.Forms.Button();
            this.panAddNew = new System.Windows.Forms.Panel();
            this.dgSpl = new System.Windows.Forms.DataGridView();
            this.panSearch = new System.Windows.Forms.Panel();
            this.dgSearch = new System.Windows.Forms.DataGridView();
            this.panMainNew = new System.Windows.Forms.Panel();
            this.panNew = new System.Windows.Forms.Panel();
            this.btnNewCancel = new System.Windows.Forms.Button();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStreamName = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panMenu.SuspendLayout();
            this.panAddNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpl)).BeginInit();
            this.panSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).BeginInit();
            this.panMainNew.SuspendLayout();
            this.panNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbStreamName);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRefersh);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbSplPlaylist);
            this.panel1.Controls.Add(this.dtpStartTime);
            this.panel1.Controls.Add(this.dtpEndTime);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.cmbFormat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1240, 135);
            this.panel1.TabIndex = 0;
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
            this.btnSave.Location = new System.Drawing.Point(617, 81);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 37);
            this.btnSave.TabIndex = 225;
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
            this.btnRefersh.Location = new System.Drawing.Point(718, 81);
            this.btnRefersh.Name = "btnRefersh";
            this.btnRefersh.Size = new System.Drawing.Size(95, 37);
            this.btnRefersh.TabIndex = 224;
            this.btnRefersh.Text = "Refersh";
            this.btnRefersh.UseVisualStyleBackColor = false;
            this.btnRefersh.Click += new System.EventHandler(this.btnRefersh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(458, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 19);
            this.label7.TabIndex = 223;
            this.label7.Text = "Format";
            // 
            // cmbSplPlaylist
            // 
            this.cmbSplPlaylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplPlaylist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSplPlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSplPlaylist.FormattingEnabled = true;
            this.cmbSplPlaylist.Location = new System.Drawing.Point(139, 49);
            this.cmbSplPlaylist.Name = "cmbSplPlaylist";
            this.cmbSplPlaylist.Size = new System.Drawing.Size(313, 25);
            this.cmbSplPlaylist.TabIndex = 222;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "hh:mm tt";
            this.dtpStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(535, 52);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(103, 23);
            this.dtpStartTime.TabIndex = 217;
            this.dtpStartTime.Value = new System.DateTime(2015, 1, 20, 0, 0, 0, 0);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "hh:mm tt";
            this.dtpEndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(714, 52);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(99, 23);
            this.dtpEndTime.TabIndex = 218;
            this.dtpEndTime.Value = new System.DateTime(2015, 1, 20, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(458, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 19);
            this.label13.TabIndex = 219;
            this.label13.Text = "Start Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(40, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 19);
            this.label2.TabIndex = 220;
            this.label2.Text = "Playlist Name";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(644, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 19);
            this.label17.TabIndex = 221;
            this.label17.Text = "End Time";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(535, 17);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(278, 25);
            this.cmbFormat.TabIndex = 216;
            this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_SelectedIndexChanged);
            this.cmbFormat.Click += new System.EventHandler(this.cmbFormat_Click);
            // 
            // panMenu
            // 
            this.panMenu.BackColor = System.Drawing.SystemColors.Control;
            this.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMenu.Controls.Add(this.btnMenuSearch);
            this.panMenu.Controls.Add(this.btnMenuAddNew);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMenu.Location = new System.Drawing.Point(0, 0);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(1264, 47);
            this.panMenu.TabIndex = 106;
            // 
            // btnMenuSearch
            // 
            this.btnMenuSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnMenuSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnMenuSearch.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnMenuSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnMenuSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMenuSearch.ForeColor = System.Drawing.Color.Black;
            this.btnMenuSearch.Location = new System.Drawing.Point(11, 4);
            this.btnMenuSearch.Name = "btnMenuSearch";
            this.btnMenuSearch.Size = new System.Drawing.Size(172, 37);
            this.btnMenuSearch.TabIndex = 14;
            this.btnMenuSearch.Text = "Search";
            this.btnMenuSearch.UseVisualStyleBackColor = false;
            this.btnMenuSearch.Click += new System.EventHandler(this.btnMenuSearch_Click);
            // 
            // btnMenuAddNew
            // 
            this.btnMenuAddNew.BackColor = System.Drawing.Color.Transparent;
            this.btnMenuAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAddNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnMenuAddNew.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnMenuAddNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnMenuAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAddNew.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMenuAddNew.ForeColor = System.Drawing.Color.Black;
            this.btnMenuAddNew.Location = new System.Drawing.Point(190, 4);
            this.btnMenuAddNew.Name = "btnMenuAddNew";
            this.btnMenuAddNew.Size = new System.Drawing.Size(172, 37);
            this.btnMenuAddNew.TabIndex = 15;
            this.btnMenuAddNew.Text = "Add New";
            this.btnMenuAddNew.UseVisualStyleBackColor = false;
            this.btnMenuAddNew.Click += new System.EventHandler(this.btnMenuAddNew_Click);
            // 
            // panAddNew
            // 
            this.panAddNew.Controls.Add(this.dgSpl);
            this.panAddNew.Controls.Add(this.panel1);
            this.panAddNew.Location = new System.Drawing.Point(12, 60);
            this.panAddNew.Name = "panAddNew";
            this.panAddNew.Size = new System.Drawing.Size(1240, 249);
            this.panAddNew.TabIndex = 107;
            // 
            // dgSpl
            // 
            this.dgSpl.AllowUserToAddRows = false;
            this.dgSpl.AllowUserToDeleteRows = false;
            this.dgSpl.AllowUserToResizeColumns = false;
            this.dgSpl.AllowUserToResizeRows = false;
            this.dgSpl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSpl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgSpl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSpl.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(131)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSpl.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgSpl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSpl.Location = new System.Drawing.Point(0, 135);
            this.dgSpl.Name = "dgSpl";
            this.dgSpl.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(131)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSpl.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgSpl.RowHeadersVisible = false;
            this.dgSpl.RowTemplate.Height = 30;
            this.dgSpl.Size = new System.Drawing.Size(1240, 114);
            this.dgSpl.TabIndex = 106;
            this.dgSpl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSpl_CellClick);
            // 
            // panSearch
            // 
            this.panSearch.Controls.Add(this.dgSearch);
            this.panSearch.Location = new System.Drawing.Point(263, 373);
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(151, 143);
            this.panSearch.TabIndex = 108;
            // 
            // dgSearch
            // 
            this.dgSearch.AllowUserToAddRows = false;
            this.dgSearch.AllowUserToDeleteRows = false;
            this.dgSearch.AllowUserToResizeColumns = false;
            this.dgSearch.AllowUserToResizeRows = false;
            this.dgSearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(131)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSearch.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSearch.Location = new System.Drawing.Point(0, 0);
            this.dgSearch.Name = "dgSearch";
            this.dgSearch.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(131)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgSearch.RowHeadersVisible = false;
            this.dgSearch.RowTemplate.Height = 30;
            this.dgSearch.Size = new System.Drawing.Size(151, 143);
            this.dgSearch.TabIndex = 107;
            this.dgSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellClick);
            // 
            // panMainNew
            // 
            this.panMainNew.Controls.Add(this.panNew);
            this.panMainNew.Location = new System.Drawing.Point(716, 395);
            this.panMainNew.Name = "panMainNew";
            this.panMainNew.Size = new System.Drawing.Size(176, 131);
            this.panMainNew.TabIndex = 109;
            this.panMainNew.Visible = false;
            // 
            // panNew
            // 
            this.panNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panNew.Controls.Add(this.btnNewCancel);
            this.panNew.Controls.Add(this.btnSaveNew);
            this.panNew.Controls.Add(this.label20);
            this.panNew.Controls.Add(this.txtName);
            this.panNew.Controls.Add(this.lblCaption);
            this.panNew.Location = new System.Drawing.Point(179, 204);
            this.panNew.Name = "panNew";
            this.panNew.Size = new System.Drawing.Size(566, 174);
            this.panNew.TabIndex = 14;
            // 
            // btnNewCancel
            // 
            this.btnNewCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnNewCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnNewCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnNewCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnNewCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCancel.ForeColor = System.Drawing.Color.Yellow;
            this.btnNewCancel.Location = new System.Drawing.Point(405, 84);
            this.btnNewCancel.Name = "btnNewCancel";
            this.btnNewCancel.Size = new System.Drawing.Size(95, 38);
            this.btnNewCancel.TabIndex = 106;
            this.btnNewCancel.Text = "Cancel";
            this.btnNewCancel.UseVisualStyleBackColor = false;
            this.btnNewCancel.Click += new System.EventHandler(this.btnNewCancel_Click);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveNew.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSaveNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnSaveNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnSaveNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.ForeColor = System.Drawing.Color.Yellow;
            this.btnSaveNew.Location = new System.Drawing.Point(304, 84);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(95, 38);
            this.btnSaveNew.TabIndex = 105;
            this.btnSaveNew.Text = "Save";
            this.btnSaveNew.UseVisualStyleBackColor = false;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(64, 53);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 20);
            this.label20.TabIndex = 104;
            this.label20.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtName.Location = new System.Drawing.Point(148, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(352, 27);
            this.txtName.TabIndex = 103;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCaption.ForeColor = System.Drawing.Color.Yellow;
            this.lblCaption.Location = new System.Drawing.Point(3, 8);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(100, 20);
            this.lblCaption.TabIndex = 102;
            this.lblCaption.Text = "Stream Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 19);
            this.label1.TabIndex = 227;
            this.label1.Text = "Stream Name";
            // 
            // cmbStreamName
            // 
            this.cmbStreamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStreamName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStreamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStreamName.FormattingEnabled = true;
            this.cmbStreamName.Location = new System.Drawing.Point(139, 17);
            this.cmbStreamName.Name = "cmbStreamName";
            this.cmbStreamName.Size = new System.Drawing.Size(278, 25);
            this.cmbStreamName.TabIndex = 226;
            this.cmbStreamName.SelectedIndexChanged += new System.EventHandler(this.cmbStreamName_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PowderBlue;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.btnNew.Location = new System.Drawing.Point(423, 17);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(29, 25);
            this.btnNew.TabIndex = 228;
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmStreamShedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1264, 624);
            this.Controls.Add(this.panMainNew);
            this.Controls.Add(this.panSearch);
            this.Controls.Add(this.panAddNew);
            this.Controls.Add(this.panMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStreamShedule";
            this.Text = "Stream Scheduling";
            this.Load += new System.EventHandler(this.frmStreamShedule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panMenu.ResumeLayout(false);
            this.panAddNew.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSpl)).EndInit();
            this.panSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).EndInit();
            this.panMainNew.ResumeLayout(false);
            this.panNew.ResumeLayout(false);
            this.panNew.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.ComboBox cmbSplPlaylist;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefersh;
        private System.Windows.Forms.Panel panMenu;
        private System.Windows.Forms.Button btnMenuSearch;
        private System.Windows.Forms.Button btnMenuAddNew;
        private System.Windows.Forms.Panel panAddNew;
        private System.Windows.Forms.Panel panSearch;
        private System.Windows.Forms.DataGridView dgSpl;
        private System.Windows.Forms.DataGridView dgSearch;
        private System.Windows.Forms.Panel panMainNew;
        private System.Windows.Forms.Panel panNew;
        private System.Windows.Forms.Button btnNewCancel;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStreamName;
        private System.Windows.Forms.Button btnNew;
    }
}