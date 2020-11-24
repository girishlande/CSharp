// --------------------------------------------------------------------------------------
// THis excercise is for creating simple WCF service with some simple operations 
// --------------------------------------------------------------------------------------

> Open visual studio , Create new project , WCF service library 
> By default it gives one template interface and its corresponding class. 
> You can rename it and add some more operation as given below. 
> You can add one more interface and its corresponding class.
> You can declare some operations in interface and implement the same in the class. 
> You will have to add entry for new interface in app.config file so that it gets picked as service. 

// -----------------------
// 1. Interface 
// -----------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    public class Student {
        public int Roll { get; set; }
        public int Marks { get; set; }
    }

    [ServiceContract]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        int AddNumbers(int[] numbers);

        [OperationContract]
        int Multiply(int x, int y);

        [OperationContract]
        string ReverseMessage(string msg);

        [OperationContract]
        bool IsPass(Student stud);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GirishLibrary
{
    [ServiceContract]
    interface IAjit
    {
        [OperationContract]
        int AddNumber(int x, int y);

        [OperationContract]
        int SubtractNumber(int x, int y);
    }
}


// -----------------------
// 2. Service class 
// -----------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        public int AddNumbers(int[] numbers)
        {
            int sum = 0;
            foreach (var item in numbers)
            {
                sum += item;
            }
            return sum;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public bool IsPass(Student stud)
        {
            if (stud.Marks > 40)
                return true;
            return false;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }

        public string ReverseMessage(string msg)
        {
            char[] charArray = msg.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
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
    class Ajit : IAjit
    {
        public int AddNumber(int x, int y)
        {
            return x + y;
        }

        public int SubtractNumber(int x, int y)
        {
            return x - y;
        }
    }
}


// 3. Config file 

<?xml version="1.0" encoding="utf-8" ?>
<configuration>

  <appSettings>
    <add key="aspnet:UseTaskFriendlySynchronizationContext" value="true" />
  </appSettings>
  <system.web>
    <compilation debug="true" />
  </system.web>
  <!-- When deploying the service library project, the content of the config file must be added to the host's 
  app.config file. System.Configuration does not support config files for libraries. -->
  <system.serviceModel>
    <services>
      <service name="GirishLibrary.Girish">
        <endpoint address="" binding="basicHttpBinding" contract="GirishLibrary.IGirish">
          <identity>
            <dns value="localhost" />
          </identity>
        </endpoint>
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
        <host>
          <baseAddresses>
            <add baseAddress="http://localhost:8733/Design_Time_Addresses/GirishLibrary/Service1/" />
          </baseAddresses>
        </host>
      </service>

		<service name="GirishLibrary.Ajit">
			<endpoint address="" binding="basicHttpBinding" contract="GirishLibrary.IAjit">
				<identity>
					<dns value="localhost" />
				</identity>
			</endpoint>
			<endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
			<host>
				<baseAddresses>
					<add baseAddress="http://localhost:8733/Design_Time_Addresses/GirishLibrary/Ajit1/" />
				</baseAddresses>
			</host>
		</service>
		
    </services>
    <behaviors>
      <serviceBehaviors>
        <behavior>
          <!-- To avoid disclosing metadata information, 
          set the values below to false before deployment -->
          <serviceMetadata httpGetEnabled="True" httpsGetEnabled="True"/>
          <!-- To receive exception details in faults for debugging purposes, 
          set the value below to true.  Set to false before deployment 
          to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="False" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>

</configuration>

