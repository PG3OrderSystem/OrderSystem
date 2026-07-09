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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderSystem.Views.Management
{
    /// <summary>
    /// Interaction logic for TopManagementPage.xaml
    /// </summary>
    public partial class TopManagementPage : Page
    {
        public TopManagementPage()
        {
            InitializeComponent();
        }

        private void ProductManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductManagementPage());
        }

        private void ReceiptManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReceiptManagementPage());
        }
    }
}
