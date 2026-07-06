using OrderSystem.Models;
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
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class ManagementPage : Page
    {
        public ManagementPage()
        {
            InitializeComponent();
            LoadMembers();
        }



        public void LoadMembers()
        {
            try
            {
                using (var context = new OrderDBContext())
                {
                    var products = context.Products.OrderBy(x => x.ProductId).ToList();
                    dataGridProducts.ItemsSource = products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "一覧表示エラー",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TopPageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TopPage());
        }
    }
}
