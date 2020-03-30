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

namespace ManagementPanel
{
    public partial class frmTokenGeneration : Form
    {
        gblClass objMainClass = new gblClass();
        Thread t2;
        Int32 User_id = 0;
        DataTable dtGetToken = new DataTable();
        DataTable dtUserToken = new DataTable();
        string MainClientEmail = "";
        string MainUserName = "";
        string UserEmail = "";
        string LocationName = "";
        string UserName = "";
        DateTime UserDateExpiry ;
        string UserNoOfTitles = "";
        string UserPlayerType = "";
        string UserPlayerVersion = "";
        string SaveMode = "New";
        Int32 ModifyUserId = 0;
        string sQrStr = "";
        Int32 LastTotalTokens = 0;
        public frmTokenGeneration()
        {
            InitializeComponent();
            InitilizeTokenGenerationGrid();
            FillClient();
            dtpCopyrightExpiry.Value = DateTime.Now.Date;
            sQrStr = "spGetTokenDetails " + DateTime.Now.Year + ",0,0,1";
            FillTokenGenerationData(sQrStr);
        }
        //private MusicPlayerTokenAdministrator mainForm = null;
        // public frmTokenGeneration(Form callingForm)
        //{
        //    mainForm = callingForm as MusicPlayerTokenAdministrator;
        //    InitializeComponent();
        //}  
        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (t2 != null)
            {
                if (t2.IsAlive == true)
                {
                    MessageBox.Show("Work Is Process");
                    return;
                }
            }
            this.Hide();
        }

        private void frmTokenGeneration_Load(object sender, EventArgs e)
        {
            //if (objMainClass.CheckForInternetConnection() == false)
            //{
            //    MessageBox.Show("Please check your Internet connection.", "Manageyourclaudio");
            //    return;
            //}
            //InitilizeTokenGenerationGrid();
            //FillClient();
            //dtpCopyrightExpiry.Value = DateTime.Now.Date;
            //sQrStr = "spGetTokenDetails " + DateTime.Now.Year + ",0,0,1";
            //FillTokenGenerationData(sQrStr);
        }
        private void FillClient()
        {
            string str = "";
            str = "select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=0 and IsDealerclient is null order by DFClientID desc";
            objMainClass.fnFillComboBox(str, cmbSearchClientName, "DFClientID", "ClientName", "");
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "");

            str = "select * from tbTokenServices order by ServiceName";
            objMainClass.fnFillComboBox(str, cmbServiceName, "ServiceId", "ServiceName", "");

            str = "";
            str = "select * from CountryCodes order by countryCode";
            objMainClass.fnFillComboBox(str, cmbCountryName, "countrycode", "countryName", "");
        }

        private void InitilizeTokenGenerationGrid()
        {
            if (dgTokenGeneration.Rows.Count > 0)
            {
                dgTokenGeneration.Rows.Clear();
            }
            if (dgTokenGeneration.Columns.Count > 0)
            {
                dgTokenGeneration.Columns.Clear();
            }

            dgTokenGeneration.Columns.Add("UserId", "UserId");
            dgTokenGeneration.Columns["UserId"].Width = 0;
            dgTokenGeneration.Columns["UserId"].Visible = false;
            dgTokenGeneration.Columns["UserId"].ReadOnly = true;

            dgTokenGeneration.Columns.Add("Username", "User Name");
            dgTokenGeneration.Columns["Username"].Width = 240;
            dgTokenGeneration.Columns["Username"].Visible = true;
            dgTokenGeneration.Columns["Username"].ReadOnly = true;
            dgTokenGeneration.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenGeneration.Columns.Add("email", "E-mail");
            dgTokenGeneration.Columns["email"].Width = 350;
            dgTokenGeneration.Columns["email"].Visible = true;
            dgTokenGeneration.Columns["email"].ReadOnly = true;
            dgTokenGeneration.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgTokenGeneration.Columns.Add("Location", "Location");
            dgTokenGeneration.Columns["Location"].Width = 190;
            dgTokenGeneration.Columns["Location"].Visible = true;
            dgTokenGeneration.Columns["Location"].ReadOnly = true;
            dgTokenGeneration.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgTokenGeneration.Columns.Add("Token", "Token");
            dgTokenGeneration.Columns["Token"].Width = 180;
            dgTokenGeneration.Columns["Token"].Visible = true;
            dgTokenGeneration.Columns["Token"].ReadOnly = true;
            dgTokenGeneration.Columns["Token"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgTokenGeneration.Columns.Add("PlayerType", "Player Type");
            dgTokenGeneration.Columns["PlayerType"].Width = 120;
            dgTokenGeneration.Columns["PlayerType"].Visible = true;
            dgTokenGeneration.Columns["PlayerType"].ReadOnly = true;
            dgTokenGeneration.Columns["PlayerType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgTokenGeneration.Columns.Add("NoTitles", "No.Titles");
            dgTokenGeneration.Columns["NoTitles"].Width = 100;
            dgTokenGeneration.Columns["NoTitles"].Visible = true;
            dgTokenGeneration.Columns["NoTitles"].ReadOnly = true;
            dgTokenGeneration.Columns["NoTitles"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn EditUser = new DataGridViewLinkColumn();
            EditUser.HeaderText = "Edit";
            EditUser.Text = "Edit";
            EditUser.DataPropertyName = "Edit";
            dgTokenGeneration.Columns.Add(EditUser);
            EditUser.UseColumnTextForLinkValue = true;
            EditUser.Width = 50;
            EditUser.Visible = true;
            dgTokenGeneration.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //DataGridViewLinkColumn DeleteUser = new DataGridViewLinkColumn();
            //DeleteUser.HeaderText = "Delete";
            //DeleteUser.Text = "Delete";
            //DeleteUser.DataPropertyName = "Delete";
            //dgTokenGeneration.Columns.Add(DeleteUser);
            //DeleteUser.UseColumnTextForLinkValue = true;
            //DeleteUser.Width = 70;


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
                    dgTokenGeneration.Rows.Add();
                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[i]["UserID"];
                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[i]["UserName"];
                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[2].Value = dtDetail.Rows[i]["UserEmail"];
                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[3].Value = dtDetail.Rows[i]["Location"];

                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[4].Value = dtDetail.Rows[i]["NoofToken"];

                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[5].Value = dtDetail.Rows[i]["PlayerType"];
                    dgTokenGeneration.Rows[dgTokenGeneration.Rows.Count - 1].Cells[6].Value = dtDetail.Rows[i]["NumberofTitles"];

                }
            }
            foreach (DataGridViewRow row in dgTokenGeneration.Rows)
            {
                row.Height = 30;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            if (objMainClass.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please check your Internet connection.", "Management Panel");
                return;
            }
            if (SubmitValidation() == false)
            {
                return;
            }

            MainUserName = cmbClientName.Text;
            UserEmail = txtEmail.Text;
            UserName = txtUserName.Text;
            UserDateExpiry = dtpCopyrightExpiry.Value;
            UserNoOfTitles = cmbNoTitles.Text;
            UserPlayerType = "Desktop";
            UserPlayerVersion = cmbPlayerVersion.Text;
            LocationName = txtLocation.Text;
            pBar.Maximum = Convert.ToInt32(txtNoToken.Text);

            

            if (SaveMode == "New")
            {
                SaveTokenUser();
            }
            else
            {
                UpdateTokenUser();
            }
        }
        private void SaveTokenUser()
        {
            
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("InsertUsers", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar));
            cmd.Parameters["@UserName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar));
            cmd.Parameters["@UserEmail"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@NoofToken", SqlDbType.BigInt));
            cmd.Parameters["@NoofToken"].Value = Convert.ToInt32(txtNoToken.Text);

            cmd.Parameters.Add(new SqlParameter("@PlayerType", SqlDbType.VarChar));
            cmd.Parameters["@PlayerType"].Value = "Desktop";

            cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.BigInt));
            cmd.Parameters["@ClientID"].Value = Convert.ToInt32(cmbClientName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@Street", SqlDbType.VarChar));
            cmd.Parameters["@Street"].Value = txtStreet.Text;

            cmd.Parameters.Add(new SqlParameter("@Cityname", SqlDbType.VarChar));
            cmd.Parameters["@Cityname"].Value = cmbCityName.Text;

            cmd.Parameters.Add(new SqlParameter("@TeamviewerId", SqlDbType.VarChar));
            cmd.Parameters["@TeamviewerId"].Value = txtTeamviewerId.Text;

            cmd.Parameters.Add(new SqlParameter("@TvPassword", SqlDbType.VarChar));
            cmd.Parameters["@TvPassword"].Value = txtTVPassword.Text;

            cmd.Parameters.Add(new SqlParameter("@MusicType", SqlDbType.VarChar));
            cmd.Parameters["@MusicType"].Value = "Copyright";

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;

            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar));
            cmd.Parameters["@Location"].Value = txtLocation.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
            cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@PlayerVersion", SqlDbType.VarChar));
            cmd.Parameters["@PlayerVersion"].Value = cmbPlayerVersion.Text;

            try
            {
                User_id = Convert.ToInt32(cmd.ExecuteScalar());
                SaveTokenGeneration(User_id);
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
        private void SaveTokenGeneration(Int32 Return_UserId)
        {
            int TotalToken = 0;
            string str = "";
            if (SaveMode == "New")
            {
                TotalToken = Convert.ToInt16(txtNoToken.Text);
            }
            else
            {
                TotalToken = Convert.ToInt16(txtNoToken.Text) - LastTotalTokens;
            }
            for (int i = 1; i <= TotalToken; i++)
            {
               
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("spUsers_AMTokensClient", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@DFClientID", SqlDbType.BigInt));
                cmd.Parameters["@DFClientID"].Value = Convert.ToInt32(cmbClientName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                cmd.Parameters["@UserId"].Value = Return_UserId;

                cmd.Parameters.Add(new SqlParameter("@InNumberofTitles", SqlDbType.BigInt));
                cmd.Parameters["@InNumberofTitles"].Value = Convert.ToInt32(cmbNoTitles.Text);

                cmd.Parameters.Add(new SqlParameter("@IsDam", SqlDbType.Int));
                cmd.Parameters["@IsDam"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@DamExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@DamExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@IsSanjivani", SqlDbType.Int));
                cmd.Parameters["@IsSanjivani"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@SanjivaniExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@SanjivaniExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@isCopyRight", SqlDbType.Int));
                cmd.Parameters["@isCopyRight"].Value = "1";

                cmd.Parameters.Add(new SqlParameter("@InDateExp", SqlDbType.DateTime));
                cmd.Parameters["@InDateExp"].Value = dtpCopyrightExpiry.Value;

                cmd.Parameters.Add(new SqlParameter("@IsFitness", SqlDbType.Int));
                cmd.Parameters["@IsFitness"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@FitnessExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@FitnessExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int));
                cmd.Parameters["@ServiceId"].Value = Convert.ToInt32(cmbServiceName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
                cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
                cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
                cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.VarChar));
                cmd.Parameters["@StreetName"].Value = txtStreet.Text;

                cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar));
                cmd.Parameters["@Location"].Value = txtLocation.Text;

                cmd.Parameters.Add(new SqlParameter("@RemoteId", SqlDbType.VarChar));
                cmd.Parameters["@RemoteId"].Value = txtTeamviewerId.Text;

                cmd.Parameters.Add(new SqlParameter("@PersonName", SqlDbType.VarChar));
                cmd.Parameters["@PersonName"].Value = txtUserName.Text;

                cmd.Parameters.Add(new SqlParameter("@IsAsian", SqlDbType.Int));
                cmd.Parameters["@IsAsian"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@AsianExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@AsianExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@IsStore", SqlDbType.Int));
                cmd.Parameters["@IsStore"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                cmd.Parameters["@pVersion"].Value = cmbPlayerVersion.Text;

                cmd.ExecuteNonQuery();
                pBar.Value = i;
            }
            str = "select * from AMPlayerTokens where UserId=" + Return_UserId + " and Code is null";
            dtGetToken = objMainClass.fnFillDataTable(str);
            dtUserToken = objMainClass.fnFillDataTable(str);


            SendEmail();
            SendEmailMainClient();
            SendMailAdmin();
            ClearFields();
        }
        private void UpdateTokenUser()
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("UpdateUsers", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Userid", SqlDbType.BigInt));
            cmd.Parameters["@Userid"].Value = ModifyUserId;

            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar));
            cmd.Parameters["@UserName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar));
            cmd.Parameters["@UserEmail"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@NoofToken", SqlDbType.BigInt));
            cmd.Parameters["@NoofToken"].Value = Convert.ToInt32(txtNoToken.Text);

            cmd.Parameters.Add(new SqlParameter("@PlayerType", SqlDbType.VarChar));
            cmd.Parameters["@PlayerType"].Value = "Desktop";

            cmd.Parameters.Add(new SqlParameter("@Street", SqlDbType.VarChar));
            cmd.Parameters["@Street"].Value = txtStreet.Text;

            cmd.Parameters.Add(new SqlParameter("@Cityname", SqlDbType.VarChar));
            cmd.Parameters["@Cityname"].Value = cmbCityName.Text;

            cmd.Parameters.Add(new SqlParameter("@TeamviewerId", SqlDbType.VarChar));
            cmd.Parameters["@TeamviewerId"].Value = txtTeamviewerId.Text;

            cmd.Parameters.Add(new SqlParameter("@TvPassword", SqlDbType.VarChar));
            cmd.Parameters["@TvPassword"].Value = txtTVPassword.Text;

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;

            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar));
            cmd.Parameters["@Location"].Value = txtLocation.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
            cmd.Parameters["@CountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = Convert.ToInt32(cmbCityName.SelectedValue);
            try
            {
                cmd.ExecuteNonQuery();
                if (LastTotalTokens < Convert.ToInt32(txtNoToken.Text))
                {
                    SaveTokenGeneration(ModifyUserId);
                }
                else
                {
                    UpdateTokenGeneration(ModifyUserId);
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
        private void UpdateTokenGeneration(Int32 Return_UserId)
        {
            int TotalToken = 0;
            string str = "";
            TotalToken = Convert.ToInt16(txtNoToken.Text);
            for (int i = 1; i <= TotalToken; i++)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("UpdateAMPlayerTokens", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                cmd.Parameters["@UserId"].Value = Return_UserId;

                cmd.Parameters.Add(new SqlParameter("@NumberofTitles", SqlDbType.BigInt));
                cmd.Parameters["@NumberofTitles"].Value = Convert.ToInt32(cmbNoTitles.Text);

                cmd.Parameters.Add(new SqlParameter("@ServiceId", SqlDbType.Int));
                cmd.Parameters["@ServiceId"].Value = Convert.ToInt32(cmbServiceName.SelectedValue);
                
                //cmd.Parameters.Add(new SqlParameter("@InDateExp", SqlDbType.DateTime));
                //cmd.Parameters["@InDateExp"].Value = dtpExpiryDate.Value;
                cmd.ExecuteNonQuery();
                pBar.Value = i;
            }
            //MainUserName = cmbClientName.Text;
            //UserEmail = txtEmail.Text;
            //UserName = txtUserName.Text;
            //UserDateExpiry = dtpExpiryDate.Value;
            //UserNoOfTitles = cmbNoTitles.Text;
            //UserPlayerType = cmbPlayerType.Text;

            //str = "select * from AMPlayerTokens where UserId=" + Return_UserId + " and Code is null";
            //dtGetToken = objMainClass.fnFillDataTable(str);
            //dtUserToken = objMainClass.fnFillDataTable(str);

            //SendEmail();
            //SendEmailMainClient();
            
            ClearFields();
        }

        private void ClearFields()
        {
            string sQr = "";
            sQr = "spGetTokenDetails " + DateTime.Now.Year + ",0,0,1";
            FillClient();
            dtpCopyrightExpiry.Value = DateTime.Now.Date;
            FillTokenGenerationData(sQr);
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtLocation.Text = "";
            txtVatnumber.Text = "";
            cmbNoTitles.Text = "";
            cmbPlayerVersion.Text = "";
            cmbSearchPlayerVersion.Text = "";
            txtNoToken.Text = "";
            SaveMode = "New";
            ModifyUserId = 0;
            cmbClientName.Enabled = true;
            pBar.Value = 0;
            txtStreet.Text = "";
            txtTeamviewerId.Text = "";
            txtTVPassword.Text = "0";
            LastTotalTokens = 0;
            lblTotalTokens.Visible = false;
        }
        private Boolean SubmitValidation()
        {
            Int32 intValue;
            string str = "";
            DataSet dsValidation = new DataSet();
            DataSet dsEmail = new DataSet();
            if (SaveMode == "New")
            {
                str = "select * from Users where UserName='" + txtUserName.Text + "' and clientid=" + cmbClientName.SelectedValue;
                //str = str + " and UserEmail='" + txtEmail.Text + "' and Playertype='" + cmbPlayerType.Text + "'";
            }
            else
            {
                str = "select * from Users where UserName='" + txtUserName.Text + "'  and userId <>" + ModifyUserId + " and clientid=" + cmbClientName.SelectedValue ;
//                str = str + " and UserEmail='" + txtEmail.Text + "' and Playertype='" + cmbPlayerType.Text + "

            }
            dsValidation = objMainClass.fnFillDataSet(str);


            str = "";
            if (SaveMode == "New")
            {
                str = "select * from Users where UserEmail='" + txtEmail.Text + "'";
            }
            else
            {
                str = "select * from Users where UserEmail='" + txtEmail.Text + "' and userId <>" + ModifyUserId;
            }
            dsEmail = objMainClass.fnFillDataSet(str);
            if (Convert.ToInt32(cmbClientName.SelectedValue) == 0)
            {
                MessageBox.Show("Client name cannot be blank", "Management Panel");
                cmbClientName.Focus();
                return false;
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("User name cannot be blank", "Management Panel");
                txtUserName.Focus();
                return false;
            }
            //else if (gblClass.EmailIsValid(txtEmail.Text) == false)
            //{
            //    MessageBox.Show("Enter a valid e-mail address", "Management Panel");
            //    txtEmail.Focus();
            //    return false;
            //}

            else if (Convert.ToInt32(txtNoToken.Text) > 100)
            {
                MessageBox.Show("Only one time 100 tokens are generated", "Management Panel");
                txtNoToken.Focus();
                return false;
            }
            else if (LastTotalTokens > Convert.ToInt32(txtNoToken.Text))
            {
                MessageBox.Show("You are not add less than old tokens");
                txtNoToken.Focus();
                return false;
            }
            
        //else if (dsEmail.Tables[0].Rows.Count > 0)
            //{
            //    MessageBox.Show("Email already exist for this player type", "Management Panel");
            //    txtEmail.Focus();
            //    return false;
            //}
            else if (txtVatnumber.Text == "")
            {
                MessageBox.Show("Vat number cannot be blank", "Management Panel");
                txtVatnumber.Focus();
                return false;
            }
            else if (txtLocation.Text == "")
            {
                MessageBox.Show("The location cannot be empty", "Management Panel");
                txtLocation.Focus();
                return false;
            }
            else if (cmbNoTitles.Text == "")
            {
                MessageBox.Show("Select no of titles", "Management Panel");
                cmbNoTitles.Focus();
                return false;
            }
            else if (cmbPlayerVersion.Text == "")
            {
                MessageBox.Show("Select a player version", "Management Panel");
                cmbPlayerVersion.Focus();
                return false;
            }
            else if (txtTeamviewerId.Text == "")
            {
                MessageBox.Show("Ammy Id cannot be blank", "Management Panel");
                txtTeamviewerId.Focus();
                return false;
            }
            else if (txtTVPassword.Text == "")
            {
                MessageBox.Show("Teamviewer password cannot be blank", "Management Panel");
                txtTVPassword.Focus();
                return false;
            }
            //if (Convert.ToInt32(cmbServiceName.SelectedValue) == 0)
            //{
            //    MessageBox.Show("Service name cannot be blank", "Management Panel");
            //    cmbServiceName.Focus();
            //    return false;
            //}
            //else if (dsValidation.Tables[0].Rows.Count > 0)
            //{
            //    MessageBox.Show("User name already used", "Management Panel");
            //    txtUserName.Focus();
            //    return false;
            //}
            if (LastTotalTokens < Convert.ToInt32(txtNoToken.Text))
            {
                if (chkCopyright.Checked == false)
                {
                    MessageBox.Show("Select atleast one subscription", "Management Panel");
                    chkCopyright.Focus();
                    return false;
                }
            }
            if (SaveMode == "New")
            {
                if (chkCopyright.Checked == false)
                {
                    MessageBox.Show("Select atleast one subscription", "Management Panel");
                    chkCopyright.Focus();
                    return false;
                }

            }
            if (Int32.TryParse(txtNoToken.Text, out intValue))
            {
            }
            else
            {
                MessageBox.Show("No of token only in numeric", "Management Panel");
                txtNoToken.Focus();
                return false;
            }
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list", "Management Panel");
                cmbCountryName.Focus();
                return false;
            }
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a state", "Management Panel");
                cmbStateName.Focus();
                return false;
            }
            if (Convert.ToInt32(cmbCityName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a city", "Management Panel");
                cmbCityName.Focus();
                return false;
            }
            return true;

        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            DataSet ds = new DataSet();
            sQr = "select * from DFClients where dfclientId=" + Convert.ToInt32(cmbClientName.SelectedValue);
            ds = objMainClass.fnFillDataSet(sQr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MainClientEmail = ds.Tables[0].Rows[0]["Email"].ToString();
            }

            DataSet dsTotalToken = new DataSet();
            sQr = "select count(*) from AMPlayerTokens where isCopyright=1 and clientId=" + Convert.ToInt32(cmbClientName.SelectedValue);
            dsTotalToken = objMainClass.fnFillDataSet(sQr);

            if (dsTotalToken.Tables[0].Rows[0][0].ToString() != "0")
            {
                lblTotalTokens.Text = "Total no of token(Copyright) : " + dsTotalToken.Tables[0].Rows[0][0].ToString();
                lblTotalTokens.Visible = true;
                if (SaveMode == "New")
                {
                    DataTable dtGetData = new DataTable();
                    sQr = "select AMPlayerTokens.ClientID,  Users.UserID,Users.UserName,Users.UserEmail,Users.NoofToken , isnull(Users.CountryId,0)as CountryId,isnull(Users.StateId,0) as StateId,isnull(Users.CityId,0) as CityId, ";
                    sQr = sQr + " DateTokenExpire,NumberofTitles, PlayerType,Street, Cityname, TeamviewerId , TvPassword  ,Users.Vatnumber,Users.Location,users.Playerversion from AMPlayerTokens ";
                    sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
                    sQr = sQr + " where AMPlayerTokens.isCopyright=1 and AMPlayerTokens.ClientID=" + Convert.ToInt32(cmbClientName.SelectedValue) + "  order by AMPlayerTokens.UserId desc ";
                    dtGetData = objMainClass.fnFillDataTable(sQr);
                    if (dtGetData.Rows.Count > 0)
                    {
                        dtpCopyrightExpiry.Value = DateTime.Now.Date;
                        cmbCountryName.SelectedValue = Convert.ToInt32(dtGetData.Rows[0]["CountryId"]);
                        txtUserName.Text = dtGetData.Rows[0]["UserName"].ToString();
                        txtEmail.Text = dtGetData.Rows[0]["UserEmail"].ToString();
                        cmbNoTitles.Text = dtGetData.Rows[0]["NumberofTitles"].ToString();
                        txtVatnumber.Text = dtGetData.Rows[0]["Vatnumber"].ToString();
                        cmbStateName.SelectedValue = Convert.ToInt32(dtGetData.Rows[0]["StateId"]);
                        txtLocation.Text = "";
                        txtNoToken.Text = "1";
                        cmbPlayerVersion.Text = dtGetData.Rows[0]["Playerversion"].ToString();
                        txtStreet.Text = dtGetData.Rows[0]["Street"].ToString();
                        cmbCityName.SelectedValue = Convert.ToInt32(dtGetData.Rows[0]["CityId"]);
                        txtTeamviewerId.Text = dtGetData.Rows[0]["TeamviewerId"].ToString();
                        txtTVPassword.Text = dtGetData.Rows[0]["TvPassword"].ToString();
                        dtpCopyrightExpiry.Value = dtpCopyrightExpiry.Value.AddYears(1);
                    }
                }
                else
                {

                    lblTotalTokens.Visible = true;
                    txtUserName.Text = "";
                    txtEmail.Text = "";
                    txtLocation.Text = "";
                    txtVatnumber.Text = "";
                    cmbNoTitles.Text = "";
                    cmbPlayerVersion.Text = "";
                    txtNoToken.Text = "";
                    cmbSearchPlayerVersion.Text = "";
                    ModifyUserId = 0;
                    pBar.Value = 0;
                    txtStreet.Text = "";
                    txtTeamviewerId.Text = "";
                    txtTVPassword.Text = "0";
                }
            }
            else 
            {
                lblTotalTokens.Text = "No Tokens ";
                txtUserName.Text = "";
                txtEmail.Text = "";
                txtLocation.Text = "";
                txtVatnumber.Text = "";
                cmbNoTitles.Text = "";
                cmbPlayerVersion.Text = "";
                txtNoToken.Text = "";
                cmbSearchPlayerVersion.Text = "";
                ModifyUserId = 0;
                pBar.Value = 0;
                txtStreet.Text = "";
                txtTeamviewerId.Text = "";
                txtTVPassword.Text = "0";
            }

        }

        private void SendEmail()
        {
            t2 = new Thread(SendEmailUser);
            t2.IsBackground = true;
            t2.Start();
        }

        private void SendEmailUser()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = UserEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Regarding Tokens";
                string body = "Dear Customer ,\n";
                body += "\n";
                body += "This email contains all the information that is required for you to install your Copyright Music Player.\n";
                body += "The installation is required to be done in Chrome. \n";
                body += " \n";
                body += "1. Click on the link and follow the instructions:-\n";

                if (UserPlayerVersion == "Normal")
                {
                    body += "Copyright music player setup for Windows 7,8 or 10: \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/publish.htm \n";
                    body += "Copyright music player setup for windowXp only:- \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright/publish.htm \n";
                }
                if (UserPlayerVersion == "NativeCR")
                {
                    body += "Copyright music player setup for Windows 7,8 or 10:\n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/NativeCR/publish.htm \n";
                    body += "Copyright music player setup for windowXp only:- \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright//NativeCR/publish.htm \n";

                }
                body += "2. Once the installation is started you will need to use your Token & Client name to complete this installation. \n";
                body += "Your installation user name: " + MainUserName + " \n";
                if (dtUserToken.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtUserToken.Rows.Count - 1; i++)
                    {
                        body += "Your installation token no: " + dtUserToken.Rows[i]["Token"].ToString()  + " \n";
                    }
                }
                body += "Your installation dealer code: More000 \n";
                body += "Your Copright subscription expiry date is: " + UserDateExpiry + " \n";
                body += "  \n";
                body += "3.After starting the player there is a username (admin) and pasword (admin) to fill in and tick the box to keep this in memory. \n";
                body += " \n";
                body += "WARNING:  Install the player on the network what you normally use./ This can be LAN or WIFI. \n";
                body += "Switching or changing your network interface will make your player inoperative until it is switched back to the initial settings. \n";
                body += "\n";
                body += "Thank you for using our software. \n";
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
                SendEmail();
               
            }
        }

        private void SendEmailMainClient()
        {
//            t2 = new Thread(SendEmailMainClientData);
  //          t2.IsBackground = true;
    //        t2.Start();
        }

        private void SendEmailMainClientData()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = MainClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Regarding Tokens";
                string body = "Hello " + MainUserName + " \n";
                body += "\n";
                body += "This email contains the information required for you to install Copyright Music Player. \n";
                body += "Please follow the instructions carefully for the best result. \n";
                body += "The installation is required to be done in Chrome. \n";
                body += "1) Click on the link and follow the instructions:-\n";
                body += "\n";
                body += "Copyright music player setup for window7 or upper version:- \n";
                body += "http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/publish.htm \n";
                body += "\n";
                body += "Copyright music player setup for windowXp only:- \n";
                body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright/publish.htm \n";
                body += "\n"; 
                body += "2) Once the installation is started you will need to use Token & Client Name to complete the installation. \n";
                body += "Your installation user name: " + MainUserName + " \n";
                if (dtGetToken.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtGetToken.Rows.Count - 1; i++)
                    {
                        body += "Your installation token no: " + dtGetToken.Rows[i]["Token"].ToString() + " \n";
                    }
                }

                body += "Your Dealer Code: More000 \n";
                body += "Your token no expiry date: " + UserDateExpiry + " \n";
                body += "Your song download limit: " + UserNoOfTitles + " \n";
                body += "Your player type: " + UserPlayerType + " \n";
                body += "3) After starting up the player there is a username (admin) and pasword (admin) to fill in and tick the box to remember. \n";
                body += "WARNING:  Install the player on the network what you normally use LAN or WIFI. \n";
                body += "Switching or changing your network interface will make you player inoperative until it is switched back to the initial settings. \n";
                body += "\n";
                body += "Thanks \n";

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
                SendEmailMainClient();
            }
        }
        private void SendMailAdmin()
        {
            t2 = new Thread(AdminMail);
            t2.IsBackground = true;
            t2.Start();
        }
        private void AdminMail()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = "jan@jaroconsult.com";
              // var toAddress = "talwinder.parastechnologies@gmail.com";
                const string fromPassword = "Claudio@123456";
                string subject = "New Client Register";
                string body = "Dear Administrator, \n";
                body += "\n";
                body += "This is to inform you that a new client is registred on the copyright player and his credentials are: \n";
                body += "The Installation Username:" + MainUserName + " \n";
                if (dtGetToken.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtGetToken.Rows.Count - 1; i++)
                    {
                        body += "The Installation token no: " + dtGetToken.Rows[i]["Token"].ToString() + " \n";
                    }
                }
                body += "The Installation dealer code: More000 \n";

                if (UserPlayerVersion == "Normal")
                {
                    body += "Copyright music player setup for Windows 7,8 or 10: \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/publish.htm \n";
                    body += "Copyright music player setup for windowXp only:- \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright/publish.htm \n";
                }
                if (UserPlayerVersion == "NativeCR")
                {
                    body += "Copyright music player setup for Windows 7,8 or 10: \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/copyright/NativeCR/publish.htm \n";
                    body += "Copyright music player setup for windowXp only:- \n";
                    body += "http://manageyourclaudio.eu/manageyourclaudioSetup/WinXp/copyright//NativeCR/publish.htm \n";

                }
                body += "\n";
                body += "Thank you for using our software \n";
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
                SendMailAdmin();
            }
        }
        private void dgTokenGeneration_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtModify = new DataTable();
            DataTable dtExprie = new DataTable();
            string sQr = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 7)
            {

                sQr = "select distinct AMPlayerTokens.ClientID,  Users.UserID,Users.UserName,Users.UserEmail,Users.NoofToken , isnull(Users.CountryId,0) as CountryId ,isnull(Users.StateId,0) as StateId,isnull(Users.CityId,0) as CityId, ";
                sQr = sQr + " DateTokenExpire,NumberofTitles, PlayerType,Street, Cityname, TeamviewerId , TvPassword  ,Users.Vatnumber,Users.Location,users.Playerversion,AMPlayerTokens.ServiceId from AMPlayerTokens ";
                sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
                sQr = sQr + " where Users.UserID=" + Convert.ToInt32(dgTokenGeneration.Rows[e.RowIndex].Cells[0].Value) + " ";
                dtModify = objMainClass.fnFillDataTable(sQr);
                if (dtModify.Rows.Count > 0)
                {
                    SaveMode = "Modify";
                    cmbClientName.SelectedValue = Convert.ToInt32(dtModify.Rows[0]["ClientID"]);
                    ModifyUserId = Convert.ToInt32(dtModify.Rows[0]["UserID"]);
                    txtUserName.Text = dtModify.Rows[0]["UserName"].ToString();
                    txtEmail.Text = dtModify.Rows[0]["UserEmail"].ToString();
                    cmbNoTitles.Text = dtModify.Rows[0]["NumberofTitles"].ToString();
                    cmbCountryName.SelectedValue = Convert.ToInt32(dtModify.Rows[0]["CountryId"]);
                    //sQr = "select top 1 * from AMPlayerTokens where userid= " + ModifyUserId;
                    //dtExprie = objMainClass.fnFillDataTable(sQr);
                    //dtpCopyrightExpiry.Value = Convert.ToDateTime(dtExprie.Rows[0]["DateTokenExpire"]);
                    txtVatnumber.Text = dtModify.Rows[0]["Vatnumber"].ToString();
                    txtLocation.Text = dtModify.Rows[0]["Location"].ToString();
                    txtNoToken.Text = dtModify.Rows[0]["NoofToken"].ToString();
                    cmbStateName.SelectedValue = Convert.ToInt32(dtModify.Rows[0]["StateId"]);
                    LastTotalTokens = Convert.ToInt32(txtNoToken.Text);
                    cmbPlayerVersion.Text = dtModify.Rows[0]["Playerversion"].ToString();
                    txtStreet.Text = dtModify.Rows[0]["Street"].ToString();
                    cmbCityName.SelectedValue = Convert.ToInt32(dtModify.Rows[0]["CityId"]);
                    txtTeamviewerId.Text = dtModify.Rows[0]["TeamviewerId"].ToString();
                    txtTVPassword.Text = dtModify.Rows[0]["TvPassword"].ToString();
                    
                    cmbClientName.Enabled = false;
                    if (dtModify.Rows[0]["ServiceId"] != System.DBNull.Value)
                    {
                        cmbServiceName.SelectedValue = Convert.ToInt32(dtModify.Rows[0]["ServiceId"]);
                    }
                    //txtNoToken.Enabled = false;
                    //cmbNoTitles.Enabled = false;
                    //chkCopyright.Enabled = false;
                    //chkFitness.Enabled = false;
                }
            }
            else if (e.ColumnIndex == 8)
            {
                result = MessageBox.Show("Are you sure to delete this user ?", "Management Panel", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand("Delete_User", StaticClass.constr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                    cmd.Parameters["@UserId"].Value = Convert.ToInt32(dgTokenGeneration.Rows[e.RowIndex].Cells[0].Value);
                    try
                    {
                        cmd.ExecuteNonQuery();
//                        SendDeleteEmail();
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
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
        }

        private void SendDeleteEmail()
        {
            t2 = new Thread(GetData);
            t2.IsBackground = true;
            t2.Start();
        }

        private void GetData()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = MainClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "!! Account Delete  !!";
                string body = "Hello " + MainUserName + "\n";
                body += "Your user account ( " + UserName + " ) is deleted by admin";
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

        private void cmbSearchClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            if (Convert.ToInt32(cmbSearchClientName.SelectedValue) > 0)
            {
                txtSearchUserName.Text = "";
                cmbSearchPlayerVersion.Text = "";
                sQr = "select distinct   Users.UserID,Users.UserName,Users.UserEmail,Users.NoofToken , ";
                sQr = sQr + " NumberofTitles, PlayerType,isnull(Users.Location,'') as Location from AMPlayerTokens ";
                sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
                sQr = sQr + " inner join DfClients on AMPlayerTokens.ClientId= DfClients.DfClientId";
                sQr = sQr + " where AMPlayerTokens.clientId=" + Convert.ToInt32(cmbSearchClientName.SelectedValue) + "  and isnull(Users.MusicType,'') <>'Copyleft' ";
                sQr = sQr + " and AMPlayerTokens.IsCopyright=1  and DfClients.IsDealer=0 order by Users.UserID desc  ";
                FillTokenGenerationData(sQr);
            }
            else
            {
                FillTokenGenerationData(sQrStr);
            }
        }

        private void txtSearchUserName_KeyDown(object sender, KeyEventArgs e)
        {
            string sQr = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchUserName.Text.Length <= 0)
                {
                    MessageBox.Show("Atleast enter one character for search", "Management Panel");
                    return;
                }
                if (txtSearchUserName.Text.Length >= 1)
                {
                    cmbSearchClientName.Text = "";
                    cmbSearchPlayerVersion.Text = "";
                    sQr = "select distinct   Users.UserID,Users.UserName,Users.UserEmail,Users.NoofToken , ";
                    sQr = sQr + " NumberofTitles, PlayerType,isnull(Users.Location,'') as Location from AMPlayerTokens ";
                    sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
                    sQr = sQr + " inner join DfClients on AMPlayerTokens.ClientId= DfClients.DfClientId";
                    sQr = sQr + " where Users.UserName like '%" + txtSearchUserName.Text + "%'  and isnull(Users.MusicType,'') <>'Copyleft' ";
                    sQr = sQr + " and AMPlayerTokens.IsCopyright=1  and DfClients.IsDealer=0 and DfClients.IsDealerClient is null order by Users.UserID desc  ";
                    FillTokenGenerationData(sQr);
                }
                else
                {
                    FillTokenGenerationData(sQrStr);
                }


            }
        }

       

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtStreet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCityname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void dgTokenGeneration_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void cmbClientName_Click(object sender, EventArgs e)
        {
            FillClient();
        }

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                cmbStateName.DataSource = null;
                cmbStateName.Refresh();
                return;
            }
            string strState = "";
            strState = "select * from tbState where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue);
            objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");
        }

        private void cmbStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                cmbCityName.DataSource = null;
                cmbCityName.Refresh();
                return; 
            }
            string strCity = "";
            strCity = "select * from tbCity where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " and stateid =" + Convert.ToInt32(cmbStateName.SelectedValue);
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
            txtName.Focus();
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.BringToFront();
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            lblCaption.Text = "State Name";
            txtName.Focus();
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
            txtName.Focus();
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.BringToFront();
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            lblCaption.Text = "City Name";
            txtName.Focus();
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

        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            panMainNew.Visible = false;
            txtName.Text = "";
        }

        private void txtSearchUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSearchPlayerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            if (cmbSearchPlayerVersion.Text != "")
            {
                cmbSearchClientName.Text = "";
                txtSearchUserName.Text = "";
                sQr = "select distinct   Users.UserID,Users.UserName,Users.UserEmail,Users.NoofToken , ";
                sQr = sQr + " NumberofTitles, PlayerType ,isnull(Users.Location,'') as Location from AMPlayerTokens ";
                sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
                sQr = sQr + " inner join DfClients on AMPlayerTokens.ClientId= DfClients.DfClientId";
                sQr = sQr + " where Playerversion='" + cmbSearchPlayerVersion.Text + "' and  PlayerType='Desktop'  and isnull(Users.MusicType,'') <>'Copyleft'";
                sQr = sQr + " and AMPlayerTokens.IsCopyright=1  and DfClients.IsDealer=0  and DfClients.IsDealerClient is null order by Users.UserID desc  ";
                FillTokenGenerationData(sQr);
            }
            else
            {
                FillTokenGenerationData(sQrStr);
            }
        }

       

    }
}
