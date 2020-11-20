// -------------------------------------------------------------------------------------------
// This will demonstrate how to configure multiple end points using configuration file
// (declarative method) 
// -------------------------------------------------------------------------------------------

// --------------------
// 1. Interface 
// --------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AjitLibrary
{
    [ServiceContract]
    public interface IAjit
    {
        [OperationContract]
        string GetData(int value);
    }
}

// --------------------
// 2. Service class 
// --------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AjitLibrary
{
    public class Ajit : IAjit
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
     
    }
}

// --------------------
// 3. App.config 
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
			<service name="AjitLibrary.Ajit" behaviorConfiguration="MyBeh">
				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />

				<endpoint address="http://localhost:9091/MyHttpEndPoint" binding="basicHttpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="http://localhost:9091/MyHttpEndPoint/mex" binding="mexHttpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />

				<endpoint address="net.pipe://localhost/MyPipeEndPoint" binding="netNamedPipeBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="net.pipe://localhost/MyPipeEndPoint/mex" binding="mexNamedPipeBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />


			</service>
		</services>
	</system.serviceModel>
	
// --------------------
// 4. Host 
// --------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using AjitLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sh = new ServiceHost(typeof(Ajit));
            sh.Open();
            Console.WriteLine("Started service...");
            foreach (var item in sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
                Console.WriteLine();
            }

            Console.ReadLine();

            sh.Close();
            Console.ReadLine();
        }
    }
}

// ---------------------
// 5. Client 
// ---------------------


using ConsoleApp2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new AjitClient("NetNamedPipeBinding_IAjit");
            //var client = new AjitClient("BasicHttpBinding_IAjit");
            var client = new AjitClient("NetTcpBinding_IAjit");
            var output = client.GetData(11);
            Console.WriteLine("service output:"+output);
            Console.ReadLine();

        }
    }
}


