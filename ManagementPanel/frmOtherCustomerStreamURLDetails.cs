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
    public partial class frmOtherCustomerStreamURLDetails : Form
    {
        gblClass objMainClass = new gblClass();
        public frmOtherCustomerStreamURLDetails()
        {
            InitializeComponent();
        }

        private void cmbCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
        }
        private void InitilizeStreamGrid()
        {
            if (dgStream.Rows.Count > 0)
            {
                dgStream.Rows.Clear();
            }
            if (dgStream.Columns.Count > 0)
            {
                dgStream.Columns.Clear();
            }
            dgStream.Columns.Add("sName", "Stream Name");
            dgStream.Columns["sName"].Visible = true;
            dgStream.Columns["sName"].ReadOnly = true;
            dgStream.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgStream.Columns.Add("dName", "User Name");
            dgStream.Columns["dName"].Visible = true;
            dgStream.Columns["dName"].ReadOnly = true;
            dgStream.Columns["dName"].Width = 200;
            dgStream.Columns["dName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgStream.Columns.Add("tId", "Token Id");
            dgStream.Columns["tId"].Visible = true;
            dgStream.Columns["tId"].ReadOnly = true;
            dgStream.Columns["tId"].Width = 120;
            dgStream.Columns["tId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgStream.Columns.Add("lName", "Location");
            dgStream.Columns["lName"].Visible = true;
            dgStream.Columns["lName"].ReadOnly = true;
            dgStream.Columns["lName"].Width = 200;
            dgStream.Columns["lName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgStream.Columns.Add("State", "State");
            dgStream.Columns["State"].Visible = true;
            dgStream.Columns["State"].ReadOnly = true;
            dgStream.Columns["State"].Width = 150;
            dgStream.Columns["State"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgStream.Columns.Add("Country", "Country");
            dgStream.Columns["Country"].Visible = true;
            dgStream.Columns["Country"].ReadOnly = true;
            dgStream.Columns["Country"].Width = 200;
            dgStream.Columns["Country"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void FillOtherStreams()
        {
            string str = "GetOtherCustomerTokensStreamURL " + Convert.ToInt32(cmbCustomer.SelectedValue);
            DataTable dtDetail = new DataTable();
            dtDetail = objMainClass.fnFillDataTable(str);
            InitilizeStreamGrid();
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetail.Rows.Count; i++)
                {
                    dgStream.Rows.Add();
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StreamName"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["dName"].Value = dtDetail.Rows[i]["ClientName"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["tId"].Value = dtDetail.Rows[i]["Tokenid"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["lName"].Value = dtDetail.Rows[i]["location"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["state"].Value = dtDetail.Rows[i]["statename"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["country"].Value = dtDetail.Rows[i]["countryname"];
                }
            }
            //dtDetail.Rows[i]["Personname"] + "," + dtDetail.Rows[i]["location"] + "," + dtDetail.Rows[i]["streetname"] + "," + dtDetail.Rows[i]["cname"] + "," + dtDetail.Rows[i]["statename"] + "," + dtDetail.Rows[i]["countryname"];

        }
        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOtherStreams();
        }

        private void frmOtherCustomerStreamURLDetails_Load(object sender, EventArgs e)
        {
            InitilizeStreamGrid();
        }
    }
}
