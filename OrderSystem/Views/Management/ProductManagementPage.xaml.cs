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
    /// Interaction logic for ProductManagementPage.xaml
    /// </summary>
    public partial class ProductManagementPage : Page
    {
        public ProductManagementPage()
        {
            InitializeComponent();
            LoadItems();
        }



        public void LoadItems()
        {
            try
            {
                    dataGridProducts.ItemsSource = DataAccess.GetAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "一覧表示エラー",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var products = new Products
                {
                    ProductId = ProductIdTxtBox.Text,
                    ProductName = ProductNameTxtBox.Text,
                    Price = int.Parse(ProductPriceTxtBox.Text),
                    Category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                };
                DataAccess.AddProduct(products);
                MessageBox.Show("情報を追加しました。");
                LoadItems();
                Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"追加に失敗しました。\n{ex.Message}");
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Products
                {
                    ProductId = ProductIdTxtBox.Text,
                    ProductName = ProductNameTxtBox.Text,
                    Price = int.Parse(ProductPriceTxtBox.Text),
                    Category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                };
                DataAccess.UpdateProduct(product);
                MessageBox.Show("商品情報を更新しました。");
                LoadItems();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新に失敗しました。\n{ex.Message}");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("本当に削除しますか？", "確認", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DataAccess.DeleteProduct(ProductIdTxtBox.Text);
                    MessageBox.Show("商品情報を削除しました。");
                    LoadItems();
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"削除に失敗しました。\n{ex.Message}");
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = ProductIdTxtBox.Text.Trim();
                string name = ProductNameTxtBox.Text.Trim();
                string priceText = ProductPriceTxtBox.Text.Trim();
                string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                var results = DataAccess.SearchProducts(id, name, category, priceText);

                if (results.Any())
                {
                    dataGridProducts.ItemsSource = results;
                }
                else
                {
                    MessageBox.Show("入力されたデータがありません。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索に失敗しました。\n{ex.Message}");
            }
        }

        public void Clear()
        { 
            ProductIdTxtBox.Text = "";
            ProductNameTxtBox.Text = "";
            ProductPriceTxtBox.Text = "";
            CategoryComboBox.SelectedIndex = -1;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
