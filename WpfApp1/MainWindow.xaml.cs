using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Products> Products { get; set; }=new ObservableCollection<Products>(){ };

        public MainWindow()
        {
            InitializeComponent();
            GetDataFromDB();
            this.DataContext = this;
            Products product = new Products();
            product.Pname = "Table";
            product.Pprice = "12";
            //product.Pimg = new BitmapImage(new Uri(@"C:\Users\Huseyn\source\repos\WpfApp1\WpfApp1\images\Table-PNG-File.png"));
            Products.Add(product);

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in LeftMenu_WrapPanel.Children)
            {
                if(item is Button)
                {
                    var item2 = item as Button;
                    if (item2.Foreground == Brushes.Black)
                    {
                        item2.Foreground = Brushes.Gray;
                    }
                }
            }
            
            var sender2 = sender as Button;
            sender2.Foreground = Brushes.Black;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SearchTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTxtBox.Text.Length <= 0)
            {
                SearchTxtBox.Text = "Search Product";
                SearchTxtBox.Foreground = Brushes.Gray;
            }
        }

        private void SearchTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTxtBox.Text == "Search Product")
            {
                var sender2 = sender as TextBox;
                sender2.Text = "";
                sender2.Foreground = Brushes.Black;
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            SecondMainGrid.Effect = new BlurEffect();

            UserControls.AddProduct addProduct = new UserControls.AddProduct();
            addProduct.Width = 400;
            addProduct.Height = 300;
            this.MainGrid.Children.Add(addProduct);

        }
        public async void  GetDataFromDB()
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "WAITFOR Delay '00:00:05';SELECT * FROM Product";

                var table = new DataTable();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    do
                    {
                        var hasColumnAdded = false;
                        while (await reader.ReadAsync())
                        {
                            //Yarattigimiz Cedvele Datatabledaki column larin adini kopyalamq ucun
                            //1 defe olsun deye hascolumnAdded ile yoxlayiriq 
                            if (!hasColumnAdded)
                            {
                                hasColumnAdded = true;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    table.Columns.Add(reader.GetName(i));
                                }
                            }
                            //bu hissede ise bir row yaradiriq rowun sxemasi tablemizin sxemasiyla
                            //eyni olsun deye table.NewRow edirik

                            //Sonra ise rowun columnuna readerdan aldigimiz datalari doldurub rowu table rowsa elave edirik
                            var row = table.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = await reader.GetFieldValueAsync<Object>(i);
                                //var test = ;
                            }
                            var arr = row.ItemArray.ToArray();
                            Products.Add(new Products { Pname = arr[0].ToString(), Pprice = arr[1].ToString() });
                            table.Rows.Add(row);
                            
                        }
                    } while (reader.NextResult());

                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetDataFromDB();
            MyGrid.ItemsSource=Products;
        }
    }
}
