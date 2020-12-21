// This is demo of how to create and host a WCF service using named pipe binding 

// 1. Interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);
    }
}

// 2. Class 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
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


// 4. Client

 public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new GirishClient();
                var output = client.GetData(11);
                text1.Content = "Girish";
            }
            catch (Exception ex)
            {
                text1.Content = ex.Message;
            }
            
        }
    }