namespace ManagementPanel
{
    partial class frmTokenDetail
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
            this.lblErrormsg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClientName = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgToken = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panDealerSum.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToken)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panDealerSum);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbClientName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1213, 99);
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
            this.panDealerSum.Controls.Add(this.lblErrormsg);
            this.panDealerSum.Dock = System.Windows.Forms.DockStyle.Right;
            this.panDealerSum.Location = new System.Drawing.Point(429, 0);
            this.panDealerSum.Name = "panDealerSum";
            this.panDealerSum.Size = new System.Drawing.Size(780, 95);
            this.panDealerSum.TabIndex = 148;
            // 
            // lblTotalTokens
            // 
            this.lblTotalTokens.AutoSize = true;
            this.lblTotalTokens.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTotalTokens.ForeColor = System.Drawing.Color.White;
            this.lblTotalTokens.Location = new System.Drawing.Point(340, 15);
            this.lblTotalTokens.Name = "lblTotalTokens";
            this.lblTotalTokens.Size = new System.Drawing.Size(15, 17);
            this.lblTotalTokens.TabIndex = 155;
            this.lblTotalTokens.Text = "0";
            this.lblTotalTokens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(242, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 17);
            this.label9.TabIndex = 154;
            this.label9.Text = "Total Tokens :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAsianToken
            // 
            this.lblAsianToken.AutoSize = true;
            this.lblAsianToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAsianToken.ForeColor = System.Drawing.Color.Red;
            this.lblAsianToken.Location = new System.Drawing.Point(130, 64);
            this.lblAsianToken.Name = "lblAsianToken";
            this.lblAsianToken.Size = new System.Drawing.Size(15, 17);
            this.lblAsianToken.TabIndex = 153;
            this.lblAsianToken.Text = "0";
            this.lblAsianToken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopyrightToken
            // 
            this.lblCopyrightToken.AutoSize = true;
            this.lblCopyrightToken.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCopyrightToken.ForeColor = System.Drawing.Color.Red;
            this.lblCopyrightToken.Location = new System.Drawing.Point(130, 40);
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
            this.lblCopyleftToken.Location = new System.Drawing.Point(340, 40);
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
            this.lblDealerCode.Location = new System.Drawing.Point(130, 15);
            this.lblDealerCode.Name = "lblDealerCode";
            this.lblDealerCode.Size = new System.Drawing.Size(15, 17);
            this.lblDealerCode.TabIndex = 148;
            this.lblDealerCode.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 149;
            this.label7.Text = "Customer Code :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 149;
            this.label3.Text = "Android Tokens :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(12, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 17);
            this.label6.TabIndex = 147;
            this.label6.Text = "Copyright Tokens :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErrormsg
            // 
            this.lblErrormsg.AutoSize = true;
            this.lblErrormsg.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblErrormsg.ForeColor = System.Drawing.Color.Yellow;
            this.lblErrormsg.Location = new System.Drawing.Point(242, 40);
            this.lblErrormsg.Name = "lblErrormsg";
            this.lblErrormsg.Size = new System.Drawing.Size(95, 17);
            this.lblErrormsg.TabIndex = 141;
            this.lblErrormsg.Text = "Direct License :";
            this.lblErrormsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 21);
            this.label5.TabIndex = 132;
            this.label5.Text = "Customer\'s Token Detail";
            // 
            // cmbClientName
            // 
            this.cmbClientName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientName.FormattingEnabled = true;
            this.cmbClientName.Location = new System.Drawing.Point(13, 60);
            this.cmbClientName.Name = "cmbClientName";
            this.cmbClientName.Size = new System.Drawing.Size(338, 25);
            this.cmbClientName.TabIndex = 130;
            this.cmbClientName.SelectedIndexChanged += new System.EventHandler(this.cmbClientName_SelectedIndexChanged);
            this.cmbClientName.Click += new System.EventHandler(this.cmbClientName_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(10, 40);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(103, 17);
            this.lblName.TabIndex = 131;
            this.lblName.Text = "Customer Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 510);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1213, 52);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dgToken);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1213, 411);
            this.panel3.TabIndex = 17;
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
            this.dgToken.Location = new System.Drawing.Point(0, 0);
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
            this.dgToken.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgToken.Size = new System.Drawing.Size(1209, 407);
            this.dgToken.TabIndex = 7;
            this.dgToken.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgToken_CellClick);
            // 
            // frmTokenDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(51)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1213, 562);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTokenDetail";
            this.Text = "frmTokenDetail";
            this.Load += new System.EventHandler(this.frmTokenDetail_Load);
            this.SizeChanged += new System.EventHandler(this.frmTokenDetail_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panDealerSum.ResumeLayout(false);
            this.panDealerSum.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgToken)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbClientName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgToken;
        private System.Windows.Forms.Panel panDealerSum;
        private System.Windows.Forms.Label lblTotalTokens;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAsianToken;
        private System.Windows.Forms.Label lblCopyrightToken;
        private System.Windows.Forms.Label lblCopyleftToken;
        private System.Windows.Forms.Label lblDealerCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblErrormsg;
    }
}