using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for FrmReport.xaml
    /// </summary>
    public partial class FrmReport : Window
    {
        readonly SqlConnection con = new SqlConnection("server= DESKTOP-C4DNQ9C; Database= DbPost; Integrated security=true");

        public FrmReport()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ssn, type, posttype;
                int price;
                double weight;
                if (TxtUserSSN.Text.Trim().Length <= 0)
                    ssn = "%";
                else
                    ssn = TxtUserSSN.Text.Trim();
                if (CmbType.Text == "All")
                    type = "%";
                else
                    type = CmbType.Text;
                if (TxtPrice.Text.Trim().Length <= 0)
                    price = 0;
                else
                    price = Convert.ToInt32(TxtPrice.Text.Trim());
                if (TxtWeight.Text.Trim().Length <= 0)
                    weight = 0;
                else
                    weight = Convert.ToDouble(TxtWeight.Text.Trim());
                if (CmbPostType.Text == "All")
                    posttype = "%";
                else
                    posttype = CmbPostType.Text;
                SqlDataAdapter da = new SqlDataAdapter("AdvanceSearch", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@userssn", ssn);
                da.SelectCommand.Parameters.AddWithValue("@type", type);
                da.SelectCommand.Parameters.AddWithValue("@price", price);
                da.SelectCommand.Parameters.AddWithValue("@weight", weight);
                da.SelectCommand.Parameters.AddWithValue("@posttype", posttype);
                DataTable dt = new DataTable();
                da.Fill(dt);
                StreamWriter sw = new StreamWriter("D:\\project\\uni\\AP_Project\\Report.csv", false);
                //headers    
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
                MessageBox.Show("Your Report Has successfully Created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
