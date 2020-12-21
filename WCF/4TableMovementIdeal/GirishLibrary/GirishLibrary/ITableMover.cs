using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract = typeof(ITableMoverCallback))]
    public interface ITableMover
    {
        [OperationContract(IsOneWay = true)]
        void GetPosition();

        [OperationContract(IsOneWay = true)]
        void MoveTable(int targetPosition);

        [OperationContract(IsOneWay = true)]
        void IsOnline();
    }

    public interface ITableMoverCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendTablePosition(int position);


        [OperationContract(IsOneWay = true)]
        void SendOnlineStatus(bool flag);
    }

}
