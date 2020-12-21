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

            var t = new TaskCompletionSource<bool>();

            new Thread(() =>
            {
                Console.WriteLine("New task started!");
                Thread.Sleep(2000);
                Console.WriteLine("New task finished!");
                t.TrySetResult(true);
            }).Start();

            Console.WriteLine("Thread started!!");
            var output = t.Task.Result;
            Console.WriteLine("Task result:"+output);

        }

    }
}
