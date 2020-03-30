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
    public partial class frmClientDetail : Form
    {
        gblClass objMainClass = new gblClass();
        public frmClientDetail()
        {
            InitializeComponent();
            FillCountry();
        }
        private void FillCountry()
        {
            //string strCountry = "";
            //strCountry = "select * from CountryCodes order by countryName";
            //objMainClass.fnFillComboBox(strCountry, cmbCountryName, "countrycode", "countryName", "");
            string str = "";
            str = "select distinct DFClients.DFClientID,DFClients.ClientName from DFClients ";
            str = str + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
            str = str + " where DFClients.CountryCode is not null and DFClients.IsDealer=0 and DFClients.IsDealerclient is null ";
            str = str + " and AMPlayerTokens.IsDam=1 ";
            str = str + " order by DFClients.ClientName ";
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "");
        }
        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string str = "GetLicenseExpiryReport 0 , " + Convert.ToInt32(cmbCountryName.SelectedValue) + " , " + Convert.ToInt32(cmbClientName.SelectedValue) + " , '" + cmbPlayerType.Text + "'  , 'Dam' ";
            FillAdvertisementData(str);
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
            dgAdvtDetail.Columns.Add("TokenId", "TokenId");
            dgAdvtDetail.Columns["TokenId"].Width = 70;
            dgAdvtDetail.Columns["TokenId"].Visible = false;
            dgAdvtDetail.Columns["TokenId"].ReadOnly = true;

            //1
            dgAdvtDetail.Columns.Add("ClientName", "Client Name");
            dgAdvtDetail.Columns["ClientName"].Width = 200;
            dgAdvtDetail.Columns["ClientName"].Visible = true;
            dgAdvtDetail.Columns["ClientName"].ReadOnly = true;

            //2
            dgAdvtDetail.Columns.Add("DealerCode", "Dealer Code");
            dgAdvtDetail.Columns["DealerCode"].Width = 115;
            dgAdvtDetail.Columns["DealerCode"].Visible = false;
            dgAdvtDetail.Columns["DealerCode"].ReadOnly = false;

            //3
            dgAdvtDetail.Columns.Add("PlayerType", "Player Type");
            dgAdvtDetail.Columns["PlayerType"].Width = 120;
            dgAdvtDetail.Columns["PlayerType"].Visible = true;
            dgAdvtDetail.Columns["PlayerType"].ReadOnly = false;

            //4
            dgAdvtDetail.Columns.Add("AdvtExpiryDate", "Expiry Date");
            dgAdvtDetail.Columns["AdvtExpiryDate"].Width = 110;
            dgAdvtDetail.Columns["AdvtExpiryDate"].Visible = true;
            dgAdvtDetail.Columns["AdvtExpiryDate"].ReadOnly = false;

            //5
            dgAdvtDetail.Columns.Add("Username", "User Name");
            dgAdvtDetail.Columns["Username"].Width = 170;
            dgAdvtDetail.Columns["Username"].Visible = true;
            dgAdvtDetail.Columns["Username"].ReadOnly = false;

            //6
            dgAdvtDetail.Columns.Add("Cityname", "City Name");
            dgAdvtDetail.Columns["Cityname"].Width = 170;
            dgAdvtDetail.Columns["Cityname"].Visible = true;
            dgAdvtDetail.Columns["Cityname"].ReadOnly = false;

            //7
            dgAdvtDetail.Columns.Add("Location", "Location");
            dgAdvtDetail.Columns["Location"].Width = 150;
            dgAdvtDetail.Columns["Location"].Visible = true;
            dgAdvtDetail.Columns["Location"].ReadOnly = false;

            //8
            dgAdvtDetail.Columns.Add("IsAdvt", "Is Advt");
            dgAdvtDetail.Columns["IsAdvt"].Width = 80;
            dgAdvtDetail.Columns["IsAdvt"].Visible = true;
            dgAdvtDetail.Columns["IsAdvt"].ReadOnly = false;
            //9
            dgAdvtDetail.Columns.Add("AdvtExpiryDate1", "Advt Expiry Date");
            dgAdvtDetail.Columns["AdvtExpiryDate1"].Width = 235;
            dgAdvtDetail.Columns["AdvtExpiryDate1"].Visible = true;
            dgAdvtDetail.Columns["AdvtExpiryDate1"].ReadOnly = false;
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
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[i]["TokenID"];
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[i]["ClientName"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[2].Value = dtDetail.Rows[i]["Dealercode"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[3].Value = dtDetail.Rows[i]["Playertype"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[4].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["Expirydate"]);
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[5].Value = dtDetail.Rows[i]["username"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[6].Value = dtDetail.Rows[i]["Cityname"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[7].Value = dtDetail.Rows[i]["Location"].ToString();
                    dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[8].Value = Convert.ToBoolean(dtDetail.Rows[i]["isadvt"]);
                    if ((string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["advtExpiryDate"])).ToString() != "01-Jan-1900") 
                    {
                     //   MessageBox.Show(((string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["advtExpiryDate"])).ToString()));
                        dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[9].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["advtExpiryDate"]); 
                    }
                    else
                    {
                        dgAdvtDetail.Rows[dgAdvtDetail.Rows.Count - 1].Cells[9].Value = "- - -";
                    }
                }
            }
            foreach (DataGridViewRow row in dgAdvtDetail.Rows)
            {
                row.Height = 30;
            }
            lblTotalTokens.Text = dgAdvtDetail.Rows.Count.ToString();
        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalTokens.Text = "";
            InitilizeAdvtGrid();
        }

        private void cmbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalTokens.Text = "";
            InitilizeAdvtGrid();
        }
    }
}
