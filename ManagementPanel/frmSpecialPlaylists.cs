using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmSpecialPlaylists : Form
    {

        Type FormType;
        Form ObjFormName;
        DateTimeFormatInfo fi = new DateTimeFormatInfo();
        gblClass objMainClass = new gblClass();

        CheckBox ClientCheckBox = null;
        bool IsClientCheckBoxClicked = false;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;

        Int32 ReturnSchId = 0;
        Int32 rtPschId = 0;
        string IsRecordModify = "No";


        CheckBox PublishCheckBox = null;
        bool IsPublishCheckBoxClicked = false;
        int TotalCheckBoxesPublish = 0;
        int TotalCheckedCheckBoxesPublish = 0;

        private frmMain mainForm = null;
        public frmSpecialPlaylists(Form callingForm)
        {
            mainForm = callingForm as frmMain;
            InitializeComponent();
        }
        public frmSpecialPlaylists()
        {
            InitializeComponent();

        }


        private void FillSplPlaylists(ComboBox cmbName, Int32 ForId)
        {
            string str = "";
            str = "select  tbSpecialPlaylists.splPlaylistid, (tbSpecialPlaylists.splPlaylistName+ ' (' +convert(varchar(50), count(*) ) + ')' ) as splPlaylistName from tbSpecialPlaylists ";
            str = str + " inner join tbSpecialPlaylists_Titles on tbSpecialPlaylists_Titles.splPlaylistid = tbSpecialPlaylists.splPlaylistid";
            str = str + " where tbSpecialPlaylists.formatid=" + ForId + " ";
            str = str + " group by tbSpecialPlaylists.splPlaylistid, tbSpecialPlaylists.splPlaylistName";
            objMainClass.fnFillComboBox(str, cmbName, "splPlaylistId", "splPlaylistName", "");

        }
        private void FillFormat()
        {
            string strState = "";
            strState = "select max(Formatid) as Formatid, formatname from tbSpecialFormat group by formatname";
            objMainClass.fnFillComboBox(strState, cmbFormat, "FormatId", "FormatName", "");
             
            objMainClass.fnFillComboBox(strState, cmbSearchFormat, "FormatId", "FormatName", "");

            strState = "";
            strState = "select  max(tbSpecialPlaylists.splPlaylistid) as splPlaylistid, tbSpecialPlaylists.splPlaylistName from tbSpecialPlaylists ";
            strState = strState + " group by tbSpecialPlaylists.splPlaylistName";
            objMainClass.fnFillComboBox(strState, cmbSearchPlaylist, "splPlaylistId", "splPlaylistName", "");


        }
        private void frmSpecialPlaylists_Load(object sender, EventArgs e)
        {
            fi.AMDesignator = "AM";
            fi.PMDesignator = "PM";
            AddClientCheckBox(dgToken);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);

            AddPublishCheckBox(dgPublish);
            PublishCheckBox.KeyUp += new KeyEventHandler(PublishCheckBox_KeyUp);
            PublishCheckBox.MouseClick += new MouseEventHandler(PublishCheckBox_MouseClick);


            dtpStartTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", DateTime.Now));
            dtpEndTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", DateTime.Now));

            InitilizeGrid();
            InitilizeSplGrid();
            FillFormat();
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panSearch.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
            panAssign.Visible = false;

            InitilizePublishGrid();


        }
        #region Add Check Box
        private void ClientCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                ClientCheckBoxClick((CheckBox)sender);
        }
        private void ClientCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            ClientCheckBoxClick((CheckBox)sender);
        }
        private void ClientCheckBoxClick(CheckBox HCheckBox)
        {
            IsClientCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgToken.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgToken.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsClientCheckBoxClicked = false;
        }

        private void AddClientCheckBox(DataGridView dgToken)
        {
            ClientCheckBox = new CheckBox();
            ClientCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgToken.Controls.Add(ClientCheckBox);

        }
        private void dgToken_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsClientCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dgToken[e.ColumnIndex, e.RowIndex]);
        }
        private void dgToken_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgToken.CurrentCell is DataGridViewCheckBoxCell)
                dgToken.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgToken_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }
        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgToken.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - ClientCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - ClientCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            ClientCheckBox.Location = oPoint;
        }
        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    ClientCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    ClientCheckBox.Checked = true;
            }
        }
        #endregion




        private void FillData()
        {
            string sQr = "";
            //sQr = "SELECT AMPlayerTokens.TokenID , (iif(token='used','',isnull(tokennobkp,'')) + ' ' + convert(varchar(100) ,Tokenid)) as tNo, isnull(AMPlayerTokens.Location,'') as Location,";
            //sQr = sQr + " isnull(tbCity.CityName,'') as CityName, isnull(tbState.StateName,'') as StateName, isnull(CountryCodes.CountryName,'') as CountryName,isnull(AMPlayerTokens.PersonName ,'') as PersonName , AMPlayerTokens.userid, isnull(AMPlayerTokens.IsStore,0) as IsStore, isnull(AMPlayerTokens.IsStream,0) as IsStream FROM  AMPlayerTokens ";
            //sQr = sQr + " LEFT OUTER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId LEFT OUTER JOIN tbState ON AMPlayerTokens.StateId = tbState.StateId LEFT OUTER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode";
            //sQr = sQr + " WHERE AMPlayerTokens.ClientID = " + Convert.ToInt32(cmbDname.SelectedValue) + " ";

            sQr = "GetTokenInfo " + Convert.ToInt32(cmbDname.SelectedValue);

            DataTable dtDetail = new DataTable();
            InitilizeGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgToken.Rows.Add();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["tokenid"];
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells[1].Value = 0;
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["tNo"].Value = dtDetail.Rows[i]["tNo"].ToString();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["PersonName"].ToString();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["loc"].Value = dtDetail.Rows[i]["Location"].ToString();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["CityName"].ToString();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StateName"].ToString();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["coName"].Value = dtDetail.Rows[i]["CountryName"].ToString();
                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsStore"]) == true)
                    {
                        dgToken.Rows[dgToken.Rows.Count - 1].Cells["ver"].Value = "Store";
                    }
                    else
                    {
                        dgToken.Rows[dgToken.Rows.Count - 1].Cells["ver"].Value = "Stream";
                    }
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["uId"].Value = dtDetail.Rows[i]["userid"].ToString();

                }
            }
            foreach (DataGridViewRow row in dgToken.Rows)
            {
                row.Height = 30;
            }



        }
        private void InitilizeGrid()
        {
            if (dgToken.Rows.Count > 0)
            {
                dgToken.Rows.Clear();
            }
            if (dgToken.Columns.Count > 0)
            {
                dgToken.Columns.Clear();
            }
            dgToken.Dock = DockStyle.Fill;
            //0
            dgToken.Columns.Add("Id", "Id");
            dgToken.Columns["Id"].Width = 0;
            dgToken.Columns["Id"].Visible = false;
            dgToken.Columns["Id"].ReadOnly = true;
            //1
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "";
            chk.DataPropertyName = "IsChecked";
            dgToken.Columns.Add(chk);
            chk.Width = 50;
            chk.Visible = true;
            dgToken.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //2
            dgToken.Columns.Add("tNo", "Token No");
            dgToken.Columns["tNo"].Width = 200;
            dgToken.Columns["tNo"].Visible = true;
            dgToken.Columns["tNo"].ReadOnly = true;
            dgToken.Columns["tNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("pName", "Station Name");
            dgToken.Columns["pName"].Width = 250;
            dgToken.Columns["pName"].Visible = true;
            dgToken.Columns["pName"].ReadOnly = true;
            dgToken.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("loc", "Location");
            dgToken.Columns["loc"].Width = 150;
            dgToken.Columns["loc"].Visible = true;
            dgToken.Columns["loc"].ReadOnly = true;
            dgToken.Columns["loc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("cName", "City");
            dgToken.Columns["cName"].Width = 150;
            dgToken.Columns["cName"].Visible = true;
            dgToken.Columns["cName"].ReadOnly = true;
            dgToken.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("sName", "State");
            dgToken.Columns["sName"].Width = 150;
            dgToken.Columns["sName"].Visible = true;
            dgToken.Columns["sName"].ReadOnly = true;
            dgToken.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("coName", "Country");
            dgToken.Columns["coName"].Width = 150;
            dgToken.Columns["coName"].Visible = true;
            dgToken.Columns["coName"].ReadOnly = true;
            dgToken.Columns["coName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("ver", "Type");
            dgToken.Columns["ver"].Width = 100;
            dgToken.Columns["ver"].Visible = true;
            dgToken.Columns["ver"].ReadOnly = true;
            dgToken.Columns["ver"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn ModifyToken = new DataGridViewLinkColumn();
            ModifyToken.HeaderText = "Modify";
            ModifyToken.Text = "Modify";
            ModifyToken.DataPropertyName = "Modify";
            dgToken.Columns.Add(ModifyToken);
            ModifyToken.UseColumnTextForLinkValue = true;
            ModifyToken.Width = 70;
            ModifyToken.Visible = true;
            dgToken.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgToken.Columns.Add("uId", "uId");
            dgToken.Columns["uId"].Width = 0;
            dgToken.Columns["uId"].Visible = false;
            dgToken.Columns["uId"].ReadOnly = true;

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAll.Checked == true)
            {

                chkSun.Checked = false;
                chkMon.Checked = false;
                chkTue.Checked = false;
                chkWed.Checked = false;
                chkThu.Checked = false;
                chkFri.Checked = false;
                chkSat.Checked = false;

                chkSun.Enabled = false;
                chkMon.Enabled = false;
                chkTue.Enabled = false;
                chkWed.Enabled = false;
                chkThu.Enabled = false;
                chkFri.Enabled = false;
                chkSat.Enabled = false;
            }
            else
            {

                chkSun.Enabled = true;
                chkMon.Enabled = true;
                chkTue.Enabled = true;
                chkWed.Enabled = true;
                chkThu.Enabled = true;
                chkFri.Enabled = true;
                chkSat.Enabled = true;
            }
        }
        private Boolean SubmitValidationGet()
        {

            if (Convert.ToInt32(cmbFormat.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a format name", "Management Panel");
                cmbFormat.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbSplPlaylist.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a special playlist name.", "Management Panel");
                cmbSplPlaylist.Focus();
                return false;
            }
            if ( dtpEndTime.Value.Hour < dtpStartTime.Value.Hour)
            {
              MessageBox.Show("End time should be greater than start time", "Management Panel");
                dtpEndTime.Focus();
               return false;
            }
            if ((chkAll.Checked == false) && (chkSun.Checked == false) && (chkMon.Checked == false) && (chkTue.Checked == false) && (chkWed.Checked == false) && (chkThu.Checked == false) && (chkFri.Checked == false) && (chkSat.Checked == false))
            {
                MessageBox.Show("Please select a week day", "Management Panel");
                chkAll.Focus();
                return false;
            }
            if (CheckGridValidationAdvt(dgToken) == false)
            {
                MessageBox.Show("Please select token no's from list", "Management Panel");
                return false;
            }
            string strDealerTimeValid = "";
            //if (btnSave.Text == "Save")
            //{
            //    strDealerTimeValid = "select * from tbSpecialPlaylistSchedule  where pversion='" + cmbPlayerType.Text + "' and dfclientid=" + Convert.ToInt32(cmbDealer.SelectedValue) + " and formatid=" + Convert.ToInt32(cmbFormat.SelectedValue) + " and splPlaylistId=" + Convert.ToInt32(cmbSplPlaylist.SelectedValue) + " ";
            //}
            //else
            //{
            //    strDealerTimeValid = "select * from tbSpecialPlaylistSchedule  where pversion='" + cmbPlayerType.Text + "' and dfclientid=" + Convert.ToInt32(cmbDealer.SelectedValue) + " and formatid=" + Convert.ToInt32(cmbFormat.SelectedValue) + " and splPlaylistId=" + Convert.ToInt32(cmbSplPlaylist.SelectedValue) + " ";
            //    strDealerTimeValid = strDealerTimeValid + " and pSchId <> " + ReturnSchId + " ";
            //}
            //DataTable dtDealerTimeValid = new DataTable();
            //dtDealerTimeValid = objMainClass.fnFillDataTable(strDealerTimeValid);
            //if (dtDealerTimeValid.Rows.Count > 0)
            //{
            //     MessageBox.Show("This playlist is already used in this format", "Management Panel");
            //     cmbSplPlaylist.Focus();
            //     return false;
            //}

            return true;
        }
        private Boolean CheckGridValidationAdvt(DataGridView dgGrid)
        {
            for (int i = 0; i < dgGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgGrid.Rows[i].Cells[1].Value) == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void cmbSplPlaylist_Click(object sender, EventArgs e)
        {

        }

        private void frmSpecialPlaylists_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strTit = "";
            if (SubmitValidationGet() == false) return;
            if (string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value) == "12:00 AM")
            {
                dtpEndTime.Value= dtpEndTime.Value.AddMinutes(-1);
            }
            
            DataTable dtPrv = new DataTable();
            int dbWId = 0;
            if (chkAll.Checked == true)
            {
                dbWId = 0;
            }
            if (chkMon.Checked == true)
            {
                dbWId = 1;
            }
            if (chkTue.Checked == true)
            {
                dbWId = 2;
            }
            if (chkWed.Checked == true)
            {
                dbWId = 3;
            }
            if (chkThu.Checked == true)
            {
                dbWId = 4;
            }
            if (chkFri.Checked == true)
            {
                dbWId = 5;
            }
            if (chkSat.Checked == true)
            {
                dbWId = 6;
            }
            if (chkSun.Checked == true)
            {
                dbWId = 7;
            }



            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgToken.Rows[i].Cells[1].Value) == true)
                {
                    strTit = "CheckTokenSchedule " + Convert.ToInt32(cmbDname.SelectedValue) + "," + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + "," + dbWId + ",'" + string.Format(fi, "{0:hh:mm tt}", dtpStartTime.Value.AddMinutes(1)) + "','" + string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value.AddMinutes(-1)) + "'";
                    dtPrv = objMainClass.fnFillDataTable(strTit);
                    if (dtPrv.Rows.Count > 0)
                    {
                        for (int iTit = 0; iTit <= dtPrv.Rows.Count - 1; iTit++)
                        {
                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmdTit = new SqlCommand();
                            cmdTit.Connection = StaticClass.constr;
                            strTit = "";
                            strTit = "delete from tbSpecialPlaylistSchedule_Token where pSchId=" + dtPrv.Rows[iTit]["pSchId"] + " and tokenid="+ Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and dfclientid= " + Convert.ToInt32(cmbDname.SelectedValue) + "";
                            cmdTit.CommandText = strTit;
                           // cmdTit.ExecuteNonQuery();
                            StaticClass.constr.Close();
                        }
                    }



                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmdPublish = new SqlCommand();
                    cmdPublish.Connection = StaticClass.constr;
                    cmdPublish.CommandText = "update AMPlayerTokens set isPublish=0 where tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + "";
                    cmdPublish.ExecuteNonQuery();
                    StaticClass.constr.Close();














                    SaveMainData();

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = StaticClass.constr;
                    cmd1.CommandText = "delete from tbSpecialPlaylistSchedule_Weekday where pSchId=" + rtPschId;
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();


                    if (chkAll.Checked == true)
                    {
                        SaveWeek(0, 1, rtPschId);
                    }
                    if (chkMon.Checked == true)
                    {
                        SaveWeek(1, 0, rtPschId);
                    }
                    if (chkTue.Checked == true)
                    {
                        SaveWeek(2, 0, rtPschId);
                    }
                    if (chkWed.Checked == true)
                    {
                        SaveWeek(3, 0, rtPschId);
                    }
                    if (chkThu.Checked == true)
                    {
                        SaveWeek(4, 0, rtPschId);
                    }
                    if (chkFri.Checked == true)
                    {
                        SaveWeek(5, 0, rtPschId);
                    }
                    if (chkSat.Checked == true)
                    {
                        SaveWeek(6, 0, rtPschId);
                    }
                    if (chkSun.Checked == true)
                    {
                        SaveWeek(7, 0, rtPschId);
                    }




                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd1 = new SqlCommand();
                    cmd1.Connection = StaticClass.constr;
                    cmd1.CommandText = "delete from tbSpecialPlaylistSchedule_Token where tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and pSchId= " + rtPschId + " ";
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    SaveTokenDetail(Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0, rtPschId);

                }
            }




            //SaveToken(ReturnSchId);

            MessageBox.Show("Record saved", "Management Panel");

            //FillSchData();
            ClearData();
            //FillSaveData();
        }


        private void SaveMainData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spSaveSpecialPlaylistSchedule", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@pSchId", SqlDbType.BigInt));
                cmd.Parameters["@pSchId"].Value = ReturnSchId;

                cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                cmd.Parameters["@pVersion"].Value = "c";

                cmd.Parameters.Add(new SqlParameter("@dfClientId", SqlDbType.BigInt));
                cmd.Parameters["@dfClientId"].Value = Convert.ToInt32(cmbDname.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
                cmd.Parameters["@splPlaylistId"].Value = Convert.ToInt32(cmbSplPlaylist.SelectedValue);


                cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime));
                cmd.Parameters["@StartTime"].Value = string.Format(fi, "{0:hh:mm tt}", dtpStartTime.Value);

                cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
                cmd.Parameters["@EndTime"].Value = string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value);

                cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
                cmd.Parameters["@FormatId"].Value = Convert.ToInt32(cmbFormat.SelectedValue);


                //cmd.Parameters.Add(new SqlParameter("@sHour", SqlDbType.Int));
                //cmd.Parameters["@sHour"].Value = string.Format(fi, "{0:HH}", dtpStartTime.Value);

                //cmd.Parameters.Add(new SqlParameter("@eHour", SqlDbType.Int));
                //cmd.Parameters["@eHour"].Value = string.Format(fi, "{0:HH}", dtpEndTime.Value);

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                rtPschId = Convert.ToInt32(cmd.ExecuteScalar());
                StaticClass.constr.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private void SaveWeek(int WeekId, int IsAllWeek, int pSch_id)
        {
            SqlCommand cmd = new SqlCommand("spSaveSpecialPlaylistSchedule_Week", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pSchId", SqlDbType.BigInt));
            cmd.Parameters["@pSchId"].Value = pSch_id;

            cmd.Parameters.Add(new SqlParameter("@wId", SqlDbType.Int));
            cmd.Parameters["@wId"].Value = WeekId;

            cmd.Parameters.Add(new SqlParameter("@IsAllWeek", SqlDbType.Int));
            cmd.Parameters["@IsAllWeek"].Value = IsAllWeek;

            cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
            cmd.Parameters["@FormatId"].Value = Convert.ToInt32(cmbFormat.SelectedValue);

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
        }
        private void SaveToken(Int32 pSchId)
        {

            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgToken.Rows[i].Cells[1].Value) == true)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = StaticClass.constr;
                    cmd1.CommandText = "delete from tbSpecialPlaylistSchedule_Token where tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and pSchId= " + pSchId + " ";
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    SaveTokenDetail(Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0, pSchId);

                }
            }
        }
        private void SaveTokenDetail(Int32 TokenId, int IsAllToken, Int32 pSchId)
        {
            SqlCommand cmd = new SqlCommand("spSaveSpecialPlaylistSchedule_Token", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pSchId", SqlDbType.BigInt));
            cmd.Parameters["@pSchId"].Value = pSchId;

            cmd.Parameters.Add(new SqlParameter("@tokenId", SqlDbType.BigInt));
            cmd.Parameters["@tokenId"].Value = TokenId;

            cmd.Parameters.Add(new SqlParameter("@IsAllToken", SqlDbType.Int));
            cmd.Parameters["@IsAllToken"].Value = IsAllToken;

            cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
            cmd.Parameters["@FormatId"].Value = Convert.ToInt32(cmbFormat.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@DfClientid", SqlDbType.BigInt));
            cmd.Parameters["@DfClientid"].Value = Convert.ToInt32(cmbDname.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
            cmd.Parameters["@splPlaylistId"].Value = Convert.ToInt32(cmbSplPlaylist.SelectedValue);


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {

            dtpStartTime.Value = Convert.ToDateTime(DateTime.Now.ToString("hh:mm tt", fi));
            dtpEndTime.Value = Convert.ToDateTime(DateTime.Now.ToString("hh:mm tt", fi));

            FillSplPlaylists(cmbSplPlaylist, Convert.ToInt32(cmbFormat.SelectedValue));
            ClearData();
        }
        private void ClearData()
        {
            btnSave.Text = "Save";

            ReturnSchId = 0;
            //cmbPlayerType.Text = "";
            //  dtpStartTime.Value = DateTime.Now;
            //  dtpEndTime.Value = DateTime.Now;
            chkAll.Checked = false;
            chkSun.Checked = false;
            chkMon.Checked = false;
            chkTue.Checked = false;
            chkWed.Checked = false;
            chkThu.Checked = false;
            chkFri.Checked = false;
            chkSat.Checked = false;

            //  FillDealers();
            FillSplPlaylists(cmbSplPlaylist, Convert.ToInt32(cmbFormat.SelectedValue));

        }

        private void cmbSearchDealer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID, RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbSearchDealer, "DFClientID", "ClientName", "");
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

            dgSpl.Columns.Add("cName", "Customer Name");
            dgSpl.Columns["cName"].Width = 200;
            dgSpl.Columns["cName"].Visible = true;
            dgSpl.Columns["cName"].ReadOnly = true;
            dgSpl.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("fName", "Format Name");
            dgSpl.Columns["fName"].Width = 200;
            dgSpl.Columns["fName"].Visible = true;
            dgSpl.Columns["fName"].ReadOnly = true;
            dgSpl.Columns["fName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("pName", "Playlist Name");
            dgSpl.Columns["pName"].Width = 200;
            dgSpl.Columns["pName"].Visible = true;
            dgSpl.Columns["pName"].ReadOnly = true;
            dgSpl.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgSpl.Columns.Add("tNo", "Token Code");
            dgSpl.Columns["tNo"].Width = 200;
            dgSpl.Columns["tNo"].Visible = true;
            dgSpl.Columns["tNo"].ReadOnly = true;
            dgSpl.Columns["tNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("plName", "Player Name");
            dgSpl.Columns["plName"].Width = 200;
            dgSpl.Columns["plName"].Visible = true;
            dgSpl.Columns["plName"].ReadOnly = true;
            dgSpl.Columns["plName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("locName", "Location");
            dgSpl.Columns["locName"].Width = 200;
            dgSpl.Columns["locName"].Visible = true;
            dgSpl.Columns["locName"].ReadOnly = true;
            dgSpl.Columns["locName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgSpl.Columns.Add("sTime", "Start Time");
            dgSpl.Columns["sTime"].Width = 150;
            dgSpl.Columns["sTime"].Visible = true;
            dgSpl.Columns["sTime"].ReadOnly = true;
            dgSpl.Columns["sTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("eTime", "End Time");
            dgSpl.Columns["eTime"].Width = 150;
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
            dgSpl.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteAdvt = new DataGridViewLinkColumn();
            DeleteAdvt.HeaderText = "Delete";
            DeleteAdvt.Text = "Delete";
            DeleteAdvt.DataPropertyName = "Delete";
            dgSpl.Columns.Add(DeleteAdvt);
            DeleteAdvt.UseColumnTextForLinkValue = true;
            DeleteAdvt.Width = 70;
            DeleteAdvt.Visible = true;
            dgSpl.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("TokenId", "TokenId");
            dgSpl.Columns["TokenId"].Width = 0;
            dgSpl.Columns["TokenId"].Visible = false;
            dgSpl.Columns["TokenId"].ReadOnly = true;
            dgSpl.Columns["TokenId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }
        private void FillSaveData()
        {
            string sQr = "";

            sQr = "GetCustomerPlaylistSchedule '" + GenrateQuery() + "'";

            DataTable dtDetail = new DataTable();
            InitilizeSplGrid();
            if (GenrateQuery() == "")
            {
                return;
            }
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    sQr = "";
                    dgSpl.Rows.Add();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["pSchid"];
                    //                    sQr = dtDetail.Rows[i]["splPlaylistName"].ToString() + " (" + GetSongCounter(Convert.ToInt32(dtDetail.Rows[i]["splPlaylistid"])) + ")";
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["cName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Value = dtDetail.Rows[i]["FormatName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["pName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tNo"].Value = dtDetail.Rows[i]["Tokenid"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["plName"].Value = dtDetail.Rows[i]["personname"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["locName"].Value = dtDetail.Rows[i]["Location"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Value = string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dtDetail.Rows[i]["StartTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Value = string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dtDetail.Rows[i]["EndTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Value = GetWeekName(Convert.ToInt32(dtDetail.Rows[i]["pSchId"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tokenid"].Value = dtDetail.Rows[i]["MainTokenid"].ToString();

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["cname"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fname"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pname"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["plname"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["locname"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tNo"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["cname"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fname"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pname"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["plname"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["locName"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tNo"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Style.SelectionBackColor = Color.FromArgb(Convert.ToInt32(dtDetail.Rows[i]["R"]), Convert.ToInt32(dtDetail.Rows[i]["G"]), Convert.ToInt32(dtDetail.Rows[i]["B"]));

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["cname"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fname"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pname"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["plname"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["locname"].Style.SelectionForeColor = Color.Black;
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tNo"].Style.SelectionForeColor = Color.Black;
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
        public string GenrateQuery()
        {
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and  formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by tokenid, personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " order by  tokenid, personname ,  StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by  tokenid, personname ,  StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by   tokenid, personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " order by  tokenid,  personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " order by tokenid,   personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by tokenid,  personname ,  StartTime";
            }
            return "";
        }
        private string GetWeekName(Int32 pSchId)
        {

            string str = "";
            DataTable dtDetail = new DataTable();
            str = "SELECT pSchId, STUFF((SELECT ', ' +iif(wId=1,'Mon',iif(wid=2,'Tue',iif(wid=3,'Wed',iif(wid=4,'Thu',iif(wid=5,'Fri',iif(wid=6,'Sat',iif(wid=7,'Sun','All'))))))) FROM tbSpecialPlaylistSchedule_WeekDay A";
            str = str + " Where A.pSchId=B.pSchId FOR XML PATH('')),1,1,'') As wName ";
            str = str + " From tbSpecialPlaylistSchedule_WeekDay B ";
            str = str + " where b.pSchId in(" + pSchId + ") ";
            str = str + " Group By pSchId ";
            dtDetail = objMainClass.fnFillDataTable(str);

            str = "";
            if (dtDetail.Rows.Count > 0)
            {
                str = dtDetail.Rows[0]["wName"].ToString();
                //str = "";
                //for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                //{
                //    if (str == "")
                //    {
                //        str = dtDetail.Rows[i]["wName"].ToString();
                //    }
                //    else
                //    {
                //        str = str + "," + dtDetail.Rows[i]["wName"].ToString();
                //    }

                //}
            }


            return str;
        }

        private Int32 GetSongCounter(Int32 spl_Playlistid)
        {
            string str = "";
            DataTable dtDetail = new DataTable();
            str = "select count(*) as Total from tbSpecialPlaylists_Titles where splPlaylistid= " + spl_Playlistid;
            dtDetail = objMainClass.fnFillDataTable(str);
            return Convert.ToInt32(dtDetail.Rows[0]["Total"]);
        }
        private void cmbSearchDealer_SelectedIndexChanged(object sender, EventArgs e)
        {

            string strState = "";
            strState = "select max(sf.Formatid) as Formatid , sf.formatname from tbSpecialFormat sf inner join tbSpecialPlaylistSchedule_Token st on st.formatid= sf.formatid";
            strState = strState + " inner join tbSpecialPlaylistSchedule sp on sp.pschid= st.pschid  where st.dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + " group by  sf.formatname";
            // objMainClass.fnFillComboBox(strState, cmbSearchFormat, "FormatId", "FormatName", "");
            InitilizeSplGrid();

            FillSaveData();

        }



        private void dgSpl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 10)
            {
                panSearch.Enabled = false;
                panMenu.Enabled = false;
                panEdit.Visible = true;
                panEdit.BringToFront();
                panEdit.Size = new Size(872, 243);
                panEdit.Location = new Point(
          this.panSearch.Width / 2 - panEdit.Size.Width / 2,
          this.panSearch.Height / 2 - panEdit.Size.Height / 2);
                ReturnSchId = Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                txtCustomer.Text = dgSpl.Rows[e.RowIndex].Cells["cName"].Value.ToString();
                txtFormat.Text = dgSpl.Rows[e.RowIndex].Cells["fName"].Value.ToString();
                txtPlaylist.Text = dgSpl.Rows[e.RowIndex].Cells["pName"].Value.ToString();
                dtpUpStartTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["stime"].Value)));
                dtpUpEndTime.Value = Convert.ToDateTime(string.Format(fi, "{0:HH:mm}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["etime"].Value)));
            }
            if (e.ColumnIndex == 11)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    DataTable dtDetail = new DataTable();
                    strDel = "select * from tbSpecialPlaylistSchedule_Token where pSchId= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    dtDetail = objMainClass.fnFillDataTable(strDel);
                    if ((dtDetail.Rows.Count > 0))
                    {
                        result = MessageBox.Show("This playlist is assigned to tokens. Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                    }

                    strDel = "delete from tbSpecialPlaylistSchedule_Weekday where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tbSpecialPlaylistSchedule_Token where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tbSpecialPlaylistSchedule where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    dgSpl.Rows.RemoveAt(e.RowIndex);

                }
            }
        }
        private void TokenCheckBoxClick(CheckBox HCheckBox)
        {
            IsClientCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgToken.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgToken.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsClientCheckBoxClicked = false;
        }
        private int GetWeekId()
        {
            if (chkMon.Checked == true)
            {
                return 1;
            }
            if (chkTue.Checked == true)
            {
                return 2;
            }
            if (chkWed.Checked == true)
            {
                return 3;
            }
            if (chkThu.Checked == true)
            {
                return 4;
            }
            if (chkFri.Checked == true)
            {
                return 5;
            }
            if (chkSat.Checked == true)
            {
                return 6;
            }
            if (chkSun.Checked == true)
            {
                return 7;
            }
            return 0;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            string returnValue = "";
            string strState = "";

            try
            {
                SqlCommand cmd = new SqlCommand("spSaveSpecialFormat", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
                if (btnSaveNew.Text == "Update")
                {
                    cmd.Parameters["@FormatId"].Value = Convert.ToInt32(cmbFormat.SelectedValue);
                }
                else
                {
                    cmd.Parameters["@FormatId"].Value = "0";
                }


                cmd.Parameters.Add(new SqlParameter("@FormatName", SqlDbType.VarChar));
                cmd.Parameters["@FormatName"].Value = txtName.Text;

                cmd.Parameters.Add(new SqlParameter("@R", SqlDbType.Int));
                cmd.Parameters["@R"].Value = lblR.Text;

                cmd.Parameters.Add(new SqlParameter("@G", SqlDbType.Int));
                cmd.Parameters["@G"].Value = lblG.Text;

                cmd.Parameters.Add(new SqlParameter("@B", SqlDbType.Int));
                cmd.Parameters["@B"].Value = lblB.Text;

                cmd.Parameters.Add(new SqlParameter("@DfClientId", SqlDbType.BigInt));
                cmd.Parameters["@DfClientId"].Value = Convert.ToInt32(0);

                cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                cmd.Parameters["@pVersion"].Value = "c";

                cmd.Parameters.Add(new SqlParameter("@sTime", SqlDbType.DateTime));
                cmd.Parameters["@sTime"].Value = string.Format("{0:hh:mm tt}", dtpsTime.Value);

                cmd.Parameters.Add(new SqlParameter("@eTime", SqlDbType.DateTime));
                cmd.Parameters["@eTime"].Value = string.Format("{0:hh:mm tt}", dtpeTime.Value);

                string startTime = string.Format("{0:hh:mm tt}", dtpsTime.Value);
                string endTime = string.Format("{0:hh:mm tt}", dtpeTime.Value);
                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                if (chk24Hour.Checked == false)
                {
                    cmd.Parameters.Add(new SqlParameter("@TotalHour", SqlDbType.Int));
                    cmd.Parameters["@TotalHour"].Value = duration.TotalHours;
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@TotalHour", SqlDbType.Int));
                    cmd.Parameters["@TotalHour"].Value = "24";
                }
                cmd.Parameters.Add(new SqlParameter("@Is24Hour", SqlDbType.Bit));
                cmd.Parameters["@Is24Hour"].Value = Convert.ToByte(chk24Hour.Checked);

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                returnValue = cmd.ExecuteScalar().ToString();
                if (returnValue != "-2")
                {

                    panMainNew.Visible = false;
                    if (btnSaveNew.Text == "Save")
                    {


                        MessageBox.Show("No special playlists for this format. Please create a special playlists.", "Management Panel");
                        this.mainForm.nameOfControlVisible2 = "";

                        // sprOpenForm(Application.ProductName + ".frmSpecialPlaylistFormat");


                    }

                }
                if (returnValue == "-2")
                {
                    MessageBox.Show("This format name already exists", "Management Panel");
                    // panMainNew.Visible = false;
                    //  lblCaption.Text = "";
                    txtName.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void sprOpenForm(string FormName)
        {
            string mlsFormName = FormName;
            try
            {

                FormType = Type.GetType(mlsFormName, true, true);
                ObjFormName = (Form)Activator.CreateInstance(FormType);
                ObjFormName.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                foreach (Form ChildForm in this.MdiChildren)
                {
                    if (ChildForm.Name == ObjFormName.Name)
                    {
                        ChildForm.Show();
                        ChildForm.Activate();
                        Application.DoEvents();
                        ChildForm.BringToFront();
                        ChildForm.WindowState = FormWindowState.Normal;
                        ChildForm.Dock = DockStyle.Fill;
                        return;
                    }
                }

                ObjFormName.MdiParent = this.MdiParent;
                ObjFormName.Show();
                Application.DoEvents();
                ObjFormName.BringToFront();
                ObjFormName.WindowState = FormWindowState.Normal;
                ObjFormName.Dock = DockStyle.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("This module is under process", "Under Construction");
            }

        }
        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            panMainNew.Visible = false;
            txtName.Text = "";
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {

        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSplPlaylists(cmbSplPlaylist, Convert.ToInt32(cmbFormat.SelectedValue));
            //FillSchData();
        }



        private void cmbDname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbDname.SelectedValue) == 0)
            {
                InitilizeGrid();
                return;
            }
            IsClientCheckBoxClicked = true;
            ClientCheckBox.Checked = false;
            FillData();
            TotalCheckBoxes = dgToken.RowCount;
            TotalCheckedCheckBoxes = 0;
            
             //   TickTokenFormat();
            

        }

         

        private void cmbSearchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = "select  max(tbSpecialPlaylists.splPlaylistid) as splPlaylistid, tbSpecialPlaylists.splPlaylistName from tbSpecialPlaylists ";
            str = str + " inner join tbSpecialPlaylistSchedule on tbSpecialPlaylistSchedule.splPlaylistid= tbSpecialPlaylists.splPlaylistid inner join tbSpecialPlaylistSchedule_Token on tbSpecialPlaylistSchedule_Token.pschid  = tbSpecialPlaylistSchedule.pschid ";
            str = str + " where tbSpecialPlaylists.formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and tbSpecialPlaylistSchedule.dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue);
            str = str + " group by tbSpecialPlaylists.splPlaylistid, tbSpecialPlaylists.splPlaylistName";
            //objMainClass.fnFillComboBox(str, cmbSearchPlaylist, "splPlaylistId", "splPlaylistName", "");
            InitilizeSplGrid();

            FillSaveData();

        }

        private void dgToken_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 9)
            {
                frmTokenInformation frm = new frmTokenInformation();
                StaticClass.DealerTokenId = 0;
                StaticClass.dealerUserId = Convert.ToInt32(dgToken.Rows[e.RowIndex].Cells["uId"].Value);
                StaticClass.DealerDfClientId = Convert.ToInt32(cmbDname.SelectedValue);
                StaticClass.DealerTokenId = Convert.ToInt32(dgToken.Rows[e.RowIndex].Cells["id"].Value);
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
                
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {

            string Localstr = "";
            Localstr = "select isnull(r,224) as R, isnull(g,224) as G , isnull(b,244) as B , stime, eTime,TotalHour,Is24Hour from tbSpecialFormat where formatid=  " + Convert.ToInt32(cmbFormat.SelectedValue);
            DataTable dtCommon = new DataTable();
            dtCommon = objMainClass.fnFillDataTable(Localstr);
            //Only for dealer exe
            //if ((dtCommon.Rows.Count > 0))
            //{
            //    if (Convert.ToInt32(dtCommon.Rows[0]["dfclientId"]) == 409)
            //    {
            //        MessageBox.Show("You have no permission to modify admin formats", "Management Panel");
            //        cmbFormat.Focus();
            //        return;
            //    }
            //}
            txtName.Focus();
            if (Convert.ToInt32(cmbFormat.SelectedValue) == 0)
            {
                txtName.Text = "";
                btnSaveNew.Text = "Save";
            }
            else
            {
                txtName.Text = cmbFormat.Text;
                btnSaveNew.Text = "Update";
            }
            if ((dtCommon.Rows.Count > 0))
            {
                lblR.Text = dtCommon.Rows[0]["R"].ToString();
                lblG.Text = dtCommon.Rows[0]["G"].ToString();
                lblB.Text = dtCommon.Rows[0]["B"].ToString();
                lblFormatColor.BackColor = Color.FromArgb(Convert.ToInt32(lblR.Text), Convert.ToInt32(lblG.Text), Convert.ToInt32(lblB.Text));
                chk24Hour.Checked = Convert.ToBoolean(dtCommon.Rows[0]["Is24Hour"]);
                dtpsTime.Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", dtCommon.Rows[0]["sTime"]));
                dtpeTime.Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", dtCommon.Rows[0]["eTime"]));

            }
            else
            {
                lblR.Text = "224";
                lblG.Text = "224";
                lblB.Text = "224";
                lblFormatColor.BackColor = Color.FromArgb(Convert.ToInt32(lblR.Text), Convert.ToInt32(lblG.Text), Convert.ToInt32(lblB.Text));
                chk24Hour.Checked = false;
                dtpsTime.Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", DateTime.Now));
                dtpeTime.Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", DateTime.Now));

            }

            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.BringToFront();
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            txtName.Focus();
        }
        int TotalHr = 0;
        private void cmbSchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
         
        private void DeSelectList()
        {
            ClientCheckBox.Checked = false;
            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                dgToken.Rows[i].Cells[1].Value = false;
            }
        }

        private void lblFormatDelete_Click(object sender, EventArgs e)
        {

            //DialogResult result;

            //if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
            //{
            //    MessageBox.Show("Please select a dealer name", "Management Panel");
            //    cmbSearchDealer.Focus();
            //    return;
            //}
            //if (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0)
            //{
            //    MessageBox.Show("Please select a format", "Management Panel");
            //    cmbSearchFormat.Focus();
            //    return;
            //}
            //result = MessageBox.Show("Are you sure to delete this format schedule?", "Management Panel", MessageBoxButtons.YesNo);
            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{
            //    string strDel = "";
            //    DataTable dtDetail = new DataTable();
            //    strDel = "select * from tbSpecialPlaylistSchedule_Token where pschid in (select pschid from  tbSpecialPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    dtDetail = objMainClass.fnFillDataTable(strDel);
            //    if ((dtDetail.Rows.Count > 0))
            //    {
            //        MessageBox.Show("This format cannot be deleted, as it is assigned to tokens", "Management Panel");
            //        return;
            //    }
            //    strDel = "delete from tbSpecialPlaylistSchedule_Weekday where pSchid in (select pSchid from  tbSpecialPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    strDel = "";
            //    strDel = "delete from tbSpecialPlaylistSchedule_Token where pSchid in (select pSchid from  tbSpecialPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    strDel = "";
            //    strDel = "delete from tbSpecialPlaylistSchedule where pSchid in (select pSchid from  tbSpecialPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    string str = "select * from tbSpecialFormat where formatid in(select distinct formatid from tbSpecialPlaylistSchedule where dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    objMainClass.fnFillComboBox(str, cmbSearchFormat, "FormatId", "FormatName", "");
            //    FillSaveData();

            //}
        }


        private void cmbSearchFormat_Click(object sender, EventArgs e)
        {
            //string str = "select * from tbSpecialFormat where formatid in(select distinct formatid from tbSpecialPlaylistSchedule where dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            // objMainClass.fnFillComboBox(str, cmbSearchFormat, "FormatId", "FormatName", "");
        }

        
        private void btnBack_Click(object sender, EventArgs e)
        {
            ReturnSchId = 0;
            panSearch.Enabled = true;
            panMenu.Enabled = true;
            panEdit.Visible = false;
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
            FillSaveData();
            ReturnSchId = 0;
            panSearch.Enabled = true;
            panMenu.Enabled = true;
            panEdit.Visible = false;
        }



        private void chkUpAll_CheckedChanged(object sender, EventArgs e)
        {

            //if (chkUpAll.Checked == true)
            //{

            //    chkUpSun.Checked = false;
            //    chkUpMon.Checked = false;
            //    chkUpTue.Checked = false;
            //    chkUpWed.Checked = false;
            //    chkUpThu.Checked = false;
            //    chkUpFri.Checked = false;
            //    chkUpSat.Checked = false;

            //    chkUpSun.Enabled = false;
            //    chkUpMon.Enabled = false;
            //    chkUpTue.Enabled = false;
            //    chkUpWed.Enabled = false;
            //    chkUpThu.Enabled = false;
            //    chkUpFri.Enabled = false;
            //    chkUpSat.Enabled = false;
            //}
            //else
            //{

            //    chkUpSun.Enabled = true;
            //    chkUpMon.Enabled = true;
            //    chkUpTue.Enabled = true;
            //    chkUpWed.Enabled = true;
            //    chkUpThu.Enabled = true;
            //    chkUpFri.Enabled = true;
            //    chkUpSat.Enabled = true;
            //}
        }

        private void UpdateMainData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spSaveSpecialPlaylistSchedule", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@pSchId", SqlDbType.BigInt));
                cmd.Parameters["@pSchId"].Value = ReturnSchId;

                cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
                cmd.Parameters["@pVersion"].Value = "c";

                cmd.Parameters.Add(new SqlParameter("@dfClientId", SqlDbType.BigInt));
                cmd.Parameters["@dfClientId"].Value = Convert.ToInt32(cmbSearchDealer.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
                cmd.Parameters["@splPlaylistId"].Value = Convert.ToInt32(cmbSearchPlaylist.SelectedValue);


                cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime));
                cmd.Parameters["@StartTime"].Value = string.Format(fi, "{0:hh:mm tt}", dtpUpStartTime.Value);

                cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
                cmd.Parameters["@EndTime"].Value = string.Format(fi, "{0:hh:mm tt}", dtpUpEndTime.Value);

                cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
                cmd.Parameters["@FormatId"].Value = Convert.ToInt32(cmbSearchFormat.SelectedValue);

                //cmd.Parameters.Add(new SqlParameter("@sHour", SqlDbType.Int));
                //cmd.Parameters["@sHour"].Value = string.Format(fi, "{0:HH}", dtpUpStartTime.Value);

                //cmd.Parameters.Add(new SqlParameter("@eHour", SqlDbType.Int));

                //if (string.Format(fi, "{0:hh:mm tt}", dtpUpEndTime.Value) == "12:00 AM")
                //{
                //    cmd.Parameters["@eHour"].Value = "24";
                //}
                //else if (dtpUpEndTime.Value < dtpUpStartTime.Value)
                //{
                //    if (string.Format(fi, "{0:hh:mm tt}", dtpUpEndTime.Value) != "12:00 AM")
                //    {
                //        cmd.Parameters["@eHour"].Value = GetEndHour(1, Convert.ToInt32(string.Format(fi, "{0:HH}", dtpUpEndTime.Value)));
                //    }
                //}


                //else
                //{
                //    cmd.Parameters["@eHour"].Value = string.Format(fi, "{0:HH}", dtpUpEndTime.Value);
                //}
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                ReturnSchId = Convert.ToInt32(cmd.ExecuteScalar());
                StaticClass.constr.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private Int32 GetEndHour(int dAy, int hOur)
        {
            SqlCommand cmd = new SqlCommand("GetHourDetail", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Day", SqlDbType.Int));
            cmd.Parameters["@Day"].Value = dAy;

            cmd.Parameters.Add(new SqlParameter("@Hour", SqlDbType.Int));
            cmd.Parameters["@Hour"].Value = hOur;

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());

        }

        private void lblFormatColor_Click(object sender, EventArgs e)
        {
            ColorDialog cld = new ColorDialog();
            if (cld.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblFormatColor.BackColor = cld.Color;
                lblR.Text = cld.Color.R.ToString();
                lblG.Text = cld.Color.G.ToString();
                lblB.Text = cld.Color.B.ToString();
            }
        }



        private void btnMenuSearch_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panSearch.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
            panAssign.Visible = false;
        }

        private void btnMenuAddNew_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuAddNew);
            panSearch.Visible = false;
            panAddNew.Dock = DockStyle.Fill;
            panAddNew.Visible = true;
            panAssign.Visible = false;
        }

        private void btnMenuAssign_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuAssign);
            panSearch.Visible = false;
            panAssign.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
            panAssign.Visible = true;
        }
        private void SetButtonColor(Button btnName)
        {
            Color light = Color.FromName("ControlLightLight");
            Color bLight = Color.FromName("Control");
            btnMenuSearch.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAddNew.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAssign.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnName.BackColor = Color.White;
        }

        private void cmbSplPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panMainNew_VisibleChanged(object sender, EventArgs e)
        {
            if (panMainNew.Visible == true)
            {
                panNew.Location = new Point(
         this.panMainNew.Width / 2 - panNew.Size.Width / 2,
         this.panMainNew.Height / 2 - panNew.Size.Height / 2);
            }
        }

        private void chk24Hour_CheckedChanged(object sender, EventArgs e)
        {
            if (chk24Hour.Checked == true)
            {
                dtpeTime.Enabled = false;
                dtpsTime.Enabled = false;
                dtpeTime.Value = Convert.ToDateTime("00:00");
                dtpsTime.Value = Convert.ToDateTime("00:00");

            }
            else
            {
                dtpeTime.Enabled = true;
                dtpsTime.Enabled = true;
                dtpeTime.Value = DateTime.Now;
                dtpsTime.Value = DateTime.Now;
            }
        }

        private void cmbDname_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbDname, "DFClientID", "ClientName", "");
        }

        private void cmbSearchPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitilizeSplGrid();

            FillSaveData();


        }

        private void cmbPublishCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbPublishCustomer, "DFClientID", "ClientName", "");
        }

        private void InitilizePublishGrid()
        {
            if (dgPublish.Rows.Count > 0)
            {
                dgPublish.Rows.Clear();
            }
            if (dgPublish.Columns.Count > 0)
            {
                dgPublish.Columns.Clear();
            }
            dgPublish.Dock = DockStyle.Fill;
            //0
            dgPublish.Columns.Add("Id", "Id");
            dgPublish.Columns["Id"].Width = 0;
            dgPublish.Columns["Id"].Visible = false;
            dgPublish.Columns["Id"].ReadOnly = true;
            //1
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "";
            chk.DataPropertyName = "IsChecked";
            dgPublish.Columns.Add(chk);
            chk.Width = 50;
            chk.Visible = true;
            dgPublish.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewCheckBoxColumn chkDelete = new DataGridViewCheckBoxColumn();
            chkDelete.HeaderText = "Delete Songs";
            chkDelete.DataPropertyName = "IsChecked";
            dgPublish.Columns.Add(chkDelete);
            chkDelete.Width = 150;
            chkDelete.Visible = true;
            dgPublish.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            //2
            dgPublish.Columns.Add("tNo", "Token No");
            dgPublish.Columns["tNo"].Width = 200;
            dgPublish.Columns["tNo"].Visible = true;
            dgPublish.Columns["tNo"].ReadOnly = true;
            dgPublish.Columns["tNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("pName", "Name");
            dgPublish.Columns["pName"].Width = 250;
            dgPublish.Columns["pName"].Visible = true;
            dgPublish.Columns["pName"].ReadOnly = true;
            dgPublish.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("loc", "Location");
            dgPublish.Columns["loc"].Width = 150;
            dgPublish.Columns["loc"].Visible = true;
            dgPublish.Columns["loc"].ReadOnly = true;
            dgPublish.Columns["loc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("cName", "City");
            dgPublish.Columns["cName"].Width = 150;
            dgPublish.Columns["cName"].Visible = true;
            dgPublish.Columns["cName"].ReadOnly = true;
            dgPublish.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("sName", "State");
            dgPublish.Columns["sName"].Width = 150;
            dgPublish.Columns["sName"].Visible = true;
            dgPublish.Columns["sName"].ReadOnly = true;
            dgPublish.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("coName", "Country");
            dgPublish.Columns["coName"].Width = 150;
            dgPublish.Columns["coName"].Visible = true;
            dgPublish.Columns["coName"].ReadOnly = true;
            dgPublish.Columns["coName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgPublish.Columns.Add("ver", "Type");
            dgPublish.Columns["ver"].Width = 100;
            dgPublish.Columns["ver"].Visible = true;
            dgPublish.Columns["ver"].ReadOnly = true;
            dgPublish.Columns["ver"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

             
            dgPublish.Columns.Add("uId", "uId");
            dgPublish.Columns["uId"].Width = 0;
            dgPublish.Columns["uId"].Visible = false;
            dgPublish.Columns["uId"].ReadOnly = true;

        }

        private void cmbPublishCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPublishCustomer.SelectedValue) == 0)
            {
                InitilizePublishGrid();
                return;
            }
            IsPublishCheckBoxClicked = true;
            PublishCheckBox.Checked = false;
            FillPublishData();
            TotalCheckBoxesPublish = dgPublish.RowCount;
            TotalCheckedCheckBoxesPublish = 0;
        }
        private void FillPublishData()
        {
            string sQr = "";

            sQr = "GetPublishTokenInfo " + Convert.ToInt32(cmbPublishCustomer.SelectedValue);

            DataTable dtDetail = new DataTable();
            InitilizePublishGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgPublish.Rows.Add();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["tokenid"];
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells[1].Value = 1;
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells[2].Value = 0;
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["tNo"].Value = dtDetail.Rows[i]["tNo"].ToString();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["PersonName"].ToString();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["loc"].Value = dtDetail.Rows[i]["Location"].ToString();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["CityName"].ToString();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StateName"].ToString();
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["coName"].Value = dtDetail.Rows[i]["CountryName"].ToString();
                    if (Convert.ToBoolean(dtDetail.Rows[i]["IsStore"]) == true)
                    {
                        dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["ver"].Value = "Store";
                    }
                    else
                    {
                        dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["ver"].Value = "Stream";
                    }
                    dgPublish.Rows[dgPublish.Rows.Count - 1].Cells["uId"].Value = dtDetail.Rows[i]["userid"].ToString();

                }
            }
            


        }


        private void PublishCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                PublishCheckBoxClick((CheckBox)sender);
        }
        private void PublishCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            PublishCheckBoxClick((CheckBox)sender);
        }
        private void PublishCheckBoxClick(CheckBox HCheckBox)
        {
            IsPublishCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgPublish.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgPublish.RefreshEdit();

            TotalCheckedCheckBoxesPublish = HCheckBox.Checked ? TotalCheckBoxesPublish : 0;

            IsPublishCheckBoxClicked = false;
        }


        private void dgPublish_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsPublishCheckBoxClicked)
                RowCheckBoxClickPublish((DataGridViewCheckBoxCell)dgPublish[e.ColumnIndex, e.RowIndex]);
        }

        private void dgPublish_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgPublish.CurrentCell is DataGridViewCheckBoxCell)
                dgPublish.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgPublish_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
                    if (e.RowIndex == -1 && e.ColumnIndex == 1)
               ResetHeaderCheckBoxLocationPublish(e.ColumnIndex, e.RowIndex);
        }


        private void AddPublishCheckBox(DataGridView dgPublish)
        {
            PublishCheckBox = new CheckBox();
            PublishCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgPublish.Controls.Add(PublishCheckBox);

        }
        private void ResetHeaderCheckBoxLocationPublish(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgPublish.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - PublishCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - PublishCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            PublishCheckBox.Location = oPoint;
        }

        private void RowCheckBoxClickPublish(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesPublish < TotalCheckBoxesPublish)
                    TotalCheckedCheckBoxesPublish++;
                else if (TotalCheckedCheckBoxesPublish > 0)
                    TotalCheckedCheckBoxesPublish--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesPublish < TotalCheckBoxesPublish)
                    PublishCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesPublish == TotalCheckBoxesPublish)
                    PublishCheckBox.Checked = true;
            }
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPublishCustomer.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a customer name", "Management Panel");
                cmbPublishCustomer.Focus();
                return  ;
            }
            if (CheckGridValidationAdvt(dgPublish) == false)
            {
                MessageBox.Show("Please select token no's from list", "Management Panel");
                return  ;
            }

            for (int i = 0; i < dgPublish.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dgPublish.Rows[i].Cells[2].Value) == true)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = StaticClass.constr;
                    cmd1.CommandText = "update AMPlayerTokens set IsDeleteSong=1 where tokenid=" + Convert.ToInt32(dgPublish.Rows[i].Cells["Id"].Value) + "";
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();
                }

                if (Convert.ToBoolean(dgPublish.Rows[i].Cells[1].Value) == true)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = StaticClass.constr;
                    cmd1.CommandText = "update AMPlayerTokens set isPublish=1,IsPublishUpdate=0 where tokenid=" + Convert.ToInt32(dgPublish.Rows[i].Cells["Id"].Value) + "";
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();
                }
            }
            InitilizePublishGrid();
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value) == "12:00 AM")
            {
                dtpEndTime.Value = dtpEndTime.Value.AddMinutes(-1);
            }
        }
    }
}
