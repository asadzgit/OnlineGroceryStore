using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OnlineGroceryStore.ViewModels;

namespace OnlineGroceryStore.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    //usercontrol class for all available products screen view
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel();
        }
    }
}
