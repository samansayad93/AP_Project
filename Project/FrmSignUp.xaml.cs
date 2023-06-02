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
    /// Interaction logic for FrmSignUp.xaml
    /// </summary>
    public partial class FrmSignUp : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmSignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Validation.IsThisNameValid(TxtName.Text.Trim()) == false)
            {
                MessageBox.Show("Name is Invalid");
                return;
            }
            if (Validation.IsThisNameValid(TxtLastName.Text.Trim()) == false)
            {
                MessageBox.Show("LastName is Invalid");
                return;
            }
            if(Validation.IsThisIDValid(TxtPersonalID.Text.Trim()) == false)
            {
                MessageBox.Show("Personal ID is Invalid");
                return;
            }
            if(Validation.IsThisPasswordValid(TxtPassword.Text.Trim())== false)
            {
                MessageBox.Show("Password is Invalid");
                return;
            }
            if (Validation.IsThisEmailValid(TxtEmail.Text.Trim()) == false)
            {
                MessageBox.Show("Email is Invalid");
                return;
            }
            if (TxtRePassword.Text.Trim() != TxtPassword.Text.Trim())
            {
                MessageBox.Show("Not Same Password");
                TxtPassword.Text = "";
                TxtRePassword.Text = "";
                return;
            }

            SqlCommand cmd = new SqlCommand("AddAdmin", con);
            cmd.CommandType =  CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID",int.Parse(TxtPersonalID.Text));
            cmd.Parameters.AddWithValue("@name",TxtName.Text.Trim());
            cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text.Trim());
            cmd.Parameters.AddWithValue("@username",TxtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@password",TxtRePassword.Text.Trim());
            cmd.Parameters.AddWithValue("@email",TxtEmail.Text.Trim());
            cmd.Parameters.Add("@result", SqlDbType.Int);
            cmd.Parameters["@result"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
            if(res == 1)
            {
                MessageBox.Show("User Exists");
            }
            else if(res == 0)
            {
                MessageBox.Show("Admin Added Successfully");
            }
            this.Close();
        }
    }
}
