using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void function1()
        {
            Console.WriteLine("Function 1 started");
            Console.WriteLine("Function 1 ended");
        }

        static void function2()
        {
            Console.WriteLine("Function 2 started");
            Console.WriteLine("Function 2 ended");
        }

        static void Main(string[] args)
        {
            var t1 = new Thread(function1);
            var t2 = new Thread(function2);

            t2.Start();
            t1.Start();
            

            t1.Join();
            Console.WriteLine("Thread 1 endded");
            t2.Join();
            Console.WriteLine("Thread 2 endded");

            Console.WriteLine("Main thread ended");

        }

    }
}
