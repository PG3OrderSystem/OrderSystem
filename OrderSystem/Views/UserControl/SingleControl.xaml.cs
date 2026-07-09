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
    /// Interaction logic for SingleControl.xaml
    /// </summary>
    public partial class SingleControl : UserControl
    {
        private TopPage _parentPage;

        public SingleControl(TopPage parentPage)
        {
            InitializeComponent();
            _parentPage = parentPage;
            LoadProducts("Single");
        }



        private void LoadProducts(string category)
        {
            var products = DataAccess.GetProductsByCategory(category);

            foreach (var product in products)
            {
                var btn = new Button
                {
                    // Display name + price on the button
                    Content = $"{product.ProductName}\n¥{product.Price}",
                    Width = 120,
                    Height = 70,
                    Margin = new Thickness(10),
                    Tag = product
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
