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
    /// Interaction logic for FrmSearch.xaml
    /// </summary>
    public partial class FrmSearch : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmSearch()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if(Validation.IsThisSSNValid(TxtSearch.Text.Trim()) == false)
                //{
                //    MessageBox.Show("SSN is Invalid");
                //    return;
                //}
                App.Current.Properties["UserSSN"] = TxtSearch.Text.Trim();
                SqlCommand cmd = new SqlCommand("SearchUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ssn", App.Current.Properties["UserSSN"]);
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if (res == 1)
                {
                    //FrmAddPost frmAddPost = new FrmAddPost();
                    //this.Close();
                    //frmAddPost.ShowDialog();
                    FrmAddPost frmAddPost = new FrmAddPost();
                    this.Close();
                    frmAddPost.ShowDialog();
                }
                else if (res == 2)
                {
                    //FrmSignUser frmSignUser = new FrmSignUser();
                    //this.Close();
                    //frmSignUser.ShowDialog();
                    FrmAddPost frmAddPost = new FrmAddPost();
                    this.Close();
                    frmAddPost.ShowDialog();
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
