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
    public partial class frmStreamShedule : Form
    {
        gblClass objMainClass = new gblClass();
        DateTimeFormatInfo fi = new DateTimeFormatInfo();
        public frmStreamShedule()
        {
            InitializeComponent();
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
        }
        private void cmbFormat_Click(object sender, EventArgs e)
        {
            string strState = "";
            strState = "select max(Formatid) as Formatid, formatname from tbSpecialFormat group by formatname";
            objMainClass.fnFillComboBox(strState, cmbFormat, "FormatId", "FormatName", "");
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSplPlaylists(cmbSplPlaylist, Convert.ToInt32(cmbFormat.SelectedValue));
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtPrv = new DataTable();
            if (SubmitValidationGet() == false) return;
            string str = "";
            str = "select *  from tbStreamPlaylistSchedule where streamPlaylistid=" + Convert.ToInt32(cmbStreamName.SelectedValue) + " and  '" + string.Format(fi, "{0:hh:mm tt}", dtpStartTime.Value.AddMinutes(1)) + "' between starttime and endtime";
            dtPrv = objMainClass.fnFillDataTable(str);
            if (dtPrv.Rows.Count > 0)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmdTit = new SqlCommand();
                cmdTit.Connection = StaticClass.constr;
                str = "";
                str = "delete from tbStreamPlaylistSchedule where streamPlaylistid=" + Convert.ToInt32(cmbStreamName.SelectedValue) + " and   '" + string.Format(fi,"{0:hh:mm tt}", dtpStartTime.Value.AddMinutes(1)) + "' between starttime and endtime";
                cmdTit.CommandText = str;
                cmdTit.ExecuteNonQuery();
                StaticClass.constr.Close();
            }
            str = "";
            str = "select *  from tbStreamPlaylistSchedule where streamPlaylistid=" + Convert.ToInt32(cmbStreamName.SelectedValue) + " and   '" + string.Format(fi,"{0:hh:mm tt}", dtpEndTime.Value.AddMinutes(-1)) + "' between starttime and endtime";
            dtPrv = objMainClass.fnFillDataTable(str);
            if (dtPrv.Rows.Count > 0)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                str = "";
                str = "delete from tbStreamPlaylistSchedule where streamPlaylistid=" + Convert.ToInt32(cmbStreamName.SelectedValue) + " and   '" + string.Format(fi,"{0:hh:mm tt}", dtpEndTime.Value.AddMinutes(-1)) + "' between starttime and endtime";
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
            }
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmdS = new SqlCommand();
            cmdS.Connection = StaticClass.constr;
            str = "";
            str = "insert into tbStreamPlaylistSchedule(streamPlaylistid,splPlaylistid,StartTime,EndTime,FormatId) values(" + Convert.ToInt32(cmbStreamName.SelectedValue) + "," + Convert.ToInt32(cmbSplPlaylist.SelectedValue) + ", '" + string.Format(fi, "{0:hh:mm tt}", dtpStartTime.Value) + "','" + string.Format(fi, "{0:hh:mm tt}", dtpEndTime.Value) + "'," + Convert.ToInt32(cmbFormat.SelectedValue) + ")";
            cmdS.CommandText = str;
            cmdS.ExecuteNonQuery();
            StaticClass.constr.Close();
            FillSaveData();
        }
        private Boolean SubmitValidationGet()
        {
            if (Convert.ToInt32(cmbStreamName.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a stream playlist name", "Management Panel");
                cmbFormat.Focus();
                return false;
            }
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
            return true;
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            cmbStreamName.SelectedValue = 0;
            cmbFormat.SelectedValue = 0;
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
        }

        private void frmStreamShedule_Load(object sender, EventArgs e)
        {
            SetButtonColor(btnMenuSearch);
            panSearch.Visible = true;
            panAddNew.Visible = false;
            panSearch.Dock = DockStyle.Fill;

            fi.AMDesignator = "AM";
            fi.PMDesignator = "PM";

            string strState = "";
            strState = "select max(Formatid) as Formatid, formatname from tbSpecialFormat group by formatname";
            objMainClass.fnFillComboBox(strState, cmbFormat, "FormatId", "FormatName", "");
            FillSaveStreamPlaylist();
            FillStreamPlaylist();
            FillSaveData();
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
            dgSpl.Columns.Add("pschId", "Id");
            dgSpl.Columns["pschId"].Width = 0;
            dgSpl.Columns["pschId"].Visible = false;
            dgSpl.Columns["pschId"].ReadOnly = true;
            //1

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
            dgSpl.Columns["sTime"].Width = 150;
            dgSpl.Columns["sTime"].Visible = true;
            dgSpl.Columns["sTime"].ReadOnly = true;
            dgSpl.Columns["sTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("eTime", "End Time");
            dgSpl.Columns["eTime"].Width = 150;
            dgSpl.Columns["eTime"].Visible = true;
            dgSpl.Columns["eTime"].ReadOnly = true;
            dgSpl.Columns["eTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgSpl.Columns.Add("splId", "splId");
            dgSpl.Columns["splId"].Width = 0;
            dgSpl.Columns["splId"].Visible = false;
            dgSpl.Columns["splId"].ReadOnly = true;
            dgSpl.Columns["splId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSpl.Columns.Add("fId", "fId");
            dgSpl.Columns["fId"].Width = 0;
            dgSpl.Columns["fId"].Visible = false;
            dgSpl.Columns["fId"].ReadOnly = true;
            dgSpl.Columns["fId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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



        }
        private void FillSaveData()
        {
            string sQr = "";
            sQr = "GetStreamPlaylistSchedule " + Convert.ToInt32(cmbStreamName.SelectedValue);
            DataTable dtDetail = new DataTable();
            InitilizeSplGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    sQr = "";
                    dgSpl.Rows.Add();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pSchid"].Value = dtDetail.Rows[i]["pschid"];
                    //                    sQr = dtDetail.Rows[i]["splPlaylistName"].ToString() + " (" + GetSongCounter(Convert.ToInt32(dtDetail.Rows[i]["splPlaylistid"])) + ")";
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fName"].Value = dtDetail.Rows[i]["FormatName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[i]["splPlaylistName"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["sTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["StartTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["eTime"].Value = string.Format(fi, "{0:hh:mm tt}", Convert.ToDateTime(dtDetail.Rows[i]["EndTime"]));
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["fId"].Value = dtDetail.Rows[i]["formatid"].ToString();
                    dgSpl.Rows[dgSpl.Rows.Count - 1].Cells["splId"].Value = dtDetail.Rows[i]["splPlaylistId"].ToString();
                }

            }
        }

        private void dgSpl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 7)
            {
                cmbFormat.SelectedValue = Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["fId"].Value);
                cmbSplPlaylist.SelectedValue= Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["splId"].Value);
                dtpStartTime.Value= Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["sTime"].Value);
                dtpEndTime.Value = Convert.ToDateTime(dgSpl.Rows[e.RowIndex].Cells["eTime"].Value);
            }
            if (e.ColumnIndex == 8)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                   string str = "";
                    str = "delete from tbStreamPlaylistSchedule where pschid= " + Convert.ToInt32(dgSpl.Rows[e.RowIndex].Cells["pschid"].Value);
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    FillSaveData();
                }
            }

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
            FillSaveStreamPlaylist();
        }
        private void SetButtonColor(Button btnName)
        {
            Color light = Color.FromName("ControlLightLight");
            Color bLight = Color.FromName("Control");
            btnMenuSearch.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnMenuAddNew.BackColor = Color.FromArgb(bLight.A, bLight.R, bLight.G, bLight.B);
            btnName.BackColor = Color.White;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Focus();
            if (Convert.ToInt32(cmbStreamName.SelectedValue) == 0)
            {
                txtName.Text = "";
                btnSaveNew.Text = "Save";
            }
            else
            {
                txtName.Text = cmbStreamName.Text;
                btnSaveNew.Text = "Update";
            }
            panMainNew.Width = this.Width;
            panMainNew.Height = this.Height;
            panMainNew.BringToFront();
            panMainNew.Location = new Point(0, 0);
            panMainNew.Visible = true;
            txtName.Focus();
            panNew.Location = new Point(
          this.panMainNew.Width / 2 - panNew.Size.Width / 2,
          this.panMainNew.Height / 2 - panNew.Size.Height / 2);
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                int returnValue = 0;

                SqlCommand cmd = new SqlCommand("sp_SaveStreamPlaylist", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@streamPlaylistid", SqlDbType.BigInt));
                if (btnSaveNew.Text == "Update")
                {
                    cmd.Parameters["@streamPlaylistid"].Value = Convert.ToInt32(cmbStreamName.SelectedValue);
                }
                else
                {
                    cmd.Parameters["@streamPlaylistid"].Value = "0";
                }


                cmd.Parameters.Add(new SqlParameter("@streamname", SqlDbType.VarChar));
                cmd.Parameters["@streamname"].Value = txtName.Text;

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                if (returnValue != 999999999)
                {
                    FillStreamPlaylist();
                    cmbStreamName.SelectedValue = Convert.ToInt32(returnValue);
                    panMainNew.Visible = false;

                }
                if (returnValue == 999999999)
                {
                    MessageBox.Show("This stream palylist name already exists", "Management Panel");

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

        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            panMainNew.Visible = false;
            txtName.Text = "";
        }
        private void FillStreamPlaylist()
        {
            string strState = "";
            strState = "select streamPlaylistid, streamName from tbStreamPlaylist order by streamName";
            objMainClass.fnFillComboBox(strState, cmbStreamName, "streamPlaylistid", "streamName", "");
        }

        private void cmbStreamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSaveData();
        }
        private void InitilizeSearchGrid()
        {
            if (dgSearch.Rows.Count > 0)
            {
                dgSearch.Rows.Clear();
            }
            if (dgSearch.Columns.Count > 0)
            {
                dgSearch.Columns.Clear();
            }
            dgSearch.Dock = DockStyle.Fill;
            //0
            dgSearch.Columns.Add("sid", "Id");
            dgSearch.Columns["sid"].Width = 0;
            dgSearch.Columns["sid"].Visible = false;
            dgSearch.Columns["sid"].ReadOnly = true;
            //1

            dgSearch.Columns.Add("sName", "Stream Playlist Name");
            dgSearch.Columns["sName"].Width = 200;
            dgSearch.Columns["sName"].Visible = true;
            dgSearch.Columns["sName"].ReadOnly = true;
            dgSearch.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

           
            DataGridViewLinkColumn EditAdvt = new DataGridViewLinkColumn();
            EditAdvt.HeaderText = "Edit";
            EditAdvt.Text = "Edit";
            EditAdvt.DataPropertyName = "Edit";
            dgSearch.Columns.Add(EditAdvt);
            EditAdvt.UseColumnTextForLinkValue = true;
            EditAdvt.Width = 70;
            EditAdvt.Visible = true;
            dgSearch.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteAdvt = new DataGridViewLinkColumn();
            DeleteAdvt.HeaderText = "Delete";
            DeleteAdvt.Text = "Delete";
            DeleteAdvt.DataPropertyName = "Delete";
            dgSearch.Columns.Add(DeleteAdvt);
            DeleteAdvt.UseColumnTextForLinkValue = true;
            DeleteAdvt.Width = 70;
            DeleteAdvt.Visible = true;
            dgSearch.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;



        }
        private void FillSaveStreamPlaylist()
        {
            string sQr = "";
            sQr = "select streamPlaylistid, streamName from tbStreamPlaylist order by streamName";
            DataTable dtDetail = new DataTable();
            InitilizeSearchGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);
            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    sQr = "";
                    dgSearch.Rows.Add();
                    dgSearch.Rows[dgSearch.Rows.Count - 1].Cells["sid"].Value = dtDetail.Rows[i]["streamPlaylistid"];
                    dgSearch.Rows[dgSearch.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["streamName"].ToString();
                }

            }
        }

        private void dgSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                cmbStreamName.SelectedValue = Convert.ToInt32(dgSearch.Rows[e.RowIndex].Cells["sid"].Value);
                SetButtonColor(btnMenuAddNew);
                panSearch.Visible = false;
                panAddNew.Visible = true;
                panAddNew.Dock = DockStyle.Fill;
            }
            if (e.ColumnIndex == 3)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string str = "";
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    str = "";
                    str = "delete from tbStreamPlaylistSchedule where streamPlaylistid= " + Convert.ToInt32(dgSearch.Rows[e.RowIndex].Cells["sid"].Value);
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();

                    cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    str = "";
                    str = "delete from tbStreamPlaylist where streamPlaylistid= " + Convert.ToInt32(dgSearch.Rows[e.RowIndex].Cells["sid"].Value);
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    FillStreamPlaylist();
                    FillSaveStreamPlaylist();
                }
            }
        }
    }
}
