using NAudio.Wave;
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
    public partial class frmImportExcel : Form
    {
        string mType = "";
        string mediaType = "";
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public frmImportExcel()
        {
            InitializeComponent();
        }

        private void frmImportExcel_Load(object sender, EventArgs e)
        {

        }

        private void picBrowse_Click(object sender, EventArgs e)
        {
            ofdExcel.Filter = "Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls";
            if (ofdExcel.ShowDialog() == DialogResult.OK)
            {
                txtExcelFilePath.Text = ofdExcel.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if (rdoAudio.Checked == true)
            {
                mType = ".mp3";
                mediaType = "Audio";
            }
            if (rdoVideo.Checked == true)
            {
                mType = ".mp4";
                mediaType = "Video";
            }
           

            if (StaticClass.constr.State == ConnectionState.Open) { StaticClass.constr.Close(); }
            StaticClass.constr.Open();
            SqlCommand cmdOnline = new SqlCommand();
            cmdOnline.Connection = StaticClass.constr;
            cmdOnline.CommandType = CommandType.Text;
            cmdOnline.CommandText = "delete from tbAlenkaMedia";
            cmdOnline.ExecuteNonQuery();


            if (txtExcelFilePath.Text == "")
            {
                MessageBox.Show("Please select a excel file", "Direct Upload");
                picBrowse.Focus();
                return;
            }
            if (txtMp3Path.Text == "")
            {
                MessageBox.Show("Please select a mp3 songs path", "Direct Upload");
                picBrowseMp3.Focus();
                return;
            }
            if (txtSavePath.Text == "")
            {
                MessageBox.Show("Please select a song save path", "Direct Upload");
                picBrowseSave.Focus();
                return;
            }

            #region Import Excel
            string filePath = txtExcelFilePath.Text;
            string extension = Path.GetExtension(filePath);
            string header = "YES";
            string conStr, sheetName;
            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();
                        dgExcel.DataSource = dt;

                        dgExcel.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgExcel.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgExcel.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgExcel.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgExcel.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgExcel.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgExcel.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    }
                }
            }
            #endregion

            if (dgExcel.ColumnCount != 15)
            {
               
                MessageBox.Show("Selected excel file is not a correct file", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[0].Name.ToLower() != "title")
            {
                MessageBox.Show("Title column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[1].Name.ToLower() != "album")
            {
                MessageBox.Show("Album column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[2].Name.ToLower() != "artist")
            {
                MessageBox.Show("Artist column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[3].Name.ToLower() != "time")
            {
                MessageBox.Show("Time column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[4].Name.ToLower() != "genre")
            {
                MessageBox.Show("Genre column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[5].Name.ToLower() != "tempo")
            {
                MessageBox.Show("Tempo column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[6].Name.ToLower() != "filename")
            {
                MessageBox.Show("Filename column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[7].Name.ToLower() != "year")
            {
                MessageBox.Show("year column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[8].Name.ToLower() != "category")
            {
                MessageBox.Show("category column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[9].Name.ToLower() != "language")
            {
                MessageBox.Show("Language column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[10].Name.ToLower() != "isroyaltyfree")
            {
                MessageBox.Show("isRoyaltyfree column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[11].Name.ToLower() != "isrc")
            {
                MessageBox.Show("ISRC column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[12].Name.ToLower() != "label")
            {
                MessageBox.Show("Label column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[13].Name.ToLower() != "isexplicit")
            {
                MessageBox.Show("IsExplicit column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }
            if (dgExcel.Columns[14].Name.ToLower() != "country")
            {
                MessageBox.Show("Country column is not match with sequence", "Direct Upload");
                dgExcel.DataSource = null;
                return;
            }

            panControls.Enabled = false;
            panPopUp.Visible = true;
            lblName.Text = "Copy excel sheet in database";
            bgWorker.RunWorkerAsync();


             


            


        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Int64 iSize = dgExcel.Rows.Count + 1;
            Int64 iRunningByteTotal = 0;
            foreach (DataGridViewRow row in dgExcel.Rows)
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO tbAlenkaMedia(Title, Album, Artist,Genre, Tempo, Filename,Year,time,category,language,isRoyaltyfree,isrc,label,IsExplicit,Country) VALUES(@Title, @Album, @Artist,@Genre, @Tempo, @Filename,@Year,@time,@category,@language,@isRoyaltyfree,@isrc,@label,@IsExplicit,@Country)", StaticClass.constr))
                {

                    byte[] byteBuffer = new byte[iSize];
                    if ((row.Cells[0].Value.ToString() != "") && (row.Cells[1].Value.ToString() != "") && (row.Cells[2].Value.ToString() != ""))
                    {
                        cmd.Parameters.AddWithValue("@Title", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@Album", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@Artist", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@Genre", row.Cells[4].Value);
                        cmd.Parameters.AddWithValue("@Tempo", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@Filename", row.Cells[6].Value);
                        cmd.Parameters.AddWithValue("@Year", row.Cells[7].Value);
                        cmd.Parameters.AddWithValue("@time", row.Cells[3].Value);
                        cmd.Parameters.AddWithValue("@category", row.Cells[8].Value);
                        cmd.Parameters.AddWithValue("@language", row.Cells[9].Value);
                        cmd.Parameters.AddWithValue("@isRoyaltyfree", row.Cells[10].Value);
                        cmd.Parameters.AddWithValue("@isrc", row.Cells[11].Value);

                        cmd.Parameters.AddWithValue("@label", row.Cells[12].Value);
                        cmd.Parameters.AddWithValue("@IsExplicit", row.Cells[13].Value);
                        cmd.Parameters.AddWithValue("@Country", row.Cells[14].Value);
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

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pBar.Value = 0;
            DataTable dtDetail = new DataTable();
            string str = "select count(*) from tbAlenkaMedia ";
            dtDetail = fnFillDataTable(str);
            if ((dtDetail.Rows.Count > 0))
            {
                iSize_M = Convert.ToInt32(dtDetail.Rows[0][0].ToString());
            }
            panPopUp.Visible = true;
            lblName.Text = "Finding songs and implement song id";
          bgWorkerStep2.RunWorkerAsync();
        }
        private void Step2()
        {
            try
            {
                string str = "";
                Int32 GenreId = 0;

                Int64 iRunningByteTotal = 0;
                DataTable dtGenre = new DataTable();
                DataTable dtGenreId = new DataTable();
                DataTable dtDetail = new DataTable();
                //DataTable dtTitleMaxId = new DataTable();
                str = "select distinct genre from tbAlenkaMedia";
                dtGenre = fnFillDataTable(str);
               // MessageBox.Show(dtGenre.Rows.Count.ToString() + " 1");
                if (dtGenre.Rows.Count > 0)
                {
                    for (int iGen = 0; (iGen <= (dtGenre.Rows.Count - 1)); iGen++)
                    {

                        str = "select * from tbGenre where genre ='" + dtGenre.Rows[iGen]["genre"].ToString().Trim() + "' ";
                        dtGenreId = fnFillDataTable(str);
                        if (dtGenreId.Rows.Count > 0)
                        {
                            GenreId = Convert.ToInt32(dtGenreId.Rows[0]["GenreId"].ToString());
                        }
                        else
                        {
                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmdGenre = new SqlCommand("spInsertAlenkaGenre", StaticClass.constr);
                            cmdGenre.CommandType = CommandType.StoredProcedure;
                            cmdGenre.Parameters.Add(new SqlParameter("@genre", SqlDbType.VarChar));
                            cmdGenre.Parameters["@genre"].Value = dtGenre.Rows[iGen]["genre"].ToString().Trim();
                            GenreId = Convert.ToInt32(cmdGenre.ExecuteScalar());
                        }

                      //  MessageBox.Show(GenreId.ToString() + " 2");
                        str = "select *,isnull(tempo,0) as aTempo,isnull(year,0) as ayear, isnull(Category,'') as aCategory from tbAlenkaMedia where genre='" + dtGenre.Rows[iGen]["genre"].ToString().Trim() + "'";
                        dtDetail = fnFillDataTable(str);
                       // MessageBox.Show(dtDetail.Rows.Count.ToString() + " 3");
                        if ((dtDetail.Rows.Count > 0))
                        {
                            for (int iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                            {
                                byte[] byteBuffer = new byte[iSize_M];
                                FileName = "";
                                LocalFileName = "";

                                FileName = dtDetail.Rows[iCtr]["filename"].ToString();

                                bool IsMp3Find = FileName.EndsWith(mType, System.StringComparison.CurrentCultureIgnoreCase);

                                if (IsMp3Find == false)
                                {
                                    LocalFileName = txtMp3Path.Text + "\\" + dtDetail.Rows[iCtr]["filename"].ToString() + mType;
                                }
                                else
                                {
                                    LocalFileName = txtMp3Path.Text + "\\" + dtDetail.Rows[iCtr]["filename"].ToString();
                                }
                               // MessageBox.Show(LocalFileName);
                                if (System.IO.File.Exists(LocalFileName))
                                {
                                    #region Save songs in table
                                    str = "select isnull(max(titleid),0)+1 as Tit_Id from titles";
                                    //  dtTitleMaxId = fnFillDataTable(str);

                                    reader = new Mp3FileReader(LocalFileName);
                                    TimeSpan duration = reader.TotalTime;

                                    Int32 Title_Id = 0;
                                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                                    StaticClass.constr.Open();
                                    SqlCommand cmd = new SqlCommand("InsertArtistsAlbumsTitles_AlenkaMedia", StaticClass.constr);
                                    cmd.CommandType = CommandType.StoredProcedure;



                                    cmd.Parameters.Add(new SqlParameter("@TiTleTiTle", SqlDbType.VarChar));
                                    cmd.Parameters["@TiTleTiTle"].Value = dtDetail.Rows[iCtr]["title"].ToString();

                                    cmd.Parameters.Add(new SqlParameter("@TitleArtistName", SqlDbType.VarChar));
                                    cmd.Parameters["@TitleArtistName"].Value = dtDetail.Rows[iCtr]["Artist"].ToString();

                                    cmd.Parameters.Add(new SqlParameter("@AlbumName", SqlDbType.VarChar));
                                    cmd.Parameters["@AlbumName"].Value = dtDetail.Rows[iCtr]["Album"].ToString();

                                    cmd.Parameters.Add(new SqlParameter("@titlecategoryid", SqlDbType.BigInt));
                                    cmd.Parameters["@titlecategoryid"].Value = Convert.ToInt32("4");

                                    cmd.Parameters.Add(new SqlParameter("@titleSubcategoryid", SqlDbType.VarChar));
                                    cmd.Parameters["@titleSubcategoryid"].Value = "22";
                                    
                                    cmd.Parameters.Add(new SqlParameter("@Time", SqlDbType.VarChar));
                                    cmd.Parameters["@Time"].Value =string.Format("{0:hh:mm:ss}", dtDetail.Rows[iCtr]["time"].ToString());

                                    cmd.Parameters.Add(new SqlParameter("@AlbumLabel", SqlDbType.VarChar));
                                    cmd.Parameters["@AlbumLabel"].Value = "0";

                                    cmd.Parameters.Add(new SqlParameter("@CatalogCode", SqlDbType.VarChar));
                                    cmd.Parameters["@CatalogCode"].Value = "0";

                                    cmd.Parameters.Add(new SqlParameter("@titleYear", SqlDbType.Int));
                                    cmd.Parameters["@titleYear"].Value = dtDetail.Rows[iCtr]["ayear"];

                                    cmd.Parameters.Add(new SqlParameter("@GenreId", SqlDbType.Int));
                                    cmd.Parameters["@GenreId"].Value = GenreId;

                                    cmd.Parameters.Add(new SqlParameter("@tempo", SqlDbType.VarChar));
                                    cmd.Parameters["@tempo"].Value = dtDetail.Rows[iCtr]["aTempo"];


                                    cmd.Parameters.Add(new SqlParameter("@acategory", SqlDbType.VarChar));
                                    cmd.Parameters["@acategory"].Value = dtDetail.Rows[iCtr]["aCategory"];


                                    Title_Id = Convert.ToInt32(cmd.ExecuteScalar());
                                    //  MessageBox.Show(Title_Id.ToString());
                                    reader.Close();

                                    if (Title_Id != 0)
                                    {
                                        string OldName = "";
                                        if (IsMp3Find == false)
                                        {
                                            OldName = txtMp3Path.Text + "\\" + FileName + mType;
                                        }
                                        else
                                        {
                                            OldName = txtMp3Path.Text + "\\" + FileName;
                                        }
                                        string NewName = txtSavePath.Text + "\\" + Title_Id.ToString() + mType;
                                        System.IO.File.Move(OldName, NewName);
                                        StaticClass.constr.Close();

                                        //if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                                        //StaticClass.constr.Open();
                                        //cmd = new SqlCommand();
                                        //cmd.Connection = StaticClass.constr;
                                        //cmd.CommandText = "update titles set GenreId =" + GenreId + " where titleid=" + Title_Id + "";
                                        //cmd.ExecuteNonQuery();
                                        //StaticClass.constr.Close();
                                    }
                                    #endregion
                                }

                                iRunningByteTotal = iRunningByteTotal + 1;
                                double dIndex = (double)(iRunningByteTotal);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage = (int)(dProgressPercentage * 100);
                                //bgWorkerStep2.ReportProgress(iProgressPercentage);

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   -------  " + FileName);
            }
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
            lblPercentage.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void picBrowseMp3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtMp3Path.Text = fbd.SelectedPath;
            }
        }

        private void picBrowseSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSavePath.Text = fbd.SelectedPath;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgExcel.DataSource = null;
            txtExcelFilePath.Text = "";
            txtMp3Path.Text = "";
            txtSavePath.Text = "";
            panPopUp.Visible = false;
            pBar.Value = 0;
        }
        string FileName = "";
        string LocalFileName = "";
        Mp3FileReader reader;
        Int64 iSize_M = 0;
        private void bgWorkerStep2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string str = "";
                Int32 GenreId = 0;

                Int64 iRunningByteTotal = 0;
                DataTable dtGenre = new DataTable();
                DataTable dtGenreId = new DataTable();
                DataTable dtDetail = new DataTable();
                //DataTable dtTitleMaxId = new DataTable();
                str = "select distinct genre from tbAlenkaMedia";
                dtGenre = fnFillDataTable(str);
               // MessageBox.Show(dtGenre.Rows.Count.ToString() + " 1");
                if (dtGenre.Rows.Count > 0)
                {
                    for (int iGen = 0; (iGen <= (dtGenre.Rows.Count - 1)); iGen++)
                    {

                        str = "select * from tbGenre where genre ='" + dtGenre.Rows[iGen]["genre"].ToString().Trim() + "' ";
                        dtGenreId = fnFillDataTable(str);
                        if (dtGenreId.Rows.Count > 0)
                        {
                            GenreId = Convert.ToInt32(dtGenreId.Rows[0]["GenreId"].ToString());
                        }
                        else
                        {
                            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                            StaticClass.constr.Open();
                            SqlCommand cmdGenre = new SqlCommand("spInsertAlenkaGenre", StaticClass.constr);
                            cmdGenre.CommandType = CommandType.StoredProcedure;
                            cmdGenre.Parameters.Add(new SqlParameter("@genre", SqlDbType.VarChar));
                            cmdGenre.Parameters["@genre"].Value = dtGenre.Rows[iGen]["genre"].ToString().Trim();
                            GenreId = Convert.ToInt32(cmdGenre.ExecuteScalar());
                        }

                      //  MessageBox.Show(GenreId.ToString() + " 2");
                        str = "select *,isnull(tempo,0) as aTempo,isnull(year,0) as ayear, isnull(Category,'') as aCategory , isnull(language,'') as lang, isnull(isRoyaltyfree,0) as isRF, isnull(isrc,'') as isr, 4 as  titlecategoryid,22 as titleSubcategoryid, isnull(label,'') as lbel, isnull(country,'') as ctry from tbAlenkaMedia where genre='" + dtGenre.Rows[iGen]["genre"].ToString().Trim() + "'";
                        dtDetail = fnFillDataTable(str);
                       // MessageBox.Show(dtDetail.Rows.Count.ToString() + " 3");
                        if ((dtDetail.Rows.Count > 0))
                        {
                            for (int iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                            {
                                byte[] byteBuffer = new byte[iSize_M];
                                FileName = "";
                                LocalFileName = "";

                                FileName = dtDetail.Rows[iCtr]["filename"].ToString();

                                bool IsMp3Find = FileName.EndsWith(mType, System.StringComparison.CurrentCultureIgnoreCase);

                                if (IsMp3Find == false)
                                {
                                    LocalFileName = txtMp3Path.Text + "\\" + dtDetail.Rows[iCtr]["filename"].ToString() + mType;
                                }
                                else
                                {
                                    LocalFileName = txtMp3Path.Text + "\\" + dtDetail.Rows[iCtr]["filename"].ToString();
                                }
                              //  MessageBox.Show(LocalFileName);
                                if (System.IO.File.Exists(LocalFileName))
                                {
                                    long fileSize = new FileInfo(LocalFileName).Length;
                                    #region Save songs in table
                                    str = "select isnull(max(titleid),0)+1 as Tit_Id from titles";
                                    //  dtTitleMaxId = fnFillDataTable(str);

                                    //reader = new Mp3FileReader(LocalFileName);
                                   // TimeSpan duration = reader.TotalTime;

                                    Int32 Title_Id = 0;
                                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                                    StaticClass.constr.Open();
                                    SqlCommand cmd = new SqlCommand("InsertArtistsAlbumsTitles_AlenkaMedia", StaticClass.constr);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    
                                    cmd.Parameters.Add(new SqlParameter("@TiTleTiTle", SqlDbType.NVarChar));
                                    cmd.Parameters["@TiTleTiTle"].Value = dtDetail.Rows[iCtr]["title"].ToString();
                                     

                                    
                                    cmd.Parameters.Add(new SqlParameter("@TitleArtistName", SqlDbType.NVarChar));
                                    cmd.Parameters["@TitleArtistName"].Value = dtDetail.Rows[iCtr]["Artist"].ToString();
                                    var k2 = dtDetail.Rows[iCtr]["Artist"].ToString();

                                    cmd.Parameters.Add(new SqlParameter("@AlbumName", SqlDbType.NVarChar));
                                    cmd.Parameters["@AlbumName"].Value = dtDetail.Rows[iCtr]["Album"].ToString();

                                    cmd.Parameters.Add(new SqlParameter("@titlecategoryid", SqlDbType.BigInt));
                                    cmd.Parameters["@titlecategoryid"].Value = Convert.ToInt32(dtDetail.Rows[iCtr]["titlecategoryid"].ToString());

                                    cmd.Parameters.Add(new SqlParameter("@titleSubcategoryid", SqlDbType.VarChar));
                                    cmd.Parameters["@titleSubcategoryid"].Value = dtDetail.Rows[iCtr]["titleSubcategoryid"].ToString();
                                    
                                    cmd.Parameters.Add(new SqlParameter("@Time", SqlDbType.NVarChar));
                                    cmd.Parameters["@Time"].Value ="00:" + string.Format("{0:mm:ss}", dtDetail.Rows[iCtr]["time"]);

                                    cmd.Parameters.Add(new SqlParameter("@AlbumLabel", SqlDbType.VarChar));
                                    cmd.Parameters["@AlbumLabel"].Value = "0";

                                    cmd.Parameters.Add(new SqlParameter("@CatalogCode", SqlDbType.VarChar));
                                    cmd.Parameters["@CatalogCode"].Value = "0";

                                    cmd.Parameters.Add(new SqlParameter("@titleYear", SqlDbType.Int));
                                    cmd.Parameters["@titleYear"].Value = dtDetail.Rows[iCtr]["ayear"];

                                    cmd.Parameters.Add(new SqlParameter("@GenreId", SqlDbType.Int));
                                    cmd.Parameters["@GenreId"].Value = GenreId;

                                    cmd.Parameters.Add(new SqlParameter("@tempo", SqlDbType.VarChar));
                                    cmd.Parameters["@tempo"].Value = dtDetail.Rows[iCtr]["aTempo"];

                                    
                                    cmd.Parameters.Add(new SqlParameter("@mType", SqlDbType.VarChar));
                                    cmd.Parameters["@mType"].Value = mediaType;

                                    cmd.Parameters.Add(new SqlParameter("@acategory", SqlDbType.NVarChar));
                                    cmd.Parameters["@acategory"].Value = dtDetail.Rows[iCtr]["aCategory"];

                                    cmd.Parameters.Add(new SqlParameter("@language", SqlDbType.VarChar));
                                    cmd.Parameters["@language"].Value = dtDetail.Rows[iCtr]["lang"];

                                    cmd.Parameters.Add(new SqlParameter("@isRF", SqlDbType.VarChar));
                                    cmd.Parameters["@isRF"].Value = dtDetail.Rows[iCtr]["isRF"];

                                    cmd.Parameters.Add(new SqlParameter("@isrc", SqlDbType.VarChar));
                                    cmd.Parameters["@isrc"].Value = dtDetail.Rows[iCtr]["isr"];

                                    cmd.Parameters.Add(new SqlParameter("@FileSize", SqlDbType.VarChar));
                                    cmd.Parameters["@FileSize"].Value = fileSize.ToString();
                                     
                                    cmd.Parameters.Add(new SqlParameter("@dfclientid", SqlDbType.BigInt));
                                    cmd.Parameters["@dfclientid"].Value = "0";
                                    cmd.Parameters.Add(new SqlParameter("@folderId", SqlDbType.Int));
                                    cmd.Parameters["@folderId"].Value = "0";
                                    cmd.Parameters.Add(new SqlParameter("@AssetID_WeMix", SqlDbType.VarChar));
                                    cmd.Parameters["@AssetID_WeMix"].Value = "";
                                    cmd.Parameters.Add(new SqlParameter("@Country_WeMix", SqlDbType.VarChar));
                                    cmd.Parameters["@Country_WeMix"].Value = dtDetail.Rows[iCtr]["ctry"]; 
                                    cmd.Parameters.Add(new SqlParameter("@Explicit", SqlDbType.Int));
                                    cmd.Parameters["@Explicit"].Value = dtDetail.Rows[iCtr]["IsExplicit"];
                                    cmd.Parameters.Add(new SqlParameter("@label", SqlDbType.VarChar));
                                    cmd.Parameters["@label"].Value = dtDetail.Rows[iCtr]["lbel"];
                                    var k1 = dtDetail.Rows[iCtr]["lbel"].ToString();
                                    cmd.Parameters.Add(new SqlParameter("@dbType", SqlDbType.VarChar));
                                    cmd.Parameters["@dbType"].Value = "Both";
                                    Title_Id = Convert.ToInt32(cmd.ExecuteScalar());
                                 //MessageBox.Show(Title_Id.ToString());
                                    //reader.Close();
                                    
                                    if (Title_Id != 0)
                                    {
                                        string OldName = "";
                                        if (IsMp3Find == false)
                                        {
                                            OldName = txtMp3Path.Text + "\\" + FileName + mType;
                                        }
                                        else
                                        {
                                            OldName = txtMp3Path.Text + "\\" + FileName ;
                                        }
                                        string NewName = txtSavePath.Text + "\\" + Title_Id.ToString() + mType;
                                        if (File.Exists(NewName) == false)
                                        {
                                            File.Move(OldName, NewName);
                                        }
                                        else
                                        {
                                            string wt= "C:\\NewDBContent\\Wrong\\" + Title_Id.ToString()+"_"+DateTime.Now.Millisecond.ToString() + mType;
                                        //    MessageBox.Show(OldName.ToString() + Environment.NewLine + wt);
                                            File.Move(OldName, wt);
                                        }
                                        StaticClass.constr.Close();

                                        //if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                                        //StaticClass.constr.Open();
                                        //cmd = new SqlCommand();
                                        //cmd.Connection = StaticClass.constr;
                                        //cmd.CommandText = "update titles set GenreId =" + GenreId + " where titleid=" + Title_Id + "";
                                        //cmd.ExecuteNonQuery();
                                        //StaticClass.constr.Close();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   -------  " + FileName);
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
            panControls.Enabled = true;
            dgExcel.DataSource = null;
            txtExcelFilePath.Text = "";
            txtMp3Path.Text = "";
            txtSavePath.Text = "";
            panPopUp.Visible = false;
            pBar.Value = 0;

        }
        public DataTable fnFillDataTable(string sSql)
        {
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable mldData;
            try
            {
                Adp = new SqlDataAdapter(sSql, StaticClass.constr);
                mldData = new DataTable();
                Adp.Fill(mldData);
            }
            catch (Exception ex)
            {
                mldData = new DataTable();
                // MessageBox.Show(ex.Message);
            }
            return mldData;
        }

        private void frmImportExcel_SizeChanged(object sender, EventArgs e)
        {
            panPopUp.Location = new Point(
           this.Width / 2 - panPopUp.Size.Width / 2,
           this.Height / 2 - panPopUp.Size.Height / 2);
        }

        private void rdoVideo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoAudio_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
