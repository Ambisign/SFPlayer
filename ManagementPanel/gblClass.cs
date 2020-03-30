using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ManagementPanel
{
    class gblClass
    {
        public static string MusicType = "";
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
        public DataSet fnFillDataSet(string sQuery)
        {
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataSet mlds;
            try
            {
                Adp = new SqlDataAdapter(sQuery, StaticClass.constr);
                mlds = new DataSet();
                Adp.Fill(mlds);
            }
            catch (Exception ex)
            {
                mlds = new DataSet();
                // MessageBox.Show(ex.Message);

            }
            return mlds;
        }
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void fnFillComboBox(string mlsSql, ComboBox Combo, string ValMember, string DispMember, string displayTextAtZeroIndex = "")
        {
            try
            {
                DataSet ds = new DataSet();
                // Warning!!! Optional parameters not supported
                DataRow dr;
                ds = fnFillDataSet(mlsSql);
              //  ds.Tables[0].DefaultView.Sort = DispMember;
                Combo.DataSource = null;
                Combo.Refresh();
                if ((ds.Tables[0].Rows.Count > 0))
                {
                    dr = ds.Tables[0].NewRow();
                    dr[ValMember] = 0;
                    dr[DispMember] = displayTextAtZeroIndex;
                    ds.Tables[0].Rows.InsertAt(dr, 0);
                    Combo.ValueMember = ValMember;
                    Combo.DisplayMember = DispMember;
                    Combo.DataSource = ds.Tables[0];
                    Combo.Refresh();
                    Combo.SelectedValue = 0;
                    
                }
                else
                {
                    dr = ds.Tables[0].NewRow();
                    dr[ValMember] = 0;
                    dr[DispMember] = "";
                    ds.Tables[0].Rows.InsertAt(dr, 0);
                    Combo.ValueMember = ValMember;
                    Combo.DisplayMember = DispMember;
                    Combo.DataSource = ds.Tables[0];
                    Combo.Refresh();
                    Combo.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        public void fnFillComboBoxSpl(string mlsSql, ComboBox Combo, string ValMember, string DispMember, string displayTextAtZeroIndex = "")
        {
            try
            {
                DataSet ds = new DataSet();
                // Warning!!! Optional parameters not supported
                DataRow dr;
                ds = fnFillDataSet(mlsSql);
                // ds.Tables[0].DefaultView.Sort = DispMember;
                Combo.DataSource = null;
                if ((ds.Tables[0].Rows.Count > 0))
                {
                    //dr = ds.Tables[0].NewRow();
                    //dr[ValMember] = 0;
                    //dr[DispMember] = displayTextAtZeroIndex;
                    // ds.Tables[0].Rows.InsertAt(dr, 0);
                    Combo.ValueMember = ValMember;
                    Combo.DisplayMember = DispMember;
                    Combo.DataSource = ds.Tables[0];
                    Combo.Refresh();
                    //Combo.SelectedValue = 0;
                }
                else
                {
                    dr = ds.Tables[0].NewRow();
                    dr[ValMember] = 0;
                    dr[DispMember] = "";
                    ds.Tables[0].Rows.InsertAt(dr, 0);
                    Combo.ValueMember = ValMember;
                    Combo.DisplayMember = DispMember;
                    Combo.DataSource = ds.Tables[0];
                    Combo.Refresh();
                    Combo.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

         
       
       
       
    }

}
