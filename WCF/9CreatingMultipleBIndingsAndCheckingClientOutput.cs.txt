
// THis example demonstrates that you can create multiple bindings available for client. 
// Depending upon which binding client choses he will get different output from the service 
// Because some bindings support session data(for e.g TCP) and Some do not(for e.g HTTP).  

// ------------------------
// 1. Interface 
// ------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AAService
{
    public class Student {
        public int Roll { get; set; }
        public string Name { get; set; }
    }

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        List<Student> GetStudents();

        [OperationContract]
        void AddStudent(Student s);

    }
    
}

// ------------------------
// 2. Class 
// ------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AAService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        List<Student> m_students;

        Service1()
        {
            m_students = new List<Student>();
            m_students.Add(new Student() { Roll = 11, Name = "Girish" });
            m_students.Add(new Student() { Roll = 12, Name = "Suhas" });
        }
        public void AddStudent(Student s)
        {
            m_students.Add(s);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
      

        public List<Student> GetStudents()
        {
            return m_students;
        }
    }
}


// ------------------------
// 3. Host 
// ------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AAService;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpBaseAddress = new Uri("net.tcp://localhost:6789");
            Uri httpBaseAddress = new Uri("http://localhost:6790/MyHttpEndPoint");

            ServiceHost Sh = new ServiceHost(typeof(Service1), new Uri[] { tcpBaseAddress, httpBaseAddress });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IService1), new NetTcpBinding(), tcpBaseAddress);
            ServiceEndpoint httpSe = Sh.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), httpBaseAddress);

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
                Console.WriteLine();
            }

            Console.ReadLine();

            Sh.Close();
        }
    }
}


// ------------------------
// 4. Client 
// ------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.ServiceReference1;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client("NetTcpBinding_IService1");
            //Service1Client client = new Service1Client("BasicHttpBinding_IService1");
            client.AddStudent(new Student() { Roll = 13, Name = "Ajit" });
            client.AddStudent(new Student() { Roll = 14, Name = "Prabhu" });

            var students = client.GetStudents();
            foreach(var s in students)
            {
                Console.WriteLine("Student Roll:"+s.Roll + " Name:"+s.Name);
            }
        }
    }
}


// -------------------------------------------------------------------------------------
// 5. Output (Service1Client client = new Service1Client("NetTcpBinding_IService1");
// -------------------------------------------------------------------------------------
Student Roll:11 Name:Girish
Student Roll:12 Name:Suhas
Student Roll:13 Name:Ajit
Student Roll:14 Name:Prabhu
Press any key to continue . . .

// -------------------------------------------------------------------------------------
// 6. Output Service1Client client = new Service1Client("BasicHttpBinding_IService1");
// -------------------------------------------------------------------------------------
Student Roll:11 Name:Girish
Student Roll:12 Name:Suhas
Press any key to continue . . .



