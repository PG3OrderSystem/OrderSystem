using OrderSystem.Views.Management;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            AdminTxtBox.Focus();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = AdminTxtBox.Text;
            string password = PassTxtBox.Password;

            if (DataAccess.CheckLogin(id, password))
            {
                NavigationService.Navigate(new TopManagementPage());
            }
            else
            {
                MessageBox.Show("Id・Passwordが違います。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TopPage());
        }
    }
}