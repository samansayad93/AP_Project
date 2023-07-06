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
    /// Interaction logic for FrmUser.xaml
    /// </summary>
    public partial class FrmUser : Window
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        private void M00_Click(object sender, RoutedEventArgs e)
        {
            FrmReportUser frmReport = new FrmReportUser();
            frmReport.ShowDialog();
        }

        private void M10_Click(object sender, RoutedEventArgs e)
        {
            FrmInformationUser user = new FrmInformationUser();
            user.ShowDialog();
        }

        private void M20_Click(object sender, RoutedEventArgs e)
        {
            FrmCharge frmCharge = new FrmCharge();
            frmCharge.ShowDialog();
        }

        private void M30_Click(object sender, RoutedEventArgs e)
        {
            FrmChangeInformation frmChange = new FrmChangeInformation();
            frmChange.ShowDialog();
        }
    }
}
