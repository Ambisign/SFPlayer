using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
namespace ManagementPanel
{
    public partial class frmTokenInformation : Form
    {
        Int32 Clientid = 0;
        gblClass objMainClass = new gblClass();
        DateTimeFormatInfo fi = new DateTimeFormatInfo();
        Int32 ReturnSchId = 0;
        public frmTokenInformation()
        {
            InitializeComponent();

        }
        private void FillCombo()
        {
            string str = "";
            str = "select * from CountryCodes order by countryCode";
            objMainClass.fnFillComboBox(str, cmbCountryName, "countrycode", "countryName", "");
        }
        private void btnStateNew_Click(object sender, EventArgs e)
        {
            if (cmbCountryName.Text == "")
            {
                MessageBox.Show("Select a Country from the list name", "Management Panel");
                cmbCountryName.Focus();
                return;
            }
            txtName.Text = "";
            txtName.Focus();
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.Location = new Point(0, 0);
            panMainNew.BringToFront();
            panMainNew.Visible = true;
            lblCaption.Text = "State Name";
        }

        private void btnCityNew_Click(object sender, EventArgs e)
        {
            if (cmbCountryName.Text == "")
            {
                MessageBox.Show("Select a Country from the list name", "Management Panel");
                cmbCountryName.Focus();
                return;
            }
            if (cmbStateName.Text == "")
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

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0) return;
            string strState = "";
            strState = "select * from tbState where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " order by StateName";
            objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");
        }

        private void cmbStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0) return;
            string strCity = "";
            strCity = "select * from tbCity where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " and stateid =" + Convert.ToInt32(cmbStateName.SelectedValue)+ " order by CityName ";
            objMainClass.fnFillComboBox(strCity, cmbCityName, "Cityid", "CityName", "");
        }

        private void panMainNew_VisibleChanged(object sender, EventArgs e)
        {
            if (panMainNew.Visible == true)
            {
                txtName.Focus();
            }
        }

        private void frmTokenInformation_Load(object sender, EventArgs e)
        {
            fi.AMDesignator = "AM";
            fi.PMDesignator = "PM";
            this.Icon = Properties.Resources.Eufory;
            FillCombo();
            FillTokenInfoSaved("GetTokenInformation " + StaticClass.DealerTokenId);
            FillPlaylist();
            FillAdvt();
            FillPrayer();
        }
        private void ClearFields()
        {
            ReturnSchId = 0;
            FillTokenInfoSaved("GetTokenInformation " + StaticClass.DealerTokenId);
            
        }
        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (objMainClass.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please check your Internet connection.", "Management Panel");
                return;
            }
            if (SubmitValidation() == false) return;
            SaveTokenInformation();
            this.Hide();
        }
        private Boolean SubmitValidation()
        {
            if (txtTokenNo.Text == "")
            {
                //MessageBox.Show("Token No cannot be blank", "Management Panel");
                //txtTokenNo.Focus();
                //return false;
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
            if (txtLocation.Text == "")
            {
                MessageBox.Show("The location cannot be empty", "Management Panel");
                txtLocation.Focus();
                return false;
            }
            if (cmblType.Text == "")
            {
                MessageBox.Show("The Licence Type cannot be empty", "Management Panel");
                cmblType.Focus();
                return false;
            }
            if (cmbpType.Text == "")
            {
                MessageBox.Show("The player Type cannot be empty", "Management Panel");
                cmbpType.Focus();
                return false;
            }
            return true;
        }
        private void SaveTokenInformation()
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spTokenInformation", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Tokenid", SqlDbType.BigInt));
            cmd.Parameters["@Tokenid"].Value = StaticClass.DealerTokenId;

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


            cmd.Parameters.Add(new SqlParameter("@PersonName", SqlDbType.VarChar));
            cmd.Parameters["@PersonName"].Value = txtPersonName.Text;

            cmd.Parameters.Add(new SqlParameter("@Store", SqlDbType.Int));
            cmd.Parameters["@Store"].Value = Convert.ToByte(rdoStore.Checked);

            cmd.Parameters.Add(new SqlParameter("@Stream", SqlDbType.Int));
            cmd.Parameters["@Stream"].Value = Convert.ToByte(rdoStream.Checked);

            cmd.Parameters.Add(new SqlParameter("@ExpDate", SqlDbType.DateTime));
            cmd.Parameters["@ExpDate"].Value = dtpExpiryDate.Value;

            cmd.Parameters.Add(new SqlParameter("@IsStopControl", SqlDbType.Int));
            if (rdoControl.Checked == true)
            {
                cmd.Parameters["@IsStopControl"].Value = "1";
            }
            else
            {
                cmd.Parameters["@IsStopControl"].Value = "0";
            }

            cmd.Parameters.Add(new SqlParameter("@IsVedioActive", SqlDbType.Int));
            if (rdoActive.Checked == true)
            {
                cmd.Parameters["@IsVedioActive"].Value = "1";
            }
            else
            {
                cmd.Parameters["@IsVedioActive"].Value = "0";
            }

            cmd.Parameters.Add(new SqlParameter("@pType", SqlDbType.VarChar));
            cmd.Parameters["@pType"].Value = cmbpType.Text;

            cmd.Parameters.Add(new SqlParameter("@lType", SqlDbType.VarChar));
            cmd.Parameters["@lType"].Value = cmblType.Text;

            cmd.Parameters.Add(new SqlParameter("@TokenNo", SqlDbType.VarChar));
            cmd.Parameters["@TokenNo"].Value = txtTokenNo.Text;



            try
            {
                cmd.ExecuteNonQuery();
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
        private void InitilizeSplGrid()
        {
            if (dgSpl.Rows.Count > 0)
            {
                dgSpl.Rows.Clear();
            }
            if (dgSpl.Columns.Count > 0)
            {
                dgSpl.Columns.Clear();
            }
            dgSpl.Dock = DockStyle.Fill;
            //0
            dgSpl.Columns.Add("Id", "Id");
            dgSpl.Columns["Id"].Width = 0;
            dgSpl.Columns["Id"].Visible = false;
            dgSpl.Columns["Id"].ReadOnly = true;
            //1

            dgSpl.Columns.Add("cName", "Client Name");
            dgSpl.Columns["cName"].Width = 200;
            dgSpl.Columns["cName"].Visible = false;
            dgSpl.Columns["cName"].ReadOnly = true;
            dgSpl.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("fName", "Format Name");
            dgSpl.Columns["fName"].Width = 200;
            dgSpl.Columns["fName"].Visible = true;
            dgSpl.Columns["fName"].ReadOnly = true;
            dgSpl.Columns["fName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("pName", "Playlist Name");
            dgSpl.Columns["pName"].Width = 400;
            dgSpl.Columns["pName"].Visible = true;
            dgSpl.Columns["pName"].ReadOnly = true;
            dgSpl.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("sTime", "Start Time");
            dgSpl.Columns["sTime"].Width = 200;
            dgSpl.Columns["sTime"].Visible = true;
            dgSpl.Columns["sTime"].ReadOnly = true;
            dgSpl.Columns["sTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("eTime", "End Time");
            dgSpl.Columns["eTime"].Width = 200;
            dgSpl.Columns["eTime"].Visible = true;
            dgSpl.Columns["eTime"].ReadOnly = true;
            dgSpl.Columns["eTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("wDay", "Week Day");
            dgSpl.Columns["wDay"].Width = 200;
            dgSpl.Columns["wDay"].Visible = true;
            dgSpl.Columns["wDay"].ReadOnly = true;
            dgSpl.Columns["wDay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditAdvt = new DataGridViewLinkColumn();
            EditAdvt.HeaderText = "Edit";
            EditAdvt.Text = "Edit";
            EditAdvt.DataPropertyName = "Edit";
            dgSpl.Columns.Add(EditAdvt);
            EditAdvt.UseColumnTextForLinkValue = true;
            EditAdvt.Width = 70;
            EditAdvt.Visible = true;
            dgSpl.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("TokenId", "TokenId");
            dgSpl.Columns["TokenId"].Width = 0;
            dgSpl.Columns["TokenId"].Visible = false;
            dgSpl.Columns["TokenId"].ReadOnly = true;


        }
        private void FillPlaylist()
        {
            string sQr = "";
            sQr = "GetCustomerPlaylistSchedule  'where m.tokenid="+StaticClass.DealerTokenId.ToString()+" order by StartTime'";

            DataTable dtDetail = new DataTable();
            InitilizeSplGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    sQr = "";
                    dgSpl.Rows.Add();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["pSchid"];
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["cName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Value = dtDetail.Rows[i]["FormatName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["pName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tokenid"].Value = dtDetail.Rows[i]["MainTokenid"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Value = string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dtDetail.Rows[i]["StartTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Value = string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dtDetail.Rows[i]["EndTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Value = GetWeekName(Convert.ToInt32(dtDetail.Rows[i]["pSchId"]));

                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));

                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    //dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.BackColor = Color.FromArgb(224,224,224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.BackColor = Color.FromArgb(224, 224, 224);

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);


                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.SelectionForeColor = Color.Black;


                }
            }
            foreach (DataGridViewRow row in dgSpl.Rows)
            {
                row.Height = 30;
            }

        }
        private string GetWeekName(Int32 pSchId)
        {

            string str = "";
            DataTable dtDetail = new DataTable();
            str = "select iif(wId=1,'Mon',iif(wid=2,'Tue',iif(wid=3,'Wed',iif(wid=4,'Thu',iif(wid=5,'Fri',iif(wid=6,'Sat',iif(wid=7,'Sun','All'))))))) as wName from tbSpecialPlaylistSchedule_WeekDay where pSchId= " + pSchId;
            dtDetail = objMainClass.fnFillDataTable(str);


            if (dtDetail.Rows.Count > 0)
            {
                str = "";
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    if (str == "")
                    {
                        str = dtDetail.Rows[i]["wName"].ToString();
                    }
                    else
                    {
                        str = str + "," + dtDetail.Rows[i]["wName"].ToString();
                    }

                }
            }


            return str;
        }
        private void FillTokenInfoSaved(string Qr)
        {
            DataTable dtDetail = new DataTable();
            dtDetail = objMainClass.fnFillDataTable(Qr);

            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    txtTokenNo.Text = dtDetail.Rows[i]["tokenNo"].ToString();
                    cmbCountryName.SelectedValue = Convert.ToInt32(dtDetail.Rows[i]["CountryId"]);
                    txtTokenCode.Text = dtDetail.Rows[i]["TokenNobkp"].ToString();
                    txtPersonName.Text = dtDetail.Rows[i]["PersonName"].ToString();
                    cmbStateName.SelectedValue = Convert.ToInt32(dtDetail.Rows[i]["StateId"]);
                    txtStreet.Text = dtDetail.Rows[i]["StreetName"].ToString();
                    txtLocation.Text = dtDetail.Rows[i]["Location"].ToString();
                    cmblType.Text = dtDetail.Rows[i]["ltype"].ToString();
                    cmbCityName.SelectedValue = Convert.ToInt32(dtDetail.Rows[i]["CityId"]);

                    rdoStore.Checked = Convert.ToBoolean(dtDetail.Rows[i]["Store"]);
                    rdoStream.Checked = Convert.ToBoolean(dtDetail.Rows[i]["Stream"]);
                    dtpExpiryDate.Value = Convert.ToDateTime(string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["cDate"]));

                    txtDeviceId.Text = dtDetail.Rows[i]["deviceId"].ToString();

                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsStopControl"]) == true)
                    {
                        rdoControl.Checked = true;
                    }
                    else
                    {
                        rdoUnControl.Checked = true;
                    }
                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsVedioActive"]) == true)
                    {
                        rdoActive.Checked = true;
                    }
                    else
                    {
                        rdoUnActive.Checked = true;
                    }
                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsStore"]) == true)
                    {
                        rdoStore.Checked = true;
                    }
                    else
                    {
                        rdoStream.Checked = true;
                    }
                    cmbpType.Text = dtDetail.Rows[i]["ptype"].ToString();


                    Clientid = Convert.ToInt32(dtDetail.Rows[i]["Clientid"]);

                }
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void dgSpl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 7)
            {
                p1.Enabled = false;
                p2.Enabled = false;
                tbcMain.Enabled = false;
                panEdit.Visible = true;
                panEdit.BringToFront();
                panEdit.Size = new Size(872, 243);
                panEdit.Location = new Point(
          this.Width / 2 - panEdit.Size.Width / 2,
          this.Height / 2 - panEdit.Size.Height / 2);
                ReturnSchId = Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                txtCustomer.Text = dgSpl.Rows[e.RowIndex].Cells["cname"].Value.ToString();
                txtFormat.Text = dgSpl.Rows[e.RowIndex].Cells["fname"].Value.ToString();
                txtPlaylist.Text = dgSpl.Rows[e.RowIndex].Cells["pname"].Value.ToString();
                dtpUpStartTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["stime"].Value)));
                dtpUpEndTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["etime"].Value)));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmdPublish = new SqlCommand();
            cmdPublish.Connection = StaticClass.constr;
            cmdPublish.CommandText = "update AMPlayerTokens set isPublish=0 where tokenid=" + Convert.ToInt32(dgSpl.Rows[dgSpl.CurrentCell.RowIndex].Cells["tokenid"].Value) + "";
            cmdPublish.ExecuteNonQuery();
            StaticClass.constr.Close();

            UpdateMainData();
            FillPlaylist();
            ReturnSchId = 0;
            panEdit.Visible = false;
            p1.Enabled = true;
            p2.Enabled = true;
            tbcMain.Enabled = true;
        }

       
        private void UpdateMainData()
        {
            string str = "";
            str = "update tbSpecialPlaylistSchedule set StartTime='" + string.Format(fi, "{0:hh:mm tt}", dtpUpStartTime.Value) + "', ";
            str = str+  " EndTime ='" + string.Format(fi, "{0:hh:mm tt}", dtpUpEndTime.Value) + "' where pschid= " + ReturnSchId;
            SqlCommand cmd = new SqlCommand(str,StaticClass.constr);
            cmd.CommandType = CommandType.Text;
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnSchId = 0;
            panEdit.Visible = false;
            p1.Enabled = true;
            p2.Enabled = true;
            tbcMain.Enabled = true;
        }

        private int ReturnWeekId(string CurrentWeekday)
        {
            if (CurrentWeekday == "Sunday")
            {
                return 1;
            }
            if (CurrentWeekday == "Monday")
            {
                return 2;
            }
            if (CurrentWeekday == "Tuesday")
            {
                return 3;
            }
            if (CurrentWeekday == "Wednesday")
            {
                return 4;
            }
            if (CurrentWeekday == "Thursday")
            {
                return 5;
            }
            if (CurrentWeekday == "Friday")
            {
                return 6;
            }
            if (CurrentWeekday == "Saturday")
            {
                return 7;
            }
            return 0;
        }
        private void InitilizeAdvertisementGrid()
        {
            if (dgAdvt.Rows.Count > 0)
            {
                dgAdvt.Rows.Clear();
            }
            if (dgAdvt.Columns.Count > 0)
            {
                dgAdvt.Columns.Clear();
            }

            dgAdvt.Columns.Add("Advtid", "Advt Id");
            dgAdvt.Columns["Advtid"].Width = 0;
            dgAdvt.Columns["Advtid"].Visible = false;
            dgAdvt.Columns["Advtid"].ReadOnly = true;

            dgAdvt.Columns.Add("Advt", "Advt Name");
            dgAdvt.Columns["Advt"].Width = 320;
            dgAdvt.Columns["Advt"].Visible = true;
            dgAdvt.Columns["Advt"].ReadOnly = true;
            dgAdvt.Columns["Advt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAdvt.Columns.Add("pMode", "");
            dgAdvt.Columns["pMode"].Width = 100;
            dgAdvt.Columns["pMode"].Visible = true;
            dgAdvt.Columns["pMode"].ReadOnly = true;
            dgAdvt.Columns["pMode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgAdvt.Columns.Add("StartDate", "Start Date");
            dgAdvt.Columns["StartDate"].Width = 150;
            dgAdvt.Columns["StartDate"].Visible = true;
            dgAdvt.Columns["StartDate"].ReadOnly = true;
            dgAdvt.Columns["StartDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgAdvt.Columns.Add("AdvtTime", "");
            dgAdvt.Columns["AdvtTime"].Width = 150;
            dgAdvt.Columns["AdvtTime"].Visible = true;
            dgAdvt.Columns["AdvtTime"].ReadOnly = true;
            dgAdvt.Columns["AdvtTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void FillAdvt()
        {
            string str = "";
            DataTable dtDetail = new DataTable();
            str = "spGetAdvtAdmin_NativeOnly '" + string.Format("{0:dd/MMM/yyyy}", DateTime.Now.Date) + "','NativeCR'," + Clientid + "," + ReturnWeekId(DateTime.Now.DayOfWeek.ToString()) + ", " + Convert.ToInt32(cmbCityName.SelectedValue) + "," + Clientid + " , " + Convert.ToInt32(cmbCountryName.SelectedValue) + ", " + Convert.ToInt32(cmbStateName.SelectedValue) + "," + StaticClass.DealerTokenId + "";
            dtDetail = objMainClass.fnFillDataTable(str);
            InitilizeAdvertisementGrid();
            if ((dtDetail.Rows.Count > 0))
            {
                for (int iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgAdvt.Rows.Add();
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advtid"].Value = dtDetail.Rows[iCtr]["AdvtId"];
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advt"].Value = dtDetail.Rows[iCtr]["AdvtDisplayName"];
                    if (Convert.ToInt32(dtDetail.Rows[iCtr]["IsVideo"]) == 1)
                    {
                        dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Value = "Video";
                    }
                    else if (Convert.ToInt32(dtDetail.Rows[iCtr]["IsPicture"]) == 1)
                    {
                        dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Value = "Picture";
                    }
                    else
                    {
                        dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Value = "Audio";
                    }
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["StartDate"].Value = string.Format("{0:dd-MMM-yyyy}", dtDetail.Rows[iCtr]["AdvtStartDate"]);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Value = string.Format("{0:hh:mm tt}", dtDetail.Rows[iCtr]["AdvtTime"]);
                    if (Convert.ToBoolean(dtDetail.Rows[iCtr]["IsMinute"]) == true)
                    {
                        dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Value = "After " + dtDetail.Rows[iCtr]["TotalMinutes"].ToString().Trim() + " min";
                    }
                    if (Convert.ToBoolean(dtDetail.Rows[iCtr]["IsSong"]) == true)
                    {
                        dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Value = "After " + dtDetail.Rows[iCtr]["TotalSongs"].ToString().Trim() + " songs";
                    }
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advtid"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advt"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["StartDate"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Style.BackColor = Color.FromArgb(224, 224, 224);

                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advtid"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advt"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["StartDate"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);


                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advtid"].Style.SelectionForeColor = Color.Black;
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["Advt"].Style.SelectionForeColor = Color.Black;
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["pMode"].Style.SelectionForeColor = Color.Black;
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["StartDate"].Style.SelectionForeColor = Color.Black;
                    dgAdvt.Rows[dgAdvt.Rows.Count - 1].Cells["AdvtTime"].Style.SelectionForeColor = Color.Black;
                }
                foreach (DataGridViewRow row in dgAdvt.Rows)
                {
                    row.Height = 30;
                }
            }
        }

        private void InitilizePrayer(DataGridView dgGrid)
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.Rows.Clear();
            }
            if (dgGrid.Columns.Count > 0)
            {
                dgGrid.Columns.Clear();
            }

            dgGrid.Columns.Add("sDate", "Start Date");
            dgGrid.Columns["sDate"].Width = 180;
            dgGrid.Columns["sDate"].Visible = true;
            dgGrid.Columns["sDate"].ReadOnly = true;
            dgGrid.Columns["sDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["sDate"].ValueType = typeof(string);

            dgGrid.Columns.Add("Time1", "Time 1");
            dgGrid.Columns["Time1"].Width = 100;
            dgGrid.Columns["Time1"].Visible = true;
            dgGrid.Columns["Time1"].ReadOnly = true;
            dgGrid.Columns["Time1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["Time1"].ValueType = typeof(string);

            dgGrid.Columns.Add("Time2", "Time 2");
            dgGrid.Columns["Time2"].Width = 100;
            dgGrid.Columns["Time2"].Visible = true;
            dgGrid.Columns["Time2"].ReadOnly = true;
            dgGrid.Columns["Time2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["Time2"].ValueType = typeof(string);

            dgGrid.Columns.Add("Time3", "Time 3");
            dgGrid.Columns["Time3"].Width = 100;
            dgGrid.Columns["Time3"].Visible = true;
            dgGrid.Columns["Time3"].ReadOnly = true;
            dgGrid.Columns["Time3"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["Time3"].ValueType = typeof(string);

            dgGrid.Columns.Add("Time4", "Time 4");
            dgGrid.Columns["Time4"].Width = 100;
            dgGrid.Columns["Time4"].Visible = true;
            dgGrid.Columns["Time4"].ReadOnly = true;
            dgGrid.Columns["Time4"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["Time4"].ValueType = typeof(string);

            dgGrid.Columns.Add("Time5", "Time 5");
            dgGrid.Columns["Time5"].Width = 100;
            dgGrid.Columns["Time5"].Visible = true;
            dgGrid.Columns["Time5"].ReadOnly = true;
            dgGrid.Columns["Time5"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["Time5"].ValueType = typeof(string);

            
        }
        private void FillPrayer()
        {
           
            string iPr = "";
            iPr = "spGetPrayerDataInfo " + DateTime.Now.Date.Month + ", " + Convert.ToInt32(cmbCountryName.SelectedValue) + ", " + Convert.ToInt32(cmbStateName.SelectedValue) + " ," + Convert.ToInt32(cmbCityName.SelectedValue) + "," + StaticClass.DealerTokenId + "";
            int iCtr;
            DataTable dtDetail;
            dtDetail = objMainClass.fnFillDataTable(iPr);
            InitilizePrayer(dgPrayer);
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgPrayer.Rows.Add();
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["sDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[iCtr]["sDate"]);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time1"].Value = dtDetail.Rows[iCtr]["Time1"].ToString();
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time2"].Value = dtDetail.Rows[iCtr]["Time2"].ToString();
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time3"].Value = dtDetail.Rows[iCtr]["Time3"].ToString();
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time4"].Value = dtDetail.Rows[iCtr]["Time4"].ToString();
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time5"].Value = dtDetail.Rows[iCtr]["Time5"].ToString();

                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["sDate"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time1"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time2"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time3"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time4"].Style.BackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time5"].Style.BackColor = Color.FromArgb(224, 224, 224);

                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["sDate"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time1"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time2"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time3"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time4"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time5"].Style.SelectionBackColor = Color.FromArgb(224, 224, 224);


                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["sDate"].Style.SelectionForeColor = Color.Black;
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time1"].Style.SelectionForeColor = Color.Black;
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time2"].Style.SelectionForeColor = Color.Black;
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time3"].Style.SelectionForeColor = Color.Black;
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time4"].Style.SelectionForeColor = Color.Black;
                    dgPrayer.Rows[dgPrayer.Rows.Count - 1].Cells["Time5"].Style.SelectionForeColor = Color.Black;
                }
                foreach (DataGridViewRow row in dgPrayer.Rows)
                {
                    row.Height = 30;
                }
            }
        }

        private void btnResetToken_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure to reset this token ?", "Management Panel", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string strDel = "";
                strDel = "update AMPlayerTokens set token='" + txtTokenCode.Text + "',code=null where tokenid= " + StaticClass.DealerTokenId;
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                strDel = "";
                strDel = "delete from titlesinplaylists where playlistid in(select distinct playlistid from playlists where tokenid="+StaticClass.DealerTokenId+")";
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd = new SqlCommand(strDel, StaticClass.constr);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                strDel = "";
                strDel = "delete from playlists where tokenid=" + StaticClass.DealerTokenId; 
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd = new SqlCommand(strDel, StaticClass.constr);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                MessageBox.Show("Token is reset", "Management Panel");
            }
        }
    }
}
