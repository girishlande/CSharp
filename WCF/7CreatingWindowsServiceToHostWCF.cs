
// -----------------------------------------------
// Hosting WCF service on Windows service. 
// -----------------------------------------------

1. Create simple WCF service and Test service using Visual studio hosting and client test application 

2. Create new Sub project "Windows Service" and Rename default service to MyServiceHost.

3. Add reference of WCF library project 
   >Right click on References and Add reference. Select WCF service project from reference and add it
   >Right click on Reference and Add reference. Select Assemblies and select System.ServiceModel 
   >Build this . It should succeed and should copy the service library dll in bin folder. 
 
4. Add service End point and MEx end point  
   > First Service end point. 
   > Right click on App.config , select edit WCF configuration , Create service. Select dll from its bin folder. 
   > tcp.net:/localhost:9091/myservice
   
   > Lets add mex end point now. In declarative manner 
   > Copy template mex end point from some other configuration file. 
      <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                    bindingConfiguration="" contract="IMetadataExchange" />
   > Add behavior under <system.serviceModel>
     <behaviors>
        <serviceBehaviors>
          <behavior name="MyBeh">
            <serviceMetadata/>
          </behavior>
        </serviceBehaviors>
      </behaviors>
	> set behaviorConfiguration of service to MyBeh
	
5. Double click on MyServiceHost and it will open design. Click on switch to code. 
   Add namespace
   using GirishServiceLibrary;
   using System.ServiceModel;
   
   Add ServiceHost object
   ServiceHost Sh = null; // Member
   Start() {
      Sh = new ServiceHost(typeof(GIrishService), new Uri("net.tcp://localhost:9091/myService"));
	  Sh.Open();
   }
   
   Stop() {
	   if (Sh!=null) {
		   Sh.close();
	   }
	   sh = null;
   }
	
6. Double click on MyServiceHost and it will open design
   Right click in design window and select "Add installer"
7. It will create ProjectInstaller.cs
   There will be ServiceProcessInstaller1.cs 
   Select that and go to properties
   Change the account to Network service so that it can be accessed over network 
8. Select ServiceInstaller.cs and go to properties
   Select start type to "Automatic"
   Change display name 
9. Now lets install this windows service. First build this service project.

10. Open Visual studio command prompt using Admin mode
11. Go to bin folder . for e.g D:\Test\GirishServiceLibrary\MyWCFServiceHost\bin\Debug
12. Fire this command. 
     > installutil.exe MyWCFServiceHost.exe
	 
13. This should start the service. Go to windows services and check if its there. 

14. Now Create another client application. C# console application is fine. 
    Right click on project Add -> ServiceReference
	Copy URL of your service and check if it finds that. 
	It should find that URL 
	NOW stop the windows service and again check if it can find that . 
	
15. Unintall the windows service
    > installutil.exe MyWCFServiceHost.exe /u