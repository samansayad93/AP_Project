using System;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmInformation.xaml
    /// </summary>
    public partial class FrmInformation : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmInformation()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SearchPost", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@postid", Convert.ToInt32(TxtPostID.Text.Trim()));
                da.SelectCommand.Parameters.Add("@result", SqlDbType.Int);
                da.SelectCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                DataTable dt = new DataTable();
                da.Fill(dt);
                int res = Convert.ToInt32(da.SelectCommand.Parameters["@result"].Value);
                if (res == 1)
                {
                    MessageBox.Show("Post Does Not Found");
                }
                else if (res == 2)
                {
                    BtnUpdate.IsEnabled = false;
                    CmbStatus.IsEnabled = false;
                    TxtSendSSN.Text = dt.Rows[0][1].ToString();
                    TxtSLocation.Text = dt.Rows[0][2].ToString();
                    TxtRLocation.Text = dt.Rows[0][3].ToString();
                    CmbType.Text = dt.Rows[0][4].ToString();
                    ChckValuable.IsChecked = Convert.ToBoolean(dt.Rows[0][5].ToString());
                    TxtWeigth.Text = dt.Rows[0][6].ToString();
                    CmbPostType.Text = dt.Rows[0][7].ToString();
                    TxtPhone.Text = dt.Rows[0][8].ToString();
                    TxtPrice.Text = dt.Rows[0][9].ToString();
                    CmbStatus.Text = dt.Rows[0][10].ToString();
                    TxtComment.Text = dt.Rows[0][11].ToString();
                    if(CmbStatus.Text != "Delivered")
                    {
                        BtnUpdate.IsEnabled = true;
                        CmbStatus.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdatePostStatus",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@postid",TxtPostID.Text.Trim());
                cmd.Parameters.AddWithValue("@status",CmbStatus.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Post Status has successfully changed");
                if (CmbStatus.Text == "Delivered")
                {
                    SqlDataAdapter da = new SqlDataAdapter("findemailbyssn", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@userssn", TxtSendSSN.Text.Trim());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var email = dt.Rows[0][0].ToString();
                    string frommail = "samansayad93@gmail.com";
                    string mailpassword = "jtgkwdggxxydxqnq";
                    SmtpClient smtpclient = new SmtpClient("smtp.gmail.com");
                    smtpclient.Port = 587;
                    smtpclient.Credentials = new NetworkCredential(frommail, mailpassword);
                    smtpclient.EnableSsl = true;
                    var mailmessage = new MailMessage();
                    mailmessage.From = new MailAddress("samansayad93@gmail.com");
                    mailmessage.Subject = "your post has been delivered";
                    mailmessage.Body = $"<html><body><h1>your post box with id {TxtPostID.Text} has been delivered</h1></br><h2>you can put your comment for this post box in your panel</h2></body></html>";
                    mailmessage.IsBodyHtml = true;
                    mailmessage.To.Add(new MailAddress(email));
                    smtpclient.Send(mailmessage);
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
