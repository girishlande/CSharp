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
using WpfClient.ServiceReference1;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GirishClient client = null;
        public MainWindow()
        {
            InitializeComponent();
            client = new GirishClient();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SyncProcess();
        }

        void SyncProcess()
        {
            var x = client.ServiceAsyncMethod("Hello Girish");
            Console.WriteLine("output:" + x);
            text1.Text = x;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AsyncProcess();
        }

        async void AsyncProcess()
        {
            var y = await client.ServiceAsyncMethodAsync("Hello Ajit");
            Console.WriteLine("output:" + y);
            text2.Dispatcher.Invoke(() =>
            {
                text2.Text = y;
            });
        }


    }
}
