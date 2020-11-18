using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MulServiceLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpBaseAddress = new Uri("net.tcp://localhost:6789");
            Uri httpBaseAddress = new Uri("http://localhost:6790/MyHttpEndPoint");

            ServiceHost Sh = new ServiceHost(typeof(MulService), new Uri[] { tcpBaseAddress, httpBaseAddress });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IMulService), new NetTcpBinding(), tcpBaseAddress);
            ServiceEndpoint httpSe = Sh.AddServiceEndpoint(typeof(IMulService), new BasicHttpBinding(), httpBaseAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            Sh.Description.Behaviors.Add(smb);

            ServiceEndpoint httpSeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                                MetadataExchangeBindings.CreateMexHttpBinding(),
                                                                "http://localhost:6790/MyHttpEndPoint/mex");

            ServiceEndpoint tcpSeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                                MetadataExchangeBindings.CreateMexTcpBinding(),
                                                                "net.tcp://localhost:6789/mex");

            Sh.Open();

            Console.WriteLine("Started.....");

            foreach (var item in Sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
            }

            Console.ReadLine();

            Sh.Close();
        }
    }
}
