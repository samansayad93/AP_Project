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
            SqlCommand cmd = new SqlCommand("LOGIN", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", TxtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@password", TxtPassword.DataContext.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
            if(res == 0)
            {

            }
            else if (res == 1)
            {

            }
            else if (res == 2)
            {

            }
        }
    }
}
