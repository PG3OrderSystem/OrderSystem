using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OrderSystem.Views
{
    /// <summary>
    /// Interaction logic for SetControl.xaml
    /// </summary>
    public partial class SetControl : UserControl
    {
        private TopPage _parentPage;

        public SetControl(TopPage parentPage)
        {
            InitializeComponent();
            _parentPage = parentPage;
            LoadProducts("Set");
        }

        private void LoadProducts(string category)
        {
            var products = DataAccess.GetProductsByCategory(category);

            foreach (var product in products)
            {
                var btn = new Button
                {
                    Content = $"🍽️ {product.ProductName}\n¥{product.Price}",
                    Width = 110,
                    Height = 70,
                    Margin = new Thickness(10),
                    Tag = product,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FFE67E22")
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