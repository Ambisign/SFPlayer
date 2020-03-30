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
    public partial class frmMoblieAppStreams : Form
    {
        gblClass objMainClass = new gblClass();
        string AppSaveType = "Save";
        Int32 NewFileName = 0;
        Int32 ModifyStreamIdApp = 0;
        public frmMoblieAppStreams()
        {
            InitializeComponent();
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.FileName = "";
            OpenDialog.DefaultExt = ".png";
            OpenDialog.Filter = "|*.png";
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (ValidFile(OpenDialog.FileName, 36864, 125, 125))
                {
                    txtPath.Text = OpenDialog.FileName;
                    AppSaveType = "Save";
                }
                else
                {
                    MessageBox.Show("Image size is invalid", "Management Panel");
                    txtPath.Text = "";
                    btnDialog.Focus();
                    return;
                }

            }
        }
        private bool ValidFile(string filename, long limitInBytes, int limitWidth, int limitHeight)
        {
            var fileSizeInBytes = new FileInfo(filename).Length;
            if (fileSizeInBytes > limitInBytes) return false;

            using (var img = new Bitmap(filename))
            {
                if (img.Width > limitWidth || img.Height > limitHeight) return false;
            }

            return true;
        }

        private void btnAppSave_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy == false)
            {
                if (AppSubmitValidation() == false) return;
                btnAppRefersh.Enabled = false;
                btnAppSave.Enabled = false;
                AppRecordSave();
            }
        }
        private Boolean AppSubmitValidation()
        {
            if (txtAppStreamName.Text == "")
            {
                MessageBox.Show("The stream name cannot be empty", "Management Panel");
                txtAppStreamName.Focus();
                return false;
            }
              if (txtAppStreamLink.Text == "")
            {
                MessageBox.Show("The stream link cannot be empty", "Management Panel");
                txtAppStreamLink.Focus();
                return false;
            }

             if (txtPath.Text == "")
            {
                MessageBox.Show("Image path cannot be blank", "Management Panel");
                txtPath.Focus();
                return false;
            }
            
            if (Convert.ToInt32(cmbCustomer.SelectedValue) == 0)
            {
                MessageBox.Show("The customer name cannot be empty", "Management Panel");
                cmbCustomer.Focus();
                return false;
            }
            return true;
        }
        private void AppRecordSave()
        {

            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_AppStream_Save", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@StreamId", SqlDbType.BigInt));
            cmd.Parameters["@StreamId"].Value = ModifyStreamIdApp;

            cmd.Parameters.Add(new SqlParameter("@StreamName", SqlDbType.VarChar));
            cmd.Parameters["@StreamName"].Value = txtAppStreamName.Text;

            cmd.Parameters.Add(new SqlParameter("@StreamLink", SqlDbType.VarChar));
            cmd.Parameters["@StreamLink"].Value = txtAppStreamLink.Text;

            

            cmd.Parameters.Add(new SqlParameter("@dfclientid", SqlDbType.BigInt));
            cmd.Parameters["@dfclientid"].Value = Convert.ToInt32(cmbCustomer.SelectedValue);

            try
            {
                NewFileName = Convert.ToInt32(cmd.ExecuteScalar());
                if (AppSaveType == "Save")
                {
                    bgWorker.RunWorkerAsync();
                }
                else
                {
                    ClearAppFelids();
                }
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
        private void ClearAppFelids()
        {
            txtAppStreamName.Text = "";
            txtAppStreamLink.Text = "";
            txtPath.Text = "";
            AppSaveType = "Save";
            FillStreamAppData(0);
            ModifyStreamIdApp = 0;
            progressBar3.Value = 0;
            lblAdvtPercentage.Text = "";
            btnAppRefersh.Enabled = true;
            btnAppSave.Enabled = true;
            btnAppSave.Text = "Save";
            cmbCustomer.SelectedValue=0;
        }
        private void InitilizeAppStreamGrid()
        {
            if (dgAppStream.Rows.Count > 0)
            {
                dgAppStream.Rows.Clear();
            }
            if (dgAppStream.Columns.Count > 0)
            {
                dgAppStream.Columns.Clear();
            }

            dgAppStream.Columns.Add("Streamid", "Stream Id");
            dgAppStream.Columns["Streamid"].Width = 0;
            dgAppStream.Columns["Streamid"].Visible = false;
            dgAppStream.Columns["Streamid"].ReadOnly = true;

            dgAppStream.Columns.Add("StreamLink", "Stream Link");
            dgAppStream.Columns["StreamLink"].Width = 0;
            dgAppStream.Columns["StreamLink"].Visible = false;
            dgAppStream.Columns["StreamLink"].ReadOnly = true;

            DataGridViewImageColumn Column_Img = new DataGridViewImageColumn();
            dgAppStream.Columns.Add(Column_Img);
            Column_Img.HeaderText = "";
            Column_Img.Name = "Column_Img";
            Column_Img.Width = 70;
            Column_Img.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgAppStream.Columns["Column_Img"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column_Img.Visible = true;

            dgAppStream.Columns.Add("StreamName", "Stream Name");
            dgAppStream.Columns["StreamName"].Width = 950;
            dgAppStream.Columns["StreamName"].Visible = true;
            dgAppStream.Columns["StreamName"].ReadOnly = true;
            dgAppStream.Columns["StreamName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgAppStream.Columns.Add("ImgPath", "ImgPath");
            dgAppStream.Columns["ImgPath"].Width = 0;
            dgAppStream.Columns["ImgPath"].Visible = false;
            dgAppStream.Columns["ImgPath"].ReadOnly = true;

            DataGridViewLinkColumn EditUser = new DataGridViewLinkColumn();
            EditUser.HeaderText = "Edit";
            EditUser.Text = "Edit";
            EditUser.DataPropertyName = "Edit";
            dgAppStream.Columns.Add(EditUser);
            EditUser.UseColumnTextForLinkValue = true;
            EditUser.Width = 50;
            EditUser.Visible = true;
            dgAppStream.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn Delete = new DataGridViewLinkColumn();
            Delete.HeaderText = "Delete";
            Delete.Text = "Delete";
            Delete.DataPropertyName = "Delete";
            dgAppStream.Columns.Add(Delete);
            Delete.UseColumnTextForLinkValue = true;
            Delete.Width = 90;
            Delete.Visible = true;
            dgAppStream.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }
        private void FillStreamAppData(Int32 TitleCategoryId)
        {
            string str;
            int iCtr;
            DataTable dtDetail;
            str = "Select * from  tbStreaming_App where dfclientid = " + Convert.ToInt32(cmbSearchCustomer.SelectedValue) + "  order by  StreamNameapp";
            dtDetail = objMainClass.fnFillDataTable(str);
            Image image;
            InitilizeAppStreamGrid();

            if ((dtDetail.Rows.Count > 0))
            {
                for (iCtr = 0; (iCtr <= (dtDetail.Rows.Count - 1)); iCtr++)
                {
                    dgAppStream.Rows.Add();
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["Streamid"].Value = dtDetail.Rows[iCtr]["StreamId"];
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["StreamLink"].Value = dtDetail.Rows[iCtr]["StreamLinkapp"];
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["StreamName"].Value = dtDetail.Rows[iCtr]["StreamNameapp"];
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["ImgPath"].Value = dtDetail.Rows[iCtr]["ImgPath"];


                    dgAppStream.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    var request = WebRequest.Create(dtDetail.Rows[iCtr]["ImgPath"].ToString());
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        image = Bitmap.FromStream(stream);
                    }
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["Column_Img"].Value = image;
                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["Column_Img"].Style.Padding = new Padding(0, 5, 0, 5);
                    dgAppStream.Columns["Column_Img"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                    dgAppStream.Rows[dgAppStream.Rows.Count - 1].Cells["StreamName"].Style.Font = new Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);

                }
                

            }

        }
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string filename = txtPath.Text;
                FileInfo objFile = new FileInfo(filename);
                string ftpServerIP = "ftp://85.195.82.94:21/AMMusicFiles/ripper/AppStreamPic/" + NewFileName + objFile.Extension;
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
                    bgWorker.ReportProgress(Convert.ToInt32(offset * progressBar3.Maximum / fileStream.Length));
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

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar3.Value = e.ProgressPercentage;
            lblAdvtPercentage.Text = e.ProgressPercentage + "%";
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
            FileInfo objFile = new FileInfo(txtPath.Text);
            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StaticClass.constr;
            cmd.CommandText = "update tbStreaming_App set ImgPath='http://85.195.82.94/AppStreamPic/" + NewFileName + objFile.Extension + "' where StreamId=" + NewFileName;
            cmd.ExecuteNonQuery();
            StaticClass.constr.Close();
            ClearAppFelids();
        }

        private void btnAppRefersh_Click(object sender, EventArgs e)
        {
            ClearAppFelids();
        }

        private void frmMoblieAppStreams_Load(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
            objMainClass.fnFillComboBox(str, cmbSearchCustomer, "DFClientID", "ClientName", "");

            cmbSearchCustomer.SelectedValue = 6;
        }

        private void dgAppStream_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 5)
            {
                AppSaveType = "Modify";
                btnAppSave.Text = "Update";
                ModifyStreamIdApp = Convert.ToInt32(dgAppStream.Rows[e.RowIndex].Cells["Streamid"].Value);
                txtAppStreamLink.Text = dgAppStream.Rows[e.RowIndex].Cells["StreamLink"].Value.ToString();
                txtAppStreamName.Text = dgAppStream.Rows[e.RowIndex].Cells["StreamName"].Value.ToString();
                txtPath.Text = dgAppStream.Rows[e.RowIndex].Cells["ImgPath"].Value.ToString();
                cmbCustomer.SelectedValue = cmbSearchCustomer.SelectedValue;
            }
            if (e.ColumnIndex == 6)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Are you sure to delete ?", "Management Panel", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                    StaticClass.constr.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = StaticClass.constr;
                    cmd.CommandText = "delete from tbStreaming_App where streamid=" + Convert.ToInt32(dgAppStream.Rows[e.RowIndex].Cells["Streamid"].Value);
                    cmd.ExecuteNonQuery();
                    StaticClass.constr.Close();
                    ClearAppFelids();
                }
            }
        }

        private void cmbCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbCustomer, "DFClientID", "ClientName", "");
        }

        private void cmbSearchCustomer_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "select DFClientID,RIGHT(ClientName, LEN(ClientName) - 3) as ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=1 ";
            str = str + " and DFClientID in (select distinct clientid from AMPlayerTokens) ";
            str = str + " order by RIGHT(ClientName, LEN(ClientName) - 3) ";
            objMainClass.fnFillComboBox(str, cmbSearchCustomer, "DFClientID", "ClientName", "");
        }

        private void cmbSearchCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStreamAppData(0);
        }
    }
}
