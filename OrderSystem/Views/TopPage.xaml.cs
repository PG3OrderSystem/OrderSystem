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

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for TopPage.xaml
    /// </summary>
    public partial class TopPage : Page
    {
        public TopPage()
        {
            InitializeComponent();
        }

        private void CallStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("スタッフを呼びました。\n少々お待ちください。", "呼び出し完了", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SetControl();
        }

        private void SingleBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SingleControl();
        }

        

        private void DrinkBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new DrinksControl();
        }

        private void SideBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SideControl();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.LastPage());
        }

        private void ManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("管理者用ページに移動します。", "管理者ページ", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (MessageBoxResult.Yes == MessageBox.Show("管理者用ページに移動します。\nよろしいですか？", "管理者ページ", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                NavigationService.Navigate(new Views.ManagementPage());
            }

        }
}
}
