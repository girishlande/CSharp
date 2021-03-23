// Demo of using net pipe binding in WCF using Configuration file 

1. Create WCF library project 
2. Rename Service Interface to IAjit
3. Rename Service class to Ajit
4. Clean up functions

5. Create new subproject with console (.net framework)
6. Add reference of WCF service project
7. Add reference of System.ServiceModel
8. Add service hosting code 
9. Update App.config file 
10. Write client application.  (Console .net framework)
11. Right click and add service reference 
11. Add service calling code. 

// ----------
// Interface
// ----------
namespace AjitLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAjit
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        void Process(Byte[] array);
    }

}

// ---------------
// Service class 
// ---------------

namespace AjitLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Ajit : IAjit
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void Process(byte[] array)
        {
            Console.WriteLine("Service received byte array:" + array.Length);
            string result = System.Text.Encoding.UTF8.GetString(array);
            Console.WriteLine("Content:" + result);
        }
    }
}

// -----------------------
// Service hosting code
// -----------------------
namespace AjitHost
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

// ------------------
// App.config file 
// ------------------

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
				<endpoint address="net.pipe://localhost/MyPipeEndPoint" binding="netNamedPipeBinding"
                    bindingConfiguration="" contract="AjitLibrary.IAjit" />

				<endpoint address="net.pipe://localhost/MyPipeEndPoint/mex" binding="mexNamedPipeBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />

			</service>
		</services>
	</system.serviceModel>
</configuration>

// ----------------------
// Client application 

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int n = 1;
            while (n > 0)
            {
                var client = new AjitClient("NetNamedPipeBinding_IAjit");
                string input = "Hello";
                string path = @"D:\a.txt";
                if (File.Exists(path))
                {
                    input = File.ReadAllText(path);
                }

                byte[] bytes = Encoding.ASCII.GetBytes(input);
                Console.WriteLine("Input size:" + bytes.Length);
                try
                {
                    client.Process(bytes);
                }
                catch (CommunicationException e)
                {
                    Console.WriteLine("Comm exception:" + e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                }

                Console.WriteLine("Want to continue? 1/0:");
                var in1 = Console.ReadLine();
                n = Convert.ToInt32(in1);
            }
        }
    }
}


