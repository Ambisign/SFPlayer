using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Diagnostics;
using NetFwTypeLib;
namespace UpdateNativePlayerCopyright
{
    public partial class frmUpdateMusicPlayer : Form
    {
        
        string FileLocation = "";
        SqlConnection constr;
        public frmUpdateMusicPlayer()
        {
            InitializeComponent();
        }

        private void frmUpdateMusicPlayer_SizeChanged(object sender, EventArgs e)
        {
           // panMain.Location = new Point(
           //this.panelMain.Width / 2 - panMain.Size.Width / 2,
           //this.panelMain.Height / 2 - panMain.Size.Height / 2);
            panMain.Width = this.panelMain.Width-20;
        }
        private void GetNewVersion()
        {
            string strUpdateVersion = "";
            DataTable dtUpdateVersion = new DataTable();
            constr = new SqlConnection("Data Source=134.119.178.26;database=OnlineDB;uid=sa;password=Jan@Server007");
            strUpdateVersion = "select * from tbPlayerUpdateVersion where UpdateId in(select MAX(UpdateId) from tbPlayerUpdateVersion where musictype='NativeCR')  and musictype='NativeCR' ";
            dtUpdateVersion = fnFillDataTable(strUpdateVersion);
            if (dtUpdateVersion.Rows.Count > 0)
            {
                FileLocation = dtUpdateVersion.Rows[0]["FileLocation"].ToString();
            }
            bgWorker.RunWorkerAsync();
        }
        public DataTable fnFillDataTable(string sSql)
        {
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable mldData;
            try
            {
                Adp = new SqlDataAdapter(sSql, constr);
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
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the object used to communicate with the server.
            string localPath = Application.StartupPath + "\\StoreAndForwardPlayer.exe";
            try
            {

                FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(FileLocation);
                requestFileDownload.Credentials = new NetworkCredential("FtpParas", "moh!@#123");
                //requestFileDownload.KeepAlive = true;
                requestFileDownload.UsePassive = false;
                //requestFileDownload.UseBinary = true;
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                Stream responseStream = responseFileDownload.GetResponseStream();
                FileStream writeStream = new FileStream(localPath, FileMode.Create);


                FtpWebRequest requestFileSize = (FtpWebRequest)WebRequest.Create(FileLocation);
                requestFileSize.Credentials = new NetworkCredential("FtpParas", "moh!@#123");
                requestFileSize.UsePassive = false;
                requestFileSize.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)requestFileSize.GetResponse();
                Int64 Length = response.ContentLength;
                Byte[] byteBuffer = new Byte[Length];
                int iByteSize = 0;
                Int64 iRunningByteTotal = 0;
                while ((iByteSize = responseStream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    writeStream.Write(byteBuffer, 0, iByteSize);
                    iRunningByteTotal += iByteSize;
                    double dIndex = (double)(iRunningByteTotal);
                    double dTotal = (double)byteBuffer.Length;
                    double dProgressPercentage = (dIndex / dTotal);
                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                    bgWorker.ReportProgress(iProgressPercentage);

                }
                responseStream.Close();
                writeStream.Close();

                requestFileDownload = null;
                responseFileDownload = null;
            }
            catch
            {
                 
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblPercentage.Text = e.ProgressPercentage + "%";
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string filename = Application.StartupPath + "\\tid.amp";
            string textline = "";
            string strUpdateVersion = "";
            Int64 UpdateVersion = 0;
            DataTable dtUpdateVersion = new DataTable();
            try
            {
                GC.Collect();
                if (File.Exists(filename))
                {
                    System.IO.StreamReader objReader;
                    objReader = new System.IO.StreamReader(filename);
                    do
                    {
                        textline = textline + objReader.ReadLine();
                    } while (objReader.Peek() != -1);
                    objReader.Close();
                }

                strUpdateVersion = "select * from tbPlayerUpdateVersion where UpdateId in(select MAX(UpdateId) from tbPlayerUpdateVersion where musictype='NativeCR') and musictype='NativeCR'";
                dtUpdateVersion = fnFillDataTable(strUpdateVersion);
                UpdateVersion = Convert.ToInt32(dtUpdateVersion.Rows[0]["UpdateId"]);

                if (constr.State == ConnectionState.Open) constr.Close();
                constr.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = constr;
                cmd.CommandText = "update AMPlayerTokens set IsUpdated = " + UpdateVersion + " where tokenid=" + textline;
                cmd.ExecuteNonQuery();
                constr.Close();

                string VersionApplicationPath = "";
                VersionApplicationPath = Application.StartupPath + "\\StoreAndForwardPlayer.exe";
                System.Diagnostics.Process.Start(VersionApplicationPath);
                Application.Exit();
            }
            catch (Exception ex)
            {
                Application.Exit();
            }

        }

        private void frmUpdateMusicPlayer_Load(object sender, EventArgs e)
        {
            Process[] prs = Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "StoreAndForwardPlayer")
                    pr.Kill();
            }
            GetNewVersion();
        }

        private void frmUpdateMusicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (bgWorker.IsBusy == true)
                    {
                        MessageBox.Show("Please wait music player is updating", "Native Player Updating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }
                    e.Cancel = false;
                    string VersionApplicationPath = "";
                    VersionApplicationPath = Application.StartupPath + "\\StoreAndForwardPlayer.exe";
                    System.Diagnostics.Process.Start(VersionApplicationPath);
                    Application.Exit();
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Process[] prs = Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "StoreAndForwardPlayer")
                    pr.Kill();
            }
            //GetNewVersion();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

      
    }
}
