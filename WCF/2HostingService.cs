// -------------------------------------------------------------------------------
// Hosting service in console application 
// Create end point using App.config (declarative method ) 
// -------------------------------------------------------------------------------

> Create new C# project
> Select WCF Service Library 
> Name it GirishLibrary
> Rename Default interface as IGirish
> Rename default service class as Girish
> Build and run. Check that service can be run using visual studio hosting
> Then add new project "GirishConsoleHost"
> Add reference of System.ServiceModel 
> Add reference of Local project GirishLibrary
> Declare end points in App.config

> Create new project in another solution. For writing client application 
> Select console application typeof
> Right click on project and select Add service reference
> Use address of service as per config file . for e.g net.tcp://localhost:9090/MyTcpEndPoint
> Now create proxy client object and invoke method of service. 

// ---------------------
// 1. Interface
// ---------------------
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


// ---------------------
// 2. Service class 
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
            return string.Format("Hey You entered: {0}", value);
        }
       
    }
}


// ---------------------
// 3. Console Host
// ---------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using GirishLibrary;

namespace GirishConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost Sh = new ServiceHost(typeof(Girish)); 
            Sh.Open();

            Console.WriteLine("Started.....");

            foreach (var item in Sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
                Console.WriteLine();
            }

            Console.ReadLine();

            Sh.Close();
        }
    }
}


// --------------------
// 4. App.config
// --------------------

 <system.serviceModel>
        <behaviors>
            <serviceBehaviors>
                <behavior name="MyBeh">
                    <serviceMetadata/>
                </behavior>
            </serviceBehaviors>
        </behaviors>
        <services>
            <service name="GirishLibrary.Girish" behaviorConfiguration="MyBeh">
                <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
                    bindingConfiguration="" contract="GirishLibrary.IGirish" />
                
                <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />
            </service>
        </services>
    </system.serviceModel>
    
	
// --------------------
// 5. Client 
// --------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApp1.ServiceReference1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GirishClient client = new GirishClient();
            var output = client.GetData(11);
            Console.WriteLine("Service output:"+output);
        }
    }
}


