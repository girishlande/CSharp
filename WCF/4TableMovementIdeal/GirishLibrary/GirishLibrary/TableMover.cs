using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    public class TableMover : ITableMover
    {
        int _Position = 0;

        public void GetPosition()
        {
            SendCurrentTablePosition();
        }

        public void IsOnline()
        {
            ITableMoverCallback cb = OperationContext.Current.GetCallbackChannel<ITableMoverCallback>();
            cb.SendOnlineStatus(true);
        }

        public void MoveTable(int targetPosition)
        {
            Console.WriteLine("Request recived: Move Table target Position:"+targetPosition);

            bool validInput = (targetPosition >= 0 && targetPosition <= 100);
            if (!validInput)
            {
                Console.WriteLine("Invalid Input targetPosition");
                return;
            }
            if (targetPosition == _Position)
            {
                Console.WriteLine("Target Position same as Current position. No movement required");
                return;
            }

            int movement = _Position - targetPosition;
            int absmovement = Math.Abs(movement);
            bool negative = (movement < 0);
            int interval = 10;
            int step = (absmovement / interval) * (negative?1:-1);
            Console.WriteLine("step:"+step);
            while(absmovement>Math.Abs(step))
            {
                _Position += step;
                Console.WriteLine("new Position:"+_Position);
                SendCurrentTablePosition();
                movement = _Position - targetPosition;
                absmovement = Math.Abs(movement);
                Thread.Sleep(200);
            }
            if (_Position != targetPosition)
            {
                _Position = targetPosition;
                Console.WriteLine("new Position:" + _Position);
                SendCurrentTablePosition();
            }
        }

        public void SendCurrentTablePosition()
        {
            ITableMoverCallback cb = OperationContext.Current.GetCallbackChannel<ITableMoverCallback>();
            cb.SendTablePosition(_Position);
        }
       
    }
}
