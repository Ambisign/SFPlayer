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
    public partial class frmTempPlaylistSchedule : Form
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

        
        private frmMain mainForm = null;
        public frmTempPlaylistSchedule(Form callingForm)
        {
            mainForm = callingForm as frmMain;
            InitializeComponent();
        }
        public frmTempPlaylistSchedule()
        {
            InitializeComponent();

        }


        private void FillSplPlaylists(ComboBox cmbName, Int32 ForId)
        {
            string str = "";
            str = "select  tbSpecialPlaylists.splPlaylistid, (tbSpecialPlaylists.splPlaylistName+ ' (' +convert(varchar(50), count(*) ) + ')' ) as splPlaylistName from tbSpecialPlaylists ";
            str = str + " inner join tbSpecialPlaylists_Titles on tbSpecialPlaylists_Titles.splPlaylistid = tbSpecialPlaylists.splPlaylistid";
            str = str + " where tbSpecialPlaylists.formatid=" + ForId + " ";
            str = str + " group by tbSpecialPlaylists.splPlaylistid, tbSpecialPlaylists.splPlaylistName  order by tbSpecialPlaylists.splPlaylistName";
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


       
            strState = "select * from tbgroup order by groupname";
            objMainClass.fnFillComboBox(strState, cmbGrpName, "groupId", "groupname", "");

        }
        private void frmSpecialPlaylists_Load(object sender, EventArgs e)
        {
            fi.AMDesignator = "AM";
            fi.PMDesignator = "PM";
            AddClientCheckBox(dgToken);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);

             

            dtpStartTime.Value = Convert.ToDateTime(string.Format(fi, "{0:hh:mm tt}", DateTime.Now));
            dtpEndTime.Value = Convert.ToDateTime(string.Format(fi, "{0:hh:mm tt}", DateTime.Now));

            dtpFromDate.Value =  DateTime.Now;
            dtpToDate.Value =  DateTime.Now;

            InitilizeGrid();
            InitilizeSplGrid();
            FillFormat();
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panSearch.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
             

             

            tbcMain.ColumnStyles[0].Width = 0;
            tbcMain.ColumnStyles[1].Width = 0;
            tbcMain.ColumnStyles[2].Width = 0;
            tbcMain.ColumnStyles[3].Width = 0;
            tbcMain.ColumnStyles[4].Width = 0;
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
            string stateId = "";
            string CityId = "";
            string CountryId = "";
            string Location = "";
            string Pincode = "";
            string vQry = " where AMPlayerTokens.clientid ="+ Convert.ToInt32(cmbDname.SelectedValue) ;


            CountryId = GetReturnId(dgCountry);
            stateId = GetReturnId(dgState);
            CityId = GetReturnId(dgCity);
            Location = GetReturnName(dgLocation);
            Pincode = GetReturnName(dgLocation);

            if ((chkState.Checked == true) && (stateId == ""))
            {
                InitilizeGrid();
                return;
            }
            if ((chkCity.Checked == true) && (CityId == ""))
            {
                InitilizeGrid();
                return;
            }
            if ((chkCountry.Checked == true) && (CountryId == ""))
            {
                InitilizeGrid();
                return;
            }
            if ((chkLocation.Checked == true) && (Location == ""))
            {
                InitilizeGrid();
                return;
            }
            if ((chkPinCode.Checked == true) && (Pincode == ""))
            {
                InitilizeGrid();
                return;
            }

            if (Convert.ToInt32(cmbGrpName.SelectedValue) !=0)
            {
                vQry = vQry + " and AMPlayerTokens.GroupId in( " + Convert.ToInt32(cmbGrpName.SelectedValue) + " )";
            }
            if ((stateId != "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.stateid in( " + stateId + " )";
            }

            if (CityId != "")
            {
                vQry = vQry + " and AMPlayerTokens.cityid in( " + CityId + " )";
            }
            if ((CountryId != "") && (stateId == "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.CountryId in( " + CountryId + " )";
            }

            if (Location != "")
            {
                vQry = vQry + " and AMPlayerTokens.Location in( " + Location + " )";
            }
            if (Pincode != "")
            {
                vQry = vQry + " and AMPlayerTokens.Location in( " + Pincode + " )";
            }

            sQr = "GetTokenInfoNew 0 ,'" + vQry + "'";

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

            dgToken.Columns.Add("pName", "Name");
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
            dgToken.Columns["cName"].Visible = false;
            dgToken.Columns["cName"].ReadOnly = true;
            dgToken.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("sName", "State");
            dgToken.Columns["sName"].Width = 150;
            dgToken.Columns["sName"].Visible = false;
            dgToken.Columns["sName"].ReadOnly = true;
            dgToken.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("coName", "Country");
            dgToken.Columns["coName"].Width = 150;
            dgToken.Columns["coName"].Visible = false;
            dgToken.Columns["coName"].ReadOnly = true;
            dgToken.Columns["coName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgToken.Columns.Add("ver", "Type");
            dgToken.Columns["ver"].Width = 100;
            dgToken.Columns["ver"].Visible = false;
            dgToken.Columns["ver"].ReadOnly = true;
            dgToken.Columns["ver"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn ModifyToken = new DataGridViewLinkColumn();
            ModifyToken.HeaderText = "Modify";
            ModifyToken.Text = "Modify";
            ModifyToken.DataPropertyName = "Modify";
            dgToken.Columns.Add(ModifyToken);
            ModifyToken.UseColumnTextForLinkValue = true;
            ModifyToken.Width = 70;
            ModifyToken.Visible = false;
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
            if (dtpToDate.Value < dtpFromDate.Value)
            {
                MessageBox.Show("Please select proper date", "Management Panel");
                dtpFromDate.Focus();
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
            //    strDealerTimeValid = "select * from tbSpecialTempPlaylistSchedule  where pversion='" + cmbPlayerType.Text + "' and dfclientid=" + Convert.ToInt32(cmbDealer.SelectedValue) + " and formatid=" + Convert.ToInt32(cmbFormat.SelectedValue) + " and splPlaylistId=" + Convert.ToInt32(cmbSplPlaylist.SelectedValue) + " ";
            //}
            //else
            //{
            //    strDealerTimeValid = "select * from tbSpecialTempPlaylistSchedule  where pversion='" + cmbPlayerType.Text + "' and dfclientid=" + Convert.ToInt32(cmbDealer.SelectedValue) + " and formatid=" + Convert.ToInt32(cmbFormat.SelectedValue) + " and splPlaylistId=" + Convert.ToInt32(cmbSplPlaylist.SelectedValue) + " ";
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
                    strTit = "CheckTokenTempSchedule " + Convert.ToInt32(cmbDname.SelectedValue) + "," + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + "," + dbWId + ",'" + string.Format(fi, "{0:hh:mm tt}", dtpStartTime.Value.AddMinutes(1)) + "','" + string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value.AddMinutes(-1)) + "','" + string.Format(fi, "{0:dd-MMM-yyyy}", dtpFromDate.Value) + "','" + string.Format(fi, "{0:dd-MMM-yyyy}", dtpToDate.Value) + "'";
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
                            strTit = "delete from tbSpecialTempPlaylistSchedule_Token where pSchId=" + dtPrv.Rows[iTit]["pSchId"] + " and tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and dfclientid= " + Convert.ToInt32(cmbDname.SelectedValue) + "";
                            cmdTit.CommandText = strTit;
                            cmdTit.ExecuteNonQuery();
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
                    cmd1.CommandText = "delete from tbSpecialTempPlaylistSchedule_Weekday where pSchId=" + rtPschId;
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
                    cmd1.CommandText = "delete from tbSpecialTempPlaylistSchedule_Token where tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and pSchId= " + rtPschId + " ";
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
                SqlCommand cmd = new SqlCommand("spSaveSpecialTemporaryPlaylistSchedule", StaticClass.constr);
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

                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime));
                cmd.Parameters["@StartDate"].Value = string.Format(fi, "{0:dd/MMM/yyyy}", dtpFromDate.Value);

                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime));
                cmd.Parameters["@EndDate"].Value = string.Format(fi, "{0:dd/MMM/yyyy}", dtpToDate.Value);
                if (chkReplaceAll.Checked == true)
                {
                    cmd.Parameters.Add(new SqlParameter("@IsReplaceAll", SqlDbType.Int));
                    cmd.Parameters["@IsReplaceAll"].Value = 1;
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@IsReplaceAll", SqlDbType.Int));
                    cmd.Parameters["@IsReplaceAll"].Value = 0;
                }

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
            SqlCommand cmd = new SqlCommand("spSaveSpecialTempPlaylistSchedule_Week", StaticClass.constr);
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
                    cmd1.CommandText = "delete from tbSpecialTempPlaylistSchedule_Token where tokenid=" + Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) + " and pSchId= " + pSchId + " ";
                    cmd1.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    SaveTokenDetail(Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0, pSchId);

                }
            }
        }
        private void SaveTokenDetail(Int32 TokenId, int IsAllToken, Int32 pSchId)
        {
            SqlCommand cmd = new SqlCommand("spSaveSpecialTempPlaylistSchedule_Token", StaticClass.constr);
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

            dgSpl.Columns.Add("sDate", "Start Date");
            dgSpl.Columns["sDate"].Width = 0;
            dgSpl.Columns["sDate"].Visible = false;
            dgSpl.Columns["sDate"].ReadOnly = true;
            dgSpl.Columns["sDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("eDate", "End Date");
            dgSpl.Columns["eDate"].Width = 0;
            dgSpl.Columns["eDate"].Visible = false;
            dgSpl.Columns["eDate"].ReadOnly = true;
            dgSpl.Columns["eDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void FillSaveData()
        {
            string sQr = "";

            sQr = "GetCustomerPlaylistTempSchedule '" + GenrateQuery() + "'";

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
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["StartTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["EndTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Value = GetWeekName(Convert.ToInt32(dtDetail.Rows[i]["pSchId"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["tokenid"].Value = dtDetail.Rows[i]["MainTokenid"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sDate"].Value = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dtDetail.Rows[i]["Startdate"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eDate"].Value = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dtDetail.Rows[i]["Enddate"]));

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
                return " where ''"+ string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and  formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by tokenid, personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " order by  tokenid, personname ,  StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by  tokenid, personname ,  StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by   tokenid, personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue).ToString() + " order by  tokenid,  personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) != 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) == 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue).ToString() + " order by tokenid,   personname , StartTime";
            }
            if ((Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchFormat.SelectedValue) == 0) && (Convert.ToInt32(cmbSearchPlaylist.SelectedValue) != 0))
            {
                return " where ''" + string.Format(fi, "{0:dd-MMM-yyyy}", DateTime.Now) + "''  between startdate and enddate  and splPlaylistid=" + Convert.ToInt32(cmbSearchPlaylist.SelectedValue).ToString() + " order by tokenid,  personname ,  StartTime";
            }
            return "";
        }
        private string GetWeekName(Int32 pSchId)
        {

            string str = "";
            DataTable dtDetail = new DataTable();
            str = "SELECT pSchId, STUFF((SELECT ', ' +iif(wId=1,'Mon',iif(wid=2,'Tue',iif(wid=3,'Wed',iif(wid=4,'Thu',iif(wid=5,'Fri',iif(wid=6,'Sat',iif(wid=7,'Sun','All'))))))) FROM tbSpecialTempPlaylistSchedule_WeekDay A";
            str = str + " Where A.pSchId=B.pSchId FOR XML PATH('')),1,1,'') As wName ";
            str = str + " From tbSpecialTempPlaylistSchedule_WeekDay B ";
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
            strState = "select max(sf.Formatid) as Formatid , sf.formatname from tbSpecialFormat sf inner join tbSpecialTempPlaylistSchedule_Token st on st.formatid= sf.formatid";
            strState = strState + " inner join tbSpecialTempPlaylistSchedule sp on sp.pschid= st.pschid  where st.dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + " group by  sf.formatname";
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
                dtpUpStartTime.Value = Convert.ToDateTime(string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["stime"].Value)));
                dtpUpEndTime.Value = Convert.ToDateTime(string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["etime"].Value)));
                dtpUpFromDate.Value = Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["sDate"].Value);
                dtpUpToDate.Value = Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["eDate"].Value);
            }
            if (e.ColumnIndex == 11)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    DataTable dtDetail = new DataTable();
                    strDel = "select * from tbSpecialTempPlaylistSchedule_Token where pSchId= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    dtDetail = objMainClass.fnFillDataTable(strDel);
                    if ((dtDetail.Rows.Count > 0))
                    {
                        result = MessageBox.Show("This playlist is assigned to tokens. Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                    }

                    strDel = "delete from tbSpecialTempPlaylistSchedule_Weekday where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tbSpecialTempPlaylistSchedule_Token where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    strDel = "";
                    strDel = "delete from tbSpecialTempPlaylistSchedule where pSchid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["ID"].Value);
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
            str = str + " inner join tbSpecialTempPlaylistSchedule on tbSpecialTempPlaylistSchedule.splPlaylistid= tbSpecialPlaylists.splPlaylistid inner join tbSpecialTempPlaylistSchedule_Token on tbSpecialTempPlaylistSchedule_Token.pschid  = tbSpecialTempPlaylistSchedule.pschid ";
            str = str + " where tbSpecialPlaylists.formatid=" + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and tbSpecialTempPlaylistSchedule.dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue);
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
            //    strDel = "select * from tbSpecialTempPlaylistSchedule_Token where pschid in (select pschid from  tbSpecialTempPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    dtDetail = objMainClass.fnFillDataTable(strDel);
            //    if ((dtDetail.Rows.Count > 0))
            //    {
            //        MessageBox.Show("This format cannot be deleted, as it is assigned to tokens", "Management Panel");
            //        return;
            //    }
            //    strDel = "delete from tbSpecialTempPlaylistSchedule_Weekday where pSchid in (select pSchid from  tbSpecialTempPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    strDel = "";
            //    strDel = "delete from tbSpecialTempPlaylistSchedule_Token where pSchid in (select pSchid from  tbSpecialTempPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    strDel = "";
            //    strDel = "delete from tbSpecialTempPlaylistSchedule where pSchid in (select pSchid from  tbSpecialTempPlaylistSchedule where formatid= " + Convert.ToInt32(cmbSearchFormat.SelectedValue) + " and pversion='" + cmbSearchPlayerVersion.Text + "' and dfclientid= " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //    StaticClass.constr.Open();
            //    cmd = new SqlCommand(strDel, StaticClass.constr);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.ExecuteNonQuery();
            //    StaticClass.constr.Close();

            //    string str = "select * from tbSpecialFormat where formatid in(select distinct formatid from tbSpecialTempPlaylistSchedule where dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
            //    objMainClass.fnFillComboBox(str, cmbSearchFormat, "FormatId", "FormatName", "");
            //    FillSaveData();

            //}
        }


        private void cmbSearchFormat_Click(object sender, EventArgs e)
        {
            //string str = "select * from tbSpecialFormat where formatid in(select distinct formatid from tbSpecialTempPlaylistSchedule where dfclientid=" + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ")";
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
                SqlCommand cmd = new SqlCommand("spSaveSpecialTemporaryPlaylistSchedule", StaticClass.constr);
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

                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime));
                cmd.Parameters["@StartDate"].Value = string.Format(fi, "{0:dd/MMM/yyyy}", dtpUpFromDate.Value);

                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime));
                cmd.Parameters["@EndDate"].Value = string.Format(fi, "{0:dd/MMM/yyyy}", dtpUpToDate.Value);
                if (chkReplaceAll.Checked == true)
                {
                    cmd.Parameters.Add(new SqlParameter("@IsReplaceAll", SqlDbType.Int));
                    cmd.Parameters["@IsReplaceAll"].Value = 1;
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@IsReplaceAll", SqlDbType.Int));
                    cmd.Parameters["@IsReplaceAll"].Value = 0;
                }

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
             
        }

        private void btnMenuAddNew_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuAddNew);
            panSearch.Visible = false;
            panAddNew.Dock = DockStyle.Fill;
            panAddNew.Visible = true;
            
        }

         
        private void SetButtonColor(Button btnName)
        {
            Color light = Color.FromName("ControlLightLight");
            Color bLight = Color.FromName("Control");
            btnMenuSearch.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAddNew.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
             
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

         

       

            
        private void InitilizeGrid(DataGridView dgGrid, string DisplayName)
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.Rows.Clear();
            }
            if (dgGrid.Columns.Count > 0)
            {
                dgGrid.Columns.Clear();
            }
            dgGrid.Dock = DockStyle.Fill;
            //0
            dgGrid.Columns.Add("Id", "Id");
            dgGrid.Columns["Id"].Width = 0;
            dgGrid.Columns["Id"].Visible = false;
            dgGrid.Columns["Id"].ReadOnly = true;
            //1
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "";
            chk.DataPropertyName = "IsChecked";
            dgGrid.Columns.Add(chk);
            chk.Width = 50;
            chk.Visible = true;
            dgGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //2
            dgGrid.Columns.Add("Name", DisplayName);
            dgGrid.Columns["Name"].Width = 200;
            dgGrid.Columns["Name"].Visible = true;
            dgGrid.Columns["Name"].ReadOnly = true;
            dgGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void FillDataParamter(DataGridView dgGrid, string DisplayName, string sQr)
        {


            DataTable dtDetail = new DataTable();
            InitilizeGrid(dgGrid, DisplayName);

            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgGrid.Rows.Add();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["Id"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells[1].Value = 0;
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Name"].Value = dtDetail.Rows[i]["DisplayName"].ToString();
                }
            }
             
        }
        private void FillCountry()
        {

            //IsCountryCheckBoxClicked = true;
            //CountryCheckBox.Checked = false;
            string strCou = "";
            strCou = "";
            strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
            strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
            strCou = strCou + " order by CountryCodes.CountryName";

            FillDataParamter(dgCountry, "Country Name", strCou);
        }
        private string GetReturnId(DataGridView dgGrid)
        {
            string ReturnId = "";
            dgGrid.EndEdit();
            for (int i = 0; i < dgGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgGrid.Rows[i].Cells[1].Value) == true)
                {
                    if (ReturnId == "")
                    {
                        ReturnId = dgGrid.Rows[i].Cells["Id"].Value.ToString();
                    }
                    else
                    {
                        ReturnId = ReturnId + "," + dgGrid.Rows[i].Cells["Id"].Value.ToString();
                    }
                }
            }
            return ReturnId;
        }

        private string GetReturnName(DataGridView dgGrid)
        {
            string ReturnId = "";
            dgGrid.EndEdit();
            for (int i = 0; i < dgGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgGrid.Rows[i].Cells[1].Value) == true)
                {
                    if (ReturnId == "")
                    {
                        ReturnId= "''"+ dgGrid.Rows[i].Cells["Name"].Value.ToString() + "''";
                    }
                    else
                    {
                        ReturnId = ReturnId + ",''"+ dgGrid.Rows[i].Cells["Name"].Value.ToString() + "''";
                    }
                }
            }
            return ReturnId;
        }
        private void FillCountryState()
        {
            string CountryId = "";
            CountryId = GetReturnId(dgCountry);
            if (CountryId == "")
            {
                InitilizeGrid(dgState, "State Name");
                InitilizeGrid(dgCity, "City Name");
                InitilizeGrid();
                return;
            }
            InitilizeGrid(dgCity, "City Name");
            InitilizeGrid();
            string str = "";
            str = "";
            str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
            str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
            str = str + " where   ";

            str = str + "   tbState.CountryId in (" + CountryId + " ) ";
            str = str + " order by StateName ";

            FillDataParamter(dgState, "State Name", str);

        }
        private void FillStateCity()
        {
             
            string StateId = "";
            StateId = GetReturnId(dgState);
            if (StateId == "")
            {
                InitilizeGrid(dgCity, "City Name");
                return;
            }
            string str = "";
            str = "";
            str = "SELECT distinct tbCity.CityId as Id, tbCity.CityName as DisplayName FROM AMPlayerTokens  ";
            str = str + " INNER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId ";
            str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
            str = str + " where ";

            str = str + "   tbCity.StateId in( " + StateId + " ) ";
            str = str + " order by tbCity.CityName ";
            FillDataParamter(dgCity, "City Name", str);
            

             
        }



        private void chkCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCountry.Checked == true)
            {
                FillCountry();
                tbcMain.ColumnStyles[0].Width = 15;
                InitilizeGrid();
            }
            else
            {
                tbcMain.ColumnStyles[0].Width = 0;
                InitilizeGrid(dgCountry, "Country Name");
                FillCountryState();
            }
            if (chkPinCode.Checked == true)
            {
                FillPincode();
            }
            if (chkLocation.Checked == true)
            {
                FillLocations();
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
            {
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
            }
        }

        private void chkState_CheckedChanged(object sender, EventArgs e)
        {
            if (chkState.Checked == true)
            {
                FillCountryState();
                tbcMain.ColumnStyles[1].Width = 15;
                
            }
            else
            {
                InitilizeGrid(dgState, "State Name");
                tbcMain.ColumnStyles[1].Width = 0;
                FillStateCity();
            }
            if (chkPinCode.Checked == true)
            {
                FillPincode();
            }
            if (chkLocation.Checked == true)
            {
                FillLocations();
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
            {
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
            }
        }

        private void chkCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCity.Checked == true)
            {
                FillStateCity();
                tbcMain.ColumnStyles[2].Width = 15;
            }
            else
            {
                InitilizeGrid(dgCity, "City Name");
                tbcMain.ColumnStyles[2].Width = 0;
            }
            if (chkPinCode.Checked == true)
            {
                FillPincode();
            }
            if (chkLocation.Checked == true)
            {
                FillLocations();
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
            {
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
            }
        }

        private void dgCountry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (chkState.Checked == true)
                {
                    FillCountryState();
                }
                if (chkPinCode.Checked == true)
                {
                    FillPincode();
                }
                if (chkLocation.Checked == true)
                {
                    FillLocations();
                }
                if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
                {
                    IsClientCheckBoxClicked = true;
                    ClientCheckBox.Checked = false;
                    FillData();
                }
            }
        }

        private void dgState_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (chkCity.Checked == true)
                {
                    FillStateCity();
                }
                if (chkPinCode.Checked == true)
                {
                    FillPincode();
                }
                if (chkLocation.Checked == true)
                {
                    FillLocations();
                }
                if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
                {
                    IsClientCheckBoxClicked = true;
                    ClientCheckBox.Checked = false;
                    FillData();
                }
            }
        }

        private void dgCity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (chkPinCode.Checked == true)
                {
                    FillPincode();
                }
                if (chkLocation.Checked == true)
                {
                    FillLocations();
                }

                if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
                {
                    IsClientCheckBoxClicked = true;
                    ClientCheckBox.Checked = false;
                    FillData();
                }
            }
        }

        private void cmbGrpName_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            IsClientCheckBoxClicked = true;
            ClientCheckBox.Checked = false;
            FillData();
            TotalCheckBoxes = dgToken.RowCount;
            TotalCheckedCheckBoxes = 0;

        }

        private void chkLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocation.Checked == true)
            {
                FillLocations();
                tbcMain.ColumnStyles[3].Width = 15;
            }
            else
            {
                InitilizeGrid(dgLocation, "Location");
                tbcMain.ColumnStyles[3].Width = 0;
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
            {
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
            }
        }
        private void FillLocations()
        {
            string sQr = "";
            string stateId = "";
            string CityId = "";
            string CountryId = "";
            string vQry = "  and AMPlayerTokens.clientid =" + Convert.ToInt32(cmbDname.SelectedValue);


            CountryId = GetReturnId(dgCountry);
            stateId = GetReturnId(dgState);
            CityId = GetReturnId(dgCity);

            if ((chkState.Checked == true) && (stateId == ""))
            {
                InitilizeGrid(dgLocation, "Location");
                return;
            }
            if ((chkCity.Checked == true) && (CityId == ""))
            {
                InitilizeGrid(dgLocation, "Location");
                return;
            }
            if ((chkCountry.Checked == true) && (CountryId == ""))
            {
                InitilizeGrid(dgLocation, "Location");
                return;
            }
            if ((chkState.Checked == false) && (chkCity.Checked == false) && (chkCountry.Checked == false))
            {
                InitilizeGrid(dgLocation, "Location");
                return;
            }
            if (Convert.ToInt32(cmbGrpName.SelectedValue) != 0)
            {
                vQry = vQry + " and AMPlayerTokens.GroupId in( " + Convert.ToInt32(cmbGrpName.SelectedValue) + " )";
            }
            if ((stateId != "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.stateid in( " + stateId + " )";
            }

            if (CityId != "")
            {
                vQry = vQry + " and AMPlayerTokens.cityid in( " + CityId + " )";
            }
            if ((CountryId != "") && (stateId == "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.CountryId in( " + CountryId + " )";
            }



            sQr = "select location as DisplayName, ROW_NUMBER() over (ORDER BY location) AS id from( ";
            sQr= sQr + " select distinct location from AMPlayerTokens where location is not null  " + vQry + "  ) as a order by location ";
            FillDataParamter(dgLocation, "Location", sQr);


        }
       

        private void dgLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
                {
                    IsClientCheckBoxClicked = true;
                    ClientCheckBox.Checked = false;
                    FillData();
                }
            }
        }
        private void chkPinCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPinCode.Checked == true)
            {
                FillPincode();
                tbcMain.ColumnStyles[4].Width = 15;
            }
            else
            {
                InitilizeGrid(dgLocation, "Pincode");
                tbcMain.ColumnStyles[4].Width = 0;
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
            {
                IsClientCheckBoxClicked = true;
                ClientCheckBox.Checked = false;
                FillData();
            }
        }
        private void FillPincode()
        {
            string sQr = "";
            string stateId = "";
            string CityId = "";
            string CountryId = "";
            string vQry = "  and AMPlayerTokens.clientid =" + Convert.ToInt32(cmbDname.SelectedValue);


            CountryId = GetReturnId(dgCountry);
            stateId = GetReturnId(dgState);
            CityId = GetReturnId(dgCity);

            if ((chkState.Checked == true) && (stateId == ""))
            {
                InitilizeGrid(dgPinCode, "Pincode");
                return;
            }
            if ((chkCity.Checked == true) && (CityId == ""))
            {
                InitilizeGrid(dgPinCode, "Pincode");
                return;
            }
            if ((chkCountry.Checked == true) && (CountryId == ""))
            {
                InitilizeGrid(dgPinCode, "Pincode");
                return;
            }
            if ((chkState.Checked == false) && (chkCity.Checked == false) && (chkCountry.Checked == false))
            {
                InitilizeGrid(dgPinCode, "Pincode");
                return;
            }
            if (Convert.ToInt32(cmbGrpName.SelectedValue) != 0)
            {
                vQry = vQry + " and AMPlayerTokens.GroupId in( " + Convert.ToInt32(cmbGrpName.SelectedValue) + " )";
            }
            if ((stateId != "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.stateid in( " + stateId + " )";
            }

            if (CityId != "")
            {
                vQry = vQry + " and AMPlayerTokens.cityid in( " + CityId + " )";
            }
            if ((CountryId != "") && (stateId == "") && (CityId == ""))
            {
                vQry = vQry + " and AMPlayerTokens.CountryId in( " + CountryId + " )";
            }



            sQr = "select pincode as DisplayName, ROW_NUMBER() over (ORDER BY pincode) AS id from( ";
            sQr = sQr + " select distinct pincode from AMPlayerTokens where pincode is not null  " + vQry + "  ) as a order by pincode ";
            FillDataParamter(dgPinCode, "Pincode", sQr);


        }

        private void dgPinCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (Convert.ToInt32(cmbDname.SelectedValue) != 0)
                {
                    IsClientCheckBoxClicked = true;
                    ClientCheckBox.Checked = false;
                    FillData();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkReplaceAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReplaceAll.Checked == true)
            {
                chkScheduleTime.Checked = false;
            }
        }

        private void chkScheduleTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkScheduleTime.Checked == true)
            {
                chkReplaceAll.Checked = false;
            }
        }
    }
}
