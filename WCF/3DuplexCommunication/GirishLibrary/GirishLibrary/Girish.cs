using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        public void GetData(int value)
        {
            Thread.Sleep(4000);
            string data =  string.Format(" Square of {0} is : {1} ", value, value*value);
            IGirishCallback cb = OperationContext.Current.GetCallbackChannel<IGirishCallback>();
            cb.SendDataCallback(data);
        }
       
        public void IsOnline()
        {
            IGirishCallback cb = OperationContext.Current.GetCallbackChannel<IGirishCallback>();
            cb.SendOnlineCallback(true);
        }
    }
}
