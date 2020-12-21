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
        static void Main(string[] args)
        {
            Console.WriteLine("Running main thread:"+Thread.CurrentThread.ManagedThreadId + " Started");
            Task.Run(() =>
            {
                Console.WriteLine("\nRunning task thread:" + Thread.CurrentThread.ManagedThreadId + " Started");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Task running :" + i);
                }
                Console.WriteLine("Running task thread:" + Thread.CurrentThread.ManagedThreadId + " Finished");
            });

            Console.WriteLine("Main thread finished:");
            Console.ReadLine();
        }

    }
}
