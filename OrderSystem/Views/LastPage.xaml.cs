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
using static OrderSystem.Views.TopPage;

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for LastPage.xaml
    /// </summary>
    public partial class LastPage : Page
    {
        public LastPage(List<CartItem> cartItems)
        {
            InitializeComponent();
            ListDataGrid.ItemsSource = cartItems;
            int total = cartItems.Sum(c => c.Subtotal);
            TotalSumTxtBlock.Text = $"合計: ¥{total}";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.TopPage());
        }


    }
}
