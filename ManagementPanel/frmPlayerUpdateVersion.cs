using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Threading;

namespace ManagementPanel
{
    public partial class frmPlayerUpdateVersion : Form
    {
        gblClass objMainClass = new gblClass();
        string FileLocation = "";
        Int32 UpdateId = 0;
        string TempPath = "";
        public frmPlayerUpdateVersion()
        {
            InitializeComponent();
        }
        
         public frmPlayerUpdateVersion(Form callingForm)
        {
            
            InitializeComponent();
        }

         private void btnUnload_Click(object sender, EventArgs e)
         {

             MessageBox.Show("Work in Process", "Player Update");
             return;
         }
        

        private void frmPlayerUpdateVersion_Load(object sender, EventArgs e)
        {
            
            ClearFields();
        }
        private void ClearFields()
        {
            txtVersionNo.Text = "";
            dtpVersionDate.Value = DateTime.Now.Date;
            txtPath.Text = "";
            pBar.Value = 0;
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            OpenDialog.FileName = ""; 
            OpenDialog.DefaultExt = ".exe";
            OpenDialog.Filter = "Application (.exe)|*.exe"; 
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                   txtPath.Text=OpenDialog.FileName;
            } 
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void CreateFile(string NewFileName)
        {
            string fileName = Application.StartupPath + "\\tid.amp";

            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file
                    Byte[] title = new UTF8Encoding(true).GetBytes(NewFileName);
                    fs.Write(title, 0, title.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TempPath = "";
            if (objMainClass.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please check your Internet connection.", "Music Player Update");
                return;
            }
            if (SubmitValidation() == false)
            {
                return;
            }
            panUpload.Visible = true;
            TempPath = txtPath.Text;
            SaveUpdateVersion();
            //UploadNewVersion();
            bgWorker.RunWorkerAsync();
             
            
        }
        private Boolean SubmitValidation()
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Application path cannot be blank", "Music Player Update");
                txtPath.Focus();
                return false;
            }
            else if (cmbMusicType.Text == "")
            {
                MessageBox.Show("Music type cannot be blank", "Music Player Update");
                cmbMusicType.Focus();
                return false;
            }

            return true;
        }
        private void SaveUpdateVersion()
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("InsertPlayerUpdateVersion", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@AviableDate", SqlDbType.DateTime));
            cmd.Parameters["@AviableDate"].Value = dtpVersionDate.Value;

            cmd.Parameters.Add(new SqlParameter("@FileLocation", SqlDbType.VarChar));
            cmd.Parameters["@FileLocation"].Value = txtPath.Text;

            cmd.Parameters.Add(new SqlParameter("@MusicType", SqlDbType.VarChar));
            cmd.Parameters["@MusicType"].Value = cmbMusicType.Text;

            try
            {
                UpdateId = Convert.ToInt32(cmd.ExecuteScalar());
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
        private void UploadNewVersion()
        {
            /////////////// Create Directory ////////////////////////////

            WebRequest request = WebRequest.Create("ftp://81.83.13.236:2112/AMMusicFiles/PlayerUpdate/Ver" + UpdateId + ".0");
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("paras", "paras2014");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
            //////////////////////////////////////////////////////////////

            string filename = TempPath;
            string ftpServerIP = "81.83.13.236:2112/AMMusicFiles/PlayerUpdate/Ver" + UpdateId + ".0";
            string ftpUserName = "paras";
            string ftpPassword = "paras2014";

            FileInfo objFile = new FileInfo(filename);
            FtpWebRequest objFTPRequest;
            FileLocation = "ftp://" + ftpServerIP + "/" + objFile.Name;
            objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));
            objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            objFTPRequest.KeepAlive = false;
            objFTPRequest.UseBinary = true;
            objFTPRequest.ContentLength = objFile.Length;
            objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;
            int intBufferLength = 16 * 1024;

            byte[] objBuffer = new byte[intBufferLength];
            FileStream objFileStream = objFile.OpenRead();
            try
            {
                Stream objStream = objFTPRequest.GetRequestStream();
                int len = 0;
                int pBarValue = 0;
                pBar.Maximum = Convert.ToInt32(objFile.Length);
                while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
                {
                    pBarValue = pBarValue + 1000;
                    objStream.Write(objBuffer, 0, len);
                    //pBar.Value = pBarValue;
                }
                objStream.Close();
                objFileStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateFileLocation()
        {
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("UpdatePlayerFileLocation", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UpdateId", SqlDbType.BigInt));
            cmd.Parameters["@UpdateId"].Value = UpdateId;

            cmd.Parameters.Add(new SqlParameter("@FileLocation", SqlDbType.VarChar));
            cmd.Parameters["@FileLocation"].Value = FileLocation;
            try
            {
                UpdateId = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            /////////////// Create Directory ////////////////////////////
            string DirPath = "ftp://37.61.214.210:21/AMMusicFiles/PlayerUpdate/Ver" + UpdateId + ".0";
            FtpWebRequest request ;
            request = (FtpWebRequest)FtpWebRequest.Create(new Uri(DirPath));

            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("ftpTalwinder", "Roop!@#123");
            request.KeepAlive = true;
            request.UseBinary = true;
            request.UsePassive = false;
            

            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
            //////////////////////////////////////////////////////////////

            string filename = TempPath;
            string ftpServerIP = "37.61.214.210:21/AMMusicFiles/PlayerUpdate/Ver" + UpdateId + ".0";
            string ftpUserName = "ftpTalwinder";
            string ftpPassword = "Roop!@#123";

            FileInfo objFile = new FileInfo(filename);
            FtpWebRequest objFTPRequest;
            FileLocation = "ftp://" + ftpServerIP + "/" + objFile.Name;
            objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));
            objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            objFTPRequest.KeepAlive = true;
            objFTPRequest.UseBinary = true;
            objFTPRequest.UsePassive = false;

            objFTPRequest.ContentLength = objFile.Length;
            objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;
            int intBufferLength = 16 * 1024;

            int pBarLength =1024;

            byte[] objBuffer = new byte[pBarLength];
            FileStream objFileStream = objFile.OpenRead();
            try
            {
                Stream objStream = objFTPRequest.GetRequestStream();
                int len = objFileStream.Read(objBuffer, 0, pBarLength);

                while (len != 0)
                {
                    objStream.Write(objBuffer, 0, len);
                    len = objFileStream.Read(objBuffer, 0, pBarLength);

                    double dIndex = (double)(len);
                    double dTotal = (double)pBarLength;
                    double dProgressPercentage = (dIndex / dTotal);
                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                    bgWorker.ReportProgress(iProgressPercentage);
                }
                objStream.Close();
                objFileStream.Close();
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
            
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();

            if (bgWorker.IsBusy == false)
            {
                UpdateFileLocation();
                panUpload.Visible = false;
                ClearFields();
               MessageBox.Show("Application is saved", "Music Player Update");
            }
        
        }
        

    }
}
