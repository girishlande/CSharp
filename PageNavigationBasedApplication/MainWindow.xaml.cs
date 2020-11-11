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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Page1 page1;
        Page2 page2;
        Page3 page3;

        public MainWindow()
        {
            InitializeComponent();
            page1 = new Page1();
            page1.ShowsNavigationUI = false;
            page2 = new Page2() { ShowsNavigationUI=false };
            page3 = new Page3();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame1.Content = page1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame1.Content = page2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frame1.Content = page3;
        }

        private void frame1_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
