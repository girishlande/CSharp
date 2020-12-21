// This exercise will show how to create WCF service. 
// How to host it using IMPERATIVE end point creation 

> Start visual studio . Create new project . Select WCF library. 
> Rename default service interface. Add operation in it. 
> Build this service. It will generate dll file. 
> Add new console project for hosting this WCF library 
> Add reference of System.serviceModel and WCF library project
> Use imperative code to create service end points and declaring binding and behavior. 
> Run this console application 

> Create another console project for writing client application for WCF service
> Right click project and add service reference. Give Address at which service is hosted. 
> Now create instance of proxy class and call service methods. 

// ---------------------
// 1. Interface 
// ---------------------

using System.ServiceModel;

namespace GirishLibrary
{
    [ServiceContract]
    public interface IAjit
    {

        [OperationContract]
        int AddNumber(int x, int y);
    }
}


using System.ServiceModel;
namespace GirishLibrary
{
    [ServiceContract]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);
    }

}


// ---------------------
// 2. service Class 
// ---------------------

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirishLibrary
{
    public class Ajit : IAjit
    {
        public int AddNumber(int x, int y)
        {
            Console.WriteLine("Ajit::AddNumber() is called ");
            return x + y;
        }
    }
}


// ---------------------
// 3. Host 
// ---------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GirishLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Service 1 
            Uri httpBaseAddress = new Uri("http://localhost:6790");
            var Sh = new ServiceHost(typeof(Ajit), new Uri[] { httpBaseAddress });
            Sh.AddServiceEndpoint(typeof(IAjit), new BasicHttpBinding(), httpBaseAddress);

            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            Sh.Description.Behaviors.Add(smb);

            Sh.Open();
            Console.WriteLine("Started.....");
            foreach (var item in Sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
            }


            Console.ReadLine();
        }
    }
}


// ---------------------
// 4. Client 
// ---------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GirishClient.ServiceReference1;

namespace GirishClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AjitClient client = new AjitClient();
            var output = client.AddNumber(10, 20);
            Console.WriteLine("Service Output:" + output );

        }
    }
}
