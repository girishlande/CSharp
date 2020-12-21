using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract = typeof(IProgressCallback))]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract(IsOneWay = true)]
        void StartProgress(int target);
    }

    [ServiceContract]
    public interface IProgressCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateProgress(int progress);
    }

  



}
