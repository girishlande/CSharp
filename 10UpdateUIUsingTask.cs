using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Synchronous execution started");

            f1();
            
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ASynchronous execution started");
            Task.Run(() => f1());
        }

        async void f1()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Function f1() completed");
        }

    }
}
