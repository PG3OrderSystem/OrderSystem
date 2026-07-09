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
    /// Interaction logic for TopPage.xaml
    /// </summary>
    public partial class TopPage : Page
    {
        public TopPage()
        {
            InitializeComponent();
            ListDataGrid.ItemsSource = cartItems;
        }


        private List<CartItem> cartItems = new List<CartItem>();


        public class CartItem
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
            public int Subtotal { get; set; }
            

        }


        public void AddToCart(Models.Products product)
        {
            // Check if this product is already in the cart
            var existing = cartItems.FirstOrDefault(c => c.ProductId == product.ProductId);

            if (existing != null)
            {
                // Already in cart → just increase quantity
                existing.Quantity++;
                existing.Subtotal = existing.Price * existing.Quantity;
            }
            else
            {
                // Not in cart yet → add new row
                cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1,
                    Subtotal = product.Price
                });
            }

            // Refresh the DataGrid to show the changes
            ListDataGrid.Items.Refresh();

            // Update the total amount text
            int total = cartItems.Sum(c => c.Subtotal);
            TotalSumTxtBlock.Text = $"合計: ¥{total}";
        }














        private void CallStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("スタッフを呼びました。\n少々お待ちください。", "呼び出し完了", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SetControl(this);
        }

        private void SingleBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SingleControl(this);
        }



        private void DrinkBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new DrinksControl(this);
        }

        private void SideBtn_Click(object sender, RoutedEventArgs e)
        {
            MiddleContent.Content = new SideControl(this);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            cartItems.Clear();

            ListDataGrid.Items.Refresh();

            TotalSumTxtBlock.Text = "合計: ¥0";
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Views.LastPage(cartItems));

            if (cartItems.Count == 0)
            {
                MessageBox.Show("商品を選択してください", "注文エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using (var context = new OrderDBContext())
            {
                var order = new Order
                {
                    DateTime = DateTime.Now,
                    TotalAmount = cartItems.Sum(c => c.Subtotal)
                };
                context.Orders.Add(order);
                context.SaveChanges();

                foreach (var item in cartItems)
                {
                    var detail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Subtotal = item.Subtotal
                    };
                    context.OrderDetails.Add(detail);
                }
                context.SaveChanges();

                NavigationService.Navigate(new LastPage(cartItems, order.OrderId));
            }
        }

    
        

        private void ManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.LoginPage());
        }





















        



    }
}
