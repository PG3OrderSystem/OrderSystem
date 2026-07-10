using System;
using System.Windows;
using System.Windows.Controls;

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

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}