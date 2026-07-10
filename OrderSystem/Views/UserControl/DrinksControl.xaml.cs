using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for DrinksControl.xaml
    /// </summary>
    public partial class DrinksControl : UserControl
    {
        private TopPage _parentPage;

        public DrinksControl(TopPage parentPage)
        {
            InitializeComponent();
            _parentPage = parentPage;
            LoadProducts("Drinks");
        }

        private void LoadProducts(string category)
        {
            var products = DataAccess.GetProductsByCategory(category);

            foreach (var product in products)
            {
                var btn = new Button
                {
                    Content = $"🥤 {product.ProductName}\n¥{product.Price}",
                    Width = 110,
                    Height = 70,
                    Margin = new Thickness(10),
                    Tag = product,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FF2980B9")
                };

                btn.Click += (s, e) =>
                {
                    var clickedProduct = (Models.Products)((Button)s).Tag;
                    _parentPage.AddToCart(clickedProduct);
                };

                ProductPanel.Children.Add(btn);
            }
        }
    }
}