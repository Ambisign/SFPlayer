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
    public partial class frmAssignMobileStreams : Form
    {
        DateTimeFormatInfo fi = new DateTimeFormatInfo();
        gblClass objMainClass = new gblClass();

        CheckBox ClientCheckBox = null;
        bool IsClientCheckBoxClicked = false;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;

        CheckBox StreamCheckBox = null;
        bool IsStreamCheckBoxClicked = false;
        int TotalCheckBoxesStream = 0;
        int TotalCheckedCheckBoxesStream = 0;

        public frmAssignMobileStreams()
        {
            InitializeComponent();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCustomer.SelectedValue) == 0)
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

            sQr = "GetTokenInfo " + Convert.ToInt32(cmbCustomer.SelectedValue);

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
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["ver"].Value = dtDetail.Rows[i]["liType"].ToString();
                    //if (Convert.ToBoolean(dtDetail.Rows[i]["IsStore"]) == true)
                    //{
                    //    dgToken.Rows[dgToken.Rows.Count - 1].Cells["ver"].Value = "Store";
                    //}
                    //else
                    //{
                    //    dgToken.Rows[dgToken.Rows.Count - 1].Cells["ver"].Value = "Stream";
                    //}
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

        private void cmbCustomer_Click(object sender, EventArgs e)
        {
             
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
        }

        private void cmbLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void SetButtonColor(Button btnName)
        {
            Color light = Color.FromName("ControlLightLight");
            Color bLight = Color.FromName("Control");
            btnMenuSearch.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAddNew.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            
            btnName.BackColor = Color.White;
        }
        private void frmAssignTokenStreams_Load(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panSearch.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
            

            AddClientCheckBox(dgToken);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);
            InitilizeGrid();

            AddStreamCheckBox(dgStream);
            StreamCheckBox.KeyUp += new KeyEventHandler(StreamCheckBox_KeyUp);
            StreamCheckBox.MouseClick += new MouseEventHandler(StreamCheckBox_MouseClick);
            InitilizeStreamGrid();

        }


        #region Add Check Box Stream
        private void StreamCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                StreamCheckBoxClick((CheckBox)sender);
        }
        private void StreamCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            StreamCheckBoxClick((CheckBox)sender);
        }
        private void StreamCheckBoxClick(CheckBox HCheckBox)
        {
            IsStreamCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgStream.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgStream.RefreshEdit();

            TotalCheckedCheckBoxesStream = HCheckBox.Checked ? TotalCheckBoxesStream : 0;

            IsStreamCheckBoxClicked = false;
        }

        private void AddStreamCheckBox(DataGridView dgStream)
        {
            StreamCheckBox = new CheckBox();
            StreamCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgStream.Controls.Add(StreamCheckBox);

        }
        private void dgStream_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsStreamCheckBoxClicked)
                RowCheckBoxClickStream((DataGridViewCheckBoxCell)dgStream[e.ColumnIndex, e.RowIndex]);
        }
        private void dgStream_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgStream.CurrentCell is DataGridViewCheckBoxCell)
                dgStream.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgStream_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetHeaderCheckBoxLocationStream(e.ColumnIndex, e.RowIndex);
        }
        private void ResetHeaderCheckBoxLocationStream(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgStream.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StreamCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StreamCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            StreamCheckBox.Location = oPoint;
        }
        private void RowCheckBoxClickStream(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesStream < TotalCheckBoxesStream)
                    TotalCheckedCheckBoxesStream++;
                else if (TotalCheckedCheckBoxesStream > 0)
                    TotalCheckedCheckBoxesStream--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesStream < TotalCheckBoxesStream)
                    StreamCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesStream == TotalCheckBoxesStream)
                    StreamCheckBox.Checked = true;
            }
        }
        #endregion

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

            //0
            dgStream.Columns.Add("Id", "Id");
            dgStream.Columns["Id"].Width = 0;
            dgStream.Columns["Id"].Visible = false;
            dgStream.Columns["Id"].ReadOnly = true;
            //1
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "";
            chk.DataPropertyName = "IsChecked";
            dgStream.Columns.Add(chk);
            chk.Width = 50;
            chk.Visible = true;
            dgStream.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //2
            dgStream.Columns.Add("sName", "Stream Name");
            dgStream.Columns["sName"].Width = 200;
            dgStream.Columns["sName"].Visible = true;
            dgStream.Columns["sName"].ReadOnly = true;
            dgStream.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
 

        }

        private void FillStreamData()
        {
            
            string str = "";
            str = "select *  from tbStreaming_App where   dfclientid=" + Convert.ToInt32(cmbStreamOwner.SelectedValue) + "  order by streamNameapp";
            //
            DataTable dtDetail = new DataTable();
            InitilizeStreamGrid();
            dtDetail = objMainClass.fnFillDataTable(str);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgStream.Rows.Add();
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["streamid"];
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells[1].Value = 0;
                    dgStream.Rows[dgStream.Rows.Count - 1].Cells["sname"].Value = dtDetail.Rows[i]["streamNameapp"].ToString();
                }
            }
            


        }
        Int32 StreamOwnerClientid = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SubmitValidation() == false) return;
            StreamOwnerClientid = Convert.ToInt32(cmbStreamOwner.SelectedValue);
            SaveData();

        }
        private Boolean SubmitValidation()
        {
            if (CheckGridValidationAdvt(dgStream) == false)
            {
                MessageBox.Show("Select a stream name from the list ", "Management Panel");
                dgStream.Focus();
                return false;
            }
            if (CheckGridValidationAdvt(dgToken) == false)
            {
                MessageBox.Show("Please select a token from list ", "Management Panel");
                dgToken.Focus();
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
        Int64 iSize_M = 0;
        private void SaveData()
        {
            panPopUp.BringToFront();
            panPopUp.Visible = true;
            panPopUp.Location = new Point(
           this.Width / 2 - panPopUp.Size.Width / 2,
           this.Height / 2 - panPopUp.Size.Height / 2);

            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int iStream = 0; iStream < dgStream.Rows.Count; iStream++)
            {
                if (Convert.ToBoolean(dgStream.Rows[iStream].Cells[1].Value) == true)
                {
                    for (int iToken = 0; iToken < dgToken.Rows.Count; iToken++)
                    {
                        if (Convert.ToBoolean(dgToken.Rows[iToken].Cells[1].Value) == true)
                        {
                            if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
                            StaticClass.constr.Open();
                            SqlCommand cmdOnline = new SqlCommand();
                            cmdOnline.Connection = StaticClass.constr;
                            cmdOnline.CommandType = CommandType.Text;
                            cmdOnline.CommandText = "insert into tbAssignMobileStreamToken (tokenid,streamid,StreamOwnerClientid) values (" + dgToken.Rows[iToken].Cells[0].Value + "," + dgStream.Rows[iStream].Cells[0].Value + "," + StreamOwnerClientid + ")";
                            cmdOnline.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
            panPopUp.Visible = false;
            MessageBox.Show("Record Save");
        }

        private void frmAssignTokenStreams_SizeChanged(object sender, EventArgs e)
        {
            panPopUp.Location = new Point(
          this.Width / 2 - panPopUp.Size.Width / 2,
          this.Height / 2 - panPopUp.Size.Height / 2);
        }

        private void dgStreamUpdate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strDel = "";
                    strDel = "delete from tbAssignMobileStreamToken where tokenid=" + Convert.ToInt32(cmbTokenUpdate.SelectedValue) + " ";
                    strDel = strDel + " and streamid= " + Convert.ToInt32(dgStreamUpdate.Rows[e.RowIndex].Cells["Id"].Value);

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    FillStreamDataUpdate();
                }
            }
        }

        private void cmbTokenUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTokenUpdate.SelectedValue) == 0)
            {
                InitilizeStreamGrid();
                return;
            }
            FillStreamDataUpdate();
        }
        private void InitilizeStreamGridUpdate()
        {
            if (dgStreamUpdate.Rows.Count > 0)
            {
                dgStreamUpdate.Rows.Clear();
            }
            if (dgStreamUpdate.Columns.Count > 0)
            {
                dgStreamUpdate.Columns.Clear();
            }

            //0
            dgStreamUpdate.Columns.Add("Id", "Id");
            dgStreamUpdate.Columns["Id"].Width = 0;
            dgStreamUpdate.Columns["Id"].Visible = false;
            dgStreamUpdate.Columns["Id"].ReadOnly = true;

            dgStreamUpdate.Columns.Add("sName", "Stream Name");
            dgStreamUpdate.Columns["sName"].Width = 200;
            dgStreamUpdate.Columns["sName"].Visible = true;
            dgStreamUpdate.Columns["sName"].ReadOnly = true;
            dgStreamUpdate.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn DeleteStream = new DataGridViewLinkColumn();
            DeleteStream.HeaderText = "Delete";
            DeleteStream.Text = "Delete";
            DeleteStream.DataPropertyName = "Delete";
            dgStreamUpdate.Columns.Add(DeleteStream);
            DeleteStream.UseColumnTextForLinkValue = true;
            DeleteStream.Width = 70;
            DeleteStream.Visible = true;
            dgStreamUpdate.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void FillStreamDataUpdate()
        {

            string str = "";
            str = "select distinct tbStreaming_App.*  from tbStreaming_App ";
            str = str + " inner join tbAssignMobileStreamToken on tbStreaming_App.streamid= tbAssignMobileStreamToken.streamId ";
            str = str + " where tbAssignMobileStreamToken.tokenid=" + Convert.ToInt32(cmbTokenUpdate.SelectedValue);
            str = str + " order by tbStreaming_App.streamNameapp ";
            DataTable dtDetail = new DataTable();

            InitilizeStreamGridUpdate();
            dtDetail = objMainClass.fnFillDataTable(str);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgStreamUpdate.Rows.Add();
                    dgStreamUpdate.Rows[dgStreamUpdate.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["streamid"];
                    dgStreamUpdate.Rows[dgStreamUpdate.Rows.Count - 1].Cells["sname"].Value = dtDetail.Rows[i]["streamNameapp"].ToString();
                }
            }



        }

        private void cmbCustomerUpdate_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomerUpdate, "DFClientID", "ClientName", "");
        }

        private void cmbCustomerUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = " GetTokenInfoCombo " + Convert.ToInt32(cmbCustomerUpdate.SelectedValue);
            objMainClass.fnFillComboBox(str, cmbTokenUpdate, "TokenID", "tno", "");
        }

        private void btnMenuAddNew_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuAddNew);
            panSearch.Visible = false;
            panAddNew.Dock = DockStyle.Fill;
            panAddNew.Visible = true;
            
        }

        private void btnMenuSearch_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panSearch.Dock = DockStyle.Fill;
            panAddNew.Visible = false;
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            cmbCustomer.SelectedValue = 0;
             
        }

        private void cmbStreamOwner_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbStreamOwner, "DFClientID", "ClientName", "");
        }

        private void cmbStreamOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsStreamCheckBoxClicked = true;
            StreamCheckBox.Checked = false;
            FillStreamData();
            TotalCheckBoxesStream = dgStream.RowCount;
            TotalCheckedCheckBoxesStream = 0;
        }
    }
}
