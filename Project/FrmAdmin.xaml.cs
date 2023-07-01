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
    /// Interaction logic for FrmAdmin.xaml
    /// </summary>
    public partial class FrmAdmin : Window
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void M00_Click(object sender, RoutedEventArgs e)
        {
            FrmSignUser frmSignUser = new FrmSignUser();
            frmSignUser.ShowDialog();
        }

        private void M10_Click(object sender, RoutedEventArgs e)
        {
            FrmSearch frmSearch = new FrmSearch();
            frmSearch.ShowDialog();
        }

        private void M30_Click(object sender, RoutedEventArgs e)
        {
            FrmInformation frmInformation = new FrmInformation();
            frmInformation.ShowDialog();
        }

        private void M20_Click(object sender, RoutedEventArgs e)
        {
            FrmReport report = new FrmReport();
            report.ShowDialog();
        }
    }
}
