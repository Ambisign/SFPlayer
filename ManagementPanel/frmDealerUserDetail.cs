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
    public partial class frmDealerUserDetail : Form
    {
        gblClass objMainClass = new gblClass();
        public frmDealerUserDetail()
        {
            InitializeComponent();
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmDealerUserDetail_Load(object sender, EventArgs e)
        {
            this.Location = new Point(StaticClass.LocationX, StaticClass.LocationY);
            //this.Height = 520;
            //this.Width = 988;
            lblClientName.Text = StaticClass.DealerUserName;
            InitilizeTokenDetailGrid();
            GetDealerUserLedger();
        }

        private void InitilizeTokenDetailGrid()
        {
            if (dgTokenDetail.Rows.Count > 0)
            {
                dgTokenDetail.Rows.Clear();
            }
            if (dgTokenDetail.Columns.Count > 0)
            {
                dgTokenDetail.Columns.Clear();
            }

            dgTokenDetail.Columns.Add("tId", "TokenId");
            dgTokenDetail.Columns["tId"].Width = 0;
            dgTokenDetail.Columns["tId"].Visible = false;
            dgTokenDetail.Columns["tId"].ReadOnly = true;
            dgTokenDetail.Columns["tId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgTokenDetail.Columns.Add("tNo", "Token No");
            dgTokenDetail.Columns["tNo"].Width = 325;
            dgTokenDetail.Columns["tNo"].Visible = true;
            dgTokenDetail.Columns["tNo"].ReadOnly = true;
            dgTokenDetail.Columns["tNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("pName", "Name");
            dgTokenDetail.Columns["pName"].Width = 200;
            dgTokenDetail.Columns["pName"].Visible = true;
            dgTokenDetail.Columns["pName"].ReadOnly = true;
            dgTokenDetail.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("Loc", "Advertising Category");
            dgTokenDetail.Columns["Loc"].Width = 130;
            dgTokenDetail.Columns["Loc"].Visible = true;
            dgTokenDetail.Columns["Loc"].ReadOnly = true;
            dgTokenDetail.Columns["Loc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("cName", "City");
            dgTokenDetail.Columns["cName"].Width = 150;
            dgTokenDetail.Columns["cName"].Visible = true;
            dgTokenDetail.Columns["cName"].ReadOnly = true;
            dgTokenDetail.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgTokenDetail.Columns.Add("sName", "State");
            dgTokenDetail.Columns["sName"].Width = 150;
            dgTokenDetail.Columns["sName"].Visible = true;
            dgTokenDetail.Columns["sName"].ReadOnly = true;
            dgTokenDetail.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("cuName", "Country");
            dgTokenDetail.Columns["cuName"].Width = 120;
            dgTokenDetail.Columns["cuName"].Visible = true;
            dgTokenDetail.Columns["cuName"].ReadOnly = true;
            dgTokenDetail.Columns["cuName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("ExpiryDate", "Expiry Date");
            dgTokenDetail.Columns["ExpiryDate"].Width = 120;
            dgTokenDetail.Columns["ExpiryDate"].Visible = true;
            dgTokenDetail.Columns["ExpiryDate"].ReadOnly = true;
            dgTokenDetail.Columns["ExpiryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenDetail.Columns.Add("pType", "Type");
            dgTokenDetail.Columns["pType"].Width = 120;
            dgTokenDetail.Columns["pType"].Visible = true;
            dgTokenDetail.Columns["pType"].ReadOnly = true;
            dgTokenDetail.Columns["pType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            dgTokenDetail.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            dgTokenDetail.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgTokenDetail.Columns.Add("uId", "uId");
            dgTokenDetail.Columns["uId"].Width = 0;
            dgTokenDetail.Columns["uId"].Visible = false;
            dgTokenDetail.Columns["uId"].ReadOnly = true;

        }
        private void GetDealerUserLedger()
        {
            string str = "GetUserTokenInformation " + Convert.ToInt32(StaticClass.dealerUserId);
            DataTable dtUserLedger = new DataTable();
            dtUserLedger = objMainClass.fnFillDataTable(str);
            if (dtUserLedger.Rows.Count > 0)
            {
                InitilizeTokenDetailGrid();
                /////////////// Fill User Token Detail //////////////
                for (int i = 0; i <= dtUserLedger.Rows.Count - 1; i++)
                {
                    dgTokenDetail.Rows.Add();
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["tId"].Value = dtUserLedger.Rows[i]["TokenID"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["tNo"].Value = dtUserLedger.Rows[i]["TokenID"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["pName"].Value = dtUserLedger.Rows[i]["PersonName"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["cName"].Value = dtUserLedger.Rows[i]["CityName"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["sName"].Value = dtUserLedger.Rows[i]["StateName"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["Loc"].Value = dtUserLedger.Rows[i]["Location"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["cuName"].Value = dtUserLedger.Rows[i]["CountryName"];
                    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["uId"].Value = dtUserLedger.Rows[i]["UserId"];
                    if (Convert.ToBoolean(dtUserLedger.Rows[i]["IsDam"]) == true)
                    {
                        dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["ExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtUserLedger.Rows[i]["DamExpiryDate"]);
                    }
                    //else if (Convert.ToBoolean(dtUserLedger.Rows[i]["IsSanjivani"]) == true)
                    //{
                    //    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["ExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtUserLedger.Rows[i]["SanjivaniExpiryDate"]);
                    //}
                    //else if (Convert.ToBoolean(dtUserLedger.Rows[i]["IsThai"]) == true)
                    //{
                    //    dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["ExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtUserLedger.Rows[i]["ThaiExpiryDate"]);
                    //}
                    else if (Convert.ToBoolean(dtUserLedger.Rows[i]["isCopyRight"]) == true)
                    {
                        dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["ExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtUserLedger.Rows[i]["CopyrightExpiryDate"]);
                    }
                    else if (Convert.ToBoolean(dtUserLedger.Rows[i]["IsAsian"]) == true)
                    {
                        dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["ExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtUserLedger.Rows[i]["AsianExpiryDate"]);
                    }

                    if (Convert.ToBoolean(dtUserLedger.Rows[i]["IsStore"]) == true)
                    {
                        dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["pType"].Value = "Store";
                    }
                    else
                    {
                        dgTokenDetail.Rows[dgTokenDetail.Rows.Count - 1].Cells["pType"].Value = "Stream";
                    }
                }
                dgTokenDetail.ClearSelection();

            }
        }

        private void dgTokenDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 9)
            {
                if (Convert.ToInt32(dgTokenDetail.Rows[e.RowIndex].Cells["tId"].Value) <= 0) return;
                frmTokenInformation frm = new frmTokenInformation();
              //  frm.MdiParent = this.MdiParent;
                StaticClass.DealerTokenId = Convert.ToInt32(dgTokenDetail.Rows[e.RowIndex].Cells["tId"].Value);
               // StaticClass.LocationX = 0;
              //  StaticClass.LocationY = 92;
              //  frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                GetDealerUserLedger();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
