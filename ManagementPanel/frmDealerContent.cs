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
    public partial class frmDealerContent : Form
    {
        gblClass objMainClass = new gblClass();
        public frmDealerContent()
        {
            InitializeComponent();
            string mType = "";
            if (rdoAudio.Checked == true)
            {
                mType = "Audio";
            }
            if (rdoVideo.Checked == true)
            {
                   mType = "Video";
            }
            string str = "SELECT TOP (500) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, isnull(Titles.tempo,'') as Tempo,isnull(tbGenre.genre,'') as genre, Titles.titleyear , isnull(acategory,'') as Category FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId    where titlecategoryid=4  and mediatype='" + mType + "' order by TitleID desc";
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
            dgGrid.Columns["Length"].Width = 70;
            dgGrid.Columns["Length"].Visible = true;
            dgGrid.Columns["Length"].ReadOnly = true;
            dgGrid.Columns["Length"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Artist", "Artist");
            dgGrid.Columns["Artist"].Width = 150;
            dgGrid.Columns["Artist"].Visible = true;
            dgGrid.Columns["Artist"].ReadOnly = true;
            dgGrid.Columns["Artist"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Album", "Album");
            dgGrid.Columns["Album"].Width = 150;

            dgGrid.Columns["Album"].Visible = true;

            dgGrid.Columns["Album"].ReadOnly = true;
            dgGrid.Columns["Album"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            dgGrid.Columns.Add("Genre", "Genre");
            dgGrid.Columns["Genre"].Width = 150;

            dgGrid.Columns["Genre"].Visible = true;

            dgGrid.Columns["Genre"].ReadOnly = true;
            dgGrid.Columns["Genre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Tempo", "Tempo");
            dgGrid.Columns["Tempo"].Width = 60;
            dgGrid.Columns["Tempo"].Visible = true;
            dgGrid.Columns["Tempo"].ReadOnly = true;
            dgGrid.Columns["Tempo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("year", "year");
            dgGrid.Columns["year"].Width = 60;
            dgGrid.Columns["year"].Visible = true;
            dgGrid.Columns["year"].ReadOnly = true;
            dgGrid.Columns["year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.Columns.Add("Category", "Category");
            dgGrid.Columns["Category"].Width = 150;
            dgGrid.Columns["Category"].Visible = true;
            dgGrid.Columns["Category"].ReadOnly = true;
            dgGrid.Columns["Category"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
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
                dtDetail = objMainClass.fnFillDataTable(str);
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


                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Genre"].Value = dtDetail.Rows[iCtr]["genre"];
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Tempo"].Value = dtDetail.Rows[iCtr]["Tempo"];
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["year"].Value = dtDetail.Rows[iCtr]["TitleYear"];
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Category"].Value = dtDetail.Rows[iCtr]["Category"];

                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Genre"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Category"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["Tempo"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                        dgCommanGrid.Rows[dgCommanGrid.Rows.Count - 1].Cells["year"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    }
                    
                    
                }

            }

            catch
            {

                return;
            }
        }
        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCustomer.SelectedValue) == 0)
            {
                InitilizeGrid(dgPlaylist);
            }
            PopulateInputFileTypeDetail(dgPlaylist);
        }

        private void cmbCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
        }
        string SearchText = "";
        private void picSearch_Click(object sender, EventArgs e)
        {
            string mType = "";
            if (rdoAudio.Checked == true)
            {
                mType = "Audio";
            }
            if (rdoVideo.Checked == true)
            {
                mType = "Video";
            }
            if (txtSearch.Visible == true)
            {
                if (txtSearch.Text == "")
                {
                    string str = "SELECT TOP (500) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, isnull(Titles.tempo,'') as Tempo,isnull(tbGenre.genre,'') as genre , Titles.titleyear ,isnull(acategory,'') as Category FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId    where titlecategoryid=4 and mediaType='" + mType + "' order by TitleID desc";
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
                stSearch = "spSearch_Title_Comman '" + SearchText + "'";
                FillGrid(stSearch);
            }
            else if (rdoArtist.Checked == true)
            {
                stSearch = "spSearch_Artist_Comman '" + SearchText + "'";
                FillGrid(stSearch);
            }
            else if (rdoAlbum.Checked == true)
            {
                strAlbum = "spSearch_Album_Copyright '" + SearchText + "'";
                objMainClass.fnFillComboBoxSpl(strAlbum, cmbAlbum, "AlbumID", "Name", "");
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

        private void rdoGenre_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGenre.Checked == true)
            {
                string str = "select *  from tbGenre order by genre";
                objMainClass.fnFillComboBoxSpl(str, cmbAlbum, "GenreId", "genre", "");
                cmbAlbum.Visible = true;
                txtSearch.Visible = false;
            }
        }

        private void rdoTempo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTempo.Checked == true)
            {
                string str = "select  tempo  as Tempo, 1 as tempo from titles where tempo is not null group by tempo order by tempo";
                objMainClass.fnFillComboBoxSpl(str, cmbAlbum, "tempo", "Tempo", "");
                cmbAlbum.Visible = true;
                txtSearch.Visible = false;
            }
        }

        private void rdoCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCategory.Checked == true)
            {
                string str = "select  acategory as Acat, 1 as acategory from titles where acategory is not null group by acategory order by acategory";
                objMainClass.fnFillComboBoxSpl(str, cmbAlbum, "acategory", "Acat", "");
                cmbAlbum.Visible = true;
                txtSearch.Visible = false;
            }
        }

        private void rdoPlaylist_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPlaylist.Checked == true)
            {
                objMainClass.fnFillComboBoxSpl("GetPlaylistLibrary", cmbAlbum, "idname", "textname", "");
                cmbAlbum.Visible = true;
                txtSearch.Visible = false;
            }
        }

        private void cmbAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mType = "";
            if (rdoAudio.Checked == true)
            {
                mType = "Audio";
            }
            if (rdoVideo.Checked == true)
            {
                mType = "Video";
            }
            string stSearch = "";
            if (rdoAlbum.Checked == true)
            {
                stSearch = "spSearch_Album_spl " + cmbAlbum.SelectedValue;
            }
            else if (rdoGenre.Checked == true)
            {
                stSearch = " select distinct TitleID, ltrim(Title) as Title,Time, ltrim(ArtistName) as ArtistName, ltrim(AlbumName) as AlbumName , isnull(genre,'') as genre, Tempo ,titleyear,Category  from( ";
                stSearch = stSearch + " SELECT  Titles.TitleID, Titles.Title,Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, tbGenre.genre, isnull(Titles.tempo,'') as Tempo,Titles.titleyear , isnull(acategory,'') as Category FROM Titles ";
                stSearch = stSearch + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID  ";
                stSearch = stSearch + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
                stSearch = stSearch + " LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId  ";
                stSearch = stSearch + " where Titles.GenreId= " + Convert.ToInt32(cmbAlbum.SelectedValue) + " and Titles.mediatype='" + mType + "'";

                stSearch = stSearch + " ) as d  order by TitleID desc    ";
            }
            else if (rdoTempo.Checked == true)
            {
                stSearch = " select distinct TitleID, ltrim(Title) as Title,Time, ltrim(ArtistName) as ArtistName, ltrim(AlbumName) as AlbumName , isnull(genre,'') as genre, Tempo, titleyear,Category  from( ";
                stSearch = stSearch + " SELECT  Titles.TitleID, Titles.Title,Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, tbGenre.genre, isnull(Titles.tempo,'') as Tempo , Titles.titleyear, isnull(acategory,'') as Category  FROM Titles ";
                stSearch = stSearch + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID  ";
                stSearch = stSearch + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
                stSearch = stSearch + " LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId  ";
                stSearch = stSearch + " where Titles.tempo= '" + cmbAlbum.Text + "'  and Titles.mediatype='" + mType + "'";

                stSearch = stSearch + " ) as d  order by TitleID desc    ";
            }
            else if (rdoCategory.Checked == true)
            {
                stSearch = " select distinct TitleID, ltrim(Title) as Title,Time, ltrim(ArtistName) as ArtistName, ltrim(AlbumName) as AlbumName , isnull(genre,'') as genre, Tempo , titleyear,Category from( ";
                stSearch = stSearch + " SELECT  Titles.TitleID, Titles.Title,Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, tbGenre.genre, isnull(Titles.tempo,'') as Tempo , Titles.titleyear , isnull(acategory,'') as Category  FROM Titles ";
                stSearch = stSearch + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID  ";
                stSearch = stSearch + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
                stSearch = stSearch + " LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId  ";
                stSearch = stSearch + " where Titles.acategory= '" + cmbAlbum.Text + "'  and Titles.mediatype='" + mType + "'";

                stSearch = stSearch + " ) as d  order by TitleID desc    ";
            }
            else if (rdoPlaylist.Checked == true)
            {
                stSearch = " select distinct TitleID, ltrim(Title) as Title,Time, ltrim(ArtistName) as ArtistName, ltrim(AlbumName) as AlbumName , isnull(genre,'') as genre, Tempo , titleyear,Category from( ";
                stSearch = stSearch + " SELECT  Titles.TitleID, Titles.Title,Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, tbGenre.genre, isnull(Titles.tempo,'') as Tempo , Titles.titleyear , isnull(acategory,'') as Category  FROM Titles ";
                stSearch = stSearch + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID  ";
                stSearch = stSearch + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
                stSearch = stSearch + " LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId  ";
                stSearch = stSearch + " inner JOIN tbSpecialPlaylists_Titles ON Titles.TitleID = tbSpecialPlaylists_Titles.TitleID    ";
                stSearch = stSearch + " where tbSpecialPlaylists_Titles.splPlaylistid= " + Convert.ToInt32(cmbAlbum.SelectedValue) + "  and Titles.mediatype='" + mType + "'";

                stSearch = stSearch + " ) as d  order by TitleID desc    ";
            }


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
            string mType = "";
            if (rdoAudio.Checked == true)
            {
                mType = "Audio";
            }
            if (rdoVideo.Checked == true)
            {
                mType = "Video";
            }

            if (e.KeyCode == Keys.Return)
            {
                if (txtSearch.Text == "")
                {
                    string str = "SELECT TOP (500) Titles.TitleID, Titles.Title, Titles.Time, Artists.Name as ArtistName, Albums.Name AS AlbumName, isnull(Titles.tempo,'') as Tempo,isnull(tbGenre.genre,'') as genre , Titles.titleyear, isnull(acategory,'') as Category  FROM Titles INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId    where titlecategoryid=4  and mediatype='" + mType + "' order by TitleID desc";
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

        private void picAddtoPlaylist_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCustomer.SelectedValue) == 0)
            {
                MessageBox.Show("Please select a customer name","Management Panel");
                return;
            }
                string ContentType = "";
            if (rdoCopyright.Checked == true)
            {
                ContentType = "CR";
            }
            if (rdoRoyalty.Checked == true)
            {
                ContentType = "RF";
            }
            if (rdoVideoType.Checked == true)
            {
                ContentType = "Video";
            }
            string lStr;
            try
            {
                
                for (int i = 0; i < dgCommanGrid.Rows.Count; i++)
                {
                    if (dgCommanGrid.Rows[i].Selected == true)
                    {
                        //If already song add in playlist then this step:-
                        lStr = "select * from tbDealerContent where  TitleID=" + dgCommanGrid.Rows[i].Cells[0].Value+ " and ContentType='"+ ContentType + "'" ;

                        DataSet ds = new DataSet();
                        ds = objMainClass.fnFillDataSet(lStr);
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
                            dgPlaylist.Rows.Insert(dgPlaylist.Rows.Count, Title_id, Title, TitleTime, ArtistName, AlbumName, dgCommanGrid.Rows[i].Cells["Genre"].Value.ToString(), dgCommanGrid.Rows[i].Cells["Tempo"].Value.ToString());
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Length"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Genre"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                            dgPlaylist.Rows[dgPlaylist.Rows.Count - 1].Cells["Tempo"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);

                            Insert_DealerContent(dgCommanGrid.Rows[i].Cells[0].Value.ToString());
                             
                        }
                    }
                }
                

            }
            catch(Exception ex)
            {


            }
        }

        void Insert_DealerContent(string songid)
        {

            try
            {
                string ContentType = "";
                if (rdoCopyright.Checked == true)
                {
                    ContentType = "CR";
                }
                if (rdoRoyalty.Checked == true)
                {
                    ContentType = "RF";
                }
                if (rdoVideoType.Checked == true)
                {
                    ContentType = "Video";
                }

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("InsertDealerContent", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DfClientId", SqlDbType.BigInt));
                cmd.Parameters["@DfClientId"].Value = Convert.ToInt32(cmbCustomer.SelectedValue);
                cmd.Parameters.Add(new SqlParameter("@TitleID", SqlDbType.BigInt));
                cmd.Parameters["@TitleID"].Value = songid;
                cmd.Parameters.Add(new SqlParameter("@ContentType", SqlDbType.VarChar));
                cmd.Parameters["@ContentType"].Value = ContentType;
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void PopulateInputFileTypeDetail(DataGridView dgGrid)
        {
            string mlsSql = "";
            string TitleTime = "";
            var Special_Name = "";
            string Special_Change = "";
            Int32 iCtr = 0;
            DataTable dtDetail = new DataTable();
            string ContentType = "";
            if (rdoCopyright.Checked == true)
            {
                ContentType = "CR";
            }
            if (rdoRoyalty.Checked == true)
            {
                ContentType = "RF";
            }
            if (rdoVideoType.Checked == true)
            {
                ContentType = "Video";
            }
            mlsSql = "SELECT  Titles.TitleID, rtrim(ltrim(Titles.Title)) as Title, Titles.Time,rtrim(ltrim(Albums.Name)) AS AlbumName ,";
            mlsSql = mlsSql + " Titles.TitleYear ,  rtrim(ltrim(Artists.Name)) as ArtistName , isnull(tbGenre.genre,'') as genre, isnull(Titles.tempo,'') as Tempo  , isnull(acategory,'') as Category  FROM   tbDealerContent  ";
            mlsSql = mlsSql + " INNER JOIN Titles ON tbDealerContent.TitleID = Titles.TitleID   ";
            mlsSql = mlsSql + " INNER JOIN Albums ON Titles.AlbumID = Albums.AlbumID  ";
            mlsSql = mlsSql + " INNER JOIN Artists ON Titles.ArtistID = Artists.ArtistID  ";
            mlsSql = mlsSql + " LEFT OUTER JOIN tbGenre ON Titles.GenreId = tbGenre.GenreId  ";
            mlsSql = mlsSql + " where tbDealerContent.dfClientid= " + Convert.ToInt32(cmbCustomer.SelectedValue) + " and tbDealerContent.ContentType= '" + ContentType + "' order by Titles.Title ";

            dtDetail = objMainClass.fnFillDataTable(mlsSql);
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

                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["genre"].Value = dtDetail.Rows[iCtr]["genre"];

                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Tempo"].Value = dtDetail.Rows[iCtr]["Tempo"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["year"].Value = dtDetail.Rows[iCtr]["TitleYear"];
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Category"].Value = dtDetail.Rows[iCtr]["Category"];

                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["songname"].Style.Font = new Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Length"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Album"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Artist"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Genre"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Tempo"].Style.Font = new Font("Segoe UI", 8, System.Drawing.FontStyle.Regular);
                    dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Category"].Style.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                }
            }
            
        }

        private void rdoCopyright_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCopyright.Checked == true)
            {
                PopulateInputFileTypeDetail(dgPlaylist);
            }
        }

        private void rdoRoyalty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRoyalty.Checked == true)
            {
                PopulateInputFileTypeDetail(dgPlaylist);
            }
        }

        private void rdoVideoType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoVideoType.Checked == true)
            {
                PopulateInputFileTypeDetail(dgPlaylist);
            }
        }

        private void picDeleteSongs_Click(object sender, EventArgs e)
        {
            try
            {
                string ContentType = "";
                if (rdoCopyright.Checked == true)
                {
                    ContentType = "CR";
                }
                if (rdoRoyalty.Checked == true)
                {
                    ContentType = "RF";
                }
                if (rdoVideoType.Checked == true)
                {
                    ContentType = "Video";
                }
                if (dgPlaylist.CurrentCell.RowIndex == -1) return;
                DialogResult rlt;
                rlt = MessageBox.Show("Are you sure to delete selected song?", "Management Panel", MessageBoxButtons.YesNo);
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
                            cmd.CommandText = "delete from tbDealerContent where TitleID =" + dgPlaylist.Rows[i].Cells[0].Value + " and dfclientid= " + Convert.ToInt32(cmbCustomer.SelectedValue) + " and ContentType='" + ContentType + "'";
                            cmd.ExecuteNonQuery();
                            StaticClass.constr.Close();
                        }
                    }
                    PopulateInputFileTypeDetail(dgPlaylist);
                }
            }
            catch (Exception ex) { }
        }
    }
}
