
For any service we have to follow 3 step process
1. Creating service
2. Hosting service 
3. Consuming service - writing a client 

Following code is step 1 . Creating service
Basically we create interface first which declares what all apis should be supported
Then we write actual service class which is implementation of the interface. 
Configuration file contains service name and method of connection 

ServiceContract and OperationContract are attributes which specify the 
information about the service. 
DataContract can be used to serialise user defined objects.
DataMember can be used for members of custom class. 

Create new project
Select WCF Service Library 
Right click project . Select Add item-> interface. 
Right click project. Select Add item-> Class
Add attribute to interface [ServiceContract]
Add attrivute to method as [OperationContract]
Add implementation to the class 

Run . You will see WCF Client. 
Visual studio will host this service and and consume as well. 

// --------------------------------------------------------------
// Service Interface 
// --------------------------------------------------------------

namespace wcfGirishServiceLibrary
{
    [ServiceContract]
    interface IGirishService
    {
        [OperationContract]
        string GetString();

        [OperationContract]
        int GAdd(int x, int y);

        [OperationContract]
        int GSub(int x, int y);

        [OperationContract]
        int GMult(int x, int y);

        [OperationContract]
        Contact XmlData(string id);
    }
}

// ---------------------------------------------
// Class used by service to return as data
// ---------------------------------------------
namespace wcfGirishServiceLibrary
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public int Roll { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}

// ----------------------------------------
// Service implementation 
// ----------------------------------------
namespace wcfGirishServiceLibrary
{
    class GirishService : IGirishService
    {
        public int GAdd(int x, int y)
        {
            return (x + y);
        }

        public int GMult(int x, int y)
        {
            return x * y;
        }

        public int GSub(int x, int y)
        {
            return x - y;
        }

        public Contact XmlData(string id)
        {
            return new Contact(){
                Address="Flat 10 nivrutti",
                Age=33,
                Name="Girish",
                Roll=11
            };
        }

        string IGirishService.GetString()
        {
            return "Girish Parshuram Lande";
        }
    }
}



