using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Drawing.Imaging;
namespace ManagementPanel
{
    public partial class frmDealerRegistration : Form
    {
        gblClass objMainClass = new gblClass();
        SqlCommand cmdDealerLogo;
        Thread t2;
        Int32 DfClientId = 0;
        //Int32 LastTotalTokens = 0;
        Int32 LastTotalDamTokens = 0;
        Int32 LastTotalAsianTokens = 0;
        Int32 LastTotalSanjivaniTokens = 0;
        Int32 LastTotalCopyrightTokens = 0;
        Int32 SaveDfClientId = 0;
        Int32 ModifyLoginId = 0;
        Int32 TotalToken = 0;
        Int32 DamTotalToken = 0;
        Int32 CopyrightTotalToken = 0;
        Int32 SanjivaniTotalToken = 0;
        Int32 AsianTotalToken = 0;
        Int32 TempDfClientId = 0;
        string ExpiryDate="";
        string TotalEmail = "";
        string LoginName = "";
        string LoginPassword = "";
        string ClientEmail = "";
        string ClientName = "";
        string mAction = "Save";
        string str = "";
        string strDealerCountry = "";
        string strDealerName = "";
        string DealerCode = "";
        string intDealerCodeId = "";
        string MailMatterCopyleft = "";
        string MailMatterCopyright = "";
        string MailMatterAsian = "";
        string MailMatterSanjivani = "";
        MemoryStream ms;
        byte[] photo_aray;
        Boolean IsMailSend = true;
        public frmDealerRegistration()
        {
            InitializeComponent();
        }
        //private MusicPlayerTokenAdministrator mainForm = null;
        //public frmDealerRegistration(Form callingForm)
        //{
        //    mainForm = callingForm as MusicPlayerTokenAdministrator;
        //    InitializeComponent();
        //}  
        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy == true)
            {
                MessageBox.Show("The work is in progress…please wait", "Management Panel");
                return;
            }
            if (t2 != null)
            {
                if (t2.IsAlive == true)
                {
                    MessageBox.Show("Work Is Process");
                    return;
                }
            }
            Application.Exit();
        }

        private void frmDealerRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                dtpExpiryDate.Value = DateTime.Now.Date;
                InitilizeRegistrationGrid();
                FillCountry();
                str = "select DFClients.DFClientID,CountryCodes.CountryName, DFClients.ClientName,isnull(DFClients.Email,'') as email,DFClients.orderno , DFClients.DealerNoTotalToken ,DFClients.DealerCode, tbdealerlogin.Expirydate";
                str = str + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
                str = str + " inner join tbdealerlogin on DFClients.DFClientID= tbdealerlogin.DFClientID  ";
                str = str + " where DFClients.IsDealer=1 order by DFClientID desc";
                FillDealerRegistrationData(str);
            }
            catch (Exception ex)
            {
            }
        }
        private void FillCountry()
        {
            string strCountry = "";
            strCountry = "select * from CountryCodes order by countryCode";
            objMainClass.fnFillComboBox(strCountry, cmbSearchCountryName, "countrycode", "countryName", "");
            objMainClass.fnFillComboBox(strCountry, cmbCountryName, "countrycode", "countryName", "");
        }

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lStr = "";
            string sQr = "";
            Int32 OrderId = 0;
            intDealerCodeId = "";
            DataSet dsOrder = new DataSet();
            DataSet ds = new DataSet();


            lStr = "select * from CountryCodes where countryCode=" + Convert.ToInt32(cmbCountryName.SelectedValue);
            if (mAction == "Save")
            {
                sQr = "select count(DfclientId)+1 as MaxId from DFClients where year(RegistrationDate)=" + DateTime.Now.Year;
            }
            else
            {
                sQr = "select count(DfclientId)+1 as MaxId from DFClients where year(RegistrationDate)=" + DateTime.Now.Year + " and dfclientid < " + DfClientId;
            }
            dsOrder = objMainClass.fnFillDataSet(sQr);
            ds = objMainClass.fnFillDataSet(lStr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                OrderId = Convert.ToInt32(dsOrder.Tables[0].Rows[0]["MaxId"]);
                intDealerCodeId = "00" + OrderId;
                txtDealerName.Text = ds.Tables[0].Rows[0]["CountryNameShort"].ToString() + "-";
                
                txtOrderNo.Text = "A-" + DateTime.Now.Year + "-" + OrderId;
                strDealerCountry = ds.Tables[0].Rows[0]["CountryNameShort"].ToString();
                //txtCityName.Text = "";
            }
            string strState = "";
            strState = "select * from tbState where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue)+ " order by StateName ";
            objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");

            if (rdoSubDealer.Checked == true)
            {
                string str = "";
                str = "select DFClientID,ClientName from DFClients where CountryCode =" + Convert.ToInt32(cmbCountryName.SelectedValue) + " and DFClients.IsDealer=1 and dfclientid <> " + DfClientId + " order by DFClientID desc";
                objMainClass.fnFillComboBox(str, cmbMainDealer, "DFClientID", "ClientName", "");
            }
        }
        private void FillDealerRegistrationData(string sQr)
        {
            DataTable dtDetail = new DataTable();
            InitilizeRegistrationGrid();

            dtDetail = objMainClass.fnFillDataTable(sQr);

            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgDealerRegistration.Rows.Add();
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[i]["DFClientID"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[i]["CountryName"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[2].Value = dtDetail.Rows[i]["DealerCode"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[3].Value = dtDetail.Rows[i]["ClientName"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[4].Value = dtDetail.Rows[i]["email"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[5].Value = dtDetail.Rows[i]["DealerNoTotalToken"];
                    dgDealerRegistration.Rows[dgDealerRegistration.Rows.Count - 1].Cells[6].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["Expirydate"]);

                }
            }
            foreach (DataGridViewRow row in dgDealerRegistration.Rows)
            {
                row.Height = 30;
            }

        }
        private void InitilizeRegistrationGrid()
        {
            if (dgDealerRegistration.Rows.Count > 0)
            {
                dgDealerRegistration.Rows.Clear();
            }
            if (dgDealerRegistration.Columns.Count > 0)
            {
                dgDealerRegistration.Columns.Clear();
            }

            dgDealerRegistration.Columns.Add("ClientId", "ClientId");
            dgDealerRegistration.Columns["ClientId"].Width = 0;
            dgDealerRegistration.Columns["ClientId"].Visible = false;
            dgDealerRegistration.Columns["ClientId"].ReadOnly = true;

            dgDealerRegistration.Columns.Add("Countryname", "Country Name");
            dgDealerRegistration.Columns["Countryname"].Width = 160;
            dgDealerRegistration.Columns["Countryname"].Visible = true;
            dgDealerRegistration.Columns["Countryname"].ReadOnly = true;
            dgDealerRegistration.Columns["Countryname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgDealerRegistration.Columns.Add("DealerCode", "Customer Id");
            dgDealerRegistration.Columns["DealerCode"].Width = 120;
            dgDealerRegistration.Columns["DealerCode"].Visible = true;
            dgDealerRegistration.Columns["DealerCode"].ReadOnly = true;
            dgDealerRegistration.Columns["DealerCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgDealerRegistration.Columns.Add("Clientname", "Customer Name");
            dgDealerRegistration.Columns["Clientname"].Width = 220;
            dgDealerRegistration.Columns["Clientname"].Visible = true;
            dgDealerRegistration.Columns["Clientname"].ReadOnly = true;
            dgDealerRegistration.Columns["Clientname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerRegistration.Columns.Add("email", "E-mail");
            dgDealerRegistration.Columns["email"].Width = 230;
            dgDealerRegistration.Columns["email"].Visible = true;
            dgDealerRegistration.Columns["email"].ReadOnly = true;
            dgDealerRegistration.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDealerRegistration.Columns.Add("TotalToken", "Total Token");
            dgDealerRegistration.Columns["TotalToken"].Width = 120;
            dgDealerRegistration.Columns["TotalToken"].Visible = true;
            dgDealerRegistration.Columns["TotalToken"].ReadOnly = true;
            dgDealerRegistration.Columns["TotalToken"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgDealerRegistration.Columns.Add("ExpiryDate", "Expiry Date");
            dgDealerRegistration.Columns["ExpiryDate"].Width = 120;
            dgDealerRegistration.Columns["ExpiryDate"].Visible = true;
            dgDealerRegistration.Columns["ExpiryDate"].ReadOnly = true;
            dgDealerRegistration.Columns["ExpiryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            dgDealerRegistration.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            dgDealerRegistration.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn SetMainDealer = new DataGridViewLinkColumn();
            SetMainDealer.HeaderText = "";
            SetMainDealer.Text = "Set as Main Customer";
            SetMainDealer.DataPropertyName = "MainDealer";
            SetMainDealer.LinkColor = Color.FromArgb(64, 64, 64);
            SetMainDealer.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10 );
            SetMainDealer.LinkBehavior = LinkBehavior.SystemDefault;
            dgDealerRegistration.Columns.Add(SetMainDealer);
            SetMainDealer.UseColumnTextForLinkValue = true;
            SetMainDealer.Width = 150;
            dgDealerRegistration.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteClient = new DataGridViewLinkColumn();
            DeleteClient.HeaderText = "Delete";
            DeleteClient.Text = "Delete";
            DeleteClient.DataPropertyName = "Delete";
            DeleteClient.LinkColor = Color.FromArgb(64, 64, 64);
            DeleteClient.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10 );
            DeleteClient.LinkBehavior = LinkBehavior.SystemDefault;
            dgDealerRegistration.Columns.Add(DeleteClient);
            DeleteClient.UseColumnTextForLinkValue = true;
            DeleteClient.Width = 70;
            dgDealerRegistration.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            mAction = "Save";
            FillCountry();
            FillDealerRegistrationData(str);
            txtOrderNo.Text = "";
            txtDealerName.Text = "";
            txtEmail.Text = "";
            LastTotalDamTokens = 0;
            LastTotalAsianTokens = 0;
            LastTotalSanjivaniTokens = 0;
            LastTotalCopyrightTokens = 0;
            //txtCityName.Text = "";
            txtStreet.Text = "";
            picDealerLogo.Image = null;
            txtPlayerServiceName.Text = "";
            txtVatnumber.Text = "";
            SaveDfClientId = 0;
            ModifyLoginId = 0;
            DfClientId = 0;
            cmbCountryName.Focus();
            pBar.Value = 0;
            dtpExpiryDate.Value = DateTime.Now.Date;
            rdoMainDealer.Checked = false;
            rdoSubDealer.Checked = false;
            cmbMainDealer.SelectedValue = 0;
            cmbMainDealer.Text = "";
            txtDamToken.Text = "";
            txtCopyrightToken.Text = "";
            txtSanjivaniToken.Text = "";
            txtAsianToken.Text = "";
            chkAsianToken.Checked = false;
            chkDamToken.Checked = false;
            chkCopyrightToken.Checked = true;
            chkSanjivaniToken.Checked = false;
            txtTotalToken.Text = "";
            TotalToken = 0;
            MailMatterCopyleft = "";
            MailMatterCopyright = "";
            MailMatterAsian = "";
            MailMatterSanjivani = "";
            txtSupportEmail.Text = "";
            txtSupportPhNo.Text = "";
        }
        private Boolean SubmitValidation()
        {
            string sTr = "";
            DataSet dsEmail = new DataSet();
            DataSet dsUser = new DataSet();
            DataSet dsMainDeealer = new DataSet();
            if (mAction == "Save")
            {
                sTr = "select * from DFClients where email='" + txtEmail.Text + "' ";
            }
            else
            {
                sTr = "select * from DFClients where email='" + txtEmail.Text + "' and dfclientid <>" + DfClientId;
            }
            dsEmail = objMainClass.fnFillDataSet(sTr);

            sTr = "";
            if (mAction == "Save")
            {
                if (rdoMainDealer.Checked == true)
                {
                    sTr = "select * from DFClients where IsMainDealer=1 and CountryCode= " + Convert.ToInt32(cmbCountryName.SelectedValue);
                    dsMainDeealer = objMainClass.fnFillDataSet(sTr);
                    if (dsMainDeealer.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("The main dealer is already assigned for " + cmbCountryName.Text + " ", "Management Panel");
                        rdoMainDealer.Checked = false;
                        return false;
                    }
                }
            }
            else
            {
                if (rdoMainDealer.Checked == true)
                {
                    sTr = "select * from DFClients where IsMainDealer=1 and dfclientid <>" + DfClientId + " and CountryCode= " + Convert.ToInt32(cmbCountryName.SelectedValue);
                    dsMainDeealer = objMainClass.fnFillDataSet(sTr);
                    if (dsMainDeealer.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("The main dealer is already assigned " + cmbCountryName.Text + " ", "Management Panel");
                        rdoMainDealer.Checked = false;
                        return false;
                    }
                }
            }

            sTr = "";
            if (mAction == "Save")
            {

                sTr = "select * from DFClients where ClientName='" + txtDealerName.Text + "'";
            }
            else
            {
                sTr = "select * from DFClients where ClientName='" + txtDealerName.Text + "' and dfclientid <>" + DfClientId;
            }
            dsUser = objMainClass.fnFillDataSet(sTr);

            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list", "Management Panel");
                cmbCountryName.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a state", "Management Panel");
                cmbStateName.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbCityName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a city", "Management Panel");
                cmbCityName.Focus();
                return false;
            }
            else if (txtDealerName.Text.Substring(3, txtDealerName.Text.Length - 3) == "")
            {
                MessageBox.Show("Enter a proper customer name", "Management Panel");
                txtDealerName.Focus();
                txtDealerName.Select(3, 3);
                return false;
            }
            //else if (gblClass.EmailIsValid(txtEmail.Text) == false)
            //{
            //    MessageBox.Show("Enter a valid e-mail address", "Management Panel");
            //    txtEmail.Focus();
            //    return false;
            //}
            //else if (dsEmail.Tables[0].Rows.Count > 0)
            //{
            //    MessageBox.Show("Email already exists", "Management Panel");
            //    txtEmail.Focus();
            //    return false;
            //}
            else if (dsUser.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("This name already exists", "Management Panel");
                txtDealerName.Focus();
                return false;
            }
            
            else if ((txtDamToken.Text == "") && (txtCopyrightToken.Text == "") && (txtSanjivaniToken.Text == ""))
            {
                //MessageBox.Show("No of token cannot be blank or only use numeric words.", "Management Panel");
                MessageBox.Show("Please enter atleast one subscription token (Copyleft,Copyright).", "Management Panel");
                txtDamToken.Focus();
                return false;
            }
            if (txtTotalToken.Text == "") txtTotalToken.Text = "0";
            if (txtDamToken.Text == "") txtDamToken.Text = "0";
            else if (LastTotalDamTokens > Convert.ToInt32(txtDamToken.Text))
            {
                MessageBox.Show("You are not add less than old tokens", "Management Panel");
                //txtDamToken.Focus();
                return false;
            }
            if (txtCopyrightToken.Text == "") txtCopyrightToken.Text = "0";
            else if (LastTotalCopyrightTokens > Convert.ToInt32(txtCopyrightToken.Text))
            {
                MessageBox.Show("You are not add less than old tokens", "Management Panel");
                //txtDamToken.Focus();
                return false;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("E-mail cannot be blank", "Management Panel");
                txtEmail.Focus();
                return false;
            }
            else if (txtStreet.Text == "")
            {
                MessageBox.Show("The street name cannot be empty", "Management Panel");
                txtStreet.Focus();
                return false;
            }
            else if (txtStreet.TextLength <= 4)
            {
               // MessageBox.Show("Enter a correct street name", "Management Panel");
              //  txtStreet.Focus();
              //  return false;
            }
            if ((rdoMainDealer.Checked == false) && (rdoSubDealer.Checked == false))
            {
                MessageBox.Show("Assign the right dealer type", "Management Panel");
                cmbMainDealer.Focus();
                return false;
            }
            if (rdoSubDealer.Checked == true)
            {
                if (Convert.ToInt32(cmbMainDealer.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select a main customer name", "Management Panel");
                    cmbMainDealer.Focus();
                    return false;
                }
            }
            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtpExpiryDate.Value == DateTime.Now.Date)
            {
                dtpExpiryDate.Value = dtpExpiryDate.Value.AddYears(1);
            }
             
            if (objMainClass.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please check your Internet connection.", "Manageyourclaudio");
                return;
            }
            if (SubmitValidation() == false)
            {
                return;
            }
            
            ClientEmail = txtEmail.Text;
            ClientName = txtDealerName.Text;

            if (txtDamToken.Text == "")
            {
                txtDamToken.Text = "0";
            }
            if (txtCopyrightToken.Text == "")
            {
                txtCopyrightToken.Text = "0";
            }
            if (txtAsianToken.Text == "")
            {
                txtAsianToken.Text = "0";
            }
            if (txtSanjivaniToken.Text == "")
            {
                txtSanjivaniToken.Text = "0";
            }

            txtTotalToken.Text = (Convert.ToInt32(txtDamToken.Text) + Convert.ToInt32(txtCopyrightToken.Text) + Convert.ToInt32(txtSanjivaniToken.Text) + Convert.ToInt32(txtAsianToken.Text)).ToString();
            TotalToken = Convert.ToInt32(txtTotalToken.Text);

            DealerCode = txtDealerCode.Text;

            if (mAction == "Save")
            {
                SaveRegistration();
            }
            else if (mAction == "Modify")
            {
                ModifyRegistration();
            }
        }
        private void ModifyRegistration()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_DealerRegistration_Modify", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DFClientID", SqlDbType.BigInt));
            cmd.Parameters["@DFClientID"].Value = DfClientId;

            cmd.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.VarChar));
            cmd.Parameters["@ClientName"].Value = txtDealerName.Text;

            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters["@email"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@ResponsiblePersonName", SqlDbType.VarChar));
            cmd.Parameters["@ResponsiblePersonName"].Value = txtDealerName.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.BigInt));
            cmd.Parameters["@CountryCode"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.VarChar));
            cmd.Parameters["@StreetName"].Value = txtStreet.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
            cmd.Parameters["@CityName"].Value = cmbCityName.Text.Trim();

            if (txtDamToken.Text == "")
            {
                txtDamToken.Text = "0";
            }
            if (txtCopyrightToken.Text == "")
            {
                txtCopyrightToken.Text = "0";
            }
            if (txtAsianToken.Text == "")
            {
                txtAsianToken.Text = "0";
            }
            if (txtSanjivaniToken.Text == "")
            {
                txtSanjivaniToken.Text = "0";
            }

            txtTotalToken.Text = (Convert.ToInt32(txtDamToken.Text) + Convert.ToInt32(txtCopyrightToken.Text) + Convert.ToInt32(txtSanjivaniToken.Text) + Convert.ToInt32(txtAsianToken.Text)).ToString();

            DamTotalToken = Convert.ToInt32(txtDamToken.Text);
            CopyrightTotalToken = Convert.ToInt32(txtCopyrightToken.Text);
            SanjivaniTotalToken = Convert.ToInt32(txtSanjivaniToken.Text);
            AsianTotalToken = Convert.ToInt32(txtAsianToken.Text);

            cmd.Parameters.Add(new SqlParameter("@DealerNoTotalToken", SqlDbType.Int));
            cmd.Parameters["@DealerNoTotalToken"].Value = Convert.ToInt32(txtTotalToken.Text);

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = txtDealerCode.Text;

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@IsMainDealer", SqlDbType.Bit));
            cmd.Parameters["@IsMainDealer"].Value = Convert.ToByte(rdoMainDealer.Checked);

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;

            cmd.Parameters.Add(new SqlParameter("@IsSubDealer", SqlDbType.Bit));
            cmd.Parameters["@IsSubDealer"].Value = Convert.ToByte(rdoSubDealer.Checked);

            cmd.Parameters.Add(new SqlParameter("@MainDealerId", SqlDbType.BigInt));
            cmd.Parameters["@MainDealerId"].Value = Convert.ToInt32(cmbMainDealer.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@supportEmail", SqlDbType.VarChar));
            cmd.Parameters["@supportEmail"].Value = txtSupportEmail.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@supportPhoneNo", SqlDbType.VarChar));
            cmd.Parameters["@supportPhoneNo"].Value = txtSupportPhNo.Text.Trim();

            try
            {
                cmd.ExecuteNonQuery();

                SqlCommand cmdPublish = new SqlCommand();
                cmdPublish.Connection = StaticClass.constr;
                cmdPublish.CommandText = "update AMPlayerTokens set datetokenexpire = '"+ string.Format("{0:dd/MMM/yyyy}", dtpExpiryDate.Value) + "' where clientid=" + DfClientId;
                cmdPublish.ExecuteNonQuery();

                SaveDfClientId = DfClientId;
                SaveDealerLogin();
                //SendEmail();
                // MessageBox.Show("Record Modify", "Management Panel");
                //ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }
        private void SaveRegistration()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_DealerRegistration", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@InClientName", SqlDbType.VarChar));
            cmd.Parameters["@InClientName"].Value = txtDealerName.Text;

            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters["@email"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@OrderNo", SqlDbType.VarChar));
            cmd.Parameters["@Orderno"].Value = txtOrderNo.Text;

            cmd.Parameters.Add(new SqlParameter("@ResponsiblePersonName", SqlDbType.VarChar));
            cmd.Parameters["@ResponsiblePersonName"].Value = txtDealerName.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.BigInt));
            cmd.Parameters["@CountryCode"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.VarChar));
            cmd.Parameters["@StreetName"].Value = txtStreet.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
            cmd.Parameters["@CityName"].Value = cmbCityName.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@IsDealer", SqlDbType.Bit));
            cmd.Parameters["@IsDealer"].Value = 1;

            if (txtDamToken.Text == "")
            {
                txtDamToken.Text = "0";
            }
            if (txtCopyrightToken.Text == "")
            {
                txtCopyrightToken.Text = "0";
            }
            if (txtAsianToken.Text == "")
            {
                txtAsianToken.Text = "0";
            }
            if (txtSanjivaniToken.Text == "")
            {
                txtSanjivaniToken.Text = "0";
            }

            txtTotalToken.Text = (Convert.ToInt32(txtDamToken.Text) + Convert.ToInt32(txtCopyrightToken.Text) + Convert.ToInt32(txtSanjivaniToken.Text) + Convert.ToInt32(txtAsianToken.Text)).ToString();


            DamTotalToken = Convert.ToInt32(txtDamToken.Text);
            CopyrightTotalToken = Convert.ToInt32(txtCopyrightToken.Text);
            SanjivaniTotalToken = Convert.ToInt32(txtSanjivaniToken.Text);
            AsianTotalToken = Convert.ToInt32(txtAsianToken.Text);

            cmd.Parameters.Add(new SqlParameter("@DealerNoTotalToken", SqlDbType.Int));
            cmd.Parameters["@DealerNoTotalToken"].Value = (Convert.ToInt32(txtDamToken.Text) + Convert.ToInt32(txtCopyrightToken.Text) + Convert.ToInt32(txtSanjivaniToken.Text) + Convert.ToInt32(txtAsianToken.Text)).ToString();

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = txtDealerCode.Text;

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@IsMainDealer", SqlDbType.Bit));
            cmd.Parameters["@IsMainDealer"].Value = Convert.ToBoolean(rdoMainDealer.Checked);

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;

            cmd.Parameters.Add(new SqlParameter("@IsSubDealer", SqlDbType.Bit));
            cmd.Parameters["@IsSubDealer"].Value = Convert.ToByte(rdoSubDealer.Checked);

            cmd.Parameters.Add(new SqlParameter("@MainDealerId", SqlDbType.BigInt));
            cmd.Parameters["@MainDealerId"].Value = Convert.ToInt32(cmbMainDealer.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@supportEmail", SqlDbType.VarChar));
            cmd.Parameters["@supportEmail"].Value = txtSupportEmail.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@supportPhoneNo", SqlDbType.VarChar));
            cmd.Parameters["@supportPhoneNo"].Value = txtSupportPhNo.Text.Trim();
            try
            {

                SaveDfClientId = Convert.ToInt32(cmd.ExecuteScalar());

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                cmd.CommandText = "update DFClients set dealerdfclientid= " + Convert.ToInt32(SaveDfClientId) + "  where dfclientid= " + SaveDfClientId;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                //SaveOnlineRecord();
                SaveDealerLogin();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void SaveDealerLogin()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_DealerLogin", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SaveType", SqlDbType.VarChar));
            cmd.Parameters["@SaveType"].Value = mAction;

            cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt));
            cmd.Parameters["@LoginId"].Value = ModifyLoginId;

            cmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar));
            cmd.Parameters["@LoginName"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@DfClientId", SqlDbType.BigInt));
            cmd.Parameters["@DfClientId"].Value = SaveDfClientId;

            cmd.Parameters.Add(new SqlParameter("@LoginPassword", SqlDbType.VarChar));
            cmd.Parameters["@LoginPassword"].Value = "Player!@#" + SaveDfClientId + "player";

            cmd.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime));
            cmd.Parameters["@ExpiryDate"].Value =string.Format("{0:dd/MMM/yyyy}", dtpExpiryDate.Value);

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = txtDealerCode.Text;

            cmd.Parameters.Add(new SqlParameter("@DamTotalToken", SqlDbType.Int));
            cmd.Parameters["@DamTotalToken"].Value = txtDamToken.Text;
            cmd.Parameters.Add(new SqlParameter("@CopyrightTotalToken", SqlDbType.Int));
            cmd.Parameters["@CopyrightTotalToken"].Value = txtCopyrightToken.Text;
            cmd.Parameters.Add(new SqlParameter("@SanjivaniTotalToken", SqlDbType.Int));
            cmd.Parameters["@SanjivaniTotalToken"].Value = txtSanjivaniToken.Text;

            cmd.Parameters.Add(new SqlParameter("@IsDam", SqlDbType.Bit));
            cmd.Parameters["@IsDam"].Value = Convert.ToBoolean(chkDamToken.Checked);
            cmd.Parameters.Add(new SqlParameter("@IsCopyright", SqlDbType.Bit));
            cmd.Parameters["@IsCopyright"].Value = Convert.ToBoolean(chkCopyrightToken.Checked);
            cmd.Parameters.Add(new SqlParameter("@IsSanjivani", SqlDbType.Bit));
            cmd.Parameters["@IsSanjivani"].Value = Convert.ToBoolean(chkSanjivaniToken.Checked);

            cmd.Parameters.Add(new SqlParameter("@AsianTotalToken", SqlDbType.Int));
            cmd.Parameters["@AsianTotalToken"].Value = txtAsianToken.Text;

            cmd.Parameters.Add(new SqlParameter("@IsAsian", SqlDbType.Bit));
            cmd.Parameters["@IsAsian"].Value = Convert.ToBoolean(chkAsianToken.Checked);

            LoginName = txtEmail.Text;


           // ExpiryDate = dtpExpiryDate.Value;
            try
            {
                cmd.ExecuteNonQuery();

                if (picDealerLogo.Image != null)
                {
                    cmdDealerLogo = new SqlCommand("update tbDealerLogin set Dealerservicename='" + txtPlayerServiceName.Text + "' , DealerLogo=@photo where dfclientid=" + SaveDfClientId, StaticClass.constr);
                    conv_photo();
                }
                else
                {
                    cmdDealerLogo = new SqlCommand("update tbDealerLogin set Dealerservicename='" + txtPlayerServiceName.Text + "' , DealerLogo=null where dfclientid=" + SaveDfClientId, StaticClass.constr);
                }


                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                int n = cmdDealerLogo.ExecuteNonQuery();
                StaticClass.constr.Close();
                if (n > 0)
                {
                }



                if (mAction == "Save")
                {
                    LoginPassword = "Player!@#" + SaveDfClientId + "player";
                }
                else
                {
                    DataTable dtDetail = new DataTable();
                    string strResend = "";
                    strResend = "select * from  tbdealerlogin where dfclientid =" + SaveDfClientId;
                    dtDetail = objMainClass.fnFillDataTable(strResend);
                    LoginPassword = dtDetail.Rows[0]["LoginPassword"].ToString();
                }

                string sQr25 = "";
                DataTable dtModify = new DataTable();

                if (mAction == "Save")
                {
                    if (txtDamToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID, NoofToken from Users where(musictype='Copyleft' or musictype='NativeCL') and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Copyleft", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtDamToken.Text), 1, 0, Convert.ToInt32(txtTotalToken.Text), txtDealerCode.Text,0,0);

                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Copyleft", Convert.ToInt32(txtDamToken.Text), 1, 0, LastTotalDamTokens, txtDealerCode.Text,0,0);
                        }
                    }
                    if (txtCopyrightToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID , NoofToken from Users where (musictype='Copyright' or musictype='NativeCR') and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Copyright", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtCopyrightToken.Text), 0, 1, Convert.ToInt32(txtTotalToken.Text), txtDealerCode.Text,0,0);
                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Copyright", Convert.ToInt32(txtCopyrightToken.Text), 0, 1, LastTotalCopyrightTokens, txtDealerCode.Text,0,0);
                        }
                    }
                    if (txtAsianToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID, NoofToken from Users where (musictype='Asian' or musictype='NativeAsian')  and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Asian", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtAsianToken.Text), 0, 0, Convert.ToInt32(txtTotalToken.Text), txtDealerCode.Text,1,0);

                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Asian", Convert.ToInt32(txtAsianToken.Text), 0, 0, LastTotalAsianTokens, txtDealerCode.Text,1,0);
                        }
                    }
                    if (txtSanjivaniToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID, NoofToken from Users where musictype='Sanjivani' and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Sanjivani", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtSanjivaniToken.Text), 0, 0, Convert.ToInt32(txtTotalToken.Text), txtDealerCode.Text, 0, 1);
                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Sanjivani", Convert.ToInt32(txtSanjivaniToken.Text), 0, 0, LastTotalAsianTokens, txtDealerCode.Text, 0, 1);
                        }
                    }

                }

                else
                {
                    if (txtDamToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID, NoofToken from Users where (musictype='Copyleft' or musictype='NativeCL') and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Copyleft", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtDamToken.Text), 1, 0, LastTotalDamTokens, txtDealerCode.Text,0,0);
                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Copyleft", Convert.ToInt32(txtDamToken.Text), 1, 0, LastTotalDamTokens, txtDealerCode.Text,0,0);
                        }

                    }
                    if (txtCopyrightToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID,NoofToken from Users where (musictype='Copyright' or musictype='NativeCR') and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Copyright", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtCopyrightToken.Text), 0, 1, LastTotalCopyrightTokens, txtDealerCode.Text,0,0);
                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Copyright", Convert.ToInt32(txtCopyrightToken.Text), 0, 1, LastTotalCopyrightTokens, txtDealerCode.Text,0,0);
                        }

                    }
                    if (txtAsianToken.Text != "0")
                    {
                        sQr25 = "select top 1 UserID, NoofToken from Users where (musictype='Asian' or musictype='NativeAsian')  and clientid= " + SaveDfClientId + " order by UserID desc";
                        dtModify = objMainClass.fnFillDataTable(sQr25);
                        if (dtModify.Rows.Count > 0)
                        {
                            UpdateTokenUser(Convert.ToInt32(dtModify.Rows[0]["UserID"]), SaveDfClientId, "Asian", Convert.ToInt32(dtModify.Rows[0]["NoofToken"]), Convert.ToInt32(txtAsianToken.Text), 0, 0, Convert.ToInt32(txtTotalToken.Text), txtDealerCode.Text, 1, 0);

                        }
                        else
                        {
                            SaveTokenUser(SaveDfClientId, "Asian", Convert.ToInt32(txtAsianToken.Text), 0, 0, LastTotalAsianTokens, txtDealerCode.Text, 1, 0);
                        }
                    }
                }


                if (mAction == "Save")
                {
                    SendEmail();
                   // SendMailAdmin();
                }
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void UpdateTokenUser(Int32 ModifyUserId, Int32 dfClientid, string MusicType, Int32 TotalUserToken, Int32 TotalGenToken, int IsDam, int IsCopyright, Int32 LastTotalGenToken, string Savedealercode, int IsAsian,int IsSanjivani)
        {
            Int32 TotalNewToken = 0;
            TotalNewToken = TotalGenToken - LastTotalGenToken;
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = "update users set useremail='" + txtEmail.Text + "',countryid=" + Convert.ToInt32(cmbCountryName.SelectedValue) + ",Stateid=" + Convert.ToInt32(cmbStateName.SelectedValue) + ", Cityid=" + Convert.ToInt32(cmbCityName.SelectedValue) + ", cityname='" + cmbCityName.Text + "' ,street='" + txtStreet.Text + "', NoofToken=" + Convert.ToInt32(TotalUserToken + TotalNewToken) + " where userid= " + ModifyUserId;
            try
            {
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
                if (LastTotalGenToken < TotalGenToken)
                {

                    SaveTokenGeneration(ModifyUserId, TotalNewToken, IsDam, IsCopyright, Savedealercode,IsAsian,IsSanjivani);
                    //////////Mail Matter///////////
                    DataTable dtGetToken = new DataTable();
                    string MailMatter = "";
                    string str2 = "";
                    str2 = "select * from AMPlayerTokens where UserId=" + ModifyUserId + " and Code is null";
                    dtGetToken = objMainClass.fnFillDataTable(str2);
                    if (dtGetToken.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtGetToken.Rows.Count - 1; i++)
                        {
                            MailMatter = MailMatter + "Your installation token no: " + dtGetToken.Rows[i]["Token"].ToString() + " \n";
                        }
                    }
                    if (IsDam != 0) MailMatterCopyleft = MailMatter;
                    if (IsCopyright != 0)MailMatterCopyright = MailMatter;
                    if (IsAsian != 0) MailMatterAsian = MailMatter;
                    if (IsSanjivani != 0) MailMatterSanjivani = MailMatter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void SaveTokenUser(Int32 dfClientid, string MusicType, Int32 TotalGenToken, int IsDam, int IsCopyright, Int32 LastTotalGenToken, string Savedealercode, int IsAsian,int IsSanjivani)
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("InsertUsers", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar));
            cmd.Parameters["@UserName"].Value = txtDealerName.Text.Substring(3, txtDealerName.Text.Length - 3) + "--" + MusicType;

            cmd.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar));
            cmd.Parameters["@UserEmail"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@NoofToken", SqlDbType.BigInt));
            cmd.Parameters["@NoofToken"].Value = TotalGenToken;

            cmd.Parameters.Add(new SqlParameter("@PlayerType", SqlDbType.VarChar));
            cmd.Parameters["@PlayerType"].Value = "Desktop";

            cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.BigInt));
            cmd.Parameters["@ClientID"].Value = dfClientid;

            cmd.Parameters.Add(new SqlParameter("@Street", SqlDbType.VarChar));
            cmd.Parameters["@Street"].Value = txtStreet.Text;

            cmd.Parameters.Add(new SqlParameter("@Cityname", SqlDbType.VarChar));
            cmd.Parameters["@Cityname"].Value = cmbCityName.Text;

            cmd.Parameters.Add(new SqlParameter("@TeamviewerId", SqlDbType.VarChar));
            cmd.Parameters["@TeamviewerId"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@TvPassword", SqlDbType.VarChar));
            cmd.Parameters["@TvPassword"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@MusicType", SqlDbType.VarChar));
            cmd.Parameters["@MusicType"].Value = MusicType;

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;

            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar));
            cmd.Parameters["@Location"].Value = cmbCityName.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
            cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@PlayerVersion", SqlDbType.VarChar));
            if (MusicType == "Copyleft")
            {
                cmd.Parameters["@PlayerVersion"].Value = "NativeCL";
            }
            else if (MusicType == "Copyright")
            {
                cmd.Parameters["@PlayerVersion"].Value = "NativeCR";
            }
            else if (MusicType == "Asian")
            {
                cmd.Parameters["@PlayerVersion"].Value = "NativeAsian";
            }
            else
            {
                cmd.Parameters["@PlayerVersion"].Value = MusicType;
            }


            try
            {
                Int32 User_id = 0;
                User_id = Convert.ToInt32(cmd.ExecuteScalar());

                SaveTokenGeneration(User_id, TotalGenToken, IsDam, IsCopyright, Savedealercode,IsAsian,IsSanjivani);

                //////////Mail Matter///////////
                DataTable dtGetToken = new DataTable();
                string MailMatter = "";
                string strQ = "";
                strQ = "select * from AMPlayerTokens where UserId=" + User_id + " and Code is null";
                dtGetToken = objMainClass.fnFillDataTable(strQ);
                if (dtGetToken.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtGetToken.Rows.Count - 1; i++)
                    {
                        MailMatter = MailMatter + "Your installation token no: " + dtGetToken.Rows[i]["Token"].ToString() + " \n";
                    }
                    if (IsDam != 0) MailMatterCopyleft = MailMatter;
                    if (IsCopyright != 0) MailMatterCopyright = MailMatter;
                    if (IsAsian != 0) MailMatterAsian = MailMatter;
                    if (IsSanjivani != 0) MailMatterSanjivani = MailMatter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }


        private void SaveTokenGeneration(Int32 UserId, int TotalTokenGenrate, int IsDam, int IsCopyright, string SaveDealerCode, int IsAsian, int IsSanjivani)
        {
            pBar.Maximum = TotalTokenGenrate;
            for (int i = 1; i <= TotalTokenGenrate; i++)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("spDealer_AMTokensClient", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@DFClientID", SqlDbType.BigInt));
                cmd.Parameters["@DFClientID"].Value = SaveDfClientId;

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                cmd.Parameters["@UserId"].Value = UserId;

                cmd.Parameters.Add(new SqlParameter("@InNumberofTitles", SqlDbType.BigInt));
                cmd.Parameters["@InNumberofTitles"].Value = 5000;

                cmd.Parameters.Add(new SqlParameter("@isCopyRight", SqlDbType.Int));
                cmd.Parameters["@isCopyRight"].Value = IsCopyright;

                cmd.Parameters.Add(new SqlParameter("@InDateExp", SqlDbType.DateTime));
                if (IsCopyright == 0)
                {
                    cmd.Parameters["@InDateExp"].Value = "01-01-1900";
                }
                else
                {
                    cmd.Parameters["@InDateExp"].Value =string.Format("{0:dd/MMM/yyyy}", dtpExpiryDate.Value);
                }
                cmd.Parameters.Add(new SqlParameter("@IsDam", SqlDbType.Int));
                cmd.Parameters["@IsDam"].Value = IsDam;

                cmd.Parameters.Add(new SqlParameter("@DamExpiryDate", SqlDbType.DateTime));
                if (IsDam == 0)
                {
                    cmd.Parameters["@DamExpiryDate"].Value = "01-01-1900";
                }
                else
                {
                    cmd.Parameters["@DamExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}",dtpExpiryDate.Value);
                }

                cmd.Parameters.Add(new SqlParameter("@IsSanjivani", SqlDbType.Int));
                cmd.Parameters["@IsSanjivani"].Value = IsSanjivani;
                cmd.Parameters.Add(new SqlParameter("@SanjivaniExpiryDate", SqlDbType.DateTime));
                if (IsSanjivani == 0)
                {
                    cmd.Parameters["@SanjivaniExpiryDate"].Value = "01-01-1900";
                }
                else
                {
                    cmd.Parameters["@SanjivaniExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}",dtpExpiryDate.Value);
                }
                cmd.Parameters.Add(new SqlParameter("@IsFitness", SqlDbType.Int));
                cmd.Parameters["@IsFitness"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@FitnessExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@FitnessExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int));
                cmd.Parameters["@ServiceId"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@Dealercode", SqlDbType.VarChar));
                cmd.Parameters["@Dealercode"].Value = SaveDealerCode;

                cmd.Parameters.Add(new SqlParameter("@IsAsian", SqlDbType.Int));
                cmd.Parameters["@IsAsian"].Value = IsAsian;
                cmd.Parameters.Add(new SqlParameter("@AsianExpiryDate", SqlDbType.DateTime));
                if (IsAsian == 0)
                {
                    cmd.Parameters["@AsianExpiryDate"].Value = "01-01-1900";
                }
                else
                {
                    cmd.Parameters["@AsianExpiryDate"].Value = string.Format("{0:dd/MMM/yyyy}",dtpExpiryDate.Value);
                }
                if (IsDam == 1)
                {
                    cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                    cmd.Parameters["@pVersion"].Value = "NativeCL";
                }
                if (IsCopyright == 1)
                {
                    cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                    cmd.Parameters["@pVersion"].Value = "NativeCR";
                }
                if (IsAsian == 1)
                {
                    cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                    cmd.Parameters["@pVersion"].Value = "NativeAsian";
                }
                cmd.ExecuteNonQuery();
                pBar.Value = i;
            }
        }
        private void SendMailAdmin()
        {
            //t2 = new Thread(AdminMail);
            //t2.IsBackground = true;
            //t2.Start();
        }
        private void AdminMail()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                 var toAddress = "jan@jaroconsult.com";
               //var toAddress = "talwinder.parastechnologies@gmail.com";
                const string fromPassword = "Claudio@123456";
                string subject = "New Dealer Register";
                string body = "Dear Administrator, \n";
                body += "\n";
                body += "This is to inform you, that an email is sent to a new dealer for ";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += ", Copyright ";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += ", Sanjivani ";
                }
                if (AsianTotalToken != 0)
                {
                    body += ", Asian ";
                }
                body += "music services and his account is activated. \n";
                body += "The dealer credentials are: \n";
                body += "The main username is: " + ClientName + "\n";
                body += "The email address is: " + ClientEmail + "\n";
                body += "The dealer code is:- " + DealerCode + " \n";
                if (DamTotalToken != 0)
                {
                    body += "The copyleft player tokens are: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "The copyright player tokens are: " + CopyrightTotalToken + " \n";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "The sanjivani player tokens are: " + SanjivaniTotalToken + "\n";
                }
                if (AsianTotalToken!= 0)
                {
                    body += "The asian player tokens are: " + AsianTotalToken + "\n";
                }
                body += "The total tokens are: " + TotalToken + "\n";
                body += " \n";
                body += "Thank you for using our software. \n";
                body += "The Screen & Sound Solutions Team. \n";
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "gladiolus.arvixe.com";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 999999999;
                }
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                SendMailAdmin();
            }
        }
        private void SendEmail()
        {
            
            IsMailSend = true;
            GetRecord(SaveDfClientId);
        }

        private void SendMailCopyleft()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Welcome";
                string body = "Dear Customer \n";
                body += "\n";
                body += "This is to inform that you are assigned as a new dealer for ";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft(DAM) ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "and Copyright ";
                }
                if (AsianTotalToken != 0)
                {
                    body += "and Asian ";
                }
                body += "music services.\n ";
                body += "You are now registered as a new license holder on manageyourclaudio. \n";
                body += "\n";
                body += "Your token details are: \n";
                body += "Your installation user name: " + ClientName + " \n";
                body += "Your dealer code:-  " + DealerCode + " \n";
                body += "Your dealership expiry date: " + ExpiryDate + "   \n";
                if (DamTotalToken != 0)
                {
                    body += "Your copyleft player tokens: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {
                    body += "Your copyright player tokens: " + CopyrightTotalToken + " \n";
                }
                if (AsianTotalToken != 0)
                {
                    body += "Your asian player tokens: " + AsianTotalToken + " \n";
                }
                body += "Your total tokens: " + TotalToken + " \n";
                body += "The installation is required to be done in Chrome. \n";
                body += "Click on the link and follow the instructions carefully to install our software. \n";
                body += "http://manageyourclaudio.eu/manageyourclaudioSetup/TokenLicenseHolder.msi \n";
                body += "\n";
                body += "Once the installation is started you will need to enter your Username and your Password to complete the installation. \n";
                body += "Login credentials are :- \n";
                body += "Your User name:- " + LoginName + "   \n";
                body += "Your Password:-  " + LoginPassword + " \n";
                body += "\n";
                body += "Thank you for using our software.\n";
                body += "The MYCLAUD and Screen & Sound Solutions Team \n";

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "gladiolus.arvixe.com";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 999999999;
                }
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SendMailTotalFina()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Welcome";
                string body = "Dear Customer \n";
                body += "\n";
                body += "This is to you inform that you have become a dealer for ";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft(DAM) ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "and Copyright ";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "and Copyleft(Sanjivani) ";
                }
                body += "music services and you are registered as a new token license holder on manageyourclaudio. \n";
                body += "Token details are: \n";
                //body += "Email: " + LoginName + "   \n";
                body += "Dealership expiry date: " + ExpiryDate + "   \n";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft player tokens: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "Copyright player tokens: " + CopyrightTotalToken + " \n";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "and Sanjivani player tokens: " + SanjivaniTotalToken + " \n";
                }
                body += "Total tokens: " + TotalToken + " \n";
                body += "Please follow the instructions carefully for the best result. \n";
                body += "The installation is required to be done in Chrome. \n";
                if (DamTotalToken != 0)
                {
                    body += "Click on the link and follow the instructions carefully to install. \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/Total_Centomedia_MYM.msi \n";
                    body += "Once the installation is started you will need to use Token & Clientname to complete the installation. \n";
                    body += "Your installation user name: " + ClientName + " \n";
                    body += "" + MailMatterCopyleft + "";
                    body += "Your Dealer Code:-  " + DealerCode + " \n";
                    body += "Token expiry date: " + ExpiryDate + "   \n";
                }
                if (CopyrightTotalToken != 0)
                {
                    //body += "\n";
                    //body += "Click on the link and follow the instructions carefully to install. \n";
                    //body += "Copyright player setup for window7 or upper version:- \n";
                    //body += "http://manageyourclaudio.eu/manageyourclaudioSetup/Copyright/publish.htm \n";
                    //body += "Copyright player setup for windowXp only:- \n";
                    //body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/Copyright/publish.htm \n";
                    //body += "Once the installation is started you will need to use Token & Clientname to complete the installation. \n";
                    //body += "Your installation user name: " + ClientName + " \n";
                    //body += "" + MailMatterCopyright + "";
                    //body += "Your Dealer Code:-  " + DealerCode + " \n";
                    //body += "Token expiry date: " + ExpiryDate + "   \n";
                }
                body += "After starting up the player there is a username (admin) and pasword (admin) to fill in and tick the box to remember. \n";
                body += "WARNING:  Install the player on the network what you normally use LAN or WIFI. \n";
                body += "Switching or changing your network interface will make you player inoperative until it is switched back to the initial settings. \n";
                body += "\n";
                body += "Thank you ! \n";
                body += "The Manage your Media Team \n";

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "gladiolus.arvixe.com";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 999999999;
                }
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void GetDataModify()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Notification";
                string body = "Dear Customer, \n";
                body += "\n";
                body += "This is to you inform that your record is modified and modifications are:- \n";
                body += "You have become a dealer for ";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft(DAM) ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "and Copyright ";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "and Copyleft(Sanjivani) ";
                }
                body += "music services. \n";
                body += "You’re credentials are: \n";
                body += "Main username:" + ClientName + "\n";
                body += "Email: " + LoginName + "   \n";
                body += "Dealership expiry date: " + ExpiryDate + "   \n";
                if (DamTotalToken != 0)
                {
                    body += "Copyleft player tokens: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "Copyright player tokens: " + CopyrightTotalToken + " \n";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "and Sanjivani player tokens: " + SanjivaniTotalToken + " \n";
                }
                body += "Total tokens: " + TotalToken + " \n";
                body += "\n";
                body += "Click on the link and follow the instructions carefully to install. \n";
                body += "http://manageyourclaudio.eu/manageyourclaudioSetup/TokenLicenseHolder.msi \n";
                body += "Once the installation is started you will need to use Username and Password to complete the installation. \n";
                body += "\n";
                body += "Login credentials are :- \n";
                body += "Your User name:- " + LoginName + "   \n";
                body += "Your Password:-  " + LoginPassword + " \n";
                body += "Your Dealer Code:-  " + DealerCode + " \n";
                body += "\n";
                body += "Thank you ! \n";
                body += "The Manage your Media Team \n";

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "gladiolus.arvixe.com";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 999999999;
                }
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                SendEmail();
            }
        }

        private void frmDealerRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dgDealerRegistration_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DfClientId = 0;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 7)
            {
                DataTable dtDetail = new DataTable();
                string sQr = "";
                sQr = "select DFClientID,CountryCode , ClientName,isnull(Email,'') as email,orderno,cityname,streetname ,DealerNoTotalToken,DealerCode, ";
                sQr = sQr + " Stateid,cityId , isnull(IsMainDealer,0) as IsMainDealer,vatnumber , isnull(issubdealer,0) as isSubdealer, isnull(MainDealerId,0) as MainDealerId, isnull(supportEmail,'') as supportEmail,isnull(supportPhoneNo,'') as supportPhoneNo from DFClients ";
                sQr = sQr + " where DFClientID=" + dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value;
                dtDetail = objMainClass.fnFillDataTable(sQr);
                if (dtDetail.Rows.Count > 0)
                {
                    mAction = "Modify";
                    DfClientId = Convert.ToInt32(dtDetail.Rows[0]["DFClientID"]);
                    cmbCountryName.SelectedValue = dtDetail.Rows[0]["CountryCode"];
                    txtDealerName.Text = dtDetail.Rows[0]["ClientName"].ToString();
                    txtEmail.Text = dtDetail.Rows[0]["email"].ToString();
                    txtOrderNo.Text = dtDetail.Rows[0]["orderno"].ToString();
                    txtStreet.Text = dtDetail.Rows[0]["streetname"].ToString();
                    txtVatnumber.Text = dtDetail.Rows[0]["vatnumber"].ToString();
                    cmbStateName.SelectedValue = dtDetail.Rows[0]["Stateid"];

                    txtTotalToken.Text = dtDetail.Rows[0]["DealerNoTotalToken"].ToString();

                    cmbCityName.SelectedValue = dtDetail.Rows[0]["cityId"];
                    rdoMainDealer.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsMainDealer"]);
                    rdoSubDealer.Checked = Convert.ToBoolean(dtDetail.Rows[0]["issubdealer"]);

                    cmbMainDealer.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["MainDealerId"]);


                    txtDealerCode.Text = dtDetail.Rows[0]["DealerCode"].ToString();
                    txtSupportEmail.Text = dtDetail.Rows[0]["supportEmail"].ToString();
                    txtSupportPhNo.Text = dtDetail.Rows[0]["supportPhoneNo"].ToString();

                }
                sQr = "";
                sQr = "select * from  tbDealerLogin ";
                sQr = sQr + " where DFClientID=" + dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value;
                dtDetail = objMainClass.fnFillDataTable(sQr);
                if (dtDetail.Rows.Count > 0)
                {

                    chkDamToken.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsDam"]);
                    chkCopyrightToken.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsCopyright"]);
                    chkSanjivaniToken.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsSanjivani"]);
                    chkAsianToken.Checked = Convert.ToBoolean(dtDetail.Rows[0]["isAsian"]);

                    txtAsianToken.Text = Convert.ToString(dtDetail.Rows[0]["AsianTotalToken"]);
                    txtDamToken.Text = Convert.ToString(dtDetail.Rows[0]["DamTotalToken"]);
                    txtCopyrightToken.Text = Convert.ToString(dtDetail.Rows[0]["CopyrightTotalToken"]);
                    txtSanjivaniToken.Text = Convert.ToString(dtDetail.Rows[0]["SanjivaniTotalToken"]);

                    LastTotalDamTokens = Convert.ToInt32(txtDamToken.Text);
                    LastTotalCopyrightTokens = Convert.ToInt32(txtCopyrightToken.Text);
                    LastTotalAsianTokens= Convert.ToInt32(txtAsianToken.Text);
                    LastTotalSanjivaniTokens= Convert.ToInt32(txtSanjivaniToken.Text);

                    ModifyLoginId = Convert.ToInt32(dtDetail.Rows[0]["LoginId"]);
                    dtpExpiryDate.Value = Convert.ToDateTime(dtDetail.Rows[0]["ExpiryDate"]);
                    if (dtDetail.Rows[0]["DealerLogo"] != System.DBNull.Value)
                    {
                        photo_aray = (byte[])dtDetail.Rows[0]["DealerLogo"];
                        MemoryStream ms = new MemoryStream(photo_aray);
                        picDealerLogo.Image = Image.FromStream(ms);
                    }
                    if (dtDetail.Rows[0]["DealerServiceName"] != System.DBNull.Value)
                    {
                        txtPlayerServiceName.Text = dtDetail.Rows[0]["DealerServiceName"].ToString();
                    }
                }

            }
            if (e.ColumnIndex == 8)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to change the main customer ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    strDel = "update dfclients set ismaindealer=1,isSubdealer=0,Maindealerid=0 where dfclientid= " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    DataTable dtDetail = new DataTable();
                    dtDetail = objMainClass.fnFillDataTable("select countrycode from dfclients where dfclientid= " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value));
                    strDel = "";
                    strDel = "update dfclients set ismaindealer=0,isSubdealer=1,Maindealerid=" + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " where isdealer=1 and dfclientid !=" + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " and countrycode=" + Convert.ToInt32(dtDetail.Rows[0][0]);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    MessageBox.Show("Record is update","Management Panel");
                }
            }
            if (e.ColumnIndex == 9)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    strDel = "delete from UserDownloadTitle where dfclientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tblUser_Client_Rights where userid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tblmusic_player_settings where dfclientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from TitlesInPlaylists where Playlistid in( ";
                    strDel = strDel + "select distinct Playlistid from playlists where userid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + ")";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from playlists where userid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from users where clientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from AMPlayerTokens where clientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tbdealerlogin where dfclientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from DFClients where dfclientid = " + Convert.ToInt32(dgDealerRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    FillDealerRegistrationData(str);
                }
            }
        }

        private void cmbSearchCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            if (Convert.ToInt32(cmbSearchCountryName.SelectedValue) > 0)
            {
                sQr = "select DFClients.DFClientID,CountryCodes.CountryName, DFClients.ClientName,isnull(DFClients.Email,'') as email,DFClients.orderno , DFClients.DealerNoTotalToken ,DFClients.DealerCode, tbdealerlogin.Expirydate";
                sQr = sQr + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
                sQr = sQr + " inner join tbdealerlogin on DFClients.DFClientID= tbdealerlogin.DFClientID  ";
                sQr = sQr + " where CountryCodes.CountryCode = " + Convert.ToInt32(cmbSearchCountryName.SelectedValue) + " and DFClients.IsDealer=1 order by DFClientID desc";
                FillDealerRegistrationData(sQr);
            }
            else
            {
                FillDealerRegistrationData(str);
            }
        }

        private void txtSearchDealerName_KeyDown(object sender, KeyEventArgs e)
        {
            string sQr = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchDealerName.Text.Length <= 0)
                {
                    MessageBox.Show("Atleast enter one character for search", "Management Panel");
                    return;
                }
                if (txtSearchDealerName.Text.Length >= 1)
                {
                    sQr = "select DFClients.DFClientID,CountryCodes.CountryName, DFClients.ClientName,isnull(DFClients.Email,'') as email,DFClients.orderno , DFClients.DealerNoTotalToken ,DFClients.DealerCode, tbdealerlogin.Expirydate";
                    sQr = sQr + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
                    sQr = sQr + " inner join tbdealerlogin on DFClients.DFClientID= tbdealerlogin.DFClientID  ";
                    sQr = sQr + " where ClientName like '%" + txtSearchDealerName.Text + "%' and DFClients.IsDealer=1 order by DFClientID desc";
                    FillDealerRegistrationData(sQr);
                }
                else
                {
                    FillDealerRegistrationData(str);
                }


            }
        }

        private void txtDealerName_TextChanged(object sender, EventArgs e)
        {
            if (mAction == "Save")
            {
                txtDealerCode.Text = "";
                string strDealer = txtDealerName.Text;
                string strDealerCity = cmbCityName.Text;



                if (strDealer.Length >= 6)
                {
                    strDealerName = strDealerCountry + "" + strDealer.Substring(4, 2);
                    txtDealerCode.Text = strDealerName.ToUpper();
                }
                if (strDealerCity.Length >= 2)
                {
                    txtDealerCode.Text = strDealerName.ToUpper() + strDealerCity.Substring(0, 3).ToUpper() + intDealerCodeId;
                }
            }
        }

        private void txtCityName_TextChanged(object sender, EventArgs e)
        {
            if (mAction == "Save")
            {
                txtDealerCode.Text = "";
                string strDealerCity = cmbCityName.Text;
                string strDealer = txtDealerName.Text;
                if (strDealer.Length >= 5)
                {
                    strDealerName = strDealerCountry + "" + strDealer.Substring(3, 2);
                    txtDealerCode.Text = strDealerName.ToUpper();
                }
                if (strDealerCity.Length >= 2)
                {
                    txtDealerCode.Text = strDealerName.ToUpper() + strDealerCity.Substring(0, 2).ToUpper() + intDealerCodeId;
                }
            }
        }

        private void cmbStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCity = "";
            strCity = "select * from tbCity where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " and stateid =" + Convert.ToInt32(cmbStateName.SelectedValue) + " order by CityName";
            objMainClass.fnFillComboBox(strCity, cmbCityName, "Cityid", "CityName", "");
        }

        private void btnStateNew_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list name", "Management Panel");
                cmbCountryName.Focus();
                return;
            }
            txtName.Text = "";
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            lblCaption.Text = "State Name";
        }

        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            panMainNew.Visible = false;
            txtName.Text = "";
        }

        private void btnCityNew_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list name", "Management Panel");
                cmbCountryName.Focus();
                return;
            }
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a state name", "Management Panel");
                cmbStateName.Focus();
                return;
            }
            txtName.Text = "";
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            lblCaption.Text = "City Name";
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            string returnValue = "";
            string strState = "";
            if (lblCaption.Text == "State Name")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SaveState", StaticClass.constr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
                    cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

                    cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.VarChar));
                    cmd.Parameters["@StateName"].Value = txtName.Text;
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    returnValue = cmd.ExecuteScalar().ToString();
                    if (returnValue != "-2")
                    {
                        strState = "select * from tbState where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue);
                        objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");
                        cmbStateName.SelectedValue = Convert.ToInt32(returnValue);
                        panMainNew.Visible = false;
                        lblCaption.Text = "";
                    }
                    if (returnValue == "-2")
                    {
                        MessageBox.Show("State Name already exixts", "Management Panel");
                        panMainNew.Visible = false;
                        lblCaption.Text = "";
                        return;
                    }
                }
                catch (Exception ex) { }
            }
            else if (lblCaption.Text == "City Name")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SaveCity", StaticClass.constr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
                    cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

                    cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
                    cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

                    cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
                    cmd.Parameters["@CityName"].Value = txtName.Text;

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    returnValue = cmd.ExecuteScalar().ToString();
                    if (returnValue != "-2")
                    {

                        string strCity = "";
                        strCity = "select * from tbCity where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " and stateid =" + Convert.ToInt32(cmbStateName.SelectedValue);
                        objMainClass.fnFillComboBox(strCity, cmbCityName, "Cityid", "CityName", "");
                        cmbCityName.SelectedValue = Convert.ToInt32(returnValue);
                        panMainNew.Visible = false;
                        lblCaption.Text = "";
                    }
                    if (returnValue == "-2")
                    {
                        MessageBox.Show("City Name already exixts", "Management Panel");
                        panMainNew.Visible = false;
                        lblCaption.Text = "";
                        return;
                    }
                }
                catch (Exception ex) { }
            }
        }
        private void SaveState()
        {

        }

        private void cmbCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mAction == "Save")
            {
                txtDealerCode.Text = "";
                string strDealerCity = cmbCityName.Text;
                string strDealer = txtDealerName.Text;
                if (strDealer.Length >= 6)
                {
                    strDealerName = strDealerCountry + "" + strDealer.Substring(4, 2);
                    txtDealerCode.Text = strDealerName.ToUpper();
                }
                if (strDealerCity.Length >= 2)
                {
                    txtDealerCode.Text = strDealerName.ToUpper() + strDealerCity.Substring(0, 3).ToUpper() + intDealerCodeId;
                }
            }
        }

        private void btnMailResend_Click(object sender, EventArgs e)
        {
            if (dgDealerRegistration.CurrentCell.RowIndex == -1) return;
            Int32 Df_Client = Convert.ToInt32(dgDealerRegistration.Rows[dgDealerRegistration.CurrentCell.RowIndex].Cells[0].Value);
            IsMailSend = true;
            GetRecord(Df_Client);
        }
        int TotalTok = 0;
        string MailMatter = "";
        string CountryId = "";
        string SupportMatter = "";
        private void GetRecord(Int32 Df_Client)
        {
            DataTable dtDetail = new DataTable();
            string strResend = "";
            //////////Mail Matter///////////
            DataTable dtGetToken = new DataTable();

            string strQ = "";
            TotalTok = 0;
            MailMatter = "";
            strQ = "select * from AMPlayerTokens where Clientid=" + Df_Client + " and Code is null";
            dtGetToken = objMainClass.fnFillDataTable(strQ);
            if (dtGetToken.Rows.Count > 0)
            {
                for (int i = 0; i <= dtGetToken.Rows.Count - 1; i++)
                {
                    TotalTok = TotalTok + 1;
                    MailMatter = MailMatter + TotalTok + ". " + dtGetToken.Rows[i]["Token"].ToString() + " \n";
                }
            }


            SupportMatter = "";
            //strResend = "select * from  tbResendMail";
            strResend = "  select DFClients.*, tbDealerLogin.LoginPassword,tbDealerLogin.ExpiryDate, tbdealerlogin.DamTotalToken,tbdealerlogin.CopyrightTotalToken,tbdealerlogin.SanjivaniTotalToken ";
            strResend = strResend + "  from DFClients inner join tbDealerLogin on DFClients.DFClientID= tbDealerLogin.DFClientID ";
            strResend = strResend + " where DFClients.IsDealer=1 and DFClients.DFClientID = " + Df_Client;
            dtDetail = objMainClass.fnFillDataTable(strResend);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    if (bgWorker.IsBusy == false)
                    {
                        TempDfClientId = Convert.ToInt32(dtDetail.Rows[i]["DfclientId"]);
                        ClientEmail = dtDetail.Rows[i]["Email"].ToString();
                        TotalToken = Convert.ToInt32(dtDetail.Rows[i]["DealerNoTotalToken"]);
                        ExpiryDate = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["ExpiryDate"]);
                        ClientName = dtDetail.Rows[i]["ClientName"].ToString();
                        LoginName = dtDetail.Rows[i]["Email"].ToString();
                        LoginPassword = dtDetail.Rows[i]["LoginPassword"].ToString();
                        DealerCode = dtDetail.Rows[i]["DealerCode"].ToString();

                        DamTotalToken = Convert.ToInt32(dtDetail.Rows[i]["DamTotalToken"]);
                        CopyrightTotalToken = Convert.ToInt32(dtDetail.Rows[i]["CopyrightTotalToken"]);
                        SanjivaniTotalToken = Convert.ToInt32(dtDetail.Rows[i]["SanjivaniTotalToken"]);

                        CountryId = dtDetail.Rows[i]["countryCode"].ToString();
                        if (dtDetail.Rows[i]["supportPhoneNo"].ToString() != "")
                        {
                            SupportMatter = "For installation support, please call "+ dtDetail.Rows[i]["supportPhoneNo"].ToString() + " \n";
                        }
                        if (dtDetail.Rows[i]["supportEmail"].ToString() != "")
                        {
                            if (SupportMatter == "")
                            {
                                SupportMatter = "For installation support, please email "+ dtDetail.Rows[i]["supportEmail"].ToString() + " \n";
                            }
                            else
                            {
                                SupportMatter = "For installation support, please call " + dtDetail.Rows[i]["supportPhoneNo"].ToString() + " or email " + dtDetail.Rows[i]["supportEmail"].ToString() + " \n";
                            }
                        }
                        

                        bgWorker.RunWorkerAsync();
                        break;
                    }
                }
            }
        }
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {




                //var fromAddress = new MailAddress("noreplyadvikon@gmail.com", "Advikon");
                //var toAddress = new MailAddress(ClientEmail);

                //const string fromPassword = "Advikon@123";
                var fromAddress = new MailAddress("advikonservice@gmail.com", "Advikon Service");
                var toAddress = new MailAddress(ClientEmail);

                const string fromPassword = "Gilles23789@";
                string subject = "Notification";
                string body = "Dear Customer, \n";
                body += "\n";
                body += "Thank you for registering with ADVIKON music services.You have registered " + TotalTok + " tokens.";
                body += "\n";
                body += "\n";
                body += "Customer Name: "+ ClientName + "\n";
                body += "Dealer Code: "+ DealerCode + "\n";
                //body += "Player License Tokens: \n";
                //body += MailMatter;
                body += "\n";
                body += "You can manage your players through web panel. Login details are: \n";
                body += "Website: https://advikon.com/ \n";
                body += "Login Name: " + LoginName + " \n";
                body += "Password: " + LoginPassword + " \n";
                body += "License Expiry: " + ExpiryDate + "   \n";
                body += "\n";
                body += "Player download links are avilable on website under the Links menu. \n";
                //body += "https://play.google.com/store/apps/details?id=com.myclaudstreaming&hl=en";
                //body += "http://134.119.178.26/app/StoreAndForwardPlayer.zip";
               // body += "http://85.195.82.94/copyleft.zip";
             //   body += "\n";
               // body += SupportMatter;
                body += "\n";
                body += "Regards \n";
                body += "Support Team";
                body += "\n";
                body += "\n";
                body += "Note: Please Unzip the window player file after download. Then click on player exe for activation.";
                body += "\n";
                body += "\n";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                MailMessage message = new MailMessage();
                message.Subject = subject;
                message.Body = body;
                message.To.Add(toAddress);
             message.To.Add("jan@advikon.eu");
                message.To.Add("talwinder@advikon.eu");
                message.From = fromAddress;

                //using (var message = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body,

                //})
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                IsMailSend = false;
               // MessageBox.Show(ex.Message);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
            delete_temp_table();
            if (IsMailSend == true)
            {
              //  ReSendMailAdmin();
            }


            //SendMailAdmin();
            MessageBox.Show("Email is send succesfully", "Management Panel");
            //GetRecord();
        }
        void delete_temp_table()
        {
            //if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
            //StaticClass.constr.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = StaticClass.constr;
            //cmd.CommandText = "delete from tbResendMail where dfclientID= " + TempDfClientId;
            //cmd.ExecuteNonQuery();
            //StaticClass.constr.Close();
        }

        private void txtDealerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            OpenDialog.Filter = "png|*.png*";
            DialogResult res = OpenDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                picDealerLogo.Image = Image.FromFile(OpenDialog.FileName);
            }
        }
        void conv_photo()
        {
            //converting photo to binary data
            if (picDealerLogo.Image != null)
            {
                ms = new MemoryStream();
                picDealerLogo.Image.Save(ms, ImageFormat.Png);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                cmdDealerLogo.Parameters.AddWithValue("@photo", photo_aray);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            picDealerLogo.Image = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtVatnumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpExpiryDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdoSubDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSubDealer.Checked == true)
            {
                lblMainDealer.Enabled = true;
                cmbMainDealer.Enabled = true;
                string str = "";
                str = "select DFClientID,ClientName from DFClients where   DFClients.IsDealer=1 and dfclientid <> " + DfClientId + " order by DFClientID desc";
                objMainClass.fnFillComboBox(str, cmbMainDealer, "DFClientID", "ClientName", "");
            }
        }

        private void rdoMainDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMainDealer.Checked == true)
            {
                lblMainDealer.Enabled = false;
                cmbMainDealer.Enabled = false;
                cmbMainDealer.Text = "";
                cmbMainDealer.SelectedValue = 0;
            }
        }

        private void chkDamToken_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamToken.Checked == true)
            {
                txtDamToken.Enabled = true;
            }
            else
            {
                txtDamToken.Enabled = false;
            }
            if (txtDamToken.Text == "0")
            {
                txtDamToken.Text = "";
            }
        }

        private void chkCopyrightToken_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkCopyrightToken.Checked == true)
            //{
            //    txtCopyrightToken.Enabled = true;
            //}
            //else
            //{
            //    txtCopyrightToken.Enabled = false;
            //}
            //if (txtCopyrightToken.Text == "0")
            //{
            //    txtCopyrightToken.Text = "";
            //}
        }

        private void chkSanjivaniToken_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSanjivaniToken.Checked == true)
            {
                txtSanjivaniToken.Enabled = true;
            }
            else
            {
                txtSanjivaniToken.Enabled = false;
            }
            if (txtSanjivaniToken.Text == "0")
            {
                txtSanjivaniToken.Text = "";
            }
        }

        private void ReSendMailAdmin()
        {
            //t2 = new Thread(ReAdminMail);
            //t2.IsBackground = true;
            //t2.Start();
        }
        private void ReAdminMail()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = "jan@jaroconsult.com";
               // var toAddress = "talwinder.parastechnologies@gmail.com";
                const string fromPassword = "Claudio@123456";
                string subject = "Notification";
                string body = "Dear Administrator, \n";
                body += "\n";
                body += "This is to inform you, that an email is sent to a new dealer for ";

                if (DamTotalToken != 0)
                {
                    body += "Copyleft ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "and Copyright ";
                }
                if (AsianTotalToken != 0)
                {
                    body += "and Asian ";
                }
                body += " music Services and his account is activated. \n";
                body += "The dealer credentials are: \n";
                body += "The main Username is: " + ClientName + "\n";
                body += "The email address is: " + ClientEmail + "\n";

                if (DamTotalToken != 0)
                {
                    body += "The copyleft player tokens are: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "The copyright player tokens are: " + CopyrightTotalToken + " \n";
                }
                if (AsianTotalToken!= 0)
                {
                    body += "The asian player tokens are: " + AsianTotalToken + "\n";
                }

                body += "The total tokens are: " + TotalToken + "\n";
                body += "Dealer Code:- " + DealerCode + " \n";
                body += " \n";
                body += "Thank you for using our software.\n";
                body += "The Screen & Sound Solutions Team.\n";

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "gladiolus.arvixe.com";
                    //smtp.Host = "juniper.arvixe.com";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 999999999;
                }
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //SendMailAdmin();
            }
        }

        private void chkAsianToken_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsianToken.Checked == true)
            {
                txtAsianToken.Enabled = true;
            }
            else
            {
                txtAsianToken.Enabled = false;
            }
            if (txtAsianToken.Text == "0")
            {
                txtAsianToken.Text = "";
            }
        }


    }
}
//body += "Manageyourclaudio setup for window7 or upper version:- \n";
//if (DamTotalToken != 0)
//{
//    body += "Copyleft player:- http://manageyourclaudio.eu/manageyourclaudioSetup/Copyleft/publish.htm \n";

//}
//if (CopyrightTotalToken != 0)
//{
//    body += "Copyright player:- http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/publish.htm \n";
//}
//if (SanjivaniTotalToken != 0)
//{
//    body += "Sanjivani player:- http://manageyourclaudio.eu/manageyourclaudioSetup/IndianLicense/publish.htm \n";
//}

//body += "\n";
//body += "Manageyourclaudio setup for windowXp only:- \n";
//if (DamTotalToken != 0)
//{
//    body += "Copyleft player:- http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/Copyleft/publish.htm \n";
//}
//if (CopyrightTotalToken != 0)
//{
//    body += "Copyright player:- http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright/publish.htm \n";

//}
//if (SanjivaniTotalToken != 0)
//{
//    body += "Sanjivani player:- http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/IndianLicense/publish.htm \n";
//}