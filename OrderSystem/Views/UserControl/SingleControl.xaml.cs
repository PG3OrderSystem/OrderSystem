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
        public SingleControl()
        {
            InitializeComponent();
            LoadProducts("Single");
        }



        private void LoadProducts(string category)
        {
            using (var context = new Models.OrderDBContext())
            {
                var products = context.Products
                    .Where(p => p.Category == category)
                    .ToList();

                foreach (var product in products)
                {
                    var btn = new Button
                    {
                        // Display name + price on the button
                        Content = $"{product.ProductName}\n¥{product.Price}",
                        Width = 123,
                        Height = 70,
                        Margin = new Thickness(10),
                        Tag = product
                    };

                    ProductPanel.Children.Add(btn);
                }
            }
        }

    }
}
