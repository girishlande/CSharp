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
using GirishLibrary;

namespace GirishHostWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost host;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            buttonStart.IsEnabled = false;

            Uri tcpAdrs = new Uri("net.tcp://localhost:9098/WPFHost/");
            Uri httpAdrs = new Uri("http://localhost:9099/WPFHost/");

            Uri[] baseAdresses = { tcpAdrs, httpAdrs };
            host = new ServiceHost(typeof(GirishLibrary.Girish), baseAdresses);

            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None, true);
            //Updated: to enable file transefer of 64 MB
            tcpBinding.MaxBufferPoolSize = (int)67108864;
            tcpBinding.MaxBufferSize = 67108864;
            tcpBinding.MaxReceivedMessageSize = (int)67108864;
            tcpBinding.TransferMode = TransferMode.Buffered;
            tcpBinding.ReaderQuotas.MaxArrayLength = 67108864;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 67108864;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 67108864;

            tcpBinding.MaxConnections = 100;
            //To maxmize MaxConnections you have to assign another port for mex endpoint

            //and configure ServiceThrottling as well
            ServiceThrottlingBehavior throttle;
            throttle = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
            if (throttle == null)
            {
                throttle = new ServiceThrottlingBehavior();
                throttle.MaxConcurrentCalls = 100;
                throttle.MaxConcurrentSessions = 100;
                host.Description.Behaviors.Add(throttle);
            }


            //Enable reliable session and keep the connection alive for 20 hours.
            tcpBinding.ReceiveTimeout = new TimeSpan(20, 0, 0);
            tcpBinding.ReliableSession.Enabled = true;
            tcpBinding.ReliableSession.InactivityTimeout = new TimeSpan(20, 0, 10);

            host.AddServiceEndpoint(typeof(GirishLibrary.IGirish), tcpBinding, "tcp");

            //Define Metadata endPoint, So we can publish information about the service
            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(mBehave);

            host.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexTcpBinding(),
                "net.tcp://" + "localhost:9097/WPFHost/mex");


            try
            {
                host.Open();
            }
            catch (Exception ex)
            {
                description.Text = ex.Message.ToString();
            }
            finally
            {
                if (host.State == CommunicationState.Opened)
                {
                    labelStatus.Content = "Opened";
                    buttonStop.IsEnabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                try
                {
                    host.Close();
                }
                catch (Exception ex)
                {
                    labelStatus.Content = ex.Message.ToString();
                }
                finally
                {
                    if (host.State == CommunicationState.Closed)
                    {
                        labelStatus.Content = "Closed";
                        buttonStart.IsEnabled = true;
                        buttonStop.IsEnabled = false;
                    }
                }
            }
        }

        private void textBoxIP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       

    }
}
