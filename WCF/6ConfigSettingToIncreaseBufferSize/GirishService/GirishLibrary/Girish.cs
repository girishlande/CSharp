using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Girish : IGirish
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void Process(byte[] array)
        {
            Console.WriteLine("Service received byte array:" + array.Length);
            //string result = System.Text.Encoding.UTF8.GetString(array);
            //Console.WriteLine("Content:" + result);
        }
    }
}
