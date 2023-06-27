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
    /// Interaction logic for FrmInformationUser.xaml
    /// </summary>
    public partial class FrmInformationUser : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmInformationUser()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SearchPostUser", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@postid", Convert.ToInt32(TxtPostID.Text.Trim()));
                da.SelectCommand.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                da.SelectCommand.Parameters.Add("@result", SqlDbType.Int);
                da.SelectCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                DataTable dt = new DataTable();
                da.Fill(dt);
                int res = Convert.ToInt32(da.SelectCommand.Parameters["@result"].Value);
                if (res == 1)
                {
                    MessageBox.Show("Post Does Not Found or It's Not Yours");
                }
                else if (res == 2)
                {
                    TxtSLocation.Text = dt.Rows[0][2].ToString();
                    TxtRLocation.Text = dt.Rows[0][3].ToString();
                    CmbType.Text = dt.Rows[0][4].ToString();
                    ChckValuable.IsChecked = Convert.ToBoolean(dt.Rows[0][5].ToString());
                    TxtWeigth.Text = dt.Rows[0][6].ToString();
                    CmbType.Text = dt.Rows[0][7].ToString();
                    TxtPhone.Text = dt.Rows[0][8].ToString();
                    TxtPrice.Text = dt.Rows[0][9].ToString();
                    CmbStatus.Text = dt.Rows[0][10].ToString();
                    TxtComment.Text = dt.Rows[0][11].ToString();
                    if(CmbStatus.Text == "Delivered")
                    {
                        BtnUpdate.IsEnabled = true;
                        TxtComment.IsEnabled = true;
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
                SqlCommand cmd = new SqlCommand("UpdatePostComment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@postid", TxtPostID.Text.Trim());
                cmd.Parameters.AddWithValue("@comment", TxtComment.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Post Comment has successfully changed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
