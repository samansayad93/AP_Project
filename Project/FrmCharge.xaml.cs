using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                if (Validation.IsCardValid(TxtCardNumber.Text.Trim()) == false)
                {
                    MessageBox.Show("Card Number is Invalid");
                    return;
                }
                if (Validation.IsThisCVVValid(TxtCVV.Text.Trim()) == false)
                {
                    MessageBox.Show("CVV2 is Invalid");
                    return;
                }
                if (Validation.IsExpired(Convert.ToDateTime($"{Convert.ToInt32(TxtExpire2.Text)}/{1}/{2000 + Convert.ToInt32(TxtExpire1.Text)}")) == false)
                {
                    MessageBox.Show("Your Card is Expired");
                    return;
                }
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
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.FileName = "Receipt.pdf";
                    if(dialog.ShowDialog() == true)
                    {
                        Document doc = new Document(iTextSharp.text.PageSize.A5);
                        PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream(dialog.FileName, FileMode.Create));
                        string index = $"Your Receipt\nYour SSN: {App.Current.Properties["SSN"]}\nAmount: {TxtAmount.Text.Trim()}\nDate and Time: {DateTime.Now.ToString()}\nYour Current Balance: {TxtCharge.Text.Trim()}</h2>";
                        doc.Open();
                        iTextSharp.text.Paragraph par = new iTextSharp.text.Paragraph(index);
                        doc.Add(par);
                        doc.Close();
                        MessageBox.Show("Your Receipt Saved Successfully");
                    }
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
