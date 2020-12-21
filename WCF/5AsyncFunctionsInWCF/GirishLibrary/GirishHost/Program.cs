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

            Uri netPipeBase = new Uri("net.pipe://localhost/Girish");
            var binding = new NetNamedPipeBinding();
            binding.Security.Mode = NetNamedPipeSecurityMode.None;
            ServiceHost Sh = new ServiceHost(typeof(Girish), new Uri[] { netPipeBase });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IGirish), binding, netPipeBase);
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            Sh.Description.Behaviors.Add(smb);

            ServiceEndpoint pipeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                                                "net.pipe://localhost/Girish/mex");

            Sh.Open();

            Console.WriteLine("Named pipe Binding Service started...");
            Console.ReadLine();
        }
    }
}
