using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpdateNativePlayerCopyright
{
    public partial class frmTitles : Form
    {
        OleDbConnection LocalCon = new OleDbConnection();
        public frmTitles()
        {
            InitializeComponent();
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            DataTable dtDetail = new DataTable();
            string mlsSql = "";
            try
            {
                Process[] prs = Process.GetProcesses();
                foreach (Process pr in prs)
                {
                    if (pr.ProcessName == "StoreAndForwardPlayer")
                        pr.Kill();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message + " --- ");
            }
            
            LocalCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\db.mdb;User Id=admin;Password=;";
            SqlConnection constr = new SqlConnection("Data Source=85.195.82.94;database=dbVideoAdvt;uid=sa;password=phoh7Aiheeki");
            string str = "";
            string lPath = "";
            string titles = "410068,410222,410248,410249,410252,410299,410303,410320,410357,410383,410389,410417,410433,410435,410497,410502,410524,410600,410610,410775,410801,410856,410862,410877,410965,410969,410985,411033,411039,411060,411080,411088,411130,411133,411136,411150,411156,411169,411170,411182,411186,411226,411277,411287,411300,411357,411512,411519,411537,411607,411643,411682,411689,411696,411699,411718,411719,411770,411907,412010,412054";

            str = "delete from Titles where titleid in(" + titles + ")";
            if (LocalCon.State == ConnectionState.Closed) { LocalCon.Open(); }
            OleDbCommand cmdDel = new OleDbCommand();
            cmdDel.Connection = LocalCon;
            cmdDel.CommandText = str;
            cmdDel.ExecuteNonQuery();


            str = "delete from tbSpecialPlaylists_Titles where titleid in(" + titles + ")";
            if (LocalCon.State == ConnectionState.Closed) { LocalCon.Open(); }
            cmdDel = new OleDbCommand();
            cmdDel.Connection = LocalCon;
            cmdDel.CommandText = str;
            cmdDel.ExecuteNonQuery();


            str = "delete from TitlesInPlaylists where titleid in(" + titles + ")";
            if (LocalCon.State == ConnectionState.Closed) { LocalCon.Open(); }
            cmdDel = new OleDbCommand();
            cmdDel.Connection = LocalCon;
            cmdDel.CommandText = str;
            cmdDel.ExecuteNonQuery();

            try
            {

                int[] a = new int[] { 410068, 410222, 410248, 410249, 410252, 410299, 410303, 410320, 410357, 410383, 410389, 410417, 410433, 410435, 410497, 410502, 410524, 410600, 410610, 410775, 410801, 410856, 410862, 410877, 410965, 410969, 410985, 411033, 411039, 411060, 411080, 411088, 411130, 411133, 411136, 411150, 411156, 411169, 411170, 411182, 411186, 411226, 411277, 411287, 411300, 411357, 411512, 411519, 411537, 411607, 411643, 411682, 411689, 411696, 411699, 411718, 411719, 411770, 411907, 412010, 412054 };
                foreach (int s in a)
                {
                    lPath = Application.StartupPath + "\\" + Convert.ToString(s) + ".sec";
                    if (System.IO.File.Exists(lPath))
                    {
                        File.Delete(lPath);
                    }
                }


                string GetLocalPath = "";
                mlsSql = "select * from tbSpecialPlaylists_Titles";
                dtDetail = fnFillDataTable_Local(mlsSql);
                 if (dtDetail.Rows.Count > 0)
                 {
                     for (int iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                     {
                         GetLocalPath = Application.StartupPath + "\\" + dtDetail.Rows[iCtr]["titleId"] + ".ogg";
                         if (File.Exists(GetLocalPath))
                         {
                             FileInfo fi = new FileInfo(GetLocalPath );
                             if (fi.Length <= 1000000)
                             {
                                 str = "update tbSpecialPlaylists_Titles set isDownload=0 where titleid in(" + dtDetail.Rows[iCtr]["titleId"] + ")";
                                 if (LocalCon.State == ConnectionState.Closed) { LocalCon.Open(); }
                                 cmdDel = new OleDbCommand();
                                 cmdDel.Connection = LocalCon;
                                 cmdDel.CommandText = str;
                                 cmdDel.ExecuteNonQuery();
                                 File.Delete(GetLocalPath);
                             }

                         }
                     }
                 }


                 LocalCon.Close();

                 MessageBox.Show("Process is complete.Please wait player is run automatically.", "Player");

                 string VersionApplicationPath = "";
                 VersionApplicationPath = Application.StartupPath + "\\StoreAndForwardPlayer.exe";
                 System.Diagnostics.Process.Start(VersionApplicationPath);
                Application.Exit();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }
        public DataTable fnFillDataTable_Local(string sSql)
        {
            OleDbDataAdapter Adp = new OleDbDataAdapter();
            DataTable mldData;
            try
            {
                Adp = new OleDbDataAdapter(sSql, LocalCon);
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
    }
}
