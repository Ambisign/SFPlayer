using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ManagementPanel
{
    public partial class frmChangeAppMiddleImage : Form
    {
        gblClass objMainClass = new gblClass();
        public frmChangeAppMiddleImage()
        {
            InitializeComponent();
            DataTable dtDetail = new DataTable();
            string str = "Select * from  tblMiddleImage_App";
            dtDetail = objMainClass.fnFillDataTable(str);
            picImage.ImageLocation = dtDetail.Rows[0]["imgPath"].ToString();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg;  *.png";
            DialogResult res = OpenDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                picImage.Image = Image.FromFile(OpenDialog.FileName);
                lblPath.Text = OpenDialog.FileName;
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (lblPath.Text == "")
            {
                MessageBox.Show("Please browse your image", "Management Panel");
                btnImage.Focus();
                return;
            }
            btnUploadImage.Enabled = false;
            bgMiddleImage.RunWorkerAsync();
        }
        string MaxId = "";
        private void bgMiddleImage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MaxId = "";
                MaxId = GetMaxId();
                string filename = lblPath.Text;
                FileInfo objFile = new FileInfo(filename);
                //string ftpServerIP = "ftp://146.0.229.66/AppMiddlePic/1" + objFile.Extension;
                //string ftpUserName = "FtpParas";
                //string ftpPassword = "moh!@#123";
                string ftpServerIP = "ftp://85.195.82.94/AMMusicFiles/ripper/AppMiddlePic/" + MaxId + objFile.Extension;
                string ftpUserName = "harish";
                string ftpPassword = "Mohali123";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpServerIP));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                request.UseBinary = true;
                request.UsePassive = false;
                byte[] fileStream = System.IO.File.ReadAllBytes(filename);
                System.IO.Stream requestStream = request.GetRequestStream();
                for (int offset = 0; offset <= fileStream.Length; offset += 1024)
                {
                    bgMiddleImage.ReportProgress(Convert.ToInt32(offset * pBarImage.Maximum / fileStream.Length));
                    int chSize = fileStream.Length - offset;
                    if (chSize > 1024)
                        chSize = 1024;
                    requestStream.Write(fileStream, offset, chSize);
                }
                requestStream.Close();
                requestStream.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return;
            }
        }

        private void bgMiddleImage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarImage.Value = e.ProgressPercentage;
            lblPer.Text = e.ProgressPercentage + "%";
        }
        private string GetMaxId()
        {
            string st = "";
            st = "select isnull(max(imgid),0)+1 from tblMiddleImage_App_list";
            DataTable dt = new DataTable();
            dt = objMainClass.fnFillDataTable(st);
            return dt.Rows[0][0].ToString();
        }

        private void bgMiddleImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
            FileInfo objFile = new FileInfo(lblPath.Text);

            //if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //StaticClass.constr.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = StaticClass.constr;
            //cmd.CommandText = "delete from  tblMiddleImage_App";
            //cmd.ExecuteNonQuery();
            //StaticClass.constr.Close();

            //if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            //StaticClass.constr.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = StaticClass.constr;
            //cmd.CommandText = "insert into tblMiddleImage_App(imgPath) values('http://146.0.229.66/AppMiddlePic/1" + objFile.Extension + "' )";
            //cmd.ExecuteNonQuery();
            //StaticClass.constr.Close();


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = "insert into tblMiddleImage_App_list(imgid,imgPath) values("+MaxId+ ",'http://85.195.82.94/AppMiddlePic/" + MaxId + objFile.Extension + "' )";
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();

            lblPer.Text = "";
            pBarImage.Value = 0;
            lblPath.Text = "";
            btnUploadImage.Enabled = true;

            FillImage();
        }

        private void frmChangeAppMiddleImage_Load(object sender, EventArgs e)
        {
            InitilizeGrid();
            FillImage();
        }
        private void InitilizeGrid()
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.Rows.Clear();
            }
            if (dgGrid.Columns.Count > 0)
            {
                dgGrid.Columns.Clear();
            }

            dgGrid.Columns.Add("mid", "M Id");
            dgGrid.Columns["mid"].Width = 0;
            dgGrid.Columns["mid"].Visible = false;
            dgGrid.Columns["mid"].ReadOnly = true;

            dgGrid.Columns.Add("mLink", "m Link");
            dgGrid.Columns["mLink"].Width = 0;
            dgGrid.Columns["mLink"].Visible = false;
            dgGrid.Columns["mLink"].ReadOnly = true;

            DataGridViewImageColumn Column_Img = new DataGridViewImageColumn();
            dgGrid.Columns.Add(Column_Img);
            Column_Img.HeaderText = "";
            Column_Img.Name = "Column_Img";
            Column_Img.Width = 310;
            Column_Img.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgGrid.Columns["Column_Img"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column_Img.Visible = true;



            DataGridViewLinkColumn EditUser = new DataGridViewLinkColumn();
            EditUser.HeaderText = "Select";
            EditUser.Text = "Select";
            EditUser.DataPropertyName = "Select";

            EditUser.LinkColor = Color.FromArgb(255, 255, 255);
            EditUser.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
            EditUser.LinkBehavior = LinkBehavior.SystemDefault;

            dgGrid.Columns.Add(EditUser);
            EditUser.UseColumnTextForLinkValue = true;
            EditUser.Width = 90;
            EditUser.Visible = true;
            dgGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn Delete = new DataGridViewLinkColumn();
            Delete.HeaderText = "Delete";
            Delete.Text = "Delete";
            Delete.DataPropertyName = "Delete";
            Delete.LinkColor = Color.FromArgb(255, 255, 255);
            Delete.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
            Delete.LinkBehavior = LinkBehavior.SystemDefault;

            dgGrid.Columns.Add(Delete);
            Delete.UseColumnTextForLinkValue = true;
            Delete.Width = 90;
            Delete.Visible = true;
            dgGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void FillImage()
        {
            try
            {


                InitilizeGrid();

                string str;
                int iCtr;
                DataTable dtDetail = new DataTable();
                str = "Select * from  tblMiddleImage_App_list order by imgid desc ";
                dtDetail = objMainClass.fnFillDataTable(str);

                Image image;
                 
                if ((dtDetail.Rows.Count > 0))
                {
                    for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                    {
                        
                        var request = WebRequest.Create(dtDetail.Rows[iCtr]["ImgPath"].ToString());
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            image = Bitmap.FromStream(stream);
                        }
                        dgGrid.Rows.Add();
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["mId"].Value = dtDetail.Rows[iCtr]["imgid"].ToString();
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["mLink"].Value = dtDetail.Rows[iCtr]["ImgPath"].ToString();

                        dgGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Column_Img"].Value = image;
                        dgGrid.Rows[dgGrid.Rows.Count - 1].Cells["Column_Img"].Style.Padding = new Padding(0, 5, 0, 5);
                        dgGrid.Columns["Column_Img"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dgGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 3)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                cmd.CommandText = "delete from  tblMiddleImage_App";
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();

                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                cmd = new SqlCommand();
                cmd.Connection = StaticClass.constr;
                cmd.CommandText = "insert into tblMiddleImage_App(imgPath) values('"+ dgGrid.Rows[e.RowIndex].Cells["mLink"].Value.ToString()+ "' )";
                cmd.ExecuteNonQuery();
                StaticClass.constr.Close();
                picImage.ImageLocation = dgGrid.Rows[e.RowIndex].Cells["mLink"].Value.ToString();
            }
            if (e.ColumnIndex == 4)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from  tblMiddleImage_App_list where imgid="+ dgGrid.Rows[e.RowIndex].Cells[0].Value;
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    FillImage();
                }
            }
        }
    }
}
