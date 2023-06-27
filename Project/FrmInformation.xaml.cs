using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                if(CmbStatus.Text == "Delivered")
                {
                    SqlDataAdapter da = new SqlDataAdapter("FindEmailBySSN", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@userssn",TxtSendSSN.Text.Trim());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var email = dt.Rows[0][0].ToString();
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 465;
                    smtpClient.EnableSsl = true;
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("samansayad93@gmail.com");
                    mailMessage.Subject = "Your Post Has Been Delivered";
                    mailMessage.Body = $"<html><body><h1>Your Post Box with ID {TxtPostID.Text} has been delivered</h1></br><h2>You can Put Your Comment for this Post Box in your Panel</h2></body></html>";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(email));
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
