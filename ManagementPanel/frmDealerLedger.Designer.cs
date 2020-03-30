namespace ManagementPanel
{
    partial class frmDealerLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDealerLedger));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnUnload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panDealerSum = new System.Windows.Forms.Panel();
            this.lblTotalTokens = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAsianToken = new System.Windows.Forms.Label();
            this.lblCopyrightToken = new System.Windows.Forms.Label();
            this.lblCopyleftToken = new System.Windows.Forms.Label();
            this.lblDealerCode = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblErrormsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsedTokens = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbClientName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNotUsed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblIssueToken = new System.Windows.Forms.Label();
            this.lblDistribute = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgDealerDetail = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panDealerSum.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDealerDetail)).BeginInit();
            this.SuspendLayout();
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
            this.btnUnload.Location = new System.Drawing.Point(411, 7);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(95, 38);
            this.btnUnload.TabIndex = 14;
            this.btnUnload.Text = "Cancel";
            this.btnUnload.UseVisualStyleBackColor = false;
            this.btnUnload.Visible = false;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panDealerSum);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cmbClientName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblNotUsed);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblIssueToken);
            this.panel1.Controls.Add(this.lblDistribute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 92);
            this.panel1.TabIndex = 15;
            // 
            // panDealerSum
            // 
            this.panDealerSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDealerSum.Controls.Add(this.lblTotalTokens);
            this.panDealerSum.Controls.Add(this.label9);
            this.panDealerSum.Controls.Add(this.lblAsianToken);
            this.panDealerSum.Controls.Add(this.lblCopyrightToken);
            this.panDealerSum.Controls.Add(this.lblCopyleftToken);
            this.panDealerSum.Controls.Add(this.lblDealerCode);
            this.panDealerSum.Controls.Add(this.label7);
            this.panDealerSum.Controls.Add(this.label3);
            this.panDealerSum.Controls.Add(this.label6);
            this.panDealerSum.Controls.Add(this.lblExpiryDate);
            this.panDealerSum.Controls.Add(this.lblErrormsg);
            this.panDealerSum.Controls.Add(this.label4);
            this.panDealerSum.Controls.Add(this.label2);
            this.panDealerSum.Controls.Add(this.lblUsedTokens);
            this.panDealerSum.Location = new System.Drawing.Point(521, 7);
            this.panDealerSum.Name = "panDealerSum";
            this.panDealerSum.Size = new System.Drawing.Size(734, 71);
            this.panDealerSum.TabIndex = 147;
            this.panDealerSum.Visible = false;
            // 
            // lblTotalTokens
            // 
            this.lblTotalTokens.AutoSize = true;
            this.lblTotalTokens.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTotalTokens.ForeColor = System.Drawing.Color.Red;
            this.lblTotalTokens.Location = new System.Drawing.Point(671, 43);
            this.lblTotalTokens.Name = "lblTotalTokens";
            this.lblTotalTokens.Size = new System.Drawing.Size(15, 17);
            this.lblTotalTokens.TabIndex = 155;
            this.lblTotalTokens.Text = "0";
            this.lblTotalTokens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(578, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 21);
            this.label9.TabIndex = 154;
            this.label9.Text = "Total Tokens :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAsianToken
            // 
            this.lblAsianToken.AutoSize = true;
            this.lblAsianToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAsianToken.ForeColor = System.Drawing.Color.Red;
            this.lblAsianToken.Location = new System.Drawing.Point(495, 42);
            this.lblAsianToken.Name = "lblAsianToken";
            this.lblAsianToken.Size = new System.Drawing.Size(29, 17);
            this.lblAsianToken.TabIndex = 153;
            this.lblAsianToken.Text = "636";
            this.lblAsianToken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopyrightToken
            // 
            this.lblCopyrightToken.AutoSize = true;
            this.lblCopyrightToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCopyrightToken.ForeColor = System.Drawing.Color.Red;
            this.lblCopyrightToken.Location = new System.Drawing.Point(349, 42);
            this.lblCopyrightToken.Name = "lblCopyrightToken";
            this.lblCopyrightToken.Size = new System.Drawing.Size(15, 17);
            this.lblCopyrightToken.TabIndex = 152;
            this.lblCopyrightToken.Text = "0";
            this.lblCopyrightToken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopyleftToken
            // 
            this.lblCopyleftToken.AutoSize = true;
            this.lblCopyleftToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCopyleftToken.ForeColor = System.Drawing.Color.Red;
            this.lblCopyleftToken.Location = new System.Drawing.Point(119, 42);
            this.lblCopyleftToken.Name = "lblCopyleftToken";
            this.lblCopyleftToken.Size = new System.Drawing.Size(15, 17);
            this.lblCopyleftToken.TabIndex = 151;
            this.lblCopyleftToken.Text = "0";
            this.lblCopyleftToken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDealerCode
            // 
            this.lblDealerCode.AutoSize = true;
            this.lblDealerCode.BackColor = System.Drawing.Color.Transparent;
            this.lblDealerCode.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDealerCode.ForeColor = System.Drawing.Color.White;
            this.lblDealerCode.Location = new System.Drawing.Point(119, 8);
            this.lblDealerCode.Name = "lblDealerCode";
            this.lblDealerCode.Size = new System.Drawing.Size(100, 17);
            this.lblDealerCode.TabIndex = 148;
            this.lblDealerCode.Text = "NLMYBR005898";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(12, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 21);
            this.label7.TabIndex = 149;
            this.label7.Text = "Dealer Code :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(384, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 21);
            this.label3.TabIndex = 149;
            this.label3.Text = "Asian Tokens :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(231, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 21);
            this.label6.TabIndex = 147;
            this.label6.Text = "Copyright Tokens :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExpiryDate.ForeColor = System.Drawing.Color.Red;
            this.lblExpiryDate.Location = new System.Drawing.Point(495, 7);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(82, 17);
            this.lblExpiryDate.TabIndex = 146;
            this.lblExpiryDate.Text = "31/Sep/2014";
            this.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblErrormsg
            // 
            this.lblErrormsg.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblErrormsg.ForeColor = System.Drawing.Color.Yellow;
            this.lblErrormsg.Location = new System.Drawing.Point(12, 39);
            this.lblErrormsg.Name = "lblErrormsg";
            this.lblErrormsg.Size = new System.Drawing.Size(109, 21);
            this.lblErrormsg.TabIndex = 141;
            this.lblErrormsg.Text = "Copyleft tokens  :";
            this.lblErrormsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(384, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 21);
            this.label4.TabIndex = 145;
            this.label4.Text = "Expiry Date  :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(231, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 21);
            this.label2.TabIndex = 142;
            this.label2.Text = "Used tokens  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsedTokens
            // 
            this.lblUsedTokens.AutoSize = true;
            this.lblUsedTokens.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUsedTokens.ForeColor = System.Drawing.Color.Red;
            this.lblUsedTokens.Location = new System.Drawing.Point(349, 8);
            this.lblUsedTokens.Name = "lblUsedTokens";
            this.lblUsedTokens.Size = new System.Drawing.Size(15, 17);
            this.lblUsedTokens.TabIndex = 144;
            this.lblUsedTokens.Text = "0";
            this.lblUsedTokens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.btnSearch.Location = new System.Drawing.Point(446, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 32);
            this.btnSearch.TabIndex = 105;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbClientName
            // 
            this.cmbClientName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientName.FormattingEnabled = true;
            this.cmbClientName.Location = new System.Drawing.Point(111, 40);
            this.cmbClientName.Name = "cmbClientName";
            this.cmbClientName.Size = new System.Drawing.Size(329, 25);
            this.cmbClientName.TabIndex = 100;
            this.cmbClientName.SelectedIndexChanged += new System.EventHandler(this.cmbClientName_SelectedIndexChanged);
            this.cmbClientName.Click += new System.EventHandler(this.cmbClientName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 21);
            this.label1.TabIndex = 101;
            this.label1.Text = "Dealer Name";
            // 
            // lblNotUsed
            // 
            this.lblNotUsed.AutoSize = true;
            this.lblNotUsed.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNotUsed.ForeColor = System.Drawing.Color.Red;
            this.lblNotUsed.Location = new System.Drawing.Point(518, 61);
            this.lblNotUsed.Name = "lblNotUsed";
            this.lblNotUsed.Size = new System.Drawing.Size(74, 17);
            this.lblNotUsed.TabIndex = 150;
            this.lblNotUsed.Text = "lblNotUsed";
            this.lblNotUsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNotUsed.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(2, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 21);
            this.label5.TabIndex = 98;
            this.label5.Text = "License Holder Control";
            // 
            // lblIssueToken
            // 
            this.lblIssueToken.AutoSize = true;
            this.lblIssueToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblIssueToken.ForeColor = System.Drawing.Color.Red;
            this.lblIssueToken.Location = new System.Drawing.Point(514, 14);
            this.lblIssueToken.Name = "lblIssueToken";
            this.lblIssueToken.Size = new System.Drawing.Size(86, 17);
            this.lblIssueToken.TabIndex = 143;
            this.lblIssueToken.Text = "lblIssueToken";
            this.lblIssueToken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIssueToken.Visible = false;
            // 
            // lblDistribute
            // 
            this.lblDistribute.AutoSize = true;
            this.lblDistribute.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDistribute.ForeColor = System.Drawing.Color.Red;
            this.lblDistribute.Location = new System.Drawing.Point(514, 37);
            this.lblDistribute.Name = "lblDistribute";
            this.lblDistribute.Size = new System.Drawing.Size(78, 17);
            this.lblDistribute.TabIndex = 148;
            this.lblDistribute.Text = "lblDistribute";
            this.lblDistribute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDistribute.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnUnload);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 646);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1280, 54);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dgDealerDetail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 92);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1280, 554);
            this.panel3.TabIndex = 17;
            // 
            // dgDealerDetail
            // 
            this.dgDealerDetail.AllowUserToAddRows = false;
            this.dgDealerDetail.AllowUserToDeleteRows = false;
            this.dgDealerDetail.AllowUserToResizeColumns = false;
            this.dgDealerDetail.AllowUserToResizeRows = false;
            this.dgDealerDetail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDealerDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDealerDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDealerDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDealerDetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgDealerDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDealerDetail.GridColor = System.Drawing.Color.Gainsboro;
            this.dgDealerDetail.Location = new System.Drawing.Point(0, 0);
            this.dgDealerDetail.Name = "dgDealerDetail";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(162)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDealerDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgDealerDetail.RowHeadersVisible = false;
            this.dgDealerDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDealerDetail.Size = new System.Drawing.Size(1276, 550);
            this.dgDealerDetail.TabIndex = 4;
            this.dgDealerDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDealerDetail_CellClick);
            // 
            // frmDealerLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDealerLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dealer Detail";
            this.Load += new System.EventHandler(this.frmDealerLedger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panDealerSum.ResumeLayout(false);
            this.panDealerSum.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDealerDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgDealerDetail;
        private System.Windows.Forms.Label lblUsedTokens;
        private System.Windows.Forms.Label lblIssueToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblErrormsg;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panDealerSum;
        private System.Windows.Forms.Label lblDistribute;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNotUsed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDealerCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCopyleftToken;
        private System.Windows.Forms.Label lblCopyrightToken;
        private System.Windows.Forms.Label lblAsianToken;
        private System.Windows.Forms.Label lblTotalTokens;
        private System.Windows.Forms.Label label9;
    }
}