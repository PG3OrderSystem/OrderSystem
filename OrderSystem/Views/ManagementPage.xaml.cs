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
            LoadItems();
        }



        public void LoadItems()
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

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TopPage());
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new OrderDBContext())
                {
                    var products = new Products
                    {
                        ProductId = ProductIdTxtBox.Text,
                        ProductName = ProductNameTxtBox.Text,
                        Price = int.Parse(ProductPriceTxtBox.Text),
                        Category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };
                    context.Products.Add(products);
                    context.SaveChanges();
                    MessageBox.Show("情報を追加しました。");
                    LoadItems();
                    Clear();
                }
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
                using (var context = new OrderDBContext())
                {
                    string id = ProductIdTxtBox.Text;
                    var product = context.Products.Find(id);

                    if (product != null)
                    {
                        product.ProductName = ProductNameTxtBox.Text;
                        product.Price = int.Parse(ProductPriceTxtBox.Text);
                        product.Category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                        context.SaveChanges();
                        MessageBox.Show("商品情報を更新しました。");
                        LoadItems();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("該当する商品が見つかりません。");
                    }
                }
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
                    using (var context = new OrderDBContext())
                    {
                        string id = ProductIdTxtBox.Text;
                        var product = context.Products.Find(id);

                        if (product != null)
                        {
                            context.Products.Remove(product);
                            context.SaveChanges();

                            MessageBox.Show("商品情報を削除しました。");
                            LoadItems();
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("該当する商品が見つかりません。");
                        }

                    }
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
                using (var context = new OrderDBContext())
                {
                    string id = ProductIdTxtBox.Text.Trim();
                    string name = ProductNameTxtBox.Text.Trim();
                    string priceText = ProductPriceTxtBox.Text.Trim();
                    string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                    var query = context.Products.AsQueryable();

                    bool anyInput = !string.IsNullOrWhiteSpace(id)
                        || !string.IsNullOrWhiteSpace(name)
                        || !string.IsNullOrWhiteSpace(category)
                        || !string.IsNullOrWhiteSpace(priceText);

                    if (anyInput)
                    {
                        query = query.Where(p =>
                            (!string.IsNullOrWhiteSpace(id) && p.ProductId.Contains(id)) ||
                            (!string.IsNullOrWhiteSpace(name) && p.ProductName.Contains(name)) ||
                            (!string.IsNullOrWhiteSpace(category) && p.Category == category) ||
                            (!string.IsNullOrWhiteSpace(priceText) && p.Price.ToString().Contains(priceText))
                        );
                    }


                    //ProductIdTxtBox.Text = id;
                    //ProductNameTxtBox.Text = name;
                    //ProductPriceTxtBox.Text = priceText;
                    //CategoryComboBox.ItemsSource = category;



                    var results = query.ToList();

                    if (results.Any())
                    {
                        dataGridProducts.ItemsSource = results;
                    }
                    else
                    {
                        MessageBox.Show("入力されたデータがありません。");
                    }
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
    }
}
