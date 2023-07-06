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
                cmd.Parameters.AddWithValue("@password", TxtPassword.Password.Trim());
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (res == 1)
                {
                    App.Current.Properties["Type"] = "admin";
                    FrmAdmin frmAdmin = new FrmAdmin();
                    this.Close();
                    frmAdmin.ShowDialog();
                }
                else if (res == 2)
                {
                    SqlDataAdapter da = new SqlDataAdapter("FindSSNByUsername",con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@username",TxtUsername.Text.Trim());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    App.Current.Properties["Type"] = "user";
                    App.Current.Properties["SSN"] = dt.Rows[0][0].ToString();
                    FrmUser frmUser = new FrmUser();
                    this.Close();
                    frmUser.ShowDialog();
                }
                else if (res == 3)
                {
                    MessageBox.Show("User does not exists");
                    TxtUsername.Text = "";
                    TxtPassword.Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
