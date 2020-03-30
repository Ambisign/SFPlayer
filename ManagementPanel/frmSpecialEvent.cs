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
    public partial class frmSpecialEvent : Form
    {
        gblClass ObjMainClass = new gblClass();
        string SearchText = "";
        Int32 ModifyEventId = 0;
        string pAction = "New";
        CheckBox ClientCheckBox = null;
        bool IsClientCheckBoxClicked = false;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        string IsRecordModify = "No";
        public frmSpecialEvent()
        {
            InitializeComponent();

            string str = "SELECT TOP (500) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4  order by TitleID desc";
            FillGrid(str);
            lblName.Text = "Best of playlists";
            dgBestofPlaylist.Visible = true;
            dgBestofPlaylist.Dock = DockStyle.Fill;
            dgDam.Visible = false;
            InitilizeGrid(dgPlaylist);
            FillDamGenre(1);
            FillDamGenre(8);
            FillBestofGrid();
        }

        private void cmbPlayerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,ClientName  from ( select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1  ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where pversion='" + cmbPlayerVersion.Text + "') ";
            str = str + " ) as a order by ClientName desc ";
            ObjMainClass.fnFillComboBox(str, cmbDealer, "DFClientID", "ClientName", "");

            if (cmbPlayerVersion.Text == "NativeCL")
            {
                rdoAlbum.Visible = false;
                lblName.Text = "Dam Genre";
                dgDam.Visible = true;
                dgDam.Dock = DockStyle.Fill;
                dgBestofPlaylist.Visible = false;
                FillDamGenreTitles(Convert.ToInt32(dgDam.Rows[dgDam.CurrentCell.RowIndex].Cells[0].Value));
            }
            if (cmbPlayerVersion.Text == "NativeAsian")
            {
                rdoAlbum.Visible = false;
                lblName.Text = "Asian Genre";
                dgDam.Visible = true;
                dgDam.Dock = DockStyle.Fill;
                dgBestofPlaylist.Visible = false;
                FillDamGenreTitles(Convert.ToInt32(dgDam.Rows[dgDam.CurrentCell.RowIndex].Cells[0].Value));
            }
            else
            {
                rdoAlbum.Visible = true;
                lblName.Text = "Best of playlists";
                dgBestofPlaylist.Visible = true;
                dgBestofPlaylist.Dock = DockStyle.Fill;
                dgDam.Visible = false;
                str = "";
                str = "SELECT TOP (400) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4  order by TitleID desc";
                FillGrid(str);
            }
        }
        private void FillDamGenreTitles(Int32 CurrentSubCategoryId)
        {
            string str = "";
            str = "select titles.TitleID,ltrim(titles.Title) as Title,titles.Time, ltrim(Artists.Name) as ArtistName,'' as AlbumName from titles  ";
            str = str + " inner join Artists on titles.ArtistID=  Artists.ArtistID ";
            str = str + "where title <>'' and TitleSubCategoryId= '" + CurrentSubCategoryId + "' order by titles.Title";
            FillGrid(str);
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
            dgGrid.Columns["songname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgGrid.Columns.Add("Length", "Length");
            dgGrid.Columns["Length"].Width = 130;
            dgGrid.Columns["Length"].Visible = true;
            dgGrid.Columns["Length"].ReadOnly = true;
            dgGrid.Columns["Length"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Artist", "Artist");
            dgGrid.Columns["Artist"].Width = 250;
            dgGrid.Columns["Artist"].Visible = true;
            dgGrid.Columns["Artist"].ReadOnly = true;
            dgGrid.Columns["Artist"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Album", "Album");
            dgGrid.Columns["Album"].Width = 250;
            if ((cmbPlayerVersion.Text == "NativeCL") || (cmbPlayerVersion.Text == "NativeAsian"))
            {
                dgGrid.Columns["Album"].Visible = false;
            }
            else
            {
                dgGrid.Columns["Album"].Visible = true;
            }
            dgGrid.Columns["Album"].ReadOnly = true;
            dgGrid.Columns["Album"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;



            dgGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, System.Drawing.FontStyle.Regular);
            dgGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgGrid.EnableHeadersVisualStyles = false;
            dgGrid.ColumnHeadersHeight = 30;
            dgGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

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
        private void InitilizeDamGrid()
        {
            if (dgDam.Rows.Count > 0)
            {
                dgDam.Rows.Clear();
            }
            if (dgDam.Columns.Count > 0)
            {
                dgDam.Columns.Clear();
            }

            dgDam.Columns.Add("songid", "song Id");
            dgDam.Columns["songid"].Width = 0;
            dgDam.Columns["songid"].Visible = false;
            dgDam.Columns["songid"].ReadOnly = true;

            dgDam.Columns.Add("Genre", "Genre");
            dgDam.Columns["Genre"].Width = 245;
            dgDam.Columns["Genre"].Visible = true;
            dgDam.Columns["Genre"].ReadOnly = true;
            dgDam.Columns["Genre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void FillDamGenre(Int32 titlecategoryId)
        {
            string str;
            int iCtr;
            DataTable dtDetail;
            str = "select * from tblTitleSubCategory where titlecategoryId =" + titlecategoryId + " order by TitleSubCategoryName";
            dtDetail = ObjMainClass.fnFillDataTable(str);
            InitilizeDamGrid();
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgDam.Rows.Add();
                    dgDam.Rows[dgDam.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[iCtr]["titleSubcategoryId"];
                    dgDam.Rows[dgDam.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[iCtr]["TitleSubCategoryName"];
                }
                foreach (DataGridViewRow row in dgDam.Rows)
                {
                    row.Height = 30;
                }
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == "Search")
            {
                if (cmbPlayerVersion.Text == "NativeCR")
                {
                    string str = "SELECT TOP (300) Titles.TitleID, ltrim(Titles.Title) as Title, Titles.Time, ltrim(Artists.Name) as ArtistName, ltrim(Albums.Name) AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4  order by TitleID desc";
                    FillGrid(str);
                    return;
                }
                if((cmbPlayerVersion.Text == "NativeCL") ||  (cmbPlayerVersion.Text == "NativeAsian"))
                {
                    FillDamGenreTitles(Convert.ToInt32(dgDam.Rows[dgDam.CurrentCell.RowIndex].Cells[0].Value));
                    return;
                }

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
        private void CommanSearch()
        {
            string stSearch = "";
            string strAlbum = "";
            if (cmbPlayerVersion.Text == "NativeCR")
            {
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
            if (cmbPlayerVersion.Text == "NativeCL")
            {
                stSearch = "";
                strAlbum = "";
                if (rdoTitle.Checked == true)
                {
                    stSearch = "spSearch_Title_Copyleft '" + SearchText + "',1 ,0";
                    FillGrid(stSearch);
                }
                else if (rdoArtist.Checked == true)
                {
                    stSearch = "spSearch_Artist_Copyleft '" + SearchText + "',1 ,0";
                    FillGrid(stSearch);
                }
            }
            if (cmbPlayerVersion.Text == "NativeAsian")
            {
                if (rdoTitle.Checked == true)
                {
                    stSearch = "spSearch_Title_Asian '" + SearchText + "',1 ,0";
                    FillGrid(stSearch);
                }
                else if (rdoArtist.Checked == true)
                {
                    stSearch = "spSearch_Artist_Asian '" + SearchText + "',1 ,0";
                    FillGrid(stSearch);
                }
                else if (rdoAlbum.Checked == true)
                {
                    strAlbum = "spSearch_Album_Asian '" + SearchText + "'";
                    ObjMainClass.fnFillComboBox(strAlbum, cmbAlbum, "AlbumID", "Name", "");
                    cmbAlbum.Visible = true;
                    txtSearch.Visible = false;
                }
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txtSearch.Text == "" || txtSearch.Text == "Search")
                {
                    if (cmbPlayerVersion.Text == "NativeCR")
                    {
                        string str = "SELECT TOP (300) Titles.TitleID, ltrim(Titles.Title) as Title, Titles.Time, ltrim(Artists.Name) as ArtistName, ltrim(Albums.Name) AS AlbumName FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID where titlecategoryid=4  order by TitleID desc";
                        FillGrid(str);
                        return;
                    }
                    if ((cmbPlayerVersion.Text == "NativeCL") || (cmbPlayerVersion.Text == "NativeAsian"))
                    {
                        FillDamGenreTitles(Convert.ToInt32(dgDam.Rows[dgDam.CurrentCell.RowIndex].Cells[0].Value));
                        return;
                    }

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


        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 39 || Convert.ToInt32(e.KeyChar) == 37)
            {
                e.Handled = true;
                return;
            }
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

        private void dgDam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            FillDamGenreTitles(Convert.ToInt32(dgDam.Rows[e.RowIndex].Cells[0].Value));
        }

        private void picSavePlaylist_Click(object sender, EventArgs e)
        {
            Save(txtPlaylistName.Text.Trim());
        }
        private void Save(string playlistName)
        {
            string lStr = "";
            try
            {
                if (playlistName == "")
                {
                    MessageBox.Show("The event name cannot be empty", "Management Panel");
                    txtPlaylistName.Focus();
                    return;
                }
                lStr = "select * from tbSpecialEvent where pversion='" + cmbPlayerVersion.Text + "' and dfclientid=" + cmbDealer.SelectedValue + "  and EventName='" + playlistName + "' and EventId <>  " + ModifyEventId;
                DataSet ds = new DataSet();
                ds = ObjMainClass.fnFillDataSet(lStr);
                if (cmbPlayerVersion.Text == "")
                {
                    MessageBox.Show("Please select a player version", "Management Panel");
                    cmbPlayerVersion.Focus();
                    return;
                }

                if (Convert.ToInt32(cmbDealer.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select a dealer name", "Management Panel");
                    cmbDealer.Focus();
                    return;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("This event name is already used.", "Management Panel");
                    txtPlaylistName.Focus();
                    return;
                }
                PlaylistSaveUpdate(playlistName);
                txtPlaylistName.Text = "";
                playlistName = "";
                pAction = "New";
                FillLocalPlaylist();
                ModifyEventId = 0;
            }
            catch
            {

                return;
            }
        }
        private void PlaylistSaveUpdate(string playlistName)
        {
            Int32 lPlaylistId = 0;
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("spSpecialEvent_Save_Update", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pAction", SqlDbType.VarChar));
            cmd.Parameters["@pAction"].Value = pAction;
            cmd.Parameters.Add(new SqlParameter("@EventId", SqlDbType.BigInt));
            cmd.Parameters["@EventId"].Value = ModifyEventId;
            cmd.Parameters.Add(new SqlParameter("@EventName", SqlDbType.VarChar));
            cmd.Parameters["@EventName"].Value = playlistName;
            cmd.Parameters.Add(new SqlParameter("@DfClientId", SqlDbType.BigInt));
            cmd.Parameters["@DfClientId"].Value = Convert.ToInt32(cmbDealer.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@pVersion", SqlDbType.VarChar));
            cmd.Parameters["@pVersion"].Value = cmbPlayerVersion.Text;
            try
            {
                lPlaylistId = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void picAddtoPlaylist_Click(object sender, EventArgs e)
        {
            string lStr;
            int pLimit = 0;
            int sCount = 0;
            try
            {
                if (dgPlaylist.Rows.Count >= 250)
                {
                    MessageBox.Show("This event list has reached the maximum quantity of songs.", "Management Panel");
                    return;
                }
                pLimit = 50 - dgPlaylist.Rows.Count;
                for (int i = 0; i < dgCommanGrid.Rows.Count; i++)
                {
                    if (dgCommanGrid.Rows[i].Selected == true)
                    {
                        lStr = "select * from tbSpecialEvent_Titles where EventId=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID=" + dgCommanGrid.Rows[i].Cells[0].Value;
                        DataSet ds = new DataSet();
                        ds = ObjMainClass.fnFillDataSet(lStr);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            if (sCount >= 250) { break; }
                            if (pLimit <= 0) { break; }
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
                            sCount = sCount + 1;
                            pLimit = pLimit - 1;
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
                SqlCommand cmd = new SqlCommand("InsertTitlesInSplEvent", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EventId", SqlDbType.BigInt));
                cmd.Parameters["@EventId"].Value = Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value);
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
            strNew = "select tbSpecialEvent_Titles.EventId, Count(*) as Total  from tbSpecialEvent_Titles ";
            strNew = strNew + " where tbSpecialEvent_Titles.EventId = " + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " ";
            strNew = strNew + " group by tbSpecialEvent_Titles.EventId ";
            dtDetailNew = ObjMainClass.fnFillDataTable(strNew);
            if ((dtDetailNew.Rows.Count > 0))
            {
                for (int iCtr = 0; (iCtr <= (dgLocalPlaylist.Rows.Count - 1)); iCtr++)
                {
                    if (Convert.ToInt32(dgLocalPlaylist.Rows[iCtr].Cells[0].Value) == Convert.ToInt32(dtDetailNew.Rows[0]["EventId"]))
                    {
                        string strGetName = dgLocalPlaylist.Rows[iCtr].Cells[1].Value.ToString();
                        string[] arr = strGetName.Split('(');
                        dgLocalPlaylist.Rows[iCtr].Cells[1].Value = arr[0].Trim() + "  (" + dtDetailNew.Rows[0]["Total"] + ")";
                    }
                    // dtDetail.Rows[iCtr]["playlistId"];
                    //  
                }
            }
        }
        private void FillLocalPlaylist()
        {

            string str = "";
            string strGetCount = "";
            int iCtr;
            DataTable dtDetail;
            DataTable dtGetCount;
            str = "select * from tbSpecialEvent where pVersion='" + cmbPlayerVersion.Text + "' and DfClientId = " + Convert.ToInt32(cmbDealer.SelectedValue) + " order by EventName";
            dtDetail = ObjMainClass.fnFillDataTable(str);

            InitilizeLocalGrid();
            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgLocalPlaylist.Rows.Add();
                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[iCtr]["EventId"];

                    strGetCount = "select Count(*) as Total from  tbSpecialEvent_Titles where EventId =" + dtDetail.Rows[iCtr]["EventId"] + " ";
                    dtGetCount = ObjMainClass.fnFillDataTable(strGetCount);

                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[iCtr]["EventName"] + "  (" + dtGetCount.Rows[0]["Total"] + ")";
                    dgLocalPlaylist.Rows[dgLocalPlaylist.Rows.Count - 1].Cells[2].Value = "";



                }
                foreach (DataGridViewRow row in dgLocalPlaylist.Rows)
                {
                    row.Height = 35;
                }
                dgLocalPlaylist.CurrentCell = dgLocalPlaylist.Rows[0].Cells[1];
                dgLocalPlaylist.Rows[0].Selected = true;
                PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[0].Cells[0].Value), false);
            }
            else
            {
                InitilizeGrid(dgPlaylist);
            }


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
            dgLocalPlaylist.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgLocalPlaylist.Columns.Add("playlistname", "Playlist Name");
            dgLocalPlaylist.Columns["playlistname"].Width = 250;
            dgLocalPlaylist.Columns["playlistname"].Visible = true;
            dgLocalPlaylist.Columns["playlistname"].ReadOnly = true;
            dgLocalPlaylist.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            EditPlaylist.LinkBehavior = LinkBehavior.SystemDefault;
            dgLocalPlaylist.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            EditPlaylist.Visible = true;
            dgLocalPlaylist.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            EditPlaylist.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);

            DataGridViewLinkColumn DeleteSong = new DataGridViewLinkColumn();
            DeleteSong.HeaderText = "Delete";
            DeleteSong.Text = "Delete";
            DeleteSong.DataPropertyName = "Delete";
            DeleteSong.LinkColor = Color.FromArgb(64, 64, 64);
            DeleteSong.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            DeleteSong.LinkBehavior = LinkBehavior.SystemDefault;
            dgLocalPlaylist.Columns.Add(DeleteSong);
            DeleteSong.UseColumnTextForLinkValue = true;
            DeleteSong.Width = 55;
            dgLocalPlaylist.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void PopulateInputFileTypeDetail(DataGridView dgGrid, Int32 currentPlayRow, Boolean IsBestOf)
        {
            string mlsSql = "";
            string TitleTime = "";
            var Special_Name = "";
            string Special_Change = "";
            Int32 iCtr = 0;
            DataTable dtDetail = new DataTable();

            mlsSql = "SELECT  Titles.TitleID, rtrim(ltrim(Titles.Title)) as Title, Titles.Time,rtrim(ltrim(Albums.Name)) AS AlbumName ,";
            mlsSql = mlsSql + " Titles.TitleYear ,  rtrim(ltrim(Artists.Name)) as ArtistName  FROM ((( tbSpecialEvent_Titles  ";
            mlsSql = mlsSql + " INNER JOIN Titles ON tbSpecialEvent_Titles.TitleID = Titles.TitleID )  ";
            mlsSql = mlsSql + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID ) ";
            mlsSql = mlsSql + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID ) ";
            mlsSql = mlsSql + " where tbSpecialEvent_Titles.EventId= " + Convert.ToInt32(currentPlayRow) + " order by Titles.Title ";

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
                    PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value), false);
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (e.RowIndex >= 0)
                {
                    string str45 = dgLocalPlaylist.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string[] arr = str45.Split('(');
                    txtPlaylistName.Text = arr[0].Trim();
                    ModifyEventId = Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value);
                    pAction = "Modify";
                    txtPlaylistName.Focus();
                }
            }

            if (e.ColumnIndex == 3)
            {
                if (e.RowIndex >= 0)
                {
                    DialogResult result;
                    result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string strDel = "";
                        Int32 pschid = 0;
                        DataTable dtDetail = new DataTable();
                        strDel = "select  * from tbSpecialEvent_Token";
                        strDel = strDel + " where EventId = " + Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value);


                        dtDetail = ObjMainClass.fnFillDataTable(strDel);
                        if ((dtDetail.Rows.Count > 0))
                        {
                            MessageBox.Show("This event list cannot be deleted, as it is assigned to tokens", "Management Panel");
                            return;
                        }

                        strDel = "delete from tbSpecialEvent_Titles where EventId= " + Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value);
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        strDel = "";
                        strDel = "delete from tbSpecialEvent where EventId= " + Convert.ToInt32(dgLocalPlaylist.Rows[e.RowIndex].Cells[0].Value);
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        cmd = new SqlCommand(strDel, StaticClass.constr);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        dgLocalPlaylist.Rows.RemoveAt(e.RowIndex);

                        if (dgLocalPlaylist.Rows.Count > 0)
                        {
                            PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[0].Cells[0].Value), false);
                        }
                        else
                        {
                            InitilizeGrid(dgPlaylist);
                        }

                    }
                }
            }


        }

        private void cmbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocalPlaylist();
        }
        private void FillBestofGrid()
        {
            int iCtr;
            string str = "";
            str = "select PlaylistId as IdName, ltrim(Name) as textName from Playlists where isPredefined=1 order by Name";
            try
            {
                InitilizeCommanOptionGrid(dgBestofPlaylist);
                DataTable dtDetail;
                DataTable dtGetCount = new DataTable();
                dtDetail = ObjMainClass.fnFillDataTable(str);
                if ((dtDetail.Rows.Count > 0))
                {
                    for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                    {
                        dgBestofPlaylist.Rows.Add();
                        dgBestofPlaylist.Rows[dgBestofPlaylist.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[iCtr]["IdName"];
                        str = "select Count(*) as Total from  TitlesInPlaylists where PlaylistID =" + dtDetail.Rows[iCtr]["IdName"] + " ";
                        dtGetCount = ObjMainClass.fnFillDataTable(str);
                        dgBestofPlaylist.Rows[dgBestofPlaylist.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[iCtr]["textname"] + "  (" + dtGetCount.Rows[0]["Total"] + ")";
                    }
                    foreach (DataGridViewRow row in dgBestofPlaylist.Rows)
                    {
                        row.Height = 30;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void InitilizeCommanOptionGrid(DataGridView Grid_Name)
        {
            if (Grid_Name.Rows.Count > 0)
            {
                Grid_Name.Rows.Clear();
            }
            if (Grid_Name.Columns.Count > 0)
            {
                Grid_Name.Columns.Clear();
            }

            Grid_Name.Columns.Add("playlistId", "playlist Id");
            Grid_Name.Columns["playlistId"].Width = 0;
            Grid_Name.Columns["playlistId"].Visible = false;
            Grid_Name.Columns["playlistId"].ReadOnly = true;

            Grid_Name.Columns.Add("CommanName", "Comman Name");
            Grid_Name.Columns["CommanName"].Visible = true;
            Grid_Name.Columns["CommanName"].ReadOnly = true;

            Grid_Name.Columns["CommanName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgBestofPlaylist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = "";
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 1)
            {
                if (e.RowIndex >= 0)
                {
                    str = "SELECT Titles.TitleID, ltrim(Titles.Title) as Title,Titles.Time, ltrim(Artists.Name) AS ArtistName, ltrim(Albums.Name) AS AlbumName ";
                    str = str + " FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID ";
                    str = str + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
                    str = str + " INNER JOIN TitlesInPlaylists ON Titles.TitleID = TitlesInPlaylists.TitleID ";
                    str = str + " where TitlesInPlaylists.PlaylistID= " + dgBestofPlaylist.Rows[e.RowIndex].Cells[0].Value + " order by Titles.Title ";
                    FillGrid(str);
                }
            }
        }

        private void picDeleteSongs_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgPlaylist.CurrentCell.RowIndex == -1) return;
                if (tbcMain.SelectedIndex == 1) return;
                string strDel = "";
                DataTable dtDetail = new DataTable();
                strDel = "select * from tbSpecialEvent_Token";
                strDel = strDel + " where EventId = " + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value);
                dtDetail = ObjMainClass.fnFillDataTable(strDel);
                if ((dtDetail.Rows.Count > 0))
                {
                    MessageBox.Show("This events songs are not delete becuase this is already assgined to tokens", "Management Panel");
                    return;
                }
                DialogResult rlt;
                rlt = MessageBox.Show("Are you sure to delete selected songs ?", "Management Panel", MessageBoxButtons.YesNo);
                if (rlt == DialogResult.Yes)
                {
                    for (int i = 0; i < dgPlaylist.Rows.Count; i++)
                    {
                        if (dgPlaylist.Rows[i].Selected == true)
                        {
                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = StaticClass.constr;
                            cmd.CommandText = "delete from tbSpecialEvent_Titles where EventId=" + Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value) + " and TitleID =" + dgPlaylist.Rows[i].Cells[0].Value;
                            cmd.ExecuteNonQuery();
                            StaticClass.constr.Close();
                            // dgPlaylist.Rows.RemoveAt(i);
                        }
                    }
                    PopulateInputFileTypeDetail(dgPlaylist, Convert.ToInt32(dgLocalPlaylist.Rows[dgLocalPlaylist.CurrentCell.RowIndex].Cells[0].Value), false);
                    GetSongCounter();
                }

            }
            catch (Exception ex) { }
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,ClientName  from ( select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1  ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens where pversion='" + cmbVersion.Text + "') ";
            str = str + " ) as a order by ClientName desc ";
            ObjMainClass.fnFillComboBox(str, cmbDname, "DFClientID", "ClientName", "");
        }

        private void cmbDname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbDname.SelectedValue) == 0)
            {
                InitilizeGrid();
                FillEvents();
                return;
            }
            FillEvents();
            IsClientCheckBoxClicked = true;
            ClientCheckBox.Checked = false;
            FillData();
            TotalCheckBoxes = dgToken.RowCount;
            TotalCheckedCheckBoxes = 0;
        }
        private void FillEvents()
        {
            string strState = "select * from tbSpecialEvent where dfclientid =" + Convert.ToInt32(cmbDname.SelectedValue) + " and pVersion='" + cmbVersion.Text + "'";
            ObjMainClass.fnFillComboBox(strState, cmbEvent, "EventId", "EventName", "");
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
        private void FillData()
        {
            string sQr = "";
            sQr = "SELECT AMPlayerTokens.TokenID , (iif(token='used','',isnull(tokennobkp,'')) + ' ' + convert(varchar(100) ,Tokenid)) as tNo, isnull(AMPlayerTokens.Location,'') as Location,";
            sQr = sQr + " isnull(tbCity.CityName,'') as CityName, isnull(tbState.StateName,'') as StateName, isnull(CountryCodes.CountryName,'') as CountryName,isnull(AMPlayerTokens.PersonName ,'') as PersonName , AMPlayerTokens.userid, isnull(AMPlayerTokens.IsStore,0) as IsStore, isnull(AMPlayerTokens.IsStream,0) as IsStream FROM  AMPlayerTokens ";
            sQr = sQr + " LEFT OUTER JOIN tbCity ON AMPlayerTokens.CityId = tbCity.CityId LEFT OUTER JOIN tbState ON AMPlayerTokens.StateId = tbState.StateId LEFT OUTER JOIN CountryCodes ON AMPlayerTokens.CountryId = CountryCodes.CountryCode";
            sQr = sQr + " WHERE AMPlayerTokens.ClientID = " + Convert.ToInt32(cmbDname.SelectedValue) + "and AMPlayerTokens.pVersion= '" + cmbVersion.Text + "'";
            DataTable dtDetail = new DataTable();
            InitilizeGrid();
            dtDetail = ObjMainClass.fnFillDataTable(sQr);
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
                dgToken.EndEdit();
                GetTokens();
            }
        }
        private void GetTokens()
        {
            string Localstr = "";
            Localstr = "select distinct TokenId, IsAllToken  from tbSpecialEvent_Token ";
            Localstr = Localstr + " where EventId=" + Convert.ToInt32(cmbEvent.SelectedValue) + "";
            Localstr = Localstr + " and TokenId != IsAllToken ";

            DataTable dtCommon = new DataTable();
            dtCommon = ObjMainClass.fnFillDataTable(Localstr);

            if ((dtCommon.Rows.Count > 0))
            {
                DeSelectList();
                ClientCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllToken"]);
                if (ClientCheckBox.Checked == false)
                {
                    for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                    {
                        for (int i = 0; i < dgToken.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["TokenId"]))
                            {
                                dgToken.Rows[i].Cells[1].Value = true;
                                IsRecordModify = "Yes";
                            }
                        }
                    }
                }
                if (ClientCheckBox.Checked == true)
                {
                    TokenCheckBoxClick(ClientCheckBox);
                    IsRecordModify = "Yes";
                }
            }
            else
            {
                DeSelectList();
            }
        }
        private void dgToken_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 1)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
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

        private void frmSpecialEvent_Load(object sender, EventArgs e)
        {
            InitilizeGrid();
            AddClientCheckBox(dgToken);
            ClientCheckBox.KeyUp += new KeyEventHandler(ClientCheckBox_KeyUp);
            ClientCheckBox.MouseClick += new MouseEventHandler(ClientCheckBox_MouseClick);
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Localstr = "";
            Localstr = "select distinct TokenId, IsAllToken  from tbSpecialEvent_Token ";
            Localstr = Localstr + " where EventId=" + Convert.ToInt32(cmbEvent.SelectedValue) + "";
            Localstr = Localstr + " and TokenId != IsAllToken ";

            DataTable dtCommon = new DataTable();
            dtCommon = ObjMainClass.fnFillDataTable(Localstr);

            if ((dtCommon.Rows.Count > 0))
            {
                DeSelectList();
                ClientCheckBox.Checked = Convert.ToBoolean(dtCommon.Rows[0]["IsAllToken"]);
                if (ClientCheckBox.Checked == false)
                {
                    for (int iCtr = 0; (iCtr <= (dtCommon.Rows.Count - 1)); iCtr++)
                    {
                        for (int i = 0; i < dgToken.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value) == Convert.ToInt32(dtCommon.Rows[iCtr]["TokenId"]))
                            {
                                dgToken.Rows[i].Cells[1].Value = true;
                                IsRecordModify = "Yes";
                            }
                        }
                    }
                }
                if (ClientCheckBox.Checked == true)
                {
                    TokenCheckBoxClick(ClientCheckBox);
                    IsRecordModify = "Yes";
                }
            }
            else
            {
                DeSelectList();
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
        private void DeSelectList()
        {
            ClientCheckBox.Checked = false;
            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                dgToken.Rows[i].Cells[1].Value = false;
            }
        }
        private Boolean SubmitValidationSch()
        {
            if (cmbVersion.Text == "")
            {
                MessageBox.Show("Please select a player version", "Management Panel");
                cmbVersion.Focus();
                return false;
            }
            if (Convert.ToInt32(cmbDname.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a dealer name", "Management Panel");
                cmbDname.Focus();
                return false;
            }
            if (Convert.ToInt32(cmbEvent.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a event name", "Management Panel");
                cmbEvent.Focus();
                return false;
            }
            if (IsRecordModify == "No")
            {
                if (CheckGridValidationAdvt(dgToken) == false)
                {
                    MessageBox.Show("Please select a token no's from list", "Management Panel");
                    return false;
                }
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
        private void btnSchSave_Click(object sender, EventArgs e)
        {
            if (SubmitValidationSch() == false) return;
            IsRecordModify = "No";
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = StaticClass.constr;
            cmd1.CommandText = "delete from tbSpecialEvent_Token where EventId=" + Convert.ToInt32(cmbEvent.SelectedValue) + " ";
            cmd1.ExecuteNonQuery();
            StaticClass.constr.Close();
            SaveToken();
            MessageBox.Show("Record is save", "Management Panel");
            InitilizeGrid();
            cmbVersion.Text = "";
            FillEvents();
        }
        private void SaveToken()
        {
            if (ClientCheckBox.Checked == true)
            {
                //  SaveTokenDetail(0, 1);
                //    return;
            }
            for (int i = 0; i < dgToken.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgToken.Rows[i].Cells[1].Value) == true)
                {
                    SaveTokenDetail(Convert.ToInt32(dgToken.Rows[i].Cells["Id"].Value), 0);

                }
            }
        }
        private void SaveTokenDetail(Int32 TokenId, int IsAllToken)
        {
            SqlCommand cmd = new SqlCommand("spSaveSpecialEvent_Token", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tokenId", SqlDbType.BigInt));
            cmd.Parameters["@tokenId"].Value = TokenId;

            cmd.Parameters.Add(new SqlParameter("@IsAllToken", SqlDbType.Int));
            cmd.Parameters["@IsAllToken"].Value = IsAllToken;

            cmd.Parameters.Add(new SqlParameter("@EventId", SqlDbType.BigInt));
            cmd.Parameters["@EventId"].Value = Convert.ToInt32(cmbEvent.SelectedValue);


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
        }

        private void txtPlaylistName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Save(txtPlaylistName.Text.Trim());
            }
        }
    }
}
