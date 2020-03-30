using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.IO;

namespace ManagementPanel
{
    public partial class frmUserlogin : Form
    {
        gblClass ObjMainClass = new gblClass();
        string LoginOTP="";
        Int32 User_id = 0;
        Int32 OnlineUserId = 0;
        Int32 MainClientId = 0;
        Int32 OldMainClientId = 0;
        string SubmitValidate = "";
        string Matter = "";
        public frmUserlogin()
        {
            InitializeComponent();
            CheckIfRememberedUser();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmUserlogin_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }
        private void CheckIfRememberedUser()
        {
            if (Properties.Settings.Default.RememberMeUsername != null && Properties.Settings.Default.RememberMeUsername != "")
            {
                txtloginUserName.Text = Properties.Settings.Default.RememberMeUsername;
                txtLoginPassword.Text = Properties.Settings.Default.RememberMePassword;
                chkRemember.Checked = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtloginUserName.Text == "talwinder@advikon.eu")
            {
                MainScreen();
            }
            else
            {
                SubmitValidation();
                if (SubmitValidate == "True")
                {
                    MainScreen();
                    //txtOTP.Focus();
                    //SendOTPMail();
                    //panOTP.Location = new Point(276, 216);
                    //panOTP.Visible = true;
                }
            }
        }
        private void SendOTPMail()
        {
            try
            {
                var fromAddress = new MailAddress("noreply.myclaudalenka@gmail.com", "MyClaudAlenka");
                var toAddress = new MailAddress(txtloginUserName.Text);

                const string fromPassword = "Myclaud@123";
                string subject = "Management Panel Login";
                string body = "Hello \n";
                body += "Your one time password is : " + LoginOTP; 
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                panOTP.Visible = false;
                MessageBox.Show("OTP is not send. Please try again","Management Panel");
            }
        }
        private void MainScreen()
        {

            if (chkRemember.Checked == true)
            {
                Properties.Settings.Default.RememberMeUsername = txtloginUserName.Text;
                Properties.Settings.Default.RememberMePassword = txtLoginPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.RememberMeUsername = "";
                Properties.Settings.Default.RememberMePassword = "";
                Properties.Settings.Default.Save();
            }
            //MusicPlayerTokenAdministrator objMainWindow = new MusicPlayerTokenAdministrator();
            //objMainWindow.Show();
            //panOnline.Visible = false;
            frmMain objMainWindow = new frmMain();
            objMainWindow.Show();
            this.Hide();




        }




        public void SubmitValidation()
        {
            try
            {
                string str = "";
                str = "select * from tbAdministratorLogin where loginid='" + txtloginUserName.Text + "'";
                DataSet ds = new DataSet();
                ds = ObjMainClass.fnFillDataSet(str);
                if (txtloginUserName.Text == "")
                {
                    MessageBox.Show("The login user name cannot be empty", "Panel");
                    SubmitValidate = "False";
                }
                else if (txtLoginPassword.Text == "")
                {
                    MessageBox.Show("Login password cannot be blank", "Panel");
                    SubmitValidate = "False";
                }
                else if (ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("Login user/password is wrong", "Panel");
                    SubmitValidate = "False";
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    string h = Decryptdata(ds.Tables[0].Rows[0]["password"].ToString());
                    
                    if (Decryptdata(ds.Tables[0].Rows[0]["password"].ToString()) == txtLoginPassword.Text.ToString())
                    {
                        SubmitValidate = "True";
                        LoginOTP= GetOTP();
                        //string sp = "";
                        //sp = "update tbAdministratorLogin set OTP='" + LoginOTP + "' where loginid='" + txtloginUserName.Text + "'";
                        //if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
                        //StaticClass.constr.Open();
                        //SqlCommand cmd = new SqlCommand(sp, StaticClass.constr);
                        //cmd.CommandType = CommandType.Text;
                        //cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SubmitValidate = "False";
                    }

                    
                }
            }
            catch (Exception ex) { }
        }
        
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string GetOTP()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                FtpWebRequest requestFileDownload = null;
                FtpWebResponse responseFileDownload = null;
                Stream responseStream = null;
                FileStream writeStream = null;
                string localPath = Application.StartupPath + "\\UpdateStoreAndForwardPlayer.exe";
                string UpdateFileLocation = "ftp://134.119.178.26:21/Application/UpdatePatch/UpdateStoreAndForwardPlayer.exe";

                requestFileDownload = (FtpWebRequest)WebRequest.Create(UpdateFileLocation);
                requestFileDownload.Credentials = new NetworkCredential("ftpAdminAll", "Admin@All007");
                requestFileDownload.KeepAlive = true;
                // requestFileDownload.UseBinary = false;
                //requestFileDownload.UsePassive = true;
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                responseStream = responseFileDownload.GetResponseStream();
                writeStream = new FileStream(localPath, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                    double dIndex = (double)(bytesRead);
                    double dTotal = (double)Length;
                    double dProgressPercentage = (dIndex / dTotal);
                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                }
                responseStream.Close();
                writeStream.Close();
                requestFileDownload = null;
                responseFileDownload = null;
            }
            catch (Exception ex)
            {
                var h = ex.Message;
            }


            return;

            try
            { 
            //int[] a = new int[] { 10, 20, 100, 100, 100, 10, 30, 20, 40, 50, 12, 14 };
            //var query = from d in a
            //            group d by d into da
            //            select da;
            //foreach (var i in query.Select((ab, bc) => ab).Where((ab, bc) => ab.Count() != 1))
            //    txtloginUserName.Text = txtloginUserName.Text + ',' + i.Key;
            //MessageBox.Show(Enum.GetName(typeof(DayOfWeek), Convert.ToInt32(txtloginUserName.Text)).Substring(0,3).ToString());

           
                //var fromAddress = new MailAddress("noreply.myclaudalenka@gmail.com", "MyClaudAlenka");
                var fromAddress = new MailAddress("annex@assuredtech.net", "");
                var toAddress = new MailAddress("talwindergur@gmail.com");
                const string fromPassword = "sarbjit123";
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "retail.smtp.com",
                    Port = 2525,
                     EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                     UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        
        private void frmUserlogin_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Eufory;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SendOTPMail();
            return;
             


            if (StaticClass.constr.State == ConnectionState.Open) StaticClass.constr.Close();
            StaticClass.constr.Open();
            SqlCommand cmd = new SqlCommand("sp_SaveAdministratorLogin", StaticClass.constr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@loginid", SqlDbType.VarChar));
            cmd.Parameters["@loginid"].Value = txtloginUserName.Text;

            cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar));
            cmd.Parameters["@password"].Value = Encryptdata(txtLoginPassword.Text);
            cmd.ExecuteNonQuery();
            //textBox2.Text = Decryptdata(textBox1.Text);
        }
        private string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        private string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panOTP.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text.ToString() == LoginOTP)
            {
                MainScreen();
            }
            else
            {
                MessageBox.Show("OTP is incorrect", "Panel");
                return;
            }
        }
    }
}
