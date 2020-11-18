// ---------------------------------------------------------------------------------------------------------------
// This explains how to create a simple WCF service. And write a client application to call service operations. 
// Run it using Visual studio (VS will host service and will also provide a client application for testing operations)
// This service can be consumed using another project.
// ---------------------------------------------------------------------------------------------------------------

// --------------------------------------------------------------------
// STEP 1: Creating service. Hosting and Running using Visual studio. 
// --------------------------------------------------------------------
> Create a new project in Visual studio 
> Select C# and project type "Service" and WCF service library from the template
> Project name = GirishServiceLibrary and Solution name GirishServiceLibrary
> BY default it will create IService1.cs and Service1.cs which is interface and concrete implmentation of the service.
> Goto IService1 and delete default composite type
> Rename IService1 to IGirishService
> Keep only one operation contract GetData() 
> Rename IService class to GirishService class (It will automatically rename a file as well)
> Remove composite methods from class as well.
> Now you should be able to run this service. Visual studio will host it and provide a client to invoke operations provided
  by service. WIth this client you can test your service operations. 
> Service library project will produce a DLL file 
  for e.g D:\test\GirishServiceLibrary\GirishServiceLibrary\bin\Debug\GirishServiceLibrary.dll
> To test this service, you will need to create a client and use it to call the service. You can do this using the svcutil.exe   tool from the command line.  
  
// --------------------------------------------------------------------
// STEP 2: Write a client application to consume the service. 
// --------------------------------------------------------------------

// At this point you can go to App.config file of your service project and look at the contents
// It will have information about Service such as 2 end points 
// One End point is for actual service and other end point is for meta data exchange. 
// Meta data exchange point will also have the URL which you can use to access the service using browser. 

/*
        <service name="GirishServiceLibrary.GirishService">
          
        <endpoint address="" binding="basicHttpBinding" contract="GirishServiceLibrary.IGirishService">
          <identity>
            <dns value="localhost" />
          </identity>
        </endpoint>
          
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
        <host>
          <baseAddresses>
            <add baseAddress="http://localhost:8733/Design_Time_Addresses/GirishServiceLibrary/Service1/" />
          </baseAddresses>
        </host>
          
      </service> 
*/

> Use metadata exchange address to open service in web browser. It will provide you further steps to 
  invoke this service from console application. Basically it tells you to use svcutil.exe to create service client and config file which you can use to write client application for your service. 

> Open visual studio developer command prompt. Run as administrator. Goto D: and fire the command suggested in web browser. 
  for e.g see output below. IT should create 2 files. 

C:\Windows\System32>d:
D:\>svcutil.exe http://localhost:8733/Design_Time_Addresses/GirishServiceLibrary/Service1/?wsdl
Microsoft (R) Service Model Metadata Tool
[Microsoft (R) Windows (R) Communication Foundation, Version 4.8.3928.0]
Copyright (c) Microsoft Corporation.  All rights reserved.

Attempting to download metadata from 'http://localhost:8733/Design_Time_Addresses/GirishServiceLibrary/Service1/?wsdl' using WS-Metadata Exchange or DISCO.
Generating files...
D:\GirishService.cs
D:\output.config

>This will generate a configuration file and a code file that contains the client class. Add the two files to your client application and use the generated client class to call the Service.

> Create a new project in your solution. Create C# console App (.NET framework) project
> Name it as GirishServiceClient. 
> Copy GirishService.cs and output.config file in this project folder. 
> Add GirishService.cs file in this project. 
> Right click on the project and Add reference to System.servicemodel 
> Open output.config file and copy its content in App.config file.
> Add following code in the Program.cs to invoke service operation 

GirishServiceClient client = new GirishServiceClient();
// Use the 'client' variable to call operations on the service.
var output = client.GetData(11);
Console.WriteLine("Service output:"+output);
Console.ReadLine();
// Always close the client.
client.Close();



Console.WriteLine("Enter a number(0 to exit):");
int t = Convert.ToInt32(Console.ReadLine());
            while (t != 0)
            {
                var output = client.GetData(t);
                Console.WriteLine("Service output:" + output);
                Console.WriteLine("Enter a number(0 to exit):");
                t = Convert.ToInt32(Console.ReadLine());
            }
            // Always close the client.
            client.Close();
			

// You can write a WPF client in similar fashion and send data to service which is entered by user. 
            private readonly Random _random = new Random();
            Console.WriteLine("Button is clicked:" + text1.Text);
            int t = _random.Next(1000); //Convert.ToInt32(text1.Text);  // take user input or generate random number 
            SuhasServiceClient client = new SuhasServiceClient();

            // Use the 'client' variable to call operations on the service.
            var output = client.GetData(t);
            Console.WriteLine("Service output:" + output);
            text2.Text = "Service output:" + output;

            // Always close the client.
            client.Close();






















 Uri baseAddress = new Uri("http://localhost:6789");
 ServiceHost Sh = new ServiceHost(typeof(GirishService), baseAddress);
 ServiceEndpoint Se = Sh.AddServiceEndpoint(typeof(IGirishService), new WSHttpBinding(), baseAddress);

  //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
  //smb.HttpGetEnabled = true;
  //Sh.Description.Behaviors.Add(smb);

  Sh.Open();
  Console.WriteLine("Started.....");
  Console.ReadLine();

  Sh.Close();