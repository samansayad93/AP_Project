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

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmAddPost.xaml
    /// </summary>
    public partial class FrmAddPost : Window
    {
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
    }
}
