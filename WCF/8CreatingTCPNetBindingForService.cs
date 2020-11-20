
// Following is example of creating TCP net binding for the service. 
// THis indicates that all the contents of data is preserved. 

// ----------------------------------
// 1. Interface of Service 
// ----------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SuhasServiceLibrary
{
    public class Course {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        void AddCourse(Course c);

        [OperationContract]
        List<Course> GetCourses();
    }

  
}



// ----------------------------------
// 2. Service Class 
// ----------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SuhasServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private List<Course> m_courses;

        Service1()
        {
            m_courses = new List<Course>();
            m_courses.Add(new Course() { CourseId = 11, CourseName = "Math" });
        }

        public void AddCourse(Course c)
        {
            m_courses.Add(c);
        }

        public List<Course> GetCourses()
        {
            return m_courses;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        
    }
}

// ----------------------------------
// 3. Service Host 
// ----------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuhasServiceLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SuhasServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpBaseAddress = new Uri("net.tcp://localhost:6789");
            
            ServiceHost Sh = new ServiceHost(typeof(Service1), new Uri[] { tcpBaseAddress });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IService1), new NetTcpBinding(), tcpBaseAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            Sh.Description.Behaviors.Add(smb);

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


// ----------------------------------
// 4. Service Client 
// ----------------------------------

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
            Service1Client client = new Service1Client();
            var output = client.GetData(11);
            Console.WriteLine("service output:" + output);
            client.AddCourse(new Course() { CourseName = "physics", CourseId = 12 });
            client.AddCourse(new Course() { CourseName = "English", CourseId = 14 });
            client.AddCourse(new Course() { CourseName = "Marathi", CourseId = 16 });

            var courses = client.GetCourses();
            foreach (var x in courses)
            {
                Console.WriteLine(x.CourseId + ":" + x.CourseName);
            }
        }
    }
}


