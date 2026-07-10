using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for TopPage.xaml
    /// </summary>
    public partial class TopPage : Page
    {
        private List<CartItem> cartItems = new List<CartItem>();

        public class CartItem
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
            public int Subtotal { get; set; }
        }

        public TopPage()
        {
            InitializeComponent();
            ListDataGrid.ItemsSource = cartItems;
        }

        public void AddToCart(Models.Products product)
        {
            var existing = cartItems.FirstOrDefault(c => c.ProductId == product.ProductId);

            if (existing != null)
            {
                existing.Quantity++;
                existing.Subtotal = existing.Price * existing.Quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1,
                    Subtotal = product.Price
                });
            }

            ListDataGrid.Items.Refresh();

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
            if (cartItems.Count == 0)
            {
                MessageBox.Show("商品を選択してください", "注文エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int orderId = DataAccess.SaveOrder(cartItems.Sum(c => c.Subtotal), cartItems);
            NavigationService.Navigate(new LastPage(cartItems, orderId));
        }

        private void ManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.LoginPage());
        }
    }
}