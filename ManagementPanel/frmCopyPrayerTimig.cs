using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmCopyPrayerTimig : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        gblClass objMainClass = new gblClass();
        Int32 ReturnDealerId = 0;
        Int32 ReturnPrayerId = 0;
        Int32 ModifyPrayerId = 0;
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



        CheckBox TokenCheckBox = null;
        bool IsTokenCheckBoxClicked = false;
        int TotalCheckBoxesToken = 0;
        int TotalCheckedCheckBoxesToken = 0;



        public frmCopyPrayerTimig()
        {
            InitializeComponent();
        }

         
        

        private void chkCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCountry.Checked == true)
            {

                FillCountry();
                panCountry.Visible = true;
                tlpMain.ColumnStyles[0].Width = 15;
            }
            else
            {
                panCountry.Visible = false;
                tlpMain.ColumnStyles[0].Width = 0;
                InitilizeGrid(dgCountry, "Country Name");
                FillCountryState();
                FillCountryDealer();

            }
        }

        private void chkState_CheckedChanged(object sender, EventArgs e)
        {
            if (chkState.Checked == true)
            {

                panState.Visible = true;
                tlpMain.ColumnStyles[1].Width = 16;
               
            }
            else
            {
                panState.Visible = false;
                tlpMain.ColumnStyles[1].Width = 0;
                FillStateCity();
            }
            if (chkDealerClient.Checked == true)
            {
                IsTokenCheckBoxClicked = true;
                TokenCheckBox.Checked = false;
                FillData();
            }
        }

        private void chkCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCity.Checked == true)
            {

                panCity.Visible = true;
                tlpMain.ColumnStyles[2].Width = 16;
                
            }
            else
            {
                panCity.Visible = false;
                tlpMain.ColumnStyles[2].Width = 0;
            }
            if (chkDealerClient.Checked == true)
            {
                IsTokenCheckBoxClicked = true;
                TokenCheckBox.Checked = false;
                FillData();
            }
        }

        private void chkDealer_CheckedChanged(object sender, EventArgs e)
        {

            if (chkDealer.Checked == true)
            {

                chkDealerClient.Checked = true;

                FillCountryDealer();
                panDealer.Visible = true;
                tlpMain.ColumnStyles[5].Width = 16;
            }
            else
            {
                panDealer.Visible = false;
                tlpMain.ColumnStyles[5].Width = 0;
                InitilizeGrid();
            }
        }


        private void chkDealerClient_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDealerClient.Checked == true)
            {
                panToken.Visible = true;
                tlpMain.ColumnStyles[6].Width = 21;
                IsTokenCheckBoxClicked = true;
                TokenCheckBox.Checked = false;
                FillData();
            }
            else
            {
                panToken.Visible = false;
                tlpMain.ColumnStyles[6].Width = 0;
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
            ModifyToken.Visible = false;
            dgToken.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgToken.Columns.Add("uId", "uId");
            dgToken.Columns["uId"].Width = 0;
            dgToken.Columns["uId"].Visible = false;
            dgToken.Columns["uId"].ReadOnly = true;

        }

        private void FillData()
        {

            string DealerId = "";
            string stateId = "";
            string CityId = "";
            string CountryId = "";

            

            CountryId = GetReturnId(dgCountry);
            DealerId = GetReturnId(dgDealer);
            stateId = GetReturnId(dgState);
            CityId = GetReturnId(dgCity);

            if ((chkState.Checked == true) && (stateId==""))
            {
                stateId = "0";
            }
            if ((chkCity.Checked == true) && (CityId==""))
            {
                CityId = "0";
            }
            TokenCheckBox.Checked = false;
            if ((CountryId == "") && (stateId == "") && (CityId == ""))
            {
                InitilizeGrid();
                return;
            }
            string sQr = "";
            string vQry = " where ";
            sQr = "SELECT AMPlayerTokens.TokenID , iif(isnull(AMPlayerTokens.tokenno,'')='' ,iif(AMPlayerTokens.token='used',convert(varchar(100) ,AMPlayerTokens.Tokenid),AMPlayerTokens.token),AMPlayerTokens.tokenno) as tNo, isnull(AMPlayerTokens.Location,'') as Location,";
            sQr = sQr + " isnull(tbCity.CityName,'') as CityName, isnull(tbState.StateName,'') as StateName, isnull(CountryCodes.CountryName,'') as CountryName,isnull(AMPlayerTokens.PersonName ,'') as PersonName , AMPlayerTokens.userid, isnull(AMPlayerTokens.IsStore,0) as IsStore, isnull(AMPlayerTokens.IsStream,0) as IsStream FROM  AMPlayerTokens ";
            sQr = sQr + " LEFT OUTER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId LEFT OUTER JOIN tbState ON AMPlayerTokens.StateId = tbState.StateId LEFT OUTER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode";
            

            if (CountryId != "")
            {
                if (vQry != " where ")
                {
                    if ((stateId != "") || (CityId != ""))
                    {
                        vQry = vQry + " and ";
                    }
                }
                vQry = vQry + " AMPlayerTokens.CountryId in( " + CountryId + " )";
            }
            if (stateId != "")
            {
                if (vQry != " where ")
                {
                    if ((CountryId != "") || (CityId != ""))
                    {
                        vQry = vQry + " and ";
                    }
                }
                vQry = vQry + " AMPlayerTokens.stateid in( " + stateId + " )";
            }

            if (CityId != "")
            {
                if (vQry != " where ")
                {
                    if ((CountryId != "") || (stateId != ""))
                    {
                        vQry = vQry + " and ";
                    }
                }
                vQry = vQry + " AMPlayerTokens.cityid in( " + CityId + " )";
            }



            sQr = sQr + vQry;

            DataTable dtDetail = new DataTable();
            InitilizeGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgToken.Rows.Add();
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells["Id"].Value = dtDetail.Rows[i]["tokenid"];
                    dgToken.Rows[dgToken.Rows.Count - 1].Cells[1].Value = 1;
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
        }
        private void FillDataParam(DataGridView dgGrid, string DisplayName, string sQr)
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

        private void cmbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCountry();
            FillCountryState();
            FillCountryDealer();

        }
        private void FillCountry()
        {
            IsDealerCheckBoxClicked = true;
            DealerCheckBox.Checked = false;



            IsCountryCheckBoxClicked = true;
            CountryCheckBox.Checked = false;
            string strCou = "";
            strCou = "";
            strCou = "SELECT distinct CountryCodes.CountryCode as Id, CountryCodes.CountryName as DisplayName FROM AMPlayerTokens ";
            strCou = strCou + " INNER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode ";
            strCou = strCou + " order by countryCode";

            FillDataParam(dgCountry, "Country Name", strCou);
            TotalCheckBoxes = dgCountry.RowCount;
            TotalCheckedCheckBoxes = 0;
        }
        private void frmPrayer_Load(object sender, EventArgs e)
        {
            dtpCurrentDate.Value = DateTime.Now;
            string str = "";
            str = "select * from CountryCodes order by countryCode";
            objMainClass.fnFillComboBox(str, cmbCountryName, "countrycode", "countryName", "");

             
            

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


            IsTokenCheckBoxClicked = true;
            AddTokenCheckBox(dgToken);
            TokenCheckBox.KeyUp += new KeyEventHandler(TokenCheckBox_KeyUp);
            TokenCheckBox.MouseClick += new MouseEventHandler(TokenCheckBox_MouseClick);



            InitilizeGrid(dgState, "State Name");
            InitilizeGrid(dgCity, "City Name");
            InitilizeGrid(dgDealer, "Customer Name");

            InitilizeGrid();



        }
        #region Add Country Check Box
        private void CountryCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                CountryCheckBoxClick((CheckBox)sender);
        }
        private void CountryCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            CountryCheckBoxClick((CheckBox)sender);
            FillCountryState();
            FillCountryDealer();

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
            FillStateCity();
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
            FillData();
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

        
         
        private void btnSave_Click(object sender, EventArgs e)
        {
            


            if (SubmitValidation() == false) return;

            if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
            StaticClass.constr.Open();
            SqlCommand cmdOnline = new SqlCommand();
            cmdOnline.Connection = StaticClass.constr;
            cmdOnline.CommandType = CommandType.Text;
            cmdOnline.CommandText = "delete from tbPrayerExcel";
            cmdOnline.ExecuteNonQuery();

            string strD1 = "";
            string strD2 = "";
            string strD3 = "";
            string stateId = "";
            string CityId = "";
            string CountryId = "";
            CountryId = GetReturnId(dgCountry);
            stateId = GetReturnId(dgState);
            CityId = GetReturnId(dgCity);

           
            if (CityId != "")
            {
                strD3 = "insert into PrayerDelete select distinct prayerid from tbprayer where month(startdate)="+ string.Format("{0:MM}",dtpCurrentDate.Value) + " and  prayerid in (select prayerid from tbprayerdetail where cityid in(" + CityId + "))";
                strD1 = "delete from tbprayer where prayerid in (select prayerid from PrayerDelete)";
                strD2 = "delete from tbprayerdetail where prayerid in (select prayerid from PrayerDelete)";
                if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
                StaticClass.constr.Open();
                cmdOnline = new SqlCommand();
                cmdOnline.Connection = StaticClass.constr;
                cmdOnline.CommandType = CommandType.Text;
                cmdOnline.CommandText = strD3;
                cmdOnline.ExecuteNonQuery();

                if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
                StaticClass.constr.Open();
                cmdOnline = new SqlCommand();
                cmdOnline.Connection = StaticClass.constr;
                cmdOnline.CommandType = CommandType.Text;
                cmdOnline.CommandText = strD1;
                cmdOnline.ExecuteNonQuery();

                if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
                StaticClass.constr.Open();
                cmdOnline = new SqlCommand();
                cmdOnline.Connection = StaticClass.constr;
                cmdOnline.CommandType = CommandType.Text;
                cmdOnline.CommandText = strD2;
                cmdOnline.ExecuteNonQuery();
            }
            

            if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
            StaticClass.constr.Open();
            cmdOnline = new SqlCommand();
            cmdOnline.Connection = StaticClass.constr;
            cmdOnline.CommandType = CommandType.Text;
            cmdOnline.CommandText = "delete from PrayerDelete";
            cmdOnline.ExecuteNonQuery();

            
             






            panPopUp.Location = new Point(
         this.Width / 2 - panPopUp.Size.Width / 2,
         this.Height / 2 - panPopUp.Size.Height / 2);

            panSearch.Enabled = false;
            panAddNew.Enabled = false;
             

            panPopUp.Visible = true;
            panPopUp.BringToFront();
            lblName.Text = "Copy excel sheet in database";
            bgWorker.RunWorkerAsync();




            //    FillData(dgPrayer, "spGetPrayer '" + string.Format("{0:dd/MMM/yyyy}", dtpCurrentDate.Value) + "', " + Convert.ToInt32(cmbSearchDealer.SelectedValue) + ", 'c' ,0");

        }

        private void SaveCountryList(int WeekId, int IsAllWeek)
        {
            if (CountryCheckBox.Checked == true)
            {
                // SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                //  return;
            }
            for (int i = 0; i < dgCountry.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgCountry.Rows[i].Cells[1].Value) == true)
                {
                    SaveDetail(ReturnPrayerId, Convert.ToInt32(dgCountry.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0, 0, 0);
                }
            }
        }
        private void SaveStateList(int WeekId, int IsAllWeek)
        {
            if (StateCheckBox.Checked == true)
            {
                //  SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, WeekId, IsAllWeek, 0, 0);
                //  return;
            }
            for (int i = 0; i < dgState.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgState.Rows[i].Cells[1].Value) == true)
                {
                    SaveDetail(ReturnPrayerId, 0, Convert.ToInt32(dgState.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0, 0, 0);
                }
            }
        }
        private void SaveCityList(int WeekId, int IsAllWeek)
        {
            if (CityCheckBox.Checked == true)
            {
                //  SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, WeekId, IsAllWeek, 0, 0);
                // return;
            }
            for (int i = 0; i < dgCity.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgCity.Rows[i].Cells[1].Value) == true)
                {
                    SaveDetail(ReturnPrayerId, 0, 0, Convert.ToInt32(dgCity.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0, 0, 0);
                }
            }
        }
        private void SaveDealerList(int WeekId, int IsAllWeek)
        {
            if (DealerCheckBox.Checked == true)
            {
                // SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, WeekId, IsAllWeek, 0, 0);
                // return;
            }
            for (int i = 0; i < dgDealer.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDealer.Rows[i].Cells[1].Value) == true)
                {
                    SaveDetail(ReturnPrayerId, 0, 0, 0, Convert.ToInt32(dgDealer.Rows[i].Cells["Id"].Value), 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 0, 0, 0);
                }
            }
        }

        private void SaveTokenList(int WeekId, int IsAllWeek)
        {
            if (TokenCheckBox.Checked == true)
            {
                // SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, 0, 1);
                //  return;
            }
            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgToken.Rows[i].Cells[1].Value) == true)
                {
                    SaveDetail(ReturnPrayerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, WeekId, IsAllWeek, Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0, 0, 0);
                }
            }
        }


        private void SaveDetail(Int32 pId, Int32 CountryId, Int32 StateId, Int32 CityId, Int32 DealerId, Int32 ClientId, int IsAllCountry, int IsAllState, int IsAllCity, int IsAllDealer, int IsAllClient, int WeekId, int IsAllWeek, Int32 TokenId, int IsAllToken, Int32 AdminTokenId, int IsAllAdminToken)
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spSavePrayerDetail", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@PrayerId", SqlDbType.BigInt));
            cmd.Parameters["@PrayerId"].Value = pId;

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


            cmd.Parameters.Add(new SqlParameter("@AdminTokenId", SqlDbType.BigInt));
            cmd.Parameters["@AdminTokenId"].Value = AdminTokenId;

            cmd.Parameters.Add(new SqlParameter("@IsAllAdminToken", SqlDbType.Bit));
            cmd.Parameters["@IsAllAdminToken"].Value = Convert.ToByte(IsAllAdminToken);


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


        private void ClearData()
        {
            btnRefersh.Enabled = true;
            btnSave.Enabled = true;
            btnSave.Text = "Save";

             
            ReturnDealerId = 0;
            ModifyPrayerId = 0;
            chkCountry.Checked = false;
            chkState.Checked = false;
            chkCity.Checked = false;
            chkDealer.Checked = false;

            chkDealerClient.Checked = false;

            CountryCheckBox.Checked = false;
            StateCheckBox.Checked = false;
            CityCheckBox.Checked = false;
            TokenCheckBox.Checked = false;



            IsCountryCheckBoxClicked = true;
            IsTokenCheckBoxClicked = true;

            IsDealerCheckBoxClicked = true;


            InitilizeGrid(dgState, "State Name");
            InitilizeGrid(dgCity, "City Name");
        }
        private Boolean SubmitValidation()
        {
            
            if (dgExcel.Rows.Count <= 0)
            {
                MessageBox.Show("Prayer timing is found for this month", "Management Panel");
                picSearch.Focus();
                return false;
            }

            if ((chkCountry.Checked == false) && (chkState.Checked == false) && (chkCity.Checked == false) && (chkDealer.Checked == false) && (chkDealerClient.Checked == false))
            {
                MessageBox.Show("Select a playing option", "Management Panel");
                chkCountry.Focus();
                return false;
            }
            if (chkCountry.Checked == true)
            {
                if (CheckGridValidationAdvt(dgCountry) == false)
                {
                    MessageBox.Show("Select a Country from the list", "Management Panel");
                    chkCountry.Focus();
                    return false;
                }
            }
            if (chkState.Checked == true)
            {
                //if (CheckGridValidationAdvt(dgState) == false)
                //{
                //    MessageBox.Show("Please select a states from list", "Management Panel");
                //    chkState.Focus();
                //    return false;
                //}
            }
            if (chkCity.Checked == true)
            {
                //if (CheckGridValidationAdvt(dgCity) == false)
                //{
                //    MessageBox.Show("Please select a cities from list", "Management Panel");
                //    chkCity.Focus();
                //    return false;
                //}
            }
            if (chkDealer.Checked == true)
            {
                if (CheckGridValidationAdvt(dgDealer) == false)
                {
                    MessageBox.Show("Select a dealer from the list", "Management Panel");
                    chkDealer.Focus();
                    return false;
                }
            }


            if (chkDealerClient.Checked == true)
            {
                if (CheckGridValidationAdvt(dgToken) == false)
                {
                    MessageBox.Show("Please select a token from list", "Management Panel");
                    chkDealerClient.Focus();
                    return false;
                }
            }

            //string str = "";
            //if (chkDealer.Checked == true)
            //{
            //    for (int i = 0; i < dgDealer.Rows.Count; i++)
            //    {
            //        if (Convert.ToBoolean(dgDealer.Rows[i].Cells[1].Value) == true)
            //        {
            //            str = "";
            //            str = "select distinct tbPrayer.* from tbPrayer inner join tbprayerdetail  on tbprayerdetail.prayerid=tbPrayer.prayerid";
            //            str = str + " where tbPrayer.playertype='" + cmbPlayerType.Text + "' and tbprayerdetail.Dealerid = " + dgDealer.Rows[i].Cells["Id"].Value.ToString() + " and  tbprayerdetail.Dealerid !=0";
            //            str = str + " and ('" + string.Format("{0:dd/MMM/yyyy}", dtpStartDate.Value) + "' between startdate and enddate ";
            //            str = str + " or '" + string.Format("{0:dd/MMM/yyyy}", dtpEndDate.Value) + "' between startdate and enddate )";
            //            str = str + " and   tbPrayer.prayerid !=" + ModifyPrayerId;
            //            DataTable dtDetail = new DataTable();
            //            dtDetail = objMainClass.fnFillDataTable(str);
            //            if ((dtDetail.Rows.Count >= 6))
            //            {
            //                //MessageBox.Show("This selected dates maximum prayer limit has reached for selected dealer.", "Management Panel");
            //                //return false;
            //                //break;
            //            }


            //            str = "";
            //            str = "select distinct tbPrayer.* from tbPrayer inner join tbprayerdetail  on tbprayerdetail.prayerid=tbPrayer.prayerid";
            //            str = str + " where tbPrayer.playertype='" + cmbPlayerType.Text + "' and tbprayerdetail.Dealerid = " + dgDealer.Rows[i].Cells["Id"].Value.ToString() + " and  tbprayerdetail.Dealerid !=0";
            //            str = str + " and ('" + string.Format("{0:dd/MMM/yyyy}", dtpStartDate.Value) + "' between startdate and enddate ";
            //            str = str + " or '" + string.Format("{0:dd/MMM/yyyy}", dtpEndDate.Value) + "' between startdate and enddate )";
            //            str = str + " and ('" + string.Format("{0:hh:mm tt}", dtpStartTime.Value) + "' between startTime and endTime ";
            //            str = str + " or '" + string.Format("{0:hh:mm tt}", dtpEndTime.Value) + "' between startTime and endTime)";
            //            str = str + " and   tbPrayer.prayerid !=" + ModifyPrayerId;
            //            dtDetail = new DataTable();
            //            dtDetail = objMainClass.fnFillDataTable(str);
            //            if ((dtDetail.Rows.Count > 0))
            //            {
            //                MessageBox.Show("The selected time frame already exsist for this dealer", "Management Panel");
            //                return false;
            //                break;
            //            }

            //     }
            //  }

            // }
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

            dgGrid.Columns.Add("ETime1", "");
            dgGrid.Columns["ETime1"].Width = 100;
            dgGrid.Columns["ETime1"].Visible = false;
            dgGrid.Columns["ETime1"].ReadOnly = true;
            dgGrid.Columns["ETime1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["ETime1"].ValueType = typeof(string);

            dgGrid.Columns.Add("ETime2", "");
            dgGrid.Columns["ETime2"].Width = 100;
            dgGrid.Columns["ETime2"].Visible = false;
            dgGrid.Columns["ETime2"].ReadOnly = true;
            dgGrid.Columns["ETime2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["ETime2"].ValueType = typeof(string);

            dgGrid.Columns.Add("ETime3", "");
            dgGrid.Columns["ETime3"].Width = 100;
            dgGrid.Columns["ETime3"].Visible = false;
            dgGrid.Columns["ETime3"].ReadOnly = true;
            dgGrid.Columns["ETime3"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["ETime3"].ValueType = typeof(string);

            dgGrid.Columns.Add("ETime4", "");
            dgGrid.Columns["ETime4"].Width = 100;
            dgGrid.Columns["ETime4"].Visible = false;
            dgGrid.Columns["ETime4"].ReadOnly = true;
            dgGrid.Columns["ETime4"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["ETime4"].ValueType = typeof(string);

            dgGrid.Columns.Add("ETime5", "");
            dgGrid.Columns["ETime5"].Width = 100;
            dgGrid.Columns["ETime5"].Visible = false;
            dgGrid.Columns["ETime5"].ReadOnly = true;
            dgGrid.Columns["ETime5"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgGrid.Columns["ETime5"].ValueType = typeof(string);
        }
        private void FillData(DataGridView dgGrid, string Query)
        {
            int iCtr;
            DataTable dtDetail;
            dtDetail = objMainClass.fnFillDataTable(Query);
            InitilizeAdvertisement(dgGrid);
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgGrid.Rows.Add();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["sDate"].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[iCtr]["sDate"]);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time1"].Value = dtDetail.Rows[iCtr]["Time1"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time2"].Value = dtDetail.Rows[iCtr]["Time2"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time3"].Value = dtDetail.Rows[iCtr]["Time3"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time4"].Value = dtDetail.Rows[iCtr]["Time4"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Time5"].Value = dtDetail.Rows[iCtr]["Time5"].ToString();


                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["ETime1"].Value = dtDetail.Rows[iCtr]["ETime1"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["ETime2"].Value = dtDetail.Rows[iCtr]["ETime2"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["ETime3"].Value = dtDetail.Rows[iCtr]["ETime3"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["ETime4"].Value = dtDetail.Rows[iCtr]["ETime4"].ToString();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["ETime5"].Value = dtDetail.Rows[iCtr]["ETime5"].ToString();

                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["eTime"].Value = string.Format("{0:hh:mm tt}", dtDetail.Rows[iCtr]["eTime"]);
                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["pType"].Value = dtDetail.Rows[iCtr]["PlayerType"].ToString();
                }
                
            }
        }

        private void dtpCurrentDate_ValueChanged(object sender, EventArgs e)
        {
            InitilizeAdvertisement(dgExcel);
        }

        

       

       

        private void dgPrayer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 6)
            {
                ClearData();
                string Localstr = "";
                Localstr = " select * from tbPrayer ";
                Localstr = Localstr + "	where PrayerId= " + Convert.ToInt32(dgExcel.Rows[e.RowIndex].Cells["pId"].Value);
                DataTable dtDetail;
                DataTable dtCommon;
                dtDetail = objMainClass.fnFillDataTable(Localstr);
                if ((dtDetail.Rows.Count > 0))
                {  
                    panSearch.Visible = false;
                    panAddNew.Visible = true;
                    panAddNew.Dock = DockStyle.Fill;
                    btnSave.Text = "Update";
                    ModifyPrayerId = Convert.ToInt32(dtDetail.Rows[0]["PrayerId"]);

                    chkCountry.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsCountry"]);
                    chkState.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsState"]);
                    chkCity.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsCity"]);
                    chkDealer.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsDealer"]);

                    chkDealerClient.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsToken"]);
                    ReturnDealerId = Convert.ToInt32(dtDetail.Rows[0]["DealerId"]);
                    CountryCheckBox.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsAllCountry"]);
                    StateCheckBox.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsAllState"]);
                    CityCheckBox.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsAllCity"]);
                    DealerCheckBox.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsAllDealer"]);

                    TokenCheckBox.Checked = Convert.ToBoolean(dtDetail.Rows[0]["IsAllToken"]);


                    #region Get Country List
                    Localstr = "";
                    Localstr = "select distinct countryId, IsAllCountry from tbPrayerDetail where Prayerid=" + ModifyPrayerId + " and countryId != IsAllCountry";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {

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
                    FillCountryState();
                    FillCountryDealer();




                    #region Get State List
                    Localstr = "";
                    Localstr = "select distinct Stateid, IsAllState  from tbPrayerDetail where PrayerId=" + ModifyPrayerId + " and Stateid != IsAllState  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {


                        //cmbCountryName.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["StateCountryId"]);
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
                    Localstr = "select distinct CityId, IsAllCity from tbPrayerDetail where PrayerId=" + ModifyPrayerId + " and CityId != IsAllCity  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {


                        // cmbCityCountry.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["CityCountryId"]);
                        //  cmbStateName.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["CityStateId"]);
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
                    Localstr = "select distinct DealerId, IsAllDealer from tbPrayerDetail where PrayerId=" + ModifyPrayerId + " and DealerId != IsAllDealer  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {


                        //cmbDealerCountry.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["DealerCountryId"]);
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



                    FillData();

                    #region Get Token List
                    Localstr = "";
                    Localstr = "select distinct TokenId , IsAllToken from tbPrayerDetail where PrayerId=" + ModifyPrayerId + " and TokenId != IsAllToken  ";
                    dtCommon = new DataTable();
                    dtCommon = objMainClass.fnFillDataTable(Localstr);
                    if ((dtCommon.Rows.Count > 0))
                    {


                        //cmbDealer.SelectedValue = Convert.ToInt32(dtDetail.Rows[0]["TokenDealerId"]);
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
            if (e.ColumnIndex == 7)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                if (dgExcel.Rows.Count <= 0) return;
                if (Convert.ToInt32(dgExcel.Rows[e.RowIndex].Cells[0].Value) != 0)
                {
                    result = MessageBox.Show("Are you sure to delete this prayer?", "Management Panel", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = StaticClass.constr;
                        cmd.CommandText = "delete from tbPrayerDetail where Prayerid=" + Convert.ToInt32(dgExcel.Rows[e.RowIndex].Cells[0].Value);
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = StaticClass.constr;
                        cmd2.CommandText = "delete from tbPrayer where Prayerid=" + Convert.ToInt32(dgExcel.Rows[e.RowIndex].Cells[0].Value);
                        cmd2.ExecuteNonQuery();
                        StaticClass.constr.Close();
                        dgExcel.Rows.RemoveAt(e.RowIndex);
                    }
                }
                ClearData();
            }
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private string GetReturnId(DataGridView dgGrid)
        {
            string ReturnId = "";
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

        private void dgCountry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                FillCountryState();
                FillCountryDealer();
                if (chkDealerClient.Checked == true)
                {
                    IsTokenCheckBoxClicked = true;
                    TokenCheckBox.Checked = false;
                    FillData();
                }
            }
        }
        private void FillCountryState()
        {
            IsStateCheckBoxClicked = true;
            StateCheckBox.Checked = false;
            CityCheckBox.Checked = false;
            DealerCheckBox.Checked = false;

            TokenCheckBox.Checked = false;
            string CountryId = "";
            CountryId = GetReturnId(dgCountry);
            if (CountryId == "")
            {
                InitilizeGrid(dgState, "State Name");
                InitilizeGrid(dgCity, "City Name");
                InitilizeGrid(dgDealer, "Customer Name");

                InitilizeGrid();
                return;
            }
            InitilizeGrid(dgCity, "City Name");
            InitilizeGrid(dgDealer, "Customer Name");

            InitilizeGrid();
            string str = "";
            str = "";
            str = "SELECT distinct tbState.StateID as Id, tbState.StateName as DisplayName FROM AMPlayerTokens ";
            str = str + " INNER JOIN tbState ON AMPlayerTokens.StateId = tbState.Stateid ";
            str = str + " where   ";

            str = str + "   tbState.CountryId in (" + CountryId + " ) ";
            str = str + " order by StateName ";

            FillDataParam(dgState, "State Name", str);
            TotalCheckBoxesState = dgState.RowCount;
            TotalCheckedCheckBoxesState = 0;
        }

        private void FillStateCity()
        {
            IsCityCheckBoxClicked = true;
            CityCheckBox.Checked = false;
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
            FillDataParam(dgCity, "City Name", str);
            TotalCheckBoxesCity = dgCity.RowCount;
            TotalCheckedCheckBoxesCity = 0;
        }

        private void dgState_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                FillStateCity();
                if (chkDealerClient.Checked == true)
                {
                    IsTokenCheckBoxClicked = true;
                    TokenCheckBox.Checked = false;
                    FillData();
                }
            }
        }
        private void FillCountryDealer()
        {
            return;
            string CountryId = "";
            CountryId = GetReturnId(dgCountry);
            if (CountryId == "")
            {
                InitilizeGrid(dgDealer, "Customer Name");
                return;
            }

            IsDealerCheckBoxClicked = true;
            DealerCheckBox.Checked = false;
            string str = "";
            str = "select DFClientID as Id,ClientName as DisplayName  from ( select DFClientID,ClientName from DFClients where CountryCode in (" + CountryId + ") and DFClients.IsDealer=1  ";
            str = str + " ) as a order by ClientName desc ";

            FillDataParam(dgDealer, "Customer Name", str);
            TotalCheckBoxesDealer = dgDealer.RowCount;
            TotalCheckedCheckBoxesDealer = 0;
        }


        private void dgDealer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                IsTokenCheckBoxClicked = true;
                TokenCheckBox.Checked = false;
                FillData();
            }
        }












        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }



        private void dgCity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (chkDealerClient.Checked == true)
                {
                    IsTokenCheckBoxClicked = true;
                    TokenCheckBox.Checked = false;
                    FillData();
                }
            }
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {

        }
        Int64 iSize_M = 0;

        
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Int64 iSize = dgExcel.Rows.Count + 1;
            Int64 iRunningByteTotal = 0;
            foreach (DataGridViewRow row in dgExcel.Rows)
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO tbPrayerExcel(Date, Day, [Fajr AM],[Dhuhur PM], [Asr PM], [Magrib PM],[Isha PM]) VALUES(@Date, @Day, @Fajr,@Dhuhur, @Asr, @Magrib,@Isha)", StaticClass.constr))
                {

                    byte[] byteBuffer = new byte[iSize];
                    if ((row.Cells[0].Value.ToString() != "") && (row.Cells[1].Value.ToString() != "") && (row.Cells[2].Value.ToString() != ""))
                    {
                        cmd.Parameters.AddWithValue("@Date", row.Cells["sDate"].Value);
                        cmd.Parameters.AddWithValue("@Day","1");
                        cmd.Parameters.AddWithValue("@Fajr", row.Cells["Time1"].Value);
                        cmd.Parameters.AddWithValue("@Dhuhur", row.Cells["Time2"].Value);
                        cmd.Parameters.AddWithValue("@Asr", row.Cells["Time3"].Value);
                        cmd.Parameters.AddWithValue("@Magrib", row.Cells["Time4"].Value);
                        cmd.Parameters.AddWithValue("@Isha", row.Cells["Time5"].Value);
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                    }
                    iRunningByteTotal = iRunningByteTotal + 1;
                    double dIndex = (double)(iRunningByteTotal);
                    double dTotal = (double)byteBuffer.Length;
                    double dProgressPercentage = (dIndex / dTotal);
                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                    bgWorker.ReportProgress(iProgressPercentage);
                }

            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
            lblPercentage.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pBar.Value = 0;

            DataTable dtDetail = new DataTable();
            string str = "select count(*) from tbPrayerExcel ";
            dtDetail = objMainClass.fnFillDataTable(str);
            if ((dtDetail.Rows.Count > 0))
            {
                iSize_M = Convert.ToInt32(dtDetail.Rows[0][0].ToString());
            }
            panPopUp.Visible = true;
            lblName.Text = "Implementing prayer on selected records";
            bgWorkerStep2.RunWorkerAsync();
        }

        private void bgWorkerStep2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string str = "";
                 
                Int32 GenreId = 0;

                string startTime = dgExcel.Rows[0].Cells["Time1"].Value.ToString();
                string endTime = dgExcel.Rows[0].Cells["ETime1"].Value.ToString();

             //   TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
                TimeSpan span = (Convert.ToDateTime(endTime) - Convert.ToDateTime(startTime));
                int tm = span.Minutes;
                Int64 iRunningByteTotal = 0;
                DataTable dtM = new DataTable();
                str = "select * from tbPrayerExcel order by date";
                dtM = objMainClass.fnFillDataTable(str);
                if (dtM.Rows.Count > 0)
                {
                    for (int iRow = 0; (iRow <= (dtM.Rows.Count - 1)); iRow++)
                    {
                        byte[] byteBuffer = new byte[iSize_M];
                        for (int iCol = 2; (iCol <= (dtM.Columns.Count - 1)); iCol++)
                        {
                            #region Save songs in table

                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmd = new SqlCommand("spSavePrayer", StaticClass.constr);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@PrayerId", SqlDbType.BigInt));
                            cmd.Parameters["@PrayerId"].Value = ModifyPrayerId;

                            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime));
                            cmd.Parameters["@StartDate"].Value = Convert.ToDateTime(string.Format("{0:dd/MMM/yyyy}", dtM.Rows[iRow]["date"]));

                            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime));
                            cmd.Parameters["@EndDate"].Value = Convert.ToDateTime(string.Format("{0:dd/MMM/yyyy}", dtM.Rows[iRow]["date"]));

                            cmd.Parameters.Add(new SqlParameter("@PlayerType", SqlDbType.VarChar));
                            cmd.Parameters["@PlayerType"].Value = "c";

                            cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime));
                            cmd.Parameters["@StartTime"].Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", dtM.Rows[iRow][iCol]));

                            cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
                            cmd.Parameters["@EndTime"].Value = Convert.ToDateTime(string.Format("{0:hh:mm tt}", dtM.Rows[iRow][iCol])).AddMinutes(span.Minutes);

                            cmd.Parameters.Add(new SqlParameter("@IsCountry", SqlDbType.Bit));
                            cmd.Parameters["@IsCountry"].Value = Convert.ToByte(chkCountry.Checked);


                            cmd.Parameters.Add(new SqlParameter("@IsState", SqlDbType.Bit));
                            cmd.Parameters["@IsState"].Value = Convert.ToByte(chkState.Checked);

                            cmd.Parameters.Add(new SqlParameter("@IsCity", SqlDbType.Bit));
                            cmd.Parameters["@IsCity"].Value = Convert.ToByte(chkCity.Checked);

                            cmd.Parameters.Add(new SqlParameter("@IsDealer", SqlDbType.Bit));
                            cmd.Parameters["@IsDealer"].Value = Convert.ToByte(chkDealer.Checked);

                            cmd.Parameters.Add(new SqlParameter("@IsClient", SqlDbType.Bit));
                            cmd.Parameters["@IsClient"].Value = false;

                            cmd.Parameters.Add(new SqlParameter("@StateCountryId", SqlDbType.BigInt));
                            cmd.Parameters["@StateCountryId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@CityStateId", SqlDbType.BigInt));
                            cmd.Parameters["@CityStateId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@CityCountryId", SqlDbType.BigInt));
                            cmd.Parameters["@CityCountryId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@DealerCountryId", SqlDbType.BigInt));
                            cmd.Parameters["@DealerCountryId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@ClientCountryId", SqlDbType.BigInt));
                            cmd.Parameters["@ClientCountryId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@IsToken", SqlDbType.Bit));
                            cmd.Parameters["@IsToken"].Value = Convert.ToByte(chkDealerClient.Checked);

                            cmd.Parameters.Add(new SqlParameter("@TokenDealerId", SqlDbType.BigInt));
                            cmd.Parameters["@TokenDealerId"].Value = "0";

                            cmd.Parameters.Add(new SqlParameter("@DealerId", SqlDbType.BigInt));
                            cmd.Parameters["@DealerId"].Value = ReturnDealerId;

                            cmd.Parameters.Add(new SqlParameter("@IsAllCountry", SqlDbType.Bit));
                            cmd.Parameters["@IsAllCountry"].Value = Convert.ToByte(CountryCheckBox.Checked);
                            cmd.Parameters.Add(new SqlParameter("@IsAllState", SqlDbType.Bit));
                            cmd.Parameters["@IsAllState"].Value = Convert.ToByte(StateCheckBox.Checked);
                            cmd.Parameters.Add(new SqlParameter("@IsAllCity", SqlDbType.Bit));
                            cmd.Parameters["@IsAllCity"].Value = Convert.ToByte(CityCheckBox.Checked);
                            cmd.Parameters.Add(new SqlParameter("@IsAllDealer", SqlDbType.Bit));
                            cmd.Parameters["@IsAllDealer"].Value = Convert.ToByte(DealerCheckBox.Checked);
                            cmd.Parameters.Add(new SqlParameter("@IsAllClient", SqlDbType.Bit));
                            cmd.Parameters["@IsAllClient"].Value = false;
                            cmd.Parameters.Add(new SqlParameter("@IsAllToken", SqlDbType.Bit));
                            cmd.Parameters["@IsAllToken"].Value = Convert.ToByte(TokenCheckBox.Checked);

                            cmd.Parameters.Add(new SqlParameter("@IsAllAdminToken", SqlDbType.Bit));
                            cmd.Parameters["@IsAllAdminToken"].Value = false;

                            cmd.Parameters.Add(new SqlParameter("@IsAdminToken", SqlDbType.Bit));
                            cmd.Parameters["@IsAdminToken"].Value = false;


                            ReturnPrayerId = Convert.ToInt32(cmd.ExecuteScalar());

                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.Connection = StaticClass.constr;
                            cmd1.CommandText = "delete from tbPrayerDetail where PrayerId=" + ReturnPrayerId;
                            cmd1.ExecuteNonQuery();
                            StaticClass.constr.Close();


                            if (chkCountry.Checked == true)
                            {
                                SaveCountryList(0, 1);
                            }
                            if (chkState.Checked == true)
                            {
                                SaveStateList(0, 1);
                            }
                            if (chkCity.Checked == true)
                            {
                                SaveCityList(0, 1);
                            }
                            if (chkDealer.Checked == true)
                            {
                                SaveDealerList(0, 1);
                            }

                            if (chkDealerClient.Checked == true)
                            {
                                SaveTokenList(0, 1);
                            }
                            #endregion
                        }
                        iRunningByteTotal = iRunningByteTotal + 1;
                        double dIndex = (double)(iRunningByteTotal);
                        double dTotal = (double)byteBuffer.Length;
                        double dProgressPercentage = (dIndex / dTotal);
                        int iProgressPercentage = (int)(dProgressPercentage * 100);
                        bgWorkerStep2.ReportProgress(iProgressPercentage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgWorkerStep2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
            lblPercentage.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bgWorkerStep2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Records inserted.");
            panSearch.Enabled = true;
            panAddNew.Enabled = true;
             

            dgExcel.DataSource = null;
             
            panPopUp.Visible = false;
            pBar.Value = 0;
            ClearData();
        }

        
        

        private void panState_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbCountryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0) return;
            string strState = "";
            strState = "select * from tbState where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue);
            objMainClass.fnFillComboBox(strState, cmbStateName, "stateid", "StateName", "");
            InitilizeAdvertisement(dgExcel);
        }

        private void cmbStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0) return;
            string strCity = "";
            strCity = "select * from tbCity where countryId= " + Convert.ToInt32(cmbCountryName.SelectedValue) + " and stateid =" + Convert.ToInt32(cmbStateName.SelectedValue);
            objMainClass.fnFillComboBox(strCity, cmbCityName, "Cityid", "CityName", "");

            InitilizeAdvertisement(dgExcel);
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCountryName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a Country from the list", "Management Panel");
                cmbCountryName.Focus();
                return  ;
            }
            if (Convert.ToInt32(cmbStateName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a state", "Management Panel");
                cmbStateName.Focus();
                return  ;
            }
            if (Convert.ToInt32(cmbCityName.SelectedValue) == 0)
            {
                MessageBox.Show("Select a city", "Management Panel");
                cmbCityName.Focus();
                return  ;
            }

            FillData(dgExcel, "spPrayerData_AdminPanel " + dtpCurrentDate.Value.Month  + ", " + Convert.ToInt32(cmbCountryName.SelectedValue) + ", " + Convert.ToInt32(cmbStateName.SelectedValue) + " ,"+ Convert.ToInt32(cmbCityName.SelectedValue) + "");
        }

        private void cmbCityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitilizeAdvertisement(dgExcel);
        }

        private void picExport_Click(object sender, EventArgs e)
        {
            if (dgExcel.Rows.Count > 0)
            {
                FolderBrowserDialog fBd = new FolderBrowserDialog();
                DialogResult result = fBd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DataTable dt = new DataTable();
                    //Adding the Columns
                    foreach (DataGridViewColumn column in dgExcel.Columns)
                    {
                        dt.Columns.Add(column.HeaderText, column.ValueType);
                    }
                    //Adding the Rows
                    foreach (DataGridViewRow row in dgExcel.Rows)
                    {
                        dt.Rows.Add();
                        foreach (DataGridViewCell cell in row.Cells)
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();

                        }
                    }
                    //Exporting to Excel
                    string folderPath = fBd.SelectedPath + "\\";
                    string FileName = cmbCityName.Text+  "-" + string.Format("{0:MMM}", dtpCurrentDate.Value) + ".xlsx";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Detail");
                        wb.SaveAs(folderPath + FileName);
                        MessageBox.Show("Records are export to excel on selected path");
                    }
                }
            }
        }
    }
}
