using GirishLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var sh = new ServiceHost(typeof(Girish));
            sh.Open();
            Console.WriteLine("Started service...");
            foreach (var item in sh.Description.Endpoints)
            {
                Console.WriteLine("Address: " + item.Address.ToString());
                Console.WriteLine("Binding: " + item.Binding.Name.ToString());
                Console.WriteLine("Contract: " + item.Contract.Name.ToString());
                Console.WriteLine();
            }

            Console.ReadLine();

            sh.Close();
            Console.ReadLine();
        }
    }
}
