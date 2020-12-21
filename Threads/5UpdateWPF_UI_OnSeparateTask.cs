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

        private async void button1_Click(object sender, RoutedEventArgs e)
        {

            await Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Counter:" + (i + 1));
                    Dispatcher.Invoke(() =>
                    {
                        text1.Text = " Counter:" + (i + 1);
                    });
                    Thread.Sleep(1000);
                }
            });

            text1.Text = "Task is finished!";
        }
    }
}
