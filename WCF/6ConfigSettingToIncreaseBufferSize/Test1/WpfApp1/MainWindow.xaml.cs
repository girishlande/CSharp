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
using WpfApp1.GirishServiceReference;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GirishClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new GirishClient();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(text1.Text);
            if (n > 0)
            {
                string result = new String('-', n);
                byte[] bytes = Encoding.ASCII.GetBytes(result);
                try
                {
                    client.Process(bytes);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                    text2.Text += "Exception:" + ex.Message + "\n";
                }
            }
        }
    }
}
