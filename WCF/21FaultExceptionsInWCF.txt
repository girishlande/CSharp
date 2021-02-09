56

On your service operation, you can specify a FaultContract that will serve both purposes like so:

[OperationContract]
[FaultContract(typeof(MyServiceFault))]
void MyServiceOperation();

Note that MyServiceFault must be marked with DataContract and DataMember attributes, 
in the same way you would a complex type:

[DataContract]
public class MyServiceFault
{
    private string _message;

    public MyServiceFault(string message)
    {
        _message = message;
    }

    [DataMember]
    public string Message { get { return _message; } set { _message = value; } }
}
On the service-side, you are then able to:

throw new FaultException<MyServiceFault>(new MyServiceFault("Unauthorized Access"));
And on the client-side:

try
{
    ...
}
catch (FaultException<MyServiceFault> fault)
{
    // fault.Detail.Message contains "Unauthorized Access"
}