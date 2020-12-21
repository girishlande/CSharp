using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract = typeof(IGirishCallback))]
    public interface IGirish
    {
        [OperationContract(IsOneWay =true)]
        void GetData(int value);


        [OperationContract(IsOneWay = true)]
        void IsOnline();

    }

    public interface IGirishCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendDataCallback(string data);


        [OperationContract(IsOneWay = true)]
        void SendOnlineCallback(bool flag);
    }
}
