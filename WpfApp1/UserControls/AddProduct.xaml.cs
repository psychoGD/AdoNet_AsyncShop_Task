using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Entities;

namespace WpfApp1.UserControls
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    /// 


    public partial class AddProduct : UserControl
    {
        private static readonly Regex _regex = new Regex("[0-9.,]");
        public BitmapImage img { get; set; }
        public AddProduct()
        {
            InitializeComponent();

            background_image.Effect = new BlurEffect();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            var parent = this.Parent as Grid;
            foreach (var item in parent.Children)
            {
                if (item is Grid)
                {
                    var item2 = item as Grid;
                    item2.Effect = null;
                }
            }

            parent.Children.Remove(this);
        }

        //private void OpenFolderBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog op = new OpenFileDialog();
        //    op.Title = "Select a picture";
        //    op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
        //      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        //      "Portable Network Graphic (*.png)|*.png";
        //    if (op.ShowDialog() == true)
        //    {
        //        if (op.CheckFileExists == true)
        //        {
        //            img = new BitmapImage(new Uri(op.FileName));
        //            ProductPath.Text = op.FileName;
        //        }

        //    }
        //}

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductName.Text.Length > 3 && ProductPrice.Text.Length > 0)
            {
                
                    if (double.Parse(ProductPrice.Text) > 0)
                    {
                        var parent = this.Parent as Grid;
                        var mainWindow = parent.Parent as MainWindow;
                        
                        mainWindow.Products.Add(new Products
                        {
                            Pname = ProductName.Text,
                            Pprice = ProductPrice.Text,
                            //Pimg = img,

                        });

                        Button_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter price");
                    }
            }
            else
            {

                MessageBox.Show("Error: Name Should Be Longer Than 3 Character Or Price Cannot Be Empty");
            }
        }

        private void ProductPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_regex.IsMatch(e.Text);
        }
    }
}
