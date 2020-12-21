using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int Roll { get; set; }

        [DataMember]
        public string Name { get; set; }


    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Student GetStudent(int roll);

        [OperationContractAttribute]
        string SampleMethod(string msg);

        [OperationContractAttribute(AsyncPattern = true)]
        IAsyncResult BeginSampleMethod(string msg, AsyncCallback callback, object asyncState);

        //Note: There is no OperationContractAttribute for the end method.
        string EndSampleMethod(IAsyncResult result);

        [OperationContractAttribute(AsyncPattern = true)]
        IAsyncResult BeginServiceAsyncMethod(string msg, AsyncCallback callback, object asyncState);

        // Note: There is no OperationContractAttribute for the end method.
        string EndServiceAsyncMethod(IAsyncResult result);
    }

  
}
