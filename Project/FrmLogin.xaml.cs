using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmLogin.xaml
    /// </summary>
    public partial class FrmLogin : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            FrmSignUp frmSignUp = new FrmSignUp();
            frmSignUp.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("LOGIN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TxtPassword.Text.Trim());
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (res == 1)
                {
                    FrmAdmin frmAdmin = new FrmAdmin();
                    frmAdmin.ShowDialog();
                    this.Close();
                }
                else if (res == 2)
                {
                    FrmUser frmUser = new FrmUser();
                    frmUser.ShowDialog();
                    this.Close();
                }
                else if (res == 3)
                {
                    MessageBox.Show("User does not exists");
                    TxtUsername.Text = "";
                    TxtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
