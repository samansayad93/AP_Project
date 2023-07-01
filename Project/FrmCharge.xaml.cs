using IronPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
    /// Interaction logic for FrmCharge.xaml
    /// </summary>
    public partial class FrmCharge : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmCharge()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("FindCharge", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                TxtCharge.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if(Validation.IsCardValid(TxtCardNumber.Text.Trim()) == false)
                //{
                //    MessageBox.Show("Card Number is Invalid");
                //    return;
                //}
                //if (Validation.IsThisCVVValid(TxtCVV.Text.Trim()) == false)
                //{
                //    MessageBox.Show("CVV2 is Invalid");
                //    return;
                //}
                //if(Validation.IsExpired(Convert.ToInt32(TxtExpire1.Text.Trim()),Convert.ToInt32(TxtExpire2.Text.Trim())) == false)
                //{
                //    MessageBox.Show("Your Card is Expired");
                //    return;
                //}
                SqlCommand cmd = new SqlCommand("UpdateWallet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(TxtAmount.Text.Trim()));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter da = new SqlDataAdapter("FindCharge", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@userssn", App.Current.Properties["SSN"].ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                TxtCharge.Text = dt.Rows[0][0].ToString();
                if (MessageBox.Show("Your Account has been charged successfully.\nDo you want Receipt?", "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var pdf = new ChromePdfRenderer();
                    var doc = pdf.RenderHtmlAsPdf($"<h1>Your Receipt</h1><h2>Your SSN: {App.Current.Properties["SSN"]}</h2><h2>Amount: {TxtAmount.Text.Trim()}</h2><h2>Date and Time: {DateTime.Now.ToString()}</h2><h2>Your Current Balance: {TxtCharge.Text.Trim()}</h2>");
                    doc.SaveAs("D:\\project\\uni\\AP_Project\\Reciept.pdf");
                }
                this.Close();
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
