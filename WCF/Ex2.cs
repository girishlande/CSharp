// This excercise will show how to Create WCF service and host using multiple end points using declarative approach 

// ----------------
// 1. Interface 
// ----------------

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AjitLibrary
{
    [ServiceContract]
    public interface IGirish
    {
        [OperationContract]
        int AddNumber(int x, int y);

        [OperationContract]
        int SubtractNumber(int x, int y);
    }
}


// ----------------
// 2. Service class 
// ----------------

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
            Console.WriteLine("Ajit::GetData() called with input:"+value);
            return string.Format("You entered: {0}", value);
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjitLibrary
{
    public class Girish : IGirish
    {
        public int AddNumber(int x, int y)
        {
            Console.WriteLine("Girish::AddNumber called with x:" + x + " y:"+y);
            return x + y;
        }

        public int SubtractNumber(int x, int y)
        {
            Console.WriteLine("Girish::SubtractNumber called with x:" + x + " y:" + y);
            return x - y;
        }
    }
}


// ---------------
// 3. Host 
// ---------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AjitLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace AjitConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var Sh1 = new ServiceHost(typeof(Ajit));
            Sh1.Open();
            Console.WriteLine("Started Service Ajit..");

            var Sh2 = new ServiceHost(typeof(Girish));
            Sh2.Open();
            Console.WriteLine("Started Service Girish..");

            Console.ReadLine();
        }
    }
}

// ---------------
// 4. App.config
// ---------------

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>

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
				<endpoint address="net.tcp://localhost:9090/Ajit" binding="netTcpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />
				<endpoint address="net.tcp://localhost:9090/Ajit/mex" binding="mexTcpBinding"
						bindingConfiguration="" contract="IMetadataExchange" />
			</service>

			<service name="AjitLibrary.Girish" behaviorConfiguration="MyBeh">
				<endpoint address="net.tcp://localhost:9090/Girish" binding="netTcpBinding"
                    bindingConfiguration="" contract="AjitLibrary.IGirish" />
				<endpoint address="net.tcp://localhost:9090/Girish/mex" binding="mexTcpBinding"
						bindingConfiguration="" contract="IMetadataExchange" />
			</service>
		</services>
	</system.serviceModel>
	
</configuration>

// ---------------
// 5. Client 
// ---------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AjitLibraryClient.ServiceReference1;
using AjitLibraryClient.ServiceReference2;

namespace AjitLibraryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ajitclient = new AjitClient();
            var output = ajitclient.GetData(11);
            Console.WriteLine("Ajit service output:"+output);
            Console.WriteLine();

            var girishclient = new GirishClient();
            Console.WriteLine("Girish service output: " + girishclient.AddNumber(10,20));
            Console.WriteLine("Girish service output: " + girishclient.SubtractNumber(10, 20));
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
