using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    /// Interaction logic for FrmChangeInformation.xaml
    /// </summary>
    public partial class FrmChangeInformation : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmChangeInformation()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtPassword.Password.Trim() != TxtConfirmPassword.Password.Trim())
                {
                    MessageBox.Show("Passwords are not same");
                    return;
                }
                SqlCommand cmd = new SqlCommand("UpdateUserPass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TxtPassword.Password.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Your Username and Password changed successfully");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
