using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for FrmAddPost.xaml
    /// </summary>
    public partial class FrmAddPost : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmAddPost()
        {
            InitializeComponent();
        }

        private double Calculate()
        {
            double basev = 10000;
            if(CmbType.Text == "Document")
            {
                basev *= 1.5;
            }
            if(CmbType.Text == "Fragile")
            {
                basev *= 2;
            }
            if(ChckValuable.IsChecked == true)
            {
                basev *= 2;
            }
            double weigth = double.Parse(TxtWeigth.Text.Trim());
            if (weigth > 0.5)
            {
                int tmp = (int)(weigth / 0.5) - 1;
                basev *= tmp * 1.2;
            }
            if(CmbPostType.Text == "Forehand")
            {
                basev *= 1.5;
            }
            return basev;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = false;
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validation.IsThisPhoneNumberValid(TxtPhone.Text.Trim()) == false)
                {
                    MessageBox.Show("Phone Number is Invalid");
                    return;
                }
                double price = Calculate();
                TxtPrice.Text = price.ToString();
                TxtSLocation.IsEnabled = false;
                TxtRLocation.IsEnabled = false;
                CmbPostType.IsEnabled = false;
                CmbType.IsEnabled = false;
                ChckValuable.IsEnabled = false;
                TxtPhone.IsEnabled = false;
                TxtWeigth.IsEnabled = false;
                BtnCalculate.IsEnabled = false;
                BtnSubmit.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddPost", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sendSSN", App.Current.Properties["UserSSN"]);
                cmd.Parameters.AddWithValue("@sendloc", TxtSLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@receiveloc", TxtRLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@valueable", Convert.ToBoolean(ChckValuable.IsChecked));
                cmd.Parameters.AddWithValue("@type",CmbType.Text.Trim());
                cmd.Parameters.AddWithValue("@posttype",CmbPostType.Text.Trim());
                cmd.Parameters.AddWithValue("@weight",Convert.ToDouble(TxtWeigth.Text.Trim()));
                cmd.Parameters.AddWithValue("@phone",TxtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@price",Convert.ToDouble(TxtPrice.Text.Trim()));
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
                if(res == 1)
                {
                    MessageBox.Show("Not Enough Money in Wallet");
                }
                else if(res == 2)
                {
                    MessageBox.Show("Your Post Added successfully\nYour Pst ID: {}");
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
