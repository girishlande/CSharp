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

namespace GuiTestAppWPF
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

            // Creating service handler 
            caller = new ServiceCaller();
            caller.RecievedTablePosition += OnTablePositionUpdated;
            caller.ErrorOccured += OnErrorOccured;
            caller.ConnectionStatusChanged += OnConnectionStatusChanged;
        }

        public void OnTablePositionUpdated(object source, EventArgs args)
        {
            var data = (DataArgs)args;
            Console.WriteLine("Table Position Updated:" + data.TablePosition);
            UpdateLogText("Table Position changed to:" + data.TablePosition);

        }

        public void OnErrorOccured(object source, EventArgs args)
        {
            var data = (MessageArgs)args;
            Console.WriteLine("Error Occured in Service:" + data.Message);
            UpdateLogText(data.Message);
        }

        public void OnConnectionStatusChanged(object source, EventArgs args)
        {
            var data = (OnlineStatusArgs)args;
            Dispatcher.Invoke(() =>
            {
                if (data.Onlineflag)
                {
                    ConnectionStatusText.Content = "Connection Status: CONNECTED";
                    StatusBar.Fill = new SolidColorBrush(System.Windows.Media.Colors.GreenYellow);
                }
                else
                {
                    ConnectionStatusText.Content = "Connection Status: DISCONNECTED";
                    StatusBar.Fill = new SolidColorBrush(System.Windows.Media.Colors.OrangeRed);
                }
            });
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caller.MoveTable(Int32.Parse(TableTargetPositionText.Text.ToString()));
            }
            catch (Exception ex)
            {
                UpdateLogText(ex.Message);
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        private void UpdateLogText(string text)
        {
            Dispatcher.Invoke(() => { LogText.Text += "\n" + text; LogText.ScrollToEnd(); });
        }
    }
}
