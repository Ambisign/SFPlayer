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
    public partial class frmAdvertisementDetail : Form
    {
        gblClass objMainClass = new gblClass();
        public frmAdvertisementDetail()
        {
            InitializeComponent();
        }

        private void chkIsDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsDealer.Checked == true)
            {
                lblName.Text = "Dealer Name";
            }
            else
            {
                lblName.Text = "Client Name";
            }
            FillClient();
            string str = "GetAdvertisementDetail " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerType.Text + "'  , '" + gblClass.MusicType + "' ";
            FillAdvertisementData(str);
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmAdvertisementDetail_Load(object sender, EventArgs e)
        {
            if (gblClass.MusicType == "DirectLicense(Thai&Chinese)")
            {
                lblfrmName.Text = "[ DirectLicense(Thai&&Chinese) ]";
            }
            else if (gblClass.MusicType == "Dam")
            {
                lblfrmName.Text = "[ Copyleft ]";
            }
            else
            {
                lblfrmName.Text = "[ " + gblClass.MusicType + " ]";
            }
            FillCountry();
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
            if (chkIsDealer.Checked == true)
            {
                str = str + " where DFClients.CountryCode is not null and DFClients.IsDealer=1 and AMPlayerTokens.IsAdvt=1 ";
            }
            else
            {
                str = str + " where DFClients.CountryCode is not null and DFClients.IsDealer=0 and AMPlayerTokens.IsAdvt=1 ";
            }
            if (gblClass.MusicType=="Dam")
            {
        	str = str + " and AMPlayerTokens.IsDam=1 ";
            }
            else if (gblClass.MusicType == "DirectLicense(Sanjivani)")
            {
                str = str + " and AMPlayerTokens.IsSanjivani=1";
            }
            else if (gblClass.MusicType == "Copyright")
            {
                str = str + " and AMPlayerTokens.IsCopyright=1";
            }
            else if (gblClass.MusicType == "Asian")
            {
                str = str + " and AMPlayerTokens.IsAsian=1 ";
            }

            str = str + " and DFClients.IsDealerclient is null and DFClients.CountryCode= " + Convert.ToInt32(cmbCountryName.SelectedValue);
            str = str + " order by DFClients.ClientName ";
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "All");
        }
        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(cmbCountryName.SelectedValue )==0) return;
            FillClient();

          //  string str = "GetAdvertisementDetail " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerType.Text + "' ";
           // FillAdvertisementData(str);
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
            dgAdvtDetail.Columns.Add("tId", "TokenId");
            dgAdvtDetail.Columns["tId"].Width = 0;
            dgAdvtDetail.Columns["tId"].Visible = false;
            dgAdvtDetail.Columns["tId"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("tNo", "Token No");
            dgAdvtDetail.Columns["tNo"].Width = 325;
            dgAdvtDetail.Columns["tNo"].Visible = true;
            dgAdvtDetail.Columns["tNo"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("pName", "Person Name");
            dgAdvtDetail.Columns["pName"].Width = 190;
            dgAdvtDetail.Columns["pName"].Visible = true;
            dgAdvtDetail.Columns["pName"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("cName", "City");
            dgAdvtDetail.Columns["cName"].Width = 150;
            dgAdvtDetail.Columns["cName"].Visible = true;
            dgAdvtDetail.Columns["cName"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("sName", "Street");
            dgAdvtDetail.Columns["sName"].Width = 150;
            dgAdvtDetail.Columns["sName"].Visible = true;
            dgAdvtDetail.Columns["sName"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("Loc", "Location");
            dgAdvtDetail.Columns["Loc"].Width = 130;
            dgAdvtDetail.Columns["Loc"].Visible = true;
            dgAdvtDetail.Columns["Loc"].ReadOnly = true;

            dgAdvtDetail.Columns.Add("dCode", "Dealer Code");
            dgAdvtDetail.Columns["dCode"].Width = 115;
            dgAdvtDetail.Columns["dCode"].Visible = false;
            dgAdvtDetail.Columns["dCode"].ReadOnly = false;

            dgAdvtDetail.Columns.Add("AdvtExpiryDate", "Expiry Date");
            dgAdvtDetail.Columns["AdvtExpiryDate"].Width = 110;
            dgAdvtDetail.Columns["AdvtExpiryDate"].Visible = true;
            dgAdvtDetail.Columns["AdvtExpiryDate"].ReadOnly = false;

            dgAdvtDetail.Columns.Add("PlayerType", "Player Type");
            dgAdvtDetail.Columns["PlayerType"].Width = 110;
            dgAdvtDetail.Columns["PlayerType"].Visible = true;
            dgAdvtDetail.Columns["PlayerType"].ReadOnly = false;

             
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
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["tNo"].Value = dtDetail.Rows[i]["tidNo"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["PersonName"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["CityName"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StreetName"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["Loc"].Value = dtDetail.Rows[i]["Location"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["dCode"].Value = dtDetail.Rows[i]["Dealercode"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["AdvtExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["AdvtExpirydate"]);
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells["PlayerType"].Value = dtDetail.Rows[i]["PlayerType"];
                    
                }
            }
            foreach (DataGridViewRow row in dgAdvtDetail.Rows)
            {
                row.Height = 30;
            }
        }
        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "GetAdvertisementDetail " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerType.Text + "'  , '" + gblClass.MusicType + "' ";
            FillAdvertisementData(str);
        }

        private void cmbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "GetAdvertisementDetail " + Convert.ToByte(chkIsDealer.Checked) + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerType.Text + "' , '" + gblClass.MusicType + "' ";
            FillAdvertisementData(str);
        }
    }
}
