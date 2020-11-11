// Tasks are distributed across different threads 
// Management of threads is done by TPL automatically. 

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<Action> measure = (body) =>
            {
                var s1 = DateTime.Now;
                body();
                Console.WriteLine("{0} {1}", DateTime.Now - s1, Thread.CurrentThread.ManagedThreadId);
            };

            Console.WriteLine("Hello World!");
            Action c1 = () => { for (int i = 0; i < 999999999; i++) ; };

            measure(() =>
            {
                var tasks = new[] {
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1)),
            Task.Factory.StartNew(()=>measure(c1))
            };

                Task.WaitAll(tasks);
            });

            Parallel.For(0, 10, _ => { measure(c1); });

        }
    }
}
