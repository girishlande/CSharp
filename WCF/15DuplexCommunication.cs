
// This is demo of How one should create WCF service and how to use DUPLEX communication between client and WCF service.

// 1. Interface 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract = typeof(IGirishCallback))]
    public interface IGirish
    {
        [OperationContract(IsOneWay =true)]
        void GetData(int value);


        [OperationContract(IsOneWay = true)]
        void IsOnline();

    }

    public interface IGirishCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendDataCallback(string data);


        [OperationContract(IsOneWay = true)]
        void SendOnlineCallback(bool flag);
    }
}


// 2. Class 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        public void GetData(int value)
        {
            Thread.Sleep(4000);
            string data =  string.Format(" Square of {0} is : {1} ", value, value*value);
            IGirishCallback cb = OperationContext.Current.GetCallbackChannel<IGirishCallback>();
            cb.SendDataCallback(data);
        }
       
        public void IsOnline()
        {
            IGirishCallback cb = OperationContext.Current.GetCallbackChannel<IGirishCallback>();
            cb.SendOnlineCallback(true);
        }
    }
}


// 3. Host 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using GirishLibrary;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri netPipeBase = new Uri("net.pipe://localhost/GirishPipe");
            var binding = new NetNamedPipeBinding();
            binding.Security.Mode = NetNamedPipeSecurityMode.None;
            ServiceHost Sh = new ServiceHost(typeof(Girish), new Uri[] { netPipeBase });
            
            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IGirish), binding, netPipeBase);
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            Sh.Description.Behaviors.Add(smb);

            ServiceEndpoint pipeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                                                "net.pipe://localhost/GirishPipe/mex");

            Sh.Open();

            Console.WriteLine("Named pipe Binding Service started...");
            Console.ReadLine();
        }
    }
}


// 4. WPF Client 

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
			
			// Creating service handler 
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


// Client class to deal with service


namespace WpfApp1
{

    public class DataArgs : EventArgs
    {
        public string Data { get; set; }
    }

    public class OnlineStatusArgs : EventArgs
    {
        public bool Onlineflag { get; set; }
    }

    public class ServiceCaller : IGirishCallback
    {
        GirishClient client = null;
        System.Timers.Timer KeepaliveTimer = null;
        public event EventHandler<DataArgs> ReceivedData;
        public event EventHandler<DataArgs> ErrorOccured;
        public event EventHandler<OnlineStatusArgs> ConnectionStatusChanged;

        public bool ValidClientState { get; set; }

        public ServiceCaller()
        {
            InitialiseClient();
            InitialiseTimer();
        }

        public void InitialiseTimer()
        {
            if (KeepaliveTimer == null)
            {
                KeepaliveTimer = new System.Timers.Timer();
                KeepaliveTimer.Interval = 1000;
                KeepaliveTimer.Elapsed += KeepAlive;
                KeepaliveTimer.Start();
            }
        }

        void InitialiseClient()
        {
            ValidClientState = true;
            try
            {
                if (client == null)
                {
                    var context = new InstanceContext(this);
                    client = new GirishClient(context);
                }
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new DataArgs() { Data = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        void DisposeClient()
        {
            ValidClientState = false;
            client.Abort();
            client = null;
        }

        public void GetData(int data)
        {
            InitialiseClient();
            try
            {
                client.GetData(data);
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new DataArgs() { Data = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        public void SendDataCallback(string adata)
        {
            DataArgs dataargs = new DataArgs() { Data = adata };
            ReceivedData(this, dataargs);
        }

        private void KeepAlive(Object source, EventArgs args)
        {
            InitialiseClient();
            try
            {
                if (client!=null)
                client.IsOnline();
            }
            catch (Exception ex)
            {
                DisposeClient();
                ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = ValidClientState });
            }
        }

        public void SendOnlineCallback(bool aflag)
        {
             ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = aflag });
        }
    }
}



