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
                if (Validation.IsThisUserValid(TxtUsername.Text.Trim()) == false)
                {
                    MessageBox.Show("Username is Invalid");
                    return;
                }
                if (Validation.IsThisUserPassValid(TxtPassword.Text.Trim()) == false)
                {
                    MessageBox.Show("Password is Invalid");
                    return;
                }
                if (TxtPassword.Text.Trim() != TxtConfirmPassword.Text.Trim())
                {
                    MessageBox.Show("Passwords are not same");
                    return;
                }
                SqlCommand cmd = new SqlCommand("UpdateUserPass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TxtPassword.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Your Username and Password changed successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
