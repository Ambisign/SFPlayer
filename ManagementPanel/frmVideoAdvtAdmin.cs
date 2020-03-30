using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmVideoAdvtAdmin : Form
    {
        Int32 ReturnAdvtId = 0;
        Int32 ReturnDealerId = 0;
        int IsAllPress = 0;

        CheckBox CountryCheckBox = null;
        bool IsCountryCheckBoxClicked = false;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;

        CheckBox StateCheckBox = null;
        bool IsStateCheckBoxClicked = false;
        int TotalCheckBoxesState = 0;
        int TotalCheckedCheckBoxesState = 0;

        CheckBox CityCheckBox = null;
        bool IsCityCheckBoxClicked = false;
        int TotalCheckBoxesCity = 0;
        int TotalCheckedCheckBoxesCity = 0;

        CheckBox DealerCheckBox = null;
        bool IsDealerCheckBoxClicked = false;
        int TotalCheckBoxesDealer = 0;
        int TotalCheckedCheckBoxesDealer = 0;

        CheckBox ClientCheckBox = null;
        bool IsClientCheckBoxClicked = false;
        int TotalCheckBoxesClient = 0;
        int TotalCheckedCheckBoxesClient = 0;

        CheckBox TokenCheckBox = null;
        bool IsTokenCheckBoxClicked = false;
        int TotalCheckBoxesToken = 0;
        int TotalCheckedCheckBoxesToken = 0;


        gblClass objMainClass = new gblClass();
        public frmVideoAdvtAdmin()
        {
            InitializeComponent();
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panAddNew.Visible = false;
            panSearch.Dock = DockStyle.Fill;
        }

        private void chkSun_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void frmAdvtAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Application.Exit();
        }

        private void chkMon_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkTue_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkWed_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkThu_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkFri_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkSat_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllPress == 1) return;
            if (chkAll.Checked == true)
            {
                chkAll.Checked = false;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                IsAllPress = 1;
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
                IsAllPress = 0;
                chkSun.Enabled = true;
                chkMon.Enabled = true;
                chkTue.Enabled = true;
                chkWed.Enabled = true;
                chkThu.Enabled = true;
                chkFri.Enabled = true;
                chkSat.Enabled = true;
            }

        }

        private void chkCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCountry.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkCountry.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panCountry.Visible = true;
                tlpMain.ColumnStyles[0].Width = 15;
            }
            else
            {
                panCountry.Visible = false;
                tlpMain.ColumnStyles[0].Width = 0;
            }

        }

        private void frmAdvtAdmin_Load(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panAddNew.Visible = false;
            panSearch.Dock = DockStyle.Fill;

            string Advttype = "select * from tbAdvertisementType";
            objMainClass.fnFillComboBox(Advttype, cmbAdvertisementTypeAdd, "AdvtTypeId", "AdvtTypeName", "");

            //string strCountry = "";
            //strCountry = "select * from CountryCodes order by countryCode";
            //objMainClass.fnFillComboBox(strCountry, cmbCountryName, "countrycode", "countryName", "");
            //objMainClass.fnFillComboBox(strCountry, cmbCityCountry, "countrycode", "countryName", "");
            //objMainClass.fnFillComboBox(strCountry, cmbDealerCountry, "countrycode", "countryName", "");
            //objMainClass.fnFillComboBox(strCountry, cmbClientCountry, "countrycode", "countryName", "");



            AddCountryCheckBox(dgCountry);
            CountryCheckBox.KeyUp += new KeyEventHandler(CountryCheckBox_KeyUp);
            CountryCheckBox.MouseClick += new MouseEventHandler(CountryCheckBox_MouseClick);


            AddStateCheckBox(dgState);
            StateCheckBox.KeyUp += new KeyEventHandler(StateCheckBox_KeyUp);
            StateCheckBox.MouseClick += new MouseEventHandler(StateCheckBox_MouseClick);

            AddCityCheckBox(dgCity);
            CityCheckBox.KeyUp += new KeyEventHandler(CityCheckBox_KeyUp);
            CityCheckBox.MouseClick += new MouseEventHandler(CityCheckBox_MouseClick);


            IsDealerCheckBoxClicked = true;
            AddDealerCheckBox(dgDealer);
            DealerCheckBox.KeyUp += new KeyEventHandler(DealerCheckBox_KeyUp);
            DealerCheckBox.MouseClick += new MouseEventHandler(DealerCheckBox_MouseClick);

            IsClientCheckBoxClicked = true;
            AddClientCheckBox(dgClient);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);

            IsTokenCheckBoxClicked = true;
            AddTokenCheckBox(dgToken);
            TokenCheckBox.KeyUp += new KeyEventHandler(TokenCheckBox_KeyUp);
            TokenCheckBox.MouseClick += new MouseEventHandler(TokenCheckBox_MouseClick);


            InitilizeGrid(dgState, "State Name");
            InitilizeGrid(dgCity, "City Name");
            dtpStartDate.Value = DateTime.Now.Date;
            dtpEndDate.Value = DateTime.Now.Date;

            dtpCurrentDate.Value = DateTime.Now.Date;


        }
        private void CountryCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                CountryCheckBoxClick((CheckBox)sender);
        }
        private void CountryCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            CountryCheckBoxClick((CheckBox)sender);
        }
        private void CountryCheckBoxClick(CheckBox HCheckBox)
        {
            IsCountryCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgCountry.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgCountry.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsCountryCheckBoxClicked = false;
        }
        private void chkState_CheckedChanged(object sender, EventArgs e)
        {
            if (chkState.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkState.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panState.Visible = true;
                tlpMain.ColumnStyles[1].Width = 16;
            }
            else
            {
                panState.Visible = false;
                tlpMain.ColumnStyles[1].Width = 0;
            }
        }

        private void chkCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCity.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkCity.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panCity.Visible = true;
                tlpMain.ColumnStyles[2].Width = 16;
            }
            else
            {
                panCity.Visible = false;
                tlpMain.ColumnStyles[2].Width = 0;
            }
        }

        private void chkDealer_CheckedChanged(object sender, EventArgs e)
        {

            if (chkDealer.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkDealer.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panDealer.Visible = true;
                tlpMain.ColumnStyles[3].Width = 16;
            }
            else
            {
                panDealer.Visible = false;
                tlpMain.ColumnStyles[3].Width = 0;
            }
        }

        private void chkClient_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClient.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkClient.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panClient.Visible = true;
                tlpMain.ColumnStyles[4].Width = 16;
            }
            else
            {
                panClient.Visible = false;
                tlpMain.ColumnStyles[4].Width = 0;
            }
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
        private void FillData(DataGridView dgGrid, string DisplayName, string sQr)
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
            foreach (DataGridViewRow row in dgGrid.Rows)
            {
                row.Height = 30;
            }
        }
        #region Add Country Check Box
        private void AddCountryCheckBox(DataGridView dgGrid)
        {
            CountryCheckBox = new CheckBox();
            CountryCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(CountryCheckBox);

        }
        private void dgCountry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsCountryCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dgCountry[e.ColumnIndex, e.RowIndex]);
        }
        private void dgCountry_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgCountry.CurrentCell is DataGridViewCheckBoxCell)
                dgCountry.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgCountry_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }
        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgCountry.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - CountryCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - CountryCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            CountryCheckBox.Location = oPoint;
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
                    CountryCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    CountryCheckBox.Checked = true;
            }
        }
        #endregion

        #region Add State Check Box
        private void AddStateCheckBox(DataGridView dgGrid)
        {
            StateCheckBox = new CheckBox();
            StateCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(StateCheckBox);

        }

        private void dgState_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsStateCheckBoxClicked)
                StateRowCheckBoxClick((DataGridViewCheckBoxCell)dgState[e.ColumnIndex, e.RowIndex]);
        }

        private void dgState_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgState.CurrentCell is DataGridViewCheckBoxCell)
                dgState.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgState_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetStateCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void ResetStateCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgState.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StateCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StateCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            StateCheckBox.Location = oPoint;
        }
        private void StateRowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesState < TotalCheckBoxesState)
                    TotalCheckedCheckBoxesState++;
                else if (TotalCheckedCheckBoxesState > 0)
                    TotalCheckedCheckBoxesState--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesState < TotalCheckBoxesState)
                    StateCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesState == TotalCheckBoxesState)
                    StateCheckBox.Checked = true;
            }
        }

        private void StateCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                StateCheckBoxClick((CheckBox)sender);
        }
        private void StateCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            StateCheckBoxClick((CheckBox)sender);
        }
        private void StateCheckBoxClick(CheckBox HCheckBox)
        {
            IsStateCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgState.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgState.RefreshEdit();

            TotalCheckedCheckBoxesState = HCheckBox.Checked ? TotalCheckBoxesState : 0;

            IsStateCheckBoxClicked = false;
        }

        #endregion


        #region Add City Check Box
        private void AddCityCheckBox(DataGridView dgGrid)
        {
            CityCheckBox = new CheckBox();
            CityCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(CityCheckBox);

        }

        private void dgCity_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsCityCheckBoxClicked)
                CityRowCheckBoxClick((DataGridViewCheckBoxCell)dgCity[e.ColumnIndex, e.RowIndex]);
        }

        private void dgCity_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgCity.CurrentCell is DataGridViewCheckBoxCell)
                dgCity.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgCity_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetCityCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void ResetCityCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgCity.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StateCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StateCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            CityCheckBox.Location = oPoint;
        }
        private void CityRowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesCity < TotalCheckBoxesCity)
                    TotalCheckedCheckBoxesCity++;
                else if (TotalCheckedCheckBoxesCity > 0)
                    TotalCheckedCheckBoxesCity--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesCity < TotalCheckBoxesCity)
                    CityCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesCity == TotalCheckBoxesCity)
                    CityCheckBox.Checked = true;
            }
        }

        private void CityCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                CityCheckBoxClick((CheckBox)sender);
        }
        private void CityCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            CityCheckBoxClick((CheckBox)sender);
        }
        private void CityCheckBoxClick(CheckBox HCheckBox)
        {
            IsCityCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgCity.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgCity.RefreshEdit();

            TotalCheckedCheckBoxesCity = HCheckBox.Checked ? TotalCheckBoxesCity : 0;

            IsCityCheckBoxClicked = false;
        }

        #endregion


        #region Add Dealer Check Box
        private void AddDealerCheckBox(DataGridView dgGrid)
        {
            DealerCheckBox = new CheckBox();
            DealerCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(DealerCheckBox);

        }

        private void dgDealer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsDealerCheckBoxClicked)
                DealerRowCheckBoxClick((DataGridViewCheckBoxCell)dgDealer[e.ColumnIndex, e.RowIndex]);
        }

        private void dgDealer_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgDealer.CurrentCell is DataGridViewCheckBoxCell)
                dgDealer.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgDealer_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetDealerCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void ResetDealerCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgDealer.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StateCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StateCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            DealerCheckBox.Location = oPoint;
        }
        private void DealerRowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesDealer < TotalCheckBoxesDealer)
                    TotalCheckedCheckBoxesDealer++;
                else if (TotalCheckedCheckBoxesDealer > 0)
                    TotalCheckedCheckBoxesDealer--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesDealer < TotalCheckBoxesDealer)
                    DealerCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesDealer == TotalCheckBoxesDealer)
                    DealerCheckBox.Checked = true;
            }
        }

        private void DealerCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                DealerCheckBoxClick((CheckBox)sender);
        }
        private void DealerCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            DealerCheckBoxClick((CheckBox)sender);
        }
        private void DealerCheckBoxClick(CheckBox HCheckBox)
        {
            IsDealerCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgDealer.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgDealer.RefreshEdit();

            TotalCheckedCheckBoxesDealer = HCheckBox.Checked ? TotalCheckBoxesDealer : 0;

            IsDealerCheckBoxClicked = false;
        }

        #endregion


        #region Add Client Check Box
        private void AddClientCheckBox(DataGridView dgGrid)
        {
            ClientCheckBox = new CheckBox();
            ClientCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(ClientCheckBox);

        }

        private void dgClient_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsClientCheckBoxClicked)
                ClientRowCheckBoxClick((DataGridViewCheckBoxCell)dgClient[e.ColumnIndex, e.RowIndex]);
        }

        private void dgClient_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgClient.CurrentCell is DataGridViewCheckBoxCell)
                dgClient.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgClient_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetClientCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void ResetClientCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgClient.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StateCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StateCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            ClientCheckBox.Location = oPoint;
        }
        private void ClientRowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesClient < TotalCheckBoxesClient)
                    TotalCheckedCheckBoxesClient++;
                else if (TotalCheckedCheckBoxesClient > 0)
                    TotalCheckedCheckBoxesClient--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesClient < TotalCheckBoxesClient)
                    ClientCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesClient == TotalCheckBoxesClient)
                    ClientCheckBox.Checked = true;
            }
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

            foreach (DataGridViewRow Row in dgClient.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgClient.RefreshEdit();

            TotalCheckedCheckBoxesClient = HCheckBox.Checked ? TotalCheckBoxesClient : 0;

            IsClientCheckBoxClicked = false;
        }

        #endregion



        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsStateCheckBoxClicked = true;

           
                string str = "";
                if (cmbPlayerType.Text == "All")
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    if (Convert.ToInt32(cmbCountryName.SelectedValue) != 0)
                    {
                        str = str + " where  tbState.CountryId=" + Convert.ToInt32(cmbCountryName.SelectedValue) + "  ";
                    }
                    str = str + " order by StateName ";
                }
                
                else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isCopyright=1 ";
                    if (cmbPlayerType.Text == "Copyright")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCR' ";
                    }
                    if (Convert.ToInt32(cmbCountryName.SelectedValue) != 0)
                    {
                        str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCountryName.SelectedValue) + "  ";
                    }
                    str = str + " order by StateName ";
                }
                else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.IsAsian=1 ";
                    if (cmbPlayerType.Text == "Asian")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeAsian' ";
                    }
                    if (Convert.ToInt32(cmbCountryName.SelectedValue) != 0)
                    {
                        str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCountryName.SelectedValue) + "  ";
                    }
                    str = str + " order by StateName ";
                }
                else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL") || (cmbPlayerType.Text == "Total"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isdam=1 ";
                    if (cmbPlayerType.Text == "Copyleft")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else if (cmbPlayerType.Text == "NativeCL")
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCL' ";
                    }
                    //else
                    //{
                    //    str = str + " and Users.Playerversion='Total' ";
                    //}

                    if (Convert.ToInt32(cmbCountryName.SelectedValue) != 0)
                    {
                        str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCountryName.SelectedValue) + "  ";
                    }
                    str = str + " order by StateName ";
                }

                FillData(dgState, "State Name", str);
                TotalCheckBoxesState = dgState.RowCount;
                TotalCheckedCheckBoxesState = 0;
             
        }

        private void cmbCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
             
                string str = "";
                if (cmbPlayerType.Text == "All")
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";

                    str = str + " where  tbState.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";

                    str = str + " order by StateName ";
                }
                 
                else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.IsAsian=1 ";
                    if (cmbPlayerType.Text == "Asian")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeAsian' ";
                    }

                    str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";

                    str = str + " order by StateName ";
                }
                else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isCopyright=1 ";
                    if (cmbPlayerType.Text == "Copyright")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCR' ";
                    }

                    str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";

                    str = str + " order by StateName ";
                }
                else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL") || (cmbPlayerType.Text == "Total"))
                {
                    str = "";
                    str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
                    str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isdam=1 ";
                    if (cmbPlayerType.Text == "Copyleft")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else if (cmbPlayerType.Text == "NativeCL")
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCL' ";
                    }
                    //else
                    //{
                    //    str = str + " and Users.Playerversion='Total' ";
                    //}

                    str = str + " and tbState.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";

                    str = str + " order by StateName ";
                }
                objMainClass.fnFillComboBox(str, cmbStateName, "Id", "DisplayName", "");
            
        }

        private void cmbStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsCityCheckBoxClicked = true;
             
                string str = "";
                if (cmbPlayerType.Text == "All")
                {
                    str = "";
                    str = "SELECT distinct tbCity.CityId as Id, tbCity.CityName as DisplayName FROM AMPlayerTokens  ";
                    str = str + " INNER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    if (Convert.ToInt32(cmbCityCountry.SelectedValue) != 0)
                    {
                        str = str + " where  tbCity.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";
                    }
                    if (Convert.ToInt32(cmbStateName.SelectedValue) != 0)
                    {
                        str = str + " and  tbCity.StateId=" + Convert.ToInt32(cmbStateName.SelectedValue) + "  ";
                    }
                    str = str + " order by tbCity.CityName ";
                }
                
                else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
                {
                    str = "";
                    str = "SELECT distinct tbCity.CityId as Id, tbCity.CityName as DisplayName FROM AMPlayerTokens  ";
                    str = str + " INNER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.IsAsian=1 ";
                    if (cmbPlayerType.Text == "Asian")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeAsian' ";
                    }
                    if (Convert.ToInt32(cmbCityCountry.SelectedValue) != 0)
                    {
                        str = str + " and  tbCity.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";
                    }
                    if (Convert.ToInt32(cmbStateName.SelectedValue) != 0)
                    {
                        str = str + " and tbCity.StateId=" + Convert.ToInt32(cmbStateName.SelectedValue) + "  ";
                    }
                    str = str + " order by tbCity.CityName ";
                }
                else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
                {
                    str = "";
                    str = "SELECT distinct tbCity.CityId as Id, tbCity.CityName as DisplayName FROM AMPlayerTokens  ";
                    str = str + " INNER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isCopyright=1 ";
                    if (cmbPlayerType.Text == "Copyright")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCR' ";
                    }
                    if (Convert.ToInt32(cmbCityCountry.SelectedValue) != 0)
                    {
                        str = str + " and  tbCity.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";
                    }
                    if (Convert.ToInt32(cmbStateName.SelectedValue) != 0)
                    {
                        str = str + " and tbCity.StateId=" + Convert.ToInt32(cmbStateName.SelectedValue) + "  ";
                    }
                    str = str + " order by tbCity.CityName ";
                }
                else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL") || (cmbPlayerType.Text == "Total"))
                {
                    str = "";
                    str = "SELECT distinct tbCity.CityId as Id, tbCity.CityName as DisplayName FROM AMPlayerTokens  ";
                    str = str + " INNER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId ";
                    str = str + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                    str = str + " where AMPlayerTokens.isdam=1 ";
                    if (cmbPlayerType.Text == "Copyleft")
                    {
                        str = str + " and AMPlayerTokens.pversion='Normal' ";
                    }
                    else if (cmbPlayerType.Text == "NativeCL")
                    {
                        str = str + " and AMPlayerTokens.pversion='NativeCL' ";
                    }
                    //else
                    //{
                    //    str = str + " and Users.Playerversion='Total' ";
                    //}



                    if (Convert.ToInt32(cmbCityCountry.SelectedValue) != 0)
                    {
                        str = str + " and  tbCity.CountryId=" + Convert.ToInt32(cmbCityCountry.SelectedValue) + "  ";
                    }
                    if (Convert.ToInt32(cmbStateName.SelectedValue) != 0)
                    {
                        str = str + " and tbCity.StateId=" + Convert.ToInt32(cmbStateName.SelectedValue) + "  ";
                    }
                    str = str + " order by tbCity.CityName ";
                }
                FillData(dgCity, "City Name", str);
                TotalCheckBoxesCity = dgCity.RowCount;
                TotalCheckedCheckBoxesCity = 0;

            
        }

        private void cmbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDealerCheckBoxClicked = true;
            DealerCheckBox.Checked = false;

            IsClientCheckBoxClicked = true;
            ClientCheckBox.Checked = false;

            IsCountryCheckBoxClicked = true;
            CountryCheckBox.Checked = false;

            string str = "";
            string strClient = "";
            string strCou = "";
            if (cmbPlayerType.Text == "All")
            {
                str = "";
                str = "select DFClientID as Id, dealercode as DisplayName from tbdealerlogin order by DFClientID desc";
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id ,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  ";
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";

                strCou = "";
                strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
                strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
                strCou = strCou + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                strCou = strCou + " where Users.Playertype='Desktop'  order by countryCode";

            }
            else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
            {
                str = "";
                str = "select DFClientID as Id, dealercode as DisplayName from tbdealerlogin where isCopyright=1 order by DFClientID desc";
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  and AMPlayerTokens.IsCopyright=1";
                if (cmbPlayerType.Text == "Copyright")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='Normal' ";
                }
                else if (cmbPlayerType.Text == "NativeCR")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='NativeCR' ";
                }
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";

                strCou = "";
                strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
                strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
                strCou = strCou + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                strCou = strCou + " where Users.Playertype='Desktop' and AMPlayerTokens.IsCopyright=1 ";
                if (cmbPlayerType.Text == "Copyright")
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='Normal' ";
                }
                else
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='NativeCR' ";
                }
                strCou = strCou + " order by countryCode";

            }
            else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
            {
                str = "";
                str = "select DFClientID as Id, dealercode as DisplayName from tbdealerlogin where isAsian=1 order by DFClientID desc";
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  and AMPlayerTokens.isAsian=1";
                if (cmbPlayerType.Text == "Asian")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='Normal' ";
                }
                else if (cmbPlayerType.Text == "NativeAsian")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='NativeAsian' ";
                }
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";

                strCou = "";
                strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
                strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
                strCou = strCou + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                strCou = strCou + " where Users.Playertype='Desktop' and AMPlayerTokens.isAsian=1 ";
                if (cmbPlayerType.Text == "Asian")
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='Normal' ";
                }
                else
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='NativeAsian' ";
                }
                strCou = strCou + " order by countryCode";

            }
            else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL") || (cmbPlayerType.Text == "Total"))
            {
                str = "";
                str = "select DFClientID as Id, dealercode as DisplayName from tbdealerlogin where isdam=1 order by DFClientID desc";
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null   and AMPlayerTokens.IsDam=1 ";
                if (cmbPlayerType.Text == "Copyleft")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='Normal' ";
                }
                else if (cmbPlayerType.Text == "NativeCL")
                {
                    strClient = strClient + " and AMPlayerTokens.pversion='NativeCL' ";
                }
                 
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";

                strCou = "";
                strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
                strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
                strCou = strCou + " INNER JOIN Users ON AMPlayerTokens.UserId = Users.UserID";
                strCou = strCou + " where Users.Playertype='Desktop' and AMPlayerTokens.IsDam=1 ";
                if (cmbPlayerType.Text == "Copyleft")
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='Normal' ";
                }
                else if (cmbPlayerType.Text == "NativeCL")
                {
                    strCou = strCou + " and AMPlayerTokens.pversion='NativeCL' ";
                }
                
                strCou = strCou + " order by countryCode";
            }

            objMainClass.fnFillComboBox(strCou, cmbCountryName, "id", "DisplayName", "");
            objMainClass.fnFillComboBox(strCou, cmbCityCountry, "id", "DisplayName", "");
           // objMainClass.fnFillComboBox(strCou, cmbDealerCountry, "id", "DisplayName", "");
            objMainClass.fnFillComboBox(strCou, cmbClientCountry, "id", "DisplayName", "");


            FillData(dgCountry, "Country Name", strCou);
             TotalCheckBoxes = dgCountry.RowCount;
            TotalCheckedCheckBoxes = 0;


            

         //   FillData(dgClient, "Client Name", strClient);
         //   TotalCheckBoxesClient = dgClient.RowCount;
        //    TotalCheckedCheckBoxesClient = 0;

            str = "";
            str = "select DFClientID as Id,ClientName as DisplayName  from ( select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1  ";
            if (cmbPlayerType.Text == "All")
            {
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "Copyleft")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where isDam=1 and pversion='Normal') ";
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "Copyright")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where pversion='Normal' and isCopyright=1) ";
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "Asian")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where isAsian=1 and pversion='Normal') ";
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "NativeCL")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where isDam=1 and pversion='NativeCL') ";
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "NativeCR")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where isCopyright=1 and pversion='NativeCR') ";
                str = str + " ) as a order by ClientName desc ";
            }
            if (cmbPlayerType.Text == "NativeAsian")
            {
                str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where IsAsian=1 and pversion='NativeAsian') ";
                str = str + " ) as a order by ClientName desc ";
            }
            objMainClass.fnFillComboBox(str, cmbDealer, "Id", "DisplayName", "");

            FillData(dgDealer, "Dealer Code", str);
            TotalCheckBoxesDealer = dgDealer.RowCount;
            TotalCheckedCheckBoxesDealer = 0;
        }

        private void cmbDealerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkDealer.Checked == true)
            {
                string str = "";
                if (cmbPlayerType.Text == "All")
                {
                    str = "";
                    str = "select tbdealerlogin.DFClientID as Id, tbdealerlogin.dealercode as DisplayName from tbdealerlogin ";
                    str = str + " inner join dfclients on tbdealerlogin.dfclientid= dfclients.dfclientid";
                    if (Convert.ToInt32(cmbDealerCountry.SelectedValue) != 0)
                    {
                        str = str + " where  dfclients.countrycode=" + Convert.ToInt32(cmbDealerCountry.SelectedValue) + "  ";
                    }
                    str = str + " order by tbdealerlogin.DFClientID desc ";
                }
                 
                else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
                {
                    str = "";
                    str = "select tbdealerlogin.DFClientID as Id, tbdealerlogin.dealercode as DisplayName from tbdealerlogin ";
                    str = str + " inner join dfclients on tbdealerlogin.dfclientid= dfclients.dfclientid where tbdealerlogin.IsAsian=1 ";
                    if (Convert.ToInt32(cmbDealerCountry.SelectedValue) != 0)
                    {
                        str = str + " and dfclients.countrycode=" + Convert.ToInt32(cmbDealerCountry.SelectedValue) + " ";
                    }
                    str = str + " order by tbdealerlogin.DFClientID desc ";
                }
                else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
                {
                    str = "";
                    str = "select tbdealerlogin.DFClientID as Id, tbdealerlogin.dealercode as DisplayName from tbdealerlogin ";
                    str = str + " inner join dfclients on tbdealerlogin.dfclientid= dfclients.dfclientid where tbdealerlogin.isCopyright=1 ";
                    if (Convert.ToInt32(cmbDealerCountry.SelectedValue) != 0)
                    {
                        str = str + " and dfclients.countrycode=" + Convert.ToInt32(cmbDealerCountry.SelectedValue) + " ";
                    }
                    str = str + " order by tbdealerlogin.DFClientID desc ";
                }
                else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL"))
                {
                    str = "";
                    str = "select tbdealerlogin.DFClientID as Id, tbdealerlogin.dealercode as DisplayName from tbdealerlogin ";
                    str = str + " inner join dfclients on tbdealerlogin.dfclientid= dfclients.dfclientid where tbdealerlogin.isdam=1 ";
                    if (Convert.ToInt32(cmbDealerCountry.SelectedValue) != 0)
                    {
                        str = str + " and dfclients.countrycode=" + Convert.ToInt32(cmbDealerCountry.SelectedValue) + " ";
                    }
                    str = str + " order by tbdealerlogin.DFClientID desc ";
                }
                FillData(dgDealer, "Dealer Code", str);
                TotalCheckBoxesDealer = dgDealer.RowCount;
                TotalCheckedCheckBoxesDealer = 0;
            }
        }

        private void cmbClientCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsClientCheckBoxClicked = true;

            string strClient = "";
            if (cmbPlayerType.Text == "All")
            {
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id ,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  ";
                if (Convert.ToInt32(cmbClientCountry.SelectedValue) != 0)
                {
                    strClient = strClient + " and  dfclients.countrycode=" + Convert.ToInt32(cmbClientCountry.SelectedValue) + "  ";
                }
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";
            }
            else if ((cmbPlayerType.Text == "Asian") || (cmbPlayerType.Text == "NativeAsian"))
            {
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  and AMPlayerTokens.IsAsian=1";
                if (Convert.ToInt32(cmbClientCountry.SelectedValue) != 0)
                {
                    strClient = strClient + " and  dfclients.countrycode=" + Convert.ToInt32(cmbClientCountry.SelectedValue) + "  ";
                }
                if (cmbPlayerType.Text == "Asian")
                {
                    strClient = strClient + " and Users.Playerversion='Normal' ";
                }
                else
                {
                    strClient = strClient + " and Users.Playerversion='NativeAsian' ";
                }
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";
            }
            else if ((cmbPlayerType.Text == "Copyright") || (cmbPlayerType.Text == "NativeCR"))
            {
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null  and AMPlayerTokens.IsCopyright=1";
                if (Convert.ToInt32(cmbClientCountry.SelectedValue) != 0)
                {
                    strClient = strClient + " and  dfclients.countrycode=" + Convert.ToInt32(cmbClientCountry.SelectedValue) + "  ";
                }
                if (cmbPlayerType.Text == "Copyright")
                {
                    strClient = strClient + " and Users.Playerversion='Normal' ";
                }
                else
                {
                    strClient = strClient + " and Users.Playerversion='NativeCR' ";
                }
                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";
            }
            else if ((cmbPlayerType.Text == "Copyleft") || (cmbPlayerType.Text == "NativeCL") || (cmbPlayerType.Text == "Total"))
            {
                strClient = "";
                strClient = "select distinct DFClients.DFClientID as Id,DFClients.ClientName as DisplayName  from DFClients ";
                strClient = strClient + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
                strClient = strClient + " inner join Users on DFClients.DfClientId=Users.Clientid  ";
                strClient = strClient + " where DFClients.CountryCode is not null and DFClients.IsDealer=0  and DFClients.IsDealerclient is null   and AMPlayerTokens.IsDam=1 ";
                if (Convert.ToInt32(cmbClientCountry.SelectedValue) != 0)
                {
                    strClient = strClient + " and  dfclients.countrycode=" + Convert.ToInt32(cmbClientCountry.SelectedValue) + "  ";
                }
                if (cmbPlayerType.Text == "Copyleft")
                {
                    strClient = strClient + " and Users.Playerversion='Normal' ";
                }
                else if (cmbPlayerType.Text == "NativeCL")
                {
                    strClient = strClient + " and Users.Playerversion='NativeCL' ";
                }
                else
                {
                    strClient = strClient + " and Users.Playerversion='Total' ";
                }

                strClient = strClient + " and Users.Playertype='Desktop' order by DFClients.DFClientID desc ";
            }

            FillData(dgClient, "Client Name", strClient);
            TotalCheckBoxesClient = dgClient.RowCount;
            TotalCheckedCheckBoxesClient = 0;
        }
        private Boolean SubmitValidationAdvt()
        {

            if (txtAdvtName.Text == "")
            {
                MessageBox.Show("Advertisement name cannot be blank", "Management Panel");
                txtAdvtName.Focus();
                return false;
            }
            if (txtCompanyName.Text == "")
            {
                MessageBox.Show("Company name cannot be blank", "Management Panel");
                txtCompanyName.Focus();
                return false;
            }
            if (cmbPlayerType.Text == "")
            {
                MessageBox.Show("Please select a player version", "Management Panel");
                cmbPlayerType.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbAdvertisementTypeAdd.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a advertisement type", "Management Panel");
                cmbAdvertisementTypeAdd.Focus();
                return false;
            }
            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                MessageBox.Show("Please select a proper date's", "Management Panel");
                dtpEndDate.Focus();
                return false;
            }
            if (rdoTime.Checked == true)
            {
                if (cmbHour.Text == "")
                {
                    MessageBox.Show("Please select a hour", "Management Panel");
                    cmbHour.Focus();
                    return false;
                }

                if (cmbMin.Text == "")
                {
                    MessageBox.Show("Please select a minute", "Management Panel");
                    cmbMin.Focus();
                    return false;
                }
                if (cmbAMPM.Text == "")
                {
                    MessageBox.Show("Please select a time type", "Management Panel");
                    cmbAMPM.Focus();
                    return false;
                }
            }
            if (rdoMin.Checked == true)
            {
                if (cmbAdvtMin.Text == "")
                {
                    MessageBox.Show("Please select a mintue", "Management Panel");
                    cmbAdvtMin.Focus();
                    return false;
                }
            }
            if (rdoSong.Checked == true)
            {
                if (cmbAdvtSongs.Text == "")
                {
                    MessageBox.Show("Please select a song", "Management Panel");
                    cmbAMPM.Focus();
                    return false;
                }
            }
            if (txtPath.Text == "")
            {
                MessageBox.Show("This path cannot be empty", "Management Panel");
                txtPath.Focus();
                return false;
            }



            if ((chkAll.Checked == false) && (chkSun.Checked == false) && (chkMon.Checked == false) && (chkTue.Checked == false) && (chkWed.Checked == false) && (chkThu.Checked == false) && (chkFri.Checked == false) && (chkSat.Checked == false))
            {
                MessageBox.Show("Please select a week day", "Management Panel");
                chkAll.Focus();
                return false;
            }
            if ((chkCountry.Checked == false) && (chkState.Checked == false) && (chkCity.Checked == false) && (chkDealer.Checked == false) && (chkClient.Checked == false) && (chkDealerClient.Checked == false))
            {
                MessageBox.Show("Select a play option", "Management Panel");
                chkCountry.Focus();
                return false;
            }
            if (chkCountry.Checked == true)
            {
                if (CheckGridValidationAdvt(dgCountry) == false)
                {
                    MessageBox.Show("Select a Country from the list from the list", "Management Panel");
                    chkCountry.Focus();
                    return false;
                }
            }
            if (chkState.Checked == true)
            {
                if (CheckGridValidationAdvt(dgState) == false)
                {
                    MessageBox.Show("Select the status from this list", "Management Panel");
                    chkState.Focus();
                    return false;
                }
            }
            if (chkCity.Checked == true)
            {
                if (CheckGridValidationAdvt(dgCity) == false)
                {
                    MessageBox.Show("Select the cities from this list", "Management Panel");
                    chkCity.Focus();
                    return false;
                }
            }
            if (chkDealer.Checked == true)
            {
                if (CheckGridValidationAdvt(dgDealer) == false)
                {
                    MessageBox.Show("Please select dealers from list", "Management Panel");
                    chkDealer.Focus();
                    return false;
                }
            }
            if (chkClient.Checked == true)
            {
                if (CheckGridValidationAdvt(dgClient) == false)
                {
                    MessageBox.Show("Please select clients from list", "Management Panel");
                    chkClient.Focus();
                    return false;
                }
            }
            if (chkDealerClient.Checked == true)
            {
                if (CheckGridValidationAdvt(dgToken) == false)
                {
                    MessageBox.Show("Select the token(s) from this list", "Management Panel");
                    chkDealerClient.Focus();
                    return false;
                }
            }
            if (rdoTime.Checked == true)
            {
                string GetComboTimeString = "";
                GetComboTimeString = cmbHour.Text + ":" + cmbMin.Text + " " + cmbAMPM.Text;
                DateTime GetComboTime = DateTime.ParseExact(GetComboTimeString, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                string strDealerTimeValid = "";
                if (btnSave.Text == "Save")
                {
                    strDealerTimeValid = "select * from tbAdvtAdmin where   ";

                    strDealerTimeValid = strDealerTimeValid + " Starttime='" + GetComboTime.ToString("hh:mm tt") + "' ";

                    if (cmbPlayerType.Text != "All")
                    {
                        strDealerTimeValid = strDealerTimeValid + " and (Playertype='" + cmbPlayerType.Text + "' or Playertype='All')  ";
                    }

                    strDealerTimeValid = strDealerTimeValid + " and ('" + dtpStartDate.Value.ToString("dd/MMM/yyyy") + "' between startdate and  enddate";
                    strDealerTimeValid = strDealerTimeValid + " or '" + dtpEndDate.Value.ToString("dd/MMM/yyyy") + "' between startdate and  enddate )";
                }
                else
                {
                    strDealerTimeValid = "select * from tbAdvtAdmin where  ";
                    strDealerTimeValid = strDealerTimeValid + " Starttime='" + GetComboTime.ToString("hh:mm tt") + "' ";
                    if (cmbPlayerType.Text != "All")
                    {
                        strDealerTimeValid = strDealerTimeValid + " and (Playertype='" + cmbPlayerType.Text + "' or Playertype='All')  ";
                    }

                    strDealerTimeValid = strDealerTimeValid + " and ('" + dtpStartDate.Value.ToString("dd/MMM/yyyy") + "' between startdate and  enddate";
                    strDealerTimeValid = strDealerTimeValid + " or '" + dtpEndDate.Value.ToString("dd/MMM/yyyy") + "' between startdate and  enddate )";
                    strDealerTimeValid = strDealerTimeValid + " and advtid <> " + ReturnAdvtId;
                }
                DataTable dtDealerTimeValid = new DataTable();
                dtDealerTimeValid = objMainClass.fnFillDataTable(strDealerTimeValid);
                if (dtDealerTimeValid.Rows.Count > 0)
                {
                   // MessageBox.Show("This time is already used", "Management Panel");
                  //  cmbMin.Focus();
                  //  return false;
                }
            }



            //string GetComboTimeString = "";
            //GetComboTimeString = cmbHour.Text + ":" + cmbMin.Text + " " + cmbAMPM.Text;
            //DateTime GetComboTime = DateTime.ParseExact(GetComboTimeString, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            //string strDealerTimeValid = "";
            //if (btnSave.Text == "Save")
            //{
            //    strDealerTimeValid = "select * from tbAdvertisement where tokenid=0 and AdvtPlayertype=iif('" + cmbPlayerType.Text + "'='Copyleft','Dam','" + cmbPlayerType.Text + "') and cityid=" + Convert.ToInt32(cmbCityName.SelectedValue) + " and dealercode='" + cmbPlayerCode.Text + "' and  ";
            //    strDealerTimeValid = strDealerTimeValid + " Advttime='" + GetComboTime.ToString("hh:mm tt") + "' ";
            //    strDealerTimeValid = strDealerTimeValid + " and ('" + dtpStartDate.Value.ToString("dd/MMM/yyyy") + "' between advtstartdate and  advtenddate";
            //    strDealerTimeValid = strDealerTimeValid + " or '" + dtpEndDate.Value.ToString("dd/MMM/yyyy") + "' between advtstartdate and  advtenddate )";
            //}
            //else
            //{
            //    strDealerTimeValid = "select * from tbAdvertisement where tokenid=0 and AdvtPlayertype=iif('" + cmbPlayerType.Text + "'='Copyleft','Dam','" + cmbPlayerType.Text + "') and cityid=" + Convert.ToInt32(cmbCityName.SelectedValue) + " and dealercode='" + cmbPlayerCode.Text + "' and  ";
            //    strDealerTimeValid = strDealerTimeValid + " Advttime='" + GetComboTime.ToString("hh:mm tt") + "' ";
            //    strDealerTimeValid = strDealerTimeValid + " and ('" + dtpStartDate.Value.ToString("dd/MMM/yyyy") + "' between advtstartdate and  advtenddate";
            //    strDealerTimeValid = strDealerTimeValid + " or '" + dtpEndDate.Value.ToString("dd/MMM/yyyy") + "' between advtstartdate and  advtenddate )";
            //    strDealerTimeValid = strDealerTimeValid + " and advtid <> " + ReturnAdvtId;
            //}
            //DataTable dtDealerTimeValid = new DataTable();
            //dtDealerTimeValid = objMainClass.fnFillDataTable(strDealerTimeValid);
            //if (dtDealerTimeValid.Rows.Count > 0)
            //{
            //    MessageBox.Show("This time is already used", "Management Panel");
            //    cmbMin.Focus();
            //    return false;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SubmitValidationAdvt() == false) return;

            if (bgAdvtWorker.IsBusy == true)
            {
                MessageBox.Show("The upload of the add is in progress", "Management Panel");
                return;
            }
            btnRefersh.Enabled = false;
            btnSave.Enabled = false;
            if (btnSave.Text == "Save")
            {
                panBar.Visible = true;
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("spTempSaveAdvtAdmin", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;
                ReturnAdvtId = Convert.ToInt32(cmd.ExecuteScalar());
                StaticClass.constr.Close();
                bgAdvtWorker.RunWorkerAsync();
            }
            else
            {
                SaveAllData();
            }
            //New proc to save details
            //spSaveAdvtDetail_Admin
            
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
             ClearData();
        }
        private void ClearData()
        {
            panBar.Visible = false;
            btnRefersh.Enabled = true;
            btnSave.Enabled = true;
            btnDialog.Visible = true;
            btnSave.Text = "Save";
            txtAdvtName.Text = "";
            txtCompanyName.Text = "";
            cmbHour.Text = "";
            cmbMin.Text = "";
            cmbAMPM.Text = "";
            txtPath.Text = "";
            cmbPlayerType.Text = "";
            cmbAdvtMin.Text = "";
            cmbAdvtSongs.Text = "";
            ReturnDealerId = 0;
            ReturnAdvtId = 0;
            rdoMin.Checked = false;
            rdoSong.Checked = false;
            rdoTime.Checked = true;
            chkAll.Checked = false;
            chkSun.Checked = false;
            chkMon.Checked = false;
            chkTue.Checked = false;
            chkWed.Checked = false;
            chkThu.Checked = false;
            chkFri.Checked = false;
            chkSat.Checked = false;

            chkCountry.Checked = false;
            chkState.Checked = false;
            chkCity.Checked = false;
            chkDealer.Checked = false;
            chkClient.Checked = false;
            chkDealerClient.Checked = false;

            CountryCheckBox.Checked = false;
            StateCheckBox.Checked = false;
            CityCheckBox.Checked = false;
            TokenCheckBox.Checked = false;

            chkVideoMute.Checked = false;

            dtpStartDate.Value = DateTime.Now.Date;
            dtpEndDate.Value = DateTime.Now.Date;

            string Advttype = "select * from tbAdvertisementType";
            objMainClass.fnFillComboBox(Advttype, cmbAdvertisementTypeAdd, "AdvtTypeId", "AdvtTypeName", "");

            IsCountryCheckBoxClicked = true;
            IsTokenCheckBoxClicked = true;
            // FillData(dgCountry, "Country Name", "select countrycode as Id, countryName as DisplayName from CountryCodes order by countryCode");
            //AddCountryCheckBox(dgCountry);
            //  TotalCheckBoxes = dgCountry.RowCount;
            //     TotalCheckedCheckBoxes = 0;
            // CountryCheckBox.KeyUp += new KeyEventHandler(CountryCheckBox_KeyUp);
            //  CountryCheckBox.MouseClick += new MouseEventHandler(CountryCheckBox_MouseClick);


            // AddStateCheckBox(dgState);
            //    StateCheckBox.KeyUp += new KeyEventHandler(StateCheckBox_KeyUp);
            //    StateCheckBox.MouseClick += new MouseEventHandler(StateCheckBox_MouseClick);

            //    AddCityCheckBox(dgCity);
            //   CityCheckBox.KeyUp += new KeyEventHandler(CityCheckBox_KeyUp);
            //   CityCheckBox.MouseClick += new MouseEventHandler(CityCheckBox_MouseClick);


            IsDealerCheckBoxClicked = true;
            //  AddDealerCheckBox(dgDealer);
            //    DealerCheckBox.KeyUp += new KeyEventHandler(DealerCheckBox_KeyUp);
            //     DealerCheckBox.MouseClick += new MouseEventHandler(DealerCheckBox_MouseClick);

            IsClientCheckBoxClicked = true;
            //     AddClientCheckBox(dgClient);
            //      ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            //        ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);

            InitilizeGrid(dgState, "State Name");
            InitilizeGrid(dgCity, "City Name");
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            OpenDialog.FileName = "";
            OpenDialog.DefaultExt = ".mp4";
            OpenDialog.Filter = "|*.mp4";
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = OpenDialog.FileName;
            }
        }

        private void bgAdvtWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string filename = txtPath.Text;
                FileInfo objFile = new FileInfo(filename);
                string ftpServerIP = "ftp://37.61.214.210:21/AMMusicFiles/ripper/AdvtSongs/" + ReturnAdvtId + ".mp4";
                string ftpUserName = "ftpTalwinder";
                string ftpPassword = "Roop!@#123";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpServerIP));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                request.UseBinary = true;
                request.UsePassive = false;
                byte[] fileStream = System.IO.File.ReadAllBytes(filename);
                System.IO.Stream requestStream = request.GetRequestStream();
                for (int offset = 0; offset <= fileStream.Length; offset += 1024)
                {
                    bgAdvtWorker.ReportProgress(Convert.ToInt32(offset * progressBar3.Maximum / fileStream.Length));
                    int chSize = fileStream.Length - offset;
                    if (chSize > 1024)
                        chSize = 1024;
                    requestStream.Write(fileStream, offset, chSize);
                }
                requestStream.Close();
                requestStream.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                cmd.CommandText = "delete from tbAdvtAdmin where advtid=" + ReturnAdvtId;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
                return;
            }
        }

        private void bgAdvtWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar3.Value = e.ProgressPercentage;
            lblAdvtPercentage.Text = e.ProgressPercentage + "%";
        }

        private void bgAdvtWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
            SaveAllData();

        }
        private void SaveAllData()
        {

            string GetComboTimeString = "";
            if (rdoTime.Checked == true)
            {
                GetComboTimeString = cmbHour.Text + ":" + cmbMin.Text + " " + cmbAMPM.Text;
              

            }
            else
            {
                GetComboTimeString = "00:00 AM";
            }
             DateTime GetComboTime = DateTime.ParseExact(GetComboTimeString, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            lblAdvtPercentage.Text = "";
            progressBar3.Value = 0;

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spSaveAdvt_Admin", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@AdvtId", SqlDbType.BigInt));
            cmd.Parameters["@AdvtId"].Value = ReturnAdvtId;

            cmd.Parameters.Add(new SqlParameter("@AdvtDisplayName", SqlDbType.VarChar));
            cmd.Parameters["@AdvtDisplayName"].Value = txtAdvtName.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@AdvtCompanyName", SqlDbType.VarChar));
            cmd.Parameters["@AdvtCompanyName"].Value = txtCompanyName.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@AdvtStartDate", SqlDbType.DateTime));
            cmd.Parameters["@AdvtStartDate"].Value = dtpStartDate.Value.ToString("dd/MMM/yyyy");

            cmd.Parameters.Add(new SqlParameter("@AdvtEndDate", SqlDbType.DateTime));
            cmd.Parameters["@AdvtEndDate"].Value = dtpEndDate.Value.ToString("dd/MMM/yyyy");

            cmd.Parameters.Add(new SqlParameter("@AdvtFilePath", SqlDbType.VarChar));
            cmd.Parameters["@AdvtFilePath"].Value = "http://37.61.214.210/AdvtSongs/" + ReturnAdvtId + ".mp4";

            cmd.Parameters.Add(new SqlParameter("@AdvtPlayertype", SqlDbType.VarChar));
            cmd.Parameters["@AdvtPlayertype"].Value = cmbPlayerType.Text.Trim();

            cmd.Parameters.Add(new SqlParameter("@AdvtTypeId", SqlDbType.BigInt));
            cmd.Parameters["@AdvtTypeId"].Value = Convert.ToInt32(cmbAdvertisementTypeAdd.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@AdvtTime", SqlDbType.DateTime));
            cmd.Parameters["@AdvtTime"].Value = GetComboTime.ToString("hh:mm tt");

            cmd.Parameters.Add(new SqlParameter("@IsCountry", SqlDbType.Bit));
            cmd.Parameters["@IsCountry"].Value = Convert.ToByte(chkCountry.Checked);


            cmd.Parameters.Add(new SqlParameter("@IsState", SqlDbType.Bit));
            cmd.Parameters["@IsState"].Value = Convert.ToByte(chkState.Checked);

            cmd.Parameters.Add(new SqlParameter("@IsCity", SqlDbType.Bit));
            cmd.Parameters["@IsCity"].Value = Convert.ToByte(chkCity.Checked);

            cmd.Parameters.Add(new SqlParameter("@IsDealer", SqlDbType.Bit));
            cmd.Parameters["@IsDealer"].Value = Convert.ToByte(chkDealer.Checked);

            cmd.Parameters.Add(new SqlParameter("@IsClient", SqlDbType.Bit));
            cmd.Parameters["@IsClient"].Value = Convert.ToByte(chkClient.Checked);

            cmd.Parameters.Add(new SqlParameter("@StateCountryId", SqlDbType.BigInt));
            cmd.Parameters["@StateCountryId"].Value = Convert.ToInt32(cmbCountryName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@CityStateId", SqlDbType.BigInt));
            cmd.Parameters["@CityStateId"].Value = Convert.ToInt32(cmbStateName.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@CityCountryId", SqlDbType.BigInt));
            cmd.Parameters["@CityCountryId"].Value = Convert.ToInt32(cmbCityCountry.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@DealerCountryId", SqlDbType.BigInt));
            cmd.Parameters["@DealerCountryId"].Value = Convert.ToInt32(cmbDealerCountry.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@ClientCountryId", SqlDbType.BigInt));
            cmd.Parameters["@ClientCountryId"].Value = Convert.ToInt32(cmbClientCountry.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@IsToken", SqlDbType.Bit));
            cmd.Parameters["@IsToken"].Value = Convert.ToByte(chkDealerClient.Checked);

            cmd.Parameters.Add(new SqlParameter("@TokenDealerId", SqlDbType.BigInt));
            cmd.Parameters["@TokenDealerId"].Value = Convert.ToInt32(cmbDealer.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@DealerId", SqlDbType.BigInt));
            cmd.Parameters["@DealerId"].Value = ReturnDealerId;

            cmd.Parameters.Add(new SqlParameter("@IsTime", SqlDbType.Bit));
            cmd.Parameters["@IsTime"].Value = Convert.ToByte(rdoTime.Checked);
            cmd.Parameters.Add(new SqlParameter("@IsMinute", SqlDbType.Bit));
            cmd.Parameters["@IsMinute"].Value = Convert.ToByte(rdoMin.Checked);
            cmd.Parameters.Add(new SqlParameter("@IsSong", SqlDbType.Bit));
            cmd.Parameters["@IsSong"].Value = Convert.ToByte(rdoSong.Checked);

            cmd.Parameters.Add(new SqlParameter("@TotalMinutes", SqlDbType.Int));
            if (cmbAdvtMin.Text == "")
            {
                cmd.Parameters["@TotalMinutes"].Value = "0";
            }
            else
            {
                cmd.Parameters["@TotalMinutes"].Value = cmbAdvtMin.Text;
            }
            cmd.Parameters.Add(new SqlParameter("@TotalSongs", SqlDbType.Int));
            if (cmbAdvtSongs.Text == "")
            {
                cmd.Parameters["@TotalSongs"].Value = "0";
            }
            else
            {
                cmd.Parameters["@TotalSongs"].Value = cmbAdvtSongs.Text;
            }



            cmd.Parameters.Add(new SqlParameter("@IsVideo", SqlDbType.Int));
            cmd.Parameters["@IsVideo"].Value = "1";

            cmd.Parameters.Add(new SqlParameter("@IsVideoMute", SqlDbType.Int));
            cmd.Parameters["@IsVideoMute"].Value = Convert.ToByte(chkVideoMute.Checked);


            try
            {
                cmd.ExecuteNonQuery();

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = StaticClass.constr;
                cmd1.CommandText = "delete from tbAdvtAdminDetail where advtid=" + ReturnAdvtId;
                cmd1.ExecuteNonQuery();
                StaticClass.constr.Close();

                if (chkSun.Checked == true)
                {
                    SaveCountryList(1, Convert.ToByte(chkAll.Checked));
                    SaveStateList(1, Convert.ToByte(chkAll.Checked));
                    SaveCityList(1, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(1, Convert.ToByte(chkAll.Checked));
                    SaveClientList(1, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(1, Convert.ToByte(chkAll.Checked));
                }
                if (chkMon.Checked == true)
                {
                    SaveCountryList(2, Convert.ToByte(chkAll.Checked));
                    SaveStateList(2, Convert.ToByte(chkAll.Checked));
                    SaveCityList(2, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(2, Convert.ToByte(chkAll.Checked));
                    SaveClientList(2, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(2, Convert.ToByte(chkAll.Checked));
                }
                if (chkTue.Checked == true)
                {
                    SaveCountryList(3, Convert.ToByte(chkAll.Checked));
                    SaveStateList(3, Convert.ToByte(chkAll.Checked));
                    SaveCityList(3, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(3, Convert.ToByte(chkAll.Checked));
                    SaveClientList(3, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(3, Convert.ToByte(chkAll.Checked));
                }
                if (chkWed.Checked == true)
                {
                    SaveCountryList(4, Convert.ToByte(chkAll.Checked));
                    SaveStateList(4, Convert.ToByte(chkAll.Checked));
                    SaveCityList(4, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(4, Convert.ToByte(chkAll.Checked));
                    SaveClientList(4, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(4, Convert.ToByte(chkAll.Checked));
                }
                if (chkThu.Checked == true)
                {
                    SaveCountryList(5, Convert.ToByte(chkAll.Checked));
                    SaveStateList(5, Convert.ToByte(chkAll.Checked));
                    SaveCityList(5, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(5, Convert.ToByte(chkAll.Checked));
                    SaveClientList(5, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(5, Convert.ToByte(chkAll.Checked));
                }
                if (chkFri.Checked == true)
                {
                    SaveCountryList(6, Convert.ToByte(chkAll.Checked));
                    SaveStateList(6, Convert.ToByte(chkAll.Checked));
                    SaveCityList(6, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(6, Convert.ToByte(chkAll.Checked));
                    SaveClientList(6, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(6, Convert.ToByte(chkAll.Checked));
                }
                if (chkSat.Checked == true)
                {
                    SaveCountryList(7, Convert.ToByte(chkAll.Checked));
                    SaveStateList(7, Convert.ToByte(chkAll.Checked));
                    SaveCityList(7, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(7, Convert.ToByte(chkAll.Checked));
                    SaveClientList(7, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(7, Convert.ToByte(chkAll.Checked));
                }
                if ((chkSun.Checked == false) && (chkMon.Checked == false) && (chkTue.Checked == false) && (chkWed.Checked == false) && (chkThu.Checked == false) && (chkFri.Checked == false) && (chkSat.Checked == false))
                {
                    SaveCountryList(0, Convert.ToByte(chkAll.Checked));
                    SaveStateList(0, Convert.ToByte(chkAll.Checked));
                    SaveCityList(0, Convert.ToByte(chkAll.Checked));
                    SaveDealerList(0, Convert.ToByte(chkAll.Checked));
                    SaveClientList(0, Convert.ToByte(chkAll.Checked));
                    SaveTokenList(0, Convert.ToByte(chkAll.Checked));
                }

                if (btnSave.Text == "Update")
                {
                    MessageBox.Show("Advertisement is updated", "Management Panel");
                }
                else
                {
                    MessageBox.Show("Advertisement is saved", "Management Panel");
                }
                ClearData();
                FillSaveAdvt(dgAdvt, "spGetAdvertisementAdmin_Video '" + string.Format("{0:dd/MMM/yyyy}", dtpCurrentDate.Value) + "', " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Record not saved", "!! Problem !!");
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = StaticClass.constr;
                cmd1.CommandText = "delete from tbAdvtAdmin where advtid=" + ReturnAdvtId;
                cmd1.ExecuteNonQuery();
                StaticClass.constr.Close();

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd1 = new SqlCommand();
                cmd1.Connection = StaticClass.constr;
                cmd1.CommandText = "delete from tbAdvtAdminDetail where advtid=" + ReturnAdvtId;
                cmd1.ExecuteNonQuery();
                StaticClass.constr.Close();

                ClearData();
                return;
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }
        private void SaveCountryList(int WeekId, int IsAllWeek)
        {
            if (CountryCheckBox.Checked == true)
            {
             //  SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, WeekId, IsAllWeek,0,0);
              //  return;
            }
            for (int i = 0; i < dgCountry.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgCountry.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, Convert.ToInt32(dgCountry.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek,0,0);
                }
            }
        }
        private void SaveStateList(int WeekId, int IsAllWeek)
        {
            if (StateCheckBox.Checked == true)
            {
              //  SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
             //   return;
            }
            for (int i = 0; i < dgState.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgState.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, 0, Convert.ToInt32(dgState.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                }
            }
        }
        private void SaveCityList(int WeekId, int IsAllWeek)
        {
            if (CityCheckBox.Checked == true)
            {
              //  SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, WeekId, IsAllWeek, 0, 0);
              //  return;
            }
            for (int i = 0; i < dgCity.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgCity.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, 0, 0, Convert.ToInt32(dgCity.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                }
            }
        }
        private void SaveDealerList(int WeekId, int IsAllWeek)
        {
            if (DealerCheckBox.Checked == true)
            {
               // SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, WeekId, IsAllWeek, 0, 0);
              //  return;
            }
            for (int i = 0; i < dgDealer.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDealer.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, Convert.ToInt32(dgDealer.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                }
            }
        }
        private void SaveClientList(int WeekId, int IsAllWeek)
        {
            if (ClientCheckBox.Checked == true)
            {
              //  SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, WeekId, IsAllWeek,0,0);
              //  return;
            }
            for (int i = 0; i < dgClient.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgClient.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, Convert.ToInt32(dgClient.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                }
            }
        }
        private void SaveTokenList(int WeekId, int IsAllWeek)
        {
            if (TokenCheckBox.Checked == true)
            {
              //  SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 1);
              //  return;
            }
            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgToken.Rows[i].Cells[1].Value) == true)
                {
                    SaveAdvtDetail(ReturnAdvtId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0);
                }
            }
        }

        private void SaveAdvtDetail(Int32 AdvtId, Int32 CountryId, Int32 StateId, Int32 CityId, Int32 DealerId, Int32 ClientId, int IsAllCountry, int IsAllState, int IsAllCity, int IsAllDealer, int IsAllClient, int WeekId, int IsAllWeek, Int32 TokenId, int IsAllToken)
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spSaveAdvtDetail_Admin", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@AdvtId", SqlDbType.BigInt));
            cmd.Parameters["@AdvtId"].Value = AdvtId;

            cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.BigInt));
            cmd.Parameters["@CountryId"].Value = CountryId;

            cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.BigInt));
            cmd.Parameters["@StateId"].Value = StateId;

            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.BigInt));
            cmd.Parameters["@CityId"].Value = CityId;

            cmd.Parameters.Add(new SqlParameter("@DealerId", SqlDbType.BigInt));
            cmd.Parameters["@DealerId"].Value = DealerId;

            cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.BigInt));
            cmd.Parameters["@ClientId"].Value = ClientId;

            cmd.Parameters.Add(new SqlParameter("@IsAllCountry", SqlDbType.Bit));
            cmd.Parameters["@IsAllCountry"].Value = Convert.ToByte(IsAllCountry);

            cmd.Parameters.Add(new SqlParameter("@IsAllState", SqlDbType.Bit));
            cmd.Parameters["@IsAllState"].Value = Convert.ToByte(IsAllState);

            cmd.Parameters.Add(new SqlParameter("@IsAllCity", SqlDbType.Bit));
            cmd.Parameters["@IsAllCity"].Value = Convert.ToByte(IsAllCity);

            cmd.Parameters.Add(new SqlParameter("@IsAllDealer", SqlDbType.Bit));
            cmd.Parameters["@IsAllDealer"].Value = Convert.ToByte(IsAllDealer);

            cmd.Parameters.Add(new SqlParameter("@IsAllClient", SqlDbType.Bit));
            cmd.Parameters["@IsAllClient"].Value = Convert.ToByte(IsAllClient);

            cmd.Parameters.Add(new SqlParameter("@WeekId", SqlDbType.Int));
            cmd.Parameters["@WeekId"].Value = WeekId;

            cmd.Parameters.Add(new SqlParameter("@IsAllWeek", SqlDbType.Bit));
            cmd.Parameters["@IsAllWeek"].Value = Convert.ToByte(IsAllWeek);

            cmd.Parameters.Add(new SqlParameter("@TokenId", SqlDbType.BigInt));
            cmd.Parameters["@TokenId"].Value = TokenId;

            cmd.Parameters.Add(new SqlParameter("@IsAllToken", SqlDbType.Bit));
            cmd.Parameters["@IsAllToken"].Value = Convert.ToByte(IsAllToken);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Record not save", "!! Problem !!");
                ClearData();
                return;
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void InitilizeAdvertisement(DataGridView dgGrid)
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.Rows.Clear();
            }
            if (dgGrid.Columns.Count > 0)
            {
                dgGrid.Columns.Clear();
            }

            dgGrid.Columns.Add("Advtid", "Advt Id");
            dgGrid.Columns["Advtid"].Width = 0;
            dgGrid.Columns["Advtid"].Visible = false;
            dgGrid.Columns["Advtid"].ReadOnly = true;

            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
            chkColumn.HeaderText = "";
            chkColumn.DataPropertyName = "chkColumn";
            dgGrid.Columns.Add(chkColumn);
            chkColumn.Width = 30;
            chkColumn.Visible = true;
            dgGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;



            dgGrid.Columns.Add("aName", "Advertisement Name");
            dgGrid.Columns["aName"].Width = 230;
            dgGrid.Columns["aName"].Visible = true;
            dgGrid.Columns["aName"].ReadOnly = true;
            dgGrid.Columns["aName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgGrid.Columns.Add("aType", "Type");
            dgGrid.Columns["aType"].Width = 200;
            dgGrid.Columns["aType"].Visible = true;
            dgGrid.Columns["aType"].ReadOnly = true;
            dgGrid.Columns["aType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgGrid.Columns.Add("StartDate", "Start Date");
            dgGrid.Columns["StartDate"].Width = 180;
            dgGrid.Columns["StartDate"].Visible = true;
            dgGrid.Columns["StartDate"].ReadOnly = true;
            dgGrid.Columns["StartDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("EndDate", "End Date");
            dgGrid.Columns["EndDate"].Width = 150;
            dgGrid.Columns["EndDate"].Visible = true;
            dgGrid.Columns["EndDate"].ReadOnly = true;
            dgGrid.Columns["EndDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Time", "Time");
            dgGrid.Columns["Time"].Width = 100;
            dgGrid.Columns["Time"].Visible = true;
            dgGrid.Columns["Time"].ReadOnly = true;
            dgGrid.Columns["Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("pType", "Player Type");
            dgGrid.Columns["pType"].Width = 180;
            dgGrid.Columns["pType"].Visible = true;
            dgGrid.Columns["pType"].ReadOnly = true;
            dgGrid.Columns["pType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("cName", "Company Name");
            dgGrid.Columns["cName"].Width = 220;
            dgGrid.Columns["cName"].Visible = false;
            dgGrid.Columns["cName"].ReadOnly = true;
            

            DataGridViewLinkColumn EditAdvt = new DataGridViewLinkColumn();
            EditAdvt.HeaderText = "Edit";
            EditAdvt.Text = "Edit";
            EditAdvt.DataPropertyName = "Edit";
            dgGrid.Columns.Add(EditAdvt);
            EditAdvt.UseColumnTextForLinkValue = true;
            EditAdvt.Width = 70;
            EditAdvt.Visible = true;
            dgGrid.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteAdvt = new DataGridViewLinkColumn();
            DeleteAdvt.HeaderText = "Delete";
            DeleteAdvt.Text = "Delete";
            DeleteAdvt.DataPropertyName = "Delete";
            dgGrid.Columns.Add(DeleteAdvt);
            DeleteAdvt.UseColumnTextForLinkValue = true;
            DeleteAdvt.Width = 70;
            DeleteAdvt.Visible = true;
            dgGrid.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.None ;



            

        }
        private void FillSaveAdvt(DataGridView dgGrid, string Query)
        {

            int iCtr;
            DataTable dtDetail;
            dtDetail = objMainClass.fnFillDataTable(Query);
            InitilizeAdvertisement(dgGrid);
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {//chkColumn
                    dgGrid.Rows.Add();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["AdvtId"].Value = dtDetail.Rows[iCtr]["AdvtId"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells[1].Value = Convert.ToBoolean(dtDetail.Rows[iCtr]["IsActive"]);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["aName"].Value = dtDetail.Rows[iCtr]["AdvtName"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["aType"].Value = dtDetail.Rows[iCtr]["AdvtTypeName"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["StartDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[iCtr]["StartDate"]);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["EndDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[iCtr]["EndDate"]);
                    if (Convert.ToBoolean(dtDetail.Rows[iCtr]["IsTime"]) == true)
                    {
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time"].Value = string.Format("{0:hh:mm tt}", dtDetail.Rows[iCtr]["StartTime"]);
                    }
                    else if (Convert.ToBoolean(dtDetail.Rows[iCtr]["IsMinute"]) == true)
                    {
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time"].Value ="After "+  dtDetail.Rows[iCtr]["TotalMinutes"].ToString() + " min";
                    }
                    else if (Convert.ToBoolean(dtDetail.Rows[iCtr]["IsSong"]) == true)
                    {
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time"].Value = "After " + dtDetail.Rows[iCtr]["TotalSongs"].ToString() + " song";
                    }
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["pType"].Value = dtDetail.Rows[iCtr]["PlayerType"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[iCtr]["CmpName"];
                }
                foreach (DataGridViewRow row in dgGrid.Rows)
                {
                    row.Height = 30;
                }
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            FillSaveAdvt(dgAdvt, "spGetAdvertisementAdmin_Video'" + string.Format("{0:dd/MMM/yyyy}", dtpCurrentDate.Value) + "', " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + "");
        }

        private void dgAdvt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            if (e.ColumnIndex == 9)
            {
                ClearData();
                string Localstr = "";

                Localstr = " select * from tbAdvtAdmin ";
                Localstr = Localstr + "	where advtid = " + Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells["AdvtId"].Value);
                DataTable dtDetail;
                DataTable dtWeek;
                DataTable dtCommon;

                dtDetail = objMainClass.fnFillDataTable(Localstr);
                if ((dtDetail.Rows.Count > 0))
                {
                    panSearch.Visible = false;
                    panAddNew.Visible = true;
                    panAddNew.Dock = DockStyle.Fill;
                    btnDialog.Visible = false;
                    SetButtonColor(btnMenuAddNew); 
                    btnSave.Text = "Update";
                    ReturnAdvtId = Convert.ToInt32(dtDetail.Rows[0]["AdvtId"]);
                    txtAdvtName.Text = dtDetail.Rows[0]["AdvtName"].ToString();
                    cmbAdvertisementTypeAdd.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["AdvtTypeId"]);
                    dtpStartDate.Value = Convert.ToDateTime(string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[0]["StartDate"]));
                    dtpEndDate.Value = Convert.ToDateTime(string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[0]["EndDate"]));
                    rdoTime.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsTime"]);
                    if (rdoTime.Checked == true)
                    {
                        cmbHour.Text = string.Format("{0:hh}", dtDetail.Rows[0]["StartTime"]);
                        cmbMin.Text = string.Format("{0:mm}", dtDetail.Rows[0]["StartTime"]);
                        cmbAMPM.Text = string.Format("{0:tt}", dtDetail.Rows[0]["StartTime"]);
                    }
                    else
                    {
                        cmbHour.Text = "";
                        cmbMin.Text = "";
                        cmbAMPM.Text = "";
                    }
                    rdoMin.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsMinute"]);
                    if (rdoMin.Checked == true)
                    {
                        if (dtDetail.Rows[0]["TotalMinutes"].ToString() == "5")
                        {
                            cmbAdvtMin.Text ="0"+ dtDetail.Rows[0]["TotalMinutes"].ToString();
                        }
                        else
                        {
                            cmbAdvtMin.Text = dtDetail.Rows[0]["TotalMinutes"].ToString();
                        }

                    }
                    else
                    {
                        cmbAdvtMin.Text = "";
                    }

                    rdoSong.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsSong"]);
                    if (rdoSong.Checked == true)
                    {
                        if (dtDetail.Rows[0]["TotalSongs"].ToString() != "10")
                        {
                            cmbAdvtSongs.Text = "0" + dtDetail.Rows[0]["TotalSongs"].ToString();
                        }
                        else
                        {
                            cmbAdvtSongs.Text = dtDetail.Rows[0]["TotalSongs"].ToString();
                        }
                    }
                    else
                    {
                        cmbAdvtSongs.Text = "";
                    }
                    cmbPlayerType.Text = dtDetail.Rows[0]["PlayerType"].ToString();
                    txtCompanyName.Text = dtDetail.Rows[0]["CmpName"].ToString();
                    txtPath.Text = dtDetail.Rows[0]["FilePath"].ToString();
                    chkCountry.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsCountry"]);
                    chkState.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsState"]);
                    chkCity.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsCity"]);
                    chkDealer.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsDealer"]);
                    chkClient.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsClient"]);
                    chkDealerClient.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsToken"]);

                    chkVideoMute.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsVideoMute"]);

                    ReturnDealerId = Convert.ToInt32(dtDetail.Rows[0]["DealerId"]);

                    #region Get Week Days
                    Localstr = "";
                    Localstr = "select distinct WeekId,IsAllWeek from tbAdvtAdminDetail  where advtid=" + ReturnAdvtId + " and WeekId != IsAllWeek";
                    dtWeek = objMainClass.fnFillDataTable(Localstr);
                    if ((dtWeek.Rows.Count > 0))
                    {
                        chkAll.Checked = Convert.ToBoolean(dtWeek.Rows[0]["IsAllWeek"]);
                        for (int iCtr = 0; (iCtr <= (dtWeek.Rows.Count - 1)); iCtr++)
                        {
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 1)
                            {
                                chkSun.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 2)
                            {
                                chkMon.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 3)
                            {
                                chkTue.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 4)
                            {
                                chkWed.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 5)
                            {
                                chkThu.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 6)
                            {
                                chkFri.Checked = true;
                            }
                            if (Convert.ToByte(dtWeek.Rows[iCtr]["WeekId"]) == 7)
                            {
                                chkSat.Checked = true;
                            }
                        }
                    }
                    #endregion

                    #region Get Country List
                    Localstr = "";
                    Localstr = "select distinct countryId, IsAllCountry from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and countryId != IsAllCountry";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        CountryCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllCountry"]);
                        if (CountryCheckBox.Checked == false)
                        {
                            for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                            {
                                for (int i = 0; i < dgCountry.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dgCountry.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["countryId"]))
                                    {
                                        dgCountry.Rows[i].Cells[1].Value = true;
                                    }
                                }
                            }
                        }
                        if (CountryCheckBox.Checked == true)
                        {
                            CountryCheckBoxClick(CountryCheckBox);
                        }
                    }
                    #endregion

                    #region Get State List
                    Localstr = "";
                    Localstr = "select distinct Stateid, IsAllState  from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and Stateid != IsAllState  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        StateCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllState"]);
                       
                        cmbCountryName.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["StateCountryId"]);
                        for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                        {
                            for (int i = 0; i < dgState.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dgState.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["Stateid"]))
                                {
                                    dgState.Rows[i].Cells[1].Value = true;
                                }
                            }
                        }
                        if (StateCheckBox.Checked == true)
                        {
                            StateCheckBoxClick(StateCheckBox);
                        } 
                    }
                    #endregion

                    #region Get City List
                    Localstr = "";
                    Localstr = "select distinct CityId, IsAllCity from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and CityId != IsAllCity  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        CityCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllCity"]);

                        cmbCityCountry.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["CityCountryId"]);
                        cmbStateName.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["CityStateId"]);
                        if (CityCheckBox.Checked == false)
                        {
                            for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                            {
                                for (int i = 0; i < dgCity.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dgCity.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["CityId"]))
                                    {
                                        dgCity.Rows[i].Cells[1].Value = true;
                                    }
                                }
                            }
                        }
                        if (CityCheckBox.Checked == true)
                        {
                            CityCheckBoxClick(CityCheckBox);
                        }
                    }
                    #endregion

                    #region Get Dealer List
                    Localstr = "";
                    Localstr = "select distinct DealerId, IsAllDealer from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and DealerId != IsAllDealer  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        DealerCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllDealer"]);

                        cmbDealerCountry.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["DealerCountryId"]);
                        if (DealerCheckBox.Checked == false)
                        {
                            for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                            {
                                for (int i = 0; i < dgDealer.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dgDealer.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["DealerId"]))
                                    {
                                        dgDealer.Rows[i].Cells[1].Value = true;
                                    }
                                }
                            }
                        }
                        if (DealerCheckBox.Checked == true)
                        {
                            DealerCheckBoxClick(DealerCheckBox);
                        }
                    }
                    #endregion
                    
                    #region Get Client List
                    Localstr = "";
                    Localstr = "select distinct Clientid, IsAllClient from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and Clientid != IsAllClient  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        ClientCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllClient"]);

                        cmbClientCountry.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["ClientCountryId"]);
                        if (ClientCheckBox.Checked == false)
                        {
                            for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                            {
                                for (int i = 0; i < dgClient.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dgClient.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["Clientid"]))
                                    {
                                        dgClient.Rows[i].Cells[1].Value = true;
                                    }
                                }
                            }
                        }
                        if (ClientCheckBox.Checked == true)
                        {
                            ClientCheckBoxClick(ClientCheckBox);
                        }
                    }
                    #endregion

                    #region Get Token List
                    Localstr = "";
                    Localstr = "select distinct TokenId , IsAllToken from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and TokenId != IsAllToken  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {
                        TokenCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllToken"]);

                        cmbDealer.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["TokenDealerId"]);
                        if (TokenCheckBox.Checked == false)
                        {
                            for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                            {
                                for (int i = 0; i < dgToken.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["TokenId"]))
                                    {
                                        dgToken.Rows[i].Cells[1].Value = true;
                                    }
                                }
                            }
                        }
                        if (TokenCheckBox.Checked == true)
                        {
                            TokenCheckBoxClick(TokenCheckBox);
                        }
                    }
                    #endregion
                }
            }

            if (e.ColumnIndex == 10)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                if (dgAdvt.Rows.Count <= 0) return;
                if (Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells[0].Value) != 0)
                {
                    result = MessageBox.Show("Are you sure to delete this advertisement?", "Management Panel", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            string ftpServerIP = "37.61.214.210:21/AMMusicFiles/ripper/AdvtSongs/" + ReturnAdvtId + ".mp4";
                            string ftpUserName = "ftpTalwinder";
                            string ftpPassword = "Roop!@#123";
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + "/" + dgAdvt.Rows[e.RowIndex].Cells[0].Value + ".mp4");
                            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                            request.Method = WebRequestMethods.Ftp.DeleteFile;
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            Console.WriteLine("Delete status: {0}", response.StatusDescription);
                            response.Close();
                        }
                        catch (Exception ex)
                        {
                            goto Nex;
                        }
                    Nex:
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = StaticClass.constr;
                        cmd.CommandText = "delete from tbAdvtAdmin where advtid=" + Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells[0].Value);
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = StaticClass.constr;
                        cmd2.CommandText = "delete from tbAdvtAdminDetail where advtid=" + Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells[0].Value);
                        cmd2.ExecuteNonQuery();
                        StaticClass.constr.Close();
                        dgAdvt.Rows.RemoveAt(e.RowIndex);
                    }
                }
                ClearData();
            }
        }

        private void chkDealerClient_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDealerClient.Checked == true)
            {
                if (cmbPlayerType.Text == "")
                {
                    MessageBox.Show("Please select player type", "Token Admin");
                    chkDealerClient.Checked = false;
                    cmbPlayerType.Focus();
                    return;
                }
                panToken.Visible = true;
                tlpMain.ColumnStyles[5].Width = 21;
            }
            else
            {
                panToken.Visible = false;
                tlpMain.ColumnStyles[5].Width = 0;
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
            dgToken.Columns["pName"].Visible = false;
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

        private void cmbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsTokenCheckBoxClicked = true;
            if (Convert.ToInt32(cmbDealer.SelectedValue) == 0)
            {
                InitilizeGrid();
                return;
            }
            FillData();
        }
        private void FillData()
        {
            string sQr = "";
            sQr = "SELECT AMPlayerTokens.TokenID , (iif(token='used','',isnull(tokennobkp,'')) + ' ' + convert(varchar(100) ,Tokenid)) as tNo, isnull(AMPlayerTokens.Location,'') as Location,";
            sQr = sQr + " isnull(tbCity.CityName,'') as CityName, isnull(tbState.StateName,'') as StateName, isnull(CountryCodes.CountryName,'') as CountryName,isnull(AMPlayerTokens.PersonName ,'') as PersonName , AMPlayerTokens.userid, isnull(AMPlayerTokens.IsStore,0) as IsStore, isnull(AMPlayerTokens.IsStream,0) as IsStream FROM  AMPlayerTokens ";
            sQr = sQr + " LEFT OUTER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId LEFT OUTER JOIN tbState ON AMPlayerTokens.StateId = tbState.StateId LEFT OUTER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode";
            sQr = sQr + " WHERE AMPlayerTokens.ClientID = " + Convert.ToInt32(cmbDealer.SelectedValue) + " ";
            if (cmbPlayerType.Text == "Asian")
            {
                sQr = sQr + "  and AMPlayerTokens.IsAsian=1 and AMPlayerTokens.pVersion= 'Normal'";
            }
            if (cmbPlayerType.Text == "Copyright")
            {
                sQr = sQr + "  and AMPlayerTokens.IsCopyright=1 and AMPlayerTokens.pVersion= 'Normal'";
            }
            if (cmbPlayerType.Text == "Copyleft")
            {
                sQr = sQr + "  and AMPlayerTokens.IsCopyleft=1 and AMPlayerTokens.pVersion= 'Normal'";
            }
            if (cmbPlayerType.Text == "NativeCL")
            {
                sQr = sQr + "  and AMPlayerTokens.IsDam=1 and AMPlayerTokens.pVersion= 'NativeCL'";
            }
            if (cmbPlayerType.Text == "NativeCR")
            {
                sQr = sQr + "  and AMPlayerTokens.IsCopyright=1 and AMPlayerTokens.pVersion= 'NativeCR'";
            }
            if (cmbPlayerType.Text == "NativeAsian")
            {
                sQr = sQr + "  and AMPlayerTokens.IsAsian=1 and AMPlayerTokens.pVersion= 'NativeAsian'";
            }
            
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
            TotalCheckBoxesToken = dgToken.RowCount;
            TotalCheckedCheckBoxesToken = 0;

           // dgToken.ClearSelection();

            if (ReturnAdvtId != 0)
            {
                #region Get Token List
                string Localstr = "";
                Localstr = "select distinct TokenId , IsAllToken from tbAdvtAdminDetail where advtid=" + ReturnAdvtId + " and TokenId != IsAllToken  ";
                DataTable dtCommon = new DataTable();
                dtCommon = objMainClass.fnFillDataTable(Localstr);
                if ((dtCommon.Rows.Count > 0))
                {
                    TokenCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllToken"]);

                   // cmbDealer.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["TokenDealerId"]);
                    if (TokenCheckBox.Checked == false)
                    {
                        for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                        {
                            for (int i = 0; i < dgToken.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["TokenId"]))
                                {
                                    dgToken.Rows[i].Cells[1].Value = true;
                                }
                            }
                        }
                    }
                    if (TokenCheckBox.Checked == true)
                    {
                        TokenCheckBoxClick(TokenCheckBox);
                    }
                }
                #endregion
            }

            dgToken.EndEdit();

        }

       

       

        


        #region Add Token Check Box
        private void AddTokenCheckBox(DataGridView dgGrid)
        {
            TokenCheckBox = new CheckBox();
            TokenCheckBox.Size = new Size(15, 15);
            //Add the CheckBox into the DataGridView
            dgGrid.Controls.Add(TokenCheckBox);

        }

        private void dgToken_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsTokenCheckBoxClicked)
                TokenRowCheckBoxClick((DataGridViewCheckBoxCell)dgToken[e.ColumnIndex, e.RowIndex]);
        }
        private void dgToken_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgToken.CurrentCell is DataGridViewCheckBoxCell)
                dgToken.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgToken_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetTokenCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }
        
        private void ResetTokenCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgToken.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - StateCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - StateCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            TokenCheckBox.Location = oPoint;
        }
        private void TokenRowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxesToken < TotalCheckBoxesToken)
                    TotalCheckedCheckBoxesToken++;
                else if (TotalCheckedCheckBoxesToken > 0)
                    TotalCheckedCheckBoxesToken--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxesToken < TotalCheckBoxesToken)
                    TokenCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxesToken == TotalCheckBoxesToken)
                    TokenCheckBox.Checked = true;
            }
        }

        private void TokenCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                TokenCheckBoxClick((CheckBox)sender);
        }
        private void TokenCheckBox_MouseClick(object sender, MouseEventArgs e)
        {

            TokenCheckBoxClick((CheckBox)sender);
        }
        private void TokenCheckBoxClick(CheckBox HCheckBox)
        {
            IsTokenCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dgToken.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells[1]).Value = HCheckBox.Checked;

            dgToken.RefreshEdit();

            TotalCheckedCheckBoxesToken = HCheckBox.Checked ? TotalCheckBoxesToken : 0;

            IsTokenCheckBoxClicked = false;
        }

        #endregion

        private void dgToken_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 9)
            {
                frmTokenInformation frm = new frmTokenInformation();
                StaticClass.DealerTokenId = 0;
                StaticClass.dealerUserId = Convert.ToInt32(dgToken.Rows[e.RowIndex].Cells["uId"].Value);
                StaticClass.DealerDfClientId = Convert.ToInt32(cmbDealer.SelectedValue);
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

        private void cmbSearchDealer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 order by DFClientID desc";
            objMainClass.fnFillComboBox(str, cmbSearchDealer, "DFClientID", "ClientName", "");
        }

        private void cmbSearchDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            FillSaveAdvt(dgAdvt, "spGetAdvertisementAdmin_Video '" + string.Format("{0:dd/MMM/yyyy}", dtpCurrentDate.Value) + "', " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + "");
        }

        private void btnMenuAddNew_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuAddNew); 
            panSearch.Visible = false;
            panAddNew.Visible = true;
            panAddNew.Dock = DockStyle.Fill;
        }

        private void btnMenuSearch_Click(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch); 
            panSearch.Visible = true;
            panAddNew.Visible = false;
            panSearch.Dock = DockStyle.Fill;
        }
        private void SetButtonColor(Button btnName)
        {
            Color light = Color.FromName("ControlLightLight");
            Color bLight = Color.FromName("Control");
            btnMenuSearch.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAddNew.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnName.BackColor = Color.White;
        }

        private void rdoMin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMin.Checked == true)
            {
                lblName.Text = "Minutes";
                cmbAdvtMin.Location = new Point(1025, 67);
                cmbAdvtMin.Visible = true;
                panTime.Visible = false;
                cmbAdvtSongs.Visible = false;
            }
        }

        private void rdoTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTime.Checked == true)
            {
                lblName.Text = "Time";
                panTime.Location = new Point(1025, 67);
                cmbAdvtMin.Visible = false;
                panTime.Visible = true;
                cmbAdvtSongs.Visible = false;
            }
        }

        private void rdoSong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSong.Checked == true)
            {
                lblName.Text = "Song";
                cmbAdvtSongs.Location = new Point(1025, 67);
                cmbAdvtMin.Visible = false;
                panTime.Visible = false;
                cmbAdvtSongs.Visible = true;
            }
        }


        private void moveUp(DataGridView dgGrid)
        {
            if (dgGrid.RowCount > 0)
            {
                if (dgGrid.SelectedRows.Count > 0)
                {
                    int rowCount = dgGrid.Rows.Count;
                    int index = dgGrid.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dgGrid.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dgGrid.ClearSelection();

                    SaveSequance(dgGrid);
                    dgGrid.Rows[index - 1].Selected = true;
                }
            }
        }
        private void moveDown(DataGridView dgGrid)
        {
            if (dgGrid.RowCount > 0)
            {
                if (dgGrid.SelectedRows.Count > 0)
                {
                    int rowCount = dgGrid.Rows.Count;
                    int index = dgGrid.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dgGrid.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dgGrid.ClearSelection();

                    SaveSequance(dgGrid);
                    dgGrid.Rows[index + 1].Selected = true;
                }
            }
        }

        private void dgAdvt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
                {
                    return;
                }
              moveUp(dgAdvt);
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
                {
                    return;
                }
               moveDown(dgAdvt);
            }
          e.Handled = true;
        }

        private void dgAdvt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgAdvt_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgAdvt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
                {
                    return;
                }
                dgAdvt.EndEdit();
                if (Convert.ToBoolean(dgAdvt.Rows[e.RowIndex].Cells[1].Value) == true)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "update tbAdvtAdmin set IsActive=1 where advtid=" + Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                }
                else
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "update tbAdvtAdmin set IsActive=0 where advtid=" + Convert.ToInt32(dgAdvt.Rows[e.RowIndex].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    MessageBox.Show("This add it will not play anymore", "Management Panel");
                }
            }
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
            {
                return;
            }
            moveUp(dgAdvt);
        }

        private void picDown_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSearchDealer.SelectedValue) == 0)
            {
                return;
            }
            moveDown(dgAdvt);
        }
        private void SaveSequance(DataGridView dgGrid)
        {
            string sWr = "";
            if (dgGrid.Rows.Count == 0) return;
            int Srno = 0;
            for (int i = 0; i <= dgGrid.Rows.Count - 1; i++)
            {
                Srno = Srno + 1;
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                cmd.CommandText = "update tbAdvtAdmin set srNo="+Srno+" where advtid=" + Convert.ToInt32(dgAdvt.Rows[i].Cells[0].Value);
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
            }
        }
    }
}
