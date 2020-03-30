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
    public partial class frmBestofPlaylist : Form
    {
        gblClass ObjMainClass = new gblClass();
        string SearchText = "";
        Int32 ModifyPlaylistId = 0;
        string pAction = "New";
        public frmBestofPlaylist()
        {
            InitializeComponent();
            FillLocalPlaylist();
            string str = "SELECT TOP (400) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4 order by TitleID desc";
            FillGrid(str);
        }
        private void InitilizeLocalGrid()
        {
            if (dgLocalPlaylist.Rows.Count > 0)
            {
                dgLocalPlaylist.Rows.Clear();
            }
            if (dgLocalPlaylist.Columns.Count > 0)
            {
                dgLocalPlaylist.Columns.Clear();
            }

            dgLocalPlaylist.Columns.Add("playlistId", "playlist Id");
            dgLocalPlaylist.Columns["playlistId"].Width = 0;
            dgLocalPlaylist.Columns["playlistId"].Visible = false;
            dgLocalPlaylist.Columns["playlistId"].ReadOnly = true;

            dgLocalPlaylist.Columns.Add("playlistname", "Playlist Name");
            dgLocalPlaylist.Columns["playlistname"].Width = 250;
            dgLocalPlaylist.Columns["playlistname"].Visible = true;
            dgLocalPlaylist.Columns["playlistname"].ReadOnly = true;
            dgLocalPlaylist.Columns["playlistname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            dgLocalPlaylist.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            EditPlaylist.Visible = true;
            dgLocalPlaylist.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }

        private void FillLocalPlaylist()
        {

            string str = "";
            string strGetCount = "";
            int iCtr;
            DataTable dtDetail;
            DataTable dtGetCount;
            str = "select PlaylistID,ltrim(rtrim(name)) as pName from Playlists  where ispredefined=1 order by name";
            dtDetail = ObjMainClass.fnFillDataTable(str);

            InitilizeLocalGrid();
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgLocalPlaylist.Rows.Add();
                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[iCtr]["PlaylistID"];

                    strGetCount = "select Count(*) as Total from  TitlesInPlaylists where PlaylistID =" + dtDetail.Rows[iCtr]["PlaylistID"] + " ";
                    dtGetCount = ObjMainClass.fnFillDataTable(strGetCount);

                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[iCtr]["pName"] + "  (" + dtGetCount.Rows[0]["Total"] + ")";
                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[2].Value = "";

                   // dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[2].Style.Font = new Font("Segoe UI", 10 );

                }
                foreach (DataGridViewRow row in dgLocalPlaylist.Rows)
                {
                    row.Height = 35;
                }
                dgLocalPlaylist.CurrentCell = dgLocalPlaylist.Rows[0].Cells[1];
                dgLocalPlaylist.Rows[0].Selected = true;
                PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[0].Cells[0].Value));
            }


        }

        private void InitilizeGrid(DataGridView dgGrid)
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.Rows.Clear();
            }
            if (dgGrid.Columns.Count > 0)
            {
                dgGrid.Columns.Clear();
            }

            dgGrid.Columns.Add("songid", "song Id");
            dgGrid.Columns["songid"].Width = 0;
            dgGrid.Columns["songid"].Visible = false;
            dgGrid.Columns["songid"].ReadOnly = true;
            

            dgGrid.Columns.Add("songname", "Title");
            dgGrid.Columns["songname"].Width = 420;
            dgGrid.Columns["songname"].Visible = true;
            dgGrid.Columns["songname"].ReadOnly = true;
            dgGrid.Columns["songname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            
            dgGrid.Columns.Add("Length", "Length");
            dgGrid.Columns["Length"].Width = 100;
            dgGrid.Columns["Length"].Visible = true;
            dgGrid.Columns["Length"].ReadOnly = true;
            dgGrid.Columns["Length"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Artist", "Artist");
            dgGrid.Columns["Artist"].Width = 160;
            dgGrid.Columns["Artist"].Visible = true;
            dgGrid.Columns["Artist"].ReadOnly = true;
            dgGrid.Columns["Artist"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Album", "Album");
            dgGrid.Columns["Album"].Width = 250;
            dgGrid.Columns["Album"].Visible = true;
            dgGrid.Columns["Album"].ReadOnly = true;
            dgGrid.Columns["Album"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (dgGrid.Name == "dgPlaylist")
            {
                DataGridViewLinkColumn DeleteSong = new DataGridViewLinkColumn();
                DeleteSong.HeaderText = "Delete";
                DeleteSong.Text = "Delete";
                DeleteSong.DataPropertyName = "Delete";
                DeleteSong.LinkColor = Color.FromArgb(64, 64, 64);
                DeleteSong.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
                DeleteSong.LinkBehavior = LinkBehavior.SystemDefault;
                dgGrid.Columns.Add(DeleteSong);
                DeleteSong.UseColumnTextForLinkValue = true;
                DeleteSong.Width = 70;
                dgGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            dgGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular);
            dgGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgGrid.EnableHeadersVisualStyles = false;
            dgGrid.ColumnHeadersHeight = 30;
            dgGrid.ColumnHeadersHeightSizeMode =DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
           
        }

        private void PopulateInputFileTypeDetail(DataGridView dgGrid, Int32 currentPlayRow)
        {
            string mlsSql = "";
            string TitleTime = "";
            var Special_Name = "";
            string Special_Change = "";
            Int32 iCtr = 0;
            DataTable dtDetail = new DataTable();
            mlsSql = "SELECT  Titles.TitleID, rtrim(ltrim(Titles.Title)) as Title, Titles.Time,rtrim(ltrim(Albums.Name)) AS AlbumName ,";
            mlsSql = mlsSql + " Titles.TitleYear ,  rtrim(ltrim(Artists.Name)) as ArtistName  FROM ((( TitlesInPlaylists  ";
            mlsSql = mlsSql + " INNER JOIN Titles ON TitlesInPlaylists.TitleID = Titles.TitleID )  ";
            mlsSql = mlsSql + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID ) ";
            mlsSql = mlsSql + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID ) ";
            mlsSql = mlsSql + " where TitlesInPlaylists.PlaylistID= " + Convert.ToInt32(currentPlayRow);


            dtDetail = ObjMainClass.fnFillDataTable(mlsSql);
            InitilizeGrid(dgGrid);
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgGrid.Rows.Add();
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["songid"].Value = dtDetail.Rows[iCtr]["TitleID"];

                    Special_Name = "";
                    Special_Change = "";
                    Special_Name = dtDetail.Rows[iCtr]["Title"].ToString();
                    Special_Change = Special_Name.Replace("??$$$??", "'");
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["songname"].Value = Special_Change;

                    string str = dtDetail.Rows[iCtr]["Time"].ToString();
                    string[] arr = str.Split(':');
                    TitleTime = arr[1] + ":" + arr[2];

                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Length"].Value = TitleTime;

                    Special_Name = "";
                    Special_Change = "";

                    Special_Name = dtDetail.Rows[iCtr]["AlbumName"].ToString();
                    Special_Change = Special_Name.Replace("??$$$??", "'");
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Album"].Value = Special_Change;

                    Special_Name = "";
                    Special_Change = "";

                    Special_Name = dtDetail.Rows[iCtr]["ArtistName"].ToString();
                    Special_Change = Special_Name.Replace("??$$$??", "'");
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Artist"].Value = Special_Change;

                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Length"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                    //dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                }
            }
            foreach (DataGridViewRow row in dgGrid.Rows)
            {
                row.Height = 30;
            }
        }

        private void dgLocalPlaylist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 1)
            {
                if (e.RowIndex >= 0)
                {
                    PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value));
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (e.RowIndex >= 0)
                {
                    string str45 = dgLocalPlaylist.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string[] arr = str45.Split('(');
                    txtPlaylistName.Text = arr[0].Trim();
                    ModifyPlaylistId = Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value);
                    pAction = "Modify";
                    txtPlaylistName.Focus();
                }
            }
        }

        private void txtPlaylistName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }
        private void FillGrid(string str)
        {
            int iCtr;

            string TitleTime = "";
            try
            {
                InitilizeGrid(dgCommanGrid);
                DataTable dtDetail;
                dtDetail = ObjMainClass.fnFillDataTable(str);
                if ((dtDetail.Rows.Count > 0))
                {

                    for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                    {

                        dgCommanGrid.Rows.Add();
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["songid"].Value = dtDetail.Rows[iCtr]["TitleID"];
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["songname"].Value = dtDetail.Rows[iCtr]["Title"];

                        string strTime = dtDetail.Rows[iCtr]["Time"].ToString();
                        string[] arr = strTime.Split(':');
                        TitleTime = arr[1] + ":" + arr[2];

                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Length"].Value = TitleTime;
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Artist"].Value = dtDetail.Rows[iCtr]["ArtistName"];
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Album"].Value = dtDetail.Rows[iCtr]["AlbumName"];

                        //dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                        //dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Length"].Style.Font = new Font("Segoe UI", 10);
                        //dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                        //dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                    }
                    foreach (DataGridViewRow row in dgCommanGrid.Rows)
                    {
                        row.Height = 30;
                    }
                    //dgCommanGrid.Sort(dgCommanGrid.Columns[1], ListSortDirection.Ascending);
                }

            }

            catch
            {

                return;
            }
        }

        private void picSavePlaylist_Click(object sender, EventArgs e)
        {
            string lStr = "";
            try
            {
                lStr = "select * from Playlists where  ispredefined=1 and name='" + txtPlaylistName.Text + "' and PlaylistID <>  " + ModifyPlaylistId;
                DataSet ds = new DataSet();
                ds = ObjMainClass.fnFillDataSet(lStr);

                if (txtPlaylistName.Text == "")
                {
                    MessageBox.Show("The playlist cannot be empty or without a name.", "Management Panel");
                    return;
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {

                    MessageBox.Show("This playlist name is already used.", "Management Panel");
                    return;

                }

                if (pAction == "New")
                {
                    PlaylistSave();
                    txtPlaylistName.Text = "";
                    pAction = "New";
                }
                else
                {
                    PlaylistModify();
                    txtPlaylistName.Text = "";
                    pAction = "New";
                }
                FillLocalPlaylist();
                ModifyPlaylistId = 0;

            }
            catch
            {

                return;
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Visible == true)
            {
                if (txtSearch.Text == "" || txtSearch.Text == "Search")
                {
                    string str = "SELECT TOP (300) Titles.TitleID, ltrim(Titles.Title) as Title, Titles.Time, ltrim(Artists.Name) as ArtistName, ltrim(Albums.Name) AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4 order by TitleID desc";
                    FillGrid(str);
                    return;
                }
                if (txtSearch.Text.Length < 2)
                {
                    MessageBox.Show("Enter minimum 2 characters for a search function.", "Token Admin");
                    return;
                }

                SearchText = txtSearch.Text.Trim();
                CommanSearch();
                txtSearch.TextAlign = HorizontalAlignment.Left;
                txtSearch.ForeColor = Color.White;
                txtSearch.Text = "";
            }
        }

        private void CommanSearch()
        {
            string stSearch = "";
            string strAlbum = "";
            if (rdoTitle.Checked == true)
            {
                stSearch = "spSearch_Title '" + SearchText + "'";
                FillGrid(stSearch);
            }
            else if (rdoArtist.Checked == true)
            {
                stSearch = "spSearch_Artist '" + SearchText + "'";
                FillGrid(stSearch);
            }
            else if (rdoAlbum.Checked == true)
            {
                strAlbum = "spSearch_Album_Copyright '" + SearchText + "'";
                ObjMainClass.fnFillComboBoxSpl(strAlbum, cmbAlbum, "AlbumID", "Name", "");
                cmbAlbum.Visible = true;
                txtSearch.Visible = false;
            }
        }

        private void rdoTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTitle.Checked == true)
            {
                cmbAlbum.Visible = false;
                txtSearch.Visible = true;
            }
        }

        private void rdoArtist_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoArtist.Checked == true)
            {
                cmbAlbum.Visible = false;
                txtSearch.Visible = true;
            }
        }

        private void rdoAlbum_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAlbum.Checked == true)
            {
                cmbAlbum.Visible = false;
                txtSearch.Visible = true;
            }
        }

        private void cmbAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stSearch = "";
            stSearch = "spSearch_Album " + cmbAlbum.SelectedValue;
            FillGrid(stSearch);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
            }
            txtSearch.TextAlign = HorizontalAlignment.Left;
            txtSearch.ForeColor = Color.White;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search";
                txtSearch.TextAlign = HorizontalAlignment.Center;
                txtSearch.ForeColor = Color.White;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txtSearch.Text == "" || txtSearch.Text == "Search")
                {
                    string str = "SELECT TOP (300) Titles.TitleID, ltrim(Titles.Title) as Title, Titles.Time, ltrim(Artists.Name) as ArtistName, ltrim(Albums.Name) AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4 order by TitleID desc";
                    FillGrid(str);
                    return;
                }
                if (txtSearch.Text.Length < 2)
                {
                    MessageBox.Show("Enter minimum 2 characters for a search function.", "Token Admin");
                    return;
                }

                SearchText = txtSearch.Text.Trim();
                CommanSearch();
                txtSearch.TextAlign = HorizontalAlignment.Left;
                txtSearch.ForeColor = Color.White;
                txtSearch.Text = "";
            }
        }
        private void PlaylistSave()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("InsertPlayListsNew", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.BigInt));
            cmd.Parameters["@UserID"].Value = 2;

            cmd.Parameters.Add(new SqlParameter("@IsPredefined", SqlDbType.Bit));
            cmd.Parameters["@IsPredefined"].Value = 1;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
            cmd.Parameters["@Name"].Value = txtPlaylistName.Text;

            cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 50));
            cmd.Parameters["@Summary"].Value = txtPlaylistName.Text;

            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50));
            cmd.Parameters["@Description"].Value = " ";

            cmd.Parameters.Add(new SqlParameter("@TokenId", SqlDbType.BigInt));
            cmd.Parameters["@TokenId"].Value = 0;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // throw new ApplicationException ("Data error.");
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void PlaylistModify()
        {
            try
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("UpdateUserPlayLists", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PlayListID", SqlDbType.BigInt));
                cmd.Parameters["@PlayListID"].Value = ModifyPlaylistId;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
                cmd.Parameters["@Name"].Value = txtPlaylistName.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                StaticClass.constr.Close();
            }
        }

        private void picAddtoPlaylist_Click(object sender, EventArgs e)
        {
            string lStr;
            try
            {
                for (int i = 0; i < dgCommanGrid.Rows.Count; i++)
                {
                    if (dgCommanGrid.Rows[i].Selected == true)
                    {
                        //If already song add in playlist then this step:-
                        lStr = "select * from TitlesInPlaylists where PlaylistID=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID=" + dgCommanGrid.Rows[i].Cells[0].Value;
                        DataSet ds = new DataSet();
                        ds = ObjMainClass.fnFillDataSet(lStr);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            string TitleTime = "";
                            string Title_id = "";
                            string Title = "";
                            string AlbumName = "";
                            string ArtistName = "";
                            Title_id = dgCommanGrid.Rows[i].Cells["songid"].Value.ToString();
                            Title = dgCommanGrid.Rows[i].Cells["songname"].Value.ToString();
                            TitleTime = dgCommanGrid.Rows[i].Cells["Length"].Value.ToString();
                            AlbumName = dgCommanGrid.Rows[i].Cells["album"].Value.ToString();
                            ArtistName = dgCommanGrid.Rows[i].Cells["Artist"].Value.ToString();
                            dgPlaylist.Rows.Insert(dgPlaylist.Rows.Count, Title_id, Title, TitleTime, ArtistName, AlbumName);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Length"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Italic);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Italic);
                            insert_Playlist_song(dgCommanGrid.Rows[i].Cells[0].Value.ToString(), "No", false);
                            GetSongCounter();
                        }
                    }
                }
                foreach (DataGridViewRow row in dgPlaylist.Rows)
                {
                    row.Height = 30;
                }

            }
            catch
            {


            }
        }

        void insert_Playlist_song(string songid, string GridReset, Boolean IsComeDropSong)
        {
            try
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("InsertTitlesInPlayLists", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PlayListID", SqlDbType.BigInt));
                cmd.Parameters["@PlayListID"].Value = Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value);
                cmd.Parameters.Add(new SqlParameter("@TitleID", SqlDbType.BigInt));
                cmd.Parameters["@TitleID"].Value = songid;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // throw new ApplicationException("Data error.");
            }
            finally
            {
                // StaticClass.constr.Close();
            }

        }

        private void GetSongCounter()
        {
            string strNew = "";
            DataTable dtDetailNew = new DataTable();
            strNew = "select TitlesInPlaylists.playlistId, Count(*) as Total  from TitlesInPlaylists ";
            strNew = strNew + " where TitlesInPlaylists.playlistId = " + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " ";
            strNew = strNew + " group by TitlesInPlaylists.playlistId ";
            dtDetailNew = ObjMainClass.fnFillDataTable(strNew);
            if ((dtDetailNew.Rows.Count > 0))
            {
                for (int iCtr = 0; (iCtr <= (dgLocalPlaylist.Rows.Count - 1)); iCtr++)
                {
                    if (Convert.ToInt32(dgLocalPlaylist.Rows[iCtr].Cells[0].Value) == Convert.ToInt32(dtDetailNew.Rows[0]["playlistId"]))
                    {
                        string strGetName = dgLocalPlaylist.Rows[iCtr].Cells[1].Value.ToString();
                        string[] arr = strGetName.Split('(');
                        dgLocalPlaylist.Rows[iCtr].Cells[1].Value = arr[0].Trim() + "  (" + dtDetailNew.Rows[0]["Total"] + ")";
                    }
                    // dtDetail.Rows[iCtr]["playlistId"];
                    //  
                }
            }
            else
            {
                FillLocalPlaylist();
            }
        }

        private void dgPlaylist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPlaylist.CurrentCell.RowIndex == -1) return;
            if (e.ColumnIndex == 5)
            {
                try
                {
                    if (dgPlaylist.CurrentCell.RowIndex == -1) return;
                    DialogResult rlt;
                    rlt = MessageBox.Show("Are you sure to delete this song?", "Management Panel", MessageBoxButtons.YesNo);
                    if (rlt == DialogResult.Yes)
                    {
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = StaticClass.constr;
                        cmd.CommandText = "delete from TitlesInPlaylists where PlaylistID=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID =" + dgPlaylist.Rows[dgPlaylist.CurrentCell.RowIndex].Cells[0].Value;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();
                        dgPlaylist.Rows.RemoveAt(dgPlaylist.CurrentCell.RowIndex);
                        GetSongCounter();
                    }
                }
                catch (Exception ex) { }
            }
            
        }

        private void dgPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (dgPlaylist.CurrentCell.RowIndex == -1) return;
                    DialogResult rlt;
                    rlt = MessageBox.Show("Are you sure to delete this song?", "Management Panel", MessageBoxButtons.YesNo);
                    if (rlt == DialogResult.Yes)
                    {
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = StaticClass.constr;
                        cmd.CommandText = "delete from TitlesInPlaylists where PlaylistID=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID =" + dgPlaylist.Rows[dgPlaylist.CurrentCell.RowIndex].Cells[0].Value;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();
                        dgPlaylist.Rows.RemoveAt(dgPlaylist.CurrentCell.RowIndex);
                        GetSongCounter();
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void picDeleteSongs_Click(object sender, EventArgs e)
        {
            try
            {
                string stId = "";
                if (dgPlaylist.CurrentCell.RowIndex == -1) return;
                DialogResult rlt;
                rlt = MessageBox.Show("Are you sure to delete selected songs ?", "Management Panel", MessageBoxButtons.YesNo);
                if (rlt == DialogResult.Yes)
                {
                    for (int i = 0; i < dgPlaylist.Rows.Count; i++)
                    {
                        if (dgPlaylist.Rows[i].Selected == true)
                        {

                            if (stId == "")
                            {
                                stId = Convert.ToString(dgPlaylist.Rows[i].Cells[0].Value);
                            }
                            else
                            {
                                stId = stId + "," + Convert.ToString(dgPlaylist.Rows[i].Cells[0].Value);
                            }
                        }
                    }

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from TitlesInPlaylists where PlaylistID=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID in(" + stId + ")";
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value));
                    GetSongCounter();
                }
               
            }
            catch (Exception ex) { }
        }

        private void picDeletePlaylists_Click(object sender, EventArgs e)
        {
            try
            {
                string stId = "";
                if (dgLocalPlaylist.CurrentCell.RowIndex == -1) return;
                DialogResult rlt;
                rlt = MessageBox.Show("Are you sure to delete selected playlists ?", "Management Panel", MessageBoxButtons.YesNo);
                if (rlt == DialogResult.Yes)
                {
                    for (int i = 0; i < dgLocalPlaylist.Rows.Count; i++)
                    {
                        if (dgLocalPlaylist.Rows[i].Selected == true)
                        {
                            if (stId == "")
                            {
                                stId = Convert.ToString(dgLocalPlaylist.Rows[i].Cells[0].Value);
                            }
                            else
                            {
                                stId = stId + "," + Convert.ToString(dgLocalPlaylist.Rows[i].Cells[0].Value);
                            }
                        }
                    }

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from TitlesInPlaylists where PlaylistID in (" + stId + ")";
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from Playlists where PlaylistID in(" + stId + ")";
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();

                    FillLocalPlaylist();
                }
               
            }
            catch (Exception ex) { }
        }

    }
}
