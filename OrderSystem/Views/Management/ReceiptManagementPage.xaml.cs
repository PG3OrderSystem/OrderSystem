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



namespace OrderSystem.Views.Management
{
    /// <summary>
    /// Interaction logic for ReceiptManagementPage.xaml
    /// </summary>
    public partial class ReceiptManagementPage : Page
    {
        public ReceiptManagementPage()
        {
            InitializeComponent();
            LoadOrders();
        }


        public class DisplayItem
        {
            public string ProductName { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
            public int Subtotal { get; set; }
        }





        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ReceiptIdTxtBox.Text, out int orderId))
            {
                MessageBox.Show("有効な注文IDを入力してください。");
                return;
            }

            using (var context = new OrderDBContext())
            {
                var order = context.Orders
                    .Where(o => o.OrderId == orderId)
                    .Select(o => new
                    {
                        Details = o.OrderDetails.Select(d => new DisplayItem
                        {
                            ProductName = d.Product.ProductName,
                            Price = d.Product.Price,
                            Quantity = d.Quantity,
                            Subtotal = d.Subtotal
                        }).ToList(),
                        o.TotalAmount
                    })
                    .FirstOrDefault();

                if (order == null)
                {
                    MessageBox.Show("指定された注文が見つかりません。");
                    return;
                }

                ListDataGrid.ItemsSource = order.Details;
                TotalSumTxtBlock.Text = $"合計: ¥{order.TotalAmount}";
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PrntBtn_Click(object sender, RoutedEventArgs e)
        {
            var items = ListDataGrid.ItemsSource as System.Collections.IList;
            if (items == null || items.Count == 0)
            {
                MessageBox.Show("表示するレシートがありません。");
                return;
            }

            var dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                dlg.PrintVisual(this, "Receipt");
            }
            
            

        }





        void LoadOrders()
        {
            OrderDataGrid.ItemsSource = DataAccess.GetOrdersById();
        }









    }
}
