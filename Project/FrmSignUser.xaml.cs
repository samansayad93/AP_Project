﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmSignUser.xaml
    /// </summary>
    public partial class FrmSignUser : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmSignUser()
        {
            InitializeComponent();
        }

        private  string GeneratePass()
        {
            string password = "";
            Random rand = new Random();
            for(int i=0;i<8;i++)
            {
                password += rand.Next(0, 9);
            }
            return password;
        }

        private string GenerateUser()
        {
            string username = "user";
            Random rand = new Random();
            int random = rand.Next(0, 9999);
            return username + random.ToString();
        }

        public int Func(string username,string password)
        {
            while (Validation.IsThisUserValid(username) == false)
            {
                username = GenerateUser();
            }
            while (Validation.IsThisUserPassValid(password) == false)
            {
                password = GeneratePass();
            }
            SqlCommand cmd = new SqlCommand("AddUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", TxtName.Text.Trim());
            cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text.Trim());
            cmd.Parameters.AddWithValue("@ssn", TxtSSN.Text.Trim());
            cmd.Parameters.AddWithValue("@mobile", TxtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@email", TxtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.Add("@result", SqlDbType.Int);
            cmd.Parameters["@result"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
            return res;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validation.IsThisNameValid(TxtName.Text.Trim()) == false)
                {
                    MessageBox.Show("Name is Invalid");
                    return;
                }
                if (Validation.IsThisNameValid(TxtLastName.Text.Trim()) == false)
                {
                    MessageBox.Show("LastName is Invalid");
                    return;
                }
                if (Validation.IsThisSSNValid(TxtSSN.Text.Trim()) == false)
                {
                    MessageBox.Show("SSN is Invalid");
                    return;
                }
                if (Validation.IsThisPhoneNumberValid(TxtMobile.Text.Trim()) == false)
                {
                    MessageBox.Show("Phone Number is Invalid");
                    return;
                }
                if (Validation.IsThisEmailValid(TxtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Email is Invalid");
                    return;
                }
                string username = GenerateUser();
                string password = GeneratePass();
                int res = Func(username, password);
                if (res == 1)
                {
                    MessageBox.Show("User Exists");
                }
                while (res == 2)
                {
                    username = GenerateUser();
                    password = GeneratePass();
                    res = Func(username, password);
                }
                if (res == 3)
                {
                    string frommail = "samansayad93@gmail.com";
                    string mailpassword = "jtgkwdggxxydxqnq";
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(frommail,mailpassword);
                    smtpClient.EnableSsl = true;
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("samansayad93@gmail.com");
                    mailMessage.Subject = "Your Post Automation Username and Password";
                    mailMessage.Body = $"<html><body><h1>Your username and passsword for sign in</h1></br><h2>Username: {username} </h2><h2>Password: {password}</h2></body></html>";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(TxtEmail.Text.Trim()));
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("User Added successfully and email send");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
