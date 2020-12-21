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

using System.ServiceModel;
using System.ServiceModel.Description;
using WpfApp1.ServiceReference1;
using System.ServiceModel.Channels;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceCaller caller = null;

        public MainWindow()
        {
            InitializeComponent();
            caller = new ServiceCaller();
            caller.ReceivedData += OnDataReceived;
            caller.ErrorOccured += OnErrorOccured;
            caller.ConnectionStatusChanged += OnConnectionStatusChanged;

            LogText.Background = new SolidColorBrush(Colors.Black);
            LogText.Foreground = new SolidColorBrush(Colors.Green);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => LogText.Text += "\nButton clicked");
            try
            {
                caller.GetData(Int32.Parse(InputNumber.Text.ToString()));
            } catch(Exception ex)
            {
                Dispatcher.Invoke(() => LogText.Text += "\n"+ex.Message);
                Console.WriteLine("Girish Exception:"+ex.Message);
            }
            
        }

        public void OnDataReceived(object source, EventArgs args)
        {
            var data = (DataArgs)args;
            Console.WriteLine("Received Data from servicecaller");
            Dispatcher.Invoke(() => LogText.Text += "\nservice says:" + data.Data);
        }

        public void OnErrorOccured(object source, EventArgs args)
        {
            var data = (DataArgs)args;
            Console.WriteLine("Received exception from servicecaller");
            Dispatcher.Invoke(() => LogText.Text += "\n"+data.Data );
        }

        public void OnConnectionStatusChanged(object source, EventArgs args)
        {
            var data = (OnlineStatusArgs)args;
            Dispatcher.Invoke(() => Status.Text = "Online:" + data.Onlineflag.ToString());
        }

        
    }
}
