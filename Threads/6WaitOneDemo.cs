using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// following program shows how to create new thread and how to user Reset Event to block execution and resume
// Execution on some event 
namespace ConsoleApp1
{
    class Program
    {
        static ManualResetEvent firstEvent = new ManualResetEvent(false);
        static ManualResetEvent secondEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var thr = new Thread(new ThreadStart(SecondThread));
            thr.IsBackground = true;
            thr.Start();

            Console.WriteLine("Main thread waiting started!");
            firstEvent.WaitOne();
            Console.WriteLine("Main thread waiting ended!");

            var isSleep = thr.ThreadState.HasFlag(ThreadState.WaitSleepJoin);
            Console.WriteLine("ISSleep:"+isSleep);
            Console.ReadLine();
        }

        static void SecondThread()
        {
            Console.WriteLine("Work starting!");
            Thread.Sleep(2000);
            Console.WriteLine("Work ending!");
            firstEvent.Set();
        }
    }
}
