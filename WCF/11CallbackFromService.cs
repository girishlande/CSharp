// --------------------------------------------------------------------------
// THis is demo of how to use callback mechanism while using WCF. 
// This enabled WCF service to call the client after performing some task 
// --------------------------------------------------------------------------

// ------------------
// 1. Interface
// ------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AjitLibrary
{
    [ServiceContract(CallbackContract = typeof(IMsgCallback))]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void SendEmail(string toAddress);

        // TODO: Add your service operations here
    }

    public interface IMsgCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendEmailCallBack(string toAddress);
    }

}

// ------------------
// 2. Service class
// ------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace AjitLibrary
{
   
    public class MessageService : IMessageService
    {
        public void SendEmail(string ToAddress)
        {

            try
            {
                Console.WriteLine("\nService Recived message:"+ ToAddress + " time:" + DateTime.Now);
                Thread.Sleep(2000);
                IMsgCallback cb = OperationContext.Current.GetCallbackChannel<IMsgCallback>();
                cb.SendEmailCallBack(ToAddress);
            }
            catch (Exception Ex)
            {

                throw new FaultException(Ex.Message);
            }
        }
    }
 
}


// ------------------
// 3. Host 
// ------------------

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
            Uri httpBaseAddress = new Uri("http://localhost:6789/");

            ServiceHost Sh = new ServiceHost(typeof(MessageService), new Uri[] { httpBaseAddress });

            ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IMessageService), new WSDualHttpBinding(), httpBaseAddress);


            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            Sh.Description.Behaviors.Add(smb);

            ServiceEndpoint tcpSeMex = Sh.AddServiceEndpoint(typeof(IMetadataExchange),
                                                             MetadataExchangeBindings.CreateMexHttpBinding(),
                                                             "http://localhost:6789/mex");

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


// ------------------
// 4. Client 
// ------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AjitClient.ServiceReference1;

namespace AjitClient
{
    public class MessageServiceCallback : IMessageServiceCallback
    {

        public void SendEmailCallBack(string toAddress)
        {
            Console.WriteLine("Got callback from Server:" + toAddress);
        }
    }
	
    class Program
    {
        static void Main(string[] args)
        {
            var context = new InstanceContext(new MessageServiceCallback());
            MessageServiceClient c = new MessageServiceClient(context);
			
            Console.WriteLine("Before sending message to service");
            c.SendEmail("Girish Lande");
            Console.WriteLine("After sending message to service");
            Console.ReadKey();
        }
    }
}

