using OrderSystem.Views;
using System.Windows;
using System.Windows.Controls;

namespace OrderSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PageFrame.Navigate(new Views.TopPage());
        }
    }
}