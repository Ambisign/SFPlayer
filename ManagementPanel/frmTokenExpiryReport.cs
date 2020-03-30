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
    public partial class frmTokenExpiryReport : Form
    {
        gblClass objMainClass = new gblClass();
        public frmTokenExpiryReport()
        {
            InitializeComponent();
            if (gblClass.MusicType == "Asian")
            {
                lblfrmName.Text = "Asian";
                cmbPlayerVersion.Items.Clear();
                cmbPlayerVersion.Items.Add("Normal");
            }
            else if (gblClass.MusicType == "Dam")
            {
                lblfrmName.Text = "[ Copyleft ]";
                cmbPlayerVersion.Items.Clear();
                cmbPlayerVersion.Items.Add("Normal");
                cmbPlayerVersion.Items.Add("NativeCL");
            }
            else if (gblClass.MusicType == "Copyright")
            {
                lblfrmName.Text = "[ " + gblClass.MusicType + " ]";
                cmbPlayerVersion.Items.Clear();
                cmbPlayerVersion.Items.Add("Normal");
                cmbPlayerVersion.Items.Add("NativeCR");
            }
            FillCountry();
            cmbPlayerVersion.Text = "Normal";
         }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTokenExpiryReport_Load(object sender, EventArgs e)
        {
        }
        private void FillCountry()
        {
            string strCountry = "";
            strCountry = "select * from CountryCodes order by countryName";
            objMainClass.fnFillComboBox(strCountry, cmbCountryName, "countrycode", "countryName", "");
        }
        private void FillClient()
        {
            string str = "";
            str = "select distinct DFClients.DFClientID,DFClients.ClientName from DFClients ";
            str = str + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
            str = str + " where DFClients.CountryCode is not null and DFClients.IsDealer=0 and DFClients.IsDealerclient is null ";
            if (gblClass.MusicType == "Dam")
            {
                str = str + " and AMPlayerTokens.IsDam=1 ";
            }
            else if (gblClass.MusicType == "Asian")
            {
                str = str + " and AMPlayerTokens.IsAsian=1";
            }
            else if (gblClass.MusicType == "Copyright")
            {
                str = str + " and AMPlayerTokens.IsCopyright=1";
            }
            str = str + " and DFClients.CountryCode= " + Convert.ToInt32(cmbCountryName.SelectedValue);
            str = str + " order by DFClients.ClientName ";
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "");
        }

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillClient();
        }
        private void InitilizeAdvtGrid()
        {
            if (dgAdvtDetail.Rows.Count > 0)
            {
                dgAdvtDetail.Rows.Clear();
            }
            if (dgAdvtDetail.Columns.Count > 0)
            {
                dgAdvtDetail.Columns.Clear();
            }
            //0
            dgAdvtDetail.Columns.Add("tId", "TokenId");
            dgAdvtDetail.Columns["tId"].Width = 70;
            dgAdvtDetail.Columns["tId"].Visible = true;
            dgAdvtDetail.Columns["tId"].ReadOnly = true;
            dgAdvtDetail.Columns["tId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvtDetail.Columns.Add("expD", "Expiry Date");
            dgAdvtDetail.Columns["expD"].Width = 120;
            dgAdvtDetail.Columns["expD"].Visible = true;
            dgAdvtDetail.Columns["expD"].ReadOnly = false;
            dgAdvtDetail.Columns["expD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //1
            dgAdvtDetail.Columns.Add("Name", "Name");
            dgAdvtDetail.Columns["Name"].Width = 200;
            dgAdvtDetail.Columns["Name"].Visible = true;
            dgAdvtDetail.Columns["Name"].ReadOnly = true;
            dgAdvtDetail.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //2
            dgAdvtDetail.Columns.Add("loc", "Location");
            dgAdvtDetail.Columns["loc"].Width = 160;
            dgAdvtDetail.Columns["loc"].Visible = true;
            dgAdvtDetail.Columns["loc"].ReadOnly = false;
            dgAdvtDetail.Columns["loc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvtDetail.Columns.Add("cName", "City Name");
            dgAdvtDetail.Columns["cName"].Width = 115;
            dgAdvtDetail.Columns["cName"].Visible = true;
            dgAdvtDetail.Columns["cName"].ReadOnly = false;
            dgAdvtDetail.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //3
            dgAdvtDetail.Columns.Add("sName", "State Name");
            dgAdvtDetail.Columns["sName"].Width = 120;
            dgAdvtDetail.Columns["sName"].Visible = true;
            dgAdvtDetail.Columns["sName"].ReadOnly = false;
            dgAdvtDetail.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvtDetail.Columns.Add("coName", "Country");
            dgAdvtDetail.Columns["coName"].Width = 170;
            dgAdvtDetail.Columns["coName"].Visible = true;
            dgAdvtDetail.Columns["coName"].ReadOnly = false;
            dgAdvtDetail.Columns["coName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvtDetail.Columns.Add("ver", "Type");
            dgAdvtDetail.Columns["ver"].Width = 170;
            dgAdvtDetail.Columns["ver"].Visible = true;
            dgAdvtDetail.Columns["ver"].ReadOnly = false;
            dgAdvtDetail.Columns["ver"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvtDetail.Columns.Add("userId", "UserId");
            dgAdvtDetail.Columns["userId"].Width = 0;
            dgAdvtDetail.Columns["userId"].Visible = false;
            dgAdvtDetail.Columns["userId"].ReadOnly = false;

            DataGridViewLinkColumn ModifyToken = new DataGridViewLinkColumn();
            ModifyToken.HeaderText = "Modify";
            ModifyToken.Text = "Modify";
            ModifyToken.DataPropertyName = "Modify";
            dgAdvtDetail.Columns.Add(ModifyToken);
            ModifyToken.UseColumnTextForLinkValue = true;
            ModifyToken.Width = 70;
            ModifyToken.Visible = true;
            dgAdvtDetail.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            //string str = "GetLicenseExpiryReport " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'  , '" + gblClass.MusicType + "' ";
            if (cmbPlayerVersion.Text == "")
            {
                str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , 'Normal'  , '" + gblClass.MusicType + "' ";
            }
            else
            {
                str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'  , '" + gblClass.MusicType + "' ";
            }
            FillAdvertisementData(str);
        }

        private void cmbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            //string str = "GetLicenseExpiryReport " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "' , '" + gblClass.MusicType + "' ";
            if (cmbPlayerVersion.Text == "")
            {
                str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , 'Normal'  , '" + gblClass.MusicType + "' ";
            }
            else
            {
                str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'  , '" + gblClass.MusicType + "' ";
            }
            FillAdvertisementData(str);
        }
        private void FillAdvertisementData(string sQr)
        {
            DataTable dtDetail = new DataTable();
            InitilizeAdvtGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgAdvtDetail.Rows.Add();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["tId"].Value = dtDetail.Rows[i]["TokenID"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["expD"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["Expirydate"]);
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["Name"].Value = dtDetail.Rows[i]["PersonName"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["CoName"].Value = dtDetail.Rows[i]["CountryName"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StateName"].ToString();
                    
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["CityName"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["Loc"].Value = dtDetail.Rows[i]["Location"].ToString();

                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsStore"]) == true)
                    {
                        dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["ver"].Value = "Store";
                    }
                    else
                    {
                        dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["ver"].Value = "Stream";
                    }
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["userid"].Value = dtDetail.Rows[i]["userid"].ToString();
                }
            }
            foreach (DataGridViewRow row in dgAdvtDetail.Rows)
            {
                row.Height = 30;
            }
        }

        private void dgAdvtDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 9)
            {
                frmTokenInformation frm = new frmTokenInformation();
                StaticClass.DealerTokenId = 0;
                StaticClass.dealerUserId = Convert.ToInt32(dgAdvtDetail.Rows[e.RowIndex].Cells["userid"].Value);
                StaticClass.DealerDfClientId = Convert.ToInt32(cmbClientName.SelectedValue);
                StaticClass.DealerTokenId = Convert.ToInt32(dgAdvtDetail.Rows[e.RowIndex].Cells["tId"].Value);
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                string str = "";
                if (cmbPlayerVersion.Text == "")
                {
                    str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , 'Normal'  , '" + gblClass.MusicType + "' ";
                }
                else
                {
                    str = "GetLicenseExpiryToken " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'  , '" + gblClass.MusicType + "' ";
                }
                FillAdvertisementData(str);
            }
        }
    }
}
