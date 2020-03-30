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
    public partial class frmRegistration : Form
    {
        gblClass objMainClass = new gblClass();
        Thread t2;
        Int32 DfClientId = 0;
        string ClientEmail = "";
        string ClientName = "";
        string mAction = "Save";
        string strMain = "";
        string strDealerName = "";
        Int32 TotalToken = 0;
        DateTime ExpiryDate;
        string LoginName = "";
        string LoginPassword = "";
        string DealerCode = "";
        Int32 DamTotalToken = 0;
        Int32 CopyrightTotalToken = 0;
        Int32 SanjivaniTotalToken = 0;

        public frmRegistration()
        {
            InitializeComponent();
            InitilizeRegistrationGrid();
            FillCountry();
            strMain = "select DFClientID,CountryCodes.CountryName, ClientName,isnull(Email,'') as email,orderno ";
            strMain = strMain + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
            strMain = strMain + " where DFClients.IsDealer=0 and IsDealerclient is null order by DFClientID desc";
            FillRegistrationData(strMain);
        }
        //private frmMain mainForm = null;
        //public frmRegistration(Form callingForm)
        //{
        //    mainForm = callingForm as frmMain;
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
            Application.Exit();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {


        }
        private void FillCountry()
        {
            string str = "";
            str = "select * from CountryCodes order by countryCode";
            objMainClass.fnFillComboBox(str, cmbSearchCountryName, "countrycode", "countryName", "");
            objMainClass.fnFillComboBox(str, cmbCountryName, "countrycode", "countryName", "");
        }

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lStr = "";
            string sQr = "";
            Int32 OrderId = 0;
            DataSet dsOrder = new DataSet();
            DataSet ds = new DataSet();
            lStr = "select * from CountryCodes where countryCode=" + Convert.ToInt32(cmbCountryName.SelectedValue);

            sQr = "select isnull(max(srno),0)+1 from DFClients where year(RegistrationDate)=" + DateTime.Now.Year;
            dsOrder = objMainClass.fnFillDataSet(sQr);
            ds = objMainClass.fnFillDataSet(lStr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                OrderId = Convert.ToInt32(dsOrder.Tables[0].Rows[0][0]);
                txtUserName.Text = ds.Tables[0].Rows[0]["CountryNameShort"].ToString() + "-";
                txtOrderNo.Text = "A-" + DateTime.Now.Year + "-" + OrderId;
            }
        }
        private void FillRegistrationData(string sQr)
        {
            DataTable dtDetail = new DataTable();
            InitilizeRegistrationGrid();

            dtDetail = objMainClass.fnFillDataTable(sQr);

            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgRegistration.Rows.Add();
                    dgRegistration.Rows[dgRegistration.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[i]["DFClientID"];
                    dgRegistration.Rows[dgRegistration.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[i]["CountryName"];
                    dgRegistration.Rows[dgRegistration.Rows.Count - 1].Cells[2].Value = dtDetail.Rows[i]["ClientName"];
                    dgRegistration.Rows[dgRegistration.Rows.Count - 1].Cells[3].Value = dtDetail.Rows[i]["email"];
                    dgRegistration.Rows[dgRegistration.Rows.Count - 1].Cells[4].Value = dtDetail.Rows[i]["orderno"];

                }
            }
            foreach (DataGridViewRow row in dgRegistration.Rows)
            {
                row.Height = 30;
            }

        }
        private void InitilizeRegistrationGrid()
        {
            if (dgRegistration.Rows.Count > 0)
            {
                dgRegistration.Rows.Clear();
            }
            if (dgRegistration.Columns.Count > 0)
            {
                dgRegistration.Columns.Clear();
            }

            dgRegistration.Columns.Add("ClientId", "ClientId");
            dgRegistration.Columns["ClientId"].Width = 0;
            dgRegistration.Columns["ClientId"].Visible = false;
            dgRegistration.Columns["ClientId"].ReadOnly = true;

            dgRegistration.Columns.Add("Countryname", "Country Name");
            dgRegistration.Columns["Countryname"].Width = 200;
            dgRegistration.Columns["Countryname"].Visible = true;
            dgRegistration.Columns["Countryname"].ReadOnly = true;
            dgRegistration.Columns["Countryname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgRegistration.Columns.Add("Clientname", "Client Name");
            dgRegistration.Columns["Clientname"].Width = 300;
            dgRegistration.Columns["Clientname"].Visible = true;
            dgRegistration.Columns["Clientname"].ReadOnly = true;
            dgRegistration.Columns["Clientname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgRegistration.Columns.Add("email", "E-mail");
            dgRegistration.Columns["email"].Width = 350;
            dgRegistration.Columns["email"].Visible = true;
            dgRegistration.Columns["email"].ReadOnly = true;
            dgRegistration.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgRegistration.Columns.Add("OrderNo", "Order No");
            dgRegistration.Columns["OrderNo"].Width = 120;
            dgRegistration.Columns["OrderNo"].Visible = true;
            dgRegistration.Columns["OrderNo"].ReadOnly = true;
            dgRegistration.Columns["OrderNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            dgRegistration.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            dgRegistration.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn TransferClient = new DataGridViewLinkColumn();
            TransferClient.HeaderText = "Move";
            TransferClient.Text = "Move to";
            TransferClient.DataPropertyName = "MoveDealer";
            TransferClient.LinkColor = Color.FromArgb(64, 64, 64);
            TransferClient.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
            TransferClient.LinkBehavior = LinkBehavior.SystemDefault;
            dgRegistration.Columns.Add(TransferClient);
            TransferClient.UseColumnTextForLinkValue = true;
            TransferClient.Width = 70;
            dgRegistration.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteClient = new DataGridViewLinkColumn();
            DeleteClient.HeaderText = "Delete";
            DeleteClient.Text = "Delete";
            DeleteClient.DataPropertyName = "Delete";
            DeleteClient.LinkColor = Color.FromArgb(64, 64, 64);
            DeleteClient.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
            DeleteClient.LinkBehavior = LinkBehavior.SystemDefault;
            dgRegistration.Columns.Add(DeleteClient);
            DeleteClient.UseColumnTextForLinkValue = true;
            DeleteClient.Width = 70;
            dgRegistration.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            mAction = "Save";
            FillCountry();
            FillRegistrationData(strMain);
            txtOrderNo.Text = "";
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtCityName.Text = "";
            txtStreet.Text = "";
            txtVatnumber.Text = "";
            cmbCountryName.Focus();
        }
        private Boolean SubmitValidation()
        {
            string sTr = "";
            DataSet dsEmail = new DataSet();
            DataSet dsUser = new DataSet();
            if (mAction == "Save")
            {
                sTr = "select * from DFClients where email='" + txtEmail.Text + "'";
            }
            else
            {
                sTr = "select * from DFClients where email='" + txtEmail.Text + "' and dfclientid <>" + DfClientId;
            }
            dsEmail = objMainClass.fnFillDataSet(sTr);

            sTr = "";
            if (mAction == "Save")
            {

                sTr = "select * from DFClients where ClientName='" + txtUserName.Text + "'";
            }
            else
            {
                sTr = "select * from DFClients where ClientName='" + txtUserName.Text + "' and dfclientid <>" + DfClientId;
            }
            dsUser = objMainClass.fnFillDataSet(sTr);

            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list", "Management Panel");
                cmbCountryName.Focus();
                return false;
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("User name cannot be blank", "Management Panel");
                txtUserName.Focus();
                return false;
            }
            else if (txtStreet.Text == "")
            {
                MessageBox.Show("Street name cannot be blank", "Management Panel");
                txtStreet.Focus();
                return false;
            }
            else if (txtStreet.TextLength <= 4)
            {
                MessageBox.Show("Enter a correct street name", "Management Panel");
                txtStreet.Focus();
                return false;
            }
            else if (txtCityName.Text == "")
            {
                MessageBox.Show("City name cannot be blank", "Management Panel");
                txtCityName.Focus();
                return false;
            }
            else if (dsUser.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("User name already exists", "Management Panel");
                txtUserName.Focus();
                return false;
            }
            else if (txtVatnumber.Text == "")
            {
                MessageBox.Show("Vat nummber cannot be blank", "Management Panel");
                txtVatnumber.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
            ClientName = txtUserName.Text;
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
            SqlCommand cmd = new SqlCommand("sp_ClientRegistration_Modify", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DFClientID", SqlDbType.BigInt));
            cmd.Parameters["@DFClientID"].Value = DfClientId;

            cmd.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.VarChar));
            cmd.Parameters["@ClientName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters["@email"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@ResponsiblePersonName", SqlDbType.VarChar));
            cmd.Parameters["@ResponsiblePersonName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.BigInt));
            cmd.Parameters["@CountryCode"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.VarChar));
            cmd.Parameters["@StreetName"].Value = txtStreet.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
            cmd.Parameters["@CityName"].Value = txtCityName.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@DealerNoTotalToken", SqlDbType.Int));
            cmd.Parameters["@DealerNoTotalToken"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;
            try
            {
                cmd.ExecuteNonQuery();
                SendEmail();
                // MessageBox.Show("Record Modify", "Management Panel");
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
        private void SaveRegistration()
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_ClientRegistration", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@InClientName", SqlDbType.VarChar));
            cmd.Parameters["@InClientName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
            cmd.Parameters["@email"].Value = txtEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@OrderNo", SqlDbType.VarChar));
            cmd.Parameters["@Orderno"].Value = txtOrderNo.Text;

            cmd.Parameters.Add(new SqlParameter("@ResponsiblePersonName", SqlDbType.VarChar));
            cmd.Parameters["@ResponsiblePersonName"].Value = txtUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.BigInt));
            cmd.Parameters["@CountryCode"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.VarChar));
            cmd.Parameters["@StreetName"].Value = txtStreet.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
            cmd.Parameters["@CityName"].Value = txtCityName.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@IsDealer", SqlDbType.Bit));
            cmd.Parameters["@IsDealer"].Value = 0;

            cmd.Parameters.Add(new SqlParameter("@DealerNoTotalToken", SqlDbType.Int));
            cmd.Parameters["@DealerNoTotalToken"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = "0";

            cmd.Parameters.Add(new SqlParameter("@Vatnumber", SqlDbType.VarChar));
            cmd.Parameters["@Vatnumber"].Value = txtVatnumber.Text;
            try
            {
                Int32 ReturnId = 0;

                ReturnId = Convert.ToInt32(cmd.ExecuteScalar());
                DataSet dsOrder = new DataSet();
                string sQr = "select isnull(max(srno),0)+1 from DFClients where year(RegistrationDate)=" + DateTime.Now.Year;
                dsOrder = objMainClass.fnFillDataSet(sQr);
                string strDel = "";
                strDel = "update DFClients set srno="+ Convert.ToInt32(dsOrder.Tables[0].Rows[0][0])+ " where dfclientid= " + ReturnId;
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd = new SqlCommand(strDel, StaticClass.constr);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                SendEmail();
                // MessageBox.Show("Record Saved", "Management Panel");
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

        private void SendEmail()
        {
            //if (mAction == "Save")
            //{
            //    t2 = new Thread(GetData);
            //}
            //else if (mAction == "Modify")
            //{
            //    t2 = new Thread(GetDataModify);
            //}
            //t2.IsBackground = true;
            //t2.Start();
        }

        private void GetData()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Welcome";
                string body = "Hello " + ClientName + "\n";
                body += "You are regsiter with manageyourclaudio \n";
                body += "\n";
                body += "Team\n";
                body += "Manageyourclaudio";
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

        private void GetDataModify()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Notification";
                string body = "Hello " + ClientName + "\n";
                body += "You record is update by admin \n";
                body += "\n";
                body += "Manageyourclaudio";
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

        private void frmRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SubmitValidation() == false)
                {
                    return;
                }
                if (mAction == "Save")
                {
                    SaveRegistration();
                }
                else if (mAction == "Modify")
                {
                    ModifyRegistration();
                }
            }
        }

        private void dgRegistration_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DfClientId = 0;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 5)
            {
                DataTable dtDetail = new DataTable();
                string sQr = "";
                sQr = "select DFClientID,CountryCode , ClientName,isnull(Email,'') as email,orderno,cityname,streetname,vatnumber ";
                sQr = sQr + " from DFClients ";
                sQr = sQr + " where DFClientID=" + dgRegistration.Rows[e.RowIndex].Cells[0].Value;
                dtDetail = objMainClass.fnFillDataTable(sQr);
                if (dtDetail.Rows.Count > 0)
                {
                    mAction = "Modify";
                    DfClientId = Convert.ToInt32(dtDetail.Rows[0]["DFClientID"]);
                    cmbCountryName.SelectedValue = dtDetail.Rows[0]["CountryCode"];
                    txtUserName.Text = dtDetail.Rows[0]["ClientName"].ToString();
                    txtEmail.Text = dtDetail.Rows[0]["email"].ToString();
                    txtOrderNo.Text = dtDetail.Rows[0]["orderno"].ToString();
                    txtStreet.Text = dtDetail.Rows[0]["streetname"].ToString();
                    txtCityName.Text = dtDetail.Rows[0]["cityname"].ToString();
                    txtVatnumber.Text = dtDetail.Rows[0]["vatnumber"].ToString();

                }
            }
            if (e.ColumnIndex == 6)
            {
                panMoveOption.Location = new Point(
          this.Width / 2 - panMoveOption.Size.Width / 2,
          this.Height / 2 - panMoveOption.Size.Height / 2);

                panMoveOption.Visible = true;
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                panel4.Enabled = false;
            }
            if (e.ColumnIndex == 7)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    strDel = "delete from UserDownloadTitle where dfclientid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tblUser_Client_Rights where userid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) ;
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tblmusic_player_settings where dfclientid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from TitlesInPlaylists where Playlistid in( " ;
                    strDel = strDel + "select distinct Playlistid from playlists where userid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) + ")";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel =  "delete from playlists where userid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from users where clientid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from AMPlayerTokens where clientid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from DFClients where dfclientid = " + Convert.ToInt32(dgRegistration.Rows[e.RowIndex].Cells[0].Value) + " ";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    FillRegistrationData(strMain);
                }
                
                return;
            }

        }

        private void cmbSearchCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            if (Convert.ToInt32(cmbSearchCountryName.SelectedValue) > 0)
            {
                sQr = "select DFClientID,CountryCodes.CountryName, ClientName,isnull(Email,'') as email,orderno ";
                sQr = sQr + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
                sQr = sQr + " where DFClients.IsDealer=0 and IsDealerclient is null and ";
                sQr = sQr + " CountryCodes.CountryCode = " + Convert.ToInt32(cmbSearchCountryName.SelectedValue) + " order by DFClientID desc";
                FillRegistrationData(sQr);
            }
            else
            {
                FillRegistrationData(strMain);
            }
        }

        private void txtSearchClientName_KeyDown(object sender, KeyEventArgs e)
        {
            string sQr = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearchClientName.Text.Length <= 0)
                {
                    MessageBox.Show("Atleast enter one character for search", "Management Panel");
                    return;
                }
                if (txtSearchClientName.Text.Length >= 1)
                {
                    sQr = "select DFClientID,CountryCodes.CountryName, ClientName,isnull(Email,'') as email,orderno ";
                    sQr = sQr + " from DFClients inner join CountryCodes on DFClients.CountryCode= CountryCodes.CountryCode ";
                    sQr = sQr + " where DFClients.IsDealer=0 and IsDealerclient is null and ";
                    sQr = sQr + " ClientName like '%" + txtSearchClientName.Text + "%' order by DFClientID desc";
                    FillRegistrationData(sQr);
                }
                else
                {
                    FillRegistrationData(strMain);
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

        private void txtCityName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panDealer.Visible = false;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel4.Enabled = true;
        }

        private void rdoSubDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSubDealer.Checked == true)
            {
                lblMainDealer.Enabled = true;
                cmbMainDealer.Enabled = true;
                string str = "";
                str = "select DFClientID,ClientName from DFClients where CountryCode =" + Convert.ToInt32(lblDealerCountryId.Text) + " and DFClients.IsDealer=1 order by DFClientID desc";
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void lblDealerCity_Click(object sender, EventArgs e)
        {

        }

        private void lblDealerCountry_Click(object sender, EventArgs e)
        {

        }

        private void lblDealerCode_Click(object sender, EventArgs e)
        {

        }

        private void lblMainDealer_Click(object sender, EventArgs e)
        {

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
                    cmd.Parameters["@CountryId"].Value = Convert.ToInt32(lblDealerCountryId.Text);

                    cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.VarChar));
                    cmd.Parameters["@StateName"].Value = txtName.Text;
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    returnValue = cmd.ExecuteScalar().ToString();
                    if (returnValue != "-2")
                    {
                        strState = "select * from tbState where countryId= " + Convert.ToInt32(lblDealerCountryId.Text);
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
        }

        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            panMainNew.Visible = false;
            txtName.Text = "";
        }

        private void btnStateNew_Click(object sender, EventArgs e)
        {
            if (lblDealerCountryId.Text == "")
            {
                MessageBox.Show("Select a Country from the list name", "Management Panel");
                return;
            }
            txtName.Text = "";
            panMainNew.Dock = DockStyle.Fill;
            panMainNew.Visible = true;
            lblCaption.Text = "State Name";
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a state name", "Management Panel");
                cmbStateName.Focus();
                return;
            }
            if (rdoSubDealer.Checked == true)
            {
                if (Convert.ToInt32(cmbMainDealer.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select a main dealer name", "Management Panel");
                    cmbMainDealer.Focus();
                    return;
                }
            }
            #region "SaveCity"
            DataSet dsCity = new DataSet();
            string strDealerCityId = "";
            string lStr = "select * from tbcity where cityname='" + txtDealerCity.Text.Trim() + "'";
            dsCity = objMainClass.fnFillDataSet(lStr);
            if (dsCity.Tables[0].Rows.Count > 0)
            {
                strDealerCityId = dsCity.Tables[0].Rows[0]["CityId"].ToString();
            }
            else
            {
                string returnValue = "";
                SqlCommand cmd = new SqlCommand("SaveCity", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
                cmd.Parameters["@CountryId"].Value = Convert.ToInt32(lblDealerCountryId.Text);

                cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
                cmd.Parameters["@StateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.VarChar));
                cmd.Parameters["@CityName"].Value = txtDealerCity.Text;

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                returnValue = cmd.ExecuteScalar().ToString();
                if (returnValue != "-2")
                {
                    strDealerCityId = returnValue;
                }
            }
            #endregion

            #region "MoveToDealer"
            lStr = "";
            lStr = "select count(*) totalToken from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text);
            dsCity = objMainClass.fnFillDataSet(lStr);

            #region "Update Dfclient Data"
            string str = "";
            str = "update DFClients set IsDealer=1,  IsDealerClient =1, Dealerdfclientid=" + Convert.ToInt32(lblDfClientId.Text) + ",  DealerCode='" + txtDealerCode.Text + "' , CityId = " + Convert.ToInt32(strDealerCityId) + ", ";
            str = str + " Stateid =" + Convert.ToInt32(cmbStateName.SelectedValue) + ", isMainDealer= " + Convert.ToByte(rdoMainDealer.Checked) + ", ";
            str = str + " IsSubDealer= " + Convert.ToByte(rdoSubDealer.Checked) + " , MainDealerId =" + Convert.ToInt32(cmbMainDealer.SelectedValue) + " , ";
            str = str + " DealerNoTotalToken= " + Convert.ToInt32(dsCity.Tables[0].Rows[0]["totalToken"]) + " where dfclientid=" + Convert.ToInt32(lblDfClientId.Text);
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = StaticClass.constr;
            cmdUpdate.CommandText = str;
            cmdUpdate.ExecuteNonQuery();
            StaticClass.constr.Close();
            #endregion

            #region "Update Token Data"
            str = "";
            str = "update AMPlayerTokens set DealerCode='" + txtDealerCode.Text + "' ";
            str = str + " where clientid=" + Convert.ToInt32(lblDfClientId.Text);
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmdTokenUpdate = new SqlCommand();
            cmdTokenUpdate.Connection = StaticClass.constr;
            cmdTokenUpdate.CommandText = str;
            cmdTokenUpdate.ExecuteNonQuery();
            StaticClass.constr.Close();
            #endregion

            SaveDealerLogin();
            #endregion
        }
        private void SaveDealerLogin()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_DealerLogin", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SaveType", SqlDbType.VarChar));
            cmd.Parameters["@SaveType"].Value = "Save";

            cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt));
            cmd.Parameters["@LoginId"].Value = 0;

            cmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar));
            cmd.Parameters["@LoginName"].Value = lblDealerEmail.Text;

            cmd.Parameters.Add(new SqlParameter("@DfClientId", SqlDbType.BigInt));
            cmd.Parameters["@DfClientId"].Value = lblDfClientId.Text;

            cmd.Parameters.Add(new SqlParameter("@LoginPassword", SqlDbType.VarChar));
            cmd.Parameters["@LoginPassword"].Value = "CLaudio!@#" + lblDfClientId.Text + "player";

            cmd.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime));
            cmd.Parameters["@ExpiryDate"].Value = dtpExpiryDate.Value;

            cmd.Parameters.Add(new SqlParameter("@DealerCode", SqlDbType.VarChar));
            cmd.Parameters["@DealerCode"].Value = txtDealerCode.Text;


            string lStr = "";
            lStr = "select count(*)  as Totalt  from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and isdam =1  ";
            DataSet dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);

            cmd.Parameters.Add(new SqlParameter("@DamTotalToken", SqlDbType.Int));
            cmd.Parameters["@DamTotalToken"].Value = Convert.ToInt32(dsDT.Tables[0].Rows[0]["Totalt"]);

            lStr = "";
            lStr = "select count(*)  as Totalt  from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and iscopyright =1  ";
            dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);
            cmd.Parameters.Add(new SqlParameter("@CopyrightTotalToken", SqlDbType.Int));
            cmd.Parameters["@CopyrightTotalToken"].Value = Convert.ToInt32(dsDT.Tables[0].Rows[0]["Totalt"]);

            lStr = "";
            lStr = "select count(*)  as Totalt  from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and isSanjivani =1  ";
            dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);
            cmd.Parameters.Add(new SqlParameter("@SanjivaniTotalToken", SqlDbType.Int));
            cmd.Parameters["@SanjivaniTotalToken"].Value = Convert.ToInt32(dsDT.Tables[0].Rows[0]["Totalt"]);

            lStr = "";
            lStr = "select distinct isdam from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and isdam !=0  ";
            dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);
            cmd.Parameters.Add(new SqlParameter("@IsDam", SqlDbType.Bit));
            if (dsDT.Tables[0].Rows.Count > 0)
            {
                cmd.Parameters["@IsDam"].Value = Convert.ToBoolean(1);
            }
            else
            {
                cmd.Parameters["@IsDam"].Value = Convert.ToBoolean(0);
            }
            lStr = "select distinct isCopyright from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and isCopyright !=0  ";
            dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);

            cmd.Parameters.Add(new SqlParameter("@IsCopyright", SqlDbType.Bit));
            if (dsDT.Tables[0].Rows.Count > 0)
            {
                cmd.Parameters["@IsCopyright"].Value = Convert.ToBoolean(1);
            }
            else
            {
                cmd.Parameters["@IsCopyright"].Value = Convert.ToBoolean(0);
            }
            lStr = "select distinct isSanjivani from AMPlayerTokens where clientid=" + Convert.ToInt32(lblDfClientId.Text) + " and isSanjivani !=0  ";
            dsDT = new DataSet();
            dsDT = objMainClass.fnFillDataSet(lStr);
            cmd.Parameters.Add(new SqlParameter("@IsSanjivani", SqlDbType.Bit));
            if (dsDT.Tables[0].Rows.Count > 0)
            {
                cmd.Parameters["@IsSanjivani"].Value = Convert.ToBoolean(1);
            }
            else
            {
                cmd.Parameters["@IsSanjivani"].Value = Convert.ToBoolean(0);
            }

            try
            {
                cmd.ExecuteNonQuery();


                string strResend = "";
                DataTable dtSendMail = new DataTable();
                strResend = "  select DFClients.*, tbDealerLogin.LoginPassword,tbDealerLogin.ExpiryDate, tbdealerlogin.DamTotalToken,tbdealerlogin.CopyrightTotalToken,tbdealerlogin.SanjivaniTotalToken ";
                strResend = strResend + "  from DFClients inner join tbDealerLogin on DFClients.DFClientID= tbDealerLogin.DFClientID ";
                strResend = strResend + " where DFClients.IsDealer=1 and DFClients.DFClientID = " + Convert.ToInt32(lblDfClientId.Text);
                dtSendMail = objMainClass.fnFillDataTable(strResend);
                if (dtSendMail.Rows.Count > 0)
                {
                    TotalToken = 0;
                    ClientEmail = "";
                    ClientName = "";
                    LoginName = "";
                    LoginPassword = "";
                    DealerCode = "";
                    DamTotalToken = 0;
                    CopyrightTotalToken = 0;
                    SanjivaniTotalToken = 0;

                    ClientEmail = dtSendMail.Rows[0]["Email"].ToString();
                    TotalToken = Convert.ToInt32(dtSendMail.Rows[0]["DealerNoTotalToken"]);
                    ExpiryDate = Convert.ToDateTime(dtSendMail.Rows[0]["ExpiryDate"]);
                    ClientName = dtSendMail.Rows[0]["ClientName"].ToString();
                    LoginName = dtSendMail.Rows[0]["Email"].ToString();
                    LoginPassword = dtSendMail.Rows[0]["LoginPassword"].ToString();
                    DealerCode = dtSendMail.Rows[0]["DealerCode"].ToString();

                    DamTotalToken = Convert.ToInt32(dtSendMail.Rows[0]["DamTotalToken"]);
                    CopyrightTotalToken = Convert.ToInt32(dtSendMail.Rows[0]["CopyrightTotalToken"]);
                    SanjivaniTotalToken = Convert.ToInt32(dtSendMail.Rows[0]["SanjivaniTotalToken"]);
                }
                SendEmailNewDealer();
                SendMailAdmin();


                panDealer.Visible = false;
                panel1.Enabled = true;
                panel2.Enabled = true;
                panel3.Enabled = true;
                panel4.Enabled = true;
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void SendEmailNewDealer()
        {
            //t2 = new Thread(SendMailDealer);
            //t2.IsBackground = true;
            //t2.Start();
        }

        private void SendMailDealer()
        {
            try
            {
                var fromAddress = "noreply@manageyourclaudio.eu";
                var toAddress = ClientEmail;
                const string fromPassword = "Claudio@123456";
                string subject = "Welcome";
                string body = "Dear Customer, \n";
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
                MessageBox.Show(ex.Message);
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
                string body = "Hello Admin, \n";
                body += "\n";
                body += "This is to you inform , mail is sent to the dealer ";
                if (DamTotalToken != 0)
                {
                    body += "and Copyleft(DAM) ";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "and Copyright ";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "and Copyleft(Sanjivani) ";
                }
                body += "music services has been activated on dealer account. \n";
                body += "\n";
                body += "Dealer credentials are: \n";
                body += "Main Username: " + ClientName + "\n";
                body += "Email: " + ClientEmail + "\n";

                if (DamTotalToken != 0)
                {
                    body += "Copyleft player token: " + DamTotalToken + " \n";
                }
                if (CopyrightTotalToken != 0)
                {

                    body += "Copyright player token: " + CopyrightTotalToken + " \n";
                }
                if (SanjivaniTotalToken != 0)
                {
                    body += "Sanjivani player token: " + SanjivaniTotalToken + "\n";
                }

                body += "Total token: " + TotalToken + "\n";
                body += " \n";
                body += "Login credentials are:- \n";
                body += "User name:- " + LoginName + "   \n";
                body += "Password:- " + LoginPassword + " \n";
                body += "Dealer Code:- " + DealerCode + " \n";
                body += "Click on the link to download the setup file \n";
                body += "http://manageyourclaudio.eu/manageyourclaudioSetup/TokenLicenseHolder.msi \n";
                body += " \n";
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
                SendMailAdmin();
            }
        }

        private void btnNewDealer_Click(object sender, EventArgs e)
        {
            DataTable dtDetail = new DataTable();
            string sQr = "";
            sQr = "select DFClientID,CountryCode , ClientName,isnull(Email,'') as email,orderno,cityname,streetname,vatnumber ";
            sQr = sQr + " from DFClients ";
            sQr = sQr + " where DFClientID=" + dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[0].Value;
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                lblDfClientId.Text = Convert.ToString(dtDetail.Rows[0]["DFClientID"]);
                lblDealerCountryId.Text = Convert.ToString(dtDetail.Rows[0]["CountryCode"]);
                string strCountry = "";
                DataTable dtCountry = new DataTable();
                strCountry = "select * from CountryCodes where countrycode= " + Convert.ToInt32(lblDealerCountryId.Text);
                dtCountry = objMainClass.fnFillDataTable(strCountry);
                if (dtCountry.Rows.Count > 0)
                {
                    lblDealerCountryCode.Text = Convert.ToString(dtCountry.Rows[0]["CountryNameShort"]);
                    txtDealerCountry.Text = Convert.ToString(dtCountry.Rows[0]["CountryName"]);
                }

                string strState = "";
                strState = "select * from tbState where countryId= " + Convert.ToInt32(lblDealerCountryId.Text);
                objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");

                txtDealerName.Text = dtDetail.Rows[0]["ClientName"].ToString();
                txtDealerCity.Text = dtDetail.Rows[0]["cityname"].ToString();
                lblDealerEmail.Text = dtDetail.Rows[0]["email"].ToString();




                txtDealerCode.Text = "";
                string strDealer = txtDealerName.Text;
                string strDealerCity = txtDealerCity.Text;
                string intDealerCodeId = "";
                DataSet ds = new DataSet();
                sQr = "";
                sQr = "select count(DfclientId)+1 as MaxId from DFClients where  IsDealer=1 ";
                ds = objMainClass.fnFillDataSet(sQr);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    intDealerCodeId = "00" + ds.Tables[0].Rows[0]["MaxId"].ToString();
                }
                if (strDealer.Length >= 5)
                {
                    strDealerName = lblDealerCountryCode.Text + "" + strDealer.Substring(3, 2);
                    txtDealerCode.Text = strDealerName.ToUpper();
                }
                if (strDealerCity.Length >= 2)
                {
                    txtDealerCode.Text = strDealerName.ToUpper() + strDealerCity.Substring(0, 2).ToUpper() + intDealerCodeId;
                }

            }

            panDealer.Location = new Point(
         this.Width / 2 - panDealer.Size.Width / 2,
         this.Height / 2 - panDealer.Size.Height / 2);

            panDealer.Visible = true;
            panMoveOption.Visible = false;
        }

        private void picMoveCancel_Click(object sender, EventArgs e)
        {
            panMoveOption.Visible = false;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel4.Enabled = true;
        }

        private void btnDealerClient_Click(object sender, EventArgs e)
        {
            DataTable dtDetail = new DataTable();
            string sQr = "";
            sQr = "   select isnull(sum(d),0) as d, isnull(sum(c),0) as c, isnull(sum(s),0) as s from (";
            sQr = sQr + "  select convert(numeric(18,0), isdam) as d, convert(numeric(18,0), iscopyright) as c,convert(numeric(18,0), isSanjivani) as s ";
            sQr = sQr + "  from AMPlayerTokens where clientid= " + dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[0].Value;
            sQr = sQr + "  ) as a";

            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                lblCopyleftToken.Text = Convert.ToString(dtDetail.Rows[0]["d"]);
                lblCopyrightToken.Text = Convert.ToString(dtDetail.Rows[0]["c"]);
                lblSanjivaniToken.Text = Convert.ToString(dtDetail.Rows[0]["s"]);
                lblTotalTokens.Text = Convert.ToString(Convert.ToInt32(dtDetail.Rows[0]["d"]) + Convert.ToInt32(dtDetail.Rows[0]["c"]) + Convert.ToInt32(dtDetail.Rows[0]["s"]));
            }
            else
            {
                lblCopyleftToken.Text = "";
                lblCopyrightToken.Text = "";
                lblSanjivaniToken.Text = "";
                lblTotalTokens.Text = "";
            }

            txtClientName.Text = Convert.ToString(dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[2].Value);
            txtClientEmail.Text = Convert.ToString(dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[3].Value);

            string str = "";
            //str = "select * from CountryCodes order by countryCode";
            //objMainClass.fnFillComboBox(str, cmbDealerCountry, "countrycode", "countryName", "");

            str = "";
            str = "select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 order by DFClientID desc";
            objMainClass.fnFillComboBox(str, cmbDealer, "DFClientID", "ClientName", "");

            panClient.Location = new Point(
         this.Width / 2 - panClient.Size.Width / 2,
         this.Height / 2 - panClient.Size.Height / 2);

            panClient.Visible = true;
            panMoveOption.Visible = false;

        }

        private void btnClientCancel_Click(object sender, EventArgs e)
        {
            panClient.Visible = false;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel4.Enabled = true;
        }

        private void btnClientMove_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbDealer.SelectedValue) == 0)
            {
                MessageBox.Show("Select a dealer name", "Registration");
                cmbDealer.Focus();
                return;
            }
            DataTable dtDetail = new DataTable();
            string sQr = "";
            Int32 dToken = 0;
            Int32 cToken = 0;
            Int32 sToken = 0;
            sQr = "   select * from tbdealerlogin where dfclientid= " + Convert.ToInt32(cmbDealer.SelectedValue);
            dtDetail = objMainClass.fnFillDataTable(sQr);

            dToken = Convert.ToInt32(dtDetail.Rows[0]["DamTotalToken"]) + Convert.ToInt32(lblCopyleftToken.Text);
            cToken = Convert.ToInt32(dtDetail.Rows[0]["CopyrightTotalToken"]) + Convert.ToInt32(lblCopyrightToken.Text);
            sToken = Convert.ToInt32(dtDetail.Rows[0]["SanjivaniTotalToken"]) + Convert.ToInt32(lblSanjivaniToken.Text);

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = "update DFClients set dealerdfclientid= " + Convert.ToInt32(cmbDealer.SelectedValue) + ", Isdealerclient=1, Dealercode='" + Convert.ToString(dtDetail.Rows[0]["DealerCode"]) + "' where dfclientid= " + dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[0].Value;
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = "update AMPlayerTokens set Dealercode='" + Convert.ToString(dtDetail.Rows[0]["DealerCode"]) + "' where clientid= " + dgRegistration.Rows[dgRegistration.CurrentCell.RowIndex].Cells[0].Value;
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();

            sQr = "";

            sQr = "update tbdealerlogin set  ";
            if (dToken > 0)
            {
                sQr = sQr + " isdam=1,";
            }
            else
            {
                sQr = sQr + " isdam=0,";
            }
            if (cToken > 0)
            {
                sQr = sQr + " isCopyright=1,";
            }
            else
            {
                sQr = sQr + " isCopyright=0,";
            }
            if (sToken > 0)
            {
                sQr = sQr + " IsSanjivani=1,";
            }
            else
            {
                sQr = sQr + " IsSanjivani=0,";
            }
            sQr = sQr + " DamTotaltoken= " + dToken + ",CopyrightTotalToken= " + cToken + ", SanjivaniTotalToken = " + sToken + " where dfclientid=" + Convert.ToInt32(cmbDealer.SelectedValue);

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = sQr;
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
            ClearFields();
            panClient.Visible = false;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel4.Enabled = true;
        }

        private void cmbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbDealer.SelectedValue) == 0) return;
            string sQr = " ";
            sQr = " select * from tbDealerLogin where dfclientid= " + Convert.ToInt32(cmbDealer.SelectedValue);
            DataTable dtDetail = new DataTable();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                lblClientDealerCode.Text = Convert.ToString(dtDetail.Rows[0]["DealerCode"]);
            }
        }
         
        

        private void cmbSearchCountryName_Leave(object sender, EventArgs e)
        {
           // label4.Text;
        }


    }
}
