using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Girish : IGirish
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void StartProgress(int target)
        {
            int progress = 0;
            double step = (double)target / 40.0;
            while (progress < target)
            {
                Thread.Sleep(100);
                progress += (int)step;
                Console.WriteLine("Progress updated:" + progress);
                IProgressCallback cb = OperationContext.Current.GetCallbackChannel<IProgressCallback>();
                cb.UpdateProgress(progress);
            }
        }

    }
}
