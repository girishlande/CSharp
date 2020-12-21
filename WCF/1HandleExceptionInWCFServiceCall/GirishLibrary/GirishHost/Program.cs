using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GirishLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost Sh = new ServiceHost(typeof(Girish));
            Sh.Open();

            Console.WriteLine("Started service:");
            Console.ReadLine();
        }
    }
}
