using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "Hello";
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            var service = new GirishServiceReference.GirishClient();
            try
            {
                service.Process(bytes);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception:"+e.Message);
            }
        }
    }
}
