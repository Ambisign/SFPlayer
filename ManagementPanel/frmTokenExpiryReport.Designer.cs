namespace ManagementPanel
{
    partial class frmTokenExpiryReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkIsDealer = new System.Windows.Forms.CheckBox();
            this.cmbCountryName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPlayerVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblfrmName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClientName = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUnload = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgAdvtDetail = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAdvtDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkIsDealer);
            this.panel1.Controls.Add(this.cmbCountryName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbPlayerVersion);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblfrmName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbClientName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1250, 96);
            this.panel1.TabIndex = 17;
            // 
            // chkIsDealer
            // 
            this.chkIsDealer.AutoSize = true;
            this.chkIsDealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsDealer.ForeColor = System.Drawing.Color.Yellow;
            this.chkIsDealer.Location = new System.Drawing.Point(777, 10);
            this.chkIsDealer.Name = "chkIsDealer";
            this.chkIsDealer.Size = new System.Drawing.Size(104, 21);
            this.chkIsDealer.TabIndex = 142;
            this.chkIsDealer.Text = "Dealer Wise";
            this.chkIsDealer.UseVisualStyleBackColor = true;
            this.chkIsDealer.Visible = false;
            // 
            // cmbCountryName
            // 
            this.cmbCountryName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCountryName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCountryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCountryName.FormattingEnabled = true;
            this.cmbCountryName.Location = new System.Drawing.Point(3, 61);
            this.cmbCountryName.Name = "cmbCountryName";
            this.cmbCountryName.Size = new System.Drawing.Size(354, 25);
            this.cmbCountryName.TabIndex = 0;
            this.cmbCountryName.SelectedIndexChanged += new System.EventHandler(this.cmbCountryName_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 138;
            this.label4.Text = "Country Name";
            // 
            // cmbPlayerVersion
            // 
            this.cmbPlayerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlayerVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlayerVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlayerVersion.FormattingEnabled = true;
            this.cmbPlayerVersion.Location = new System.Drawing.Point(747, 61);
            this.cmbPlayerVersion.Name = "cmbPlayerVersion";
            this.cmbPlayerVersion.Size = new System.Drawing.Size(207, 25);
            this.cmbPlayerVersion.TabIndex = 2;
            this.cmbPlayerVersion.SelectedIndexChanged += new System.EventHandler(this.cmbPlayerType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(747, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 19);
            this.label3.TabIndex = 135;
            this.label3.Text = "Player Version";
            // 
            // lblfrmName
            // 
            this.lblfrmName.BackColor = System.Drawing.Color.Transparent;
            this.lblfrmName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblfrmName.ForeColor = System.Drawing.Color.White;
            this.lblfrmName.Location = new System.Drawing.Point(162, 4);
            this.lblfrmName.Name = "lblfrmName";
            this.lblfrmName.Size = new System.Drawing.Size(405, 21);
            this.lblfrmName.TabIndex = 132;
            this.lblfrmName.Text = "-----";
            this.lblfrmName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 21);
            this.label5.TabIndex = 132;
            this.label5.Text = "License Expiry Report";
            // 
            // cmbClientName
            // 
            this.cmbClientName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientName.FormattingEnabled = true;
            this.cmbClientName.Location = new System.Drawing.Point(363, 61);
            this.cmbClientName.Name = "cmbClientName";
            this.cmbClientName.Size = new System.Drawing.Size(378, 25);
            this.cmbClientName.TabIndex = 1;
            this.cmbClientName.SelectedIndexChanged += new System.EventHandler(this.cmbClientName_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(359, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(97, 19);
            this.lblName.TabIndex = 131;
            this.lblName.Text = "Client Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnUnload);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 572);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1250, 52);
            this.panel2.TabIndex = 18;
            // 
            // btnUnload
            // 
            this.btnUnload.BackColor = System.Drawing.Color.Transparent;
            this.btnUnload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnload.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnUnload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnUnload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnUnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnload.ForeColor = System.Drawing.Color.Yellow;
            this.btnUnload.Location = new System.Drawing.Point(429, 7);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(95, 38);
            this.btnUnload.TabIndex = 14;
            this.btnUnload.Text = "Cancel";
            this.btnUnload.UseVisualStyleBackColor = false;
            this.btnUnload.Visible = false;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dgAdvtDetail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1250, 476);
            this.panel3.TabIndex = 19;
            // 
            // dgAdvtDetail
            // 
            this.dgAdvtDetail.AllowUserToAddRows = false;
            this.dgAdvtDetail.AllowUserToDeleteRows = false;
            this.dgAdvtDetail.AllowUserToResizeColumns = false;
            this.dgAdvtDetail.AllowUserToResizeRows = false;
            this.dgAdvtDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.dgAdvtDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAdvtDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgAdvtDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAdvtDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAdvtDetail.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgAdvtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAdvtDetail.GridColor = System.Drawing.Color.Gainsboro;
            this.dgAdvtDetail.Location = new System.Drawing.Point(0, 0);
            this.dgAdvtDetail.Name = "dgAdvtDetail";
            this.dgAdvtDetail.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAdvtDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgAdvtDetail.RowHeadersVisible = false;
            this.dgAdvtDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgAdvtDetail.Size = new System.Drawing.Size(1246, 472);
            this.dgAdvtDetail.TabIndex = 4;
            this.dgAdvtDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAdvtDetail_CellClick);
            // 
            // frmTokenExpiryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1250, 624);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTokenExpiryReport";
            this.Text = "Token Expiry Report";
            this.Load += new System.EventHandler(this.frmTokenExpiryReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAdvtDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkIsDealer;
        private System.Windows.Forms.ComboBox cmbCountryName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPlayerVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblfrmName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbClientName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgAdvtDetail;
    }
}