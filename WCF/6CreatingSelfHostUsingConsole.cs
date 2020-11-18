// ------------------------------------------------------------------
// Creating self host console application for hosting a WCF service
// ------------------------------------------------------------------

1. Create WCF library and test service using visual studio hosting and test application
2. Create console application project in c#
3. Right click on console application project and add reference of WCF library project
4. Right click on console application project and add reference of System.ServiceModel
5. Add using statetements for WCF library and System.ServiceModel and System.ServiceModel.Description
6. Create Uri object 
    Uri baseaddress = new Uri("http://localhost:6758");
7. Create ServiceHost object. 
    ServiceHost Sh = new ServiceHost(typeof(GirishService), baseaddress);
8. Create Service End point. 
    ServiceEndPoint Se = Sh.AddServiceEndPoint(typeof(IGirishService), new WSHttpBinding(), baseaddress);
9. Add service meta data behavior
   ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
   smb.HttpGetEnabled = true;
   Sh.Description.Behaviors.Add(smb);
   
10. open host 
    Sh.open();

11. Wait for some key and close host.
    Console.ReadLine();
	Sh.close();

12. Run this application . (NOTE : YOU MUST BE ADMIN TO HOST THE SERVICE)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AjitServiceLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace AjitServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:6789");

            ServiceHost Sh = new ServiceHost(typeof(AjitService), baseAddress);

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IAjitService), new WSHttpBinding(), baseAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            Sh.Description.Behaviors.Add(smb);

            Sh.Open();


            Console.WriteLine("Started.....");
            Console.ReadLine();

            Sh.Close();

        }
    }
}

