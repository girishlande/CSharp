using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread:"+Thread.CurrentThread.ManagedThreadId);
            var t1 = new System.Timers.Timer();
            t1.Interval = 1000;
            t1.Elapsed += f1;
            t1.Start();

            Console.ReadLine();
        }

        private static void f1(Object source, EventArgs args)
        {
            Console.WriteLine("Hello Girish . Timer Thread:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
