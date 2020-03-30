using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmDealerLedger : Form
    {
        gblClass objMainClass = new gblClass();
        public frmDealerLedger()
        {
            InitializeComponent();
            InitilizeTokenGenerationGrid();
            FillClient();
        }
        private void btnUnload_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmDealerLedger_Load(object sender, EventArgs e)
        {
            
        }
        private void FillClient()
        {
            string str = "";
            str = "select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 order by DFClientID desc";
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "");
        }

        private void InitilizeTokenGenerationGrid()
        {
            if (dgDealerDetail.Rows.Count > 0)
            {
                dgDealerDetail.Rows.Clear();
            }
            if (dgDealerDetail.Columns.Count > 0)
            {
                dgDealerDetail.Columns.Clear();
            }

            dgDealerDetail.Columns.Add("UserId", "User Id");
            dgDealerDetail.Columns["UserId"].Width = 0;
            dgDealerDetail.Columns["UserId"].Visible = false;
            dgDealerDetail.Columns["UserId"].ReadOnly = true;

            dgDealerDetail.Columns.Add("Clientname", "Client Name");
            dgDealerDetail.Columns["Clientname"].Width = 250;
            dgDealerDetail.Columns["Clientname"].Visible = true;
            dgDealerDetail.Columns["Clientname"].ReadOnly = true;
            dgDealerDetail.Columns["Clientname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerDetail.Columns.Add("Username", "User Name");
            dgDealerDetail.Columns["Username"].Width = 250;
            dgDealerDetail.Columns["Username"].Visible = true;
            dgDealerDetail.Columns["Username"].ReadOnly = true;
            dgDealerDetail.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerDetail.Columns.Add("email", "E-mail");
            dgDealerDetail.Columns["email"].Width = 350;
            dgDealerDetail.Columns["email"].Visible = true;
            dgDealerDetail.Columns["email"].ReadOnly = true;
            dgDealerDetail.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerDetail.Columns.Add("MusicType", "Music Type");
            dgDealerDetail.Columns["MusicType"].Width = 200;
            dgDealerDetail.Columns["MusicType"].Visible = true;
            dgDealerDetail.Columns["MusicType"].ReadOnly = true;
            dgDealerDetail.Columns["MusicType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerDetail.Columns.Add("Tokens", "Tokens");
            dgDealerDetail.Columns["Tokens"].Width = 150;
            dgDealerDetail.Columns["Tokens"].Visible = true;
            dgDealerDetail.Columns["Tokens"].ReadOnly = true;
            dgDealerDetail.Columns["Tokens"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Detail";
            EditPlaylist.Text = "Detail";
            EditPlaylist.DataPropertyName = "Detail";
            dgDealerDetail.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 70;
            dgDealerDetail.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbClientName.SelectedValue) == 0)
            {
                MessageBox.Show("Plese select a dealer name", "Token Admin");
                cmbClientName.Focus();
                panDealerSum.Visible = false;
                return;
            }
           
        }
        private void FillTokenGenerationData(string sQr)
        {
            DataTable dtDetail = new DataTable();
            InitilizeTokenGenerationGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgDealerDetail.Rows.Add();

                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["UserId"].Value = dtDetail.Rows[i]["UserId"];
                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["Clientname"].Value = dtDetail.Rows[i]["Clientname"];
                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["Username"].Value = dtDetail.Rows[i]["Username"];
                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["email"].Value = dtDetail.Rows[i]["Useremail"];
                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["MusicType"].Value = dtDetail.Rows[i]["MusicType"];
                    dgDealerDetail.Rows[dgDealerDetail.Rows.Count - 1].Cells["Tokens"].Value = dtDetail.Rows[i]["NoofToken"];
                }
            }
            foreach (DataGridViewRow row in dgDealerDetail.Rows)
            {
                row.Height = 30;
            }
        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            panDealerSum.Visible = false;
            string sQr = "";
            sQr = " spDealerLedgerSummary " + Convert.ToInt32(cmbClientName.SelectedValue);
            DataTable dtDetail = new DataTable();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                lblIssueToken.Text = Convert.ToString(dtDetail.Rows[0]["IssueTokens"]);
                lblUsedTokens.Text = Convert.ToString(dtDetail.Rows[0]["UsedTokens"]);
                lblExpiryDate.Text = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[0]["ExpiryDate"]);
                lblDistribute.Text = Convert.ToString(dtDetail.Rows[0]["Distribute"]);
                lblNotUsed.Text = Convert.ToString(dtDetail.Rows[0]["FreeToken"]);
                lblDealerCode.Text = Convert.ToString(dtDetail.Rows[0]["DealerCode"]);
                panDealerSum.Visible = true;
            }
            sQr = " ";
            sQr = " select * from tbDealerLogin where dfclientid= " + Convert.ToInt32(cmbClientName.SelectedValue);
            dtDetail = new DataTable();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                lblCopyleftToken.Text = Convert.ToString(dtDetail.Rows[0]["DamTotalToken"]);
                lblCopyrightToken.Text = Convert.ToString(dtDetail.Rows[0]["CopyrightTotalToken"]);
                lblAsianToken.Text = Convert.ToString(dtDetail.Rows[0]["AsianTotalToken"]);
                lblTotalTokens.Text = (Convert.ToInt32(lblCopyleftToken.Text) + Convert.ToInt32(lblCopyrightToken.Text) + Convert.ToInt32(lblAsianToken.Text)).ToString();
            }
            else
            {
                lblCopyleftToken.Text = "0";
                lblCopyrightToken.Text = "0";
                lblAsianToken.Text = "0";
                lblTotalTokens.Text = "0";
            }

            sQr = " spDealerLedger '" + Convert.ToString(lblDealerCode.Text) + "'";
            FillTokenGenerationData(sQr);
        }

        private void dgDealerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
             
            if (e.ColumnIndex == 6)
            {
                if (Convert.ToInt32(dgDealerDetail.Rows[e.RowIndex].Cells["UserId"].Value) <= 0) return;
                frmDealerUserDetail frm = new frmDealerUserDetail();
                frm.MdiParent = this.MdiParent;
                StaticClass.DealerDfClientId = Convert.ToInt32(cmbClientName.SelectedValue);
                StaticClass.dealerUserId = Convert.ToInt32(dgDealerDetail.Rows[e.RowIndex].Cells["UserId"].Value);
                StaticClass.DealerUserName = dgDealerDetail.Rows[e.RowIndex].Cells["Username"].Value.ToString() + "  (" + dgDealerDetail.Rows[e.RowIndex].Cells["email"].Value.ToString() + ")  ";
                StaticClass.LocationX = this.Location.X;
                 StaticClass.LocationY = this.Location.Y;
               frm.Dock = DockStyle.Fill;
               // frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                //frm.StartPosition = FormStartPosition.CenterScreen;
              //  frm.WindowState = FormWindowState.Maximized;
                frm.MaximizeBox = false;
                frm.Show();
            }

        }

        private void cmbClientName_Click(object sender, EventArgs e)
        {
            FillClient();
            cmbClientName.SelectedValue = 0;
            cmbClientName.DroppedDown = true;
        }
    }
}
