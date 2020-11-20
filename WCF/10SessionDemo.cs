
// Following files demonstrates use of sessions in WCF. 
// following decorator in Service class decides session mode. 
// [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// It can be one of the InstanceContextMode.Single, InstanceContextMode.PerCall, InstanceContextMode.PerSession
// 1. If its single you will see that service will return you all the courses from all the sessions.
//      you can create multiple instances of client application and all the courses will get reflected here. 
// 2. PerCall -> it will forget course immediately and you will not get any course 
// 3. PerSession -> it will remember courses of given session and will return those courses only. 

// ----------------
// 1. Interface
// ----------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishServiceLibrary
{
    [DataContract]
    public class Course
    {
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public string CourseName { get; set; }
    }

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ICourses
    {
        [OperationContract(IsInitiating = true)]
        void AddToCourses(Course course);

        [OperationContract(IsTerminating = true)]
        List<Course> ListCourses();
    }

}


// ----------------
// 2. Service Class 
// ----------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Courses : ICourses
    {
        private List<Course> courses;

        public Courses()
        {
            courses = new List<Course>();
        }

        public void AddToCourses(Course course)
        {
            courses.Add(course);
        }

        public List<Course> ListCourses()
        {
            return courses;
        }
    }
}



// ----------------
// 3. Host 
// ----------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using GirishServiceLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Uri tcpBaseAddress = new Uri("net.tcp://localhost:6789/");

            //Uri httpBaseAddress = new Uri("http://localhost:6790");

            ServiceHost Sh = new ServiceHost(typeof(Courses), new Uri[] { tcpBaseAddress });

            var tcpBinding = new NetTcpBinding();
            tcpBinding.ReceiveTimeout = new TimeSpan(0, 1, 0);
            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(ICourses), tcpBinding, tcpBaseAddress);

            //ServiceEndpoint httpSe = Sh.AddServiceEndpoint(typeof(ICourses), new BasicHttpBinding(), httpBaseAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
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

// ----------------
// 4. Client 
// ----------------

using Serviceclient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serviceclient
{
    class Program
    {
        static void Main(string[] args)
        {
            CoursesClient CC = new CoursesClient("NetTcpBinding_ICourses");
            CC.AddToCourses(new Course() { CourseId = int.Parse(Console.ReadLine()), CourseName = Console.ReadLine() });
            CC.AddToCourses(new Course() { CourseId = int.Parse(Console.ReadLine()), CourseName = Console.ReadLine() });
            CC.AddToCourses(new Course() { CourseId = int.Parse(Console.ReadLine()), CourseName = Console.ReadLine() });
            GetCourses(CC);

            Console.ReadLine();

        }

        private static void GetCourses(CoursesClient CC)
        {
            Console.WriteLine();
            foreach (var item in CC.ListCourses())
            {
                Console.WriteLine("CourseId:{0} CourseName:{1}", item.CourseId, item.CourseName);
            }
        }
    }
}
