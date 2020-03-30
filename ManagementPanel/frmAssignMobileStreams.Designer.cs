namespace ManagementPanel
{
    partial class frmAssignMobileStreams
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssignMobileStreams));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefersh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.dgToken = new System.Windows.Forms.DataGridView();
            this.dgStream = new System.Windows.Forms.DataGridView();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panPopUp = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.panMenu = new System.Windows.Forms.Panel();
            this.btnMenuSearch = new System.Windows.Forms.Button();
            this.btnMenuAddNew = new System.Windows.Forms.Button();
            this.panAddNew = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbStreamOwner = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panSearch = new System.Windows.Forms.Panel();
            this.dgStreamUpdate = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbTokenUpdate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomerUpdate = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStream)).BeginInit();
            this.panPopUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panMenu.SuspendLayout();
            this.panAddNew.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStreamUpdate)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefersh);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 60);
            this.panel1.TabIndex = 0;
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
            this.btnRefersh.Location = new System.Drawing.Point(622, 16);
            this.btnRefersh.Name = "btnRefersh";
            this.btnRefersh.Size = new System.Drawing.Size(95, 38);
            this.btnRefersh.TabIndex = 224;
            this.btnRefersh.Text = "Refersh";
            this.btnRefersh.UseVisualStyleBackColor = false;
            this.btnRefersh.Click += new System.EventHandler(this.btnRefersh_Click);
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
            this.btnSave.Location = new System.Drawing.Point(521, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 38);
            this.btnSave.TabIndex = 223;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 19);
            this.label4.TabIndex = 220;
            this.label4.Text = "Customer Name";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(128, 25);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(364, 25);
            this.cmbCustomer.TabIndex = 219;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.Click += new System.EventHandler(this.cmbCustomer_Click);
            // 
            // dgToken
            // 
            this.dgToken.AllowUserToAddRows = false;
            this.dgToken.AllowUserToDeleteRows = false;
            this.dgToken.AllowUserToResizeColumns = false;
            this.dgToken.AllowUserToResizeRows = false;
            this.dgToken.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgToken.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgToken.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgToken.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgToken.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgToken.GridColor = System.Drawing.Color.Gainsboro;
            this.dgToken.Location = new System.Drawing.Point(3, 3);
            this.dgToken.Name = "dgToken";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgToken.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgToken.RowHeadersVisible = false;
            this.dgToken.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgToken.Size = new System.Drawing.Size(697, 299);
            this.dgToken.TabIndex = 7;
            this.dgToken.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgToken_CellPainting);
            this.dgToken.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgToken_CellValueChanged);
            this.dgToken.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgToken_CurrentCellDirtyStateChanged);
            // 
            // dgStream
            // 
            this.dgStream.AllowUserToAddRows = false;
            this.dgStream.AllowUserToDeleteRows = false;
            this.dgStream.AllowUserToResizeColumns = false;
            this.dgStream.AllowUserToResizeRows = false;
            this.dgStream.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.dgStream.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStream.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgStream.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStream.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStream.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStream.GridColor = System.Drawing.Color.Gainsboro;
            this.dgStream.Location = new System.Drawing.Point(0, 39);
            this.dgStream.Name = "dgStream";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStream.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgStream.RowHeadersVisible = false;
            this.dgStream.RowTemplate.Height = 30;
            this.dgStream.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStream.Size = new System.Drawing.Size(464, 260);
            this.dgStream.TabIndex = 8;
            this.dgStream.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgStream_CellPainting);
            this.dgStream.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStream_CellValueChanged);
            this.dgStream.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgStream_CurrentCellDirtyStateChanged);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // panPopUp
            // 
            this.panPopUp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panPopUp.Controls.Add(this.pictureBox1);
            this.panPopUp.Controls.Add(this.lblPercentage);
            this.panPopUp.Location = new System.Drawing.Point(853, 206);
            this.panPopUp.Name = "panPopUp";
            this.panPopUp.Size = new System.Drawing.Size(439, 205);
            this.panPopUp.TabIndex = 115;
            this.panPopUp.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(170, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 96);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // lblPercentage
            // 
            this.lblPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPercentage.ForeColor = System.Drawing.Color.White;
            this.lblPercentage.Location = new System.Drawing.Point(304, 178);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(126, 19);
            this.lblPercentage.TabIndex = 16;
            this.lblPercentage.Text = "Data Importing";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.panMenu.Size = new System.Drawing.Size(1184, 46);
            this.panMenu.TabIndex = 117;
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
            this.btnMenuSearch.Location = new System.Drawing.Point(11, 3);
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
            this.btnMenuAddNew.Location = new System.Drawing.Point(190, 3);
            this.btnMenuAddNew.Name = "btnMenuAddNew";
            this.btnMenuAddNew.Size = new System.Drawing.Size(172, 37);
            this.btnMenuAddNew.TabIndex = 15;
            this.btnMenuAddNew.Text = "Add New";
            this.btnMenuAddNew.UseVisualStyleBackColor = false;
            this.btnMenuAddNew.Click += new System.EventHandler(this.btnMenuAddNew_Click);
            // 
            // panAddNew
            // 
            this.panAddNew.Controls.Add(this.tableLayoutPanel1);
            this.panAddNew.Controls.Add(this.panel1);
            this.panAddNew.Location = new System.Drawing.Point(3, 52);
            this.panAddNew.Name = "panAddNew";
            this.panAddNew.Size = new System.Drawing.Size(1173, 365);
            this.panAddNew.TabIndex = 118;
            this.panAddNew.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.dgToken, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1173, 305);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgStream);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(706, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(464, 299);
            this.panel3.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbStreamOwner);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(464, 39);
            this.panel4.TabIndex = 0;
            // 
            // cmbStreamOwner
            // 
            this.cmbStreamOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStreamOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStreamOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStreamOwner.FormattingEnabled = true;
            this.cmbStreamOwner.Location = new System.Drawing.Point(144, 5);
            this.cmbStreamOwner.Name = "cmbStreamOwner";
            this.cmbStreamOwner.Size = new System.Drawing.Size(318, 25);
            this.cmbStreamOwner.TabIndex = 222;
            this.cmbStreamOwner.SelectedIndexChanged += new System.EventHandler(this.cmbStreamOwner_SelectedIndexChanged);
            this.cmbStreamOwner.Click += new System.EventHandler(this.cmbStreamOwner_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 19);
            this.label3.TabIndex = 223;
            this.label3.Text = "Stream Owner Name";
            // 
            // panSearch
            // 
            this.panSearch.Controls.Add(this.dgStreamUpdate);
            this.panSearch.Controls.Add(this.panel2);
            this.panSearch.Location = new System.Drawing.Point(33, 236);
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(784, 328);
            this.panSearch.TabIndex = 119;
            this.panSearch.Visible = false;
            // 
            // dgStreamUpdate
            // 
            this.dgStreamUpdate.AllowUserToAddRows = false;
            this.dgStreamUpdate.AllowUserToDeleteRows = false;
            this.dgStreamUpdate.AllowUserToResizeColumns = false;
            this.dgStreamUpdate.AllowUserToResizeRows = false;
            this.dgStreamUpdate.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStreamUpdate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgStreamUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStreamUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStreamUpdate.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgStreamUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStreamUpdate.GridColor = System.Drawing.Color.Gainsboro;
            this.dgStreamUpdate.Location = new System.Drawing.Point(0, 72);
            this.dgStreamUpdate.Name = "dgStreamUpdate";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStreamUpdate.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgStreamUpdate.RowHeadersVisible = false;
            this.dgStreamUpdate.RowTemplate.Height = 30;
            this.dgStreamUpdate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStreamUpdate.Size = new System.Drawing.Size(784, 256);
            this.dgStreamUpdate.TabIndex = 10;
            this.dgStreamUpdate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStreamUpdate_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbTokenUpdate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbCustomerUpdate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 72);
            this.panel2.TabIndex = 2;
            // 
            // cmbTokenUpdate
            // 
            this.cmbTokenUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTokenUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTokenUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTokenUpdate.FormattingEnabled = true;
            this.cmbTokenUpdate.Items.AddRange(new object[] {
            "",
            "Copyright",
            "Direct License"});
            this.cmbTokenUpdate.Location = new System.Drawing.Point(582, 25);
            this.cmbTokenUpdate.Name = "cmbTokenUpdate";
            this.cmbTokenUpdate.Size = new System.Drawing.Size(588, 25);
            this.cmbTokenUpdate.TabIndex = 226;
            this.cmbTokenUpdate.SelectedIndexChanged += new System.EventHandler(this.cmbTokenUpdate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(497, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 225;
            this.label1.Text = "Token Detail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 19);
            this.label2.TabIndex = 220;
            this.label2.Text = "Customer Name";
            // 
            // cmbCustomerUpdate
            // 
            this.cmbCustomerUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCustomerUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomerUpdate.FormattingEnabled = true;
            this.cmbCustomerUpdate.Location = new System.Drawing.Point(128, 25);
            this.cmbCustomerUpdate.Name = "cmbCustomerUpdate";
            this.cmbCustomerUpdate.Size = new System.Drawing.Size(364, 25);
            this.cmbCustomerUpdate.TabIndex = 219;
            this.cmbCustomerUpdate.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerUpdate_SelectedIndexChanged);
            this.cmbCustomerUpdate.Click += new System.EventHandler(this.cmbCustomerUpdate_Click);
            // 
            // frmAssignMobileStreams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.panPopUp);
            this.Controls.Add(this.panSearch);
            this.Controls.Add(this.panAddNew);
            this.Controls.Add(this.panMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAssignMobileStreams";
            this.Text = "AssignTokenStreams";
            this.Load += new System.EventHandler(this.frmAssignTokenStreams_Load);
            this.SizeChanged += new System.EventHandler(this.frmAssignTokenStreams_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStream)).EndInit();
            this.panPopUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panMenu.ResumeLayout(false);
            this.panAddNew.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgStreamUpdate)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefersh;
        private System.Windows.Forms.DataGridView dgToken;
        private System.Windows.Forms.DataGridView dgStream;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Panel panPopUp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Panel panMenu;
        private System.Windows.Forms.Button btnMenuSearch;
        private System.Windows.Forms.Button btnMenuAddNew;
        private System.Windows.Forms.Panel panAddNew;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbTokenUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomerUpdate;
        private System.Windows.Forms.DataGridView dgStreamUpdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbStreamOwner;
        private System.Windows.Forms.Label label3;
    }
}