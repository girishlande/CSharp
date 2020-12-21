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
            Console.WriteLine("Main Thread ID:" + Thread.CurrentThread.ManagedThreadId + " started!");
            new Thread(() =>
            {
                for(int i=0;i<3;i++)
                {
                    Console.WriteLine("Thread ID:"+Thread.CurrentThread.ManagedThreadId + " Counter:"+i);
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Thread ID:" + Thread.CurrentThread.ManagedThreadId + " Finished!");
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Thread ID:" + Thread.CurrentThread.ManagedThreadId + " Counter:" + i);
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Thread ID:" + Thread.CurrentThread.ManagedThreadId + " Finished!");
            }).Start();

            Console.WriteLine("Main Thread ID:" + Thread.CurrentThread.ManagedThreadId + " Stopped!");
            Console.ReadLine();
        }

    }
}
