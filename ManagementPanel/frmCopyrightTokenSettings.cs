using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ManagementPanel
{
    public partial class frmCopyrightTokenSettings : Form
    {
        gblClass objMainClass = new gblClass();
        DateTimePicker dtpOrder;
        public frmCopyrightTokenSettings()
        {
            InitializeComponent();
            InitilizeAccountSettingsGrid();
            FillClient();
            dtpOrder = new DateTimePicker();
            dtpOrder.Format = DateTimePickerFormat.Custom;
            dtpOrder.CustomFormat = "dd/MMM/yyyy";
            dtpOrder.Value = DateTime.Now.Date;
            dtpOrder.Visible = false;
            dtpOrder.Width = 150;
            cmbPlayerVersion.Text = "Normal";
            dtpOrder.Font = new Font("Segoe UI", 12);
            dgAccountSettings.Controls.Add(dtpOrder);

            dtpOrder.ValueChanged += this.dtpOrder_ValueChanged;
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmAccountSettings_Load(object sender, EventArgs e)
        {

        }
        private void FillClient()
        {
            string str = "";
            //str = "select DFClientID,ClientName from DFClients where CountryCode is not null and DFClients.IsDealer=0 order by DFClientID desc";
            str = "select distinct DFClients.DFClientID,DFClients.ClientName from DFClients ";
            str = str + " inner join AMPlayerTokens on DFClients.DfClientid=AMPlayerTokens.Clientid ";
            str = str + " where DFClients.CountryCode is not null and DFClients.IsDealer=0 and DFClients.IsDealerclient is null and AMPlayerTokens.IsCopyright=1 ";
            str = str + " order by DFClients.DFClientID desc ";
            objMainClass.fnFillComboBox(str, cmbClientName, "DFClientID", "ClientName", "");
        }

        private void cmbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            //sQr = "select * from Users where  musictype<>'Copyleft' and clientId=" + Convert.ToInt32(cmbClientName.SelectedValue);

            sQr = " select distinct   Users.UserID, (Users.username + '( ' + isnull(Users.Location,'') + '---' + Users.Cityname + ' )') as username ";
            sQr = sQr + " from AMPlayerTokens ";
            sQr = sQr + " inner join Users on AMPlayerTokens.UserId= Users.UserID ";
            sQr = sQr + " where AMPlayerTokens.clientId=" + Convert.ToInt32(cmbClientName.SelectedValue) + "  and isnull(Users.MusicType,'') <>'Copyleft' ";
            sQr = sQr + " and PlayerType='Desktop'and AMPlayerTokens.IsCopyright=1  order by Users.UserID desc  ";

            objMainClass.fnFillComboBox(sQr, cmbUserName, "UserId", "UserName", "");
            InitilizeAccountSettingsGrid();
        }
        private void InitilizeAccountSettingsGrid()
        {
            if (dgAccountSettings.Rows.Count > 0)
            {
                dgAccountSettings.Rows.Clear();
            }
            if (dgAccountSettings.Columns.Count > 0)
            {
                dgAccountSettings.Columns.Clear();
            }
            //0
            dgAccountSettings.Columns.Add("TokenId", "TokenId");
            dgAccountSettings.Columns["TokenId"].Width = 0;
            dgAccountSettings.Columns["TokenId"].Visible = false;
            dgAccountSettings.Columns["TokenId"].ReadOnly = true;
            //1
            dgAccountSettings.Columns.Add("TokenNo", "Token No");
            dgAccountSettings.Columns["TokenNo"].Width = 290;
            dgAccountSettings.Columns["TokenNo"].Visible = true;
            dgAccountSettings.Columns["TokenNo"].ReadOnly = true;
            dgAccountSettings.Columns["TokenNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //2

            DataGridViewCheckBoxColumn Copyright = new DataGridViewCheckBoxColumn();

            Copyright.HeaderText = "Copyright";
            Copyright.DataPropertyName = "Copyright";
            dgAccountSettings.Columns.Add(Copyright);
            Copyright.Width = 90;
            Copyright.Visible = true;
            dgAccountSettings.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //3
            dgAccountSettings.Columns.Add("CopyrightExpiryDate", "Expiry Date");
            dgAccountSettings.Columns["CopyrightExpiryDate"].Width = 110;
            dgAccountSettings.Columns["CopyrightExpiryDate"].Visible = true;
            dgAccountSettings.Columns["CopyrightExpiryDate"].ReadOnly = false;
            dgAccountSettings.Columns["CopyrightExpiryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //4
            DataGridViewCheckBoxColumn Fitness = new DataGridViewCheckBoxColumn();
            Fitness.HeaderText = "Fitness";
            Fitness.DataPropertyName = "Fitness";
            dgAccountSettings.Columns.Add(Fitness);
            Fitness.Width = 0;
            Fitness.Visible = false;
            //5
            dgAccountSettings.Columns.Add("FitnessExpiryDate", "Expiry Date");
            dgAccountSettings.Columns["FitnessExpiryDate"].Width = 110;
            dgAccountSettings.Columns["FitnessExpiryDate"].Visible = false;
            dgAccountSettings.Columns["FitnessExpiryDate"].ReadOnly = false;

            //6
            DataGridViewCheckBoxColumn Stream = new DataGridViewCheckBoxColumn();
            Stream.HeaderText = "Stream";
            Stream.DataPropertyName = "Stream";
            dgAccountSettings.Columns.Add(Stream);
            Stream.Width = 60;
            Stream.Visible = true;
            dgAccountSettings.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //7
            dgAccountSettings.Columns.Add("StreamExpiryDate", "Expiry Date");
            dgAccountSettings.Columns["StreamExpiryDate"].Width = 110;
            dgAccountSettings.Columns["StreamExpiryDate"].Visible = false;
            dgAccountSettings.Columns["StreamExpiryDate"].ReadOnly = false;

            //8
            DataGridViewCheckBoxColumn Block = new DataGridViewCheckBoxColumn();
            Block.HeaderText = "Block Advt";
            Block.DataPropertyName = "Block";
            dgAccountSettings.Columns.Add(Block);
            Block.Width = 0;
            Block.Visible = false;

            //9
            DataGridViewCheckBoxColumn Advt = new DataGridViewCheckBoxColumn();
            Advt.HeaderText = "Advt";
            Advt.DataPropertyName = "Advt";
            dgAccountSettings.Columns.Add(Advt);
            Advt.Width = 50;
            Advt.Visible = true;
            dgAccountSettings.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //10
            dgAccountSettings.Columns.Add("AdvtExpiryDate", "Expiry Date");
            dgAccountSettings.Columns["AdvtExpiryDate"].Width = 110;
            dgAccountSettings.Columns["AdvtExpiryDate"].Visible = true;
            dgAccountSettings.Columns["AdvtExpiryDate"].ReadOnly = false;
            dgAccountSettings.Columns["AdvtExpiryDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //11
            dgAccountSettings.Columns.Add("cName", "City");
            dgAccountSettings.Columns["cName"].Width = 130;
            dgAccountSettings.Columns["cName"].Visible = true;
            dgAccountSettings.Columns["cName"].ReadOnly = true;
            dgAccountSettings.Columns["cName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //12
            dgAccountSettings.Columns.Add("sName", "Street");
            dgAccountSettings.Columns["sName"].Width = 130;
            dgAccountSettings.Columns["sName"].Visible = true;
            dgAccountSettings.Columns["sName"].ReadOnly = true;
            dgAccountSettings.Columns["sName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //13
            dgAccountSettings.Columns.Add("Loc", "Location");
            dgAccountSettings.Columns["Loc"].Width = 130;
            dgAccountSettings.Columns["Loc"].Visible = true;
            dgAccountSettings.Columns["Loc"].ReadOnly = true;
            dgAccountSettings.Columns["Loc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //14
            DataGridViewLinkColumn EditPlaylist = new DataGridViewLinkColumn();
            EditPlaylist.HeaderText = "Edit";
            EditPlaylist.Text = "Edit";
            EditPlaylist.DataPropertyName = "Edit";
            dgAccountSettings.Columns.Add(EditPlaylist);
            EditPlaylist.UseColumnTextForLinkValue = true;
            EditPlaylist.Width = 50;
            dgAccountSettings.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewLinkColumn DeleteClient = new DataGridViewLinkColumn();
            DeleteClient.HeaderText = "Delete";
            DeleteClient.Text = "Delete";
            DeleteClient.DataPropertyName = "Delete";
            DeleteClient.LinkColor = Color.FromArgb(64, 64, 64);
            DeleteClient.CellTemplate.Style.Font = new System.Drawing.Font("Segoe UI", 10);
            DeleteClient.LinkBehavior = LinkBehavior.SystemDefault;
            dgAccountSettings.Columns.Add(DeleteClient);
            DeleteClient.UseColumnTextForLinkValue = true;
            DeleteClient.Width = 60;
            dgAccountSettings.Columns[15].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void FillTokenGenerationData(string sQr)
        {
            DataTable dtDetail = new DataTable();

            //------------ New Skpye Id-----------------
            //parastech6
            //seghal123


            InitilizeAccountSettingsGrid();
            dtDetail = objMainClass.fnFillDataTable(sQr);

            if (dtDetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                {
                    dgAccountSettings.Rows.Add();

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[0].Value = dtDetail.Rows[i]["TokenID"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[1].Value = dtDetail.Rows[i]["Tokennobkp"].ToString() + ' ' + dtDetail.Rows[i]["TokenID"].ToString();

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[2].Value = dtDetail.Rows[i]["IsCopyright"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[3].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["CopyrightExpiryDate"]);

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[4].Value = dtDetail.Rows[i]["IsFitness"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[5].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["FitnessExpiryDate"]);

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[6].Value = dtDetail.Rows[i]["IsStream"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[7].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["StreamExpiryDate"]);

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[8].Value = dtDetail.Rows[i]["IsBlockAdvt"];

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[9].Value = dtDetail.Rows[i]["IsAdvt"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells[10].Value = string.Format("{0:dd/MMM/yyyy}", dtDetail.Rows[i]["AdvtExpiryDate"]);

                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells["cName"].Value = dtDetail.Rows[i]["CityName"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells["sName"].Value = dtDetail.Rows[i]["StreetName"];
                    dgAccountSettings.Rows[dgAccountSettings.Rows.Count - 1].Cells["Loc"].Value = dtDetail.Rows[i]["Location"];
                }
            }
            foreach (DataGridViewRow row in dgAccountSettings.Rows)
            {
                row.Height = 30;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }






        private void dgAccountSettings_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //try
            //{
            //    if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 3))
            //    {
            //        dtpOrder.Location = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
            //        dtpOrder.Visible = true;
            //        if (dgAccountSettings.CurrentCell.Value != DBNull.Value)
            //        {
            //           // dtpOrder.Value = (DateTime)dgAccountSettings.CurrentCell.Value;
            //        }
            //        else
            //        {
            //            dtpOrder.Value =  DateTime.Today;
            //        }
            //    }
            //    else
            //    {
            //        dtpOrder.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dgAccountSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 3))
            //    {
            //        dgAccountSettings.CurrentCell.Value = string.Format("{0:dd/MMM/yyyy}", dtpOrder.Value.Date);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void dtpOrder_ValueChanged(object sender, EventArgs e)
        {
            dgAccountSettings.CurrentCell.Value = dtpOrder.Text;
        }

        private void dgAccountSettings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string swr = "";
            string sAr = "";
            Int32 NoofToken = 0;
            if (e.RowIndex < 0) return;
            try
            {
                if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 3))
                {
                    dtpOrder.Location = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpOrder.Width = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                    dtpOrder.Visible = true;
                    if (dgAccountSettings.CurrentCell.Value != DBNull.Value)
                    {
                        dtpOrder.Value = Convert.ToDateTime(dgAccountSettings.CurrentCell.Value);
                    }
                    else
                    {
                        dtpOrder.Value = DateTime.Today;
                    }
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 5))
                {
                    dtpOrder.Location = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpOrder.Width = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                    dtpOrder.Visible = true;
                    if (dgAccountSettings.CurrentCell.Value != DBNull.Value)
                    {
                        dtpOrder.Value = Convert.ToDateTime(dgAccountSettings.CurrentCell.Value);
                    }
                    else
                    {
                        dtpOrder.Value = DateTime.Today;
                    }
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 7))
                {
                    dtpOrder.Location = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpOrder.Width = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                    dtpOrder.Visible = true;
                    if (dgAccountSettings.CurrentCell.Value != DBNull.Value)
                    {
                        dtpOrder.Value = Convert.ToDateTime(dgAccountSettings.CurrentCell.Value);
                    }
                    else
                    {
                        dtpOrder.Value = DateTime.Today;
                    }
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 10))
                {
                    dtpOrder.Location = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpOrder.Width = dgAccountSettings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                    dtpOrder.Visible = true;
                    if (dgAccountSettings.CurrentCell.Value != DBNull.Value)
                    {
                        dtpOrder.Value = Convert.ToDateTime(dgAccountSettings.CurrentCell.Value);
                    }
                    else
                    {
                        dtpOrder.Value = DateTime.Today;
                    }
                }
                else
                {
                    dtpOrder.Visible = false;
                }
                if (e.ColumnIndex == 14)
                {
                    frmTokenInformation frm = new frmTokenInformation();
                    
                    StaticClass.DealerTokenId = 0;
                    StaticClass.dealerUserId = Convert.ToInt32(cmbUserName.SelectedValue);
                    StaticClass.DealerDfClientId = Convert.ToInt32(cmbClientName.SelectedValue);
                    StaticClass.DealerUserName = cmbUserName.Text.ToString();
                    StaticClass.DealerTokenId = Convert.ToInt32(dgAccountSettings.Rows[e.RowIndex].Cells["tokenid"].Value);
                    frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.MaximizeBox = false;
                    frm.ShowDialog();
                    string sQr = "";
                    sQr = " spUserTokenSettingsCopyleft 'Desktop' , " + Convert.ToInt32(cmbUserName.SelectedValue);
                    FillTokenGenerationData(sQr);
                }
                if (e.ColumnIndex == 15)
                {
                    DialogResult result1;
                    result1 = MessageBox.Show("Are you sure to delete ?", "Management Panel", MessageBoxButtons.YesNo);
                    if (result1 == System.Windows.Forms.DialogResult.Yes)
                    {
                        string strDel = "";
                        strDel = "";
                        strDel = "delete from TitlesInPlaylists where Playlistid in( ";
                        strDel = strDel + "select distinct Playlistid from playlists where tokenid = " + Convert.ToInt32(dgAccountSettings.Rows[e.RowIndex].Cells["tokenid"].Value) + ")";
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        SqlCommand cmd = new SqlCommand(strDel, StaticClass.constr);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        strDel = "";
                        strDel = "delete from playlists where tokenid = " + Convert.ToInt32(dgAccountSettings.Rows[e.RowIndex].Cells["tokenid"].Value) + " ";
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        cmd = new SqlCommand(strDel, StaticClass.constr);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        strDel = "";
                        strDel = "delete from AMPlayerTokens where tokenid = " + Convert.ToInt32(dgAccountSettings.Rows[e.RowIndex].Cells["tokenid"].Value) + " ";
                        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        StaticClass.constr.Open();
                        cmd = new SqlCommand(strDel, StaticClass.constr);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        StaticClass.constr.Close();

                        dtpOrder.Visible = false;
                        string sQr = "";
                        sQr = " spUserTokenSettingsCopyleft 'Desktop' , " + Convert.ToInt32(cmbUserName.SelectedValue);
                        FillTokenGenerationData(sQr);
                    }
                }
                //if (e.ColumnIndex == 9)
                //{
                //    if (dgAccountSettings.Rows[e.RowIndex].Cells[1].Value.ToString() == "Used")
                //    {
                //        MessageBox.Show("This token is used by client. !! You only block this token !!", "Management Panel");
                //        return;
                //    }
                //    result = MessageBox.Show("Are you sure to delete this token ?", "Management Panel", buttons);
                //    if (result == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        swr = "select NoOfToken from Users where userid=" + Convert.ToInt32(cmbUserName.SelectedValue);
                //        DataTable dsUser = new DataTable();
                //        dsUser = objMainClass.fnFillDataTable(swr);
                //        NoofToken = Convert.ToInt32(dsUser.Rows[0]["NoOfToken"]);
                //        NoofToken = NoofToken - 1;
                //        if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                //        StaticClass.constr.Open();
                //        SqlCommand cmd = new SqlCommand("Delete_User_Token", StaticClass.constr);
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                //        cmd.Parameters["@UserId"].Value = Convert.ToInt32(cmbUserName.SelectedValue);

                //        cmd.Parameters.Add(new SqlParameter("@TokenId", SqlDbType.BigInt));
                //        cmd.Parameters["@TokenId"].Value = Convert.ToInt32(dgAccountSettings.Rows[e.RowIndex].Cells[0].Value);

                //        cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.BigInt));
                //        cmd.Parameters["@ClientId"].Value = Convert.ToInt32(cmbClientName.SelectedValue);

                //        cmd.Parameters.Add(new SqlParameter("@NoofToken", SqlDbType.BigInt));
                //        cmd.Parameters["@NoofToken"].Value = Convert.ToInt32(NoofToken);
                //        try
                //        {
                //            cmd.ExecuteNonQuery();
                //            dgAccountSettings.Rows.RemoveAt(e.RowIndex);
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message);
                //        }
                //        finally
                //        {
                //            StaticClass.constr.Close();
                //        }
                //    }
                //    else if (result == System.Windows.Forms.DialogResult.No)
                //    {
                //        return;
                //    }

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgAccountSettings_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 3))
                {
                    dgAccountSettings.CurrentCell.Value = string.Format("{0:dd/MMM/yyyy}", dtpOrder.Value.Date);
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 5))
                {
                    dgAccountSettings.CurrentCell.Value = string.Format("{0:dd/MMM/yyyy}", dtpOrder.Value.Date);
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 7))
                {
                    dgAccountSettings.CurrentCell.Value = string.Format("{0:dd/MMM/yyyy}", dtpOrder.Value.Date);
                }
                else if ((dgAccountSettings.Focused) && (dgAccountSettings.CurrentCell.ColumnIndex == 10))
                {
                    dgAccountSettings.CurrentCell.Value = string.Format("{0:dd/MMM/yyyy}", dtpOrder.Value.Date);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (objMainClass.CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please check your Internet connection.", "Management Panel");
                return;
            }
            if (SubmitValidation() == false)
            {
                return;
            }
            SaveUserToken();
            ClearFields();
        }
        private void SaveUserToken()
        {
            for (int i = 0; i <= dgAccountSettings.Rows.Count - 1; i++)
            {
                if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                StaticClass.constr.Open();
                SqlCommand cmd = new SqlCommand("Update_User_Token", StaticClass.constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt));
                cmd.Parameters["@UserId"].Value = Convert.ToInt32(cmbUserName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@TokenId", SqlDbType.BigInt));
                cmd.Parameters["@TokenId"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[0].Value);

                cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.BigInt));
                cmd.Parameters["@ClientId"].Value = Convert.ToInt32(cmbClientName.SelectedValue);

                cmd.Parameters.Add(new SqlParameter("@IsSuspend", SqlDbType.Int));
                cmd.Parameters["@IsSuspend"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@IsDam", SqlDbType.Int));
                cmd.Parameters["@IsDam"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@DamExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@DamExpiryDate"].Value = "01-01-1900";

                cmd.Parameters.Add(new SqlParameter("@IsSanjivani", SqlDbType.Int));
                cmd.Parameters["@IsSanjivani"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[4].Value);

                cmd.Parameters.Add(new SqlParameter("@SanjivaniExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@SanjivaniExpiryDate"].Value = dgAccountSettings.Rows[i].Cells[5].Value;

                cmd.Parameters.Add(new SqlParameter("@IsCopyRight", SqlDbType.Int));
                cmd.Parameters["@IsCopyRight"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[2].Value);

                cmd.Parameters.Add(new SqlParameter("@DateExpire", SqlDbType.DateTime));
                cmd.Parameters["@DateExpire"].Value = dgAccountSettings.Rows[i].Cells[3].Value;

                cmd.Parameters.Add(new SqlParameter("@IsFitness", SqlDbType.Int));
                cmd.Parameters["@IsFitness"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[4].Value);

                cmd.Parameters.Add(new SqlParameter("@FitnessExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@FitnessExpiryDate"].Value = dgAccountSettings.Rows[i].Cells[5].Value;

                cmd.Parameters.Add(new SqlParameter("@IsStream", SqlDbType.Int));
                cmd.Parameters["@IsStream"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[6].Value);

                cmd.Parameters.Add(new SqlParameter("@StreamExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@StreamExpiryDate"].Value = dgAccountSettings.Rows[i].Cells[7].Value;

                cmd.Parameters.Add(new SqlParameter("@IsBlock", SqlDbType.Int));
                cmd.Parameters["@IsBlock"].Value = "0";

                cmd.Parameters.Add(new SqlParameter("@IsBlockAdvt", SqlDbType.Int));
                cmd.Parameters["@IsBlockAdvt"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[8].Value);

                cmd.Parameters.Add(new SqlParameter("@IsAdvt", SqlDbType.Int));
                cmd.Parameters["@IsAdvt"].Value = Convert.ToInt32(dgAccountSettings.Rows[i].Cells[9].Value);

                cmd.Parameters.Add(new SqlParameter("@AdvtExpiryDate", SqlDbType.DateTime));
                cmd.Parameters["@AdvtExpiryDate"].Value = dgAccountSettings.Rows[i].Cells[10].Value;

                try
                {
                    cmd.ExecuteNonQuery();
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
        }
        private void ClearFields()
        {
            FillClient();
            
            InitilizeAccountSettingsGrid();
            dtpOrder.Visible = false;
        }

        private Boolean SubmitValidation()
        {
            if (Convert.ToInt32(cmbClientName.SelectedValue) == 0)
            {
                MessageBox.Show("Client name cannot be blank", "Management Panel");
                cmbClientName.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbUserName.SelectedValue) == 0)
            {
                MessageBox.Show("User name cannot be blank", "Management Panel");
                cmbUserName.Focus();
                return false;
            }
            return true;

        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void frmCopyrightTokenSettings_MdiChildActivate(object sender, EventArgs e)
        {

        }

        private void cmbClientName_Click(object sender, EventArgs e)
        {
            FillClient();
        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
           // dtpOrder.Visible = false;
            string sQr = "";
            sQr = " spUserTokenSettingsCopyleft 'Desktop' , " + Convert.ToInt32(cmbUserName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'";
            FillTokenGenerationData(sQr);
        }

        private void cmbPlayerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sQr = "";
            sQr = " spUserTokenSettingsCopyleft 'Desktop' , " + Convert.ToInt32(cmbUserName.SelectedValue) + " , '" + cmbPlayerVersion.Text + "'";
            FillTokenGenerationData(sQr);
        }
    }
}
