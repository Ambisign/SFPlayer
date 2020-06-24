using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace ManagementPanel
{
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>ma
        [STAThread]
        static void Main()
        {
            gblClass objMainClass = new gblClass();
            //if (objMainClass.CheckForInternetConnection() == false)  
            //{
            //    MessageBox.Show("Please check your Internet connection.", "music player");
            //    return;
            //}



            StaticClass.constr = new SqlConnection("Data Source=134.119.178.26;database=onlinedb;uid=sa;password=Jan@Server007;Connect Timeout=5000");
            //StaticClass.constr = new SqlConnection("Data Source=85.195.82.94;database=AlenkaMyClaud;uid=sa;password=phoh7Aiheeki");
            //dbTostTonicAlenka
            //StaticClass.constrOnline = new SqlConnection("Data Source=184.168.194.68;database=Eu4yadmin;uid=chandan;password=chandan@123");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmUserlogin());
        }
    }
}
  
