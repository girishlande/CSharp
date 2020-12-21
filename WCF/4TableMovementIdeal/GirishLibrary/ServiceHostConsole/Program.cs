using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using GirishLibrary;

namespace ServiceHostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri netPipeBase = new Uri("net.pipe://localhost/TableMover");
            var binding = new NetNamedPipeBinding();
            binding.Security.Mode = NetNamedPipeSecurityMode.None;
            ServiceHost Sh = new ServiceHost(typeof(TableMover), new Uri[] { netPipeBase });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(ITableMover), binding, netPipeBase);
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            Sh.Description.Behaviors.Add(smb);

            ServiceEndpoint pipeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                                                "net.pipe://localhost/TableMover/mex");

            Sh.Open();

            Console.WriteLine("Named pipe Binding Service started...");
            Console.ReadLine();
        }
    }
}
