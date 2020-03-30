using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmSeparation : Form
    {
        gblClass objMainClass = new gblClass();
        public frmSeparation()
        {
            InitializeComponent();
        }

        private void frmSeparation_Load(object sender, EventArgs e)
        {
            string strState = "";
            strState = "select max(Formatid) as Formatid, formatname from tbSpecialFormat group by formatname";
            objMainClass.fnFillComboBox(strState, cmbFormat, "FormatId", "FormatName", "");

            InitilizeGrid();
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = "select  tbSpecialPlaylists.splPlaylistid,  tbSpecialPlaylists.splPlaylistName  from tbSpecialPlaylists ";
            str = str + " where tbSpecialPlaylists.formatid=" + Convert.ToInt32(cmbFormat.SelectedValue) + " ";
            objMainClass.fnFillComboBox(str, cmbPlaylist, "splPlaylistId", "splPlaylistName", "");
        }

        private void InitilizeGrid()
        {
            if (dgSp.Rows.Count > 0)
            {
                dgSp.Rows.Clear();
            }
            if (dgSp.Columns.Count > 0)
            {
                dgSp.Columns.Clear();
            }

            dgSp.Columns.Add("splId", "splId");
            dgSp.Columns["splId"].Width = 0;
            dgSp.Columns["splId"].Visible = false;
            dgSp.Columns["splId"].ReadOnly = true;

            dgSp.Columns.Add("pName", "Playlist Name");
            dgSp.Columns["pName"].Width = 300;
            dgSp.Columns["pName"].Visible = true;
            dgSp.Columns["pName"].ReadOnly = true;
            dgSp.Columns["pName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSp.Columns.Add("sType", "Separation Type");
            dgSp.Columns["sType"].Width = 300;
            dgSp.Columns["sType"].Visible = true;
            dgSp.Columns["sType"].ReadOnly = true;
            dgSp.Columns["sType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgSp.Columns.Add("sTime", "Separation Time");
            dgSp.Columns["sTime"].Width = 300;
            dgSp.Columns["sTime"].Visible = true;
            dgSp.Columns["sTime"].ReadOnly = true;
            dgSp.Columns["sTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditUser = new DataGridViewLinkColumn();
            EditUser.HeaderText = "Edit";
            EditUser.Text = "Edit";
            EditUser.DataPropertyName = "Edit";
            dgSp.Columns.Add(EditUser);
            EditUser.UseColumnTextForLinkValue = true;
            EditUser.Width = 50;
            EditUser.Visible = true;
            dgSp.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn Delete = new DataGridViewLinkColumn();
            Delete.HeaderText = "Delete";
            Delete.Text = "Delete";
            Delete.DataPropertyName = "Delete";
            dgSp.Columns.Add(Delete);
            Delete.UseColumnTextForLinkValue = true;
            Delete.Width = 90;
            Delete.Visible = true;
            dgSp.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            dgSp.Columns.Add("fId", "fId");
            dgSp.Columns["fId"].Width = 0;
            dgSp.Columns["fId"].Visible = false;
            dgSp.Columns["fId"].ReadOnly = true;

            dgSp.Columns.Add("time1", "time1");
            dgSp.Columns["time1"].Width = 0;
            dgSp.Columns["time1"].Visible = false;
            dgSp.Columns["time1"].ReadOnly = true;

            dgSp.Columns.Add("timeType", "timeType");
            dgSp.Columns["timeType"].Width = 0;
            dgSp.Columns["timeType"].Visible = false;
            dgSp.Columns["timeType"].ReadOnly = true;

        }

        private void FillData(Int32 splPlaylistId)
        {
            string str;
            int iCtr;
            DataTable dtDetail = new DataTable();
            str = "Select * from  tbSeparation ";
            str = str + " inner join tbSpecialPlaylists on tbSeparation.splPlaylistId = tbSpecialPlaylists.splPlaylistId ";
            str = str + " where tbSeparation.splPlaylistId= " + splPlaylistId + " ";
            dtDetail = objMainClass.fnFillDataTable(str);

            InitilizeGrid();

            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgSp.Rows.Add();
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["splId"].Value = dtDetail.Rows[iCtr]["splPlaylistid"];
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["pName"].Value = dtDetail.Rows[iCtr]["splPlaylistName"];
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["sType"].Value = dtDetail.Rows[iCtr]["sType"];
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["sTime"].Value = dtDetail.Rows[iCtr]["sTime"].ToString() + ' ' + dtDetail.Rows[iCtr]["sBlockType"].ToString();

                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["time1"].Value = dtDetail.Rows[iCtr]["sTime"];
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["timeType"].Value = dtDetail.Rows[iCtr]["sBlockType"];
                    dgSp.Rows[dgSp.Rows.Count - 1].Cells["fId"].Value = dtDetail.Rows[iCtr]["formatid"];

                }

             
            }
        }

        private void cmbPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPlaylist.SelectedValue) != 0)
            {
                FillData(Convert.ToInt32(cmbPlaylist.SelectedValue));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SubmitValidation() == false)
            {
                return;
            }
            SaveData();
            FillData(Convert.ToInt32(cmbPlaylist.SelectedValue));
            Refersh();
        }
        private void Refersh()
        {
            rdoAlbum.Checked = false;
            rdoArtist.Checked = false;
            rdoGenre.Checked = false;
            rdoTitle.Checked = false;
            rdoYear.Checked = false;
            cmbSpTime.Text = "";
            cmbSpType.Text = "";
        }

        private void SaveData()
        {
            string sType = "";
            if (rdoAlbum.Checked == true)
            {
                sType = rdoAlbum.Text;
            }
            if (rdoArtist.Checked == true)
            {
                sType = rdoArtist.Text;
            }
            if (rdoGenre.Checked == true)
            {
                sType = rdoGenre.Text;
            }
            if (rdoTitle.Checked == true)
            {
                sType = rdoTitle.Text;
            }
            if (rdoYear.Checked == true)
            {
                sType = rdoYear.Text;
            }
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spSaveSeparation", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@sType", SqlDbType.VarChar));
            cmd.Parameters["@sType"].Value = sType;

            cmd.Parameters.Add(new SqlParameter("@sTime", SqlDbType.VarChar));
            cmd.Parameters["@sTime"].Value = cmbSpTime.Text;

            cmd.Parameters.Add(new SqlParameter("@sBlockType", SqlDbType.VarChar));
            cmd.Parameters["@sBlockType"].Value = cmbSpType.Text;

            cmd.Parameters.Add(new SqlParameter("@splPlaylistId", SqlDbType.BigInt));
            cmd.Parameters["@splPlaylistId"].Value = Convert.ToInt16(cmbPlaylist.SelectedValue);

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
        private Boolean SubmitValidation()
        {
            if (Convert.ToInt32(cmbFormat.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a format name", "Management Panel");
                return false;
            }
            if (Convert.ToInt32(cmbPlaylist.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a playlist name", "Management Panel");
                return false;
            }
            if ((rdoAlbum.Checked == false) && (rdoArtist.Checked == false) && (rdoTitle.Checked == false) && (rdoYear.Checked == false) && (rdoGenre.Checked == false))
            {
                MessageBox.Show("Please select a separation type", "Management Panel");
                return false;
            }
            if (cmbSpTime.Text == "")
            {
                MessageBox.Show("Please select a separation time", "Management Panel");
                return false;
            }
            if (cmbSpType.Text == "")
            {
                MessageBox.Show("Please select a separation time", "Management Panel");
                return false;
            }
            return true;
        }

        private void dgSp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 4)
            {
                cmbFormat.SelectedValue = dgSp.Rows[e.RowIndex].Cells["fId"].Value;
                if (dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() == "Album")
                {
                    rdoAlbum.Checked = true;
                }
                if (dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() == "Artist")
                {
                    rdoArtist.Checked = true;
                }
                if (dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() == "Genre")
                {
                    rdoGenre.Checked = true;
                }
                if (dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() == "Title")
                {
                    rdoTitle.Checked = true;
                }
                if (dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() == "Year")
                {
                    rdoYear.Checked = true;
                }
                cmbPlaylist.SelectedValue = dgSp.Rows[e.RowIndex].Cells["splid"].Value;
                cmbSpTime.Text = dgSp.Rows[e.RowIndex].Cells["time1"].Value.ToString();
                cmbSpType.Text = dgSp.Rows[e.RowIndex].Cells["timeType"].Value.ToString();
            }
            if (e.ColumnIndex == 5)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from tbSeparation where splPlaylistId=" + Convert.ToInt32(dgSp.Rows[e.RowIndex].Cells["splid"].Value) + " and stype = '" + dgSp.Rows[e.RowIndex].Cells["sType"].Value.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    FillData(Convert.ToInt32(cmbPlaylist.SelectedValue));
                }
            }
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            cmbFormat.SelectedValue = 0;
            InitilizeGrid();
            Refersh();
        }
    }
}