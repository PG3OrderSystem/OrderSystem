using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ReceiptIdTxtBox.Text, out int orderId))
            {
                MessageBox.Show("有効な注文IDを入力してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = DataAccess.GetReceipt(orderId);

            if (result == null)
            {
                MessageBox.Show("指定された注文が見つかりません。", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ListDataGrid.ItemsSource = result.Value.Details;
            TotalSumTxtBlock.Text = $"合計: ¥{result.Value.TotalAmount}";
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
                MessageBox.Show("表示するレシートがありません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ReceiptIdTxtBox.Text = string.Empty;
            ListDataGrid.ItemsSource = null;
            TotalSumTxtBlock.Text = "合計: ¥0";
        }
    }
}