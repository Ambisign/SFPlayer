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
    public partial class frmCopyPlaylistSchedule : Form
    {
        gblClass objMainClass = new gblClass();
        DateTimeFormatInfo fi = new DateTimeFormatInfo();
        CheckBox ClientCheckBox = null;
        bool IsClientCheckBoxClicked = false;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        Int32 rtPschId = 0;
        Int32 DfClientid = 0;
        public frmCopyPlaylistSchedule()
        {
            InitializeComponent();
        }

        private void cmbCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID, RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
        }

        private void cmbSaveCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID, RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbSaveCustomer, "DFClientID", "ClientName", "");
        }

        private void cmbSaveCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSaveCustomer.SelectedValue) == 0)
            {
                InitilizeGrid();
                return;
            }
            IsClientCheckBoxClicked = true;
            ClientCheckBox.Checked = false;
            FillData();
            TotalCheckBoxes = dgToken.RowCount;
            TotalCheckedCheckBoxes = 0;
        }

        private void FillData()
        {
            string sQr = "";
           
            sQr = "GetTokenInfo " + Convert.ToInt32(cmbSaveCustomer.SelectedValue);

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
            ModifyToken.Visible = false;
            dgToken.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgToken.Columns.Add("uId", "uId");
            dgToken.Columns["uId"].Width = 0;
            dgToken.Columns["uId"].Visible = false;
            dgToken.Columns["uId"].ReadOnly = true;

        }

        private void frmCopyPlaylistSchedule_Load(object sender, EventArgs e)
        {
            fi.AMDesignator = "AM";
            fi.PMDesignator = "PM";
            AddClientCheckBox(dgToken);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);
            InitilizeGrid();
            InitilizeSplGrid();
        }
        private void AddClientCheckBox(DataGridView dgToken)
        {
            ClientCheckBox = new CheckBox();
            ClientCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgToken.Controls.Add(ClientCheckBox);

        }
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

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = " GetTokenInfoCombo " + Convert.ToInt32(cmbCustomer.SelectedValue);
            objMainClass.fnFillComboBox(str, cmbToken, "TokenID", "tno", "");
        }

        private void cmbToken_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPlaylist();
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
            dgSpl.Columns["fName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("pName", "Playlist Name");
            dgSpl.Columns["pName"].Width = 200;
            dgSpl.Columns["pName"].Visible = true;
            dgSpl.Columns["pName"].ReadOnly = true;
            dgSpl.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("sTime", "Start Time");
            dgSpl.Columns["sTime"].Width = 200;
            dgSpl.Columns["sTime"].Visible = true;
            dgSpl.Columns["sTime"].ReadOnly = true;
            dgSpl.Columns["sTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("eTime", "End Time");
            dgSpl.Columns["eTime"].Width = 200;
            dgSpl.Columns["eTime"].Visible = true;
            dgSpl.Columns["eTime"].ReadOnly = true;
            dgSpl.Columns["eTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            EditAdvt.Visible = false;
            dgSpl.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("TokenId", "TokenId");
            dgSpl.Columns["TokenId"].Width = 0;
            dgSpl.Columns["TokenId"].Visible = false;
            dgSpl.Columns["TokenId"].ReadOnly = true;

            dgSpl.Columns.Add("splId", "splId");
            dgSpl.Columns["splId"].Width = 0;
            dgSpl.Columns["splId"].Visible = false;
            dgSpl.Columns["splId"].ReadOnly = true;

            dgSpl.Columns.Add("fId", "fId");
            dgSpl.Columns["fId"].Width = 0;
            dgSpl.Columns["fId"].Visible = false;
            dgSpl.Columns["fId"].ReadOnly = true;
        }
        private void FillPlaylist()
        {
            string sQr = "";
            sQr = "GetCustomerPlaylistSchedule  ' where m.tokenid=" + Convert.ToInt32(cmbToken.SelectedValue) + " order by StartTime'";

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
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["StartTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["EndTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["wDay"].Value = GetWeekName(Convert.ToInt32(dtDetail.Rows[i]["pSchId"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["splId"].Value = dtDetail.Rows[i]["splPlaylistid"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fId"].Value = dtDetail.Rows[i]["formatid"].ToString();

                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Style.BackColor = Color.FromArgb(224, 224, 224);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SubmitValidationGet() == false) return;
            DfClientid= Convert.ToInt32(cmbSaveCustomer.SelectedValue);
            if (bgWorker.IsBusy == false)
            {
                panel4.Enabled = false;
                panel3.Enabled = false;
                panel2.Enabled = false;
                panMessage.Visible = true;
                panMessage.Location = new Point(
          this.Width / 2 - panMessage.Size.Width / 2,
          this.Height / 2 - panMessage.Size.Height / 2);

                bgWorker.RunWorkerAsync();
            }
            
        }
        private void SaveTokenDetail(Int32 TokenId, int IsAllToken, Int32 pSchId, Int32 FormatId, Int32 splPlaylistId)
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
            cmd.Parameters["@FormatId"].Value = FormatId;

            cmd.Parameters.Add(new SqlParameter("@DfClientid", SqlDbType.BigInt));
            cmd.Parameters["@DfClientid"].Value = DfClientid;

            cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
            cmd.Parameters["@splPlaylistId"].Value = splPlaylistId;


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
        }
        private void SaveWeek(int WeekId, int IsAllWeek, int pSch_id,Int32 FormatId)
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
            cmd.Parameters["@FormatId"].Value = FormatId;

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
        }
        private void SaveMainData(Int32 ReturnSchId, Int32 splPlaylistId,string StartTime,string EndTime, Int32 FormatId)
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
                cmd.Parameters["@dfClientId"].Value = DfClientid;

                cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
                cmd.Parameters["@splPlaylistId"].Value = splPlaylistId;


                cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime));
                cmd.Parameters["@StartTime"].Value = StartTime;

                cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
                cmd.Parameters["@EndTime"].Value = EndTime;

                cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.BigInt));
                cmd.Parameters["@FormatId"].Value = FormatId;
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                rtPschId = Convert.ToInt32(cmd.ExecuteScalar());
                StaticClass.constr.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private Boolean SubmitValidationGet()
        {
            if (dgSpl.Rows.Count == 0)
            {
                MessageBox.Show("Please select playlist schedule", "Management Panel");
                return false;
            }
            if (CheckGridValidationAdvt(dgToken) == false)
            {
                MessageBox.Show("Please select token no's from list", "Management Panel");
                return false;
            }

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

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string str = "";
            DataTable dtWeek = new DataTable();
            DataTable dtToken = new DataTable();
            for (int iToken = 0; iToken < dgToken.Rows.Count; iToken++)
            {
                if (Convert.ToBoolean(dgToken.Rows[iToken].Cells[1].Value) == true)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmdTit = new SqlCommand();
                    cmdTit.Connection = StaticClass.constr;
                    str = "";
                    str = "delete from  tbSpecialPlaylistSchedule_Token where tokenid = " + Convert.ToInt32(dgToken.Rows[iToken].Cells["Id"].Value);
                    cmdTit.CommandText = str;
                    cmdTit.ExecuteNonQuery();
                    StaticClass.constr.Close();
                }
            }
            for (int i = 0; i < dgSpl.Rows.Count; i++)
            {
                SaveMainData(0, Convert.ToInt32(dgSpl.Rows[i].Cells["splId"].Value), string.Format(fi, "{0:hh:mm tt}", dgSpl.Rows[i].Cells["stime"].Value), string.Format(fi, "{0:hh:mm tt}", dgSpl.Rows[i].Cells["etime"].Value), Convert.ToInt32(dgSpl.Rows[i].Cells["fId"].Value));
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = StaticClass.constr;
                cmd1.CommandText = "delete from tbSpecialPlaylistSchedule_Weekday where pSchId=" + rtPschId;
                cmd1.ExecuteNonQuery();
                StaticClass.constr.Close();
                str = "select * from tbSpecialPlaylistSchedule_WeekDay where pschid=" + Convert.ToInt32(dgSpl.Rows[i].Cells["Id"].Value);
                dtWeek = objMainClass.fnFillDataTable(str);
                for (int iTit = 0; iTit <= dtWeek.Rows.Count - 1; iTit++)
                {
                    if (Convert.ToInt32(dtWeek.Rows[iTit]["wId"]) == 0)
                    {
                        SaveWeek(0, 1, rtPschId, Convert.ToInt32(dtWeek.Rows[iTit]["FormatId"]));
                    }
                    else
                    {
                        SaveWeek(Convert.ToInt32(dtWeek.Rows[iTit]["wId"]), 0, rtPschId, Convert.ToInt32(dtWeek.Rows[iTit]["FormatId"]));
                    }

                }

                for (int iToken = 0; iToken < dgToken.Rows.Count; iToken++)
                {
                    if (Convert.ToBoolean(dgToken.Rows[iToken].Cells[1].Value) == true)
                    {
                        SaveTokenDetail(Convert.ToInt32(dgToken.Rows[iToken].Cells["Id"].Value), 0, rtPschId, Convert.ToInt32(dgSpl.Rows[i].Cells["fId"].Value), Convert.ToInt32(dgSpl.Rows[i].Cells["splId"].Value));
                    }
                }


            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panel4.Enabled = true;
            panel3.Enabled = true;
            panel2.Enabled = true;
            panMessage.Visible = false;
            MessageBox.Show("Record saved", "Management Panel");
            cmbSaveCustomer.SelectedValue = 0;
            cmbCustomer.SelectedValue = 0;
        }
    }
}
