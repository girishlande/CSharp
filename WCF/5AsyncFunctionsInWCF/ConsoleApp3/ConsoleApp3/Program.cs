using ConsoleApp3.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            GirishClient client = new GirishClient();
            var x = client.ServiceAsyncMethod("Hello Girish");
            Console.WriteLine("output:" + x);

            var y = client.ServiceAsyncMethodAsync("Hello Ajit");
            Console.WriteLine("output:"+ y.Result);

        }
    }
}
