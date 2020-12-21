// -------------------------------------------------------------------------------------------------------------------
// This demo is explaining how state of the service is reflected depending upon which binding we select
// in this demo client can opt for HTTP, TCP and NET PIPE binding for testing service behavior 
// We notice that by default Service will treat each call as separate for HTTP whereas it will hold session information for TCP
// -------------------------------------------------------------------------------------------------------------------

> Create WCF service and host it using HTTP, TCP and NET PIPE binding
> At Client side switch to different binding and observe the output. 

// --------------------
// 1. Interface 
// --------------------

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

        [OperationContract]
        List<Student> GetStudents();

        [OperationContract]
        void AddStudent(Student s);
    }

    public class Student {
        public int Roll { get; set; }
        public string Name { get; set; }
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

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        private List<Student> m_Students;
        Girish()
        {
            m_Students = new List<Student>();
            m_Students.Add(new Student() { Name = "Girish", Roll = 11 });
        }

        public void AddStudent(Student s)
        {
            m_Students.Add(s);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<Student> GetStudents()
        {
            return m_Students;
        }
    }
}

// ------------------
// 3. Host 
// ------------------

using GirishLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var sh = new ServiceHost(typeof(Girish));
            sh.Open();
            Console.WriteLine("Started service... \n");

            foreach (var item in sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}


// ------------------
// 4. Configuration 
// ------------------

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
			<service name="GirishLibrary.Girish" behaviorConfiguration="MyBeh">
				
				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
					bindingConfiguration="" contract="GirishLibrary.IGirish" />
				<endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
						bindingConfiguration="" contract="IMetadataExchange" />

				<endpoint address="http://localhost:9091/MyHttpEndPoint" binding="basicHttpBinding"
                    bindingConfiguration="" contract="GirishLibrary.IGirish" />
				<endpoint address="http://localhost:9091/MyHttpEndPoint/mex" binding="mexHttpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />


				<endpoint address="net.pipe://localhost/MyPipeEndPoint" binding="netNamedPipeBinding"
                    bindingConfiguration="" contract="GirishLibrary.IGirish" />
				<endpoint address="net.pipe://localhost/MyPipeEndPoint/mex" binding="mexNamedPipeBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />
			
			</service>
		</services>
	</system.serviceModel>

</configuration>

// -------------
// 5. Client 
// -------------

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
            GirishClient client = new GirishClient("BasicHttpBinding_IGirish");
            //GirishClient client = new GirishClient("NetTcpBinding_IGirish");
            //GirishClient client = new GirishClient("NetNamedPipeBinding_IGirish");

            var s = client.GetStudents();
            Console.WriteLine("\nStudents:");
            foreach (var stud in s)
            {
                Console.WriteLine(stud.Roll + ":" + stud.Name);
            }
            client.AddStudent(new Student() { Roll = 12, Name = "Ajit" });
            s = client.GetStudents();
            Console.WriteLine("\nStudents:");
            foreach (var stud in s)
            {
                Console.WriteLine(stud.Roll + ":" + stud.Name);
            }
            client.AddStudent(new Student() { Roll = 13, Name = "Suhas" });
            s = client.GetStudents();
            Console.WriteLine("\nStudents:");
            foreach (var stud in s)
            {
                Console.WriteLine(stud.Roll + ":" + stud.Name);
            }

        }
    }
}


