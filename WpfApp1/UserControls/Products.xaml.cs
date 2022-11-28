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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {


        public string Pname
        {
            get { return (string)GetValue(PnameProperty); }
            set { SetValue(PnameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pname.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PnameProperty =
            DependencyProperty.Register("Pname", typeof(string), typeof(Products));


        public string Pprice
        {
            get { return (string)GetValue(PpriceProperty); }
            set { SetValue(PpriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pprice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PpriceProperty =
            DependencyProperty.Register("Pprice", typeof(string), typeof(Products));



        //public Image Pimg
        //{
        //    get { return (Image)GetValue(PimgProperty); }
        //    set { SetValue(PimgProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Pimg.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PimgProperty =
        //    DependencyProperty.Register("Pimg", typeof(Image), typeof(Products));


        //public BitmapImage Pimg
        //{
        //    get { return (BitmapImage)GetValue(PimgProperty); }
        //    set { SetValue(PimgProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Pimg.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PimgProperty =
        //    DependencyProperty.Register("Pimg", typeof(BitmapImage), typeof(Products));




        public Products()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
